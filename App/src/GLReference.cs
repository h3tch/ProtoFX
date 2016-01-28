namespace App
{
    class GLReference : GLObject
    {
        public object reference { get; }
        
        public GLReference(Compiler.Block block, object reference) : base(block.Name, block.Anno)
        {
            this.reference = reference;
        }

        /// <summary>
        /// Instantiate and initialize object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        /// <param name="reference">Reference to an object.</param>
        public GLReference(GLParams @params, object reference) : base(@params)
        {
            this.reference = reference;
        }

        public override void Delete() { }
    }
}
