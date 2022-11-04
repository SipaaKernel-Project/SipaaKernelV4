using Cosmos.System;
using PrismGL2D;

namespace SipaaKernel.UI.Widgets
{
    public class Button : Widget
    {
        MouseState LastMouseState;
        public Action OnClick;
        public override void OnDraw(Graphics Buffer)
        {
            Graphics buf = RenderWidget();
            if (IsAccentued)
            {
                buf.DrawString((int)Width / 2, (int)Height / 2, Text, Font.Fallback, Theme.GetAccentForegroundColor(), true);
            }
            else
            {
                buf.DrawString((int)Width / 2, (int)Height / 2, Text, Font.Fallback, Theme.GetForegroundColor(), true);
            }
            Buffer.DrawImage(X, Y, buf);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (this.State == WidgetState.Clicked && MouseManager.MouseState != LastMouseState)
                if (OnClick != null)
                    OnClick.Invoke();
            LastMouseState = MouseManager.MouseState;
        }
    }
}
