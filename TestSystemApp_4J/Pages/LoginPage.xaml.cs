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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private MainPage mainPage;

        public LoginPage(MainPage mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = DataAccessLayer.SignIn(txtLogin.Text, txtPassword.Text);

            if (user != null)
            {
                mainPage.LoadProgram(user);

                Addition.NavigationService?.Navigate(mainPage);
            }
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLoginRegister.Text;
            string password = txtPasswordRegister.Text;
            string repeatePassword = txtPasswordRepeateRegister.Text;

            if (password != repeatePassword)
            {
                MessageBox.Show("Пароли не совпадают!");
            }
            else if (password.Length <= 3)
            {
                MessageBox.Show("Пароль должен состоять из 4 символов и более!");
            }
            else if (login.Length <= 3)
            {
                MessageBox.Show("Логин должен состоять из 4 символов и более!");
            }
            else
            {
                if (DataAccessLayer.SignUp(login, password))
                {
                    MessageBox.Show("Регистрация прошла успешно! " +
                    $"Используйте login: {login} и пароль: {password} для входа в систему!");

                    txtLoginRegister.Text = "";
                    txtPasswordRegister.Text = "";
                    txtPasswordRepeateRegister.Text = "";
                }
                else
                {
                    MessageBox.Show($"Логин {login} уже присутствует в системе!" +
                        $"Пожалуйста, придумайте другой логин!");
                }
            }
        }
    }
}
