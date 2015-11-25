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
        public static string nullname = "__protogl_control__";
        private bool render = false;
        private Dict<GLObject> scene = new Dict<GLObject>();
        public Dictionary<string, GLObject> Scene { get { return scene; } }

        public GraphicControl() : base()
        {
            Paint += new PaintEventHandler(OnPaint);
            MouseDown += new MouseEventHandler(OnMouseDown);
            MouseMove += new MouseEventHandler(OnMouseMove);
            MouseUp += new MouseEventHandler(OnMouseUp);
            Resize += new EventHandler(OnResize);
        }

        public void Render()
        {
            MakeCurrent();
            scene.Where(x => x.Value is GLTech).Select(x => (GLTech)x.Value)
                .Do(x => x.Exec(ClientSize.Width, ClientSize.Height));
            SwapBuffers();
        }

        public void AddObject(string block, string includeDir)
        {
            // PARSE CLASS INFO
            string[] classDef = ExtraxtClassDef(block);
            if (classDef.Length < 2)
                throw new GLException(classDef[0]).Add("Invalid class definition.");

            // PARSE CLASS TEXT
            var start = block.IndexOf('{');
            var cmdStr = block.Substring(start + 1, block.LastIndexOf('}') - start - 1);

            // GET CLASS TYPE, ANNOTATION AND NAME
            var classType = "App.GL"
                + classDef[0].First().ToString().ToUpper()
                + classDef[0].Substring(1);
            var classAnno = classDef[classDef.Length - 2];
            var className = classDef[classDef.Length - 1];
            var type = Type.GetType(classType);

            // check for errors
            if (type == null)
                throw new GLException($"{classDef[0]} '{className}'")
                    .Add($"Class type '{classDef[0]}' not known.");
            if (scene.ContainsKey(className))
                throw new GLException($"{classDef[0]} '{className}'")
                    .Add($"Class name '{className}' already exists.");

            // instantiate class
            var instance = (GLObject)Activator.CreateInstance(
                type, includeDir, className, classAnno, cmdStr, scene);
            scene.Add(instance.name, instance);
        }

        public void ClearScene()
        {
            // call delete method of OpenGL resources
            scene.Do(x => x.Value.Delete());
            // clear list of classes
            scene.Clear();
            // add default OpenTK glControl
            scene.Add(nullname, new GLReference(nullname, null, this));
        }

        private static string[] ExtraxtClassDef(string objectblock)
        {
            // parse class info
            var lines = objectblock.Split(new char[] { '\n' });
            return lines.Select(x => Regex.Matches(x, "[\\w.]+")).Where(x => x.Count > 0)
                .First().Cast<Match>().Select(x => x.Value).ToArray();
        }

        private void OnResize(object sender, EventArgs e) => Render();

        private void OnPaint(object sender, PaintEventArgs e) => Render();

        private void OnMouseDown(object sender, MouseEventArgs e) => render = true;

        private void OnMouseUp(object sender, MouseEventArgs e) => render = false;

        private void OnMouseMove(object sender, MouseEventArgs e) => this.UseIf(render)?.Render();
    }
}
