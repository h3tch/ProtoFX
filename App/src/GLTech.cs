using System.Collections.Generic;

namespace App
{
    class GLTech : GLObject
    {
        private List<GLPass> passes = new List<GLPass>();

        public GLTech(Compiler.Block block, Dict<GLObject> scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"tech '{name}'");

            // PARSE COMMANDS
            GLPass pass;
            foreach (var cmd in block["pass"])
                if (scene.TryGetValue(cmd[0].Text, out pass, block, err + $"command '{cmd.Text}'"))
                    passes.Add(pass);

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;
        }

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLTech(GLParams @params) : base(@params)
        {
            var err = new CompileException($"tech '{@params.name}'");

            // PARSE TEXT
            var body = new Commands(@params.text, @params.file, @params.cmdLine, @params.cmdPos, err);

            // PARSE COMMANDS
            GLPass pass;
            foreach (var cmd in body["pass"])
                if (@params.scene.TryGetValue(cmd.args[0], out pass,
                    @params.file, @params.nameLine, @params.namePos,
                    err + $"command {cmd.idx} 'pass'"))
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
