using SipaaKernel.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.SysTheme2.Themes
{
    public class DarkTheme : Theme
    {
        protected override ColorDictionnary InitColorDictionnary()
        {
            ColorDictionnary cd = new ColorDictionnary();
            cd.AccentIdle = Color.FromHex("0066ff");
            cd.AccentHover = Color.FromHex("1874ff");
            cd.AccentHover = Color.FromHex("2e81ff");
            cd.AccentForeground = Color.FromHex("ffffff");

            cd.WidgetIdle = Color.FromHex("18181b");
            cd.WidgetHover = Color.FromHex("424245");
            cd.WidgetClicked = Color.FromHex("2e2e31");

            cd.WindowBackground = Color.FromARGB(255, 12, 12, 12);
            cd.Component = Color.FromARGB(255, 32, 32, 32);

            cd.Foreground = Color.FromARGB(255, 32, 32, 32);

            return cd;
        }
    }
}
