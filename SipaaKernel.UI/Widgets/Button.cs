using PrismGL2D;

namespace SipaaKernel.UI.Widgets
{
    public class Button : Widget
    {
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
            if (Theme.GetBorderRadius() > 0)
            {
                buf.DrawImage(X, Y, RenderWidget());
            }
            else
            {
                buf.DrawImage(X, Y, RenderWidget(), false);
            }
        }
    }
}
