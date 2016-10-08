using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace System.Windows.Forms
{
    public class AutoSizeListView : Panel
    {
        internal Color headerBackColor = SystemColors.ButtonFace;
        internal Color headerForeColor = SystemColors.ControlText;
        internal Color headerBorderColor = SystemColors.ControlLight;
        internal Color groupForeColor = SystemColors.ActiveCaption;
        internal Brush headerBackBrush = new SolidBrush(SystemColors.ButtonFace);
        internal Brush headerForeBrush = new SolidBrush(SystemColors.ControlText);
        internal Brush groupForeBrush = new SolidBrush(SystemColors.ActiveCaption);
        internal Pen headerBorderPen = new Pen(SystemColors.ControlLight);
        internal Pen groupBorderPen = new Pen(SystemColors.ActiveCaption);

        #region PROPERTIES

        internal ListView View { get; } = new ListView();
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public StringFormat HeaderFormat { get; set; } = new StringFormat();
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public StringFormat ItemFormat { get; set; } = new StringFormat();
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public Color HeaderBackColor
        {
            get { return headerBackColor; }
            set
            {
                headerBackBrush?.Dispose();
                headerBackBrush = new SolidBrush(value);
                headerBackColor = value;
            }
        }
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public Color HeaderForeColor
        {
            get { return headerForeColor; }
            set
            {
                headerForeBrush?.Dispose();
                headerForeBrush = new SolidBrush(value);
                headerForeColor = value;
            }
        }
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public Color HeaderBorderColor
        {
            get { return headerBorderColor; }
            set {
                headerBorderPen?.Dispose();
                headerBorderPen = new Pen(value);
                headerBorderColor = value;
            }
        }
        [Category("Appearance"), RefreshProperties(RefreshProperties.All)]
        public Color GroupForeColor
        {
            get { return groupForeColor; }
            set
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

        public void Clear()
        {
            View.Clear();
            View.View = Forms.View.Details;
            View.FullRowSelect = true;
        }

        public void AddColumn(string text, int width) => View.Columns.Add(text, width);

        public void AddGroup(ListViewGroup group) => View.Groups.Add(group);

        public void AddItem(ListViewItem item) =>View.Items.Add(item);

        public new void Update()
        {
            Size = Draw();
            base.Update();
        }

        #endregion

        #region RENDER

        protected override void OnPaint(PaintEventArgs e) => Draw(e.Graphics);

        internal Drawing.Size Draw(Graphics g = null)
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
