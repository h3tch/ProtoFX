using System.Collections.Generic;

namespace App
{
    class GLTech : GLObject
    {
        private List<GLPass> passes = new List<GLPass>();

        public GLTech(string dir, string name, string annotation, string text, Dict<GLObject> classes)
            : base(name, annotation)
        {
            var err = new GLException($"tech '{name}'");

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
