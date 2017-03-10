using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.FormWindowState;

namespace App
{
    public partial class App : Form
    {
        #region Properties

        /// <summary>
        /// The editor where the compiled code came from.
        /// </summary>
        public CodeEditor CompiledEditor;
        /// <summary>
        /// The location of the window in its normalized state. 
        /// </summary>
        private Point NormalLocation;
        /// <summary>
        /// The size of the window in its normalized state. 
        /// </summary>
        private Size NormalSize;
        /// <summary>
        /// Get the currently selected code tab.
        /// </summary>
        private TabPage SelectedTab => tabSource.SelectedTab;
        /// <summary>
        /// Get the currently selected editor.
        /// </summary>
        private CodeEditor SelectedEditor => (CodeEditor)tabSource.SelectedTab?.Controls[0];
        /// <summary>
        /// Is the window currently in maximized state.
        /// </summary>
        private bool IsMaximized => FormBorderStyle == FormBorderStyle.None;

        #region Window resize buttons

        const int HANDLE_SIZE = 10;
        const uint HTLEFT = 10u;
        const uint HTRIGHT = 11u;
        const uint HTTOP = 12u;
        const uint HTTOPLEFT = 13u;
        const uint HTTOPRIGHT = 14u;
        const uint HTBOTTOM = 15u;
        const uint HTBOTTOMLEFT = 16u;
        const uint HTBOTTOMRIGHT = 17u;

        private int TitleBarClickX, TitleBarClickY;

        private Rectangle[] ResizeBoxes = new[] {
            /* HTLEFT       */new Rectangle(0, HANDLE_SIZE, HANDLE_SIZE, 0),
            /* HTRIGHT      */new Rectangle(0, HANDLE_SIZE, HANDLE_SIZE, 0),
            /* HTTOP        */new Rectangle(HANDLE_SIZE, 0, 0, HANDLE_SIZE),
            /* HTTOPLEFT    */new Rectangle(0, 0, HANDLE_SIZE, HANDLE_SIZE),
            /* HTTOPRIGHT   */new Rectangle(0, 0, HANDLE_SIZE, HANDLE_SIZE),
            /* HTBOTTOM     */new Rectangle(HANDLE_SIZE, 0, 0, HANDLE_SIZE),
            /* HTBOTTOMLEFT */new Rectangle(0, 0, HANDLE_SIZE, HANDLE_SIZE),
            /* HTBOTTOMRIGHT*/new Rectangle(0, 0, HANDLE_SIZE, HANDLE_SIZE),
        };

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public App()
        {
            /// SET CULTURE TO ENGLISH STYLE

            CultureInfo.CurrentCulture = new CultureInfo("en");

            /// INITIALIZE FORM CONTROLS

            InitializeComponent();

            /// LOAD PREVIOUS WINDOW STATE

            // load settings
            var settings = LoadSettings();
            // apply settings to layout
            ApplyLayout(settings);
            // apply settings to theme
            ApplyTheme(settings);
        }

        #endregion

        #region Form Control

        /// <summary>
        /// On loading the app, load form settings and instantiate GLSL debugger.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void App_Load(object s, EventArgs e)
        {
            // select default item
            ConvertExtensions.str2type.Keys.ForEach(x => comboBufType.Items.Add(x));
            comboBufType.SelectedIndex = ConvertExtensions.str2type.Keys.IndexOf(x => x == "float");

            /// LINK PROPERTY VIEWER TO DEBUG SETTINGS
            
            debugProperty.SelectedObject = Glsl.Shader.Settings;
            debugProperty.CollapseAllGridItems();

            /// PROCESS COMMAND LINE ARGUMENTS

            ProcessArgs(Environment.GetCommandLineArgs());

            /// CLEAR OPENGL CONTROL

            glControl.AddEvents(output);
            DebugRender(false);
        }

        /// <summary>
        /// On form closing, save all source files, delete
        /// all OpenGL objects and save form state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            // check if there are any files with changes
            foreach (TabPage tab in tabSource.TabPages)
            {
                if (!CloseTab(tab))
                    e.Cancel = true;
            }

            // delete OpenGL objects
            glControl.ClearScene();

            // SAVE CURRENT WINDOW STATE

            // save to file
            XmlSerializer.Save(FormSettings.Create(this), Properties.Resources.WINDOW_SETTINGS_FILE);
        }
        
        /// <summary>
        /// Handle key events of the form.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void App_KeyUp(object s, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // Compile and run
                case Keys.F5:
                    ToolBtnRunDebug_Click(s, null);
                    break;
                // Compile and run
                case Keys.F6:
                    ToolBtnRunDebug_Click(toolBtnDbg, null);
                    break;
                // Debug stepping
                case Keys.F9:
                    DebugStepBreakpoint_Click();
                    break;
                case Keys.F10:
                    DebugStepOver_Click();
                    break;
                case Keys.F11:
                    DebugStepInto_Click();
                    break;
                // Save
                case Keys.S:
                    if (e.Control && e.Shift)
                        // Save all tabs
                        ToolBtnSaveAll_Click(s, null);
                    else if (e.Control)
                        // Save active tab
                        ToolBtnSave_Click(s, null);
                    else if (e.Alt)
                        // Save active tab as
                        ToolBtnSaveAs_Click(s, null);
                    break;
                // Open
                case Keys.O:
                    if (e.Control)
                        // Open tech files
                        ToolBtnOpen_Click(s, null);
                    break;
                // Fold all code
                case Keys.Left:
                    if (e.Alt)
                        SelectedEditor?.FoldAll(FoldAction.Contract);
                    break;
                // Expand all code
                case Keys.Right:
                    if (e.Alt)
                        SelectedEditor?.FoldAll(FoldAction.Expand);
                    break;
            }
        }

        /// <summary>
        /// After clicking the window close button, close the form.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void BtnWindowClose_Click(object s, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// After clicking the window minimize button, minimize the form.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void BtnWindowMinimize_Click(object s, EventArgs e)
        {
            WindowState = Minimized;
        }

        /// <summary>
        /// Maximize or normalize the window based on its current state.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void BtnWindowMaximize_Click(object s, EventArgs e)
        {
            if (IsMaximized)
                NormalizeWindow();
            else
                MaximizeWindow();
        }

        /// <summary>
        /// By holding down the left mouse button, the user can move the window.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void TitleBar_MouseDown(object s, MouseEventArgs e)
        {
            TitleBarClickX = e.X;
            TitleBarClickY = e.Y;
        }

        /// <summary>
        /// Move the window by moving the mouse.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void TitleBar_MouseMove(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (e.X != TitleBarClickX || e.Y != TitleBarClickY))
                Location = new Point(Location.X + e.X - TitleBarClickX, Location.Y + e.Y - TitleBarClickY);
        }

        /// <summary>
        /// Enable or disable certain buttons depending on the state of the app.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void TabControl_SelectedIndexChanged(object s, EventArgs e)
        {
            var enable = tabControl.SelectedIndex == 0;
            toolBtnSave.Enabled = enable;
            toolBtnSaveAll.Enabled = enable;
            toolBtnSaveAs.Enabled = enable;
            toolBtnComment.Enabled = enable;
            toolBtnUncomment.Enabled = enable;
            toolBtnDbgStepBreakpoint.Enabled = enable;
            toolBtnDbgStepInto.Enabled = enable;
            toolBtnDbgStepOver.Enabled = enable;
        }

        /// <summary>
        /// Override WndProc of the form to handle resizing of the borderless form.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int d = -(int)HTLEFT;
            const uint WM_NCHITTEST = 0x0084;
            const uint WM_MOUSEMOVE = 0x0200;
            
            if ((m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
                && FormBorderStyle == FormBorderStyle.FixedSingle)
            {
                // update resize boxes of the form
                ResizeBoxes[d + HTLEFT]       .Height = Size.Height - HANDLE_SIZE * 2;
                ResizeBoxes[d + HTRIGHT]      .X      = Size.Width  - HANDLE_SIZE;
                ResizeBoxes[d + HTRIGHT]      .Height = Size.Height - HANDLE_SIZE * 2;
                ResizeBoxes[d + HTTOP]        .Width  = Size.Width  - HANDLE_SIZE * 2;
                ResizeBoxes[d + HTTOPRIGHT]   .X      = Size.Width  - HANDLE_SIZE;
                ResizeBoxes[d + HTBOTTOM]     .Y      = Size.Height - HANDLE_SIZE;
                ResizeBoxes[d + HTBOTTOM]     .Width  = Size.Width  - HANDLE_SIZE * 2;
                ResizeBoxes[d + HTBOTTOMLEFT] .Y      = Size.Height - HANDLE_SIZE;
                ResizeBoxes[d + HTBOTTOMRIGHT].X      = Size.Width  - HANDLE_SIZE;
                ResizeBoxes[d + HTBOTTOMRIGHT].Y      = Size.Height - HANDLE_SIZE;

                // get cursor position
                var screenPoint = new Point(m.LParam.ToInt32());
                var clientPoint = PointToClient(screenPoint);

                // is cursor over a resize area of the form
                for (int i = 0; i < ResizeBoxes.Length; i++)
                {
                    if (ResizeBoxes[i].Contains(clientPoint))
                    {
                        m.Result = (IntPtr)HTLEFT + i;
                        return;
                    }
                }
            }

            // process using default wndproc
            base.WndProc(ref m);
        }
        
        /// <summary>
        /// Maximize window form.
        /// </summary>
        private void MaximizeWindow()
        {
            FormBorderStyle = FormBorderStyle.None;
            NormalLocation = Location;
            NormalSize = Size;
            TopMost = false;
            var screen = Screen.FromRectangle(DesktopBounds);
            Location = screen.WorkingArea.Location;
            Size = screen.WorkingArea.Size;
            Padding = new Padding(0);
            btnWindowMaximize.Image = Properties.Resources.Normalize;
            WindowState = Normal;
        }

        /// <summary>
        /// Normalize window size.
        /// </summary>
        private void NormalizeWindow()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Location = NormalLocation;
            Size = NormalSize;
            TopMost = false;
            Padding = new Padding(3);
            btnWindowMaximize.Image = Properties.Resources.Maximize;
            WindowState = Normal;
        }

        /// <summary>
        /// Make the window fullscreen.
        /// </summary>
        private void FullscreenWindow()
        {
            FormBorderStyle = FormBorderStyle.None;
            NormalLocation = Location;
            NormalSize = Size;
            TopMost = true;
            Padding = new Padding(0);
            btnWindowMaximize.Image = Properties.Resources.Normalize;
            WindowState = Maximized;
        }

        #endregion

        #region GlControl Events

        /// <summary>
        /// Handle mouse up events of the glCotrol.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void GlControl_MouseUp(object s, MouseEventArgs e)
        {
            // if pick tool button is checked, set
            // the fragment debug position to that pixel
            if (toolBtnPick.Checked)
            {
                Glsl.Shader.Settings.fs_FragCoord[0] = e.X;
                Glsl.Shader.Settings.fs_FragCoord[1] = glControl.Height - e.Y;
                debugProperty.Refresh();
                toolBtnPick.Checked = false;
                glControl.Cursor = Cursors.Default;
            }

            // if there are performance timings, show them
            if (glControl.TimingsCount > 0)
            {
                (var frames, var times) = PostProcessPerfData(
                    glControl.Frames, glControl.Timings, 10, true);

                var points = chartPerf.Series[0].Points;
                points.Clear();
                frames.ForEach(times, (f, t) => points.AddXY(f, t));
                chartPerf.Update();
            }

            // on mouse up, render and debug the
            // program, because there could be changes
            DebugRender(true);
        }

        #endregion

        #region Tool Buttons

        /// <summary>
        /// Open new tab.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ToolBtnNew_Click(object s, EventArgs e)
        {
            tabSource.SelectedIndex = AddTab(null);
            tabControl.SelectedIndex = 0;
        }

        /// <summary>
        /// Open file.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ToolBtnOpen_Click(object s, EventArgs e)
        {
            // create file dialog
            var openDlg = new OpenFileDialog()
            {
                Filter = "Text Files (.tech)|*.tech|All Files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = true
            };

            // open file dialog
            if (openDlg.ShowDialog() != DialogResult.OK)
                return;

            // open tabs
            openDlg.FileNames
                .Select(path => tabSource.TabPages.IndexOf(path))
                .Zip(openDlg.FileNames, (i,p) => i < 0 ? AddTab(p) : i)
                .ForEach(i => tabSource.SelectedIndex = i);

            tabControl.SelectedIndex = 0;
        }

        /// <summary>
        /// Run or debug the currently active tab.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ev"></param>
        private void ToolBtnRunDebug_Click(object s, EventArgs ev)
        {
            // if no tab page is selected nothing needs to be compiled
            if (SelectedTab == null)
                return;
            CompiledEditor = (CodeEditor)SelectedTab.Controls[0];

            // save code
            ToolBtnSave_Click(s, null);

            // clear scene and output
            output.Rows.Clear();
            glControl.ClearScene();
            glControl.RemoveEvents();

            // get include directory
            var includeDir = (CompiledEditor.Filename != null
                ? Path.GetDirectoryName(CompiledEditor.Filename)
                : Directory.GetCurrentDirectory()) + Path.DirectorySeparatorChar;

            // get code text form tab page
            // generate debug information?
            var debugging = s == toolBtnDbg;

            // COMPILE THE CURRENTLY SELECTED FILE
            var root = Compiler.Compile(CompiledEditor.Filename);
            var shaderLines = from x in root where x.Type == "shader" select new[] { x.Line, x.LineCount };
            CompiledEditor.RemoveInvalidBreakpoints(shaderLines);
            
            // INSTANTIATE THE CLASS WITH THE SPECIFIED ARGUMENTS (collect all errors)
            var ex = root.Catch(x => glControl.AddObject(x, debugging)).ToArray();
            // add events to the end of the event list
            glControl.AddEvents(output);
            glControl.MouseUp += new MouseEventHandler(GlControl_MouseUp);

            // show errors
            var exc = from x in ex
                      where x is CompileException || x.InnerException is CompileException
                      select (x is CompileException ? x : x.InnerException) as CompileException;
            var err = from x in exc from y in x select y;
            var line = from x in err select x.Line;
            err.ForEach(line, (e, l) => AddOutputItem(includeDir, e.File, l + 1, e.Msg));

            // underline all debug errors
            var ranges = line.Select(x => new[] {
                CompiledEditor.Lines[x].Position,
                CompiledEditor.Lines[x].EndPosition
            });
            CompiledEditor.ClearIndicators(CodeEditor.DebugIndicatorIndex);
            CompiledEditor.AddIndicators(CodeEditor.DebugIndicatorIndex, ranges);

            // SHOW SCENE
            DebugRender(debugging);

            // add externally created textures to the scene
            var existing = glControl.Scene.Values.ToArray();
            GLImage.FindTextures(existing).ForEach(x => glControl.Scene.Add(x.Name, x));

            // add externally created buffers to the scene
            GLBuffer.FindBuffers(existing).ForEach(x => glControl.Scene.Add(x.Name, x));

            // UPDATE DEBUG DATA
            comboBuf.Items.Clear();
            comboImg.Items.Clear();
            comboProp.Items.Clear();
            glControl.Scene.Where(x => x.Value is GLBuffer).ForEach(x => comboBuf.Items.Add(x.Value));
            glControl.Scene.Where(x => x.Value is GLImage).ForEach(x => comboImg.Items.Add(x.Value));
            glControl.Scene.Where(x => x.Value is GLInstance).ForEach(x => comboProp.Items.Add(x.Value));

            // SHOW DEBUG BUTTONS IF NECESSARY
            toolBtnDbgStepBreakpoint.Enabled = debugging;
            toolBtnDbgStepInto.Enabled = debugging;
            toolBtnDbgStepOver.Enabled = debugging;
            toolBtnDbgStepBack.Enabled = debugging;
            debugListView.Visible = debugging;
        }

        /// <summary>
        /// Save the currently active tab.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ToolBtnSave_Click(object s, EventArgs e)
        {
            if (!(SelectedTab?.Text.EndsWith("*") ?? false))
                return;
            SaveTab(SelectedTab, false);
            SelectedTab.Text = SelectedTab.Text.Substring(0, SelectedTab.Text.Length - 1);
        }

        /// <summary>
        /// Save all the tabs.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ToolBtnSaveAll_Click(object s, EventArgs e)
        {
            foreach (TabPage tab in tabSource.TabPages)
                SaveTab(tab, false);
        }

        /// <summary>
        /// Save currently active tab as a new file.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ToolBtnSaveAs_Click(object s, EventArgs e)
        {
            SaveTab(SelectedTab, true);
        }

        /// <summary>
        /// Close the currently active tab, but open the save dialog if there have been changes.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void TabSource_TabClose(object s, TabControlCancelEventArgs e)
        {
            if (!CloseTab(e.TabPage))
                e.Cancel = true;
        }

        /// <summary>
        /// Pick a debug pixel in the glControl.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ToolBtnPick_CheckedChanged(object s, EventArgs e)
        {
            glControl.Cursor = toolBtnPick.Checked ? Cursors.Cross : Cursors.Default;
        }

        /// <summary>
        /// Insert comment in all selected lines.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ToolBtnComment_Click(object s, EventArgs e)
        {
            var editor = SelectedEditor;
            if (editor == null)
                return;

            // gather some information
            int tabWidth = editor.TabWidth;
            int startLine = editor.LineFromPosition(editor.SelectionStart);
            int endLine = editor.LineFromPosition(editor.SelectionEnd);

            // find the minimal indent of blank spaces
            var minOffset = editor.Lines
                // for all selected lines 
                .Skip(startLine).Take(endLine - startLine + 1).Select(x => x.Text
                    // while lines starts with whitespace
                    .TakeWhile(c => c == '\t' || c == ' ')
                    // count space
                    .Select(c => c == '\t' ? tabWidth : 1).Sum())
                // find minimum
                .Min();

            // for all selected lines, insert the comment at minOffset
            for (int i = startLine; i <= endLine; i++)
            {
                var text = editor.Lines[i].Text;

                // get insert offset
                int offset = 0, n;
                for (n = minOffset; offset < text.Length && n > 0; offset++)
                    n -= text[offset] == '\t' ? tabWidth : 1;

                // insert comment
                editor.InsertText(editor.Lines[i].Position + offset, "//");
            }

            // reselect text
            editor.SelectionStart = editor.Lines[startLine].Position;
            editor.SelectionEnd = editor.Lines[endLine].EndPosition - 1;
        }

        /// <summary>
        /// Remove the leading line comment of the selected lines.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void ToolBtnUncomment_Click(object s, EventArgs e)
        {
            var editor = SelectedEditor;
            if (editor == null)
                return;

            // gather some information
            int startLine = editor.LineFromPosition(editor.SelectionStart);
            int endLine = editor.LineFromPosition(editor.SelectionEnd);

            // for all selected lines, remove the comment
            for (int i = startLine; i <= endLine; i++)
            {
                var line = editor.Lines[i];
                var match = RegexLineComment.Match(line.Text);
                if (match.Success)
                    editor.DeleteRange(line.Position + match.Index + match.Length - 2, 2);
            }

            // reselect text
            editor.SelectionStart = editor.Lines[startLine].Position;
            editor.SelectionEnd = editor.Lines[endLine].EndPosition - 1;
        }

        #region TOOL BUTTON FIELDS

        private Regex RegexLineComment = new Regex(@"\s*//");

        #endregion

        #endregion

        #region UTIL

        /// <summary>
        /// Save tab.
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="newfile"></param>
        private void SaveTab(TabPage tab, bool newfile)
        {
            if (tab == null)
                return;

            var editor = (CodeEditor)tab.Controls[0];

            // Open a save dialog if the tabPage is not liked
            // to a file or a new file should be created.
            if (editor.Filename == null || newfile)
            {
                var saveDlg = new SaveFileDialog()
                {
                    Filter = "Text Files (.tech)|*.tech|All Files (*.*)|*.*",
                    FilterIndex = 1
                };

                // if the dialog did not return a valid state
                if (saveDlg.ShowDialog() != DialogResult.OK)
                    return;

                editor.Filename = saveDlg.FileName;
                tab.Text = Path.GetFileName(saveDlg.FileName);
            }

            // save the file
            if (editor.IsDifferentFromFile())
            {
                editor.PauseFileWatch();
                System.IO.File.WriteAllText(editor.Filename, editor.Text);
                editor.ResumeFileWatch();
            }

            // remove change indicator
            if (tab.Text.EndsWith("*"))
                tab.Text = tab.Text.Substring(0, tab.Text.Length - 1);
        }

        /// <summary>
        /// Add new tab.
        /// </summary>
        /// <param name="path"></param>
        private int AddTab(string path)
        {
            // load file
            var filename = path != null ? Path.GetFileName(path) : "unnamed.tech";
            var text = path != null ? System.IO.File.ReadAllText(path) : "// Unnamed file";

            // create new tab objects
            var tabSourcePage = new TabPage();
            var editor = new CodeEditor(Properties.Resources.keywordsXML)
            {
                Filename = path
            };
            editor.ShowCallTip += Editor_ShowCallTip;
            editor.CancleCallTip += Editor_CancleCallTip;
            editor.CustomMouseHover += Editor_MouseHover;
            editor.VScrollBar = false;
            editor.HScrollBar = false;

            // tabSourcePage
            Theme.Apply(tabSourcePage);
            tabSourcePage.Controls.Add(editor);
            tabSourcePage.Location = new Point(4, 31);
            tabSourcePage.TabIndex = 0;
            tabSourcePage.Text = filename;
            tabSourcePage.AllowDrop = true;

            // add tab
            tabSource.Controls.Add(tabSourcePage);
            tabSource.Refresh();
            editor.PauseWatchChanges();
            editor.Text = text;
            editor.ResumeWatchChanges();
            editor.VScrollBar = true;
            editor.HScrollBar = true;
            return tabSource.TabPages.Count - 1;
        }

        /// <summary>
        /// Close tab page.
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>Returns <code>ture</code> if tab should be closed.</returns>
        private bool CloseTab(TabPage tab)
        {
            if (tab.Controls.Count == 0
                || !(tab.Controls[0] is CodeEditor editor)
                || !editor.IsDifferentFromFile())
                return true;

            var answer = MessageBox.Show(
                "Do you want to save the file before closing it?",
                "File changed", MessageBoxButtons.YesNoCancel);
            if (answer == DialogResult.Yes)
                SaveTab(tab, false);
            else if (answer == DialogResult.Cancel)
                return false;
            return true;
        }

        /// <summary>
        /// Process list of command line arguments.
        /// </summary>
        /// <param name="args">command line arguments</param>
        private void ProcessArgs(string[] args)
        {
            // process potential tech files
            tabSource.SelectedIndex = args.Skip(1)
                .Where(x => !x.StartsWith("-") && System.IO.File.Exists(x))
                .Select(x => AddTab(x)).LastOrDefault();

            // process potential settings
            foreach (var arg in args.Skip(1).Where(x => x.StartsWith("-")))
            {
                var a = arg.Split(new[] { ':' });
                switch (a[0])
                {
                    case "-HideDevGui": HideDeveloperGui(true); break;
                    case "-HideAllGui": HideAllGui(true); break;
                    case "-Compile": ToolBtnRunDebug_Click(null, null); break;
                    case "-Title": if (a.Length > 1) labelTitle.Text = a[1]; break;
                    case "-Fullscreen": FullscreenWindow(); break;
                }
            }
        }
        
        /// <summary>
        /// Hide or show developer graphical user interface.
        /// </summary>
        /// <param name="hide"></param>
        private void HideDeveloperGui(bool hide)
        {
            SplitRenderCoding.Panel2Collapsed = hide;
            splitRenderOutput.Panel2Collapsed = hide;
            btnWindowMinimize2.Visible = hide;
            btnWindowMaximize2.Visible = hide;
            btnWindowClose2.Visible = hide;
            if (hide)
            {
                SplitRenderCoding.Panel2.Hide();
                splitRenderOutput.Panel2.Hide();
            }
            else
            {
                SplitRenderCoding.Panel2.Show();
                splitRenderOutput.Panel2.Show();
            }
        }

        /// <summary>
        /// Hide or show all graphical user interface
        /// elements except glControl.
        /// </summary>
        /// <param name="hide"></param>
        private void HideAllGui(bool hide)
        {
            HideDeveloperGui(hide);
            tableLayoutRenderOutput.RowStyles[0].Height = 0;
        }

        #endregion

        #region Inner Classes

        public class FormSettings
        {
            public FormBorderStyle BorderStyle;
            public Point NormalLocation;
            public Size NormalSize;
            public float SplitRenderCoding;
            public float SplitRenderOutput;
            public float SplitDebugPerf;
            public float SplitDebug;
            public int NewLineHelper;
            public string ThemeXml;

            /// <summary>
            /// Default constructor.
            /// </summary>
            public static FormSettings CreateCentered()
            {
                int W = Screen.PrimaryScreen.WorkingArea.Width;
                int H = Screen.PrimaryScreen.WorkingArea.Height;
                var S = new Size(3 * W / 4, 3 * H / 4);
                return new FormSettings {
                    NormalSize = S,
                    NormalLocation = new Point((W - S.Width) / 2, (H - S.Height) / 2),
                    BorderStyle = FormBorderStyle.FixedSingle,
                    SplitRenderCoding = 0.5f,
                    SplitRenderOutput = 0.7f,
                    SplitDebugPerf = 0.52f,
                    SplitDebug = 0.55f,
                    NewLineHelper = 100,
                    ThemeXml = Theme.Name + ".xml",
                };
            }

            /// <summary>
            /// Create form settings structure from application.
            /// </summary>
            /// <param name="app"></param>
            /// <returns></returns>
            public static FormSettings Create(App app)
            {
                return new FormSettings
                {
                    BorderStyle = app.FormBorderStyle,
                    NormalLocation = app.IsMaximized ? app.NormalLocation : app.Location,
                    NormalSize = app.IsMaximized ? app.NormalSize : app.Size,
                    SplitRenderCoding = (float)app.SplitRenderCoding.SplitterDistance / app.SplitRenderCoding.Width,
                    SplitRenderOutput = (float)app.splitRenderOutput.SplitterDistance / app.splitRenderOutput.Height,
                    SplitDebugPerf = (float)app.splitDebugPerf.SplitterDistance / app.splitDebugPerf.Width,
                    SplitDebug = (float)app.splitDebug.SplitterDistance / app.splitDebug.Width,
                    NewLineHelper = CodeEditor.DefaultEdgeColumn,
                    ThemeXml = Theme.Name + ".xml",
                };
            }

            /// <summary>
            /// Place the form rectangle completely inside the nearest screen.
            /// </summary>
            /// <param name="app"></param>
            public void PlaceOnScreen(App app)
            {
                int L1 = int.MaxValue;
                int X = NormalLocation.X, Y = NormalLocation.Y;
                int W = NormalSize.Width, H = NormalSize.Height;

                // place the rect completely inside each screen
                // and store the result that changes the least
                foreach (var screen in Screen.AllScreens)
                {
                    var b = screen.Bounds;
                    int w = Math.Min(Math.Max(NormalSize.Width, 0), b.Size.Width);
                    int h = Math.Min(Math.Max(NormalSize.Height, 0), b.Size.Height);
                    int x = Math.Min(Math.Max(NormalLocation.X, b.Left), b.Right - NormalSize.Width);
                    int y = Math.Min(Math.Max(NormalLocation.Y, b.Top), b.Bottom - NormalSize.Height);
                    int l1 = Math.Abs(x - NormalLocation.X) + Math.Abs(y - NormalLocation.Y);
                    if (l1 < L1)
                    {
                        X = x;
                        Y = y;
                        W = w;
                        H = h;
                        L1 = l1;
                    }
                }

                // return rectangle with 
                // minimally changed position
                NormalLocation.X = X;
                NormalLocation.Y = Y;
                NormalSize.Width = W;
                NormalSize.Height = H;

                // update window
                app.Location = app.NormalLocation = NormalLocation;
                app.Size = app.NormalSize = NormalSize;
                app.FormBorderStyle = BorderStyle;
                if (app.IsMaximized)
                    app.MaximizeWindow();
                else
                    app.NormalizeWindow();
            }

            /// <summary>
            /// Adjust graphical user interface objects.
            /// </summary>
            /// <param name="app"></param>
            public void AdjustGUI(App app)
            {
                // place splitters by percentage
                app.SplitRenderCoding.SplitterDistance =
                    (int)(SplitRenderCoding * app.SplitRenderCoding.Width);
                app.splitRenderOutput.SplitterDistance =
                    (int)(SplitRenderOutput * app.splitRenderOutput.Height);
                app.splitDebugPerf.SplitterDistance =
                    (int)(SplitDebugPerf * app.splitDebugPerf.Width);
                app.splitDebug.SplitterDistance =
                    (int)(SplitDebug * app.splitDebug.Width);
                // select 'float' as the default buffer value type
                app.comboBufType.SelectedIndex = 8;
                // change new line helper position
                CodeEditor.DefaultEdgeColumn = NewLineHelper;
            }
        }

        #endregion
    }
}
