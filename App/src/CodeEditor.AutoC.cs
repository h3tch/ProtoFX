using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            var keywords = search.Zip(skip,
                (s, i) => from x in KeywordTrie[s]
                          where x.IndexOf('[', s.Length) <= 0
                          select x.Substring(i))
                .Cat();

            // show auto complete list
            if (keywords.Count() > 0)
                AutoCShow(position - WordStartPosition(position, true), keywords.Cat("|"));
        }

        /// <summary>
        /// Handle mouse move event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            string[] search;
            string hint;
            int[] skip;

            // check if code hints are enabled
            if (EnableCodeHints == false)
                return;

            // convert cursor position to text position
            int pos = CharPositionFromPoint(e.X, e.Y);

            // select keywords using the current text position
            // is the style at that position a valid hint style
            var style = GetStyleAt(pos);
            if (lexer.KeywordStylesStart <= style && style <= lexer.KeywordStylesEnd)
            {
                // is there a word at that position
                var word = GetWordFromPosition(pos);
                if (word?.Length > 0)
                {
                    // create search string
                    SelectString(pos, out search, out skip);

                    // select hint
                    for (int i = 0; i < search.Length; i++)
                        if (Hint.TryGetValue(search[i], out hint) && hint.Length > 0)
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
        public void SelectString(int position, out string[] search, out int[] skip)
        {
            // [1] ... block
            // [2] ... annotation
            // [3] ... command
            // [4] ... argument
            // [5] ... function
            // [6] ... type
            // [7] ... specifications
            // [8] ... qualifier
            // [9] ... variable
            // [10] ... branching

            // get word and preceding word at caret position
            var word = GetWordFromPosition(position);

            // get block surrounding caret position
            var block = BlockPosition(position);

            // inside the body of the block
            if (block != null && block[1] < position)
            {
                // get block header
                var header = GetTextRange(block[0], block[1] - block[0])
                    .Split(new[] { ' ', '\t', '\n', '\r' })
                    .Where(x => x.Length > 0)
                    .ToArray();
                var type = header[0];
                var anno = header[1];

                // get function surrounding caret position
                var function = FunctionPosition(position, block);

                // inside the body of the function
                if (function != null && function[1] < position)
                {
                    search = new[] {
                        // list local variables
                        $"[1]{type}[2]{anno}[9]{word}",
                        // list local functions
                        $"[1]{type}[2]{anno}[5]{word}",
                        // list global variables
                        $"[1]{type}[9]{word}",
                        // list global functions
                        $"[1]{type}[5]{word}",
                        // list global types
                        $"[1]{type}[6]{word}",
                    };
                }
                else
                {
                    // get layout surrounding caret position
                    var layout = LayoutPosition(position, block);

                    // inside the body of the layout
                    if (layout != null && layout[1] < position)
                    {
                        var text = GetTextRange(layout[0], layout[2] - layout[0]);
                        var prec = GetWordFromPosition(layout[0]);
                        var next = GetWordFromPosition(NextWordStartPosition(layout[2]));

                        search = new[] {
                            // list local qualifiers
                            $"[1]{type}[2]{anno}[7]{prec}[7]{next}[8]{word}",
                            $"[1]{type}[2]{anno}[7]{prec}[8]{word}",
                            // list global qualifiers
                            $"[1]{type}[7]{prec}[7]{next}[8]{word}",
                            $"[1]{type}[7]{prec}[8]{word}",
                        };
                    }
                    else
                    {
                        // first word in line
                        var cmd = GetWordFromPosition(
                            NextWordStartPosition(
                                Lines[LineFromPosition(position)].Position));

                        search = new[] {
                            // list local arguments
                            $"[1]{type}[2]{anno}[7]{cmd}[8]{word}",
                            // list local commands
                            $"[1]{type}[2]{anno}[7]{word}",
                            // list local specifications
                            $"[1]{type}[2]{anno}[3]{word}",
                            // list global arguments
                            $"[1]{type}[7]{cmd}[8]{word}",
                            // list global commands
                            $"[1]{type}[7]{word}",
                            // list global specifications
                            $"[1]{type}[3]{word}",
                            // list global types
                            $"[1]{type}[6]{word}",
                        };
                    }
                }
            }
            else
            {
                // get position of preceding word
                var precPos = PrecWordStartPosition(position);
                var prec = GetWordFromPosition(precPos);
                search = new[] {
                    // list annotations
                    $"[1]{prec}[2]{word}",
                    // list block types
                    $"[1]{word}"
                };
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

        private int[] FunctionPosition(int position, int[] block)
        {
            // get headers of the block surrounding the text position
            foreach (var x in FunctionPositions(block))
                if (x[0] < position && position < x[2])
                    return x;
            return null;
        }

        private int[] LayoutPosition(int position, int[] block)
        {
            // get headers of the block surrounding the text position
            foreach (var x in LayoutPositions(block))
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
        /// Get the position of all functions within a block.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int[]> FunctionPositions(int[] block)
            => from x in this.GetFunctionPositions(block[1], block[2])
               select new[] { x.Index, x.Index + x.Value.IndexOf('{'), x.Index + x.Length };

        private IEnumerable<int[]> LayoutPositions(int[] block)
            => from x in this.GetLayoutPositions(block[1], block[2])
               select new[] { x.Index, x.Index + x.Value.IndexOf('('), x.Index + x.Length };

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
