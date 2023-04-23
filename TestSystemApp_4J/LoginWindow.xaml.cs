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
using System.Windows.Shapes;
using TestSystemApp_4J.Classes;
using TestSystemApp_4J.Models;

namespace TestSystemApp_4J
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MainWindow mainWindow;
        public LoginWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = DataAccessLayer.SignIn(txtLogin.Text, txtPassword.Text);

            if (user != null)
            {
                mainWindow.LoadProgram(user);
                mainWindow.Show();
                this.Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!mainWindow.IsVisible)
            {
                mainWindow.Close();
            }
        }
    }
}
