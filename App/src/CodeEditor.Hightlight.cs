using ScintillaNET;
using System;
using System.Drawing;
using FX = App.FXLexer.Styles;

namespace App
{
    partial class CodeEditor
    {
        #region FIELDS
        private static FXLexer lexer;
        #endregion

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

            Styles[(int)FX.Default].ForeColor = lexer.Defs.ContainsKey(FX.Default.ToString())
                ? lexer.Defs[FX.Default.ToString()].Color : Color.Silver;
            Styles[(int)FX.Comment].ForeColor = lexer.Defs.ContainsKey(FX.Comment.ToString())
                ? lexer.Defs[FX.Comment.ToString()].Color : Color.FromArgb(0x7F9F00);
            Styles[(int)FX.Operator].ForeColor = lexer.Defs.ContainsKey(FX.Operator.ToString())
                ? lexer.Defs[FX.Operator.ToString()].Color : Color.FromArgb(0x6080D0);
            Styles[(int)FX.Punctuation].ForeColor = lexer.Defs.ContainsKey(FX.Punctuation.ToString())
                ? lexer.Defs[FX.Punctuation.ToString()].Color : Color.Silver;
            Styles[(int)FX.Preprocessor].ForeColor = lexer.Defs.ContainsKey(FX.Preprocessor.ToString())
                ? lexer.Defs[FX.Preprocessor.ToString()].Color : Color.FromArgb(0xE47426);
            Styles[(int)FX.Number].ForeColor = lexer.Defs.ContainsKey(FX.Number.ToString())
                ? lexer.Defs[FX.Number.ToString()].Color : Color.FromArgb(0x108030);
            Styles[(int)FX.String].ForeColor = lexer.Defs.ContainsKey(FX.String.ToString())
                ? lexer.Defs[FX.String.ToString()].Color : Color.Maroon;
            Styles[(int)FX.Char].ForeColor = lexer.Defs.ContainsKey(FX.Char.ToString())
                ? lexer.Defs[FX.Char.ToString()].Color : Color.FromArgb(163, 21, 21);
            foreach (var def in lexer.Defs)
                Styles[lexer.KeywordStylesStart + def.Value.Id].ForeColor = def.Value.Color;

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
            // get start and end position of the region that needs to be styled
            var start = Lines[LineFromPosition(GetEndStyled())].Position;
            lexer.Style(this, start, ev.Position);
        }
    }
}
