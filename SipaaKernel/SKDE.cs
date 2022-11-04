using Cosmos.Core;
using PrismGL2D;
using SipaaKernel.UI.Widgets;
using SipaaKernel.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel
{
    public class SKDE
    {
        #region Needed
        private class TopBar
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
        #endregion
        public Graphics Wallpaper;
        private TopBar topBar;
        public uint TopBarHeight { get => topBar.G.Height; }

        private Button AboutButton;

        public void Initialize()
        {
            //Wallpaper = Assets.Wallpaper;
            topBar = new TopBar(VBE.getModeInfo().width, 24);
            AboutButton = new Button();
            AboutButton.Y = 560;
            AboutButton.X = 0;
            AboutButton.Width = 40;
            AboutButton.Height = 40;
            AboutButton.Text = "A";
            AboutButton.OnClick = () => { AboutApp.Main(); };
        }

        public void Draw(Graphics g)
        {
            g.Clear(Color.GoogleBlue);
            //g.DrawImage(0, 0, Assets.Wallpaper, false);
            topBar.Draw(g);

            // Draw launcher
            //g.DrawFilledRectangle(0, 560, 800, 40, 0, Color.Black);
            AboutButton.OnDraw(g);
        }

        public void Update()
        {
            AboutButton.OnUpdate();
        }
    }
}
