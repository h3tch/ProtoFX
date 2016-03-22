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

            // set styles as defined in the keyword file
            foreach (var def in FxLexer.Defs)
                if (def.Value != null)
                    Styles[def.Value.Id].ForeColor = def.Value.Color;

            Lexer = Lexer.Container;

            StyleNeeded += new EventHandler<StyleNeededEventArgs>(HandleStyleNeeded);
        }
        
        /// <summary>
        /// Handle style needed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void HandleStyleNeeded(object sender, StyleNeededEventArgs ev)
        {
            var line = LineFromPosition(GetEndStyled());

            // get start and end position of the region that needs to be styled
            FxLexer.Style(this, Lines[line].Position, ev.Position);

            // get start and end position of the region that needs to be folded
            while (line > 0 && Lines[line].FoldLevel != 1024)
                line--;
            FxLexer.Folding(this, Lines[line].Position, ev.Position);
        }
    }
}
