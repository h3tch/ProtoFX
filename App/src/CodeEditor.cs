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
        private static Trie<string> KeywordTrie;
        private static Dictionary<string, string> Hint;
        private static int BLOCK;
        private static int ANNO;
        private static int CMD;
        private static int ARG;
        private static int FUNC;
        private static int TYPE;
        private static int SPEC;
        private static int QUAL;
        private static int VAR;
        private static int BRANCH;
        #endregion

        /// <summary>
        /// Constructor for static fields.
        /// </summary>
        static CodeEditor()
        {
            // process keyword definition file
            var lines = Regex.Split(Properties.Resources.keywords, "\r\n", RegexOptions.None);
            var keys = lines.TakeWhile(x => x.StartsWith("//"));
            int start = keys.Count();
            int end = start;
            foreach (var line in lines.Skip(start))
            {
                if (line[0] == ' ')
                    lines[end - 1] += $"\n{line.Substring(1)}";
                else
                    lines[end++] = line;
            }

            // set keyword definitions
            var defs = keys.Select(x => x.Split(' ').Where(y => y.Length > 0).ToArray());
            var Defs = new Dictionary<string, FXLexer.KeyDef>(defs.Count());
            foreach (var def in defs)
            {
                int id = int.Parse(def[1]);
                Color color = ColorTranslator.FromHtml(def[3]);
                Defs.Add(def[2], new FXLexer.KeyDef { Id = id, Prefix = $"{id}", Color = color });
            }

            // get keyword IDs
            BLOCK = Defs["block"].Id;
            ANNO = Defs["annotation"].Id;
            CMD = Defs["command"].Id;
            ARG = Defs["argument"].Id;
            FUNC = Defs["function"].Id;
            TYPE = Defs["type"].Id;
            SPEC = Defs["specifications"].Id;
            QUAL = Defs["qualifier"].Id;
            VAR = Defs["variable"].Id;
            BRANCH = Defs["branching"].Id;

            // set keyword list
            var list = lines.Skip(start).Take(end - start)
                .Select(k => k.Substring(0, k.IndexOfOrLength('|')))
                .ToArray();
            KeywordTrie = new Trie<string>(list, list);

            // set hint list
            Hint = lines.Skip(start).Take(end - start).ToDictionary(
                k => k.Substring(0, k.IndexOfOrLength('|')),
                v => v.Substring(v.IndexOfOrLength('|')));

            // create lexer
            FxLexer = new FXLexer(Properties.Resources.keywords, Defs);
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
            
            InitializeFindAndReplace();
            InitializeHighlighting();
            InitializeSelection();
            InitializeEvents();
            InitializeAutoC();

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

            // setup layout
            IndentWidth = 4;
            UseTabs = true;
            BorderStyle = BorderStyle.None;
            Dock = DockStyle.Fill;
            Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Location = new Point(0, 0);
            Margin = new Padding(0);
            TabIndex = 0;
            TextChanged += new EventHandler(HandleTextChanged);
            UpdateUI += new EventHandler<UpdateUIEventArgs>(HandleUpdateUI);
            
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
