using System.Collections.Generic;

namespace App
{
    class GLTech : GLObject
    {
        private List<GLPass> init = new List<GLPass>();
        private List<GLPass> passes = new List<GLPass>();

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLTech(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"tech '{name}'");

            // PARSE COMMANDS
            ParsePasses(ref init, block, scene, err);
            ParsePasses(ref passes, block, scene, err);

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="frame"></param>
        public void Exec(int width, int height, int frame)
        {
            // when executed the first time, execute init passes
            if (init != null)
            {
                init.ForEach(x => x.Exec(width, height, frame));
                init = null;
            }
            // execute all passes
            passes.ForEach(x => x.Exec(width, height, frame));
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete() { }

        private void ParsePasses(ref List<GLPass> list, Compiler.Block block, Dict scene,
            CompileException err)
        {
            GLPass pass;
            foreach (var cmd in block[ReferenceEquals(list, init) ? "init" : "pass"])
                if (scene.TryGetValue(cmd[0].Text, out pass, block, err | $"command '{cmd.Text}'"))
                    list.Add(pass);
        }
    }
}
