using System;
using System.Collections.Generic;

namespace App
{
    class GLText : GLObject
    {
        public string Text { get; private set; }

        public GLText(object @params)
            : this(@params.GetInstanceField<Compiler.Block>(),
                   @params.GetInstanceField<Dictionary<string, object>>())
        {
        }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLText(Compiler.Block block, Dictionary<string, object> scene)
            : base(block.Name, block.Anno)
        {
            Text = block.Body;
        }
    }
}
