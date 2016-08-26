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
            var word = GetWordFromPosition(position);
            //var word = Text.Subrange(WordStartPosition(position, false), position);
            var keywords = FxLexer.SelectKeywords(GetStyleAt(position), word);

            // show auto complete list
            if (keywords.Count() > 0)
                AutoCShow(position - WordStartPosition(position, true),
                    keywords.OrderBy(s => s, StringComparer.CurrentCultureIgnoreCase).Cat("|"));
        }

        /// <summary>
        /// Handle mouse move event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
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
                    // get hint for keyword
                    var hint = FxLexer.GetKeywordHint(GetStyleAt(pos), word);
                    if (hint != null)
                    {
                        CallTipShow(WordStartPosition(pos, true), hint);
                        return;
                    }
                }
            }

            CallTipCancel();
        }
    }
}
