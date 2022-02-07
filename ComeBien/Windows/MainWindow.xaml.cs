using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Globals;
using ComeBien.Windows;
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

namespace ComeBien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void MenuItem_LoginClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            if (AdminInfo.IsLogged)
            {
                MenuLogin.IsEnabled = false;
                MenuLogout.IsEnabled = true;
                MenuOrder.IsEnabled = true;
                MenuHello.Header = $"_Hola {AdminInfo.UserName}";
            }
        }

        private void MenuItem_LogoutClick(object sender, RoutedEventArgs e)
        {
            if (AdminInfo.IsLogged)
            {
                MenuLogin.IsEnabled = true;
                MenuLogout.IsEnabled = false;
                MenuOrder.IsEnabled = false;
                AdminInfo.IsLogged = false;
                AdminInfo.UserName = "";
                MenuHello.Header = "_Hola!!!";
            }
        }
    }
}
