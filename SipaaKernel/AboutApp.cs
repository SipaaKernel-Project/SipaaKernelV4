using PrismGL2D;
using SipaaKernel.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel
{
    internal class AboutApp
    {
        public static void Main()
        {
            var h = WindowManager.CreateWindow(new WindowOptions($"About {Kernel.OSName}", 100, 100, 200, 300));
            var w = WindowManager.WindowFromHandle(h);

            w.OnDrawWindow = (Graphics g) => 
            {
                int x = w.X + 10;
                int y = w.Y + (int)w.TitleBarHeight + 10;

                g.DrawString(x, y, $"{Kernel.OSName}", Font.Fallback, Color.White);
                y += 24;
                g.DrawString(x, y, $"Version {Kernel.OSVersion}", Font.Fallback, Color.White);
                y += 24;
                g.DrawString(x, y, $"Build {Kernel.OSBuild}", Font.Fallback, Color.White);
                y += 24;
                g.DrawString(x, y, $"Contributors : ", Font.Fallback, Color.White);
                y += 24;
                foreach (string Contributor in Kernel.Contributors)
                {
                    g.DrawString(x, y, $"{Contributor}", Font.Fallback, Color.White);
                    y += 18;
                }
            };

        }
    }
}
