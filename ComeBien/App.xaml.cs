using ComeBien.Models.Globals;
using ComeBien.Services;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IConfigService, ConfigService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IConfigService configService = _serviceProvider.GetService<IConfigService>();
            MainWindow mainWindow = _serviceProvider.GetService<MainWindow>();

            configService.Load();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.
                CultureInfo(Languages.LanguagesReference[configService.Lang]);

            mainWindow.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            IConfigService configService = _serviceProvider.GetService<IConfigService>();
            configService.Save();
        }
    }
}
