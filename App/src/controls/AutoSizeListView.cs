using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace System.Windows.Forms
{
    /// <summary>
    /// A ListView the automatically resizes
    /// itself when the content changes.
    /// </summary>
    public class AutoSizeListView : Panel
    {
        #region INTERNAL FIELDS

        private Color headerBackColor = SystemColors.ButtonFace;
        private Color headerForeColor = SystemColors.ControlText;
        private Color headerBorderColor = SystemColors.ControlLight;
        private Color groupForeColor = SystemColors.ActiveCaption;
        private Brush headerBackBrush = new SolidBrush(SystemColors.ButtonFace);
        private Brush headerForeBrush = new SolidBrush(SystemColors.ControlText);
        private Brush groupForeBrush = new SolidBrush(SystemColors.ActiveCaption);
        private Pen headerBorderPen = new Pen(SystemColors.ControlLight);
        private Pen groupBorderPen = new Pen(SystemColors.ActiveCaption);
        private StringFormat HeaderFormat = new StringFormat();
        private StringFormat ItemFormat = new StringFormat();

        #endregion

        #region PROPERTIES

        /// <summary>
        /// List view.
        /// </summary>
        private ListView View { get; } = new ListView();
        /// <summary>
        /// Horizontal alignment of the column title.
        /// </summary>
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public StringAlignment HeaderHAllign
        {
            get => HeaderFormat.Alignment; set => HeaderFormat.Alignment = value;
        }
        /// <summary>
        /// Vertical alignment of the column title.
        /// </summary>
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public StringAlignment HeaderVAllign
        {
            get => HeaderFormat.LineAlignment; set => HeaderFormat.LineAlignment = value;
        }
        /// <summary>
        /// Horizontal alignment of the cell text.
        /// </summary>
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public StringAlignment ItemHAllign
        {
            get => ItemFormat.Alignment; set => ItemFormat.Alignment = value;
        }
        /// <summary>
        /// Vertical alignment of the cell text.
        /// </summary>
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public StringAlignment ItemVAllign
        {
            get => ItemFormat.LineAlignment; set => ItemFormat.LineAlignment = value;
        }
        /// <summary>
        /// Back color of the column header.
        /// </summary>
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public Color HeaderBackColor
        {
            get => headerBackColor; set
            {
                headerBackBrush?.Dispose();
                headerBackBrush = new SolidBrush(value);
                headerBackColor = value;
            }
        }
        /// <summary>
        /// Fore color of the column header.
        /// </summary>
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public Color HeaderForeColor
        {
            get => headerForeColor; set
            {
                headerForeBrush?.Dispose();
                headerForeBrush = new SolidBrush(value);
                headerForeColor = value;
            }
        }
        /// <summary>
        /// Border color of the column header.
        /// </summary>
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public Color HeaderBorderColor
        {
            get => headerBorderColor; set {
                headerBorderPen?.Dispose();
                headerBorderPen = new Pen(value);
                headerBorderColor = value;
            }
        }
        /// <summary>
        /// Color of the group header.
        /// </summary>
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public Color GroupForeColor
        {
            get => groupForeColor; set
            {
                groupForeBrush?.Dispose();
                groupBorderPen?.Dispose();
                groupForeBrush = new SolidBrush(value);
                groupBorderPen = new Pen(value);
                groupForeColor = value;
            }
        }

        #endregion

        #region CONSTRUCTOR

        public AutoSizeListView() : base()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                headerBorderPen?.Dispose();
                headerForeBrush?.Dispose();
                headerBackBrush?.Dispose();
                groupForeBrush?.Dispose();
                headerBorderPen?.Dispose();
                groupBorderPen?.Dispose();
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Clear all items, groups and columns from the list view.
        /// </summary>
        public void Clear()
        {
            View.Clear();
            View.View = Forms.View.Details;
            View.FullRowSelect = true;
        }

        /// <summary>
        /// Add a new column to the list view.
        /// </summary>
        /// <param name="text">Header text</param>
        /// <param name="width">Column width</param>
        public void AddColumn(string text, int width)
        {
            View.Columns.Add(text, width);
        }

        /// <summary>
        /// Add a new group to the list view.
        /// </summary>
        /// <param name="group"></param>
        public void AddGroup(ListViewGroup group)
        {
            View.Groups.Add(group);
        }

        /// <summary>
        /// Add a new item to the list view.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ListViewItem item)
        {
            View.Items.Add(item);
        }

        /// <summary>
        /// Auto size and update the control.
        /// </summary>
        public new void Update()
        {
            Size = Draw();
            base.Update();
            if (Parent?.GetType() == typeof(Panel) && (Parent as Panel).AutoScroll)
                (Parent as Panel).Update();
        }

        #endregion

        #region RENDER

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        /// <summary>
        /// Draw the control on the specified GDI+ surface and
        /// return the size of the draw area. If <code>g</code>
        /// is <code>null</code> nothing will be drawn, but the 
        /// size will still be returned.
        /// </summary>
        /// <param name="g">The GDI+ surface to draw on.</param>
        /// <returns>Returns the size of the drawn area.</returns>
        private Drawing.Size Draw(Graphics g = null)
        {
            g?.Clear(BackColor);

            // get size
            var size = new Drawing.Size();
            var W = Enumerable.Range(0, View.Columns.Count)
                .Select(i => View.Columns[i].Width).ToArray();
            size.Width = W.Sum();

            if (g != null)
            {
                // draw column headers
                var rect = new Rectangle(0, 0, 0, TextRenderer.MeasureText("Wg", View.Font).Height);
                foreach (ColumnHeader column in View.Columns)
                {
                    rect.Width = column.Width;
                    g.FillRectangle(headerBackBrush, rect);
                    g.DrawRectangle(headerBorderPen, rect);
                    g.DrawString(column.Text, View.Font, headerForeBrush, rect, HeaderFormat);
                    rect.X += rect.Width;
                }

                // draw group headers
                foreach (ListViewGroup group in View.Groups)
                {
                    if (group.Items.Count == 0)
                        continue;
                    rect = group.Items[0].Bounds;
                    rect.Y -= rect.Height + 2;
                    rect.Width = TextRenderer.MeasureText(group.Header, View.Font).Width + 3;
                    g.DrawString(group.Header, View.Font, groupForeBrush, rect.X, rect.Y);
                    rect.Y += rect.Height / 2;
                    g.DrawLine(groupBorderPen, rect.Width, rect.Y, size.Width, rect.Y);
                }
            }

            // draw items
            foreach (ListViewItem item in View.Items)
            {
                var rect = item.Bounds;
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    var sub = item.SubItems[i];
                    rect.Width = W[i];
                    g?.DrawString(sub.Text, sub.Font, headerForeBrush, rect, ItemFormat);
                    rect.X += W[i];
                    size.Height = Math.Max(size.Height, rect.Bottom);
                }
            }

            size.Width++;
            size.Height++;
            return size;
        }

        #endregion
    }
}
