using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    partial class CodeEditor : Scintilla
    {
        public bool EnableCodeHints = true;
        private static int HighlightIndicatorIndex = 8;
        public static int DebugIndicatorIndex { get; } = 9;
        private List<int[]>[] IndicatorRanges;

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

        #region KEYWORDS
        string[] keywords = new[] {
            // PROTOGL KEYWORDS
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
            "image1D        " +
            "image1DArray   " +
            "image2D        " +
            "image2DArray   " +
            "image3D        " +
            "image3DArray   " +
            "imageCube      " +
            "iimage1D       " +
            "iimage1DArray  " +
            "iimage2D       " +
            "iimage2DArray  " +
            "iimage3D       " +
            "iimage3DArray  " +
            "iimageCube     " +
            "uimage1D       " +
            "uimage1DArray  " +
            "uimage2D       " +
            "uimage2DArray  " +
            "uimage3D       " +
            "uimage3DArray  " +
            "uimageCube     " +
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
            "while          " ,
            // PROTOGL ATTRIBUTE KEYWORDS
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
            "gl_GlobalInvocationID " +
            "gl_LocalInvocationID " +
            "gl_WorkGroupID " +
            "gpuformat " +
            "height    " +
            "img       " +
            "length    " +
            "local_size_x " +
            "local_size_y " +
            "local_size_z " +
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
            "txt       " +
            "type      " +
            "usage     " +
            "vert      " +
            "vertout   " +
            "width     " +
            "wrap      " +
            "xml       " ,
            // GLSL functions
            "imageAtomicAdd " +
            "imageAtomicAnd " +
            "imageAtomicCompSwap " +
            "imageAtomicExchange " +
            "imageAtomicMax " +
            "imageAtomicMin " +
            "imageAtomicOr  " +
            "imageAtomicXor " +
            "imageLoad      " +
            "imageSamples   " +
            "imageSize      " +
            "imageStore     " ,
        };
        #endregion
    }
}
