using PrismGL2D;
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
            cd.AccentIdle = Color.FromARGB(255, 73, 9, 158);
            cd.AccentHover = Color.FromARGB(255, 57, 7, 122);
            cd.AccentClicked = Color.FromARGB(255, 39, 6, 82);
            cd.AccentForeground = Color.White;

            cd.WidgetIdle = Color.FromARGB(255, 250, 250, 250);
            cd.WidgetHover = Color.FromARGB(255, 227, 227, 228);
            cd.WidgetClicked = Color.FromARGB(255, 207, 207, 208);

            cd.WindowBackground = Color.FromARGB(255, 230, 230, 230);
            cd.Component = Color.FromARGB(255, 210, 210, 210);

            cd.Foreground = Color.FromARGB(255, 0, 0, 0);

            this.BorderRadius = 0;
            this.CenterWindowTitle = false;

            return cd;
        }
    }
}
