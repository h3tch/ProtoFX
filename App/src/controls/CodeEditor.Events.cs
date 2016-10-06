using ScintillaNET;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
    public partial class CodeEditor
    {
        private bool DisableEditing = false;
        private Pen grayPen;
        private Pen dashedPen;
        private Size lineSize;
        public static int NewLineHelper = 100;

        /// <summary>
        /// Initialize class events.
        /// </summary>
        private void InitializeEvents()
        {
            TextChanged += new EventHandler(HandleTextChanged);
            UpdateUI += new EventHandler<UpdateUIEventArgs>(HandleUpdateUI);

            // enable drag & drop
            AllowDrop = true;
            DragOver += new DragEventHandler(HandleDragOver);
            DragDrop += new DragEventHandler(HandleDragDrop);

            // search & replace
            KeyUp += new KeyEventHandler(HandleKeyUp);
            KeyDown += new KeyEventHandler(HandleKeyDown);
            InsertCheck += new EventHandler<InsertCheckEventArgs>(HandleInsertCheck);
            CharAdded += new EventHandler<CharAddedEventArgs>(HandleCharAdded);
            
            MouseWheel += new MouseEventHandler(HandleMouseWheel);
            Painted += new EventHandler<EventArgs>(HandlePainted);

            // create default pens
            grayPen = new Pen(Brushes.Gray);
            dashedPen = new Pen(Brushes.LightGray);
            dashedPen.DashPattern = new[] { 3f, 6f };

            // measure default line size
            lineSize = TextRenderer.MeasureText(new string('/', NewLineHelper), Font);
        }

        /// <summary>
        /// On insert text event, auto format the text.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleInsertCheck(object s, InsertCheckEventArgs e)
        {
            // do not insert text when
            // the Ctrl key is pressed
            if (DisableEditing)
                e.Text = string.Empty;

            // auto indent
            if (e.Text.EndsWith("\n"))
            {
                // get text of line above
                var text = Lines[LineFromPosition(CurrentPosition)].Text;
                // insert indent of line above
                e.Text += Regex.Match(text, @"^[ \t]*").Value;
                // if line above ends with '{' insert indent
                if (Regex.IsMatch(text, @"{\s*$"))
                    e.Text += '\t';
            }
        }
        
        /// <summary>
        /// Wait for char add event to handle auto intent and auto complete.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleCharAdded(object s, CharAddedEventArgs e)
        {
            // auto indent
            if (e.Char == '}')
            {
                int curLine = LineFromPosition(CurrentPosition);
                // Check whether the bracket is the only non
                // whitespace in the line. For cases like "if() { }".
                if (Lines[curLine].Text.Trim() == "}")
                    SetIndent(curLine, GetIndent(curLine) - 4);
            }

            // auto complete
            if (char.IsLetter((char)e.Char))
                AutoCShow(CurrentPosition);
        }

        /// <summary>
        /// Handle text change event to mark tabs that need to be saved.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleTextChanged(object s, EventArgs e)
        {
            // get class references
            var tab = Parent as TabPage;

            // add file changed indicator '*'
            if (!tab?.Text.EndsWith("*") ?? false)
                tab.Text = tab.Text + '*';

            // update line number margins
            UpdateLineNumbers();

            // update code folding
            UpdateCodeFolding(FirstVisibleLine, LastVisibleLine);
        }

        /// <summary>
        /// Handle selection change events (when the caret changes the position).
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleUpdateUI(object s, UpdateUIEventArgs e)
        {
            // handle selection changed events
            switch (e.Change)
            {
                case UpdateChange.Selection:
                    // Update indicators only if the editor is in focus. This
                    // is necessary, because they are also used by 'Find & Replace'.
                    if (Focused)
                    {
                        // get whole words from selections and get ranges from these words
                        var ranges = FindWordRanges(GetWordsFromSelections());
                        // highlight all selected words
                        ClearIndicators(HighlightIndicatorIndex);
                        AddIndicators(HighlightIndicatorIndex, ranges);
                    }
                    break;
                case UpdateChange.HScroll:
                case UpdateChange.VScroll:
                    // update code folding
                    UpdateCodeFolding(FirstVisibleLine, LastVisibleLine);
                    break;
            }
        }

        /// <summary>
        /// Handle mouse wheel events.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleMouseWheel(object s, MouseEventArgs e)
        {
            CallTipCancel();
            UpdateCodeFolding(FirstVisibleLine, LastVisibleLine);
        }

        /// <summary>
        /// Handle drag and drop of text parts in the editor.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleDragOver(object s, DragEventArgs e)
        {
            // convert cursor position to text position
            var point = PointToClient(new Point(e.X, e.Y));
            int pos = CharPositionFromPoint(point.X, point.Y);

            // refresh text control
            Refresh();

            // if the mouse is over a selected area, dropping will not be possible
            if (Selections.IndexOf(x => x.Start <= pos && pos < x.End) >= 0)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // draw line at cursor position in text
            var g = CreateGraphics();
            var pen = new Pen(Color.Black, 2);
            var height = TextRenderer.MeasureText("0", Font).Height;
            point.X = PointXFromPosition(pos);
            point.Y = PointYFromPosition(pos);
            g.DrawLine(pen, point.X, point.Y, point.X, point.Y + height);

            // show "MOVE" cursor
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Handle drag and drop of text parts in the editor.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleDragDrop(object s, DragEventArgs e)
        {
            // convert cursor position to text position
            var point = PointToClient(new Point(e.X, e.Y));
            int pos = CharPositionFromPoint(point.X, point.Y);
            
            // is dropping possible
            foreach (var selection in Selections)
            {
                if (selection.Start <= pos && pos < selection.End)
                    return;
                // adjust caret position if necessary
                if (pos > selection.End)
                    pos -= selection.End - selection.Start;
            }

            // cut the selected text to the clipboard,
            // move caret to the insert position and
            // insert cut text from clipboard
            Cut();
            CurrentPosition = pos;
            AnchorPosition = pos;
            Paste();
        }

        /// <summary>
        /// Handle key down events of the code editor component.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleKeyDown(object s, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // disable editing if Ctrl+<Key> is pressed
                case Keys.F: // find
                case Keys.R: // replace
                case Keys.S: // save/save all/save as
                case Keys.Space: // auto complete menu
                    if (e.Control)
                    {
                        e.SuppressKeyPress = true;
                        DisableEditing = true;
                    }
                    break;
                case Keys.Z:
                    if (e.Control)
                        FxLexer.Style(this, 0, TextLength);
                    break;
            }
        }

        /// <summary>
        /// Handle key up events of the code editor component.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleKeyUp(object s, KeyEventArgs e)
        {
            // only disable editing if Ctrl is pressed
            DisableEditing = false;
            
            switch (e.KeyCode)
            {
                case Keys.F:
                    if (e.Control)
                    {
                        // start text search
                        DisableEditing = true;
                        FindText.Clear();
                        FindText.Focus();
                    }
                    break;
                case Keys.R:
                    if (e.Control)
                    {
                        // select all indicator to allow text replacement
                        DisableEditing = true;
                        SelectIndicators(HighlightIndicatorIndex);
                    }
                    break;
                case Keys.Space:
                    if (e.Control)
                    {
                        // show auto complete menu
                        DisableEditing = true;
                        AutoCShow(CurrentPosition);
                    }
                    break;
            }
        }

        /// <summary>
        /// Do additional painting after the control has been drawn.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandlePainted(object s, EventArgs e)
        {
            var g = CreateGraphics();

            // draw indicator lines where code has been folded
            g.DrawLine(dashedPen, new Point(lineSize.Width, 0), new Point(lineSize.Width, Height));

            // for all visible lines
            for (int from = FirstVisibleLine, to = LastVisibleLine, i = from; i < to; i++)
            {
                // if lines are not folded, continue
                if (Lines[i].Expanded || !Lines[i].Visible)
                    continue;

                // number of whitespaces at the beginning and the end of the line
                var wsStart = Lines[i].Text.IndexOf(x => !char.IsWhiteSpace(x));
                var wsEnd = Lines[i].Text.EndsWith("\r\n") ? 2 : 1;

                // draw indicator line below the current line
                var x1 = PointXFromPosition(Lines[i].Position + Math.Max(0, wsStart));
                var x2 = PointXFromPosition(Lines[i].EndPosition - wsEnd);
                var y = PointYFromPosition(Lines[i].Position) + lineSize.Height - 1;
                g.DrawLine(grayPen, x1, y, x2, y);
            }
        }

        //Codes for the handling the Indention of the lines.
        //They are manually added here until they get officially added to the Scintilla control.
        #region CodeIndent Handlers
        const int SCI_SETLINEINDENTATION = 2126;
        const int SCI_GETLINEINDENTATION = 2127;
        private void SetIndent(int line, int indent)
            => DirectMessage(SCI_SETLINEINDENTATION, (IntPtr)line, new IntPtr(indent));
        private int GetIndent(int line)
            => (int)DirectMessage(SCI_GETLINEINDENTATION, (IntPtr)line, IntPtr.Zero);
        #endregion
    }
}
