using ScintillaNET;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    class CodeEditor : Scintilla
    {
        public CodeEditor(string text)
        {
            // tabSourcePageText
            this.BorderStyle = BorderStyle.None;
            this.ConfigurationManager.CustomLocation = "../conf/syntax.xml";
            this.ConfigurationManager.Language = "cpp";
            this.Dock = DockStyle.Fill;
            this.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Location = new Point(0, 0);
            this.Margin = new Padding(0);
            this.TabIndex = 0;
            this.Text = text;
            this.TextChanged += new EventHandler(HandleTextChanged);
            this.SelectionChanged += new EventHandler(HandleSelectionChanged);
            // enable drag&drop
            this.AllowDrop = true;
            this.DragOver += new DragEventHandler(HandleDragOver);
            this.DragDrop += new DragEventHandler(HandleDragDrop);
            // enable code folding
            this.Folding.IsEnabled = true;
            this.Margins[2].Type = MarginType.Symbol;
            this.Margins[2].Width = 20;
            // display line numbers
            UpdateLineNumbers();
        }

        public void UpdateLineNumbers()
        {
            // UPDATE LINE NUMBERS
            int nLines = Lines.Count.ToString().Length;
            var width = TextRenderer.MeasureText(new string('9', nLines), Font).Width;
            if (Margins[0].Width != width)
                Margins[0].Width = width;
        }

        private void HandleTextChanged(object sender, EventArgs e)
        {
            var tabSourceText = (CodeEditor)sender;
            var tabSourcePage = (TabPage)tabSourceText.Parent;
            if (!tabSourcePage.Text.EndsWith("*"))
                tabSourcePage.Text = tabSourcePage.Text + '*';

            // UPDATE LINE NUMBERS
            var nLines = tabSourceText.Lines.Count.ToString().Length;
            var width = TextRenderer.MeasureText(new string('9', nLines), tabSourceText.Font).Width;
            if (tabSourceText.Margins[0].Width != width)
                tabSourceText.Margins[0].Width = width;
        }

        private void HandleSelectionChanged(object sender, EventArgs e)
        {
            var tabSourceText = (CodeEditor)sender;
            // DEACTIVATE MULTILINE SELECTION BECAUSE MULTILINE EDIT IS NOT SUPPORTED
            if (tabSourceText.Selection.IsRectangle)
                tabSourceText.Selection.Range = new Range(
                    tabSourceText.Selection.Start,
                    tabSourceText.Selection.End,
                    tabSourceText);
        }

        private void HandleDragOver(object sender, DragEventArgs e)
        {
            var tabSourceText = (CodeEditor)sender;

            // convert cursor position to text position
            Point point = new Point(e.X, e.Y);
            point = tabSourceText.PointToClient(point);
            int pos = tabSourceText.PositionFromPoint(point.X, point.Y);

            // refresh text control
            tabSourceText.Refresh();

            // is draging possible
            if (tabSourceText.GetRange(tabSourceText.Caret.Position, tabSourceText.Caret.Anchor)
                .IntersectsWith(tabSourceText.GetRange(pos)))
            {
                // if not, show "NO" cursor
                e.Effect = DragDropEffects.None;
                return;
            }

            // draw line at cursor position in text
            var g = tabSourceText.CreateGraphics();
            var pen = new Pen(Color.Black, 2);
            var height = TextRenderer.MeasureText("0", tabSourceText.Font).Height;
            point.X = tabSourceText.PointXFromPosition(pos);
            point.Y = tabSourceText.PointYFromPosition(pos);
            g.DrawLine(pen, point.X, point.Y, point.X, point.Y + height);

            // show "MOVE" cursor
            e.Effect = DragDropEffects.Move;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            var tabSourceText = (CodeEditor)sender;

            // convert cursor position to text position
            Point point = new Point(e.X, e.Y);
            point = tabSourceText.PointToClient(point);
            int pos = tabSourceText.PositionFromPoint(point.X, point.Y);

            // is dropping possible
            if (tabSourceText.GetRange(tabSourceText.Caret.Position, tabSourceText.Caret.Anchor)
                .IntersectsWith(tabSourceText.GetRange(pos)))
                return;

            // adjust caret position if necessary
            if (pos > tabSourceText.Caret.Position)
                pos -= Math.Abs(tabSourceText.Caret.Anchor - tabSourceText.Caret.Position);
            // cut the selected text to the clipboard
            tabSourceText.Clipboard.Cut();
            // move caret to the insert position
            tabSourceText.Caret.Position = pos;
            tabSourceText.Caret.Anchor = pos;
            // insert cut text from clipboard
            tabSourceText.Clipboard.Paste();
        }
    }
}
