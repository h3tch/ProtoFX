using System.Drawing;
using System.Linq;

namespace System.Windows.Forms
{
    public class FXListView : Panel
    {
        internal Color headerBackColor = SystemColors.ButtonFace;
        internal Color headerForeColor = SystemColors.ControlText;
        internal Color headerBorderColor = SystemColors.ControlLight;
        internal Color groupForeColor = SystemColors.ActiveCaption;
        internal Brush headerBackBrush = new SolidBrush(SystemColors.ButtonFace);
        internal Brush headerForeBrush = new SolidBrush(SystemColors.ControlText);
        internal Brush groupForeBrush = new SolidBrush(SystemColors.ActiveCaption);
        internal Pen headerBorderPen = new Pen(SystemColors.ControlLight);
        internal StringFormat Format = new StringFormat();

        public ListView View { get; } = new ListView();
        public StringAlignment HeaderHorizontalAlignment { get; set; } = StringAlignment.Center;
        public StringAlignment HeaderVerticalAlignment { get; set; } = StringAlignment.Center;
        public StringAlignment HorizontalAlignment { get; set; } = StringAlignment.Center;
        public StringAlignment VerticalAlignment { get; set; } = StringAlignment.Center;

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

        public Color HeaderBorderColor
        {
            get { return headerBorderColor; }
            set {
                headerBorderPen?.Dispose();
                headerBorderPen = new Pen(value);
                headerBorderColor = value;
            }
        }

        public Color GroupForeColor
        {
            get { return groupForeColor; }
            set
            {
                groupForeBrush?.Dispose();
                groupForeBrush = new SolidBrush(value);
                groupForeColor = value;
            }
        }

        public FXListView() : base()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            var W = Enumerable.Range(0, View.Columns.Count).Select(i => View.Columns[i].Width).ToArray();
            var width = W.Sum();

            foreach (ListViewGroup group in View.Groups)
            {
                var rect = group.Items[0].Bounds;
                rect.Y -= rect.Height + 2;
                rect.Width = width;
                e.Graphics.DrawString(group.Header, View.Font, groupForeBrush, rect, Format);
            }

            foreach (ListViewItem item in View.Items)
            {
                var rect = item.Bounds;
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    var sub = item.SubItems[i];
                    rect.Width = W[i];
                    e.Graphics.DrawString(sub.Text, sub.Font, headerForeBrush, rect, Format);
                    rect.X += W[i];
                }
            }
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
            }
        }
    }
}
