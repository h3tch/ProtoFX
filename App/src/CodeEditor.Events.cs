using ScintillaNET;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
    partial class CodeEditor
    {
        private bool DisableEditing = false;

        private void HandleInsertCheck(object sender, InsertCheckEventArgs e)
        {
            var editor = (CodeEditor)sender;

            // do not insert text when
            // the Ctrl key is pressed
            if (editor.DisableEditing)
                e.Text = "";

            // auto indent
            if ((e.Text.EndsWith("\r") || e.Text.EndsWith("\n")))
            {
                // get text of line above
                var text = editor.Lines[editor.LineFromPosition(editor.CurrentPosition)].Text;
                // insert indent of line above
                e.Text += Regex.Match(text, "^[ \\t]*").Value;
                // if line above ends with '{' insert indent
                if (Regex.IsMatch(text, "{\\s*$"))
                    e.Text += '\t';
            }
        }


        private void HandleCharAdded(object sender, CharAddedEventArgs e)
        {
            var editor = (CodeEditor)sender;

            // auto indent
            if (e.Char == '}')
            {
                int curLine = editor.LineFromPosition(editor.CurrentPosition);
                // Check whether the bracket is the only non
                // whitespace in the line. For cases like "if() { }".
                if (editor.Lines[curLine].Text.Trim() == "}")
                    SetIndent(editor, curLine, GetIndent(editor, curLine) - 4);
            }
        }

        //Codes for the handling the Indention of the lines.
        //They are manually added here until they get officially added to the Scintilla control.
        #region CodeIndent Handlers
        const int SCI_SETLINEINDENTATION = 2126;
        const int SCI_GETLINEINDENTATION = 2127;
        private void SetIndent(Scintilla scin, int line, int indent)
            => scin.DirectMessage(SCI_SETLINEINDENTATION, (IntPtr)line, new IntPtr(indent));
        private int GetIndent(Scintilla scin, int line)
            => (int)scin.DirectMessage(SCI_GETLINEINDENTATION, (IntPtr)line, IntPtr.Zero);
        #endregion

        private void HandleTextChanged(object sender, EventArgs e)
        {
            // get class references
            var editor = (CodeEditor)sender;
            var tab = (TabPage)editor.Parent;

            // add file changed indicator '*'
            if (tab != null && !tab.Text.EndsWith("*"))
                tab.Text = tab.Text + '*';

            // update line number margins
            editor.UpdateLineNumbers();
        }

        private void HandleUpdateUI(object sender, UpdateUIEventArgs e)
        {
            // get class references
            var editor = (CodeEditor)sender;

            // handle selection changed events
            if (e.Change == UpdateChange.Selection)
            {
                // get whole words from selections and get ranges from these words
                var ranges = editor.FindWordRanges(editor.GetWordsFromSelections());
                // highlight all selected words
                editor.ClearIndicators(HighlightIndicatorIndex);
                editor.AddIndicators(HighlightIndicatorIndex, ranges);
            }
        }

        private void HandleDragOver(object sender, DragEventArgs e)
        {
            var editor = (CodeEditor)sender;

            // convert cursor position to text position
            var point = editor.PointToClient(new Point(e.X, e.Y));
            int pos = editor.CharPositionFromPoint(point.X, point.Y);

            // refresh text control
            editor.Refresh();

            // if the mouse is over a selected area, dropping will not be possible
            if (editor.Selections.IndexOf(x => x.Start <= pos && pos < x.End) >= 0)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // draw line at cursor position in text
            var g = editor.CreateGraphics();
            var pen = new Pen(Color.Black, 2);
            var height = TextRenderer.MeasureText("0", editor.Font).Height;
            point.X = editor.PointXFromPosition(pos);
            point.Y = editor.PointYFromPosition(pos);
            g.DrawLine(pen, point.X, point.Y, point.X, point.Y + height);

            // show "MOVE" cursor
            e.Effect = DragDropEffects.Move;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            var editor = (CodeEditor)sender;

            // convert cursor position to text position
            var point = editor.PointToClient(new Point(e.X, e.Y));
            int pos = editor.CharPositionFromPoint(point.X, point.Y);
            
            // is dropping possible
            foreach (var selection in editor.Selections)
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
            editor.Cut();
            editor.CurrentPosition = pos;
            editor.AnchorPosition = pos;
            editor.Paste();
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                return;

            switch (e.KeyCode)
            {
                case Keys.F:
                case Keys.R:
                case Keys.S:
                    e.SuppressKeyPress = true;
                    DisableEditing = true;
                    break;
            }
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            // only disable editin if Ctrl is pressed
            DisableEditing = e.Control;

            if (!e.Control)
                return;

            var editor = (CodeEditor)sender;
            switch (e.KeyCode)
            {
                case Keys.F:
                    // start text search
                    editor.FindText.Clear();
                    editor.FindText.Focus();
                    break;
                case Keys.R:
                    // select all indicator to allow text replacement
                    editor.SelectIndicators(HighlightIndicatorIndex);
                    break;
            }
        }
    }
}
