using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenTK
{
    class GraphicControl : GLControl
    {
        public static string nullname = "__control__";
        private bool render = false;
        private int renderExceptions = 0;
        private DataGridView output;
        private Dict<GLObject> scene = new Dict<GLObject>();
        public Dictionary<string, GLObject> Scene { get { return scene; } }
        public int Frame { get; private set; } = 0;

        /// <summary>
        /// Instantiate and initialize graphics control based on OpenTK.
        /// </summary>
        public GraphicControl() : base()
        {
            Paint += new PaintEventHandler(HandlePaint);
            MouseDown += new MouseEventHandler(HandleMouseDown);
            MouseMove += new MouseEventHandler(HandleMouseMove);
            MouseUp += new MouseEventHandler(HandleMouseUp);
            Resize += new EventHandler(HandleResize);
            Load += new EventHandler(HandleLoad);
        }
        
        /// <summary>
        /// Render scene.
        /// </summary>
        public void Render()
        {
            // clear existing render exeptions
            if (renderExceptions > 0)
            {
                for (int i = 0; i < output.Rows.Count; i++)
                    if ((string)output.Rows[i].Cells[0].Value == "render")
                        output.Rows.RemoveAt(i--);
                renderExceptions = 0;
            }

            try
            {
                // render the scene
                MakeCurrent();
                scene.Where(x => x.Value is GLTech).Select(x => (GLTech)x.Value)
                     .Do(x => x.Exec(ClientSize.Width, ClientSize.Height, Frame));
                SwapBuffers();
            }
            catch (Exception ex)
            {
                // add exception to output
                ex = ex.InnerException != null ? ex.InnerException : ex;
                output.Rows.Add(new[] { "render", ex.Message });
                renderExceptions++;
            }

            // increase render frame
            Frame++;
        }

        /// <summary>
        /// Add a new object to the scene.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="incDir"></param>
        public void AddObject(string block, int pos, string incDir, bool debuging)
        {
            // PARSE CLASS INFO
            var classDef = ExtraxtClassDef(block);
            if (classDef.Length < 2)
                throw new CompileException(classDef[0]).Add("Invalid class definition.");

            // PARSE CLASS TEXT
            var cmdPos = block.IndexOf('{') + 1;
            var cmdStr = block.Substring(cmdPos, block.LastIndexOf('}') - cmdPos - 2);

            // GET CLASS TYPE, ANNOTATION AND NAME
            var typeStr = "App.GL"
                + classDef[0].First().ToString().ToUpper()
                + classDef[0].Substring(1);
            var anno = classDef[classDef.Length - 2];
            var name = classDef[classDef.Length - 1];
            var type = Type.GetType(typeStr);

            // check for errors
            if (type == null)
                throw new CompileException($"{classDef[0]} '{name}'")
                    .Add($"Class type '{classDef[0]}' not known.", pos);
            if (scene.ContainsKey(name))
                throw new CompileException($"{classDef[0]} '{name}'")
                    .Add($"Class name '{name}' already exists.", pos);

            // instantiate class
            var @params = new GLParams(name, anno, cmdStr, pos, pos + cmdPos, incDir, scene, debuging);
            var instance = (GLObject)Activator.CreateInstance(type, @params);
            scene.Add(instance.name, instance);
        }

        /// <summary>
        /// Clear all scene objects.
        /// </summary>
        public void ClearScene()
        {
            // call delete method of OpenGL resources
            scene.Do(x => x.Value.Delete());
            // clear list of classes
            scene.Clear();
            // add default OpenTK glControl
            scene.Add(nullname, new GLReference(new GLParams(nullname), this));
            // (re)initialize OpenGL/GLSL debugger
            GLDebugger.Initilize(scene);
        }

        private static string[] ExtraxtClassDef(string objectblock)
        {
            // parse class info
            var lines = objectblock.Split(new char[] { '\n' });
            return lines.Select(x => Regex.Matches(x, "[\\w.]+")).Where(x => x.Count > 0)
                .First().Cast<Match>().Select(x => x.Value).ToArray();
        }

        private void HandleLoad(object sender, EventArgs e)
            => output = (DataGridView)FindForm()?.Controls.Find("output", true).FirstOrDefault();

        private void HandleResize(object sender, EventArgs e) => Render();

        private void HandlePaint(object sender, PaintEventArgs e) => Render();

        private void HandleMouseDown(object sender, MouseEventArgs e) => render = true;

        private void HandleMouseUp(object sender, MouseEventArgs e) => render = false;

        private void HandleMouseMove(object sender, MouseEventArgs e) => this.UseIf(render)?.Render();
    }
}
