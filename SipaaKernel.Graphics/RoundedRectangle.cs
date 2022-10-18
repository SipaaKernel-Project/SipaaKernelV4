using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.Graphics
{
    public static class RoundRect
    {
        /// <summary>
        /// The offset of the corners.
        /// </summary>
        public static double Offset = 0;

        /// <summary>
        /// The sharpness of the corners.
        /// </summary>
        public static double Sharpness = 0.8;

        private static List<string> CacheKeys = new List<string>();
        private static List<Color[]> CacheValues = new List<Color[]>();

        /// <summary>
        /// Corner type.
        /// </summary>
        public enum Corner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        private static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        /// <summary>
        /// Draw a round anti-aliased corner.
        /// </summary>
        /// <param name="canvas">The canvas to draw on.</param>
        /// <param name="color">The colour of the corner.</param>
        /// <param name="cx">The X position of the corner.</param>
        /// <param name="cy">The Y position of the corner.</param>
        /// <param name="size">The size of the corner.</param>
        /// <param name="corner">The type of corner.</param>
        public static void DrawRoundCorner(this FrameBuffer canvas, int cx, int cy, int size, Color color, Corner corner)
        {
            Color originalColor = color;
            string key = originalColor.R.ToString() + originalColor.G.ToString() + originalColor.B.ToString() + size.ToString() + ((int)corner).ToString();
            int index = -1;
            for (int i = 0; i < CacheKeys.Count; i++)
            {
                if (CacheKeys[i] == key)
                {
                    index = i;
                }
            }
            Color[] values;
            if (index == -1)
            {
                CacheKeys.Add(key);
                values = new Color[(size * size)];
                double lookX = (corner == Corner.TopLeft || corner == Corner.BottomLeft) ? size - 1 : 0;
                double lookY = (corner == Corner.TopLeft || corner == Corner.TopRight) ? size - 1 : 0;
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        double distance = (Distance(x, y, lookX, lookY) + Offset) / size;
                        byte alpha = (byte)Math.Clamp((1 - distance) * (size * Sharpness) * 255, 0, 255);
                        values[(y * size) + x] = Color.FromARGB(alpha, originalColor.R, originalColor.G, originalColor.B);
                    }
                }
                CacheValues.Add(values);
            }
            else
            {
                values = CacheValues[index];
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    color = values[(y * size) + x];
                    canvas[cx + x, cy + y] = color;
                }
            }
            color = originalColor;
        }

        /// <summary>
        /// Draw an anti-aliased rounded rectangle.
        /// </summary>
        /// <param name="canvas">The canvas to draw on.</param>
        /// <param name="color">The colour of the rectangle.</param>
        /// <param name="x">The X position of the rectangle.</param>
        /// <param name="y">The Y position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="cornerSize">The size of the corners.</param>
        public static void DrawRoundRect(this FrameBuffer canvas, int x, int y, int width, int height, int cornerSize, Color color)
        {
            if (cornerSize == 0)
            {
                canvas.DrawFilledRectangle(x, y, width, height, 0, color);
            }

            int longest = Math.Max(width, height);
            if (cornerSize > longest / 2) cornerSize = longest / 2;

            canvas.DrawFilledRectangle(x, y + cornerSize, width, height - (cornerSize * 2), 0, color);
            canvas.DrawFilledRectangle(x + cornerSize, y, width - (cornerSize * 2), height, 0, color);
            DrawRoundCorner(canvas, x, y, cornerSize, color, Corner.TopLeft);
            DrawRoundCorner(canvas, (x + width) - cornerSize, y, cornerSize, color, Corner.TopRight);
            DrawRoundCorner(canvas, x, (y + height) - cornerSize, cornerSize, color, Corner.BottomLeft);
            DrawRoundCorner(canvas, (x + width) - cornerSize, (y + height) - cornerSize, cornerSize, color, Corner.BottomRight);
        }
    }
}
