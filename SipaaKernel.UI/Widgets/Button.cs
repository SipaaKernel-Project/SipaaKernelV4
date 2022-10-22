using Cosmos.System;
using SipaaKernel.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.Widgets
{
    public class Button : Widget
    {
        public override void OnDraw(FrameBuffer Buffer)
        {
            FrameBuffer buf = RenderWidget();
            if (IsAccentued)
            {
                buf.DrawString((int)this.Width / 2, (int)this.Height / 2, Text, Font.Default, Theme.GetAccentForegroundColor(), true);
            }
            else
            {
                buf.DrawString((int)this.Width / 2, (int)this.Height / 2, Text, Font.Default, Theme.GetForegroundColor(), true);
            }
            Buffer.DrawImage((int)X, (int)Y, buf);
        }
    }
}
