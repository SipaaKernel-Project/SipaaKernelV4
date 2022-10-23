using PrismGL2D;

namespace SipaaKernel.UI.Components
{
    public class TopBar
    {
        public TopBar(uint Width, uint Height)
		{
            G = new(Width, Height);
		}

        public Graphics G;

        public Graphics Render()
        {
            G.Clear();
            G.DrawFilledRectangle(0, 0, G.Width, G.Height, 0, 0x323232);
            G.DrawString((int)G.Width / 2, (int)G.Height / 2, Cosmos.HAL.RTC.Hour + " : " + Cosmos.HAL.RTC.Minute, Font.Fallback, Color.White, true);
            return G;
        }
        public void Draw(Graphics mainGraphics)
        {
            mainGraphics.DrawImage(0, 0, Render(), false); // the topbar will never be rounded lol
        }
    }
}