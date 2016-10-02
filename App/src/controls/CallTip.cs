using System.Drawing;

namespace System.Windows.Forms
{
    public partial class CallTip : Form
    {
        public string Tip
        {
            get { return text.Text; }
            set {
                text.Text = value;
                Style("key", KeyFont, KeyColor);
                Style("param", ParamFont, ParamColor);
                Style("code", CodeFont, CodeColor);
            }
        }
        public new Font Font
        {
            get { return text.Font; }
            set { text.Font = value; }
        }
        public Font KeyFont { get; set; } = new Font("Consolas", 10);
        public Color KeyColor { get; set; } = Color.Blue;
        public Font ParamFont { get; set; } = new Font("Sans", 10);
        public Color ParamColor { get; set; } = Color.DarkGray;
        public Font CodeFont { get; set; } = new Font("Consolas", 10);
        public Color CodeColor { get; set; } = SystemColors.ControlText;
        public new bool Visible
        {
            get { return base.Visible; }
            set
            {
                if (value)
                {
                    var size = TextSize;
                    if (size.Height > MaxTipHeight)
                    {
                        size.Width += SystemInformation.VerticalScrollBarWidth + 5;
                        size.Height = MaxTipHeight;
                    }
                    Width = size.Width + Padding.Left + Padding.Right;
                    Height = size.Height + Padding.Top + Padding.Bottom;
                }
                base.Visible = value;
            }
        }
        internal int MaxTipHeight
        {
            get
            {
                const string nl = "\n\n\n\n\n\n\n\n\n";
                var size = TextRenderer.MeasureText(nl, text.Font);
                return size.Height;
            }
        }
        internal Drawing.Size TextSize
        {
            get
            {
                var s = new Drawing.Size();
                for (int l = 0; l < text.Lines.Length; l++)
                {
                    var i = text.GetFirstCharIndexFromLine(l) + text.Lines[l].Length - 1;
                    text.Select(i, 1);
                    var p = text.GetPositionFromCharIndex(i);
                    var r = TextRenderer.MeasureText(text.SelectedText, text.SelectionFont);
                    s.Width = Math.Max(p.X + r.Width, s.Width);
                    s.Height = Math.Max(p.Y + r.Height, s.Height);
                }
                return s;
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

        private void Style(string tag, Font font, Color color)
        {
            var tagS = $"<{tag}>";
            var tagE = $"</{tag}>";
            text.ReadOnly = false;
            for (int start = 0, end; (start = text.Text.IndexOf(tagS)) >= 0; start = end)
            {
                text.Select(start, tagS.Length);
                text.SelectedText = "";
                if ((end = text.Text.IndexOf(tagE, start)) == -1)
                {
                    end = text.Text.Length;
                }
                else
                {
                    text.Select(end, tagE.Length);
                    text.SelectedText = "";
                }
                text.Select(start, end - start);
                text.SelectionColor = color;
                text.SelectionFont = font;
            }
            text.ReadOnly = true;
        }
    }
}
