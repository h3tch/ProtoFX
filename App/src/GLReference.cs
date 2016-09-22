namespace App
{
    class GLReference : GLObject
    {
        public object reference { get; }
        
        /// <summary>
        /// Create a reference to another object.
        /// </summary>
        /// <param name="name">Name of the reference object.</param>
        /// <param name="anno">Reference object annotation.</param>
        /// <param name="reference">Object to be referenced.</param>
        public GLReference(string name, string anno, object reference) : base(name, anno)
        {
            this.reference = reference;
        }
    }
}
