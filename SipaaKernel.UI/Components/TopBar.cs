using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.Components
{
    public class TopBar
    {
        public Graphics Render(Graphics mainGraphics)
        {
            Graphics b = new Graphics(mainGraphics.Width, 32);
            b.DrawFilledRectangle(0, 0, b.Width, b.Height, 0, 0x323232);
            b.DrawString((int)b.Width / 2, (int)b.Height / 2, Cosmos.HAL.RTC.Hour + " : " + Cosmos.HAL.RTC.Minute, Font.Fallback, Color.White, true);
            return b;
        }
        public void Draw(Graphics mainGraphics)
        {
            Graphics b = new Graphics(mainGraphics.Width, 32);
            b.DrawFilledRectangle(0, 0, b.Width, b.Height, 0, 0x323232);
            b.DrawString((int)b.Width / 2, (int)b.Height / 2, Cosmos.HAL.RTC.Hour + " : " + Cosmos.HAL.RTC.Minute, Font.Fallback, Color.White, true);
            mainGraphics.DrawImage(0, 0, b, false); // the topbar will never be rounded lol
        }
    }
}
