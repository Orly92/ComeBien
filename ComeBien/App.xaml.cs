using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Globals;
using ComeBien.Services;
using ComeBien.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Aplicación iniciada");

            ConfigService.Load();

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.
                CultureInfo(Languages.LanguagesReference[ConfigService.lang]);

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Log.Information("Guardando configuración...");
            ConfigService.Save();
            Log.Information("Aplicación apagandose...");
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Error(e.Exception, "Error");
            MessageBox.Show($"{ComeBien.Resources.Resources.ResourceManager.GetString("GlobalError_Log")}: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}
