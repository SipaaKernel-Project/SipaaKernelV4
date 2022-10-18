using SipaaKernel.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.SysTheme2.Themes
{
    public class LightTheme : Theme
    {
        protected override ColorDictionnary InitColorDictionnary()
        {
            ColorDictionnary cd = new ColorDictionnary();
            cd.AccentIdle = Color.FromHex("0066ff");
            cd.AccentHover = Color.FromHex("1874ff");
            cd.AccentHover = Color.FromHex("2e81ff");
            cd.AccentForeground = Color.FromHex("ffffff");

            cd.WidgetIdle = Color.FromHex("fafafa");
            cd.WidgetHover = Color.FromHex("e3e3e4");
            cd.WidgetClicked = Color.FromHex("cfcfd0");

            cd.WindowBackground = Color.FromARGB(255, 230, 230, 230);
            cd.Component = Color.FromARGB(255, 210, 210, 210);

            cd.Foreground = Color.FromARGB(255, 0, 0, 0);

            return cd;
        }
    }
}
