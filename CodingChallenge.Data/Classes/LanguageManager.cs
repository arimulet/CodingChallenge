using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodingChallenge.Data.Classes
{
    public class LanguageManager
    {
        private static Dictionary<Languages, string> languages = new Dictionary<Languages, string>() {
            { Languages.Castellano, "es" },
            { Languages.Ingles, "" }
        };


        public static void SetLanguage(Languages lang)
        {
            var langName = languages[lang];
            var cultureInfo = new CultureInfo(langName);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }
    }

    public enum Languages
    {
        Castellano = 1,
        Ingles
    }
}
