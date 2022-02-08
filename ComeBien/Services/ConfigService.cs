using ComeBien.Models.Globals;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    public class ConfigService : IConfigService
    {
        private string _lang;
        public string Lang
        {
            get => _lang;
            set => _lang = value;
        }

        private bool _isLogged;
        public bool IsLogged
        {
            get => _isLogged;
            set => _isLogged = value;
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }

        public ConfigService()
        {

        }

        public void InitDefaults()
        {
            _lang = Languages.Spanish;
            _isLogged = false;
            _userName = "";
        }

        public void Load()
        {
            _lang = Read("lang");
            _userName = Read("userName");
            _isLogged = Read("isLogged") == "true";
        }

        private string Read(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private void Write(Configuration configFile, string key, string value)
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

        public void Save()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            Write(configFile, "lang", _lang);
            Write(configFile, "isLogged", _isLogged == true ? "true" : "false");
            Write(configFile, "userName", _userName);
            
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }

    public interface IConfigService
    {
        string Lang { get; set; }
        string UserName { get; set; }
        bool IsLogged { get; set; }
        void Save();
        void Load();
        void InitDefaults();
    }
}
