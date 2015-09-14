using System;
using System.Collections.Generic;

namespace gled
{
    class GLTech : GLObject
    {
        private List<GLPass> passes = new List<GLPass>();

        public GLTech(string dir, string name, string annotation, string text, GLDict classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var cmds = Text2Cmds(text);

            foreach (var cmd in cmds)
            {
                if (!cmd[0].Equals("pass"))
                    continue;

                GLPass pass = classes.FindClass<GLPass>(cmd[1]);
                if (pass == null)
                    throw new Exception(GLDict.NotFoundMsg("tech", name, "pass", cmd[1]));

                passes.Add(pass);
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
