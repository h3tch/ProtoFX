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
            public override Color ButtonCheckedGradientBegin => Theme.HighlightBackColor;
            public override Color ButtonCheckedGradientEnd => Theme.HighlightBackColor;
            public override Color ButtonCheckedGradientMiddle => Theme.HighlightBackColor;
            public override Color ButtonCheckedHighlight => Theme.Workspace;
            public override Color ButtonCheckedHighlightBorder => Theme.WorkspaceHighlight;
            public override Color ButtonPressedBorder => Theme.WorkspaceHighlight;
            public override Color ButtonPressedGradientBegin => Theme.WorkspaceHighlight;
            public override Color ButtonPressedGradientEnd => Theme.WorkspaceHighlight;
            public override Color ButtonPressedGradientMiddle => Theme.WorkspaceHighlight;
            public override Color ButtonPressedHighlight => Theme.WorkspaceHighlight;
            public override Color ButtonPressedHighlightBorder => Theme.HighlightBackColor;
            public override Color ButtonSelectedBorder => Theme.HighlightBackColor;
            public override Color ButtonSelectedGradientBegin => Theme.Workspace;
            public override Color ButtonSelectedGradientEnd => Theme.Workspace;
            public override Color ButtonSelectedGradientMiddle => Theme.Workspace;
            public override Color ButtonSelectedHighlight => Theme.Workspace;
            public override Color ButtonSelectedHighlightBorder => Theme.WorkspaceHighlight;
            //public override Color CheckBackground => Theme.BackColor;
            //public override Color CheckPressedBackground => Theme.BackColor;
            //public override Color CheckSelectedBackground => Theme.BackColor;
            public override Color GripDark => Theme.Workspace;
            public override Color GripLight => Theme.ForeColor;
            //public override Color ImageMarginGradientBegin => Theme.BackColor;
            //public override Color ImageMarginGradientEnd => Theme.BackColor;
            //public override Color ImageMarginGradientMiddle => Theme.BackColor;
            //public override Color ImageMarginRevealedGradientBegin => Theme.BackColor;
            //public override Color ImageMarginRevealedGradientEnd => Theme.BackColor;
            //public override Color ImageMarginRevealedGradientMiddle => Theme.BackColor;
            public override Color MenuBorder => Theme.BackColor;
            public override Color MenuItemBorder => Theme.BackColor;
            public override Color MenuItemPressedGradientBegin => Theme.BackColor;
            public override Color MenuItemPressedGradientEnd => Theme.BackColor;
            public override Color MenuItemPressedGradientMiddle => Theme.BackColor;
            public override Color MenuItemSelected => Theme.BackColor;
            public override Color MenuItemSelectedGradientBegin => Theme.BackColor;
            public override Color MenuItemSelectedGradientEnd => Theme.BackColor;
            public override Color MenuStripGradientBegin => Theme.BackColor;
            public override Color MenuStripGradientEnd => Theme.BackColor;
            public override Color OverflowButtonGradientBegin => Theme.BackColor;
            public override Color OverflowButtonGradientEnd => Theme.BackColor;
            public override Color OverflowButtonGradientMiddle => Theme.BackColor;
            //public override Color RaftingContainerGradientBegin => Theme.BackColor;
            //public override Color RaftingContainerGradientEnd => Theme.BackColor;
            //public override Color StatusStripGradientBegin => Theme.BackColor;
            //public override Color StatusStripGradientEnd => Theme.BackColor;
            public override Color SeparatorDark => Theme.Workspace;
            public override Color SeparatorLight => Theme.ForeColor;
            public override Color ToolStripBorder => Theme.BackColor;
            public override Color ToolStripContentPanelGradientBegin => Theme.BackColor;
            public override Color ToolStripContentPanelGradientEnd => Theme.BackColor;
            public override Color ToolStripDropDownBackground => Theme.BackColor;
            public override Color ToolStripGradientBegin => Theme.BackColor;
            public override Color ToolStripGradientEnd => Theme.BackColor;
            public override Color ToolStripGradientMiddle => Theme.BackColor;
            public override Color ToolStripPanelGradientBegin => Theme.BackColor;
            public override Color ToolStripPanelGradientEnd => Theme.BackColor;
        }
    }
}
