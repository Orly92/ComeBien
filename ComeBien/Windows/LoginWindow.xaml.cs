﻿using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Database;
using ComeBien.Models.Globals;
using ComeBien.Services;
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

namespace ComeBien.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAdministratorRepository _administratorRepository;
        private IConfigService _configService;

        public LoginWindow(IConfigService configService)
        {
            _administratorRepository = new AdministratorRepository();
            _configService = configService;
            InitializeComponent();
        }

        private async void okButton_Click(object sender, RoutedEventArgs e)
        {
            await LoginUser();
        }

        private async Task LoginUser()
        {
            string username = userNameTextBox.Text;
            string pass = passTextBox.Text;

            Administrator admin = await _administratorRepository.GetAdmin(username, pass);

            if (admin != null)
            {
                _configService.IsLogged = true;
                _configService.UserName = admin.UserName;
                Close();
            }
            else
            {
                MessageBox.Show("Las credenciales son incorrectas",
                    "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
