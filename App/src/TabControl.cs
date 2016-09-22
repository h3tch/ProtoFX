using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    public class TabControl : System.Windows.Forms.TabControl
    {
        private new Color ForeColor = Color.LightGray;
        private Color SelectedColor = ColorTranslator.FromHtml("#FF555555");
        private new Color BackColor = ColorTranslator.FromHtml("#FF333333");

        public TabControl()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //DrawMode = TabDrawMode.OwnerDrawFixed;
            //DrawItem += new DrawItemEventHandler(HandleDrawItem);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawControl(e.Graphics);
        }

        internal void DrawControl(Graphics g)
        {
            if (!Visible)
                return;

            Rectangle TabControlArea = this.ClientRectangle;
            Rectangle TabArea = this.DisplayRectangle;

            //----------------------------
            // fill client area
            Brush br = new SolidBrush(BackColor);
            g.FillRectangle(br, TabControlArea);
            br.Dispose();
            //----------------------------
            
            //----------------------------
            // clip region for drawing tabs
            var rsaved = g.Clip;
            var rreg = new Rectangle(TabArea.Left, TabControlArea.Top,
                            TabArea.Width, TabControlArea.Height);

            g.SetClip(rreg);

            // draw tabs
            for (int i = 0; i < this.TabCount; i++)
                DrawTab(g, this.TabPages[i], i);

            g.Clip = rsaved;
            //----------------------------
        }

        internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
        {
            Rectangle recBounds = this.GetTabRect(nIndex);
            RectangleF tabTextArea = (RectangleF)this.GetTabRect(nIndex);

            bool bSelected = (this.SelectedIndex == nIndex);

            Point[] pt = new Point[7];
            if (this.Alignment == TabAlignment.Top)
            {
                pt[0] = new Point(recBounds.Left, recBounds.Bottom);
                pt[1] = new Point(recBounds.Left, recBounds.Top + 3);
                pt[2] = new Point(recBounds.Left + 3, recBounds.Top);
                pt[3] = new Point(recBounds.Right - 3, recBounds.Top);
                pt[4] = new Point(recBounds.Right, recBounds.Top + 3);
                pt[5] = new Point(recBounds.Right, recBounds.Bottom);
                pt[6] = new Point(recBounds.Left, recBounds.Bottom);
            }
            else
            {
                pt[0] = new Point(recBounds.Left, recBounds.Top);
                pt[1] = new Point(recBounds.Right, recBounds.Top);
                pt[2] = new Point(recBounds.Right, recBounds.Bottom - 3);
                pt[3] = new Point(recBounds.Right - 3, recBounds.Bottom);
                pt[4] = new Point(recBounds.Left + 3, recBounds.Bottom);
                pt[5] = new Point(recBounds.Left, recBounds.Bottom - 3);
                pt[6] = new Point(recBounds.Left, recBounds.Top);
            }

            //----------------------------
            // fill this tab with background color
            Brush br = new SolidBrush(bSelected ? SelectedColor : tabPage.BackColor);
            g.FillPolygon(br, pt);
            br.Dispose();
            //----------------------------
            
            //----------------------------
            // draw tab's icon
            if ((tabPage.ImageIndex >= 0) && (ImageList != null) &&
                       (ImageList.Images[tabPage.ImageIndex] != null))
            {
                int nLeftMargin = 8;
                int nRightMargin = 2;

                Image img = ImageList.Images[tabPage.ImageIndex];

                Rectangle rimage = new Rectangle(recBounds.X + nLeftMargin,
                                    recBounds.Y + 1, img.Width, img.Height);

                // adjust rectangles
                float nAdj = (float)(nLeftMargin + img.Width + nRightMargin);

                rimage.Y += (recBounds.Height - img.Height) / 2;
                tabTextArea.X += nAdj;
                tabTextArea.Width -= nAdj;

                // draw icon
                g.DrawImage(img, rimage);
            }
            //----------------------------

            //----------------------------
            // draw string
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            br = new SolidBrush(bSelected ? ForeColor : tabPage.ForeColor);

            g.DrawString(tabPage.Text, Font, br, tabTextArea, stringFormat);
            //----------------------------
        }

        private void HandleDrawItem(object sender, DrawItemEventArgs e)
        {
            //customize the tabs
            var bshBack = new SolidBrush(Color.DarkGray);
            e.Graphics.FillRectangle(bshBack, e.Bounds);

            //also draw the text
            var fntTab = e.Font;
            var bshFore = new SolidBrush(Color.Black);
            var tabName = TabPages[e.Index].Text;
            var sftTab = new StringFormat();
            var recTab = new Rectangle(e.Bounds.X, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4);
            e.Graphics.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
        }
    }
}
