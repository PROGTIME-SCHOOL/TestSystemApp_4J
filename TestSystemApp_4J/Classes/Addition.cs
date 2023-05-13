using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;

namespace TestSystemApp_4J.Classes
{
    public static class Addition
    {
        public static NavigationService NavigationService { get; } = 
            (Application.Current.MainWindow as MainWindow)?.frame.NavigationService;
    }
}
