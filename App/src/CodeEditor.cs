using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
    partial class CodeEditor : Scintilla
    {
        #region FIELDS
        public bool EnableCodeHints = true;
        private static int HighlightIndicatorIndex = 8;
        public static int DebugIndicatorIndex { get; } = 9;
        private List<int[]>[] IndicatorRanges;
        private static string[] KeywordDef;
        private static Dictionary<string, string> Hint;
        #endregion

        /// <summary>
        /// Constructor for static fields.
        /// </summary>
        static CodeEditor()
        {
            // process keyword definition file
            var lines = Regex.Split(Properties.Resources.keywords, "\r\n", RegexOptions.None);
            int j = 0;
            foreach (var line in lines.Skip(1))
            {
                if (line[0] == ' ')
                    lines[j] = $"{lines[j]}\n{line.Substring(1)}";
                else
                    lines[++j] = line;
            }

            // set keyword list
            KeywordDef = lines.Take(j + 1).ToArray();
            // set hint list
            Hint = KeywordDef.ToDictionary(
                k => k.Substring(0, (int)Math.Min((uint)k.Length, (uint)k.IndexOf('|'))),
                v => v.Substring((int)Math.Min((uint)v.Length, (uint)v.IndexOf('|'))));

            // create lexer
            lexer = new FXLexer(KeywordDef);
        }

        /// <summary>
        /// Instantiate and initialize ScintillaNET based code editor for ProtoGL.
        /// </summary>
        /// <param name="text">[OPTIONAL] Initialize code editor with text.</param>
        public CodeEditor(string text = null)
        {
            // instantiate fields
            IndicatorRanges = new List<int[]>[Indicators.Count];
            for (int i = 0; i < Indicators.Count; i++)
                IndicatorRanges[i] = new List<int[]>();

            // find text box
            FindText = new TextBox();
            FindText.Parent = this;
            FindText.MinimumSize = new Size(0, 0);
            FindText.MaximumSize = new Size(1, 1);
            FindText.SetBounds(0, 0, 1, 1);
            FindText.TextChanged += new EventHandler(HandleFindTextChanged);
            FindText.KeyUp += new KeyEventHandler(HandleFindKeyUp);
            FindText.GotFocus += new EventHandler(HandleFindGotFocus);
            FindText.LostFocus += new EventHandler(HandleFindLostFocus);

            // setup code coloring
            InitializeHighlighting();

            // setup code folding
            SetProperty("fold", "1");
            SetProperty("fold.compact", "1");
            Margins[2].Type = MarginType.Symbol;
            Margins[2].Mask = Marker.MaskFolders;
            Margins[2].Sensitive = true;
            Margins[2].Width = 20;
            Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
            {
                Markers[i].SetForeColor(SystemColors.ControlLightLight);
                Markers[i].SetBackColor(SystemColors.ControlDark);
            }
            AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            // setup indicator colors
            Indicators[HighlightIndicatorIndex].Style = IndicatorStyle.StraightBox;
            Indicators[HighlightIndicatorIndex].Under = true;
            Indicators[HighlightIndicatorIndex].ForeColor = Color.Crimson;
            Indicators[HighlightIndicatorIndex].OutlineAlpha = 60;
            Indicators[HighlightIndicatorIndex].Alpha = 40;
            Indicators[DebugIndicatorIndex].ForeColor = Color.Red;
            Indicators[DebugIndicatorIndex].Style = IndicatorStyle.Squiggle;

            // setup multi line selection
            MultipleSelection = true;
            MouseSelectionRectangularSwitch = true;
            AdditionalSelectionTyping = true;
            VirtualSpaceOptions = VirtualSpace.RectangularSelection;

            // enable drag & drop
            AllowDrop = true;
            DragOver += new DragEventHandler(HandleDragOver);
            DragDrop += new DragEventHandler(HandleDragDrop);

            // search & replace
            KeyUp += new KeyEventHandler(HandleKeyUp);
            KeyDown += new KeyEventHandler(HandleKeyDown);
            InsertCheck += new EventHandler<InsertCheckEventArgs>(HandleInsertCheck);
            CharAdded += new EventHandler<CharAddedEventArgs>(HandleCharAdded);

            // setup layout
            BorderStyle = BorderStyle.None;
            Dock = DockStyle.Fill;
            Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Location = new Point(0, 0);
            Margin = new Padding(0);
            TabIndex = 0;
            TextChanged += new EventHandler(HandleTextChanged);
            UpdateUI += new EventHandler<UpdateUIEventArgs>(HandleUpdateUI);

            // auto completion settings
            AutoCSeparator = '|';
            AutoCMaxHeight = 9;
            MouseMove += new MouseEventHandler(HandleMouseMove);

            // insert text
            Text = text != null ? text : "";
            UpdateLineNumbers();
        }

        /// <summary>
        /// Update line number of the text editor.
        /// </summary>
        private void UpdateLineNumbers()
        {
            // UPDATE LINE NUMBERS
            int nLines = Lines.Count.ToString().Length;
            var width = TextRenderer.MeasureText(new string('9', nLines), Font).Width;
            if (Margins[0].Width != width)
                Margins[0].Width = width;
        }
    }
}
