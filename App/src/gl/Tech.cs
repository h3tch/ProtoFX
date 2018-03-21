using System;
using System.Collections.Generic;

namespace protofx.gl
{
    class Tech : FXPerf
    {
        #region FIELDS

        private List<Pass> init = new List<Pass>();
        private List<Pass> passes = new List<Pass>();
        private List<Pass> uninit = new List<Pass>();

        #endregion

        /// <summary>
        /// Generic constructor used to build the scene objects.
        /// </summary>
        /// <param name="params">A class containing all the parameters
        /// needed to instantiate the class. The GLTech class requires a
        /// <code>Compiler.Block</code> object of the respective part in the code,
        /// a <code>Dictionary&lt;string, object&gt;</code> object containing
        /// the scene objects and a <code>bool</code> value to enable the debugger.
        /// </param>
        public Tech(object @params)
            : this(@params.GetFieldValue<Compiler.Block>(),
                   @params.GetFieldValue<Dictionary<string, object>>(),
                   @params.GetFieldValue<bool>())
        {
        }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        private Tech(Compiler.Block block, Dictionary<string, object> scene, bool debugging)
            : base(block.Name, block.Anno, 309, debugging)
        {
            var err = new CompileException($"tech '{Name}'");

            // PARSE COMMANDS
            ParsePasses(ref init, block, scene, err);
            ParsePasses(ref passes, block, scene, err);
            ParsePasses(ref uninit, block, scene, err);

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors)
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
            // begin timer query
            MeasureTime();
            StartTimer(frame);

            // when executed the first time, execute initialization passes
            if (init != null)
            {
                init.ForEach(x => x.Exec(width, height, frame));
                init = null;
            }

            // execute all passes
            passes.ForEach(x => x.Exec(width, height, frame));

            // end timer query
            EndTimer();
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            uninit.ForEach(x => x.Exec(0, 0, -1));
        }

        /// <summary>
        /// Parse commands in block.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        private void ParsePasses(ref List<Pass> list, Compiler.Block block, Dictionary<string, object> scene,
            CompileException err)
        {
            var cmdName = ReferenceEquals(list, init)
                ? "init"
                : ReferenceEquals(list, passes) ? "pass" : "uninit";
            foreach (var cmd in block[cmdName])
            {
                if (scene.TryGetValue(cmd[0].Text, out Pass pass, block,
                        err | $"command '{cmd.Text}'"))
                    list.Add(pass);
            }
        }
    }
}
