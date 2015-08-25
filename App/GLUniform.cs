using System.Collections.Generic;

namespace gled
{
    class GLUniform : GLObject
    {
        public GLUniform(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {

        }

        public void Bind(int unit)
        {

        }

        public void Unbind(int unit)
        {

        }

        public override void Delete()
        {
        }
    }
}
