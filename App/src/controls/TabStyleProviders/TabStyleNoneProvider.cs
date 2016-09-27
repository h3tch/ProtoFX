/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

namespace System.Windows.Forms
{
    [System.ComponentModel.ToolboxItem(false)]
    public class TabStyleNoneProvider : TabStyleProvider
    {
        public TabStyleNoneProvider(FXTabControl tabControl) : base(tabControl) { }

        public override void AddTabBorder(Drawing.Drawing2D.GraphicsPath path, Drawing.Rectangle tabBounds) { }
    }
}
