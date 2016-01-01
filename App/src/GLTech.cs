using System.Collections.Generic;

namespace App
{
    class GLTech : GLObject
    {
        private List<GLPass> passes = new List<GLPass>();

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLTech(GLParams @params) : base(@params)
        {
            var err = new CompileException($"tech '{@params.name}'");

            // PARSE TEXT
            var body = new Commands(@params.text, err);

            // PARSE COMMANDS
            GLPass pass;
            foreach (var cmd in body["pass"])
                if (@params.scene.TryGetValue(cmd.args[0], out pass, err + $"command {cmd.idx} 'pass'"))
                    passes.Add(pass);

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;
        }

        public void Exec(int width, int height, int frame)
            => passes.ForEach(x => x.Exec(width, height, frame));

        public override void Delete() { }
    }
}
