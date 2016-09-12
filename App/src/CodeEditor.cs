using ScintillaNET;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    public partial class CodeEditor : Scintilla
    {
        #region FIELDS
        public bool EnableCodeHints = true;
        private static int HighlightIndicatorIndex = 8;
        public static int DebugIndicatorIndex { get; } = 9;
        private List<int[]>[] IndicatorRanges;
        private static string HiddenLines = $"{(char)177}";
        
        public new int FirstVisibleLine
        {
            get
            {
                int line = base.FirstVisibleLine;
                for (int i = 0; i < line && i < Lines.Count; i++)
                    if (!Lines[i].Visible)
                        line++;
                return line;
            }
        }

        public int LastVisibleLine
        {
            get
            {
                int i, line;
                for (i = FirstVisibleLine, line = i + LinesOnScreen; i < line && i < Lines.Count; i++)
                    if (!Lines[i].Visible)
                        line++;
                return line;
            }
        }
        #endregion
        
        /// <summary>
        /// Instantiate and initialize ScintillaNET based code editor for ProtoFX.
        /// </summary>
        /// <param name="text">[OPTIONAL] Initialize code editor with text.</param>
        public CodeEditor(string text = null)
        {
            InitializeFindAndReplace();
            InitializeHighlighting();
            InitializeSelection();
            InitializeAutoC();
            InitializeCodeFolding();
            InitializeLayout();
            InitializeEvents();
            
            // insert text
            Text = text ?? string.Empty;
            UpdateLineNumbers();
            UpdateCodeFolding(0, Lines.Count);
        }

        /// <summary>
        /// Initialize Scintilla code folding.
        /// </summary>
        private void InitializeCodeFolding()
        {
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

            AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
        }

        /// <summary>
        /// Initialize Scintilla editor layout.
        /// </summary>
        private void InitializeLayout()
        {
            // automatically adjust horizontal scroll bar
            ScrollWidthTracking = true;
            ScrollWidth = 5;

            // use tabs instead of spaces
            IndentWidth = 4;
            UseTabs = true;

            // UI style
            BorderStyle = BorderStyle.None;
            Dock = DockStyle.Fill;
            Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Location = new Point(0, 0);
            Margin = new Padding(0);
            TabIndex = 0;
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
