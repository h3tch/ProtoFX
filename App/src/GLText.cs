namespace App
{
    class GLText : GLObject
    {
        public string text { get; private set; }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLText(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            text = block.Body;
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete() { }
    }
}
