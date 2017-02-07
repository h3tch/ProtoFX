﻿using ScintillaNET.Lexing;
using System;
using System.Windows.Forms;

namespace ScintillaNET
{
    partial class CodeEditor
    {
        private static ILexer FxLexer;

        /// <summary>
        /// Initialize code highlighting.
        /// </summary>
        private void InitializeHighlighting(string keywordsXml)
        {
            StyleNeeded += HandleStyleNeeded;

            CaretLineBackColor = Theme.WorkspaceHighlight;
            CaretLineVisible = true;
            CaretForeColor = Theme.HighlightForeColor;
            
            StyleResetDefault();
            Styles[Style.Default].Font = "Consolas";
            Styles[Style.Default].Size = 11;
            StyleClearAll();

            Styles[Style.Default].ForeColor = Theme.TextColor;
            Styles[Style.Default].BackColor = Theme.Workspace;
            Styles[Style.LineNumber].ForeColor = Theme.HighlightBackColor;
            Styles[Style.LineNumber].BackColor = Theme.Workspace;
            Styles[Style.CallTip].Font = "Sans";
            Styles[Style.CallTip].ForeColor = Theme.HighlightBackColor;
            Styles[Style.CallTip].BackColor = Theme.BackColor;

            if (FxLexer == null)
                FxLexer = new FxLexer(keywordsXml);

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
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleStyleNeeded(object s, StyleNeededEventArgs e)
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
