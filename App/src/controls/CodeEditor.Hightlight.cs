using ScintillaNET;
using System;
using System.Drawing;

namespace App
{
    partial class CodeEditor
    {
        public static Color TextForeColor = Color.LightGray;
        public static Color TextBackColor = ColorTranslator.FromHtml("#FF1e1e1e");
        private static Lexer.ILexer FxLexer = new Lexer.FxLexer();

        /// <summary>
        /// Initialize code highlighting.
        /// </summary>
        private void InitializeHighlighting()
        {
            StyleNeeded += new EventHandler<StyleNeededEventArgs>(HandleStyleNeeded);
            
            StyleResetDefault();
            Styles[Style.Default].Font = "Consolas";
            Styles[Style.Default].Size = 10;
            StyleClearAll();

            Styles[Style.Default].ForeColor = TextForeColor;
            Styles[Style.Default].BackColor = TextBackColor;
            Styles[Style.LineNumber].ForeColor = Color.Gray;
            Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 51, 51, 51);

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
