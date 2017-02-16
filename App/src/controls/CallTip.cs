using System.Drawing;
using static System.Drawing.ContentAlignment;

namespace System.Windows.Forms
{
    /// <summary>
    /// A call tip window that can be used to 
    /// show formated tooltips.
    /// </summary>
    public partial class CallTip : Form
    {
        #region FIELDS

        private string unformatedText;
        public Font KeyFont { get; set; }
        public Font ParamFont { get; set; }
        public Font CodeFont { get; set; }
        public Color KeyColor { get; set; } = Color.Blue;
        public Color ParamColor { get; set; } = Color.DarkGray;
        public Color CodeColor { get; set; } = SystemColors.ControlText;
        protected override bool ShowWithoutActivation => true;
        public bool IsActive => GetForegroundWindow() == Handle;
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
        public Drawing.Size TextSize { get; private set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// The default constructor.
        /// </summary>
        public CallTip() : base()
        {
            InitializeComponent();
            KeyFont = new Font("Consolas", text.Font.Size);
            ParamFont = new Font("Sans", text.Font.Size);
            CodeFont = new Font("Consolas", text.Font.Size);
            Visible = false;
        }

        #endregion

        #region METHODS

        public void SetChartIntervals(double x, double y)
        {
            chart.ChartAreas[0].AxisX.Interval = x;
            chart.ChartAreas[0].AxisY.Interval = y;
        }

        /// <summary>
        /// Show the call tip at the specified screen position.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="tip"></param>
        /// <param name="align"></param>
        public void Show(Rectangle rect, string tip, ContentAlignment align = BottomCenter)
        {
            /// SET NEW TEXT

            Tip = tip;
            text.Update();

            /// ADJUST CALL TIP POSITION AND SIZE

            Reposition(rect, TextSize, align);

            /// SHOW CALL TIP

            text.Visible = !(chart.Visible = false);
            Show();
        }

        /// <summary>
        /// Show the chart call tip at the specified screen position.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="align"></param>
        public void Show(Rectangle rect, Array X, Array Y, double width, double height,
            ContentAlignment align = TopLeft, double widthFactor = 0, double heightFactor = 0)
        {
            /// SET NEW CHART

            var points = chart.Series[0].Points;
            points.Clear();
            for (int i = 0; i < X.Length; i++)
                points.AddXY(X.GetValue(i), Y.GetValue(i));
            chart.Update();

            /// ADJUST CALL TIP POSITION AND SIZE

            var w = (int)(width + widthFactor * (X.Length - width));
            var h = (int)(height + heightFactor * (X.Length - height));
            Reposition(rect, new Drawing.Size(w, h), align);

            /// SHOW CALL TIP

            text.Visible = !(chart.Visible = true);
            Show();
        }

        /// <summary>
        /// Reposition the form and adjust size.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="size"></param>
        /// <param name="align"></param>
        public void Reposition(Rectangle rect, Drawing.Size size, ContentAlignment align = BottomCenter)
        {
            // add padding
            size.Width += Padding.Left + Padding.Right;
            size.Height += Padding.Top + Padding.Bottom;

            // move the call tip in case it is placed outside the screen
            var screen = Screen.FromPoint(new Drawing.Point(rect.X, rect.Y)).Bounds;

            // fix x-axis based coordinates
            if (((int)align & (int)(TopLeft | MiddleLeft | BottomLeft)) != 0)
                rect.X += rect.X - size.Width <= screen.Left ? rect.Width : -size.Width;
            else if (((int)align & (int)(TopRight | MiddleRight | BottomRight)) != 0)
                rect.X += rect.Width + size.Width >= screen.Right ? -size.Width : rect.Width;

            // fix y-axis based coordinates
            if (align >= BottomLeft)
                rect.Y += rect.Y + rect.Height + size.Height >= screen.Bottom ? -size.Height : rect.Height;
            else
                rect.Y += rect.Y - size.Height <= screen.Top ? rect.Height : -size.Height;

            // adjust form location and size
            Location = rect.Location;
            Size = size;
        }

        #endregion

        #region INTERNAL

        /// <summary>
        /// If the mouse manages to enter the window, close it to
        /// prevent the user from selecting the rich text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseEnterHandler(object sender, EventArgs e) => OnEnter(e);

        /// <summary>
        /// Update the TextSize property.
        /// </summary>
        private void UpdateTextSize()
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
        private void Style(string tag, Font font, Color color)
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
        
        [Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        #endregion
    }
}
