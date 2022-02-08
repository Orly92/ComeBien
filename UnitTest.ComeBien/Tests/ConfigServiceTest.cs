using ComeBien.Models.Globals;
using ComeBien.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ComeBien.Tests
{
    [TestClass]
    public class ConfigServiceTest
    {
        private IConfigService _configService;

        public ConfigServiceTest()
        {
            _configService = new ConfigService();
        }

        [TestMethod]
        public void SaveAndLoadValuesTest()
        {
            _configService.Load();
            bool originalIsLogged = _configService.IsLogged;
            string originalUserName = _configService.UserName;
            string originalLang = _configService.Lang;

            bool actualIsLogged = false;
            string actualLang = Languages.France;
            string actualUserName = "prueba";

            _configService.InitDefaults();

            _configService.IsLogged = actualIsLogged;
            _configService.Lang = actualLang;
            _configService.UserName = actualUserName;

            _configService.Save();
            _configService.InitDefaults();
            _configService.Load();

            Assert.AreEqual(_configService.IsLogged, actualIsLogged);
            Assert.AreEqual(_configService.UserName, actualUserName);
            Assert.AreEqual(_configService.Lang, actualLang);

            //Restaurando valores
            _configService.IsLogged = originalIsLogged;
            _configService.UserName = originalUserName;
            _configService.Lang = originalLang;
        }
    }
}
