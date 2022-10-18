using IL2CPU.API.Attribs;
using SipaaKernel.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestKernel
{
    public class Assets
    {
        public const string Base = "TestKernel.";
        [ManifestResourceStream(ResourceName = Base + "wallpaper.bmp")] public readonly static byte[] WallpaperB;

        // Images
        public static FrameBuffer Wallpaper = FrameBuffer.FromImage(WallpaperB);
    }
}
