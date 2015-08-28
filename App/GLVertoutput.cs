using System.Collections.Generic;

namespace gled
{
    class GLVertoutput : GLObject
    {
        public GLVertoutput(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);
        }

        public override void Delete()
        {
        }
    }
}
