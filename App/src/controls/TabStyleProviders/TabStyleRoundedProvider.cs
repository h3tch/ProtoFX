using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
    [System.ComponentModel.ToolboxItem(false)]
    public class TabStyleRoundedProvider : TabStyleProvider
    {
        public TabStyleRoundedProvider(FXTabControl tabControl) : base(tabControl)
        {
            radius = 10;
            //	Must set after the _Radius as this is used in the calculations of the actual padding
            Padding = new Drawing.Point(6, 3);
        }
        
        public override void AddTabBorder(GraphicsPath path, Rectangle tabBounds)
        {

            switch (tabControl.Alignment)
            {
                case TabAlignment.Top:
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y + this.radius);
                    path.AddArc(tabBounds.X, tabBounds.Y, this.radius * 2, this.radius * 2, 180, 90);
                    path.AddLine(tabBounds.X + this.radius, tabBounds.Y, tabBounds.Right - this.radius, tabBounds.Y);
                    path.AddArc(tabBounds.Right - this.radius * 2, tabBounds.Y, this.radius * 2, this.radius * 2, 270, 90);
                    path.AddLine(tabBounds.Right, tabBounds.Y + this.radius, tabBounds.Right, tabBounds.Bottom);
                    break;
                case TabAlignment.Bottom:
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom - this.radius);
                    path.AddArc(tabBounds.Right - this.radius * 2, tabBounds.Bottom - this.radius * 2, this.radius * 2, this.radius * 2, 0, 90);
                    path.AddLine(tabBounds.Right - this.radius, tabBounds.Bottom, tabBounds.X + this.radius, tabBounds.Bottom);
                    path.AddArc(tabBounds.X, tabBounds.Bottom - this.radius * 2, this.radius * 2, this.radius * 2, 90, 90);
                    path.AddLine(tabBounds.X, tabBounds.Bottom - this.radius, tabBounds.X, tabBounds.Y);
                    break;
                case TabAlignment.Left:
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X + this.radius, tabBounds.Bottom);
                    path.AddArc(tabBounds.X, tabBounds.Bottom - this.radius * 2, this.radius * 2, this.radius * 2, 90, 90);
                    path.AddLine(tabBounds.X, tabBounds.Bottom - this.radius, tabBounds.X, tabBounds.Y + this.radius);
                    path.AddArc(tabBounds.X, tabBounds.Y, this.radius * 2, this.radius * 2, 180, 90);
                    path.AddLine(tabBounds.X + this.radius, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    break;
                case TabAlignment.Right:
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right - this.radius, tabBounds.Y);
                    path.AddArc(tabBounds.Right - this.radius * 2, tabBounds.Y, this.radius * 2, this.radius * 2, 270, 90);
                    path.AddLine(tabBounds.Right, tabBounds.Y + this.radius, tabBounds.Right, tabBounds.Bottom - this.radius);
                    path.AddArc(tabBounds.Right - this.radius * 2, tabBounds.Bottom - this.radius * 2, this.radius * 2, this.radius * 2, 0, 90);
                    path.AddLine(tabBounds.Right - this.radius, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    break;
            }
        }
    }
}
