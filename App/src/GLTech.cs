using System.Collections.Generic;

namespace App
{
    class GLTech : GLObject
    {
        private List<GLPass> passes = new List<GLPass>();

        public GLTech(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            var err = new GLException();
            err.PushCall("tech '" + name + "'");

            // PARSE TEXT
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS
            GLPass pass;
            for (int i = 0; i < cmds.Length; i++)
            {
                // NEEDS TO BE A pass COMMAND
                var cmd = cmds[i];
                if (!cmd[0].Equals("pass"))
                    continue;

                err.PushCall("command " + i + " '" + cmd[0] + "'");

                // find pass object
                if (classes.TryFindClass(cmd[1], out pass, err))
                    passes.Add(pass);

                err.PopCall();
            }

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;
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
