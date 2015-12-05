using System.Collections.Generic;

namespace App
{
    class GLTech : GLObject
    {
        private List<GLPass> passes = new List<GLPass>();

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="dir">Directory of the tech-file.</param>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="anno">Annotation used for special initialization.</param>
        /// <param name="text">Text block specifying the object commands.</param>
        /// <param name="classes">Collection of scene objects.</param>
        public GLTech(string dir, string name, string anno, string text, Dict<GLObject> classes)
            : base(name, anno)
        {
            var err = new CompileException($"tech '{name}'");

            // PARSE TEXT
            var body = new Commands(text, err);

            // PARSE COMMANDS
            GLPass pass;
            foreach (var cmd in body["pass"])
                if (classes.TryGetValue(cmd.args[0], out pass, err + $"command {cmd.idx} 'pass'"))
                    passes.Add(pass);

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;
        }

        public void Exec(int width, int height) => passes.ForEach(x => x.Exec(width, height));

        public override void Delete() { }
    }
}
