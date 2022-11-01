using Cosmos.Core.Memory;
using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SipaaKernel
{
    /// <summary>
    /// Gaussian blur for SipaaKernel
    /// 
    /// DON'T USE IT, IT WILL MAKE SIPAAKERNEL VERY LAGGY (0 FPS)
    /// </summary>
    internal unsafe class GaussianBlur
    {
        public static void Blur(Graphics gr, int X, int Y, uint Width, uint Height, uint Intensity = 10)
        {
            uint* _raw = gr.Internal;
            for (int w = 0; w < X + Width; w++)
            {
                for (int h = 0; h < Y + Height; h++)
                {
                    long r = 0, g = 0, b = 0, a = 0;
                    int counter = 0;

                    for (int ww = w - (int)Intensity; ww < w + Intensity; ww++)
                    {
                        for (int hh = h - (int)Intensity; hh < h + Intensity; hh++)
                        {
                            if (ww >= 0 && hh >= 0 && ww < gr.Width && hh < gr.Height)
                            {
                                Color color = Color.FromARGB(_raw[gr.Width * hh + ww]);

                                r += color.R;
                                g += color.G;
                                b += color.B;
                                a += color.A;

                                counter++;
                            }
                        }
                    }

                    r /= counter;
                    g /= counter;
                    b /= counter;
                    a /= counter;

                    gr[w, h] = Color.FromARGB((byte)a, (byte)r, (byte)g, (byte)b).ARGB;
                }
            }
        }
    }
}
