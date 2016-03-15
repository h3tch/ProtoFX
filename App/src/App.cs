using OpenTK.Graphics.OpenGL4;
using ScintillaNET;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace App
{
    public partial class App : Form
    {
        public static CultureInfo Culture = new CultureInfo("en");
        public CodeEditor CompiledEditor = null;

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
            // load previous window state
            if (System.IO.File.Exists(Properties.Resources.WINDOW_SETTINGS_FILE))
            {
                // load settings
                var settings = XmlSerializer.Load<FormSettings>(Properties.Resources.WINDOW_SETTINGS_FILE);
                // place form completely inside a screen
                settings.PlaceOnScreen();
                // update window
                Left = Math.Max(0, settings.Left);
                Top = Math.Max(0, settings.Top);
                Width = settings.Width;
                Height = settings.Height;
                WindowState = settings.WindowState;
            }

            // select 'float' as the default buffer value type
            comboBufType.SelectedIndex = 8;

            // place splitters by percentage
            splitCodeError.SplitterDistance = (int)(0.7 * splitCodeError.Height);
            splitDebug.SplitterDistance = (int)(0.55 * splitDebug.Width);

            // link property viewer to debug settings
            GLDebugger.Instantiate();
            debugProperty.SelectedObject = GLDebugger.Settings;
            debugProperty.CollapseAllGridItems();
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
                Left = Left, Top = Top, Width = Width, Height = Height, WindowState = WindowState
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
            }
        }
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
                GLDebugger.Settings.fs_FragCoord[0] = e.X;
                GLDebugger.Settings.fs_FragCoord[1] = glControl.Height - e.Y;
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
        {
            AddTab(null);
            tabSource.SelectedIndex = tabSource.TabPages.Count-1;
        }

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
            foreach (var path in openDlg.FileNames)
            {
                int i = tabSource.TabPages.IndexOf(path);
                if (i < 0)
                {
                    i = tabSource.TabPages.Count;
                    AddTab(path);
                }
                tabSource.SelectedIndex = i;
            }
        }

        /// <summary>
        /// Run or debug the currently active tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void toolBtnRunDebug_Click(object sender, EventArgs ev)
        {
            // if no tab page is selected nothing needs to be compiled
            var sourceTab = (TabPage)tabSource.SelectedTab;
            if (sourceTab == null)
                return;
            CompiledEditor = (CodeEditor)sourceTab.Controls[0];
            
            // save code
            toolBtnSave_Click(sender, null);

            // clear scene and output
            output.Rows.Clear();
            glControl.ClearScene();

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

            // show errors
            var exc = from x in ex
                      where x is CompileException || x.InnerException is CompileException
                      select (x is CompileException ? x : x.InnerException) as CompileException;
            var err = from x in exc from y in x select y;
            var line = err.Select(x => x.Line);
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

            // also add externally created textures to the scene
            var internID = from x in glControl.Scene
                           where x.Value is GLImage || x.Value is GLTexture
                           select x.Value.glname;
            var externID = from x in Enumerable.Range(0, 64)
                           where !internID.Contains(x) && GL.IsTexture(x)
                           select x;
            externID
                .Select(x => new GLImage($"{GLImage.GetLabel(x)}", "tex", x))
                .Do(x => glControl.Scene.Add(x.name, x));

            // also add externally created buffers to the scene
            internID = from x in glControl.Scene
                       where x.Value is GLBuffer
                       select x.Value.glname;
            externID = from x in Enumerable.Range(0, 64)
                       where !internID.Contains(x) && GL.IsBuffer(x)
                       select x;
            externID
                .Select(x => new GLBuffer($"{GLBuffer.GetLabel(x)}", "buf", x))
                .Do(x => glControl.Scene.Add(x.name, x));

            // UPDATE DEBUG DATA
            comboBuf.Items.Clear();
            comboImg.Items.Clear();
            comboProp.Items.Clear();
            glControl.Scene.Where(x => x.Value is GLBuffer).Do(x => comboBuf.Items.Add(x.Value));
            glControl.Scene.Where(x => x.Value is GLImage).Do(x => comboImg.Items.Add(x.Value));
            glControl.Scene.Where(x => x.Value is GLInstance).Do(x => comboProp.Items.Add(x.Value));

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
            TabPage tab = (TabPage)tabSource.SelectedTab;
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
            foreach (TabPage tab in tabSource.TabPages)
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
            => SaveTabPage((TabPage)tabSource.SelectedTab, true);

        /// <summary>
        /// Close the currently active tab, but open the save dialog if there have been changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnClose_Click(object sender, EventArgs e)
        {
            if (tabSource.SelectedIndex < 0 || tabSource.SelectedIndex >= tabSource.TabPages.Count)
                return;
            TabPage tabSourcePage = (TabPage)tabSource.SelectedTab;
            if (tabSourcePage.Text.EndsWith("*"))
            {
                DialogResult answer = MessageBox.Show(
                    "Do you want to save the file before closing it?",
                    "File changed", MessageBoxButtons.YesNo);
                if (answer == DialogResult.Yes)
                    SaveTabPage(tabSourcePage, false);
            }
            tabSource.TabPages.RemoveAt(tabSource.SelectedIndex);
        }

        /// <summary>
        /// Pick a debug pixel in the glControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnPick_CheckedChanged(object sender, EventArgs e)
        {
            if (toolBtnPick.Checked)
                glControl.Cursor = Cursors.Cross;
            else
                glControl.Cursor = Cursors.Default;
        }
        #endregion
        
        #region UTIL
        /// <summary>
        /// Save tab.
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="newfile"></param>
        private void SaveTabPage(TabPage tabPage, bool newfile)
        {
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
        private void AddTab(string path)
        {
            // load file
            string filename = path != null ? Path.GetFileName(path) : "unnamed.tech";
            string text = path != null ? System.IO.File.ReadAllText(path) : "// Unnamed file";

            // create new tab objects
            var tabSourcePage = new TabPage(path);
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

            /// <summary>
            /// Place the form rectangle completely inside the nearest screen.
            /// </summary>
            public void PlaceOnScreen()
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
            }
        }
        #endregion
    }
}
