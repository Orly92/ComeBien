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
        [TestMethod]
        public void SaveAndLoadValuesTest()
        {
            ConfigService.Load();
            bool originalIsLogged = ConfigService.isLogged;
            string originalUserName = ConfigService.userName;
            string originalLang = ConfigService.lang;

            bool actualIsLogged = false;
            string actualLang = Languages.France;
            string actualUserName = "prueba";

            ConfigService.InitDefaults();

            ConfigService.isLogged = actualIsLogged;
            ConfigService.lang = actualLang;
            ConfigService.userName = actualUserName;

            ConfigService.Save();
            ConfigService.InitDefaults();
            ConfigService.Load();

            Assert.AreEqual(ConfigService.isLogged,actualIsLogged);
            Assert.AreEqual(ConfigService.userName, actualUserName);
            Assert.AreEqual(ConfigService.lang, actualLang);

            //Restaurando valores
            ConfigService.isLogged = originalIsLogged;
            ConfigService.userName = originalUserName;
            ConfigService.lang = originalLang;
        }
    }
}
