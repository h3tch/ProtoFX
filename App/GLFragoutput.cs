using System.Collections.Generic;

namespace gled
{
    class GLFragoutput : GLObject
    {
        public GLFragoutput(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {

        }

        public override void Delete()
        {
        }
    }
}
