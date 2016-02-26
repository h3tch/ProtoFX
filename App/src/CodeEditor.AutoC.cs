using System;
using System.Collections.Generic;
using System.Linq;
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
            string[] search;
            int[] skip;

            // create search string
            SelectString(position, out search, out skip);

            // search for all keywords starting with the
            // search string and not containing subkeywords
            var invalid = new[] { '.', ',' };
            for (int i = 0; i < search.Length; i++)
            {
                var keywords = from x in KeywordDef[search[i]]
                               where x.IndexOfAny(invalid, search[i].Length) <= 0
                               select x.Substring(skip[i]);

                // show auto complete list
                if (keywords.Count() > 0)
                {
                    AutoCShow(position - WordStartPosition(position, true), keywords.Cat("|"));
                    return;
                }
            }
        }

        /// <summary>
        /// Handle mouse move event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            string[] search;
            int[] skip;

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
                    for (int i = 0; i < search.Length; i++)
                    {
                        if (Hint.TryGetValue(search[i], out hint) && hint.Length > 0)
                        {
                            CallTipShow(WordStartPosition(pos, true), hint.Substring(1));
                            return;
                        }
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
        public void SelectString(int position, out string[] search, out int[] skip)
        {
            // get word and preceding word at caret position
            var word = GetWordFromPosition(position);

            // get block surrounding caret position
            var block = BlockPosition(position);

            // inside the body of the block
            if (block != null && block[1] < position)
            {
                var blocktype = GetWordFromPosition(block[0]);
                var blockannopos = NextWordStartPosition(block[0]);
                var blockanno = blockannopos < block[1] ? GetWordFromPosition(blockannopos) : "";

                // inside the body of a shader block
                if (blocktype == "shader")
                {
                    // find preceding keyword position
                    var keyPos = Enumerable.Range(block[1], position - block[1]).Reverse()
                        .FirstOrDefault(i => GetStyleAt(i) == (int)FX.GlslKeyword);
                    // find preceding closing brace
                    var bracePos = Text.LastIndexOf(')', position, position - block[1]);
                    // if no keyword was found (keyPos == 0) or,
                    // in case it was found and lies before a closing brace,
                    search = keyPos == 0 || keyPos < bracePos
                        // search for keywords associated with the block
                        ? new[] { $"{blocktype}.{word}" }
                        // else search for qualifiers associated with the keyword
                        : new[] { $"{blocktype}.{GetWordFromPosition(keyPos)}.{word}", $"{blocktype}.{word}" };
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
                    search = new[] { cmdPos == 0
                        ? $"{blocktype}.{word}"
                        : $"{blocktype}.{GetWordFromPosition(cmdPos)}.{word}" };
                }
            }
            else
            {
                // get position of preceding word
                var precPos = PrecWordStartPosition(position);

                // style of the preceding word indicates a block
                search = new[] { GetStyleAt(precPos) == (int)FX.Keyword
                    ? $"{GetWordFromPosition(precPos)},{word}"
                    : word };
            }

            skip = search.Select(x => x.Length - word.Length).ToArray();
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
        
        /// <summary>
        /// Find the start position of the preceding word. If the
        /// specified position lies outside any word the position of
        /// the closest preceding word will be returned. If it lies
        /// inside a word the position of the next preceding word will
        /// be returns.
        /// </summary>
        /// <param name="position">start position</param>
        /// <returns>Returns the position of the preceding word or 0
        /// in case no preceding word could be found.</returns>
        public int PrecWordStartPosition(int position)
        {
            // go to next non-word char
            while (position > 0 && char.IsLetterOrDigit(Text[position]))
                position--;
            // go to next word char (the end of the preceding word)
            while (position > 0 && !char.IsLetterOrDigit(Text[position]))
                position--;
            // go to start of the preceding word
            while (position > 0 && char.IsLetterOrDigit(Text[position - 1]))
                position--;

            return position;
        }

        public int NextWordStartPosition(int position)
        {
            // go to the first non-word char
            while (position < Text.Length && char.IsLetterOrDigit(Text[position]))
                position++;
            // go to the first word char (the start of the next word)
            while (position < Text.Length && !char.IsLetterOrDigit(Text[position]))
                position++;

            return position;
        }
    }
}
