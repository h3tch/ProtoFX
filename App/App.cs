using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace gled
{
    public partial class App : Form
    {
        private Dictionary<string, GLObject> classes = new Dictionary<string, GLObject>();
        private GLCamera camera = new GLCamera();

        public App()
        {
            InitializeComponent();
            this.codeText.Text = System.IO.File.ReadAllText(@"../../samples/simple.txt");
        }

        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeleteClasses();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.SkyBlue);
        }

        private void glControl_Resize(object sender, EventArgs e)
        {

        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            glControl.SwapBuffers();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            this.codeError.Text = "";
            DeleteClasses();

            // remove comments
            var code = removeComments(this.codeText.Text, "//");

            // find GLST class blocks (find "TYPE name { ... }")
            var blocks = findBlocks(code);

            // parse arguments
            for (int i = 0; i < blocks.Length; i++)
            {
                // PARSE CLASS INFO
                string[] classInfo = findClassDef(blocks[i]);

                // PARSE CLASS TEXT
                var start = blocks[i].IndexOf('{');
                string classText = blocks[i].Substring(start + 1, blocks[i].LastIndexOf('}') - start - 1);

                // GET CLASS TYPE, ANNOTATION AND NAME
                var classType = "OpenTKTurorial02.GL"
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
                        throw new Exception("ERROR in " + classInfo[0] + " " + className + ": Class type '" + classInfo[0] + "' not known.");
                    if (this.classes.ContainsKey(className))
                        throw new Exception("ERROR in " + classInfo[0] + " " + className + ": Class name '" + className + "' already exists.");
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
        }

        private void DeleteClasses()
        {
            foreach (var pair in classes)
                pair.Value.Delete();
            classes.Clear();
            classes.Add(GLCamera.cameraname, camera);
        }

        private static string removeComments(string code, string linecomment)
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

        private static string[] findBlocks(string code)
        {
            var matches = Regex.Matches(code, "(\\w+\\s*){2,3}\\{");

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

            List<string> blocks = new List<string>();
            for (int i = 0; i < matches.Count; i++)
            {
                int idx = blockBr.IndexOf(matches[i].Index + matches[i].Length - 1);
                if (idx >= 0)
                    blocks.Add(code.Substring(matches[i].Index, blockBr[idx + 1] - matches[i].Index + 1));
            }
            return blocks.ToArray();
        }

        private static string[] findClassDef(string classblock)
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
    }
}
