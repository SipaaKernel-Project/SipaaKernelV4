using Cosmos.System;
using PrismGL2D;
using SipaaKernel.UI.SysTheme2;
using SipaaKernel.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    public class Window
    {
        #region Window Manager
        public static List<Window> Windows = new List<Window>();
        #endregion

        private string _Title;
        private uint _Width = 150, _Height = 150;
        public string Title { get => _Title; set { _Title = value; RenderTitleBar(); } }

        public List<Widget> Widgets { get; set; } = new List<Widget>();
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public uint Width { get => _Width; set { _Width = value; RenderTitleBar(); winG.Scale(value, Height, PrismGL2D.Structure.ScaleMode.DontKeep); } }
        public uint Height { get => _Height; set { _Height = value; RenderTitleBar(); winG.Scale(Width, value, PrismGL2D.Structure.ScaleMode.DontKeep); } }
        public uint TitleBarHeight { get => TitleBar.Height; }
        public Theme Theme { get => ThemeManager.GetCurrentTheme(); }

        private Button CloseButton;

        int px;
        int py;
        bool lck = false;

        bool pressed;
        private bool visible = true;

        public Action OnClose;
        public bool HasWindowMoving { get; set; } = false;

        public Window(bool showWindow = true)
        {
            RenderTitleBar();
            winG = new Graphics(Width, Height);
            CloseButton = new Button() { Width = TitleBarHeight, Height = TitleBarHeight };
            CloseButton.X = this.X + (int)this.Width - (int)CloseButton.Width;
            CloseButton.Y = this.Y;
            CloseButton.Text = "X";

            CloseButton.IsAccentued = true;
            CloseButton.OnClick = () => { Window.Windows.Remove(this); };

            if (showWindow)
                Window.Windows.Add(this);
        }

        public virtual void OnDraw(Graphics g)
        {
            Graphics windowGraphics = RenderWindow();
            g.DrawImage(X, Y, windowGraphics, true);
            CloseButton.OnDraw(g);
            foreach (Widget w in Widgets)
            {
                w.OnDraw(g);
            }
        }

        public void Close()
        {
            Window.Windows.Remove(this);
            if (OnClose != null)
                OnClose.Invoke();
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
        }

        // Window renderer
        private Graphics TitleBar;
        private Graphics winG;

        /// <summary>
        /// Render the title bar
        /// </summary>
        /// <param name="Title">The title to render</param>
        public Graphics RenderTitleBar()
        {
            TitleBar = new Graphics(Width, 32);
            TitleBar.DrawFilledRectangle(0, 0, TitleBar.Width, TitleBar.Height, (uint)Theme.GetBorderRadius(),Theme.GetAccentBackgroundColor(WidgetState.Idle));
            TitleBar.DrawString((int)TitleBar.Width / 2, (int)TitleBar.Height /2, Title, Font.Fallback, Theme.GetAccentForegroundColor(), true);
            return TitleBar;
        }
        /// <summary>
        /// Render the window
        /// </summary>
        /// <returns>The graphics where the window has been rendered</returns>
        public Graphics RenderWindow()
        {
            winG.DrawFilledRectangle(0, 0, Width, Height, (uint)Theme.GetBorderRadius(), Theme.GetWindowBackgroundColor());
            winG.DrawImage(0, 0, RenderTitleBar(), true);
            return winG;
        }
    }
}
