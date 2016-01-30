using System.Collections.Generic;
using System.Linq;

namespace App
{
    partial class CodeEditor
    {
        /// <summary>
        /// Clear all indicators of indicator index.
        /// </summary>
        /// <param name="index">Indicator index.</param>
        public void ClearIndicators(int index)
        {
            // Remove all uses of our indicator
            IndicatorCurrent = index;
            IndicatorClearRange(0, TextLength);
            IndicatorRanges[index].Clear();
        }

        /// <summary>
        /// Add indicators to indicator index.
        /// </summary>
        /// <param name="index">Indicator index.</param>
        /// <param name="ranges">Indicator ranges in the text.</param>
        public void AddIndicators(int index, IEnumerable<int[]> ranges)
        {
            // set active indicator
            IndicatorCurrent = index;
            
            foreach (var range in ranges)
            {
                // add indicator range
                IndicatorRanges[index].Add(range);
                IndicatorFillRange(range[0], range[1] - range[0]);
            }
        }

        /// <summary>
        /// Go to next indicator of indicator index.
        /// </summary>
        /// <param name="index">Indicator index.</param>
        /// <param name="skip">Number of indicator positions to skip.</param>
        public void GotoNextIndicator(int index, int skip = 0)
            => GotoNextRange(IndicatorRanges[index], skip);

        /// <summary>
        /// Select (as in select text) all indicators.
        /// </summary>
        /// <param name="index">Indicator index.</param>
        public void SelectIndicators(int index)
        {
            // get current caret position
            var cur = CurrentPosition;

            // get selected word ranges
            var ranges = IndicatorRanges[index];
            var count = ranges.Count();

            // select all word ranges
            ClearSelections();
            foreach (var range in ranges)
                AddSelection(range[0], range[1]);

            // ClearSelections adds a default selection
            // at postion 0 which we need to remove
            DropSelection(0);

            // rotate through all selections until we arive at the original caret position
            for (int i = 0; (cur < CurrentPosition || AnchorPosition < cur) && i < count; i++)
                RotateSelection();
        }

        /// <summary>
        /// Get all words from all selected text ranges.
        /// If a word is only partially selected, the
        /// returned bool value will be 'false'.
        /// </summary>
        /// <returns>Returns a dictionary of all found words
        /// where the keys are the words or partial words
        /// found and the value indicates wheather the word is
        /// a whole word.</returns>
        private Dictionary<string, bool> GetWordsFromSelections()
        {
            var words = new Dictionary<string, bool>();

            // highlight all selected text
            foreach (var selection in Selections)
            {
                // text length of selection
                var len = selection.End - selection.Start;
                // if not a selected word
                var word = len == 0 ?
                    // select word at caret position
                    GetWordFromPosition(selection.Caret) :
                    // get selected text
                    GetTextRange(selection.Start, len);

                // do not add empty strings 
                // (GetWordFromPosition can return those)
                if (word.Length == 0)
                    continue;

                // was selection a whole word
                var isWholeWord = len == 0 || IsRangeWord(selection.Start, selection.End);

                // we cannot add the same key twice
                if (words.ContainsKey(word))
                    words[word] |= isWholeWord;
                else
                    words.Add(word, isWholeWord);
            }

            return words;
        }
    }
}
