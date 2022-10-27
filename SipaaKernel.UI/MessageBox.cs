using Cosmos.Core;
using SipaaKernel.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    public class MessageBox
    {
        private class MessageBoxWindow : Window
        {
            TextBlock tb;
            private Button okbtn;

            public MessageBoxWindow(string message = "Hello World!", string title = "SipaaKernel")
            {
                this.Width = 400;
                this.Height = 200;
                this.X = (int)VBE.getModeInfo().width / 2 - (int)this.Width / 2;
                this.Y = (int)VBE.getModeInfo().height / 2 - (int)this.Height / 2;
                this.Title = title;
                tb = new TextBlock { Width = this.Width, Height = this.Height, Text = message, X = this.X + 10, Y = this.Y + (int)TitleBarHeight + 10 };
                okbtn = new Button { Text = "OK", IsAccentued = true, OnClick = () => { Window.Windows.Remove(this); } };
                okbtn.X = this.X + (int)this.Width - (int)okbtn.Width - 10;
                okbtn.Y = this.Y + (int)this.Height - (int)okbtn.Height - 10;
                this.Widgets.Add(tb);
                this.Widgets.Add(okbtn);
            }

            public override void OnUpdate()
            {
                base.OnUpdate();
                okbtn.X = this.X + (int)this.Width - (int)okbtn.Width - 10;
                okbtn.Y = this.Y + (int)this.Height - (int)okbtn.Height - 10;
                tb.X = this.X + 10;
                tb.Y = this.Y + (int)TitleBarHeight + 10;
            }
        }

        public static void Show(string message = "Hello World!", string title = "SipaaKernel")
        {
            _ = new MessageBoxWindow(message, title);
        }
    }
}
