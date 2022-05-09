using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Models.Globals
{
    public class Globals
    {
        
    }

    public class Languages
    {
        public const string English = "EN";
        public const string Spanish = "ES";
        public const string France = "FR";

        public static Dictionary<string, string> LanguagesReference { get; set; } = new Dictionary<string, string>
        {
            { "EN","en_US" },
            { "ES","es_ES" },
            { "FR","fr_FR" },
        };
    }

    public enum MenuEnum
    {
        Home,
        Ingredients,
        ShoppingCart
    }
}
