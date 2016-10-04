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

        internal string unformatedText;
        public Font KeyFont { get; set; }
        public Font ParamFont { get; set; }
        public Font CodeFont { get; set; }
        public Color KeyColor { get; set; } = Color.Blue;
        public Color ParamColor { get; set; } = Color.DarkGray;
        public Color CodeColor { get; set; } = SystemColors.ControlText;
        protected override bool ShowWithoutActivation => true;
        /// <summary>
        /// The call tip text in rich text format.
        /// </summary>
        public string Tip
        {
            get { return text.Text; }
            set
            {
                if (unformatedText != null && unformatedText == value)
                    return;
                unformatedText = value;

                // we have to change the text
                text.ReadOnly = false;
                
                text.ResetText();
                text.AppendText(value);
                Style("key", KeyFont, KeyColor);
                Style("param", ParamFont, ParamColor);
                Style("code", CodeFont, CodeColor);

                UpdateTextSize();
                text.DeselectAll();

                // make read only again
                text.ReadOnly = true;
            }
        }
        /// <summary>
        /// The size of the call tip text.
        /// </summary>
        internal Drawing.Size TextSize { get; private set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// The default constructor.
        /// </summary>
        public CallTip()
        {
            InitializeComponent();
            KeyFont = new Font("Consolas", text.Font.Size);
            ParamFont = new Font("Sans", text.Font.Size);
            CodeFont = new Font("Consolas", text.Font.Size);
            Visible = false;
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

            size.Width += Padding.Left + Padding.Right;
            size.Height += Padding.Top + Padding.Bottom;

            // set call tip size
            Width = size.Width;
            Height = size.Height;

            /// ADJUST CALL TIP POSITION

            // move the call tip in case it is placed outside the screen
            var screen = Screen.FromPoint(new Drawing.Point(screenx, screeny));
            screenx += screenx + Width <= screen.Bounds.Right ? offsetx + marginx : -Width - marginx;
            screeny += screeny + Height <= screen.Bounds.Bottom ? offsety + marginy : -Height - marginy;
            Location = new Drawing.Point(screenx, screeny);

            /// SHOW CALL TIP

            Show();
        }

        #endregion

        #region INTERNAL

        /// <summary>
        /// If the mouse manages to enter the window, close it to
        /// prevent the user from selecting the rich text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void text_MouseEnter(object sender, EventArgs e) => Hide();

        /// <summary>
        /// Update the TextSize property.
        /// </summary>
        internal void UpdateTextSize()
        {
            int r = 0, b = 0;
            // for each line
            for (int i = 0; i < text.TextLength; i++)
            {
                // select char
                text.Select(i, 1);
                // get the client position for the char
                var p = text.GetPositionFromCharIndex(i);
                // measure the char size in pixels
                var s = TextRenderer.MeasureText(text.SelectedText, text.SelectionFont);
                // get maximum position
                r = Math.Max(p.X + s.Width, r);
                b = Math.Max(p.Y + s.Height, b);
            }
            
            TextSize = new Drawing.Size(Math.Max(0, r), Math.Max(0, b));
        }

        /// <summary>
        /// Process text and style regions within tags accordingly.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        internal void Style(string tag, Font font, Color color)
        {
            var tagS = $"<{tag}>";
            var tagE = $"</{tag}>";

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
        }
        
        #endregion
    }
}
