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
using TestSystemApp_4J.Pages;

namespace TestSystemApp_4J
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestSystemContext db;
        public MainWindow()
        {
            InitializeComponent();

            //DataAccessLayer layer = new DataAccessLayer();
            //layer.LoadDataToDb();

            db = new TestSystemContext();
            db.Database.EnsureCreated();

            Addition.NavigationService?.Navigate(new LoginPage(new MainPage(db)));
        }
    }
}
