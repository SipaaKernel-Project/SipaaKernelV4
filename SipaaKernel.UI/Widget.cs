using Cosmos.Core;
using Cosmos.System;
using SipaaKernel.Graphics;

namespace SipaaKernel.UI
{
    public class Widget
    {
        public Widget()
        {
            //Theme = Theme.Default;
        }

        //public Theme Theme { get; set; }

        public bool IsHovering { get; set; }
        public bool IsPressed { get; set; }
        public bool HasBorder { get; set; }
        public bool IsHidden { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public uint Height { get; set; } = 40;
        public uint Width { get; set; } = 150;
        public string Text { get; set; } = "Widget";

        protected FrameBuffer RenderWidget()
        {
            var buf = new FrameBuffer(Width, Height);
            buf.DrawFilledRectangle(0, 0, (int)Width, (int)Height, 0, Color.DeepGray);
            return buf;
        }

        public virtual void OnDraw(FrameBuffer Buffer)
        {
            Buffer.DrawImage((int)X, (int)Y, RenderWidget());
        }
        public virtual void OnUpdate()
        {

        }
        public virtual void OnClick(int X, int Y, MouseState State) { }
        public virtual void OnKeyPress(KeyEvent Key) { }

        public void Dispose()
        {
            GCImplementation.Free(this);
            GC.SuppressFinalize(this);
        }
    }
}