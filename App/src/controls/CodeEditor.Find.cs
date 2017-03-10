using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Math;

namespace ScintillaNET
{
    partial class CodeEditor
    {
        private TextBox FindText;

        /// <summary>
        /// Initialize find and replace.
        /// </summary>
        private void InitializeFindAndReplace()
        {
            FindText = new TextBox();
            FindText.Parent = this;
            FindText.MinimumSize = new Size(0, 0);
            FindText.MaximumSize = new Size(1, 1);
            FindText.SetBounds(0, 0, 1, 1);
            FindText.BackColor = Theme.BackColor;
            FindText.ForeColor = Theme.BackColor;
            FindText.TextChanged += HandleFindTextChanged;
            FindText.KeyUp += HandleFindKeyUp;
            FindText.GotFocus += HandleFindGotFocus;
            FindText.LostFocus += HandleFindLostFocus;
        }

        /// <summary>
        /// Find all ranges of the specified strings.
        /// </summary>
        /// <param name="words">Must be a dictionary of strings (keys) to search for and
        /// whether these strings represent whole words (true or false value).</param>
        /// <returns></returns>
        private IEnumerable<int[]> FindWordRanges(Dictionary<string, bool> words)
        {
            return words.SelectMany(word => FindWordRanges(word.Key, word.Value));
        }

        /// <summary>
        /// Find all ranges of the specified string.
        /// </summary>
        /// <param name="word">String to search for.</param>
        /// <param name="wholeWord">Only search for whole words if true.</param>
        /// <returns></returns>
        private IEnumerable<int[]> FindWordRanges(string word, bool wholeWord)
        {
            // Search the document
            TargetStart = 0;
            TargetEnd = TextLength;
            SearchFlags = SearchFlags.MatchCase | (wholeWord ? SearchFlags.WholeWord : 0);
            // while can find word
            while (SearchInTarget(word) != -1)
            {
                yield return new [] { TargetStart, TargetEnd };
                // Search the remainder of the document
                TargetStart = TargetEnd;
                TargetEnd = TextLength;
            }
        }

        /// <summary>
        /// Goto next range closest to the current position of the caret.
        /// </summary>
        /// <param name="ranges">The ranges to check.</param>
        /// <param name="skip">How many ranges should be skipped? E.g., if the caret
        /// is inside a range and you want to find the next one, skip should be 1.
        /// If the caret should stay at that position skip has to be 0 (default).</param>
        private void GotoNextRange(IEnumerable<int[]> ranges, int skip = 0)
        {
            var count = ranges.Count();
            if (count == 0)
                return;
            // find closest index
            var idx = (ranges
                .Select(x => new[] { x[0] - CurrentPosition, x[1] - CurrentPosition })
                .Select(x => Sign(x[0]) == Sign(x[1]) ? Sign(Min(x[0], x[1])) : 0)
                .IndexOf(x => x >= 0) + skip) % count;
            // select range at closest index
            var range = ranges.Skip(idx < 0 ? count + idx : idx).First();
            // go to range position
            if (range != null)
                GotoPosition(range[0]);
        }

        /// <summary>
        /// Update highlights and go to closest word matching the search string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleFindTextChanged(object s, EventArgs e)
        {
            var textbox = s as TextBox;
            var editor = textbox.Parent as CodeEditor;

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

        /// <summary>
        /// Switch to find-mode if the search box receives focus.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleFindGotFocus(object s, EventArgs e)
        {
            // on focus color the caret red
            var editor = (s as TextBox).Parent as CodeEditor;
            editor.CaretWidth = 2;
            editor.CaretForeColor = Color.Red;
        }

        /// <summary>
        /// Find-mode needs to be exited when any event occurs that changes the focus.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleFindLostFocus(object s, EventArgs e)
        {
            // on lost focus color the caret black
            var editor = (s as TextBox).Parent as CodeEditor;
            editor.CaretWidth = 1;
            editor.CaretForeColor = Color.Black;
        }

        /// <summary>
        /// Handle key events in find-mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleFindKeyUp(object s, KeyEventArgs e)
        {
            var editor = (s as TextBox).Parent as CodeEditor;

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
