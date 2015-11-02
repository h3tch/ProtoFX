namespace App
{
    class GLText : GLObject
    {
        public string text { get; private set; }

        public GLText(string dir, string name, string annotation, string text, Dict<GLObject> classes)
            : base(name, annotation)
        {
            this.text = text;
        }

        public override void Delete() { }
    }
}
