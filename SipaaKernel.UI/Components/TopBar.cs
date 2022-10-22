using SipaaKernel.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.Components
{
    public class TopBar
    {
        public FrameBuffer Render(FrameBuffer mainFrameBuffer)
        {
            FrameBuffer b = new FrameBuffer(mainFrameBuffer.Width, 32);
            b.DrawFilledRectangle(0, 0, b.Width, b.Height, 0, 0x323232);
            b.DrawString((int)b.Width / 2, (int)b.Height / 2, Cosmos.HAL.RTC.Hour + " : " + Cosmos.HAL.RTC.Minute, Font.Default, Color.White, true);
            return b;
        }
        public void Draw(FrameBuffer mainFrameBuffer)
        {
            FrameBuffer b = new FrameBuffer(mainFrameBuffer.Width, 32);
            b.DrawFilledRectangle(0, 0, b.Width, b.Height, 0, 0x323232);
            b.DrawString((int)b.Width / 2, (int)b.Height / 2, Cosmos.HAL.RTC.Hour + " : " + Cosmos.HAL.RTC.Minute, Font.Default, Color.White, true);
            mainFrameBuffer.DrawImage(0, 0, b, false);
        }
    }
}
