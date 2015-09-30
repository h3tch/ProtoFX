using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
    public partial class App : Form
    {
        #region FIELDS
        private Dict classes = new Dict();
        private bool render = false;
        public static CultureInfo culture = new CultureInfo("en");
        #endregion

        public App()
        {
            InitializeComponent();

            // ADD LIB FOLDER TO ENVIRONMENT VARIABLE PATH
            // this is necessary for ScintillaNET
            var envPath = Environment.GetEnvironmentVariable("PATH");
            var dir = Directory.GetCurrentDirectory();
            var lib = dir + "\\..\\lib";
            if (!envPath.Contains(lib))
                Environment.SetEnvironmentVariable("PATH", envPath + ";" + lib);

            // select 'float' as the default buffer value type
            this.comboBufType.SelectedIndex = 7;
        }
        
        #region App Control
        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            // check if there are any files with changes
            foreach (TabPage tab in this.tabSource.TabPages)
            {
                if (tab.Text.EndsWith("*"))
                {
                    // ask user whether he/she wants to save those files
                    DialogResult answer = MessageBox.Show(
                        "Do you want to save files with changes before closing them?",
                        "Save file changes", MessageBoxButtons.YesNoCancel);
                    // if so, save all files with changes
                    if (answer == DialogResult.Yes)
                        toolBtnSaveAll_Click(sender, null);
                    else if (answer == DialogResult.Cancel)
                        e.Cancel = true;
                    break;
                }
            }

            // delete OpenGL objects
            ClearGLObjects();
        }

        private void App_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    // Compile and run
                    toolBtnRun_Click(sender, null);
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

        #region OpenGL Control
        private void glControl_Resize(object sender, EventArgs e)
        {
            Render();
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            render = true;
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            render = false;
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (render)
                Render();
        }
        #endregion

        #region Debug Image
        private void comboImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureImg_Click(sender, e);
        }

        private void numImgLayer_ValueChanged(object sender, EventArgs e)
        {
            if (comboImg.SelectedItem == null || comboImg.SelectedItem.GetType() != typeof(GLImage))
                return;
            var img = (GLImage)comboImg.SelectedItem;
            numImgLayer.Maximum = Math.Max(Math.Max(img.length, img.depth) - 1, 0);
            pictureImg.Image = GetTextureLayer(img, (int)numImgLayer.Value);
        }

        private void pictureImg_Click(object sender, EventArgs e)
        {
            numImgLayer.Value = 0;
            numImgLayer_ValueChanged(sender, e);
        }
        #endregion

        #region Debug Buffer
        private void comboBuf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBuf.SelectedItem == null
                || this.comboBuf.SelectedItem.GetType() != typeof(GLBuffer))
                return;

            // gather needed info
            GLBuffer buf = (GLBuffer)this.comboBuf.SelectedItem;
            string type = (string)comboBufType.SelectedItem;
            int dim = (int)numBufDim.Value;

            // read data from GPU
            glControl.MakeCurrent();
            byte[] data = buf.Read();

            // convert data to specified type
            Type colType;
            Array da = ConvertData(data, type, out colType);

            // CREATE TABLE
            DataTable dt = new DataTable(buf.name);
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
            DataSet ds = new DataSet(buf.name);
            ds.Tables.Add(dt);
            tableBuf.DataSource = ds;
            tableBuf.DataMember = buf.name;
        }

        private void comboBufType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBuf_SelectedIndexChanged(sender, e);
        }
        
        private void numBufDim_ValueChanged(object sender, EventArgs e)
        {
            comboBuf_SelectedIndexChanged(sender, null);
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
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Text Files (.tech)|*.tech|All Files (*.*)|*.*";
            openDlg.FilterIndex = 1;
            openDlg.Multiselect = true;

            var result = openDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (var filename in openDlg.FileNames)
                {
                    int i = 0;
                    for (; i < tabSource.TabPages.Count; i++)
                    {
                        if (((TabPage)tabSource.TabPages[i]).filepath == filename)
                        {
                            this.tabSource.SelectedIndex = i;
                            break;
                        }
                    }
                    if (i == tabSource.TabPages.Count)
                    {
                        AddSourceTab(filename);
                        tabSource.SelectedIndex = i;
                    }
                }
            }
        }

        private void toolBtnRun_Click(object sender, EventArgs e)
        {
            this.codeError.Text = "";
            ClearGLObjects();

            var selectedSourceTab = (TabPage)this.tabSource.SelectedTab;
            var selectedSourceText = (Scintilla)selectedSourceTab.Controls[0];
            var dir = selectedSourceTab.filepath != null ?
                Path.GetDirectoryName(selectedSourceTab.filepath) : Directory.GetCurrentDirectory();
            dir += '\\';

            try
            {
                // remove comments
                var code = RemoveComments(selectedSourceText.Text, "//");
                code = IncludeFiles(dir, code);

                // find GLST class blocks (find "TYPE name { ... }")
                var blocks = FindObjectBlocks(code);

                // parse commands for each class block
                for (int i = 0; i < blocks.Length; i++)
                {
                    // PARSE CLASS INFO
                    string[] classInfo = FindObjectClass(blocks[i]);

                    // PARSE CLASS TEXT
                    var start = blocks[i].IndexOf('{');
                    string classText = blocks[i].Substring(start + 1, blocks[i].LastIndexOf('}') - start - 1);

                    // GET CLASS TYPE, ANNOTATION AND NAME
                    var classType = "App.GL"
                        + classInfo[0].First().ToString().ToUpper()
                        + classInfo[0].Substring(1);
                    var classAnno = classInfo[classInfo.Length - 2];
                    var className = classInfo[classInfo.Length - 1];

                    // INSTANTIATE THE CLASS WITH THE SPECIFIED ARGUMENTS
                    try
                    {
                        var type = Type.GetType(classType);
                        // check for errors
                        if (type == null)
                            throw new Exception("ERROR in " + classInfo[0] + " " + className + ": "
                                + "Class type '" + classInfo[0] + "' not known.");
                        if (this.classes.ContainsKey(className))
                            throw new Exception("ERROR in " + classInfo[0] + " " + className + ": "
                                + "Class name '" + className + "' already exists.");
                        // instantiate class
                        this.classes.Add(className, (GLObject)Activator.CreateInstance(
                            type, dir, className, classAnno, classText, this.classes));
                    }
                    catch (Exception ex)
                    {
                        // show errors
                        this.codeError.AppendText(ex.GetBaseException().Message + '\n');
                    }
                }
            }
            catch (Exception ex)
            {
                // show errors
                this.codeError.AppendText(ex.GetBaseException().Message + '\n');
            }

            // UPDATE DEBUG DATA
            this.comboBuf.Items.Clear();
            this.comboImg.Items.Clear();
            foreach (var pair in classes)
            {
                if (pair.Value.GetType() == typeof(GLBuffer))
                    this.comboBuf.Items.Add(pair.Value);
                else if (pair.Value.GetType() == typeof(GLImage))
                    this.comboImg.Items.Add(pair.Value);
            }

            // SHOW SCENE
            Render();
        }

        private void toolBtnSave_Click(object sender, EventArgs e)
        {
            TabPage tab = (TabPage)this.tabSource.SelectedTab;
            if (!tab.Text.EndsWith("*"))
                return;
            SaveTabPage(tab, false);
            tab.Text = tab.Text.Substring(0, tab.Text.Length - 1);
        }

        private void toolBtnSaveAll_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in this.tabSource.TabPages)
            {
                if (!tab.Text.EndsWith("*"))
                    continue;
                SaveTabPage(tab, false);
                tab.Text = tab.Text.Substring(0, tab.Text.Length - 1);
            }
        }

        private void toolBtnSaveAs_Click(object sender, EventArgs e)
        {
            SaveTabPage((TabPage)this.tabSource.SelectedTab, true);
        }

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

        #region Scintilla Text Control
        private void tabSourceText_TextChanged(object sender, EventArgs e)
        {
            Scintilla tabSourceText = (Scintilla)sender;
            TabPage tabSourcePage = (TabPage)tabSourceText.Parent;
            if (!tabSourcePage.Text.EndsWith("*"))
                tabSourcePage.Text = tabSourcePage.Text + '*';

            // UPDATE LINE NUMBERS
            var lineNumberLength = tabSourceText.Lines.Count.ToString().Length;
            var lineNumberWidth = TextRenderer.MeasureText(new string('9', lineNumberLength), tabSourceText.Font).Width;
            if (tabSourceText.Margins[0].Width != lineNumberWidth)
                tabSourceText.Margins[0].Width = lineNumberWidth;
        }
        
        private void tabSourceText_SelectionChanged(object sender, EventArgs e)
        {
            Scintilla tabSourceText = (Scintilla)sender;
            // DEACTIVATE MULTILINE SELECTION BECAUSE MULTILINE EDIT IS NOT SUPPORTED
            if (tabSourceText.Selection.IsRectangle)
            {
                tabSourceText.Selection.Range = new Range(
                    tabSourceText.Selection.Start,
                    tabSourceText.Selection.End ,
                    tabSourceText);
            }
        }

        private void tabSourceText_DragOver(object sender, DragEventArgs e)
        {
            Scintilla tabSourceText = (Scintilla)sender;

            // convert cursor position to text position
            Point point = new Point(e.X, e.Y);
            point = tabSourceText.PointToClient(point);
            int pos = tabSourceText.PositionFromPoint(point.X, point.Y);

            // refresh text control
            tabSourceText.Refresh();

            // is draging possible
            if (tabSourceText.GetRange(tabSourceText.Caret.Position, tabSourceText.Caret.Anchor)
                .IntersectsWith(tabSourceText.GetRange(pos)))
            {
                // if not show "NO" cursor
                e.Effect = DragDropEffects.None;
                return;
            }

            // draw line at cursor position in text
            var g = tabSourceText.CreateGraphics();
            var pen = new Pen(Color.Black, 2);
            var height = TextRenderer.MeasureText("0", tabSourceText.Font).Height;
            point.X = tabSourceText.PointXFromPosition(pos);
            point.Y = tabSourceText.PointYFromPosition(pos);
            g.DrawLine(pen, point.X, point.Y, point.X, point.Y + height);

            // show "MOVE" cursor
            e.Effect = DragDropEffects.Move;
        }

        private void tabSourceText_DragDrop(object sender, DragEventArgs e)
        {
            Scintilla tabSourceText = (Scintilla)sender;

            // convert cursor position to text position
            Point point = new Point(e.X, e.Y);
            point = tabSourceText.PointToClient(point);
            int pos = tabSourceText.PositionFromPoint(point.X, point.Y);

            // is dropping possible
            if (tabSourceText.GetRange(tabSourceText.Caret.Position, tabSourceText.Caret.Anchor)
                .IntersectsWith(tabSourceText.GetRange(pos)))
                return;
            
            // adjust caret position if necessary
            if (pos > tabSourceText.Caret.Position)
                pos -= Math.Abs(tabSourceText.Caret.Anchor - tabSourceText.Caret.Position);
            // cut the selected text to the clipboard
            tabSourceText.Clipboard.Cut();
            // move caret to the insert position
            tabSourceText.Caret.Position = pos;
            tabSourceText.Caret.Anchor = pos;
            // insert cut text from clipboard
            tabSourceText.Clipboard.Paste();
        }
        #endregion
        
        #region UTIL
        private void Render()
        {
            glControl.MakeCurrent();
            
            foreach (var c in classes)
                if (c.Value.GetType() == typeof(GLTech))
                    ((GLTech)c.Value).Exec(
                        glControl.ClientSize.Width,
                        glControl.ClientSize.Height);

            glControl.SwapBuffers();
        }

        private void ClearGLObjects()
        {
            // call delete method of OpenGL resources
            foreach (var pair in classes)
                pair.Value.Delete();
            // clear list of classes
            classes.Clear();
            // add default OpenTK glControl
            classes.Add(GraphicControl.nullname, new GraphicControl(glControl));
        }

        private void SaveTabPage(TabPage tabPage, bool newfile)
        {
            var selectedTabPageText = (Scintilla)tabPage.Controls[0];

            if (tabPage.filepath == null || newfile)
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
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
            TabPage tabSourcePage = new TabPage(path);
            Scintilla tabSourcePageText = new Scintilla();

            // tabSourcePageText
            tabSourcePageText.BorderStyle = BorderStyle.None;
            tabSourcePageText.ConfigurationManager.CustomLocation = "../res/syntax.xml";
            tabSourcePageText.ConfigurationManager.Language = "cpp";
            tabSourcePageText.Dock = DockStyle.Fill;
            tabSourcePageText.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabSourcePageText.Location = new Point(0, 0);
            tabSourcePageText.Margin = new Padding(0);
            tabSourcePageText.TabIndex = 0;
            tabSourcePageText.Text = text;
            tabSourcePageText.TextChanged += new EventHandler(this.tabSourceText_TextChanged);
            tabSourcePageText.SelectionChanged += new EventHandler(this.tabSourceText_SelectionChanged);
            // enable drag&drop
            tabSourcePageText.AllowDrop = true;
            tabSourcePageText.DragOver += new DragEventHandler(this.tabSourceText_DragOver);
            tabSourcePageText.DragDrop += new DragEventHandler(this.tabSourceText_DragDrop);
            // enable code folding
            tabSourcePageText.Folding.IsEnabled = true;
            tabSourcePageText.Margins[2].Type = MarginType.Symbol;
            tabSourcePageText.Margins[2].Width = 20;
            // display line numbers
            var lineNumberLength = tabSourcePageText.Lines.Count.ToString().Length;
            var lineNumberWidth = TextRenderer.MeasureText(new string('9', lineNumberLength), tabSourcePageText.Font).Width;
            tabSourcePageText.Margins[0].Width = lineNumberWidth;

            // tabSourcePage
            tabSourcePage.Controls.Add(tabSourcePageText);
            tabSourcePage.Location = new Point(4, 31);
            tabSourcePage.Margin = new Padding(0);
            tabSourcePage.Padding = new Padding(3);
            tabSourcePage.TabIndex = 0;
            tabSourcePage.Text = filename;

            // add tab
            this.tabSource.Controls.Add(tabSourcePage);

            tabSourcePageText.UndoRedo.EmptyUndoBuffer();
        }
        
        private Bitmap GetTextureLayer(GLImage img, int layer)
        {
            glControl.MakeCurrent();
            var bmp = img.Read(layer);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }
        #endregion
    }
}
