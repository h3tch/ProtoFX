using ScintillaNET;
using System;
using System.Drawing;

namespace App
{
    partial class CodeEditor
    {
        private static FXLexer FxLexer;

        /// <summary>
        /// Initialize code highlighting.
        /// </summary>
        private void InitializeHighlighting()
        {
            StyleResetDefault();
            Styles[Style.Default].Font = "Consolas";
            Styles[Style.Default].Size = 10;
            StyleClearAll();

            Styles[Style.LineNumber].ForeColor = Color.Gray;
            Styles[(int)FXLexer.Styles.Folding].ForeColor = Color.Red;

            // set styles as defined in the keyword file
            foreach (var def in FxLexer.Defs)
            {
                if (def.Value != null)
                {
                    Styles[def.Value.Id].ForeColor = def.Value.ForeColor;
                    Styles[def.Value.Id].BackColor = def.Value.BackColor;
                }
            }

            Lexer = Lexer.Container;
        }

        /// <summary>
        /// Update code styling between start and end position.
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        private void UpdateCodeStyling(int startPos, int endPos)
        {
            var line = LineFromPosition(GetEndStyled());
            // get start and end position of the region that needs to be styled
            FxLexer.Style(this, Lines[line].Position, endPos);
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
            FxLexer.Folding(this, Lines[startLine].Position, Lines[endLine].EndPosition);
        }
    }
}
