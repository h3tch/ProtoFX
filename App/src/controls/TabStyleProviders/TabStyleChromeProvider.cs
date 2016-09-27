﻿/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
    [System.ComponentModel.ToolboxItem(false)]
    public class TabStyleChromeProvider : TabStyleProvider
    {
        public TabStyleChromeProvider(CustomTabControl tabControl) : base(tabControl)
        {
            _Overlap = 16;
            _Radius = 16;
            _ShowTabCloser = true;
            _CloserColorActive = Color.White;
            
            //	Must set after the _Radius as this is used in the calculations of the actual padding
            Padding = new Drawing.Point(7, 5);
        }
        
        public override void AddTabBorder(GraphicsPath path, Rectangle tabBounds)
        {
            int spread;
            int eigth;
            int sixth;
            int quarter;

            if (_TabControl.Alignment <= TabAlignment.Bottom)
            {
                spread = (int)Math.Floor((decimal)tabBounds.Height * 2/3);
                eigth = (int)Math.Floor((decimal)tabBounds.Height * 1/8);
                sixth = (int)Math.Floor((decimal)tabBounds.Height * 1/6);
                quarter = (int)Math.Floor((decimal)tabBounds.Height * 1/4);
            }
            else
            {
                spread = (int)Math.Floor((decimal)tabBounds.Width * 2/3);
                eigth = (int)Math.Floor((decimal)tabBounds.Width * 1/8);
                sixth = (int)Math.Floor((decimal)tabBounds.Width * 1/6);
                quarter = (int)Math.Floor((decimal)tabBounds.Width * 1/4);
            }
            
            switch (_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    path.AddCurve(new[] {
                        new Drawing.Point(tabBounds.X, tabBounds.Bottom),
                        new Drawing.Point(tabBounds.X + sixth, tabBounds.Bottom - eigth),
                        new Drawing.Point(tabBounds.X + spread - quarter, tabBounds.Y + eigth),
                        new Drawing.Point(tabBounds.X + spread, tabBounds.Y)
                    });
                    path.AddLine(tabBounds.X + spread, tabBounds.Y, tabBounds.Right - spread, tabBounds.Y);
                    path.AddCurve(new[] {
                        new Drawing.Point(tabBounds.Right - spread, tabBounds.Y),
                        new Drawing.Point(tabBounds.Right - spread + quarter, tabBounds.Y + eigth),
                        new Drawing.Point(tabBounds.Right - sixth, tabBounds.Bottom - eigth),
                        new Drawing.Point(tabBounds.Right, tabBounds.Bottom)
                    });
                    break;

                case TabAlignment.Bottom:
                    path.AddCurve(new[] {
                        new Drawing.Point(tabBounds.Right, tabBounds.Y),
                        new Drawing.Point(tabBounds.Right - sixth, tabBounds.Y + eigth),
                        new Drawing.Point(tabBounds.Right - spread + quarter, tabBounds.Bottom - eigth),
                        new Drawing.Point(tabBounds.Right - spread, tabBounds.Bottom)
                    });
                    path.AddLine(tabBounds.Right - spread, tabBounds.Bottom, tabBounds.X + spread, tabBounds.Bottom);
                    path.AddCurve(new[] {
                        new Drawing.Point(tabBounds.X + spread, tabBounds.Bottom),
                        new Drawing.Point(tabBounds.X + spread - quarter, tabBounds.Bottom - eigth),
                        new Drawing.Point(tabBounds.X + sixth, tabBounds.Y + eigth),
                        new Drawing.Point(tabBounds.X, tabBounds.Y)
                    });
                    break;

                case TabAlignment.Left:
                    path.AddCurve(new[] {
                        new Drawing.Point(tabBounds.Right, tabBounds.Bottom),
                        new Drawing.Point(tabBounds.Right - eigth, tabBounds.Bottom - sixth),
                        new Drawing.Point(tabBounds.X + eigth, tabBounds.Bottom - spread + quarter),
                        new Drawing.Point(tabBounds.X, tabBounds.Bottom - spread)
                    });
                    path.AddLine(tabBounds.X, tabBounds.Bottom - spread, tabBounds.X ,tabBounds.Y + spread);
                    path.AddCurve(new[] {
                        new Drawing.Point(tabBounds.X, tabBounds.Y + spread),
                        new Drawing.Point(tabBounds.X + eigth, tabBounds.Y + spread - quarter),
                        new Drawing.Point(tabBounds.Right - eigth, tabBounds.Y + sixth),
                        new Drawing.Point(tabBounds.Right, tabBounds.Y)
                    });
                    break;

                case TabAlignment.Right:
                    path.AddCurve(new[] {
                        new Drawing.Point(tabBounds.X, tabBounds.Y),
                        new Drawing.Point(tabBounds.X + eigth, tabBounds.Y + sixth),
                        new Drawing.Point(tabBounds.Right - eigth, tabBounds.Y + spread - quarter),
                        new Drawing.Point(tabBounds.Right, tabBounds.Y + spread)
                    });
                    path.AddLine(tabBounds.Right, tabBounds.Y + spread, tabBounds.Right, tabBounds.Bottom - spread);
                    path.AddCurve(new[] {
                        new Drawing.Point(tabBounds.Right, tabBounds.Bottom - spread),
                        new Drawing.Point(tabBounds.Right - eigth, tabBounds.Bottom - spread + quarter),
                        new Drawing.Point(tabBounds.X + eigth, tabBounds.Bottom - sixth),
                        new Drawing.Point(tabBounds.X, tabBounds.Bottom)
                    });
                    break;
            }
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
                        using (SolidBrush closerBrush = new SolidBrush(Color.FromArgb(193, 53, 53)))
                        {
                            graphics.FillPath(closerBrush, closerPath);
                        }
                    }
                    using (GraphicsPath closerPath = GetCloserPath(closerRect))
                    {
                        using (Pen closerPen = new Pen(_CloserColorActive))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                }
                else
                {
                    using (GraphicsPath closerPath = GetCloserPath(closerRect))
                    {
                        using (Pen closerPen = new Pen(_CloserColor))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                }
            }
        }	

        private static GraphicsPath GetCloserButtonPath(Rectangle closerRect)
        {
            GraphicsPath closerPath = new GraphicsPath();
            closerPath.AddEllipse(new Rectangle(closerRect.X - 2, closerRect.Y - 2,
                closerRect.Width + 4, closerRect.Height + 4));
            closerPath.CloseFigure();
            return closerPath;
        }
    }
}
