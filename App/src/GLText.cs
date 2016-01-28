namespace App
{
    class GLText : GLObject
    {
        public string text { get; private set; }

        public GLText(Compiler.Block block, Dict<GLObject> scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            text = block.Text;
        }

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLText(GLParams @params) : base(@params)
        {
            text = @params.text;
        }

        public override void Delete() { }
    }
}
