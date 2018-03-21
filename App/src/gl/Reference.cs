namespace protofx.gl
{
    class Reference : Object
    {
        public object Ref { get; }
        
        /// <summary>
        /// Create a reference to another object.
        /// </summary>
        /// <param name="name">Name of the reference object.</param>
        /// <param name="anno">Reference object annotation.</param>
        /// <param name="reference">Object to be referenced.</param>
        public Reference(string name, string anno, object reference) : base(name, anno)
        {
            Ref = reference;
        }
    }
}
