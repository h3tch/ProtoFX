using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
    [System.ComponentModel.ToolboxItem(false)]
    public class FXTabStyleProvider : TabStyleRoundedProvider
    {
        public FXTabStyleProvider(FXTabControl tabControl) : base(tabControl)
        {
            _Radius = 1;
            _ShowTabCloser = false;
            _CloserColorActive = Color.Black;
            _CloserColor = Color.FromArgb(117, 99, 61);
            _TextColor = Color.Gray;
            _TextColorSelected = Color.Gray;
            _TextColorDisabled = Color.DarkGray;
            _BorderColor = Color.Transparent;
            _BorderColorHot = Color.FromArgb(155, 167, 183);
            _BackColor = Color.FromArgb(30, 30, 30);
            _BackColorHot = Color.FromArgb(50, 50, 50);
            _BackColorSelected = Color.FromArgb(70, 70, 70);

            //	Must set after the _Radius as this is used in the calculations of the actual padding
            Padding = new Drawing.Point(8, 3);
        }

        protected override Brush GetTabBackgroundBrush(int index)
        {
            LinearGradientBrush brush = null;

            //	Capture the colours dependant on selection state of the tab
            var start = Color.Transparent;
            var end = Color.Transparent;

            if (_TabControl.SelectedIndex == index)
            {
                start = _BackColorSelected;
                end = _BackColor;
            }
            else if (HotTrack && index == _TabControl.ActiveIndex)
                end = start = _BackColorHot;

            //	Get the correctly aligned gradient
            var tabBounds = GetTabRect(index);
            tabBounds.Inflate(1, 1);
            switch (_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    brush = new LinearGradientBrush(tabBounds, start, end, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Bottom:
                    brush = new LinearGradientBrush(tabBounds, end, start, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Left:
                    brush = new LinearGradientBrush(tabBounds, start, end, LinearGradientMode.Horizontal);
                    break;
                case TabAlignment.Right:
                    brush = new LinearGradientBrush(tabBounds, end, start, LinearGradientMode.Horizontal);
                    break;
            }

            //	Add the blend
            brush.Blend = GetBackgroundBlend();

            return brush;
        }

        private static Blend GetBackgroundBlend()
        {
            var relativeIntensities = new float[] { 0f, 1f };
            var relativePositions = new float[] { 0f, 1f };

            Blend blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;

            return blend;
        }

        public override Brush GetPageBackgroundBrush(int index)
            => new SolidBrush(_TabControl.SelectedIndex == index ? _BackColor : Color.Transparent);

        protected override void DrawTabCloser(int index, Graphics g)
        {
            if (_ShowTabCloser)
            {
                Rectangle closerRect = _TabControl.GetTabCloserRect(index);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (closerRect.Contains(_TabControl.MousePosition))
                {
                    using (GraphicsPath closerPath = GetCloserButtonPath(closerRect))
                    {
                        g.FillPath(Brushes.White, closerPath);
                        using (Pen closerPen = new Pen(_CloserColor))
                        {
                            g.DrawPath(closerPen, closerPath);
                        }
                    }
                    using (GraphicsPath closerPath = GetCloserPath(closerRect))
                    {
                        using (Pen closerPen = new Pen(_CloserColorActive))
                        {
                            closerPen.Width = 2;
                            g.DrawPath(closerPen, closerPath);
                        }
                    }
                }
                else
                {
                    if (index == _TabControl.SelectedIndex)
                    {
                        using (GraphicsPath closerPath = GetCloserPath(closerRect))
                        {
                            using (Pen closerPen = new Pen(_CloserColor))
                            {
                                closerPen.Width = 2;
                                g.DrawPath(closerPen, closerPath);
                            }
                        }
                    }
                    else if (index == _TabControl.ActiveIndex)
                    {
                        using (GraphicsPath closerPath = GetCloserPath(closerRect))
                        {
                            using (Pen closerPen = new Pen(_CloserColor))
                            {
                                closerPen.Width = 2;
                                g.DrawPath(closerPen, closerPath);
                            }
                        }
                    }
                }
            }
        }

        private static GraphicsPath GetCloserButtonPath(Rectangle closerRect)
        {
            var closerPath = new GraphicsPath();
            closerPath.AddLine(closerRect.X - 1, closerRect.Y - 2, closerRect.Right + 1, closerRect.Y - 2);
            closerPath.AddLine(closerRect.Right + 2, closerRect.Y - 1, closerRect.Right + 2, closerRect.Bottom + 1);
            closerPath.AddLine(closerRect.Right + 1, closerRect.Bottom + 2, closerRect.X - 1, closerRect.Bottom + 2);
            closerPath.AddLine(closerRect.X - 2, closerRect.Bottom + 1, closerRect.X - 2, closerRect.Y - 1);
            closerPath.CloseFigure();
            return closerPath;
        }
    }
}
