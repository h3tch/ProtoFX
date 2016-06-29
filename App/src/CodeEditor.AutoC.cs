using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;

namespace App
{
    partial class CodeEditor
    {
        public void InitializeAutoC()
        {
            // auto completion settings
            AutoCSeparator = '|';
            AutoCMaxHeight = 9;
            MouseMove += new MouseEventHandler(HandleMouseMove);
        }

        /// <summary>
        /// Show auto complete menu for the specified text position.
        /// </summary>
        /// <param name="position">The text position for which 
        /// to show the auto complete menu</param>
        public void AutoCShow(int position)
        {
            //string[] search;
            //int[] skip;

            //// create search string
            //SelectStringBlock(position, out search, out skip);

            //// search for all keywords starting with the
            //// search string and not containing subkeywords
            //var keywords = search.Zip(skip,
            //    (s, i) => from x in KeywordTrie[s]
            //              where x.IndexOf('[', s.Length) <= 0
            //              select x.Substring(i))
            //    .Cat();

            var keywords = FxLexer.GetPotentialKeywords(Text, position);

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
            //string[] search;
            //string hint;
            //int[] skip;

            // check if code hints are enabled
            if (EnableCodeHints == false)
                return;

            // convert cursor position to text position
            int pos = CharPositionFromPoint(e.X, e.Y);

            // select keywords using the current text position
            // is the style at that position a valid hint style
            var style = GetStyleAt(pos);
            if (style > 0)
            {
                // is there a word at that position
                var word = GetWordFromPosition(pos);
                if (word?.Length > 0)
                {
                    //// create search string
                    //SelectStringBlock(pos, out search, out skip);

                    //// select hint
                    //for (int i = 0; i < search.Length; i++)
                    //{
                    //    if (Hint.TryGetValue(search[i], out hint) && hint.Length > 0)
                    //    {
                    //        CallTipShow(WordStartPosition(pos, true), hint.Substring(1));
                    //        return;
                    //    }
                    //}

                    var hint = FxLexer.GetKeywordHint(Text, pos, word);
                    if (hint != null)
                    {
                        CallTipShow(WordStartPosition(pos, true), hint);
                        return;
                    }
                }
            }

            CallTipCancel();
        }
        
        ///// <summary>
        ///// Create select string for the specified text position.
        ///// </summary>
        ///// <param name="position"></param>
        ///// <param name="search"></param>
        ///// <param name="skip"></param>
        //private void SelectStringBlock(int position, out string[] search, out int[] skip)
        //{
        //    // make sure the text position lies within the text
        //    position = Math.Max(0, Math.Min(TextLength - 1, position));

        //    // get word and preceding word at caret position
        //    var word = GetWordFromPosition(position);

        //    // get block surrounding caret position
        //    var block = BlockPosition(position);

        //    // inside the body of the block
        //    if (block != null && block[1] < position)
        //    {
        //        // get block header
        //        var header = GetTextRange(block[0], block[1] - block[0])
        //            .Split(new[] { ' ', '\t', '\n', '\r' })
        //            .Where(x => x.Length > 0)
        //            .ToArray();

        //        switch (header[0])
        //        {
        //            case "shader":
        //                SelectStringGlsl(position, block, header, word, out search);
        //                break;
        //            default:
        //                SelectStringDefault(position, header, word, out search);
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        // get position of preceding word
        //        var precPos = PrecWordStartPosition(position);
        //        var prec = GetWordFromPosition(precPos);

        //        search = new[] {
        //            // list annotations
        //            $"[{BLOCK}]{prec}[{ANNO}]{word}",
        //            // list block types
        //            $"[{BLOCK}]{word}"
        //        };
        //    }

        //    skip = search.Select(x => x.Length - word.Length).ToArray();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="position"></param>
        ///// <param name="header"></param>
        ///// <param name="word"></param>
        ///// <param name="search"></param>
        //private void SelectStringDefault(int position, string[] header, string word, out string[] search)
        //{
        //    // first word in line
        //    var cmd = GetWordFromPosition(NextWordStartPosition(
        //            Lines[LineFromPosition(position)].Position));

        //    search = new[] {
        //        $"[{BLOCK}]{header[0]}[{CMD}]{cmd}[{ARG}]{word}",
        //        $"[{BLOCK}]{header[0]}[{CMD}]{word}",
        //    };
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="position"></param>
        ///// <param name="block"></param>
        ///// <param name="header"></param>
        ///// <param name="word"></param>
        ///// <param name="search"></param>
        //private void SelectStringGlsl(int position, int[] block, string[] header, string word, out string[] search)
        //{
        //    // get block header
        //    var type = header[0];
        //    var anno = header[1];

        //    // get function surrounding caret position
        //    var function = FunctionPosition(position, block);

        //    // inside the body of the function
        //    if (function != null && function[1] < position)
        //    {
        //        search = new[] {
        //            // list local variables
        //            $"[{BLOCK}]{type}[{ANNO}]{anno}[{VAR}]{word}",
        //            // list local functions
        //            $"[{BLOCK}]{type}[{ANNO}]{anno}[{FUNC}]{word}",
        //            // list global variables
        //            $"[{BLOCK}]{type}[{VAR}]{word}",
        //            // list global functions
        //            $"[{BLOCK}]{type}[{FUNC}]{word}",
        //            // list global types
        //            $"[{BLOCK}]{type}[{TYPE}]{word}",
        //        };
        //        return;
        //    }

        //    // get layout surrounding caret position
        //    var layout = LayoutPosition(position, block);

        //    // inside the body of the layout
        //    if (layout != null && layout[1] < position)
        //    {
        //        var text = GetTextRange(layout[0], layout[2] - layout[0]);
        //        var prec = GetWordFromPosition(layout[0]);
        //        var next = GetWordFromPosition(NextWordStartPosition(layout[2]));

        //        search = new[] {
        //            // list local qualifiers
        //            $"[{BLOCK}]{type}[{ANNO}]{anno}[{SPEC}]{prec}[{SPEC}]{next}[{QUAL}]{word}",
        //            $"[{BLOCK}]{type}[{ANNO}]{anno}[{SPEC}]{prec}[{QUAL}]{word}",
        //            // list global qualifiers
        //            $"[{BLOCK}]{type}[{SPEC}]{prec}[{SPEC}]{next}[{QUAL}]{word}",
        //            $"[{BLOCK}]{type}[{SPEC}]{prec}[{QUAL}]{word}",
        //        };
        //        return;
        //    }

        //    // first word in line
        //    var cmd = GetWordFromPosition(NextWordStartPosition(
        //            Lines[LineFromPosition(position)].Position));

        //    search = new[] {
        //        // list local arguments
        //        $"[{BLOCK}]{type}[{ANNO}]{anno}[{SPEC}]{cmd}[{QUAL}]{word}",
        //        // list local commands
        //        $"[{BLOCK}]{type}[{ANNO}]{anno}[{SPEC}]{word}",
        //        // list local specifications
        //        $"[{BLOCK}]{type}[{ANNO}]{anno}[{CMD}]{word}",
        //        // list global arguments
        //        $"[{BLOCK}]{type}[{SPEC}]{cmd}[{QUAL}]{word}",
        //        // list global commands
        //        $"[{BLOCK}]{type}[{SPEC}]{word}",
        //        // list global specifications
        //        $"[{BLOCK}]{type}[{CMD}]{word}",
        //        // list global types
        //        $"[{BLOCK}]{type}[{TYPE}]{word}",
        //    };
        //}

        ///// <summary>
        ///// Get the block surrounding the specified text position.
        ///// </summary>
        ///// <param name="position"></param>
        ///// <returns>Returns the start, open brace and end position
        ///// of the block as an int array.</returns>
        //private int[] BlockPosition(int position)
        //{
        //    // get headers of the block surrounding the text position
        //    foreach (var x in BlockPositions())
        //        if (x[0] < position && position < x[2])
        //            return x;
        //    return null;
        //}

        ///// <summary>
        ///// Get the function surrounding the specified text position.
        ///// </summary>
        ///// <param name="position"></param>
        ///// <param name="block"></param>
        ///// <returns></returns>
        //private int[] FunctionPosition(int position, int[] block)
        //{
        //    // get headers of the block surrounding the text position
        //    foreach (var x in FunctionPositions(block))
        //        if (x[0] < position && position < x[2])
        //            return x;
        //    return null;
        //}

        ///// <summary>
        ///// Get the layout surrounding the specified text position.
        ///// </summary>
        ///// <param name="position"></param>
        ///// <param name="block"></param>
        ///// <returns></returns>
        //private int[] LayoutPosition(int position, int[] block)
        //{
        //    // get headers of the block surrounding the text position
        //    foreach (var x in LayoutPositions(block))
        //        if (x[0] < position && position < x[2])
        //            return x;
        //    return null;
        //}

        ///// <summary>
        ///// Get the position of all blocks.
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerable<int[]> BlockPositions()
        //    => from x in this.GetBlockPositions()
        //       select new[] { x.Index, x.Index + x.Value.IndexOf('{'), x.Index + x.Length };

        ///// <summary>
        ///// Get the position of all functions within a block.
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerable<int[]> FunctionPositions(int[] block)
        //    => from x in this.GetFunctionPositions(block[1], block[2])
        //       select new[] { x.Index, x.Index + x.Value.IndexOf('{'), x.Index + x.Length };

        ///// <summary>
        ///// Get the position of all layouts within a block.
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerable<int[]> LayoutPositions(int[] block)
        //    => from x in this.GetLayoutPositions(block[1], block[2])
        //       select new[] { x.Index, x.Index + x.Value.IndexOf('('), x.Index + x.Length };

        ///// <summary>
        ///// Find the start position of the preceding word. If the
        ///// specified position lies outside any word the position of
        ///// the closest preceding word will be returned. If it lies
        ///// inside a word the position of the next preceding word will
        ///// be returned.
        ///// </summary>
        ///// <param name="position">start position</param>
        ///// <returns>Returns the position of the preceding word or 0
        ///// in case no preceding word could be found.</returns>
        //public int PrecWordStartPosition(int position)
        //{
        //    // go to next non-word char
        //    while (position > 0 && char.IsLetterOrDigit(Text[position]))
        //        position--;
        //    // go to next word char (the end of the preceding word)
        //    while (position > 0 && !char.IsLetterOrDigit(Text[position]))
        //        position--;
        //    // go to start of the preceding word
        //    while (position > 0 && char.IsLetterOrDigit(Text[position - 1]))
        //        position--;

        //    return position;
        //}

        ///// <summary>
        ///// Find the start position of the next word. If the
        ///// specified position lies outside any word the position of
        ///// the next closest word will be returned. If it lies
        ///// inside a word the position of the next word will
        ///// be returned.
        ///// </summary>
        ///// <param name="position">start position</param>
        ///// <returns>Returns the position of the next word or <code>Length</code>
        ///// in case no preceding word could be found.</returns>
        //public int NextWordStartPosition(int position)
        //{
        //    // go to the first non-word char
        //    while (position < Text.Length && char.IsLetterOrDigit(Text[position]))
        //        position++;
        //    // go to the first word char (the start of the next word)
        //    while (position < Text.Length && !char.IsLetterOrDigit(Text[position]))
        //        position++;

        //    return position;
        //}
    }
}
