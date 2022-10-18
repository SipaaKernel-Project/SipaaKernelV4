using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SipaaKernel.Graphics;

namespace SipaaKernel.UI.SysTheme2
{
    public abstract class Theme
    {
        #region Properties
        protected ColorDictionnary ColorDictionnary;
        protected int BorderRadius = 0;
        protected int BorderWeight = 0;
        protected bool CenterWindowTitle = false;

        protected abstract ColorDictionnary InitColorDictionnary();
        #endregion

        public Theme()
        {
            ColorDictionnary = InitColorDictionnary();
        }

        public Color GetAccentBackgroundColor(WidgetState state)
        {
            switch (state)
            {
                case WidgetState.Idle:
                    return ColorDictionnary.AccentIdle;
                case WidgetState.Hovered:
                    return ColorDictionnary.AccentHover;
                case WidgetState.Clicked:
                    return ColorDictionnary.AccentClicked;
            }
            return null;
        }
        public Color GetAccentForegroundColor()
        {
            return ColorDictionnary.AccentForeground;
        }
        public Color GetWidgetBackgroundColor(WidgetState state)
        {
            switch (state)
            {
                case WidgetState.Idle:
                    return ColorDictionnary.WidgetIdle;
                case WidgetState.Hovered:
                    return ColorDictionnary.WidgetHover;
                case WidgetState.Clicked:
                    return ColorDictionnary.WidgetClicked;
            }
            return null;
        }
        public Color GetWindowBackgroundColor()
        {
            return ColorDictionnary.WindowBackground;
        }
        public Color GetComponentBackgroundColor()
        {
            return ColorDictionnary.Component;
        }
        public Color GetForegroundColor()
        {
            return ColorDictionnary.Foreground;
        }

        public ColorDictionnary GetColorDictionnary()
        {
            return ColorDictionnary;
        }

        public int GetBorderRadius()
        {
            return BorderRadius;
        }
        public int GetBorderWeight()
        {
            return BorderWeight;
        }
        public bool IsWindowTitleCentered()
        {
            return CenterWindowTitle;
        }
    }
}
