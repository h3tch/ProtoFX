using System.Drawing;

namespace System.Windows.Forms
{
    public class FXScrollBar : Control
    {
        internal int scrollDelta = 0;
        internal double scrollPos = 0;
        internal double scrollSize = 1;
        internal Rectangle scrollRect = new Rectangle();
        internal Color foreHotColor = SystemColors.ControlDark;
        internal Brush foreBrush = new SolidBrush(SystemColors.ControlDarkDark);
        internal Brush foreHotBrush = new SolidBrush(SystemColors.ControlDark);
        internal int mousedown = -1;
        internal int scrollPosi => (int)((Height - scrollSizei) * scrollPos);
        internal int scrollSizei => (int)(Height * scrollSize);
        internal Rectangle ScrollRect
        {
            get {
                scrollRect.X = ScrollMargin.Left;
                scrollRect.Y = scrollDelta + scrollPosi + ScrollMargin.Top;
                scrollRect.Width = Width - ScrollMargin.Left - ScrollMargin.Right;
                scrollRect.Height = scrollSizei - ScrollMargin.Top - ScrollMargin.Bottom;
                return scrollRect;
            }
        }
        public double ScrollPos
        {
            get { return scrollPos; }
            set {
                scrollPos = Math.Min(Math.Max(0, value), 1 - scrollSize);
                Invalidate();
            }
        }
        public double ScrollSize
        {
            get { return scrollSize; }
            set {
                scrollSize = Math.Min(Math.Max(10F / Height, value), 1);
                Invalidate();
            }
        }
        public Padding ScrollMargin { get; set; } = new Padding(3);
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set {
                base.ForeColor = value;
                foreBrush?.Dispose();
                foreBrush = new SolidBrush(value);
                Invalidate();
            }
        }
        public Color ForeHotColor
        {
            get { return foreHotColor; }
            set
            {
                foreHotColor = value;
                foreBrush?.Dispose();
                foreBrush = new SolidBrush(value);
                Invalidate();
            }
        }

        public FXScrollBar() : base()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ScrollSize = 0.6;
            ScrollPos = 0.5;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mousedown = ScrollRect.Contains(e.X, e.Y) ? e.Y : -1;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mousedown >= 0)
                scrollDelta = Math.Min(Math.Max(-scrollPosi, e.Y - mousedown), Height - scrollSizei - scrollPosi);
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (mousedown >= 0)
            {
                scrollPos += (double)scrollDelta / (Height - scrollSizei);
                scrollDelta = 0;
            }
            mousedown = -1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var mouseover = ScrollRect.Contains(PointToClient(Cursor.Position));
            e.Graphics.Clear(BackColor);
            e.Graphics.FillRectangle(mouseover ? foreHotBrush : foreBrush, ScrollRect);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                return;
            foreBrush?.Dispose();
        }
    }
}
