using Cosmos.Core;
using SipaaKernel.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace TestKernel
{
    public unsafe class Kernel : Sys.Kernel
    {
        FrameBuffer f;
        FrameBuffer fBlur;

        public void Blur(FrameBuffer buf, int x, int y, int width, int height, int intensity)
        {
            uint* _raw = buf.Internal;
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    long r = 0, g = 0, b = 0, a = 0;
                    int counter = 0;

                    for (int ww = w - intensity; ww < w + intensity; ww++)
                    {
                        for (int hh = h - intensity; hh < h + intensity; hh++)
                        {
                            if (ww >= 0 && hh >= 0 && ww < f.Width && hh < f.Height)
                            {
                                Color color = buf[x + ww, y + hh];

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

                    f[x + w, y + h] = Color.FromARGB(255, (byte)r, (byte)g, (byte)b);
                }
            }
        }

        protected override void BeforeRun()
        {
            f = new FrameBuffer(VBE.getModeInfo().width, VBE.getModeInfo().height);
            Assets.Wallpaper = Assets.Wallpaper.Resize(VBE.getModeInfo().width, VBE.getModeInfo().height);
        }

        protected override void Run()
        {
            f.DrawImage(0, 0, Assets.Wallpaper);
            Blur(f, 500, 200, 100, 100, 10);
            f.DrawFilledRectangle(100, 100, 100, 100, Color.FromARGB(123, 0, 0, 0));
            f.CopyTo((uint*)VBE.getLfbOffset());
        }
    }
}
