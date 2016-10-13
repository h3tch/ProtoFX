using System.Linq;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScintillaNET
{
    partial class CodeEditor
    {
        #region FIELDS

        private static CallTip callTip;
        private static CallTip perfTip;

        #endregion

        #region EVENTS

        [Category("Behavior"), Description("Occurs when CallTipShow is called.")]
        public event ShowTipEventHandler ShowCallTip;
        [Category("Behavior"), Description("Occurs when CallTipCancel is called.")]
        public event CancleTipEventHandler CancleCallTip;
        [Category("Behavior"), Description("Occurs when CallTipShow is called.")]
        public event ShowTipEventHandler ShowPerfTip;
        [Category("Behavior"), Description("Occurs when CallTipCancel is called.")]
        public event CancleTipEventHandler CanclePerfTip;

        #endregion

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

        #region CALL TIP METHODS

        /// <summary>
        /// Show a call tip at the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="definition"></param>
        public new void CallTipShow(int position, string definition)
            => TipShow(ref callTip, ShowCallTip, position, definition);

        /// <summary>
        /// Hide call tip.
        /// </summary>
        public new void CallTipCancel() => TipCancel(callTip, CancleCallTip);

        /// <summary>
        /// Show a performance call tip at the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void PerfTipShow(int position, Array X, Array Y)
            => TipShow(ref perfTip, ShowPerfTip, position, new[] { X, Y });

        /// <summary>
        /// Hide performance call tip.
        /// </summary>
        public void PerfTipCancel() => TipCancel(perfTip, CanclePerfTip);

        /// <summary>
        /// Show the call tip.
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="handlers"></param>
        /// <param name="position"></param>
        /// <param name="hint"></param>
        private void TipShow(ref CallTip tip, ShowTipEventHandler handlers, int position, object hint)
        {
            // create calltip class
            if (tip == null)
            {
                tip = new CallTip();
                tip.Enter += new EventHandler(tip_MouseEnter);
                tip.SetChartIntervals(10, 0);
                Theme.Apply(tip);
            }

            // get screen position
            var rect = GetWordBounds(position);
            rect.Location = PointToScreen(rect.Location);
            rect.Inflate(3, 3);

            // Fore some reason PointToScreen can return
            // different positions. In this case the call
            // tip needs to be repositioned.
            if (!tip.Visible || tip.Location != rect.Location)
            {
                // invoke event hadlers
                var e = new ShowTipEventHandlerArgs();
                e.TextPosition = position;
                e.Definition = hint;
                e.ScreenPosition = rect.Location;
                handlers?.Invoke(this, e);
                if (e.Cancle)
                    return;

                // make sure the calltip window
                // is in front of all others
                tip.BringToFront();

                // show calltip
                if (hint is string)
                {
                    tip.Show(rect, (string)hint);
                }
                else if (hint is Array)
                {
                    const int w = 500;
                    var X = (Array)((Array)hint).GetValue(0);
                    var Y = (Array)((Array)hint).GetValue(1);
                    tip.Show(rect, X, Y, w, w * 0.6, ContentAlignment.TopLeft, 0.3, 0.3);
                }
            }
        }

        /// <summary>
        /// Cancel the call tip.
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="handlers"></param>
        private void TipCancel(CallTip tip, CancleTipEventHandler handlers)
        {
            if (tip == null || !tip.Visible)
                return;

            var e = new CancleTipEventHandlerArgs();
            handlers?.Invoke(this, e);
            if (e.Cancle)
                return;

            tip.Hide();
        }

        /// <summary>
        /// Close call tips when the mouse enters.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void tip_MouseEnter(object s, EventArgs e)
            => TipCancel(s as CallTip, s == callTip ? CancleCallTip : CanclePerfTip);

        #endregion
    }

    #region CALL TIP EVENTS

    public delegate void ShowTipEventHandler(object sender, ShowTipEventHandlerArgs e);
    public delegate void CancleTipEventHandler(object sender, CancleTipEventHandlerArgs e);

    public class ShowTipEventHandlerArgs
    {
        public int TextPosition;
        public Point ScreenPosition;
        public object Definition;
        public bool Cancle;
    }

    public class CancleTipEventHandlerArgs
    {
        public bool Cancle;
    }

    #endregion
}
