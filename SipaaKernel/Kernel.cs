using System;
using Sys = Cosmos.System;
using Cosmos.Core;

using PrismGL2D.Extentions;
using PrismGL2D;

using IL2CPU.API.Attribs;
using static Cosmos.Core.INTs;
using SipaaKernel.UI.Widgets;
using SipaaKernel.UI;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using SipaaKernel.Core;
using Cosmos.System.Audio.IO;

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
        public static VBECanvas g;
        protected SKDE skde;
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
            g = new();
            g.DrawString(10, 10, "SipaaKernel V4 22.10 (build 24.10.2022)", Font.Fallback, Color.White);
            g.DrawString(10, 24, $"Processor : {CPU.GetCPUBrandString()}", Font.Fallback, Color.White);
            g.DrawString(10, 38, $"Total Memory : {CPU.GetAmountOfRAM()}mb", Font.Fallback, Color.White);
            g.DrawString(10, 60, "VESA VBE Graphics by Terminal.cs", Font.Fallback, Color.White);
            g.DrawString(10, 74, "Sounds by GreenSoupDev", Font.Fallback, Color.White);
            g.DrawString(10, (int)g.Height - 24, "Made by RaphMar2019 and his community.", Font.Fallback, Color.White);
            g.DrawImage((int)g.Width / 2 - (int)Assets.BootBitmap.Width / 2, (int)g.Height / 2 - (int)Assets.BootBitmap.Height / 2, Assets.BootBitmap, false);
            g.Update();

            // Init audio
            //Audio.InitializeAudio();

            // Init network & file system
            _ = new DHCPClient().SendDiscoverPacket();
            VFSManager.RegisterVFS(new CosmosVFS(), false, false);

            // Init the mouse
            Sys.MouseManager.ScreenWidth = VBE.getModeInfo().width;
            Sys.MouseManager.ScreenHeight = VBE.getModeInfo().height;

            // Resize the wallpaper to the canvas resolution
            Assets.Wallpaper = Assets.Wallpaper.Scale(g.Width, g.Height);

            // Wait some seconds
            Cosmos.HAL.Global.PIT.Wait(10000);

            // Init SKDE
            skde = new SKDE();
            skde.Initialize();

            // Show the welcome message
            MessageBox.Show("Welcome to SipaaKernel V4, an OS made with\nCosmos!\nWARNING : This is a pre-release OS.", "Welcome!");

            // Play the startup sound
            //Audio.Play(MemoryAudioStream.FromWave(Assets.StartupWave));
        }

        protected override void Run()
        {
            try
            {
                skde.Draw(g);
                foreach (Window w in WindowManager.Windows)
                {
                    try
                    {
                        w.OnDraw(g);
                        w.OnUpdate();
                    }catch(Exception ex)
                    {
                        WindowManager.Windows.Remove(w);
                        MessageBox.Show($"A window has crashed and needs to quit.\n{ex.Message}\nWe are sorry for this exception");
                    }
                }

                g.DrawString(11, 600 - 62, $"{g.GetFPS()} FPS", Font.Fallback, Color.White);
                //g.DrawString(11, 600 - 31, "Sounds made by GreenSoupDev", Font.Fallback, Color.White);
                g.DrawFilledRectangle((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 8, 12, 0, Color.White);
                g.Update();
            }
            catch (Exception e)
            {
                SKPanic(0x01, e.Message);
            }
        }
    }
}
