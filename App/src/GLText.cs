namespace App
{
    class GLText : GLObject
    {
        public string text { get; private set; }

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="dir">Directory of the tech-file.</param>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="anno">Annotation used for special initialization.</param>
        /// <param name="text">Text block specifying the object commands.</param>
        /// <param name="classes">Collection of scene objects.</param>
        public GLText(string dir, string name, string anno, string text, Dict<GLObject> classes)
            : base(name, anno)
        {
            this.text = text;
        }

        public override void Delete() { }
    }
}
