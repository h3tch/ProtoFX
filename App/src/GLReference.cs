namespace App
{
    class GLReference : GLObject
    {
        public object reference { get; }

        /// <summary>
        /// Instantiate and initialize object.
        /// </summary>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="anno">Annotation used for special initialization.</param>
        /// <param name="reference">Reference to an object.</param>
        public GLReference(string name, string anno, object reference) : base(name, anno)
        {
            this.reference = reference;
        }

        public override void Delete()
        { }
    }
}
