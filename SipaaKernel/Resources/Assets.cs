using IL2CPU.API.Attribs;
using SipaaKernel.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel
{
    public class Assets
    {
        public const string Base = "SipaaKernel.Resources.";
        [ManifestResourceStream(ResourceName = Base + "wallpaper.bmp")] public readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "startupv1.wav")] public readonly static byte[] StartupWave;

        // Images
        public static FrameBuffer Wallpaper = FrameBuffer.FromImage(WallpaperB);
    }
}
