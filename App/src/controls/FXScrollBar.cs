using System.Drawing;

namespace System.Windows.Forms
{
    public class FXScrollBar : Control
    {
        internal int scrolldelta;
        internal double scrollpos;
        internal double scrollsize;
        internal Brush forebrush;
        internal int mousedown;
        internal int scrollposi => (int)((Height - scrollsizei) * scrollpos);
        internal int scrollsizei => (int)(Height * scrollsize);
        public double Scroll
        {
            get { return scrollpos; }
            set { scrollpos = Math.Min(Math.Max(0, value), 1 - scrollsize); }
        }
        public double ScrollSize
        {
            set { scrollsize = Math.Min(Math.Max(10F / Height, value), 1); }
        }
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; forebrush?.Dispose(); forebrush = new SolidBrush(value); }
        }

        public FXScrollBar() : base()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            forebrush = new SolidBrush(SystemColors.ControlDark);
            scrollsize = Height;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mousedown = e.Y;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            scrolldelta = Math.Min(Math.Max(-scrollposi, e.Y - mousedown), Height - scrollsizei - scrollposi);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            scrollpos += (double)scrolldelta / (Height - scrollsizei);
            scrolldelta = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.FillRectangle(forebrush, new Rectangle(0, scrolldelta + scrollposi, Width, scrollsizei));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                return;
            forebrush?.Dispose();
        }
    }
}
