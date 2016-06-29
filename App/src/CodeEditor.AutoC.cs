using System.Linq;
using System.Windows.Forms;

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
            // find potential keywords
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
                    // select hint
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
    }
}
