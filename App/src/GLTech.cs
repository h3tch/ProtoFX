﻿using System.Collections.Generic;
using System.Diagnostics;

namespace App
{
    class GLTech : FXPerf
    {
        #region FIELDS

        private List<GLPass> init = new List<GLPass>();
        private List<GLPass> passes = new List<GLPass>();
        private List<GLPass> uninit = new List<GLPass>();
        private Stopwatch clock = Stopwatch.StartNew();
        private double freq = 1.0 / Stopwatch.Frequency;

        #endregion

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
            ParsePasses(ref uninit, block, scene, err);

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
            // when executed the first time, execute initialization passes
            if (init != null)
            {
                init.ForEach(x => x.Exec(width, height, frame));
                init = null;
            }

            // execute all passes
            passes.ForEach(x => x.Exec(width, height, frame));

            // measure elapsed time
            timings.Push((float)(clock.Elapsed.Ticks * freq));
            frames.Push(frame);

            // restart timer
            clock.Restart();
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete() => uninit.ForEach(x => x.Exec(0, 0, -1));

        /// <summary>
        /// Parse commands in block.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        private void ParsePasses(ref List<GLPass> list, Compiler.Block block, Dict scene,
            CompileException err)
        {
            GLPass pass;
            var cmdName = ReferenceEquals(list, init)
                ? "init" : ReferenceEquals(list, passes) ? "pass" : "uninit";
            foreach (var cmd in block[cmdName])
                if (scene.TryGetValue(cmd[0].Text, out pass, block, err | $"command '{cmd.Text}'"))
                    list.Add(pass);
        }
    }
}
