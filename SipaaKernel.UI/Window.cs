using Cosmos.System;
using PrismGL2D;
using SipaaKernel.UI.SysTheme2;
using SipaaKernel.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    public class Window : Graphics
    {

        private string _Title;
        private uint _Width = 150, _Height = 150;
        public string Title { get => _Title; set { _Title = value; } }

        public List<Widget> Widgets { get; set; } = new List<Widget>();
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public new uint Width { get => base.Width; set { base.Width = value; } }
        public new uint Height { get => base.Height; set { base. Height = value; } }
        public uint TitleBarHeight { get => TitleBarGraphics.Height; }
        public Theme Theme { get => ThemeManager.GetCurrentTheme(); }
        public uint _Handle;
        public uint Handle { get => _Handle; }
        private Button CloseButton;

        int px;
        int py;
        bool lck = false;

        bool pressed;
        private bool visible = true;
        private Graphics TitleBarGraphics;

        public Action OnCloseWindow;
        public Action<Graphics> OnDrawWindow;
        public Action OnUpdateWindow;

        public bool HasWindowMoving { get; set; } = false;

        public Window(bool showWindow = true) : base(175, 175)
        {
            Clear(Theme.GetWindowBackgroundColor());
            TitleBarGraphics = new Graphics(Width, 32);
            TitleBarGraphics.Clear(Theme.GetAccentBackgroundColor(WidgetState.Idle));
            TitleBarGraphics.DrawString((int)TitleBarGraphics.Width / 2, (int)TitleBarGraphics.Height / 2, Title, Font.Fallback, Theme.GetAccentForegroundColor(), true);
            DrawImage(0, 0, TitleBarGraphics, true);

            CloseButton = new Button() { Width = TitleBarHeight, Height = TitleBarHeight };
            CloseButton.X = this.X + (int)this.Width - (int)CloseButton.Width;
            CloseButton.Y = this.Y;
            CloseButton.Text = "X";

            CloseButton.IsAccentued = true;
            CloseButton.OnClick = () => { Close(); };

            _Handle = (uint)new Random().Next(1024, 200082);

            if (showWindow)
                WindowManager.Windows.Add(this);
        }
        public virtual void OnDraw(Graphics g)
        {
            g.DrawImage(X, Y, this, false);
            CloseButton.OnDraw(g);
            foreach (Widget w in Widgets)
            {
                w.OnDraw(g);
            }
            if (OnDrawWindow != null)
                OnDrawWindow.Invoke(g);
        }

        public void Close()
        {
            WindowManager.Windows.Remove(this);
            if (OnCloseWindow != null)
                OnCloseWindow.Invoke();
        }

        public virtual void OnUpdate()
        {
            if (visible)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    if (!HasWindowMoving /**&& !fullscreenWindow**/ & MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + TitleBarHeight)
                    {
                        HasWindowMoving = true;

                        this.pressed = true;
                        if (!lck)
                        {
                            px = (int)((int)MouseManager.X - this.X);
                            py = (int)((int)MouseManager.Y - this.Y);
                            lck = true;
                        }
                    }
                }
                else
                {
                    pressed = false;
                    lck = false;
                    HasWindowMoving = false;
                }

                if (pressed)
                {
                    X = (int)MouseManager.X - px;
                    Y = (int)MouseManager.Y - py;
                }
            }
            CloseButton.OnUpdate();
            CloseButton.X = this.X + (int)this.Width - (int)CloseButton.Width;
            CloseButton.Y = this.Y;
            foreach (Widget w in Widgets)
            {
                w.OnUpdate();
            }
            if (OnUpdateWindow != null)
                OnUpdateWindow.Invoke();
        }
    }
}
