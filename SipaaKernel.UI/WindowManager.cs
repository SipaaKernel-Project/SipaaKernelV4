using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    /// <summary>
    /// Options for window creation to use with <see cref="WindowManager"/>.CreateWindow(/**the options**/);
    /// </summary>
    public struct WindowOptions
    {
        public string Title = "Window";
        public int X = 100, Y = 100;
        public uint Width = 150, Height = 150;

        public WindowOptions(string Title = "Window", int X = 100, int Y = 100, uint Width = 150, uint Height = 150)
        {
            this.Title = Title;
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }
    }

    /// <summary>
    /// The SipaaKernel window manager
    /// </summary>
    public class WindowManager
    {
        /// <summary>
        /// The list of windows
        /// </summary>
        public static readonly List<Window> Windows = new List<Window>();

        /// <summary>
        /// Get the window handle
        /// </summary>
        /// <param name="w">The window used to get his handle</param>
        /// <returns></returns>
        public static uint GetWindowHandle(Window w)
        {
            return w.Handle;
        }

        /// <summary>
        /// Close the window you want (with his handle)
        /// </summary>
        /// <param name="Handle">The handle of the window you want close.</param>
        public static void CloseWindow(uint Handle)
        {
            foreach (Window w in Windows)
            {
                if (w.Handle == Handle)
                {
                    Windows.Remove(w);
                    return;
                }
            }
            return;
        }

        /// <summary>
        /// Set custom options for a window
        /// </summary>
        /// <param name="w">The window to change his options.</param>
        /// <param name="opts">The new window options</param>
        public static void SetCustomOptions(Window w, WindowOptions opts)
        {
            if (w != null)
            {
                w.Width = opts.Width;
                w.Height = opts.Height;
                w.X = opts.X;
                w.Y = opts.Y;
                w.Title = opts.Title;
            }
            return;
        }

        /// <summary>
        /// Set custom options for a window
        /// </summary>
        /// <param name="w">The window to change his options.</param>
        /// <param name="opts">The new window options</param>
        public static void SetCustomOptions(uint Handle, WindowOptions opts)
        {
            var w = WindowFromHandle(Handle);

            if (w != null)
            {
                w.Width = opts.Width;
                w.Height = opts.Height;
                w.X = opts.X;
                w.Y = opts.Y;
                w.Title = opts.Title;
            }
            return;
        }

        /// <summary>
        /// Close the window you want
        /// </summary>
        /// <param name="w">The window you want close.</param>
        public static void CloseWindow(Window w)
        {
            Windows.Remove(w);
            return;
        }

        /// <summary>
        /// Get a window from a handle
        /// </summary>
        /// <param name="Handle">The handle of the window</param>
        /// <returns>The window with the handle parsed in arguments</returns>
        public static Window WindowFromHandle(uint Handle)
        {
            foreach (Window window in Windows)
            {
                if (window.Handle == Handle)
                {
                    return window;
                }
            }
            return null;
        }

        /// <summary>
        /// Creates a window with some options
        /// </summary>
        /// <param name="opts">The options for the window</param>
        /// <param name="showWindow">Shows the window at creation</param>
        /// <returns>The handle of the window</returns>
        public static uint CreateWindow(WindowOptions opts, bool showWindow = true)
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
