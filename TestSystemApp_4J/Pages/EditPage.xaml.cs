using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestSystemApp_4J.Classes;
using TestSystemApp_4J.Models;

namespace TestSystemApp_4J.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>

    public enum TypeOfOperation { Delete, ChangeOrAdd }
    public enum TableName { Test, Question, User }

    public partial class EditPage : Page
    {
        private TestSystemContext db;

        private List<(TypeOfOperation typeOfOperation, TableName tableName, object adaptedField, object? additionData)> changes =
            new List<(TypeOfOperation, TableName, object, object?)>();

        public EditPage(TestSystemContext db)
        {
            InitializeComponent();

            this.db = db;

            comboBoxTables.Items.Add("Test&Questions");
            comboBoxTables.Items.Add("User");

            testDataGrid.ItemsSource = DataAccessLayer.GetAllTests();
            userDataGrid.ItemsSource = DataAccessLayer.GetAllUsers();

            comboBoxTables.SelectedIndex = 0;
            ChangeVisibleDataGrid();
        }

        private void backToMainPage_Click(object sender, RoutedEventArgs e)
        {
            Addition.NavigationService.GoBack();
        }

        private void comboBoxTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeVisibleDataGrid();
        }

        private void ChangeVisibleDataGrid()
        {
            if (comboBoxTables.SelectedIndex == 0)
            {
                userDataGrid.Visibility = Visibility.Hidden;
                testDataGrid.Visibility = Visibility.Visible;
                questionDataGrid.Visibility = Visibility.Visible;
            }
            else if (comboBoxTables.SelectedIndex == 1)
            {
                userDataGrid.Visibility = Visibility.Visible;
                testDataGrid.Visibility = Visibility.Hidden;
                questionDataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void testDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = testDataGrid.SelectedItem as Test;

            if (test != null)
            {
                questionDataGrid.ItemsSource = DataAccessLayer.GetQuestionsForTest(test.Id);
                questionDataGrid.IsEnabled = true;
            }
            else
            {
                questionDataGrid.ItemsSource = null;
                questionDataGrid.IsEnabled = false;
            }
        }

        private void RefreshDataGrid()
        {
            if (comboBoxTables.SelectedIndex == 0)
            {
                testDataGrid.Items.Refresh();
                questionDataGrid.Items.Refresh();
            }
            else
            {
                userDataGrid.Items.Refresh();
            }
        }

        private bool _handle = true;
        private void RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (_handle)
            {
                _handle = false;
                userDataGrid.CommitEdit();

                var tempDataGrid = sender as DataGrid;

                if (tempDataGrid == tempDataGrid && e.Row.Item is Test test)
                {
                    changes.Add((TypeOfOperation.ChangeOrAdd, TableName.Test, test, null));
                }

                if (tempDataGrid == questionDataGrid && e.Row.Item is Question question)
                {
                    changes.Add((TypeOfOperation.ChangeOrAdd, TableName.Question, question, testDataGrid.SelectedItem));
                }

                if (tempDataGrid == userDataGrid && e.Row.Item is User user)
                {
                    changes.Add((TypeOfOperation.ChangeOrAdd, TableName.User, user, null));
                }

                ValidateOperations();

                _handle = true;
            }
        }

        private void UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            if (_handle)
            {
                var tempDataGrid = sender as DataGrid;

                _handle = false;

                tempDataGrid?.CommitEdit();

                if (tempDataGrid == tempDataGrid && e.Row.Item is Test test)
                {
                    changes.Add((TypeOfOperation.Delete, TableName.Test, test, null));
                }

                if (tempDataGrid == questionDataGrid && e.Row.Item is Question question)
                {
                    changes.Add((TypeOfOperation.Delete, TableName.Question, question, testDataGrid.SelectedItem));
                }

                if (tempDataGrid == userDataGrid && e.Row.Item is User user)
                {
                    changes.Add((TypeOfOperation.Delete, TableName.User, user, null));
                }

                ValidateOperations();

                _handle = true;
            }
        }

        private void ValidateOperations()
        {
            for (int i = changes.Count - 1; i > 0; i--)
            {
                if (changes[i].typeOfOperation == TypeOfOperation.Delete)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (changes[j].adaptedField == changes[i].adaptedField)
                        {
                            changes.RemoveAt(j);
                            j--;
                            i--;
                        }
                    }

                    changes.RemoveAt(i);
                }
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Сортировка для связки многие ко многим
            changes.Sort((x, y) => (x.tableName == TableName.Test || x.tableName == TableName.User) ?
            y.tableName == TableName.Question ? -1 : 0 : y.tableName == TableName.Test || y.tableName == TableName.User ? 1 : 0);

            if (changes.Count <= 0)
                return;

            foreach (var item in changes)
            {
                if (item.typeOfOperation == TypeOfOperation.ChangeOrAdd)
                {
                    switch (item.tableName)
                    {
                        case TableName.Test:
                            if (item.adaptedField is not Test)
                                break;

                            var testItem = (Test)item.adaptedField;

                            if (testItem.Id == 0)
                                db.Test.Add(testItem);
                            else
                                db.Test.Update(testItem);

                            db.SaveChanges();
                            RefreshDataGrid();
                            break;

                        case TableName.Question:
                            if (item.adaptedField is not Question || item.additionData == null || item.additionData is not Test)
                                break;

                            var questionItem = (Question)item.adaptedField;
                            var tempTestItem = (Test)item.additionData;

                            if (questionItem.Id == 0)
                            {
                                db.Question.Add(questionItem);

                                db.SaveChanges();
                                RefreshDataGrid();

                                tempTestItem.TestQuestions?.Add(new TestQuestion()
                                {
                                    Test = tempTestItem,
                                    Question = questionItem
                                });

                                //db.TestQuestion.Add(testQuestion);
                            }
                            else
                                db.Question.Update(questionItem);

                            db.SaveChanges();
                            break;

                        case TableName.User:
                            if (item.adaptedField is not User)
                                break;

                            var userItem = (User)item.adaptedField;

                            if (userItem.Id == 0)
                                db.User.Add(userItem);
                            else
                                db.User.Update(userItem);

                            db.SaveChanges();
                            break;
                        default:
                            break;
                    }
                }
                if (item.typeOfOperation == TypeOfOperation.Delete)
                {
                    switch (item.tableName)
                    {
                        case TableName.Test:
                            if (item.adaptedField is not Test)
                                break;

                            var testItem = (Test)item.adaptedField;

                            db.Test.Remove(testItem);

                            db.TestQuestion.AsNoTracking().Where(x => x.TestId == testItem.Id).ToList()
                                .ForEach(x => db.TestQuestion.Remove(x));

                            db.SaveChanges();
                            break;
                        case TableName.Question:
                            if (item.adaptedField is not Question)
                                break;

                            var questonItem = (Question)item.adaptedField;

                            db.Question.Remove(questonItem);

                            db.TestQuestion.AsNoTracking().Where(x => x.QuestionId == questonItem.Id).ToList()
                                .ForEach(x => db.TestQuestion.Remove(x));

                            db.SaveChanges();
                            break;
                        default:
                            break;
                    }
                }
            }

            changes.Clear();
            RefreshDataGrid();
        }
    }
}