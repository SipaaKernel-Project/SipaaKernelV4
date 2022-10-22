using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.Graphics.Formats
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct TargaHeader
	{
		public char Magic1;                         // must be zero
		public char ColorMap;                       // must be zero
		public char Encoding;                       // must be 2
		public short CMaporig, CMaplen, CMapent;    // must be zero
		public short X;                             // must be zero
		public short Y;                             // image's height
		public short Height;                        // image's height
		public short Width;                         // image's width
		public char ColorDepth;                     // must be 32
		public char PixelType;                      // must be 40
	}
	public unsafe class Targa : FrameBuffer
	{
		public Targa(byte[] Buffer) : base(0, 0)
		{
			fixed (byte* P = Buffer)
			{
				Header = (TargaHeader*)P;
			}

			Height = (uint)Header->Height;
			Width = (uint)Header->Width;

			switch (Header->ColorDepth)
			{
				case (char)32:
					for (uint I = 0; I < Width * Height * 4; I++)
					{
						this[I] = Color.FromARGB(Buffer[I + 22], Buffer[I + 21], Buffer[I + 20], Buffer[I + 19]);
					}
					break;
				case (char)24:
					for (uint I = 0; I < Width * Height * 3; I++)
					{
						this[I] = Color.FromARGB(255, Buffer[I + 21], Buffer[I + 20], Buffer[I + 19]);
					}
					break;
			}
		}

		public TargaHeader* Header;
	}
}
