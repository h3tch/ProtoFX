using System.Drawing;

namespace System.Windows.Forms
{
    /// <summary>
    /// A call tip window that can be used to 
    /// show formated tooltips.
    /// </summary>
    public partial class CallTip : Form
    {
        #region FIELDS

        public Font KeyFont { get; set; }
        public Font ParamFont { get; set; }
        public Font CodeFont { get; set; }
        public Color KeyColor { get; set; } = Color.Blue;
        public Color ParamColor { get; set; } = Color.DarkGray;
        public Color CodeColor { get; set; } = SystemColors.ControlText;
        public override Drawing.Size MaximumSize { get; set; }
        protected override bool ShowWithoutActivation => true;
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
        public Drawing.Size TextSize
        {
            get
            {
                var s = new Drawing.Size();
                // for each line
                for (int l = 0; l < text.Lines.Length; l++)
                {
                    // select last char in the line
                    var i = text.GetFirstCharIndexFromLine(l) + text.Lines[l].Length - 1;
                    text.Select(i, 1);
                    // get the client position for the char
                    var p = text.GetPositionFromCharIndex(i);
                    // measure the char size in pixels
                    var r = TextRenderer.MeasureText(text.SelectedText, text.SelectionFont);
                    // get maximum position
                    s.Width = Math.Max(p.X + r.Width, s.Width);
                    s.Height = Math.Max(p.Y + r.Height, s.Height);
                }
                // return maximum positions
                // which is the size of the
                // text inside the textbox
                return s;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public CallTip()
        {
            Visible = false;
            InitializeComponent();
            KeyFont = new Font("Consolas", text.Font.Size);
            ParamFont = new Font("Sans", text.Font.Size);
            CodeFont = new Font("Consolas", text.Font.Size);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Show the call tip at the specified screen position.
        /// </summary>
        /// <param name="screenx">Screen x position.</param>
        /// <param name="screeny">Screen y position.</param>
        /// <param name="marginx">X margin relative to the screen position.</param>
        /// <param name="marginy">Y margin relative to the screen position.</param>
        /// <param name="offsetx">Additional x offset from the screen position.</param>
        /// <param name="offsety">Additional y offset from the screen position.</param>
        /// <param name="text">The text that should be shown in the tool tip.</param>
        public void Show(int screenx, int screeny, int marginx, int marginy, int offsetx, int offsety, string text)
        {
            /// SET NEW TEXT

            Tip = text;

            /// ADJUST CALL TIP SIZE

            var size = TextSize;

            // if the vertical scroll bar is shown, adjust the call tip size
            if (MaximumSize.Height > 0 && size.Height > MaximumSize.Height)
            {
                size.Width += SystemInformation.VerticalScrollBarWidth + 5;
                size.Height = MaximumSize.Height;
            }

            // if the horizontal scroll bar is shown, adjust the call tip size
            if (MaximumSize.Width > 0 && size.Width > MaximumSize.Width)
            {
                size.Width = MaximumSize.Width;
                size.Height += SystemInformation.HorizontalScrollBarHeight + 5;
            }

            // set call tip size
            Width = size.Width + Padding.Left + Padding.Right;
            Height = size.Height + Padding.Top + Padding.Bottom;

            /// ADJUST CALL TIP POSITION

            // move the call tip in case it is placed outside the screen
            var screen = Screen.FromPoint(new Drawing.Point(screenx, screeny));
            screenx += screenx + Width <= screen.Bounds.Right ? offsetx + marginx : -Width - marginx;
            screeny += screeny + Height <= screen.Bounds.Bottom ? offsety + marginy : -Height - marginy;
            Location = new Drawing.Point(screenx, screeny);

            /// SHOW CALL TIP
            
            Visible = true;
        }

        #endregion

        #region INTERNAL

        /// <summary>
        /// Prevent textbox from obtaining the focus.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleGotFocus(object s, EventArgs e) => Focus();
        
        /// <summary>
        /// Process text and style regions within tags accordingly.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        private void Style(string tag, Font font, Color color)
        {
            var tagS = $"<{tag}>";
            var tagE = $"</{tag}>";

            // we might have to change the text
            text.ReadOnly = false;

            // while there are any tags left
            for (int start = 0, end; (start = text.Text.IndexOf(tagS)) >= 0; start = end)
            {
                // select start tag and remove it
                text.Select(start, tagS.Length);
                text.SelectedText = string.Empty;

                // find end tag position
                if ((end = text.Text.IndexOf(tagE, start)) == -1)
                {
                    // if no end tag could be found
                    // use the end of the text as position
                    end = text.Text.Length;
                }
                else
                {
                    // select end tag and remove it
                    text.Select(end, tagE.Length);
                    text.SelectedText = string.Empty;
                }

                // select text between the tags and style it
                text.Select(start, end - start);
                text.SelectionColor = color;
                text.SelectionFont = font;
            }

            // make read only again
            text.ReadOnly = true;
        }

        #endregion
    }
}
