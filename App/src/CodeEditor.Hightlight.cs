﻿using ScintillaNET;
using System;
using System.Drawing;

namespace App
{
    partial class CodeEditor
    {
        private static FxLexer FxLexer;

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

            Styles[Style.LineNumber].ForeColor = Color.Gray;

            // set styles as defined in the keyword file
            foreach (var def in FxLexer.Defs)
            {
                if (def.Value == null)
                    continue;
                Styles[def.Value.Id].ForeColor = def.Value.ForeColor;
                Styles[def.Value.Id].BackColor = def.Value.BackColor;
            }

            Lexer = Lexer.Container;
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
                FxLexer.Style(this, GetEndStyled(), e.Position);
            }
            catch
            {

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
            FxLexer.Folding(this, Lines[startLine].Position, Lines[endLine].EndPosition);
        }
    }
}
