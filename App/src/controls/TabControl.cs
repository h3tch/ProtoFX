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
            Rectangle tabRect = GetTabRect(nIndex);
            RectangleF tabTextRect = tabRect;

            if (SelectedIndex == nIndex)
            {
                if (Alignment == TabAlignment.Top)
                {
                    pt[2].X = 3 + (pt[0].X = pt[1].X = pt[6].X = tabRect.Left);
                    pt[3].X =-3 + (pt[4].X = pt[5].X = tabRect.Right);
                    pt[1].Y = pt[4].Y = 3 + (pt[2].Y = pt[3].Y = tabRect.Top);
                    pt[0].Y = pt[5].Y = pt[6].Y = tabRect.Bottom;
                }
                else
                {
                    pt[4].X = 3 + (pt[0].X = pt[5].X = pt[6].X = tabRect.Left);
                    pt[3].X =-3 + (pt[1].X = pt[2].X = tabRect.Right);
                    pt[0].Y = pt[1].Y = pt[6].Y = tabRect.Top;
                    pt[2].Y = pt[5].Y =- 3 + (pt[3].Y = pt[4].Y = tabRect.Bottom);
                }

                // fill this tab with background color
                g.FillPolygon(WorkspaceBrush, pt);
            }
            
            // draw tab icon
            if (tabPage.ImageIndex >= 0 && ImageList != null &&
                ImageList.Images[tabPage.ImageIndex] != null)
            {
                int marginL = 8, marginR = 2;

                var img = ImageList.Images[tabPage.ImageIndex];
                var imgRect = new Rectangle(tabRect.X + marginL, tabRect.Y + 1, img.Width, img.Height);

                // adjust rectangles
                var nAdj = (float)(marginL + img.Width + marginR);

                imgRect.Y += (tabRect.Height - img.Height) / 2;
                tabTextRect.X += nAdj;
                tabTextRect.Width -= nAdj;

                // draw icon
                g.DrawImage(img, imgRect);
            }
            
            // draw string
            g.DrawString(tabPage.Text, Font, ForeBrush, tabTextRect, textFormat);
        }
    }
}
