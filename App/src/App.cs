using OpenTK.Graphics.OpenGL4;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
    public partial class App : Form
    {
        public static CultureInfo culture = new CultureInfo("en");

        public App()
        {
            InitializeComponent();
        }

        #region App Control
        private void App_Load(object sender, EventArgs e)
        {
            // select 'float' as the default buffer value type
            comboBufType.SelectedIndex = 8;

            // link property viewer to debug settings
            GLDebugger.Instantiate();
            debugProperty.SelectedObject = GLDebugger.settings;
            debugProperty.CollapseAllGridItems();
        }

        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            // check if there are any files with changes
            foreach (TabPage tab in this.tabSource.TabPages)
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
        }

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

        private void glControl_MouseUp(object sender, EventArgs e)
        {
            propertyGrid.Refresh();
            GetSelectedEditor()?.Do(x => UpdateDebugListView(x));
        }
        #endregion
        
        #region Debug Image
        private void comboImg_SelectedIndexChanged(object sender, EventArgs e)
            => pictureImg_Click(sender, e);

        private void numImgLayer_ValueChanged(object sender, EventArgs e)
        {
            if (comboImg.SelectedItem == null || !(comboImg.SelectedItem is GLImage))
                return;

            // get selected image
            var img = (GLImage)comboImg.SelectedItem;
            numImgLayer.Maximum = Math.Max(Math.Max(img.length, img.depth) - 1, 0);

            // read image data from GPU
            glControl.MakeCurrent();
            var bmp = img.Read((int)numImgLayer.Value, 0);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureImg.Image = bmp;
        }

        private void pictureImg_Click(object sender, EventArgs e)
            => numImgLayer_ValueChanged(sender, e);
        #endregion

        #region Debug Buffer
        private void comboBuf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBuf.SelectedItem == null || !(comboBuf.SelectedItem is GLBuffer))
                return;

            // gather needed info
            var buf = (GLBuffer)comboBuf.SelectedItem;
            var type = (string)comboBufType.SelectedItem;
            var dim = (int)numBufDim.Value;

            // read data from GPU
            glControl.MakeCurrent();
            var data = buf.Read();

            // convert data to specified type
            Type colType;
            Array da = data.To(type, out colType);

            // CREATE TABLE
            var dt = new DataTable(buf.name);
            // create columns
            for (int i = 0; i < dim; i++)
                dt.Columns.Add(i.ToString(), colType);
            // create rows
            for (int i = 0; i < da.Length;)
            {
                var row = dt.NewRow();
                for (int c = 0; c < dim && i < da.Length; c++)
                    row.SetField(c, da.GetValue(i++));
                dt.Rows.Add(row);
            }

            // update GUI
            var ds = new DataSet(buf.name);
            ds.Tables.Add(dt);
            tableBuf.DataSource = ds;
            tableBuf.DataMember = buf.name;
        }

        private void comboBufType_SelectedIndexChanged(object sender, EventArgs e)
            => comboBuf_SelectedIndexChanged(sender, e);
        
        private void numBufDim_ValueChanged(object sender, EventArgs e)
            => comboBuf_SelectedIndexChanged(sender, null);
        #endregion

        #region Debug Properties
        private void comboProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProp.SelectedItem == null || !(comboProp.SelectedItem is GLInstance))
                return;

            // gather needed info
            propertyGrid.SelectedObject = ((GLInstance)comboProp.SelectedItem).instance;
        }

        private void propertyGrid_Click(object sender, EventArgs e)
            => propertyGrid.SelectedObject = propertyGrid.SelectedObject;

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            glControl.Render();
            GetSelectedEditor()?.Do(x => UpdateDebugListView(x));
        }
        #endregion

        #region Debug Variables
        public void UpdateDebugListView(CodeEditor editor)
        {
            // RESET DEBUG LIST VIEW
            debugListView.Clear();
            debugListView.View = View.Details;
            debugListView.FullRowSelect = true;
            debugListView.Columns.Add("X", 80);
            debugListView.Columns.Add("Y", 80);
            debugListView.Columns.Add("Z", 80);
            debugListView.Columns.Add("W", 80);

            // if the code has been edited no debug information can
            // be shown, because debug variables might have been
            // added or removed, which leads to invalid debug output
            if (((TabPage)editor.Parent).Text.EndsWith("*"))
                return;

            // find all debug variables
            var dbgVarMatches = Regex.Matches(editor.Text, GLDebugger.regexDbgVar);

            // get line of where the caret is placed
            var dbgLine = editor.LineFromPosition(editor.CurrentPosition);

            int ID = 0;
            foreach (Match dbgVarMatch in dbgVarMatches)
            {
                // next debug variable ID
                ID++;

                // get line of the debug variable
                var line = editor.LineFromPosition(dbgVarMatch.Index);

                // is the debug variable in the same line
                if (line < dbgLine)
                    continue;
                if (line > dbgLine)
                    break;

                // try to find the debug variable ID
                var val = GLDebugger.GetDebugVariable(ID - 1, glControl.Frame - 1);
                if (val == null)
                    continue;

                // CREATE LIST VIEW ROWS

                int rows = val.GetLength(0);
                int cols = val.GetLength(1);
                // debug variable name
                var name = dbgVarMatch.Value.Substring(3, dbgVarMatch.Value.Length - 6);

                // add list group for this debug variable
                var dbgVarGroup = new ListViewGroup(name);
                debugListView.Groups.Add(dbgVarGroup);

                for (int r = 0; r < rows; r++)
                {
                    // convert row of debug variable to string array
                    var row = from c in Enumerable.Range(0, cols)
                              select string.Format(culture, "{0:0.000}", val.GetValue(r, c));
                    // add row to list view
                    var item = new ListViewItem(row.ToArray());
                    item.Group = dbgVarGroup;
                    debugListView.Items.Add(item);
                }
            }
        }
        #endregion

        #region Tool Buttons
        private void toolBtnNew_Click(object sender, EventArgs e)
        {
            AddSourceTab(null);
            tabSource.SelectedIndex = tabSource.TabPages.Count-1;
        }

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
                    AddSourceTab(path);
                }
                tabSource.SelectedIndex = i;
            }
        }

        private void toolBtnRunDebug_Click(object sender, EventArgs e)
        {
            // if no tab page is selected nothing needs to be compiled
            var sourceTab = (TabPage)tabSource.SelectedTab;
            if (sourceTab == null)
                return;
            var editor = (CodeEditor)sourceTab.Controls[0];
            
            // save code
            toolBtnSave_Click(sender, null);

            // clear scene and output
            codeError.Text = "";
            glControl.ClearScene();

            // get include directory
            var includeDir = sourceTab.filepath != null ?
                Path.GetDirectoryName(sourceTab.filepath) : Directory.GetCurrentDirectory();
            includeDir += '\\';

            // get code text form tab page
            // generate debug information?
            var debugging = sender == toolBtnDbg;

            // remove comments
            var code = CodeEditor.RemoveComments(editor.Text);
            code = CodeEditor.RemoveNewLineIndicators(code);
            code = CodeEditor.IncludeFiles(code, includeDir);
            code = CodeEditor.ResolvePreprocessorDefinitions(code);

            // FIND PROTOGL CLASS BLOCKS (find "TYPE name { ... }")
            var blocks = CodeEditor.GetBlocks(code);

            // INSTANTIATE THE CLASS WITH THE SPECIFIED ARGUMENTS (collect all errors)
            var ex = blocks.Catch(x => glControl.AddObject(x, includeDir, debugging)).ToArray();

            // show errors
            var errors = from x in ex
                         where x is CompileException || x.InnerException is CompileException
                         select (x is CompileException ? x : x.InnerException) as CompileException;
            errors.Do(x => codeError.AppendText(x.Text));

            // SHOW SCENE
            glControl.Render();

            // also add externally created textures to the scene
            var appIDs = from x in glControl.Scene
                         where x.Value is GLImage || x.Value is GLTexture
                         select x.Value.glname;
            var glIDs = from x in Enumerable.Range(0, 64)
                        where !appIDs.Contains(x) && GL.IsTexture(x)
                        select x;
            glIDs.Do(x => glControl.Scene.Add("GLTex" + x,
                new GLImage(new GLParams("GLTex" + x, "tex"), x)));

            // also add externally created buffers to the scene
            appIDs = from x in glControl.Scene
                     where x.Value is GLBuffer select x.Value.glname;
            glIDs = from x in Enumerable.Range(0, 64)
                    where !appIDs.Contains(x) && GL.IsBuffer(x) select x;
            glIDs.Do(x => glControl.Scene.Add("GLBuf" + x,
                new GLBuffer(new GLParams("GLBuf" + x, "buf"), x)));

            // UPDATE DEBUG DATA
            comboBuf.Items.Clear();
            comboImg.Items.Clear();
            comboProp.Items.Clear();
            glControl.Scene.Where(x => x.Value is GLBuffer).Do(x => comboBuf.Items.Add(x.Value));
            glControl.Scene.Where(x => x.Value is GLImage).Do(x => comboImg.Items.Add(x.Value));
            glControl.Scene.Where(x => x.Value is GLInstance).Do(x => comboProp.Items.Add(x.Value));

            // UPDATE DEBUG INFORMATION IF NECESSARY
            if (debugging)
                UpdateDebugListView(editor);
        }

        private void toolBtnSave_Click(object sender, EventArgs e)
        {
            TabPage tab = (TabPage)tabSource.SelectedTab;
            if (!tab.Text.EndsWith("*"))
                return;
            SaveTabPage(tab, false);
            tab.Text = tab.Text.Substring(0, tab.Text.Length - 1);
        }

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

        private void toolBtnSaveAs_Click(object sender, EventArgs e)
            => SaveTabPage((TabPage)tabSource.SelectedTab, true);

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
        #endregion
        
        #region UTIL
        private void SaveTabPage(TabPage tabPage, bool newfile)
        {
            var selectedTabPageText = (CodeEditor)tabPage.Controls[0];

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

            File.WriteAllText(tabPage.filepath, selectedTabPageText.Text);
        }

        private void AddSourceTab(string path)
        {
            // load file
            string filename = path != null ? Path.GetFileName(path) : "unnamed.tech";
            string text = path != null ? File.ReadAllText(path) : "// Unnamed ProtoGL file";

            // create new tab objects
            var tabSourcePage = new TabPage(path);
            var tabSourcePageText = new CodeEditor(this, text);

            // tabSourcePage
            tabSourcePage.Controls.Add(tabSourcePageText);
            tabSourcePage.Location = new Point(4, 31);
            tabSourcePage.Margin = new Padding(0);
            tabSourcePage.Padding = new Padding(3);
            tabSourcePage.TabIndex = 0;
            tabSourcePage.Text = filename;
            tabSourcePage.AllowDrop = true;

            // add tab
            tabSource.Controls.Add(tabSourcePage);
            
        }

        private CodeEditor GetSelectedEditor()
        {
            var sourceTab = (TabPage)tabSource.SelectedTab;
            if (sourceTab == null)
                return null;
            return (CodeEditor)sourceTab.Controls[0];
        }
        #endregion
    }
}
