namespace App
{
    class GLReference : GLObject
    {
        public object reference { get; }

        public GLReference(string name, string anno, object reference)
            : base(name, anno)
        {
            this.reference = reference;
        }

        public override void Delete()
        { }
    }
}
