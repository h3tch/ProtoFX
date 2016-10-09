using System.Linq;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace ScintillaNET
{
    public 
    partial class CodeEditor
    {
        private static CallTip tip;
        private static int tipOffsetY;

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

        /// <summary>
        /// Show a calltip at the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="definition"></param>
        public new void CallTipShow(int position, string definition)
        {
            // create calltip class
            if (tip == null)
            {
                tip = new CallTip();
                var font = new Font(Styles[Style.Default].Font, Styles[Style.Default].SizeF);
                tipOffsetY = TextRenderer.MeasureText("W", font).Height;
                // apply the current theme to the call tip components
                Theme.Apply(tip);
            }
            
            // get screen position
            int x = PointXFromPosition(position);
            int y = PointYFromPosition(position);
            var p = PointToScreen(new Point(x, y));

            // Fore some reason PointToScreen can return
            // different positions. In this case the call
            // tip needs to be repositioned.
            if (!tip.Visible || tip.Location != p)
            {
                // make sure the calltip window
                // is in front of all others
                tip.BringToFront();
                // show calltip
                tip.Show(p.X, p.Y, 5, 5, 0, tipOffsetY, definition);
            }
        }

        /// <summary>
        /// Hide calltip.
        /// </summary>
        public new void CallTipCancel() => tip?.Hide();
    }
}
