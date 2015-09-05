using System.Collections.Generic;

namespace gled
{
    class GLUniform : GLObject
    {
        public GLUniform(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {

        }

        public override void Delete()
        {
        }
    }
}
