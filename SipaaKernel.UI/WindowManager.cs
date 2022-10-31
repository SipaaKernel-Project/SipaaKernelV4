using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    public struct WindowCreationOptions
    {
        public string Title = "Window";
        public int X = 100, Y = 100;
        public uint Width = 150, Height = 150;

        public WindowCreationOptions(string Title = "Window", int X = 100, int Y = 100, uint Width = 150, uint Height = 150)
        {
            this.Title = Title;
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }
    }
    public class WindowManager
    {
        internal static List<Window> Windows = new List<Window>();

        /// <summary>
        /// Creates a window with some options
        /// </summary>
        /// <param name="opts">The options for the window</param>
        /// <param name="showWindow">Shows the window at creation</param>
        /// <returns>The handle of the window</returns>
        public static uint CreateWindow(WindowCreationOptions opts, bool showWindow = true)
        {
            var w = new Window();

            w.Width = opts.Width;
            w.Height = opts.Height;
            w.X = opts.X;
            w.Y = opts.Y;
            w.Title = opts.Title;

            return w.Handle;
        }
    }
}
