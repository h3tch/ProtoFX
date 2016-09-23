using System.Drawing;
using System.Linq;

namespace System.Windows.Forms
{
    public class TabControlEx : TabControl
    {
        private new Color ForeColor;
        private new Color BackColor;
        private Color HighlightForeColor;
        private Color WorkspaceColor;
        private Brush ForeBrush;
        private Brush BackBrush;
        private Brush HighlightForeBrush;
        private Brush WorkspaceBrush;
        private StringFormat textFormat;

        public TabControlEx()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            ForeColor = Theme.ForeColor;
            BackColor = Theme.BackColor;
            HighlightForeColor = Theme.HighlightForeColor;
            WorkspaceColor = Theme.Workspace;
            ForeBrush = new SolidBrush(ForeColor);
            BackBrush = new SolidBrush(BackColor);
            HighlightForeBrush = new SolidBrush(HighlightForeColor);
            WorkspaceBrush = new SolidBrush(WorkspaceColor);
            textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Center;
            textFormat.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e) => DrawControl(e.Graphics);

        internal void DrawControl(Graphics g)
        {
            if (!Visible)
                return;

            var area = ClientRectangle;
            var tabArea = DisplayRectangle;
            var saved = g.Clip;
            
            // fill client area
            g.FillRectangle(BackBrush, area);

            // DRAW TABS
            g.SetClip(new Rectangle(tabArea.Left, area.Top, tabArea.Width, area.Height));
            for (int i = 0; i < TabCount; i++)
                DrawTab(g, TabPages[i], i);

            // reset clip space
            g.Clip = saved;
        }

        internal PointF[] pt = Enumerable.Repeat(new PointF(), 7).ToArray();
        internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
        {
            Rectangle recBounds = GetTabRect(nIndex);
            RectangleF tabTextArea = recBounds;
            var cursor = PointToClient(Cursor.Position);

            if (SelectedIndex == nIndex || recBounds.Contains(cursor.X, cursor.Y))
            {
                if (Alignment == TabAlignment.Top)
                {
                    pt[0].X = recBounds.Left;
                    pt[0].Y = recBounds.Bottom;
                    pt[1].X = recBounds.Left;
                    pt[1].Y = recBounds.Top + 3;
                    pt[2].X = recBounds.Left + 3;
                    pt[2].Y = recBounds.Top;
                    pt[3].X = recBounds.Right - 3;
                    pt[3].Y = recBounds.Top;
                    pt[4].X = recBounds.Right;
                    pt[4].Y = recBounds.Top + 3;
                    pt[5].X = recBounds.Right;
                    pt[5].Y = recBounds.Bottom;
                    pt[6].X = recBounds.Left;
                    pt[6].Y = recBounds.Bottom;
                }
                else
                {
                    pt[0].X = recBounds.Left;
                    pt[0].Y = recBounds.Top;
                    pt[1].X = recBounds.Right;
                    pt[1].Y = recBounds.Top;
                    pt[2].X = recBounds.Right;
                    pt[2].Y = recBounds.Bottom - 3;
                    pt[3].X = recBounds.Right - 3;
                    pt[3].Y = recBounds.Bottom;
                    pt[4].X = recBounds.Left + 3;
                    pt[4].Y = recBounds.Bottom;
                    pt[5].X = recBounds.Left;
                    pt[5].Y = recBounds.Bottom - 3;
                    pt[6].X = recBounds.Left;
                    pt[6].Y = recBounds.Top;
                }

                // fill this tab with background color
                g.FillPolygon(WorkspaceBrush, pt);
            }
            
            // draw tab icon
            if ((tabPage.ImageIndex >= 0) && (ImageList != null) &&
                (ImageList.Images[tabPage.ImageIndex] != null))
            {
                int nLeftMargin = 8;
                int nRightMargin = 2;

                var img = ImageList.Images[tabPage.ImageIndex];

                var rimage = new Rectangle(recBounds.X + nLeftMargin, recBounds.Y + 1, img.Width, img.Height);

                // adjust rectangles
                var nAdj = (float)(nLeftMargin + img.Width + nRightMargin);

                rimage.Y += (recBounds.Height - img.Height) / 2;
                tabTextArea.X += nAdj;
                tabTextArea.Width -= nAdj;

                // draw icon
                g.DrawImage(img, rimage);
            }
            
            // draw string
            g.DrawString(tabPage.Text, Font, ForeBrush, tabTextArea, textFormat);
        }
    }
}
