using ComeBien.Models.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    public class LanguageService
    {
        public static string GetLenguageName(string langName)
        {
            switch (langName)
            {
                case "Frances": return Languages.France;
                case Languages.English: return "Ingles";
                case Languages.Spanish: return "Español";
            }

            return "";
        }
    }
}
