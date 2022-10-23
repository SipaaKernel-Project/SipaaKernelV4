using Cosmos.Core;
using Cosmos.System;
using PrismGL2D;
using SipaaKernel.UI.SysTheme2;

namespace SipaaKernel.UI
{
    public class Widget
    {
        public Widget()
        {
            Theme = ThemeManager.GetCurrentTheme();
        }

        public Theme Theme { get; set; }

        public bool IsHovering { get; set; }
        public bool IsPressed { get; set; }
        public bool HasBorder { get; set; }
        public bool IsHidden { get; set; }
        public bool IsAccentued { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public uint Height { get; set; } = 40;
        public uint Width { get; set; } = 150;
        public string Text { get; set; } = "Widget";
        public WidgetState State { get; set; } = WidgetState.Idle;

        protected Graphics RenderWidget()
        {
            var buf = new Graphics(Width, Height);
            if (IsAccentued)
            {
                buf.DrawFilledRectangle(0, 0, Width, Height, (uint)Theme.GetBorderRadius(), Theme.GetAccentBackgroundColor(State));
                if (HasBorder) { buf.DrawRectangle(0, 0, Width, Height, (uint)Theme.GetBorderRadius(), Theme.GetAccentForegroundColor()); }
            }
            else
            {
                buf.DrawFilledRectangle(0, 0, Width, Height, (uint)Theme.GetBorderRadius(), Theme.GetWidgetBackgroundColor(State));
                if (HasBorder) { buf.DrawRectangle(0, 0, Width, Height, (uint)Theme.GetBorderRadius(), Theme.GetForegroundColor()); }
            }
            return buf;
        }

        public virtual void OnDraw(Graphics Buffer)
        {
            if (Theme.GetBorderRadius() > 0)
            {
                Buffer.DrawImage((int)X, (int)Y, RenderWidget());
            }
            else
            {
                Buffer.DrawImage((int)X, (int)Y, RenderWidget(), false);
            }
        }
        public virtual void OnUpdate()
        {
            if (MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + Height)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    State = WidgetState.Clicked;
                }
                else
                {
                    State = WidgetState.Hovered;
                }
            }
            else
            {
                State = WidgetState.Idle;
            }
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