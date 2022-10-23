using Cosmos.System;
using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.Widgets
{
    public class Button : Widget
    {
        public override void OnDraw(Graphics Buffer)
        {
            Graphics buf = RenderWidget();
            if (IsAccentued)
            {
                buf.DrawString((int)this.Width / 2, (int)this.Height / 2, Text, Font.Fallback, Theme.GetAccentForegroundColor(), true);
            }
            else
            {
                buf.DrawString((int)this.Width / 2, (int)this.Height / 2, Text, Font.Fallback, Theme.GetForegroundColor(), true);
            }
            if (Theme.GetBorderRadius() > 0)
            {
                buf.DrawImage((int)X, (int)Y, RenderWidget());
            }
            else
            {
                buf.DrawImage((int)X, (int)Y, RenderWidget(), false);
            }
        }
    }
}
