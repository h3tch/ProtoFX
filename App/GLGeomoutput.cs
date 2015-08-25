using System.Collections.Generic;

namespace gled
{
    class GLGeomoutput : GLObject
    {
        public GLGeomoutput(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {

        }

        public override void Delete()
        {
        }
    }
}
