using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core;

using PrismGL2D;
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

            Kernel.SKCPUException(lastsknowaddress, ctxinterrupt, aDescription);
        }
    }

    public unsafe class Kernel : Sys.Kernel
    {
        public static Graphics g;
        public static TopBar topBar;
        Button w;

        #region Some API functions
        public static void SKPanic(uint error, string description)
        {
            g.Clear();
            g.DrawString(10, 10, "SipaaKernel V4 (CONFIDENTIAL BUILD)", Font.Fallback, Color.White);
            g.DrawString(10, 24, "Version 22.10.", Font.Fallback, Color.White);
            g.DrawString(10, 36, "Kernel Panic", Font.Fallback, Color.White);
            g.DrawString(10, 48, "It seems than SipaaKernel have encountred an error...", Font.Fallback, Color.White);
            g.DrawString(10, 60, description, Font.Fallback, Color.White);
            g.DrawString(10, 72, "We are sorry for this exception.", Font.Fallback, Color.White);
            g.DrawString(10, 100, "Technical information : ", Font.Fallback, Color.White);
            g.DrawString(10, 112, "Error Code : " + error, Font.Fallback, Color.White);
            //g.DrawString(10, 124, "Frames before the kernel panic : " + g.TotalFrames, Font.Fallback, Color.White); 
            g.CopyTo((uint*)VBE.getLfbOffset());
        }
        public static void SKCPUException(string lastKnownAddress, string ctxInterrupt, string ctxInterruptDescription)
        {
            g.Clear();
            g.DrawString(10, 10, "SipaaKernel V4", Font.Fallback, Color.White);
            g.DrawString(10, 24, "Version 22.10.", Font.Fallback, Color.White);
            g.DrawString(10, 36, "CPU Exception", Font.Fallback, Color.White);
            g.DrawString(10, 48, $"It seems than your {CPU.GetCPUBrandString()} have encountred an problem...", Font.Fallback, Color.White);
            g.DrawString(10, 60, $"{ctxInterrupt} . {ctxInterruptDescription}", Font.Fallback, Color.White);
            g.DrawString(10, 72, "We are sorry for this exception.", Font.Fallback, Color.White);
            g.DrawString(10, 100, "Technical information : ", Font.Fallback, Color.White);
            g.DrawString(10, 112, "Last known address : " + lastKnownAddress, Font.Fallback, Color.White);
            //g.DrawString(10, 124, "Frames before the kernel panic : " + g.TotalFrames, Font.Fallback, Color.White);
            g.CopyTo((uint*)VBE.getLfbOffset());
        }
        #endregion

        protected override void BeforeRun()
        {
            // Show boot screen
            g = new Graphics(VBE.getModeInfo().width, VBE.getModeInfo().height);
            g.DrawString(10, 10, "Booting OS...", Font.Fallback, Color.White);
            g.CopyTo((uint*)VBE.getLfbOffset());
            
            // Init audio
            //InitializeAudio();

            // Init some widgets
            w = new Button();
            w.X = 100;
            w.Y = 100;
            w.IsAccentued = true;
            w.HasBorder = true;
            // Init the mouse
            Sys.MouseManager.ScreenWidth = VBE.getModeInfo().width;
            Sys.MouseManager.ScreenHeight = VBE.getModeInfo().height;

            // Init the top bar
            topBar = new TopBar();

            // Resize the wallpaper to the canvas resolution
            Assets.Wallpaper = Assets.Wallpaper.Scale(g.Width, g.Height);
            
            // Play the startup sound
            //Play(MemoryAudioStream.FromWave(Assets.StartupWave));
        }

        protected override void Run()
        {
            try
            {
                g.DrawImage(0, 0, Assets.Wallpaper, false);
                topBar.Draw(g);
                //w.OnDraw(g);
                //w.OnUpdate();
                //g.DrawString(11, 600 - 62, $"{g.FPS} FPS", Font.Fallback, Color.White);
                g.DrawString(11, 600 - 31, "Sounds made by GreenSoupDev", Font.Fallback, Color.White);
                g.DrawFilledRectangle((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 8, 12, 0, Color.White);
                //g.UpdateFPS();
                g.CopyTo((uint*)VBE.getLfbOffset());
            }
            catch (Exception e)
            {
                SKPanic(0x01, e.Message);
            }
        }
    }
}
