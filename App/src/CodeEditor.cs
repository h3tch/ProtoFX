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

        public CodeEditor(string text)
        {
            FindText = new TextBox();
            FindText.Visible = false;
            FindText.Parent = this;

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

        public void UpdateLineNumbers()
        {
            // UPDATE LINE NUMBERS
            int nLines = Lines.Count.ToString().Length;
            var width = TextRenderer.MeasureText(new string('9', nLines), Font).Width;
            if (Margins[0].Width != width)
                Margins[0].Width = width;
        }
        
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

            // UPDATE LINE NUMBERS
            var nLines = editor.Lines.Count.ToString().Length-2;
            var width = TextRenderer.MeasureText(new string('9', nLines), editor.Font).Width;
            if (editor.Margins[0].Width != width)
                editor.Margins[0].Width = width;
        }

        private void HandleSelectionChanged(object sender, UpdateUIEventArgs e)
        {
            var editor = (CodeEditor)sender;
            if (e.Change == UpdateChange.Selection)
                // highlight all selected words
                Highlight(SelectedWordRanges(SelectedWords()));
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
                    FindText.Clear();
                    FindText.Focus();
                    break;
                case Keys.R:
                    editor.SelectAllIndicators();
                    break;
            }
        }

        private void SelectAllIndicators()
        {
            // get current caret position
            var cur = this.CurrentPosition;

            // get selected word ranges
            var ranges = SelectedWordRanges(SelectedWords());

            // select all word ranges
            this.ClearSelections();
            foreach (var range in ranges)
                this.AddSelection(range.Item1, range.Item2);

            // ClearSelections adds a default selection
            // at postion 0 which we need to remove
            this.DropSelection(0);

            // rotate through all selections until we arive at the original caret position
            for (int i = 0; (cur < CurrentPosition || AnchorPosition < cur) && i < ranges.Count; i++)
                this.RotateSelection();
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

        private List<Tuple<int, int>> SelectedWordRanges(Dictionary<string, bool> words)
        {
            var ranges = new List<Tuple<int, int>>();

            foreach (var word in words)
            {
                // Search the document
                this.TargetStart = 0;
                this.TargetEnd = this.TextLength;
                this.SearchFlags = SearchFlags.MatchCase |
                    (word.Value ? SearchFlags.WholeWord : SearchFlags.None);
                while (this.SearchInTarget(word.Key) != -1)
                {
                    ranges.Add(new Tuple<int, int>(this.TargetStart, this.TargetEnd));
                    // Search the remainder of the document
                    this.TargetStart = this.TargetEnd;
                    this.TargetEnd = this.TextLength;
                }
            }

            return ranges;
        }

        private void Highlight(List<Tuple<int, int>> ranges)
        {
            // Remove all uses of our indicator
            this.IndicatorCurrent = HighlightIndicatorIndex;
            this.IndicatorClearRange(0, this.TextLength);

            // Indicators 0-7 could be in use by a lexer
            // so we'll use indicator 8 to highlight words.
            int NUM = HighlightIndicatorIndex;
            this.IndicatorCurrent = NUM;

            // Update indicator appearance
            this.Indicators[NUM].Style = IndicatorStyle.StraightBox;
            this.Indicators[NUM].Under = true;
            this.Indicators[NUM].ForeColor = Color.Green;
            this.Indicators[NUM].OutlineAlpha = 50;
            this.Indicators[NUM].Alpha = 30;

            foreach (var range in ranges)
                this.IndicatorFillRange(range.Item1, range.Item2 - range.Item1);
        }

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
