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

namespace TestSystemApp_4J
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Question> questions = new List<Question>();
        private User user;
        private Result result = new Result();

        public MainWindow()
        {
            InitializeComponent();

            //DataAccessLayer layer = new DataAccessLayer();
            //layer.LoadDataToDb();

            this.Hide();

            LoginWindow loginWindow = new LoginWindow(this);
            loginWindow.Show();
        }

        public void LoadProgram(User user)
        {
            TestSystemContext context = new TestSystemContext();
            questions = context.Question.Select(x => x).ToList();

            //user = context.User.FirstOrDefault(x => x == user);
            this.user = user;

            // UI
            lblQuestion.Content = questions.First().Text;
            lblUserName.Content = $"Name: {user.Login}, ID: {user.Id}";

            // LOGIC
            result.StartDateTime = DateTime.Now;
            result.MaxScores = questions.Count;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonAnswer_Clicked(object sender, RoutedEventArgs e)
        {
            Button btnAnswer = (Button)sender;

            bool userAnswer = false;

            if (btnAnswer.Content.ToString() == "YES")
            {
                userAnswer = true;
            }
            else if (btnAnswer.Content.ToString() == "NO")
            {
                userAnswer = false;
            }

            // check asnwers
            if (questions[result.NumCurrentQuestion].Answer == userAnswer)
            {
                result.Scores++;
            }

            result.NumCurrentQuestion++;   // next question

            if (result.NumCurrentQuestion < questions.Count)
            {
                // UI
                lblQuestion.Content = questions[result.NumCurrentQuestion].Text;
            }
            else   
            {
                // UI
                btnYes.IsEnabled = false;
                btnNo.IsEnabled = false;
                lblQuestion.Content = $"FINISH!\nScores: {result.Scores}/{result.MaxScores}";


                // Save data to DB
                result.EndDateTime = DateTime.Now;
                result.UserId = user.Id;
                
                TestSystemContext context = new TestSystemContext();
                context.Result.Add(result);
                context.SaveChanges();

                MessageBox.Show("Data saved to Db!");
            }

            
        }

        private void ButtonResults_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWindow = new ResultWindow();

            resultWindow.Show();
        }
    }
}
