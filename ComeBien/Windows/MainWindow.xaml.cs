﻿using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Globals;
using ComeBien.Services;
using ComeBien.Windows;
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
        public MainWindow()
        {
            
            InitializeComponent();

            InitializeConfigData();
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
            MenuOrder.IsEnabled = true;
            MenuHello.Header = $"_Hola {ConfigService.userName}";
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
            ConfigService.lang = lang;
            MenuLanguage.Header = $"_{lang}";
        }
    }
}
