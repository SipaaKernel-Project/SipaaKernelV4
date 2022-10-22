using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.Graphics.Formats
{
    public unsafe class Bitmap : FrameBuffer
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Bitmap"/> class.
        /// </summary>
        /// <param name="Binary">Raw binary of a bitmap file.</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NotImplementedException">Thrown if ColorDepth mode not implemented.</exception>
        public Bitmap(byte[] Binary) : base(0, 0)
        {
            Cosmos.System.Graphics.Bitmap BMP = new(Binary);
            Height = BMP.Height;
            Width = BMP.Width;

            fixed (int* PTR = BMP.rawData)
            {
                Internal = (uint*)PTR;
            }
        }

        public static bool Validate(byte[] Binary)
        {
            return Binary[0] == 'B' && Binary[1] == 'M';
        }
    }
}
