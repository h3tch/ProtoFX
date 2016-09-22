using System.Drawing;

namespace System.Windows.Forms
{
    public class TabControlEx : TabControl
    {
        private new Color ForeColor = Color.LightGray;
        private Color SelectedColor = ColorTranslator.FromHtml("#FF555555");
        private new Color BackColor = ColorTranslator.FromHtml("#FF333333");

        public TabControlEx()
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

            Rectangle ControlArea = this.ClientRectangle;
            Rectangle TabArea = this.DisplayRectangle;

            //----------------------------
            // fill client area
            Brush br = new SolidBrush(BackColor);
            g.FillRectangle(br, ControlArea);
            br.Dispose();
            //----------------------------
            
            //----------------------------
            // clip region for drawing tabs
            var rsaved = g.Clip;
            var rreg = new Rectangle(TabArea.Left, ControlArea.Top, TabArea.Width, ControlArea.Height);
            g.SetClip(rreg);

            // draw tabs
            for (int i = 0; i < this.TabCount; i++)
                DrawTab(g, this.TabPages[i], i);

            br = new SolidBrush(SelectedColor);
            rreg = new Rectangle(0, 22, ControlArea.Width, 2);
            g.FillRectangle(br, rreg);

            g.Clip = rsaved;
            //----------------------------
        }

        internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
        {
            Rectangle recBounds = GetTabRect(nIndex);
            RectangleF tabTextArea = GetTabRect(nIndex);

            bool bSelected = (SelectedIndex == nIndex);

            PointF[] pt = new PointF[7];
            if (Alignment == TabAlignment.Top)
            {
                pt[0] = new PointF(recBounds.Left, recBounds.Bottom);
                pt[1] = new PointF(recBounds.Left, recBounds.Top + 3);
                pt[2] = new PointF(recBounds.Left + 3, recBounds.Top);
                pt[3] = new PointF(recBounds.Right - 3, recBounds.Top);
                pt[4] = new PointF(recBounds.Right, recBounds.Top + 3);
                pt[5] = new PointF(recBounds.Right, recBounds.Bottom);
                pt[6] = new PointF(recBounds.Left, recBounds.Bottom);
            }
            else
            {
                pt[0] = new PointF(recBounds.Left, recBounds.Top);
                pt[1] = new PointF(recBounds.Right, recBounds.Top);
                pt[2] = new PointF(recBounds.Right, recBounds.Bottom - 3);
                pt[3] = new PointF(recBounds.Right - 3, recBounds.Bottom);
                pt[4] = new PointF(recBounds.Left + 3, recBounds.Bottom);
                pt[5] = new PointF(recBounds.Left, recBounds.Bottom - 3);
                pt[6] = new PointF(recBounds.Left, recBounds.Top);
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
