namespace App
{
    class GLText : GLObject
    {
        public string text { get; private set; }

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLText(GLParams @params) : base(@params)
        {
            text = @params.cmdText;
        }

        public override void Delete() { }
    }
}
