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
        public event ShowTipEvent ShowCallTip;
        [Category("Behavior"), Description("Occurs when CallTipCancel is called.")]
        public event CancleTipEvent CancleCallTip;
        [Category("Behavior"), Description("Occurs when CallTipShow is called.")]
        public event ShowTipEvent ShowPerfTip;
        [Category("Behavior"), Description("Occurs when CallTipCancel is called.")]
        public event CancleTipEvent CanclePerfTip;

        #endregion

        #region CONSTRUCTION

        /// <summary>
        /// Initialize auto complete functionality.
        /// </summary>
        public void InitializeAutoC()
        {
            // auto completion settings
            AutoCSeparator = '|';
            AutoCMaxHeight = 9;
            // handle some events to support call tip functionality
            CharAdded += AutoCHandleCharAdded;
            CustomMouseMove += AutoCHandleCustomMouseMove;
            CustomMouseHover += AutoCMouseHover;
            MouseScroll += AutoCMouseScroll;
        }

        #endregion

        #region SHOW AND HIDE CALLTIP OR AUTOC

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
        /// Wait for char add event to handle auto intent and auto complete.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void AutoCHandleCharAdded(object s, CharAddedEventArgs e)
        {
            // auto complete
            if (char.IsLetter((char)e.Char))
                AutoCShow(CurrentPosition);
        }

        /// <summary>
        /// Handle mouse move event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCHandleCustomMouseMove(object sender, MouseEventArgs e)
        {
            // hide call tip on mouse move
            if (CallTipActive)
                CallTipCancel();
        }

        /// <summary>
        /// Handle mouse hover events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCMouseHover(object sender, EventArgs e)
        {
            // check if code hints are enabled
            if (EnableCodeHints == false)
                return;

            // convert cursor position to text position
            var mouse = PointToClient(Cursor.Position);
            var pos = CharPositionFromPoint(mouse.X, mouse.Y);

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
                        CallTipShow(WordStartPosition(pos, true), hint);
                }
            }
        }

        /// <summary>
        /// Hide the call tip when the mouse scroll event occurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoCMouseScroll(object sender, EventArgs e)
        {
            CallTipCancel();
        }

        #endregion

        #region CALL TIP METHODS

        /// <summary>
        /// Show a call tip at the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="definition"></param>
        public new void CallTipShow(int position, string definition)
        {
            TipShow(ref callTip, ShowCallTip, position, definition);
        }

        /// <summary>
        /// Hide call tip.
        /// </summary>
        public new void CallTipCancel()
        {
            TipCancel(callTip, CancleCallTip);
        }

        /// <summary>
        /// Is the call tip tool tip visible.
        /// </summary>
        public new bool CallTipActive => callTip?.Visible ?? false;

        /// <summary>
        /// Show a performance call tip at the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void PerfTipShow(int position, Array X, Array Y)
        {
            TipShow(ref perfTip, ShowPerfTip, position, new[] { X, Y });
        }

        /// <summary>
        /// Hide performance call tip.
        /// </summary>
        public void PerfTipCancel()
        {
            TipCancel(perfTip, CanclePerfTip);
        }

        /// <summary>
        /// Show the call tip.
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="handlers"></param>
        /// <param name="position"></param>
        /// <param name="hint"></param>
        private void TipShow(ref CallTip tip, ShowTipEvent handlers, int position, object hint)
        {
            // create calltip class
            tip = new CallTip();
            tip.Enter += new EventHandler(TipMouseEnter);
            tip.SetChartIntervals(10, 0);
            Theme.Apply(tip);

            // get screen position
            var rect = GetWordBounds(position);
            rect.Location = PointToScreen(rect.Location);
            rect.Inflate(3, 3);
            
            // invoke event hadlers
            var e = new ShowTipEventArgs
            {
                TextPosition = position,
                Definition = hint,
                ScreenPosition = rect.Location
            };
            handlers?.Invoke(this, e);
            if (e.Cancle)
                return;

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

        /// <summary>
        /// Cancel the call tip.
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="handlers"></param>
        private void TipCancel(CallTip tip, CancleTipEvent handlers)
        {
            if (tip == null || !tip.Visible)
                return;

            var e = new CancleTipEventArgs();
            handlers?.Invoke(this, e);
            if (e.Cancle)
                return;

            //tip.Hide();
            tip.Close();
            tip.Dispose();
            OnMouseLeave(null);
        }

        /// <summary>
        /// Close call tips when the mouse enters.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void TipMouseEnter(object s, EventArgs e)
        {
            TipCancel(s as CallTip, s == callTip ? CancleCallTip : CanclePerfTip);
        }

        #endregion
    }

    #region CALL TIP EVENTS

    public delegate void ShowTipEvent(object sender, ShowTipEventArgs e);
    public delegate void CancleTipEvent(object sender, CancleTipEventArgs e);

    public class ShowTipEventArgs : EventArgs
    {
        public int TextPosition;
        public Point ScreenPosition;
        public object Definition;
        public bool Cancle;
    }

    public class CancleTipEventArgs : EventArgs
    {
        public bool Cancle;
    }

    #endregion
}
