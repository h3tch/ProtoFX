using System.Collections.Generic;

namespace gled
{
    class GLScene : GLObject
    {
        public GLScene (string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {

        }

        public override void Bind(int unit)
        {

        }

        public override void Unbind(int unit)
        {

        }

        public override void Delete()
        {
        }
    }
}
