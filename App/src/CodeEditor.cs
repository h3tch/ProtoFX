using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    class CodeEditor : Scintilla
    {
        private static int HighlightIndicatorIndex = 8;
        private TextBox FindText;
        private bool KeyControl = false;
        private List<Tuple<int, int>>[] IndicatorRanges;

        public CodeEditor(string text)
        {
            IndicatorRanges = new List<Tuple<int, int>>[Indicators.Count];
            for (int i = 0; i < Indicators.Count; i++)
                IndicatorRanges[i] = new List<Tuple<int, int>>();

            // find text box
            FindText = new TextBox();
            FindText.Parent = this;
            FindText.MinimumSize = new Size(0,0);
            FindText.MaximumSize = new Size(1, 1);
            FindText.SetBounds(0, 0, 1, 1);
            FindText.TextChanged += new EventHandler(HandleFindTextChanged);
            FindText.KeyUp += new KeyEventHandler(HandleFindKeyUp);
            FindText.GotFocus += new EventHandler(HandleFindGotFocus);
            FindText.LostFocus += new EventHandler(HandleFindLostFocus);

            // setup code coloring
            this.StyleResetDefault();
            this.Styles[Style.Default].Font = "Consolas";
            this.Styles[Style.Default].Size = 10;
            this.StyleClearAll();
            this.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            this.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0x7F9F00);
            this.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0x7F9F00);
            this.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128);
            this.Styles[Style.Cpp.Operator].ForeColor = Color.FromArgb(0x3050CC);
            this.Styles[Style.Cpp.Preprocessor].ForeColor = Color.FromArgb(0xE47426);
            this.Styles[Style.Cpp.Number].ForeColor = Color.FromArgb(0x108030);
            this.Styles[Style.Cpp.String].ForeColor = Color.Maroon;
            this.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21);
            this.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            this.Styles[Style.Cpp.Word2].ForeColor = Color.FromArgb(30, 120, 255);
            this.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21);
            this.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            this.Lexer = Lexer.Cpp;
            this.SetKeywords(0, keywords0);
            this.SetKeywords(1, keywords1);

            // setup code folding
            this.SetProperty("fold", "1");
            this.SetProperty("fold.compact", "1");
            this.Margins[2].Type = MarginType.Symbol;
            this.Margins[2].Mask = Marker.MaskFolders;
            this.Margins[2].Sensitive = true;
            this.Margins[2].Width = 20;
            this.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            this.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            this.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            this.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            this.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            this.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            this.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            for (int i = (int)Marker.FolderEnd; i <= (int)Marker.FolderOpen; i++)
            {
                this.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                this.Markers[i].SetBackColor(SystemColors.ControlDark);
            }
            this.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            // setup multi line selection
            this.MultipleSelection = true;
            this.MouseSelectionRectangularSwitch = true;
            this.AdditionalSelectionTyping = true;
            this.VirtualSpaceOptions = VirtualSpace.RectangularSelection;

            // enable drag&drop
            this.AllowDrop = true;
            this.DragOver += new DragEventHandler(HandleDragOver);
            this.DragDrop += new DragEventHandler(HandleDragDrop);

            // search & replace
            this.KeyUp += new KeyEventHandler(HandleKeyUp);
            this.KeyDown += new KeyEventHandler(HandleKeyDown);
            this.InsertCheck += new EventHandler<InsertCheckEventArgs>(HandleInsertCheck);

            // other settings
            this.BorderStyle = BorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Location = new Point(0, 0);
            this.Margin = new Padding(0);
            this.TabIndex = 0;
            this.Text = text;
            this.TextChanged += new EventHandler(HandleTextChanged);
            this.UpdateUI += new EventHandler<UpdateUIEventArgs>(HandleSelectionChanged);
            UpdateLineNumbers();
        }

        #region EVENT HANDLERS
        private void HandleInsertCheck(object sender, InsertCheckEventArgs e)
        {
            if (KeyControl)
                e.Text = "";
        }

        private void HandleTextChanged(object sender, EventArgs e)
        {
            var editor = (CodeEditor)sender;

            var tabSourcePage = (TabPage)editor.Parent;
            if (!tabSourcePage.Text.EndsWith("*"))
                tabSourcePage.Text = tabSourcePage.Text + '*';
            
            editor.UpdateLineNumbers();
        }

        private void HandleSelectionChanged(object sender, UpdateUIEventArgs e)
        {
            var editor = (CodeEditor)sender;
            if (e.Change == UpdateChange.Selection)
            {
                var ranges = editor.SelectedWordsRanges(editor.SelectedWords());
                // highlight all selected words
                editor.ClearIndicators(HighlightIndicatorIndex);
                editor.AddIndicators(HighlightIndicatorIndex, ranges);
            }
        }

        private void HandleDragOver(object sender, DragEventArgs e)
        {
            var editor = (CodeEditor)sender;

            // convert cursor position to text position
            Point point = new Point(e.X, e.Y);
            point = editor.PointToClient(point);
            int pos = editor.CharPositionFromPoint(point.X, point.Y);

            // refresh text control
            editor.Refresh();

            // is draging possible
            foreach (var selection in editor.Selections)
            {
                if (selection.Start <= pos && pos < selection.End)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
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
            Point point = new Point(e.X, e.Y);
            point = editor.PointToClient(point);
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
            
            // cut the selected text to the clipboard
            editor.Cut();
            // move caret to the insert position
            editor.CurrentPosition = pos;
            editor.AnchorPosition = pos;
            // insert cut text from clipboard
            editor.Paste();
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            KeyControl = e.Control;

            if (!KeyControl)
                return;

            switch (e.KeyCode)
            {
                case Keys.F:
                case Keys.R:
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            var editor = (CodeEditor)sender;
            KeyControl = e.Control;

            if (!KeyControl)
                return;

            switch (e.KeyCode)
            {
                case Keys.F:
                    // start text search
                    FindText.Clear();
                    FindText.Focus();
                    break;
                case Keys.R:
                    // select all indicator to allow text replacement
                    editor.SelectIndicators(HighlightIndicatorIndex);
                    break;
            }
        }
        #endregion

        #region FIND EVENT HANDLERS
        private void HandleFindTextChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            var editor = (CodeEditor)textbox.Parent;
            editor.ClearIndicators(HighlightIndicatorIndex);
            if (textbox.Text.Length == 0)
                return;

            // select all ranges matching the search term
            var ranges = editor.SelectedWordRanges(textbox.Text, false);

            // if nothing can be selected, reduce the search term until somethin can
            while (ranges.Count() == 0 && textbox.Text.Length > 1)
            {
                textbox.Text = textbox.Text.Substring(0, textbox.Text.Length - 1);
                ranges = editor.SelectedWordRanges(textbox.Text, false);
            }
            textbox.Select(textbox.Text.Length, 0);

            // highlight all selected words
            editor.AddIndicators(HighlightIndicatorIndex, ranges);

            // rotate through all ranges until we arive
            // at the one closest to the caret position
            foreach (var range in ranges)
            {
                if (CurrentPosition <= range.Item1)
                {
                    CurrentPosition = range.Item1;
                    AnchorPosition = range.Item2;
                    break;
                }
            }
        }

        private void HandleFindGotFocus(object sender, EventArgs e)
        {
            // on focus color the caret red
            var textbox = (TextBox)sender;
            var editor = (CodeEditor)textbox.Parent;
            editor.CaretWidth = 2;
            editor.CaretForeColor = Color.Red;
        }

        private void HandleFindLostFocus(object sender, EventArgs e)
        {
            // on lost focus color the caret black
            var textbox = (TextBox)sender;
            var editor = (CodeEditor)textbox.Parent;
            editor.CaretWidth = 1;
            editor.CaretForeColor = Color.Black;
        }

        private void HandleFindKeyUp(object sender, KeyEventArgs e)
        {
            var textbox = (TextBox)sender;
            var editor = (CodeEditor)textbox.Parent;

            switch (e.KeyCode)
            {
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
        #endregion

        #region UTIL
        public void UpdateLineNumbers()
        {
            // UPDATE LINE NUMBERS
            int nLines = Lines.Count.ToString().Length;
            var width = TextRenderer.MeasureText(new string('9', nLines), Font).Width;
            if (Margins[0].Width != width)
                Margins[0].Width = width;
        }

        private void SelectIndicators(int index)
        {
            SelectRanges(IndicatorRanges[index]);
        }

        private void SelectRanges(IEnumerable<Tuple<int, int>> ranges)
        {
            // get current caret position
            var cur = CurrentPosition;

            // get selected word ranges
            var count = ranges.Count();

            // select all word ranges
            ClearSelections();
            foreach (var range in ranges)
                AddSelection(range.Item1, range.Item2);

            // ClearSelections adds a default selection
            // at postion 0 which we need to remove
            DropSelection(0);

            // rotate through all selections until we arive at the original caret position
            for (int i = 0; (cur < CurrentPosition || AnchorPosition < cur) && i < count; i++)
                RotateSelection();
        }

        private Dictionary<string, bool> SelectedWords()
        {
            var words = new Dictionary<string, bool>();

            // highlight all selected text
            foreach (var selection in Selections)
            {
                // text length of selection
                var len = selection.End - selection.Start;
                // if not a selected word
                var word = len == 0 ?
                    // select word at caret position
                    GetWordFromPosition(selection.Caret) :
                    // get selected text
                    GetTextRange(selection.Start, len);

                // do not add empty strings 
                // (GetWordFromPosition can return those)
                if (word.Length == 0)
                    continue;

                // was selection a whole word
                var isWholeWord = len == 0 || IsRangeWord(selection.Start, selection.End);

                // we cannot add the same key twice
                if (words.ContainsKey(word))
                    words[word] |= isWholeWord;
                else
                    words.Add(word, isWholeWord);
            }

            return words;
        }

        private IEnumerable<Tuple<int, int>> SelectedWordsRanges(Dictionary<string, bool> words)
        {
            foreach (var word in words)
                foreach (var tupel in SelectedWordRanges(word.Key, word.Value))
                    yield return tupel;
        }

        private IEnumerable<Tuple<int, int>> SelectedWordRanges(string word, bool wholeWord)
        {
            // Search the document
            TargetStart = 0;
            TargetEnd = TextLength;
            SearchFlags = SearchFlags.MatchCase |
                (wholeWord ? SearchFlags.WholeWord : SearchFlags.None);
            while (SearchInTarget(word) != -1)
            {
                yield return new Tuple<int, int>(TargetStart, TargetEnd);
                // Search the remainder of the document
                TargetStart = TargetEnd;
                TargetEnd = TextLength;
            }
        }
        #endregion

        #region HIGHLIGHT TEXT
        private void ClearIndicators(int index)
        {
            // Remove all uses of our indicator
            IndicatorCurrent = index;
            IndicatorClearRange(0, TextLength);
            IndicatorRanges[index].Clear();
        }

        private void AddIndicators(int index, IEnumerable<Tuple<int, int>> ranges)
        {
            // set active indicator
            IndicatorCurrent = index;

            // Update indicator appearance
            Indicators[index].Style = IndicatorStyle.StraightBox;
            Indicators[index].Under = true;
            Indicators[index].ForeColor = Color.Crimson;
            Indicators[index].OutlineAlpha = 60;
            Indicators[index].Alpha = 40;

            foreach (var range in ranges)
            {
                // add indicator range
                IndicatorRanges[index].Add(range);
                IndicatorFillRange(range.Item1, range.Item2 - range.Item1);
            }
        }
        #endregion

        #region KEYWORDS
        string keywords0 =
            "buffer         " +
            "break          " +
            "const          " +
            "continue       " +
            "csharp         " +
            "discard        " +
            "else           " +
            "float          " +
            "for            " +
            "fragoutput     " +
            "geomoutput     " +
            "if             " +
            "image          " +
            "in             " +
            "instance       " +
            "int            " +
            "ivec2          " +
            "ivec3          " +
            "ivec4          " +
            "layout         " +
            "mat2           " +
            "mat2x3         " +
            "mat2x4         " +
            "mat3           " +
            "mat3x2         " +
            "mat3x4         " +
            "mat4           " +
            "mat4x2         " +
            "mat4x3         " +
            "out            " +
            "pass           " +
            "return         " +
            "sampler        " +
            "sampler1D      " +
            "sampler1DArray " +
            "sampler2D      " +
            "sampler2DArray " +
            "sampler3D      " +
            "sampler3DArray " +
            "samplerCube    " +
            "scene          " +
            "shader         " +
            "struct         " +
            "tech           " +
            "text           " +
            "texture        " +
            "uint           " +
            "uniform        " +
            "uvec2          " +
            "uvec3          " +
            "uvec4          " +
            "vec2           " +
            "vec3           " +
            "vec4           " +
            "vertinput      " +
            "vertoutput     " +
            "void           " +
            "while          " ;


        string keywords1 =
            "attr      " +
            "binding   " +
            "buff      " +
            "class     " +
            "color     " +
            "comp      " +
            "compute   " +
            "depth     " +
            "draw      " +
            "eval      " +
            "exec      " +
            "file      " +
            "format    " +
            "frag      " +
            "fragout   " +
            "geom      " +
            "geomout   " +
            "gpuformat " +
            "height    " +
            "img       " +
            "length    " +
            "location  " +
            "magfilter " +
            "minfilter " +
            "mipmaps   " +
            "name      " +
            "option    " +
            "samp      " +
            "show      " +
            "size      " +
            "stencil   " +
            "tess      " +
            "tex       " +
            "type      " +
            "usage     " +
            "vert      " +
            "vertout   " +
            "width     " +
            "wrap      " +
            "xml       " ;
        #endregion
        }
}
