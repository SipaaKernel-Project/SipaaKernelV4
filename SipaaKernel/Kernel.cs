using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core;

using SipaaKernel.Graphics;
using SipaaKernel.UI;

using IL2CPU.API.Attribs;
using static Cosmos.Core.INTs;
using SipaaKernel.UI.Components;
using SipaaKernel.UI.Widgets;

namespace SipaaKernel
{
    [Plug(Target = typeof(Cosmos.Core.INTs))]
    
    public unsafe class INTs
    {
        public static void HandleException(uint aEIP, string aDescription, string aName, ref IRQContext ctx, uint lastKnownAddressValue = 0)
        {
            const string xHex = "0123456789ABCDEF";

            string ctxinterrupt = "";
            ctxinterrupt = ctxinterrupt + xHex[(int)((ctx.Interrupt >> 4) & 0xF)];
            ctxinterrupt = ctxinterrupt + xHex[(int)(ctx.Interrupt & 0xF)];

            string lastsknowaddress = "";

            if (lastKnownAddressValue != 0)
            {
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 28) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 24) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 20) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 16) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 12) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 8) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 4) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)(lastKnownAddressValue & 0xF)];
            }

            Kernel.g.DrawString(10, 10, "SipaaKernel CPU Exception", Font.Default, Color.White);
            Kernel.g.DrawString(10, 25, ctxinterrupt + " : " + aDescription, Font.Default, Color.White);
            Kernel.g.DrawString(10, 40, "Last known address : " + lastsknowaddress, Font.Default, Color.White);
            Kernel.g.DrawString(10, 55, "We are sorry than that happened." + lastsknowaddress, Font.Default, Color.White);
            Kernel.g.CopyTo((uint*)VBE.getLfbOffset());
        }
    }

    public unsafe class Kernel : Sys.Kernel
    {
        public static FrameBuffer g;
        public static TopBar topBar;
        Widget w;
        protected override void BeforeRun()
        {
            // Show boot screen
            g = new FrameBuffer(VBE.getModeInfo().width, VBE.getModeInfo().height);
            g.DrawString(10, 10, "Booting OS...", Font.Default, Color.White);
            g.CopyTo((uint*)VBE.getLfbOffset());

            // Init audio
            //InitializeAudio();

            // Init some widgets
            w = new Widget();
            w.X = 100;
            w.Y = 100;

            // Init the mouse
            Sys.MouseManager.ScreenWidth = VBE.getModeInfo().width;
            Sys.MouseManager.ScreenHeight = VBE.getModeInfo().height;

            // Init the top bar
            topBar = new TopBar();

            // Resize the wallpaper to the canvas resolution
            Assets.Wallpaper = Assets.Wallpaper.Resize(g.Width, g.Height);

            // Play the startup sound
            //Play(MemoryAudioStream.FromWave(Assets.StartupWave));
        }

        protected override void Run()
        {
            try
            {
                g.DrawImage(0, 0, Assets.Wallpaper, false);
                topBar.Draw(g);
                w.OnDraw(g);
                w.OnUpdate();
                g.DrawString(11, 600 - 62, $"{g.FPS} FPS", Font.Default, Color.White);
                g.DrawString(11, 600 - 31, "Sounds made by GreenSoupDev", Font.Default, Color.White);
                g.DrawFilledRectangle((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 8, 12, 0, Color.White);
                g.UpdateFPS();
                g.CopyTo((uint*)VBE.getLfbOffset());
            }
            catch (Exception e)
            {
                g.Dispose();
                Console.WriteLine(e.Message);
            }

        }
    }
}
