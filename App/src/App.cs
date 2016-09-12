using ScintillaNET;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.FormWindowState;

namespace App
{
    public partial class App : Form
    {
        public static CultureInfo Culture = new CultureInfo("en");
        public CodeEditor CompiledEditor = null;
        public int Frame => glControl.Frame;

        public App()
        {
            InitializeComponent();
        }

        #region Form Control
        /// <summary>
        /// On loading the app, load form settings and instantiate GLSL debugger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_Load(object sender, EventArgs e)
            {
                // LOAD PREVIOUS WINDOW STATE
            
                // load settings
                var settings = System.IO.File.Exists(Properties.Resources.WINDOW_SETTINGS_FILE)
                    ? XmlSerializer.Load<FormSettings>(Properties.Resources.WINDOW_SETTINGS_FILE)
                    : new FormSettings(this);
                // place form completely inside a screen
                settings.PlaceOnScreen(this);
                // place splitters
                settings.AdjustGUI(this);
            
                // LINK PROPERTY VIEWER TO DEBUG SETTINGS

                FxDebugger.Instantiate();
                debugProperty.SelectedObject = FxDebugger.Settings;
                debugProperty.CollapseAllGridItems();

                // PROCESS COMMAND LINE ARGUMENTS

                ProcessArgs(Environment.GetCommandLineArgs());

                // CLEAR OPENGL CONTROL

                glControl.AddEvents();
                glControl.Render();
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
                if (!tab.Text.EndsWith("*"))
                    continue;
                // ask user whether he/she wants to save those files
                DialogResult answer = MessageBox.Show(
                    "Do you want to save files with changes before closing them?",
                    "Save file changes", MessageBoxButtons.YesNoCancel);
                // if so, save all files with changes
                if (answer == DialogResult.Yes)
                    toolBtnSaveAll_Click(sender, null);
                else if (answer == DialogResult.Cancel)
                    e.Cancel = true;
            }

            // delete OpenGL objects
            glControl.ClearScene();

            // save current window state
            
            // do not save window in a minimized state
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;

            // get current window state
            var settings = new FormSettings
            {
                Left = Left, Top = Top, Width = Width, Height = Height, WindowState = WindowState,
                SplitRenderCoding = (float)splitRenderCoding.SplitterDistance / splitRenderCoding.Width,
                SplitRenderOutput = (float)splitRenderOutput.SplitterDistance / splitRenderOutput.Height,
                SplitDebug = (float)splitDebug.SplitterDistance / splitDebug.Width,
            };

            // if the state is not normal, make it normal
            if (WindowState != FormWindowState.Normal)
            {
                WindowState = FormWindowState.Normal;
                settings.Left = Left;
                settings.Top = Top;
                settings.Width = Width;
                settings.Height = Height;
            }

            // save to file
            XmlSerializer.Save(settings, Properties.Resources.WINDOW_SETTINGS_FILE);
        }
        
        /// <summary>
        /// Handle key events of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    // Compile and run
                    toolBtnRunDebug_Click(sender, null);
                    break;
                case Keys.F6:
                    // Compile and run
                    toolBtnRunDebug_Click(toolBtnDbg, null);
                    break;
                case Keys.S:
                    if (e.Control && e.Shift)
                        // Save all tabs
                        toolBtnSaveAll_Click(sender, null);
                    else if (e.Control)
                        // Save active tab
                        toolBtnSave_Click(sender, null);
                    else if (e.Alt)
                        // Save active tab as
                        toolBtnSaveAs_Click(sender, null);
                    break;
                case Keys.O:
                    if (e.Control)
                        // Open tech files
                        toolBtnOpen_Click(sender, null);
                    break;
                case Keys.Left:
                    if (e.Alt)
                        // Fold all code
                        GetSelectedEditor()?.FoldAll(FoldAction.Contract);
                    break;
                case Keys.Right:
                    if (e.Alt)
                        // Expand all code
                        GetSelectedEditor()?.FoldAll(FoldAction.Expand);
                    break;
            }
        }

        /// <summary>
        /// After clicking the window close button, close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWindowClose_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// After clicking the window minimize button, minimize the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWindowMinimize_Click(object sender, EventArgs e) => WindowState = Minimized;

        /// <summary>
        /// Maximize or normalize the window based on its current state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWindowMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == Maximized ? Normal : Maximized;
            UpdateBtnWindowMaximizeButtonImage();
        }

        /// <summary>
        /// By holding down the left mouse button, the user can move the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            TitleBarClickX = e.X;
            TitleBarClickY = e.Y;
        }

        /// <summary>
        /// Move the window by moving the mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (e.X != TitleBarClickX || e.Y != TitleBarClickY))
                Location = new Point(Location.X + e.X - TitleBarClickX, Location.Y + e.Y - TitleBarClickY);
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
            
            if ((m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE) && WindowState != Maximized)
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
        /// Update the maximize button image based on the current window state.
        /// </summary>
        private void UpdateBtnWindowMaximizeButtonImage()
        {
            btnWindowMaximize.Image = WindowState == Maximized
                ? Properties.Resources.Normalize
                : Properties.Resources.Maximize;
        }

        #region CONSTANT FIELDS
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

        #region glControl Events
        /// <summary>
        /// Handle mouse up events of the glCotrol.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            // if pick tool button is checked, set
            // the fragment debug position to that pixel
            if (toolBtnPick.Checked)
            {
                FxDebugger.Settings.fs_FragCoord[0] = e.X;
                FxDebugger.Settings.fs_FragCoord[1] = glControl.Height - e.Y;
                debugProperty.Refresh();
                toolBtnPick.Checked = false;
                glControl.Cursor = Cursors.Default;
            }

            // on mouse up, render and debug the
            // program, because there could be changes
            DebugRender();
        }
        #endregion

        #region Tool Buttons
        /// <summary>
        /// Open new tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnNew_Click(object sender, EventArgs e)
            => tabSource.SelectedIndex = AddTab(null);

        /// <summary>
        /// Open file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnOpen_Click(object sender, EventArgs e)
        {
            // create file dialog
            var openDlg = new OpenFileDialog();
            openDlg.Filter = "Text Files (.tech)|*.tech|All Files (*.*)|*.*";
            openDlg.FilterIndex = 1;
            openDlg.Multiselect = true;

            // open file dialog
            if (openDlg.ShowDialog() != DialogResult.OK)
                return;

            // open tabs
            openDlg.FileNames
                .Select(path => tabSource.TabPages.IndexOf(path))
                .Zip(openDlg.FileNames, (i,p) => i < 0 ? AddTab(p) : i)
                .ForEach(i => tabSource.SelectedIndex = i);
        }

        /// <summary>
        /// Run or debug the currently active tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void toolBtnRunDebug_Click(object sender, EventArgs ev)
        {
            // if no tab page is selected nothing needs to be compiled
            var sourceTab = (TabPageEx)tabSource.SelectedTab;
            if (sourceTab == null)
                return;
            CompiledEditor = (CodeEditor)sourceTab.Controls[0];
            
            // save code
            toolBtnSave_Click(sender, null);

            // clear scene and output
            output.Rows.Clear();
            glControl.ClearScene();
            glControl.RemoveEvents();

            // get include directory
            var includeDir = (sourceTab.filepath != null
                ? Path.GetDirectoryName(sourceTab.filepath)
                : Directory.GetCurrentDirectory()) + Path.DirectorySeparatorChar;

            // get code text form tab page
            // generate debug information?
            var debugging = sender == toolBtnDbg;

            // COMPILE THE CURRENTLY SELECTED FILE
            var root = Compiler.Compile(sourceTab.filepath);

            // INSTANTIATE THE CLASS WITH THE SPECIFIED ARGUMENTS (collect all errors)
            var ex = root.Catch(x => glControl.AddObject(x, debugging)).ToArray();
            // add events to the end of the event list
            glControl.AddEvents();
            glControl.MouseUp += new MouseEventHandler(glControl_MouseUp);

            // show errors
            var exc = from x in ex
                      where x is CompileException || x.InnerException is CompileException
                      select (x is CompileException ? x : x.InnerException) as CompileException;
            var err = from x in exc from y in x select y;
            var line = from x in err select x.Line;
            err.Zip(line, (e, l) => AddOutputItem(includeDir, e.File, l + 1, e.Msg));

            // underline all debug errors
            var ranges = line.Select(x => new[] {
                CompiledEditor.Lines[x].Position,
                CompiledEditor.Lines[x].EndPosition
            });
            CompiledEditor.ClearIndicators(CodeEditor.DebugIndicatorIndex);
            CompiledEditor.AddIndicators(CodeEditor.DebugIndicatorIndex, ranges);

            // SHOW SCENE
            glControl.Render();

            // add externally created textures to the scene
            var existing = glControl.Scene.Values.ToArray();
            GLImage.FindTextures(existing).ForEach(x => glControl.Scene.Add(x.name, x));

            // add externally created buffers to the scene
            GLBuffer.FindBuffers(existing).ForEach(x => glControl.Scene.Add(x.name, x));

            // UPDATE DEBUG DATA
            comboBuf.Items.Clear();
            comboImg.Items.Clear();
            comboProp.Items.Clear();
            glControl.Scene.Where(x => x.Value is GLBuffer).ForEach(x => comboBuf.Items.Add(x.Value));
            glControl.Scene.Where(x => x.Value is GLImage).ForEach(x => comboImg.Items.Add(x.Value));
            glControl.Scene.Where(x => x.Value is GLInstance).ForEach(x => comboProp.Items.Add(x.Value));

            // UPDATE DEBUG INFORMATION IF NECESSARY
            if (debugging)
                UpdateDebugListView(CompiledEditor);
        }

        /// <summary>
        /// Save the currently active tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnSave_Click(object sender, EventArgs e)
        {
            if (tabSource.SelectedTab == null)
                return;
            var tab = (TabPageEx)tabSource.SelectedTab;
            if (!tab.Text.EndsWith("*"))
                return;
            SaveTabPage(tab, false);
            tab.Text = tab.Text.Substring(0, tab.Text.Length - 1);
        }

        /// <summary>
        /// Save all the tabs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnSaveAll_Click(object sender, EventArgs e)
        {
            foreach (TabPageEx tab in tabSource.TabPages)
            {
                if (!tab.Text.EndsWith("*"))
                    continue;
                SaveTabPage(tab, false);
                tab.Text = tab.Text.Substring(0, tab.Text.Length - 1);
            }
        }

        /// <summary>
        /// Save currently active tab as a new file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnSaveAs_Click(object sender, EventArgs e)
            => SaveTabPage((TabPageEx)tabSource.SelectedTab, true);

        /// <summary>
        /// Close the currently active tab, but open the save dialog if there have been changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnClose_Click(object sender, EventArgs e)
        {
            if (tabSource.SelectedIndex < 0 || tabSource.SelectedIndex >= tabSource.TabPages.Count)
                return;
            var tab = (TabPageEx)tabSource.SelectedTab;
            if (tab.Text.EndsWith("*"))
            {
                DialogResult answer = MessageBox.Show(
                    "Do you want to save the file before closing it?",
                    "File changed", MessageBoxButtons.YesNo);
                if (answer == DialogResult.Yes)
                    SaveTabPage(tab, false);
            }
            tabSource.TabPages.RemoveAt(tabSource.SelectedIndex);
        }

        /// <summary>
        /// Pick a debug pixel in the glControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnPick_CheckedChanged(object sender, EventArgs e)
            => glControl.Cursor = toolBtnPick.Checked ? Cursors.Cross : Cursors.Default;

        /// <summary>
        /// Insert comment in all selected lines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnComment_Click(object sender, EventArgs e)
        {
            var editor = (CodeEditor)((TabPageEx)tabSource.SelectedTab)?.Controls[0];
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnUncomment_Click(object sender, EventArgs e)
        {
            var editor = (CodeEditor)((TabPageEx)tabSource.SelectedTab)?.Controls[0];
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
        /// <param name="tabPage"></param>
        /// <param name="newfile"></param>
        private void SaveTabPage(TabPageEx tabPage, bool newfile)
        {
            if (tabPage == null)
                return;

            var editor = (CodeEditor)tabPage.Controls[0];

            if (tabPage.filepath == null || newfile)
            {
                var saveDlg = new SaveFileDialog();
                saveDlg.Filter = "Text Files (.tech)|*.tech|All Files (*.*)|*.*";
                saveDlg.FilterIndex = 1;

                var result = saveDlg.ShowDialog();
                if (result != DialogResult.OK)
                    return;

                tabPage.filepath = saveDlg.FileName;
                tabPage.Text = Path.GetFileName(saveDlg.FileName);
            }

            System.IO.File.WriteAllText(tabPage.filepath, editor.Text);
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
            var tabSourcePage = new TabPageEx(path);
            var editor = new CodeEditor(text);
            editor.UpdateUI += new EventHandler<UpdateUIEventArgs>(editor_UpdateUI);
            editor.MouseMove += new MouseEventHandler(editor_MouseMove);

            // tabSourcePage
            tabSourcePage.Controls.Add(editor);
            tabSourcePage.Location = new Point(4, 31);
            tabSourcePage.Margin = new Padding(0);
            tabSourcePage.Padding = new Padding(3);
            tabSourcePage.TabIndex = 0;
            tabSourcePage.Text = filename;
            tabSourcePage.AllowDrop = true;

            // add tab
            tabSource.Controls.Add(tabSourcePage);
            return tabSource.TabPages.Count - 1;
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
                    case "-HideGui": HideDeveloperGui(true); break;
                    case "-Compile": toolBtnRunDebug_Click(null, null); break;
                    case "-Title": Text = a.Length > 1 ? a[1] : Text; break;
                }
            }
        }
        
        /// <summary>
        /// Hide or show developer graphical user interface.
        /// </summary>
        /// <param name="hide"></param>
        private void HideDeveloperGui(bool hide)
        {
            splitRenderCoding.Panel2Collapsed = hide;
            splitRenderOutput.Panel2Collapsed = hide;
            if (hide)
            {
                splitRenderCoding.Panel2.Hide();
                splitRenderOutput.Panel2.Hide();
            }
            else
            {
                splitRenderCoding.Panel2.Show();
                splitRenderOutput.Panel2.Show();
            }
        }
        
        /// <summary>
        /// Get code editor of the currently active tab.
        /// </summary>
        /// <returns></returns>
        private CodeEditor GetSelectedEditor()
        {
            var sourceTab = (TabPage)tabSource.SelectedTab;
            if (sourceTab == null)
                return null;
            return (CodeEditor)sourceTab.Controls[0];
        }
        #endregion

        #region Inner Classes
        public struct FormSettings
        {
            public int Left, Top, Width, Height;
            public FormWindowState WindowState;
            public float SplitRenderCoding;
            public float SplitRenderOutput;
            public float SplitDebug;

            /// <summary>
            /// Default constructor.
            /// </summary>
            /// <param name="form"></param>
            public FormSettings(Form form)
            {
                Left = form.Left;
                Top = form.Top;
                Width = form.Width;
                Height = form.Height;
                WindowState = form.WindowState;
                SplitRenderCoding = 0.4f;
                SplitRenderOutput = 0.7f;
                SplitDebug = 0.55f;
            }

            /// <summary>
            /// Place the form rectangle completely inside the nearest screen.
            /// </summary>
            /// <param name="app"></param>
            public void PlaceOnScreen(App app)
            {
                int L1 = int.MaxValue;
                int X = Left, Y = Top, W = Width, H = Height;

                // place the rect completely inside each screen
                // and store the result that changes the least
                foreach (var screen in Screen.AllScreens)
                {
                    var b = screen.Bounds;
                    int w = Math.Min(Math.Max(Width, 0), b.Size.Width);
                    int h = Math.Min(Math.Max(Height, 0), b.Size.Height);
                    int x = Math.Min(Math.Max(Left, b.Left), b.Right - Width);
                    int y = Math.Min(Math.Max(Top, b.Top), b.Bottom - Height);
                    int l1 = Math.Abs(x - Left) + Math.Abs(y - Top);
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
                Left = X;
                Top = Y;
                Width = W;
                Height = H;
                
                // update window
                app.Left = Math.Max(0, Left);
                app.Top = Math.Max(0, Top);
                app.Width = Width;
                app.Height = Height;
                app.WindowState = WindowState;
                app.UpdateBtnWindowMaximizeButtonImage();
            }

            /// <summary>
            /// Adjust graphical user interface objects.
            /// </summary>
            /// <param name="app"></param>
            public void AdjustGUI(App app)
            {
                // place splitters by percentage
                app.splitRenderCoding.SplitterDistance =
                    (int)(SplitRenderCoding * app.splitRenderCoding.Width);
                app.splitRenderOutput.SplitterDistance =
                    (int)(SplitRenderOutput * app.splitRenderOutput.Height);
                app.splitDebug.SplitterDistance =
                    (int)(SplitDebug * app.splitDebug.Width);
                // select 'float' as the default buffer value type
                app.comboBufType.SelectedIndex = 8;
            }
        }
        #endregion
    }
}
