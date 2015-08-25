using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gled
{
    class GLTech : GLObject
    {
        private List<GLPass> passes = new List<GLPass>();

        public GLTech(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            foreach (var arg in args)
            {
                if (!arg[0].Equals("pass"))
                    continue;

                GLObject pass;
                if (!classes.TryGetValue(arg[1], out pass) || pass.GetType() != typeof(GLPass))
                    throw new Exception("is not a pass object");

                passes.Add((GLPass)pass);
            }
        }

        public void Exec(int width, int height)
        {
            foreach (var pass in passes)
                pass.Exec(width, height);
        }

        public override void Delete()
        {
        }
    }
}
