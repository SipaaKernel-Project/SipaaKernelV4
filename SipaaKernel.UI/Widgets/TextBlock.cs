using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.Widgets
{
    public class TextBlock : Widget
    {
        public override void OnDraw(Graphics Buffer)
        {
            //base.OnDraw(Buffer);
            Graphics g = new Graphics(Width, Height);
            g.DrawString(0, 0, Text, Font.Fallback, Theme.GetForegroundColor());
            Buffer.DrawImage(X, Y, g, true);
        }
    }
}
