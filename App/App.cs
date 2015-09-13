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

namespace gled
{
    public partial class App : Form
    {
        private GLDict classes = new GLDict();
        private bool render = false;
        public static CultureInfo culture = new CultureInfo("en");

        public App()
        {
            InitializeComponent();

            // ADD LIB FOLDER TO ENVIRONMENT VARIABLE PATH
            // this is necessary for ScintillaNET
            var envPath = Environment.GetEnvironmentVariable("PATH");
            var dir = Directory.GetCurrentDirectory();
            var lib = dir + "\\..\\..\\lib";
            if (!envPath.Contains(lib))
                Environment.SetEnvironmentVariable("PATH", envPath + ";" + lib);

            // select 'float' as the default buffer value type
            this.comboBufType.SelectedIndex = 7;
        }

        #region EVENTS

        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeleteClasses();
        }

        private void App_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
                toolBtnRun_Click(sender, null);
        }

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
        
        private void comboImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureImg_Click(sender, e);
        }

        private void pictureImg_Click(object sender, EventArgs e)
        {
            if (this.comboImg.SelectedItem == null || this.comboImg.SelectedItem.GetType() != typeof(GLImage))
                return;
            glControl.MakeCurrent();
            Bitmap bmp = ((GLImage)this.comboImg.SelectedItem).Read(0);
            this.pictureImg.Image = bmp;
        }

        private void comboBuf_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dim;
            if (this.comboBuf.SelectedItem == null
                || this.comboBuf.SelectedItem.GetType() != typeof(GLBuffer)
                || int.TryParse(textBufDim.Text, out dim) == false)
                return;

            // gather needed info
            GLBuffer buf = (GLBuffer)this.comboBuf.SelectedItem;
            string type = (string)comboBufType.SelectedItem;
            dim = Math.Max(0, dim);

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

        private void textBufDim_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                comboBuf_SelectedIndexChanged(sender, null);
        }

        private void toolBtnNew_Click(object sender, EventArgs e)
        {
            AddSourceTab(null);
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
                        AddSourceTab(filename);
                }
            }
        }

        private void toolBtnRun_Click(object sender, EventArgs e)
        {
            this.codeError.Text = "";
            DeleteClasses();

            var selectedTabPage = this.tabSource.SelectedTab;
            var selectedTabPageText = (Scintilla)selectedTabPage.Controls[0];

            // remove comments
            var code = RemoveComments(selectedTabPageText.Text, "//");

            // find GLST class blocks (find "TYPE name { ... }")
            var blocks = FindBlocks(code);

            // parse commands for each class block
            for (int i = 0; i < blocks.Length; i++)
            {
                // PARSE CLASS INFO
                string[] classInfo = FindClassDef(blocks[i]);

                // PARSE CLASS TEXT
                var start = blocks[i].IndexOf('{');
                string classText = blocks[i].Substring(start + 1, blocks[i].LastIndexOf('}') - start - 1);

                // GET CLASS TYPE, ANNOTATION AND NAME
                var classType = "gled.GL"
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
                        type, className, classAnno, classText, this.classes));
                }
                catch (Exception ex)
                {
                    // show errors
                    this.codeError.AppendText(ex.GetBaseException().Message + '\n');
                }
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
            SaveTabPage((TabPage)this.tabSource.SelectedTab);
        }

        private void toolBtnSaveAll_Click(object sender, EventArgs e)
        {
            foreach (var tab in this.tabSource.TabPages)
                SaveTabPage((TabPage)tab);
        }

        #endregion

        #region CONTROL

        private void AddSourceTab(string path)
        {
            // load file
            string filename = path != null ? Path.GetFileName(path) : "unnamed.tech";
            string text = path != null ? File.ReadAllText(path) : "";

            // create new tab objects
            gled.TabPage tabSourcePage = new gled.TabPage(path);
            Scintilla tabSourcePageText = new Scintilla();

            // suspend layouts
            tabSourcePage.SuspendLayout();
            tabSourcePageText.SuspendLayout();
            this.tabSource.SuspendLayout();

            // tabSourcePageText
            tabSourcePageText.BorderStyle = BorderStyle.None;
            tabSourcePageText.ConfigurationManager.CustomLocation = "../../syntax.xml";
            tabSourcePageText.ConfigurationManager.Language = "cpp";
            tabSourcePageText.Dock = DockStyle.Fill;
            tabSourcePageText.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabSourcePageText.Location = new Point(0, 0);
            tabSourcePageText.Margin = new Padding(0);
            tabSourcePageText.Styles.BraceBad.FontName = "Verdana\0";
            tabSourcePageText.Styles.BraceBad.Size = 10F;
            tabSourcePageText.Styles.BraceLight.FontName = "Verdana\0";
            tabSourcePageText.Styles.BraceLight.Size = 10F;
            tabSourcePageText.Styles.ControlChar.FontName = "Verdana\0";
            tabSourcePageText.Styles.ControlChar.Size = 10F;
            tabSourcePageText.Styles.Default.BackColor = SystemColors.Window;
            tabSourcePageText.Styles.Default.FontName = "Verdana\0";
            tabSourcePageText.Styles.Default.Size = 10F;
            tabSourcePageText.Styles.IndentGuide.FontName = "Verdana\0";
            tabSourcePageText.Styles.IndentGuide.Size = 10F;
            tabSourcePageText.Styles.LastPredefined.FontName = "Verdana\0";
            tabSourcePageText.Styles.LastPredefined.Size = 10F;
            tabSourcePageText.Styles.LineNumber.FontName = "Verdana\0";
            tabSourcePageText.Styles.LineNumber.Size = 10F;
            tabSourcePageText.Styles.Max.FontName = "Verdana\0";
            tabSourcePageText.Styles.Max.Size = 10F;
            tabSourcePageText.TabIndex = 0;

            // tabSourcePage
            tabSourcePage.Controls.Add(tabSourcePageText);
            tabSourcePage.Location = new Point(4, 31);
            tabSourcePage.Margin = new Padding(0);
            tabSourcePage.Padding = new Padding(3);
            tabSourcePage.TabIndex = 0;
            tabSourcePage.Text = filename;

            // add tab
            this.tabSource.Controls.Add(tabSourcePage);

            // resume layouts
            tabSourcePageText.ResumeLayout(false);
            tabSourcePage.ResumeLayout(false);
            this.tabSource.ResumeLayout(false);

            tabSourcePageText.Text = text;
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

        private void DeleteClasses()
        {
            // call delete method of OpenGL resources
            foreach (var pair in classes)
                pair.Value.Delete();
            // clear list of classes
            classes.Clear();
            // add default OpenTK glControl
            classes.Add(GledControl.nullname, new GledControl(glControl));
        }

        private static string RemoveComments(string code, string linecomment)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            return Regex.Replace(code,
                blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
                me =>
                {
                    if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                        return me.Value.StartsWith("//") ? Environment.NewLine : "";
                    // Keep the literal strings
                    return me.Value;
                },
                RegexOptions.Singleline);
        }

        private static string[] FindBlocks(string code)
        {
            // find potential block positions
            var matches = Regex.Matches(code, "(\\w+\\s*){2,3}\\{");

            // find all '{' that potentially indicate a block
            int count = 0;
            int newline = 0;
            List<int> blockBr = new List<int>();
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == '\n')
                    newline++;
                if (code[i] == '{' && count++ == 0)
                    blockBr.Add(i);
                if (code[i] == '}' && --count == 0)
                    blockBr.Add(i);
                if (count < 0)
                    throw new Exception("FATAL ERROR in line " + newline + ": Unexpected occurrence of '}'.");
            }

            // where 'matches' and 'blockBr' are aligned we have a block
            List<string> blocks = new List<string>();
            for (int i = 0; i < matches.Count; i++)
            {
                int idx = blockBr.IndexOf(matches[i].Index + matches[i].Length - 1);
                if (idx >= 0)
                    blocks.Add(code.Substring(matches[i].Index, blockBr[idx + 1] - matches[i].Index + 1));
            }

            // return blocks as array
            return blocks.ToArray();
        }

        private static string[] FindClassDef(string classblock)
        {
            // parse class info
            MatchCollection matches = null;
            var lines = classblock.Split(new char[] { '\n' });
            for (int j = 0; j < lines.Length; j++)
                // ignore empty or invalid lines
                if ((matches = Regex.Matches(lines[j], "[\\w.]+")).Count > 0)
                    return matches.Cast<Match>().Select(m => m.Value).ToArray();
            // ill defined class block
            return null;
        }

        private Array ConvertData(byte[] data, string type, out Type T)
        {
            // convert data to specified type
            switch (type)
            {
                case "byte": T = typeof(byte); return ConvertData<byte>(data);
                case "short": T = typeof(short); return ConvertData<short>(data);
                case "ushort": T = typeof(ushort); return ConvertData<ushort>(data);
                case "int": T = typeof(int); return ConvertData<int>(data);
                case "uint": T = typeof(uint); return ConvertData<uint>(data);
                case "long": T = typeof(long); return ConvertData<long>(data);
                case "ulong": T = typeof(ulong); return ConvertData<ulong>(data);
                case "float": T = typeof(float); return ConvertData<float>(data);
                case "double": T = typeof(double); return ConvertData<double>(data);
            }

            throw new Exception("INTERNAL_ERROR: Could not convert buffer data to specified type.");
        }

        private Array ConvertData<T>(byte[] data)
        {
            // find method to convert the data
            var methods = from m in typeof(BitConverter).GetMethods()
                          where m.Name == "To" + typeof(T).Name
                          select m;
            if (methods.Count() == 0)
                return data;

            var method = methods.First();

            // allocate array
            int typesize = Marshal.SizeOf(typeof(T));
            Array rs = Array.CreateInstance(typeof(T), data.Length / typesize);

            // convert data
            for (int i = 0; i < rs.Length; i++)
                rs.SetValue(Convert.ChangeType(method.Invoke(null, new object[] { data, typesize * i }), typeof(T)), i);

            return rs;
        }

        private void SaveTabPage(TabPage tabPage)
        {
            var selectedTabPageText = (Scintilla)tabPage.Controls[0];

            if (tabPage.filepath == null)
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

        #endregion
    }
}
