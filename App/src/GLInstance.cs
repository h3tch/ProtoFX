namespace App.src
{
    class GLInstance : GLObject
    {
        public GLInstance(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            var err = new GLException();
            err.PushCall($"instance '{name}'");

            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);
        }

        public override void Delete() { }
    }
}
