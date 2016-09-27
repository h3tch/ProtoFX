using ScintillaNET;
using System;
using System.Windows.Forms;

namespace App
{
    partial class CodeEditor
    {
        private static Lexer.ILexer FxLexer = new Lexer.FxLexer();

        /// <summary>
        /// Initialize code highlighting.
        /// </summary>
        private void InitializeHighlighting()
        {
            StyleNeeded += new EventHandler<StyleNeededEventArgs>(HandleStyleNeeded);

            CaretLineBackColor = Theme.WorkspaceHighlight;
            CaretLineVisible = true;
            CaretForeColor = Theme.HighlightForeColor;
            
            StyleResetDefault();
            Styles[Style.Default].Font = "Consolas";
            Styles[Style.Default].Size = 10;
            StyleClearAll();

            Styles[Style.Default].ForeColor = Theme.ForeColor;
            Styles[Style.Default].BackColor = Theme.Workspace;
            Styles[Style.LineNumber].ForeColor = Theme.Workspace;
            Styles[Style.LineNumber].BackColor = Theme.BackColor;
            Styles[Style.CallTip].ForeColor = Theme.ForeColor;
            Styles[Style.CallTip].BackColor = Theme.HighlightBackColor;

            // set styles as defined in the keyword file
            foreach (var style in FxLexer.Styles)
            {
                Styles[style.id].ForeColor = style.fore;
                Styles[style.id].BackColor = style.back;
            }

            Lexer = ScintillaNET.Lexer.Container;
        }

        /// <summary>
        /// Handle style needed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleStyleNeeded(object sender, StyleNeededEventArgs e)
        {
            try
            {
                FxLexer.Style(this, GetEndStyled(), TextLength);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.StackTrace);
            }
        }
        
        /// <summary>
        /// Update code folding between start and end line.
        /// </summary>
        /// <param name="startLine"></param>
        /// <param name="endLine"></param>
        private void UpdateCodeFolding(int startLine, int endLine)
        {
            // get start and end position of the region that needs to be folded
            while (startLine > 0 && Lines[startLine].FoldLevel != 1024)
                startLine--;
            FxLexer.Fold(this, Lines[startLine].Position, Lines[endLine].EndPosition);
        }
    }
}
