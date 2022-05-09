using ComeBien.Models.Globals;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    public class ConfigService
    {
        public static string lang { get; set; }
        public static bool isLogged { get; set; }
        public static string userName { get; set; }

        public static void InitDefaults()
        {
            lang = Languages.Spanish;
            isLogged = false;
            userName = "";
        }

        public static void Load()
        {
            Log.Information("Cargando datos de configuración");
            lang = Read("lang");
            userName = Read("userName");
            isLogged = Read("isLogged") == "true";

            Log.Information("Datos cargados correctamente");
        }

        private static string Read(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static void Write(Configuration configFile, string key, string value)
        {
            var setting = configFile.AppSettings.Settings;

            if (setting[key] == null)
            {
                setting.Add(key, value);
            }
            else
            {
                setting[key].Value = value;
            }
        }

        public static void Save()
        {
            Log.Information("Salvando los datos de configuración");
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            Write(configFile, "lang", lang);
            Write(configFile, "isLogged", isLogged == true ? "true" : "false");
            Write(configFile, "userName", userName);

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            Log.Information($"Valores salvados lang={lang},isLogged={isLogged},userName={userName}");
        }
    }
}
