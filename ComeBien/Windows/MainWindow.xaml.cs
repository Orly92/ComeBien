using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Globals;
using ComeBien.Services;
using ComeBien.UserControls;
using ComeBien.ViewModel;
using ComeBien.Windows;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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
        private readonly FrameworkElement _ingredientsControl;
        private readonly Brush _activeColor;
        private readonly Brush _blackColor;
        public MainWindow()
        {
            InitializeComponent();

            InitializeConfigData();

            _ingredientsControl = new IngredientsControl();

            _activeColor = (Brush)(new BrushConverter().ConvertFrom("#FF0096FF"));
            _blackColor = new SolidColorBrush(Colors.Black);
            ShoppingCartMenu.DataContext = ShoppingCartService.GetInstance().ShoppingCartMenuVM;

            SetMenuColor(MenuEnum.Home);
        }

        private void InitializeConfigData()
        {
            
            if (ConfigService.isLogged)
            {
                SetLoginView();
            }
            else
            {
                SetNotLoginView();
            }

            MenuLanguage.Header = $"_{ConfigService.lang}";
        }

        private void MenuItem_LoginClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            if (ConfigService.isLogged)
            {
                SetLoginView();
            }
        }

        private void SetLoginView()
        {
            MenuLogin.IsEnabled = false;
            MenuLogout.IsEnabled = true;
            MenuIngredients.IsEnabled = true;
            MenuOrder.IsEnabled = true;
            MenuHello.Header = $"{ConfigService.userName}";
        }

        private void MenuItem_LogoutClick(object sender, RoutedEventArgs e)
        {
            if (ConfigService.isLogged)
            {
                SetNotLoginView();
            }
        }

        private void SetNotLoginView()
        {
            MenuLogin.IsEnabled = true;
            MenuLogout.IsEnabled = false;
            MenuIngredients.IsEnabled = false;
            MenuOrder.IsEnabled = false;
            ConfigService.isLogged = false;
            ConfigService.userName = "";
            
            MenuHello.Header = ComeBien.Resources.Resources.ResourceManager.GetString("MenuSaludo");
        }

        private void MenuSpain_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage(Languages.Spanish);
        }

        private void MenuEnglish_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage(Languages.English);
        }

        private void MenuFrance_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage(Languages.France);
        }

        private void SetLanguage(string lang)
        {
            Log.Information($"Lenguaje cambiado a {lang}");

            ConfigService.lang = lang;
            MenuLanguage.Header = $"_{lang}";

        }

        private void MenuIngredients_Click(object sender, RoutedEventArgs e)
        {
            SetMenuColor(MenuEnum.Ingredients);
        }

        private void SetMenuColor(MenuEnum menu)
        {
            Log.Information($"Click en el menú {menu}");
            switch (menu)
            {
                case MenuEnum.Ingredients:
                    MenuHome.Foreground = _blackColor;
                    MenuIngredients.Foreground = _activeColor;
                    content.Content = _ingredientsControl;
                    break;

                case MenuEnum.ShoppingCart:
                    MenuHome.Foreground = _blackColor;
                    MenuIngredients.Foreground = _blackColor;
                    content.Content = new ShoppingCartControl();
                    break;

                default:
                    MenuHome.Foreground = _activeColor;
                    MenuIngredients.Foreground = _blackColor;
                    content.Content = new SalesProductControl();
                    break;
            }
        }

        private void MenuHomeItem_Click(object sender, RoutedEventArgs e)
        {
            SetMenuColor(MenuEnum.Home);
        }

        private void ShoppingCartMenu_Click(object sender, RoutedEventArgs e)
        {
            SetMenuColor(MenuEnum.ShoppingCart);
        }

        private void ExportPDF_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportJSON_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
