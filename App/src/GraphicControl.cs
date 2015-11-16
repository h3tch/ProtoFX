using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
    class GraphicControl : OpenTK.GLControl
    {
        public static string nullname = "__gled_control__";
        private bool render = false;
        private Dict<GLObject> classes = new Dict<GLObject>();
        public IEnumerable<KeyValuePair<string, GLObject>> Scene
        { get { return classes.AsEnumerable(); } }

        public GraphicControl() : base()
        {
            this.Paint += new PaintEventHandler(this.OnPaint);
            this.MouseDown += new MouseEventHandler(this.OnMouseDown);
            this.MouseMove += new MouseEventHandler(this.OnMouseMove);
            this.MouseUp += new MouseEventHandler(this.OnMouseUp);
            this.Resize += new EventHandler(this.OnResize);
        }

        private void OnResize(object sender, EventArgs e) => Render();

        private void OnPaint(object sender, PaintEventArgs e) => Render();

        private void OnMouseDown(object sender, MouseEventArgs e) => render = true;

        private void OnMouseUp(object sender, MouseEventArgs e) => render = false;

        private void OnMouseMove(object sender, MouseEventArgs e) => this.UseIf(render)?.Render();

        public void Render()
        {
            MakeCurrent();

            foreach (var c in classes)
                if (c.Value.GetType() == typeof(GLTech))
                    ((GLTech)c.Value).Exec(
                        ClientSize.Width,
                        ClientSize.Height);

            SwapBuffers();
        }

        public void AddObject(string block, string includeDir)
        {
            // PARSE CLASS INFO
            string[] classDef = GetObjectBlockClassDef(block);

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
                throw new GLException($"{classDef[0]} '{className}': "
                    + $"Class type '{classDef[0]}' not known.");
            if (classes.ContainsKey(className))
                throw new GLException($"{classDef[0]} '{className}': "
                    + $"Class name '{className}' already exists.");

            // instantiate class
            var instance = (GLObject)Activator.CreateInstance(
                type, includeDir, className, classAnno, cmdStr, classes);
            classes.Add(instance.name, instance);
        }

        public void ClearScene()
        {
            // call delete method of OpenGL resources
            foreach (var pair in classes)
                pair.Value.Delete();
            // clear list of classes
            classes.Clear();
            // add default OpenTK glControl
            classes.Add(nullname, new GLReference(nullname, null, this));
        }

        private static string[] GetObjectBlockClassDef(string objectblock)
        {
            // parse class info
            MatchCollection matches = null;
            var lines = objectblock.Split(new char[] { '\n' });
            for (int j = 0; j < lines.Length; j++)
                // ignore empty or invalid lines
                if ((matches = Regex.Matches(lines[j], "[\\w.]+")).Count > 0)
                    return matches.Cast<Match>().Select(m => m.Value).ToArray();
            // ill defined class block
            return null;
        }
    }
}
