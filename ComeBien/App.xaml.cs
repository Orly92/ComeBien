using ComeBien.Models.Globals;
using ComeBien.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ComeBien
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigService.Load();

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.
                CultureInfo(Languages.LanguagesReference[ConfigService.lang]);

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            ConfigService.Save();
        }
    }
}
