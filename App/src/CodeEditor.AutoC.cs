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
            var invalid = new[] { '.', ',', ' ' };
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
            var style = GetStyleAt(pos);
            if ((int)FX.Keyword <= style && style <= (int)FX.GlslFunction)
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
            var prec = GetWordFromPosition(WordStartPosition(WordStartPosition(position, true), false));
            // get block surrounding caret position
            var block = BlockPosition(position);
            // get block header from block position
            var header = BlockHeader(block).ToArray();

            // is the caret inside the header
            var inHeader = block != null ? block[0] <= position && position <= block[1] : false;
            var inBody = block != null ? block[1] < position && position <= block[2] : false;

            // create search string
            search = inHeader
                // in header
                ? header[0] == word
                    // word represents the block type -> search for block type
                    ? word
                    // preceding word is the block type
                    : header[0] == prec
                        // -> search for block annotation
                        ? $"{header[0]},{word}"
                        // this is the name of the block -> search for nothing
                        : "~"
                // not in header but body
                : inBody
                    // preceding word has subkeywords
                    ? KeywordDef.Any(x => x.StartsWith($"{header[0]}.{prec}."))
                        // -> search for subkeywords
                        ? $"{header[0]}.{prec}.{word}"
                        // -> search for keywords
                        : $"{header[0]}.{word}"
                    // nether in header nor body -> search for block type
                    : word;
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

        /// <summary>
        /// Get the header of the block.
        /// </summary>
        /// <param name="block"></param>
        /// <returns>Returns a list of words making up the header or nothing.</returns>
        private IEnumerable<string> BlockHeader(int[] block)
        {
            if (block != null)
            {
                // get headers of the block surrounding the text position
                var header = Text.Substring(block[0], block[1] - block[0]);

                // return all words before the open brace '{'
                foreach (Match match in Regex.Matches(header, @"\w+"))
                    yield return match.Value;
            }
        }
    }
}
