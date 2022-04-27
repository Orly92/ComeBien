using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Database;
using ComeBien.Models.Globals;
using ComeBien.Services;
using Serilog;
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

        public LoginWindow()
        {
            InitializeComponent();

            _administratorRepository = new AdministratorRepository();

        }

        private async void okButton_Click(object sender, RoutedEventArgs e)
        {
            await LoginUser();
        }

        private async Task LoginUser()
        {
            try
            {
                string username = userNameTextBox.Text;
                string pass = passTextBox.Text;

                Administrator admin = await _administratorRepository.GetAdmin(username, pass);

                if (admin != null)
                {
                    Log.Information($"{ComeBien.Resources.Resources.ResourceManager.GetString("UserLogin_Log")}: {username}");
                    ConfigService.isLogged = true;
                    ConfigService.userName = admin.UserName;
                    Close();
                }
                else
                {
                    Log.Information(ComeBien.Resources.Resources.ResourceManager.GetString("UserFailLogin_Log"));
                    MessageBox.Show("Las credenciales son incorrectas",
                        "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ComeBien.Resources.Resources.ResourceManager.GetString("GetAdmin_Log"));
                MessageBox.Show("Ha ocurrido un error",
                        "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
