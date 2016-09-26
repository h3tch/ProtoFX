using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace System.Windows.Forms
{
    public class TabControlEx : TabControl
    {
        #region FIELDS
        private bool closeButton;
        private int closeButtonSize;
        private Color forecolor;
        private Color backcolor;
        private Color highlightforecolor;
        private Color workspacecolor;
        public new Color BackColor
        {
            get { return backcolor; }
            set { BackBrush = new SolidBrush(value); backcolor = value; }
        }
        public new Color ForeColor {
            get { return forecolor; }
            set { ForeBrush = new SolidBrush(value); forecolor = value; }
        }
        public Color HighlightForeColor
        {
            get { return highlightforecolor; }
            set { HighlightForeBrush = new SolidBrush(value); highlightforecolor = value; }
        }
        public Color WorkspaceColor
        {
            get { return workspacecolor; }
            set { WorkspaceBrush = new SolidBrush(value); workspacecolor = value; }
        }
        public bool CloseButton
        {
            get { return closeButton; }
            set {
                if (closeButton = value)
                {
                    TextFormat.Alignment = StringAlignment.Near;
                    TextFormat.LineAlignment = StringAlignment.Near;
                    Padding = new Drawing.Point(closeButtonSize + 5, 3);
                    Margin = new Forms.Padding(3, 3, closeButtonSize + 5, 3);
                }
                else
                {
                    TextFormat.Alignment = StringAlignment.Center;
                    TextFormat.LineAlignment = StringAlignment.Center;
                    Padding = new Drawing.Point(3, 3);
                    Margin = new Forms.Padding(3);
                }
            }
        }
        public int CloseButtonSize { get { return closeButtonSize; } set { closeButtonSize = value; } }
        internal Brush BackBrush;
        internal Brush ForeBrush;
        internal Brush HighlightForeBrush;
        internal Brush WorkspaceBrush;
        internal StringFormat TextFormat;
        internal PointF[] poly;
        const int TabMarginL = 2;
        const int TabMarginR = 2;
        #endregion

        public TabControlEx()
        {
            // set relevant control settings to enable custom tab controls
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            // set the tabs text style
            TextFormat = new StringFormat();
            CloseButton = false;
            CloseButtonSize = 8;

            // set style to current theme
            ForeColor = SystemColors.ControlText;
            BackColor = SystemColors.Control;
            HighlightForeColor = SystemColors.HighlightText;
            WorkspaceColor = SystemColors.AppWorkspace;
            poly = Enumerable.Repeat(new PointF(), 6).ToArray();
        }

        protected override void OnPaint(PaintEventArgs e) => DrawControl(e.Graphics);

        internal void DrawControl(Graphics g)
        {
            if (!Visible)
                return;

            var client = ClientRectangle;
            var tabs = DisplayRectangle;
            var clip = g.Clip;
            
            // fill client area
            g.FillRectangle(BackBrush, client);

            // DRAW TABS
            for (int i = 0; i < TabCount; i++)
                DrawTab(g, TabPages[i], i);

            // reset clip space
            g.Clip = clip;
        }

        internal void DrawTab(Graphics g, TabPage tab, int nIndex)
        {
            Rectangle tabRect = GetTabRect(nIndex);
            RectangleF textRect = tabRect;
            LinearGradientBrush polyBrush, borderBrush;

            // tab background polygon
            if (Alignment == TabAlignment.Top)
            {
                poly[2].X = (poly[0].X = poly[1].X = tabRect.Left) + 3;
                poly[3].X = (poly[4].X = poly[5].X = tabRect.Right) - 3;
                poly[1].Y = poly[4].Y = (poly[2].Y = poly[3].Y = tabRect.Top + 1) + 3;
                poly[0].Y = poly[5].Y = tabRect.Bottom - 1;
                polyBrush = new LinearGradientBrush(tabRect, WorkspaceColor, BackColor, LinearGradientMode.Vertical);
                borderBrush = new LinearGradientBrush(tabRect, ForeColor, BackColor, LinearGradientMode.Vertical);
            }
            else
            {
                poly[2].X = (poly[0].X = poly[1].X = tabRect.Left) + 3;
                poly[0].Y = poly[5].Y = tabRect.Top + 1;
                poly[1].Y = poly[4].Y = (poly[2].Y = poly[3].Y = tabRect.Bottom - 1) - 3;
                poly[3].X = (poly[4].X = poly[5].X = tabRect.Right) - 3;
                polyBrush = new LinearGradientBrush(tabRect, BackColor, WorkspaceColor, LinearGradientMode.Vertical);
                borderBrush = new LinearGradientBrush(tabRect, BackColor, ForeColor, LinearGradientMode.Vertical);
            }
            var pen = new Pen(borderBrush, 1);

            // draw tab background
            if (SelectedIndex == nIndex)
            {
                // fill this tab with background color
                g.FillPolygon(polyBrush, poly);
                g.DrawLines(pen, poly);
            }
            else
            {
                g.DrawLine(pen, poly[0].X, poly[0].Y, poly[1].X, poly[1].Y);
                g.DrawLine(pen, poly[4].X, poly[4].Y, poly[5].X, poly[5].Y);
            }

            // draw tab icon
            if (tab.ImageIndex >= 0 && ImageList?.Images[tab.ImageIndex] != null)
            {
                var img = ImageList.Images[tab.ImageIndex];
                var imgRect = new Rectangle(
                    tabRect.X + TabMarginL, tabRect.Y + 1,
                    img.Width, img.Height);

                // adjust rectangles
                var nAdj = (float)(TabMarginL + img.Width + TabMarginR);

                imgRect.Y += (tabRect.Height - img.Height) / 2;
                textRect.X += nAdj;
                textRect.Width -= nAdj;

                // draw icon
                g.DrawImage(img, imgRect);
            }
            
            // draw string
            g.DrawString(tab.Text, Font, ForeBrush, textRect, TextFormat);

            if (closeButton)
            {
                var Y = tabRect.Top + (tabRect.Bottom - tabRect.Top - closeButtonSize) / 2;
                var X = tabRect.Right - 5 - closeButtonSize;
                //var rect = new Rectangle(X - 2, Y - 2, closeButtonSize + 5, closeButtonSize + 5);
                //if (rect.Contains(Cursor.Position))
                //    g.FillRectangle(ForeBrush, rect);
                g.DrawLine(pen, X, Y, X + closeButtonSize, Y + closeButtonSize);
                g.DrawLine(pen, X, Y + closeButtonSize, X + closeButtonSize, Y);
            }

            // clean up
            pen.Dispose();
            polyBrush.Dispose();
            borderBrush.Dispose();
        }
    }
}
