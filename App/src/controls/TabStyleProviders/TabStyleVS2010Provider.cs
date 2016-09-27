/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
    [System.ComponentModel.ToolboxItem(false)]
    public class TabStyleVS2010Provider : TabStyleRoundedProvider
    {
        public TabStyleVS2010Provider(CustomTabControl tabControl) : base(tabControl)
        {
            _Radius = 3;
            _ShowTabCloser = true;
            _CloserColorActive = Color.Black;
            _CloserColor = Color.FromArgb(117, 99, 61);
            _TextColor = Color.White;
            _TextColorDisabled = Color.WhiteSmoke;
            _BorderColor = Color.Transparent;
            _BorderColorHot = Color.FromArgb(155, 167, 183);

            //	Must set after the _Radius as this is used in the calculations of the actual padding
            Padding = new Drawing.Point(6, 5);
        }
    
        protected override Brush GetTabBackgroundBrush(int index)
        {
            LinearGradientBrush fillBrush = null;

            //	Capture the colours dependant on selection state of the tab
            var dark = Color.Transparent;
            var light = Color.Transparent;

            if (_TabControl.SelectedIndex == index)
            {
                dark = Color.FromArgb(100, 100, 100);
                light = SystemColors.Window;
            }
            else if (!_TabControl.TabPages[index].Enabled)
            {
                light = dark;
            }
            else if (HotTrack && index == _TabControl.ActiveIndex)
            {
                //	Enable hot tracking
                dark = Color.FromArgb(108, 108, 108);
                light = dark;
            }
            
            //	Get the correctly aligned gradient
            Rectangle tabBounds = GetTabRect(index);
            tabBounds.Inflate(3,3);
            tabBounds.X -= 1;
            tabBounds.Y -= 1;
            switch (_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Bottom:
                    fillBrush = new LinearGradientBrush(tabBounds, dark, light, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Left:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Horizontal);
                    break;
                case TabAlignment.Right:
                    fillBrush = new LinearGradientBrush(tabBounds, dark, light, LinearGradientMode.Horizontal);
                    break;
            }
            
            //	Add the blend
            fillBrush.Blend = GetBackgroundBlend();
            
            return fillBrush;
        }
        
        private static Blend GetBackgroundBlend()
        {
            var relativeIntensities = new float[] { 0f, 0.5f, 1f, 1f };
            var relativePositions = new float[] { 0f, 0.5f, 0.51f, 1f };
            
            Blend blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;
            
            return blend;
        }

        public override Brush GetPageBackgroundBrush(int index)
        {
            //	Capture the colours dependant on selection state of the tab
            var light = Color.Transparent;
            
            if (_TabControl.SelectedIndex == index) 
                light = Color.FromArgb(195, 195, 195);
            else if (!_TabControl.TabPages[index].Enabled)
                light = Color.Transparent;
            else if (_HotTrack && index == _TabControl.ActiveIndex)
                //	Enable hot tracking
                light = Color.Transparent;
            
            return new SolidBrush(light);
        }
        
        protected override void DrawTabCloser(int index, Graphics graphics)
        {
            if (_ShowTabCloser)
            {
                Rectangle closerRect = _TabControl.GetTabCloserRect(index);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if (closerRect.Contains(_TabControl.MousePosition))
                {
                    using (GraphicsPath closerPath = GetCloserButtonPath(closerRect))
                    {
                        graphics.FillPath(Brushes.White, closerPath);
                        using (Pen closerPen = new Pen(Color.FromArgb(229, 195, 101)))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                    using (GraphicsPath closerPath = GetCloserPath(closerRect))
                    {
                        using (Pen closerPen = new Pen(_CloserColorActive))
                        {
                            closerPen.Width = 2;
                            graphics.DrawPath(closerPen, closerPath);
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
                                graphics.DrawPath(closerPen, closerPath);
                            }
                        }
                    }
                    else if (index == _TabControl.ActiveIndex)
                    {
                        using (GraphicsPath closerPath = GetCloserPath(closerRect))
                        {
                            using (Pen closerPen = new Pen(Color.FromArgb(155, 167, 183)))
                            {
                                closerPen.Width = 2;
                                graphics.DrawPath(closerPen, closerPath);
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
