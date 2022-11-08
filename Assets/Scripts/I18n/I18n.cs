using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.I18n
{
    public class I18n : Mgl.I18n
    {
        protected static readonly I18n instance = new I18n();

        protected static string[] locales = new string[] {
            "es-ES",
            "en-US"
        };

        public static I18n Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
