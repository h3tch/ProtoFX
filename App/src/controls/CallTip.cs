using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public partial class CallTip : Form
    {
        public string Tip
        {
            get { return text.Text; }
            set {
                text.Text = value;
            }
        }

        public new Font Font
        {
            get { return text.Font; }
            set { text.Font = value; }
        }

        public Font FontCode { get; set; } = new Font("Consolas", 10);

        public new bool Visible
        {
            get { return base.Visible; }
            set
            {
                if (value)
                {
                    var size = TextRenderer.MeasureText(Tip, text.Font, Size, TextFormatFlags.TextBoxControl);
                    size.Width += Padding.Left + Padding.Right;
                    size.Height += Padding.Top + Padding.Bottom;
                    if (size.Height > MaxTipHeight)
                    {
                        size.Height = MaxTipHeight;
                        size.Width += SystemInformation.VerticalScrollBarWidth;
                    }
                    Width = size.Width;
                    Height = size.Height;
                }
                base.Visible = value;
            }
        }

        internal int MaxTipHeight
        {
            get
            {
                const string nl = "\n\n\n\n\n\n\n\n\n";
                var size = TextRenderer.MeasureText(nl, text.Font, Size, TextFormatFlags.TextBoxControl);
                return size.Height;
            }
        }


        public CallTip()
        {
            Visible = false;
            InitializeComponent();
        }

        private void HandleGotFocus(object sender, EventArgs e)
        {
            Focus();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
    }
}
