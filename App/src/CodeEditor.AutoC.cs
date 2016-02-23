using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FX = App.FXLexer.Styles;

namespace App
{
    partial class CodeEditor
    {
        /// <summary>
        /// Show auto complete menu for the specified text position.
        /// </summary>
        /// <param name="position">The text position for which 
        /// to show the auto complete menu</param>
        public void AutoCShow(int position)
        {
            string search;
            int skip;

            // create search string
            SelectString(position, out search, out skip);

            // search for all keywords starting with the
            // search string and not containing subkeywords
            var invalid = new[] { '.', ',' };
            var keywords = from x in KeywordDef
                           where x.StartsWith(search) && invalid.All(y => x.IndexOf(y, search.Length) < 0)
                           select x.Substring(skip);

            // show auto complete list
            AutoCShow(position - WordStartPosition(position, true), keywords.Cat("|"));
        }

        /// <summary>
        /// Handle mouse move event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            string search;
            int skip;

            // check if code hints are enabled
            if (EnableCodeHints == false)
                return;

            // convert cursor position to text position
            int pos = CharPositionFromPoint(e.X, e.Y);

            // select keywords using the current text position
            // is the style at that position a valid hint style
            var style = (FX)GetStyleAt(pos);
            if (FX.Keyword <= style && style <= FX.GlslFunction)
            {
                // is there a word at that position
                var word = GetWordFromPosition(pos);
                if (word?.Length > 0)
                {
                    // create search string
                    SelectString(pos, out search, out skip);

                    // select hint
                    string hint;
                    if (Hint.TryGetValue(search, out hint) && hint.Length > 0)
                    {
                        CallTipShow(WordStartPosition(pos, true), hint.Substring(1));
                        return;
                    }
                }
            }

            CallTipCancel();
        }

        /// <summary>
        /// Create select string for the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="search"></param>
        /// <param name="skip"></param>
        public void SelectString(int position, out string search, out int skip)
        {
            // get word and preceding word at caret position
            var word = GetWordFromPosition(position);

            // get block surrounding caret position
            var block = BlockPosition(position);

            // inside the body of the block
            if (block != null && block[1] < position)
            {
                var blocktype = GetWordFromPosition(block[0]);

                // inside the body of a shader block
                if (blocktype == "shader")
                {
                    var text = GetTextRange(block[1], position - block[1]);
                    var keyPos = Enumerable.Range(block[1], position - block[1]).Reverse()
                        .FirstOrDefault(i => GetStyleAt(i) == (int)FX.GlslKeyword);
                    var bracePos = text.LastIndexOf(')');
                    GetWordFromPosition(keyPos);
                    search = $"{blocktype}.{word}";
                }
                else
                {
                    // get the position of the beginning of the line
                    var linePos = Lines[LineFromPosition(position)].Position;
                    var wordPos = WordStartPosition(position, true);
                    // try to find the preceding command
                    var cmdPos = Enumerable.Range(linePos, wordPos - linePos).Reverse()
                        .FirstOrDefault(i => GetStyleAt(i) == (int)FX.Command);
                    // the search string depends on whether a command was found
                    search = cmdPos == 0
                        ? $"{blocktype}.{word}"
                        : $"{blocktype}.{GetWordFromPosition(cmdPos)}.{word}";
                }
            }
            else
            {
                // get position of preceding word
                var precPos = PrecWordStartPosition(position);

                // style of the preceding word indicates a block
                search = GetStyleAt(precPos) == (int)FX.Keyword
                    ? $"{GetWordFromPosition(precPos)},{word}"
                    : word;
            }

            skip = search.Length - word.Length;
        }

        /// <summary>
        /// Get the block surrounding the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Returns the start, open brace and end position
        /// of the block as an int array.</returns>
        private int[] BlockPosition(int position)
        {
            // get headers of the block surrounding the text position
            foreach (var x in BlockPositions())
                if (x[0] < position && position < x[2])
                    return x;
            return null;
        }

        /// <summary>
        /// Get the position of all blocks.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int[]> BlockPositions()
            => from x in this.GetBlockPositions()
               select new[] { x.Index, x.Index + x.Value.IndexOf('{'), x.Index + x.Length };
        
        public int PrecWordStartPosition(int position)
            => WordStartPosition(Math.Max(0, WordStartPosition(position, true) - 1), true);
    }
}
