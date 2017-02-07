using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ScintillaNET
{
    public partial class CodeEditor
    {
        #region FIELDS

        private bool DisableEditing = false;
        private Pen FoldingPen;
        private int LineHeight;
        private Point LastMouseMovePosition;
        private Timer HoverTimer = new Timer();
        public static int DefaultEdgeColumn = 80;

        #endregion

        #region ADDITIONAL EVENTS

        [Category("Behavior"), Description("Only occurs when the MouseMove event is raised and the mouse position changed.")]
        public event MouseEventHandler CustomMouseMove;
        [Category("Behavior"), Description("Occurs when CallTipCancel is called.")]
        public event EventHandler CustomMouseHover;
        [Category("Behavior"), Description("Occurs when the mouse scrolls the editor.")]
        public event EventHandler MouseScroll;

        #endregion

        #region CONSTRUCTION

        /// <summary>
        /// Initialize class events.
        /// </summary>
        private void InitializeEvents()
        {
            TextChanged += HandleTextChanged;
            UpdateUI += HandleUpdateUI;

            // enable drag & drop
            AllowDrop = true;
            DragOver += HandleDragOver;
            DragDrop += HandleDragDrop;

            // search & replace
            KeyUp += HandleKeyUp;
            KeyDown += HandleKeyDown;
            InsertCheck += HandleInsertCheck;
            CharAdded += HandleCharAdded;
            Painted += HandlePainted;

            // handle some events internally to support custom events
            MouseMove += HandleMouseMove;
            MouseLeave += HandleMouseLeave;

            // initialize hover timer
            HoverTimer.Interval = 100;
            HoverTimer.Tick += HoverTimerEvent;

            //// create default pens
            FoldingPen = new Pen(Theme.ForeColor);

            // measure default line size
            EdgeColumn = DefaultEdgeColumn;
            EdgeMode = EdgeMode.Line;
            EdgeColor = Theme.WorkspaceHighlight;
            LineHeight = TextRenderer.MeasureText("/", Font).Height;
        }

        #endregion

        #region TEXT CHANGE EVENTS

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
            if (watchChanges && (!tab?.Text.EndsWith("*") ?? false))
                tab.Text = tab.Text + '*';

            // update line number margins
            UpdateLineNumbers();

            // update code folding
            UpdateCodeFolding(FirstVisibleLine, LastVisibleLine);
        }

        #endregion

        #region UI UPDATE EVENTS

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
                    // call mouse scroll events
                    MouseScroll?.Invoke(this, e);
                    // update code folding
                    UpdateCodeFolding(FirstVisibleLine, LastVisibleLine);
                    break;
            }
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
        /// Handle mouse move event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            // only call custom mouse move events
            // if the mouse position changes
            if (LastMouseMovePosition == Cursor.Position)
                return;

            // call custom mouse move event
            LastMouseMovePosition = Cursor.Position;
            CustomMouseMove?.Invoke(this, e);

            // restart hover timer
            HoverTimer.Stop();
            HoverTimer.Start();
        }

        /// <summary>
        /// On mouse leave restart the hover timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMouseLeave(object sender, EventArgs e) => HoverTimer.Stop();

        /// <summary>
        /// Handle mouse hover events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoverTimerEvent(object sender, EventArgs e)
        {
            // stop hover timer
            HoverTimer.Stop();

            // call custom hover events
            CustomMouseHover?.Invoke(this, e);
        }

        #endregion

        #region RENDER EVENTS

        /// <summary>
        /// Do additional painting after the control has been drawn.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandlePainted(object s, EventArgs e)
        {
            var g = CreateGraphics();
            
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
                var y = PointYFromPosition(Lines[i].Position) + LineHeight - 1;
                g.DrawLine(FoldingPen, x1, y, x2, y);
            }
        }

        #endregion

        #region FILE EVENTS

        /// <summary>
        /// Watch the linked file and handle events.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void HandleFileEvent(object s, FileSystemEventArgs e)
        {
            // only handle change events
            switch (e.ChangeType)
            {
                /// HANDLE EXTERNAL EDITS
                case WatcherChangeTypes.Changed:
                    // update the text
                    Invoke(new Action(() => {
                        // if the file no longer exists
                        if (!File.Exists(Filename))
                        {
                            Filename = null;
                            return;
                        }

                        // load file
                        var newText = File.ReadAllText(Filename);
                        if (newText == Text)
                            return;

                        // file was edited externally
                        // ask the user whether he/she wants to reload it
                        var rs = MessageBox.Show(
                            $"The file '{e.Name}' was edited outside the program.\nShould it be reloaded?",
                            "File Edited", MessageBoxButtons.YesNo);
                        if (rs != DialogResult.Yes)
                            return;

                        // change text
                        PauseWatchChanges();
                        ClearAll();
                        Text = newText;
                        ResumeWatchChanges();
                    }));
                    break;

                /// HANDLE RENAMING OF THE FILE
                case WatcherChangeTypes.Renamed:
                    // if the file no longer exists
                    if (!File.Exists(e.FullPath))
                        return;

                    // get class references
                    var tab = Parent as TabPage;
                    var name = tab.Text;
                    if (name.EndsWith("*"))
                        name = name.Substring(0, name.Length - 1);

                    // file was edited externally
                    // ask the user whether he/she wants to reload it
                    var result = MessageBox.Show(
                        $"The file name '{name}' change to '{e.Name}'.\nShould the file be reloaded?",
                        "File Edited", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                        return;
                    
                    // update the text
                    Invoke(new Action(() => {
                        // change text
                        ClearAll();
                        Text = File.ReadAllText(e.FullPath);
                        tab.Text = e.Name;
                    }));

                    // update the file watcher
                    Filename = e.FullPath;
                    break;
            }
        }

        #endregion

        #region CodeIndent Handlers

        // Codes for the handling the Indention of the lines.
        // They are manually added here until they get
        // officially added to the Scintilla control.

        const int SCI_SETLINEINDENTATION = 2126;
        const int SCI_GETLINEINDENTATION = 2127;
        private void SetIndent(int line, int indent)
            => DirectMessage(SCI_SETLINEINDENTATION, (IntPtr)line, new IntPtr(indent));
        private int GetIndent(int line)
            => (int)DirectMessage(SCI_GETLINEINDENTATION, (IntPtr)line, IntPtr.Zero);

        #endregion
    }
}
