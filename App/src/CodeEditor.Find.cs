using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Math;
using Range = System.Tuple<int, int>;
using RangeIter = System.Collections.Generic.IEnumerable<System.Tuple<int, int>>;

namespace App
{
    partial class CodeEditor
    {
        private TextBox FindText;

        private RangeIter FindWordRanges(Dictionary<string, bool> words)
        {
            foreach (var word in words)
                foreach (var tupel in FindWordRanges(word.Key, word.Value))
                    yield return tupel;
        }

        private RangeIter FindWordRanges(string word, bool wholeWord)
        {
            // Search the document
            TargetStart = 0;
            TargetEnd = TextLength;
            SearchFlags = SearchFlags.MatchCase | (wholeWord ? SearchFlags.WholeWord : 0);
            // while can find word
            while (SearchInTarget(word) != -1)
            {
                yield return new Tuple<int, int>(TargetStart, TargetEnd);
                // Search the remainder of the document
                TargetStart = TargetEnd;
                TargetEnd = TextLength;
            }
        }

        private void GotoNextRange(RangeIter ranges, int skip = 0)
        {
            var count = ranges.Count();
            if (count == 0)
                return;
            // find closest index
            var idx = (ranges
                .Select(x => new[] { x.Item1 - CurrentPosition, x.Item2 - CurrentPosition })
                .Select(x => Sign(x[0]) == Sign(x[1]) ? Sign(Min(x[0], x[1])) : 0)
                .IndexOf(x => x >= 0) + skip) % count;
            // select range at closest index
            Range range = ranges.Skip(idx < 0 ? count + idx : idx).First();
            // go to range position
            if (range != null)
                GotoPosition(range.Item1);
        }

        private void HandleFindTextChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            var editor = (CodeEditor)textbox.Parent;

            editor.ClearIndicators(HighlightIndicatorIndex);
            if (textbox.Text.Length == 0)
                return;

            // select all ranges matching the search term
            var ranges = editor.FindWordRanges(textbox.Text, false);

            // if nothing can be selected, reduce the search term until something can
            while (ranges.Count() == 0 && textbox.Text.Length > 1)
            {
                textbox.Text = textbox.Text.Substring(0, textbox.Text.Length - 1);
                ranges = editor.FindWordRanges(textbox.Text, false);
            }
            textbox.Select(textbox.Text.Length, 0);

            // highlight all selected words
            editor.AddIndicators(HighlightIndicatorIndex, ranges);

            // rotate through all ranges until we arrive
            // at the one closest to the caret position
            GotoNextRange(ranges, 0);
        }

        private void HandleFindGotFocus(object sender, EventArgs e)
        {
            // on focus color the caret red
            var textbox = (TextBox)sender;
            var editor = (CodeEditor)textbox.Parent;
            editor.CaretWidth = 2;
            editor.CaretForeColor = Color.Red;
        }

        private void HandleFindLostFocus(object sender, EventArgs e)
        {
            // on lost focus color the caret black
            var textbox = (TextBox)sender;
            var editor = (CodeEditor)textbox.Parent;
            editor.CaretWidth = 1;
            editor.CaretForeColor = Color.Black;
        }

        private void HandleFindKeyUp(object sender, KeyEventArgs e)
        {
            var textbox = (TextBox)sender;
            var editor = (CodeEditor)textbox.Parent;

            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Down:
                    // go to next found indicator
                    GotoNextIndicator(HighlightIndicatorIndex, 1);
                    break;
                case Keys.Left:
                case Keys.Up:
                    // go to last found indicator
                    GotoNextIndicator(HighlightIndicatorIndex, -1);
                    break;
                case Keys.R:
                    // move focus to editor and
                    // start with text replacement
                    if (e.Control)
                    {
                        editor.SelectIndicators(HighlightIndicatorIndex);
                        editor.Focus();
                    }
                    break;
                case Keys.Escape:
                    // move focus to editor
                    editor.Focus();
                    break;
            }
        }
    }
}
