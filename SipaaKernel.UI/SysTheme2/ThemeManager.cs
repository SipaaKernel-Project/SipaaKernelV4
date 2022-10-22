using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.SysTheme2
{
    public class ThemeManager
    {
        private static Theme CurrentTheme = new Themes.DarkTheme();

        public static Theme GetCurrentTheme()
        {
            return CurrentTheme;
        }
        public static void SetCurrentTheme(Theme theme)
        {
            if (theme != null)
                CurrentTheme = theme;
        }
    }
}
