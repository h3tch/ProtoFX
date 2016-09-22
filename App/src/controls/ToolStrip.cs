using System.Drawing;

namespace System.Windows.Forms
{
    public class ToolStripEx : ToolStrip
    {
        public ToolStripEx()
        {
            Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
        }

        class CustomColorTable : ProfessionalColorTable
        {
            //public override Color ButtonCheckedGradientBegin => App.Theme.BackColor;
            //public override Color ButtonCheckedGradientEnd => App.Theme.BackColor;
            //public override Color ButtonCheckedGradientMiddle => App.Theme.BackColor;
            //public override Color ButtonCheckedHighlight => App.Theme.BackColor;
            //public override Color ButtonCheckedHighlightBorder => App.Theme.BackColor;
            //public override Color ButtonPressedBorder => App.Theme.BackColor;
            //public override Color ButtonPressedGradientBegin => App.Theme.BackColor;
            //public override Color ButtonPressedGradientEnd => App.Theme.BackColor;
            //public override Color ButtonPressedGradientMiddle => App.Theme.BackColor;
            //public override Color ButtonPressedHighlight => App.Theme.BackColor;
            //public override Color ButtonPressedHighlightBorder => App.Theme.BackColor;
            //public override Color ButtonSelectedBorder => App.Theme.BackColor;
            //public override Color ButtonSelectedGradientBegin => App.Theme.BackColor;
            //public override Color ButtonSelectedGradientEnd => App.Theme.BackColor;
            //public override Color ButtonSelectedGradientMiddle => App.Theme.BackColor;
            //public override Color ButtonSelectedHighlight => App.Theme.BackColor;
            //public override Color ButtonSelectedHighlightBorder => App.Theme.BackColor;
            //public override Color CheckBackground => App.Theme.BackColor;
            //public override Color CheckPressedBackground => App.Theme.BackColor;
            //public override Color CheckSelectedBackground => App.Theme.BackColor;
            public override Color GripDark => App.Theme.Workspace;
            public override Color GripLight => App.Theme.ForeColor;
            //public override Color ImageMarginGradientBegin => App.Theme.BackColor;
            //public override Color ImageMarginGradientEnd => App.Theme.BackColor;
            //public override Color ImageMarginGradientMiddle => App.Theme.BackColor;
            //public override Color ImageMarginRevealedGradientBegin => App.Theme.BackColor;
            //public override Color ImageMarginRevealedGradientEnd => App.Theme.BackColor;
            //public override Color ImageMarginRevealedGradientMiddle => App.Theme.BackColor;
            public override Color MenuBorder => App.Theme.BackColor;
            public override Color MenuItemBorder => App.Theme.BackColor;
            public override Color MenuItemPressedGradientBegin => App.Theme.BackColor;
            public override Color MenuItemPressedGradientEnd => App.Theme.BackColor;
            public override Color MenuItemPressedGradientMiddle => App.Theme.BackColor;
            public override Color MenuItemSelected => App.Theme.BackColor;
            public override Color MenuItemSelectedGradientBegin => App.Theme.BackColor;
            public override Color MenuItemSelectedGradientEnd => App.Theme.BackColor;
            public override Color MenuStripGradientBegin => App.Theme.BackColor;
            public override Color MenuStripGradientEnd => App.Theme.BackColor;
            public override Color OverflowButtonGradientBegin => App.Theme.BackColor;
            public override Color OverflowButtonGradientEnd => App.Theme.BackColor;
            public override Color OverflowButtonGradientMiddle => App.Theme.BackColor;
            //public override Color RaftingContainerGradientBegin => App.Theme.BackColor;
            //public override Color RaftingContainerGradientEnd => App.Theme.BackColor;
            //public override Color StatusStripGradientBegin => App.Theme.BackColor;
            //public override Color StatusStripGradientEnd => App.Theme.BackColor;
            public override Color SeparatorDark => App.Theme.Workspace;
            public override Color SeparatorLight => App.Theme.ForeColor;
            public override Color ToolStripBorder => App.Theme.BackColor;
            public override Color ToolStripContentPanelGradientBegin => App.Theme.BackColor;
            public override Color ToolStripContentPanelGradientEnd => App.Theme.BackColor;
            public override Color ToolStripDropDownBackground => App.Theme.BackColor;
            public override Color ToolStripGradientBegin => App.Theme.BackColor;
            public override Color ToolStripGradientEnd => App.Theme.BackColor;
            public override Color ToolStripGradientMiddle => App.Theme.BackColor;
            public override Color ToolStripPanelGradientBegin => App.Theme.BackColor;
            public override Color ToolStripPanelGradientEnd => App.Theme.BackColor;
        }
    }
}
