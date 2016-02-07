﻿using System.Collections.Generic;

namespace App
{
    class GLTech : GLObject
    {
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
            GLPass pass;
            foreach (var cmd in block["pass"])
                if (scene.TryGetValue(cmd[0].Text, out pass, block, err + $"command '{cmd.Text}'"))
                    passes.Add(pass);

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;
        }
        
        public void Exec(int width, int height, int frame)
            => passes.ForEach(x => x.Exec(width, height, frame));

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete() { }
    }
}
