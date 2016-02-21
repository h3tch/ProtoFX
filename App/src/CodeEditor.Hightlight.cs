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
        private int[] BlockStyles = new[] { (int)FX.Keyword, (int)FX.Annotation };
        private int[] CmdStyles = new[] { (int)FX.Command, (int)FX.Argument };
        private int[] GlslStyles = new[] {
            (int)FX.GlslKeyword, (int)FX.GlslQualifier, (int)FX.GlslFunction
        };
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

            Styles[(int)FX.Default].ForeColor = Color.Silver;
            Styles[(int)FX.Comment].ForeColor = Color.FromArgb(0x7F9F00);
            Styles[(int)FX.Operator].ForeColor = Color.FromArgb(0x3050CC);
            Styles[(int)FX.Preprocessor].ForeColor = Color.FromArgb(0xE47426);
            Styles[(int)FX.Number].ForeColor = Color.FromArgb(0x108030);
            Styles[(int)FX.String].ForeColor = Color.Maroon;
            Styles[(int)FX.Char].ForeColor = Color.FromArgb(163, 21, 21);

            Styles[(int)FX.Keyword].ForeColor = Color.Blue;
            Styles[(int)FX.Annotation].ForeColor = Color.FromArgb(30, 120, 255);
            Styles[(int)FX.Command].ForeColor = Color.FromArgb(30, 120, 255);
            Styles[(int)FX.Argument].ForeColor = Color.Purple;
            Styles[(int)FX.GlslKeyword].ForeColor = Color.Blue;
            Styles[(int)FX.GlslQualifier].ForeColor = Color.FromArgb(30, 120, 255);
            Styles[(int)FX.GlslFunction].ForeColor = Color.Purple;

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
            var end = ev.Position;

            // there can be different stylings outside and
            // inside of code blocks, therefore we need to
            // iterate and differentiate between blocks
            foreach (var block in BlockPositions())
            {
                // get start and end position of the block
                var blockStart = block[1];
                var blockEnd = block[2];
                
                // block lies before the region
                if (blockEnd < start)
                    continue;
                // all block from this point lie behind the region
                if (blockStart >= end)
                    break;
                
                // get the respective keywords to the block
                var keywords = GetWordFromPosition(block[0]) == "shader" ? GlslStyles : CmdStyles;

                // start of the block lies inside the region
                if (blockStart > start)
                    start = lexer.Style(this, start, blockStart, BlockStyles);

                // block lies completely inside the region
                if (blockEnd < end)
                {
                    start = lexer.Style(this, start, blockEnd, keywords);
                }
                // block intersects end of the region
                else
                {
                    start = lexer.Style(this, start, end, keywords);
                    // reached the end of the region
                    break;
                }
            }

            // style any remaining code regions
            if (start < end)
                lexer.Style(this, start, end, BlockStyles);
        }
    }
}
