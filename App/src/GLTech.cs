using System;
using System.Collections.Generic;

namespace App
{
    class GLTech : GLObject
    {
        private List<GLPass> passes = new List<GLPass>();

        public GLTech(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            ErrorCollector err = new ErrorCollector();
            err.PushStack("tech '" + name + "'");

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

                err.PushStack("command " + i + " '" + name + "'");

                // find pass object
                if (classes.TryFindClass(err, cmd[1], out pass))
                    passes.Add(pass);
            }

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                err.ThrowExeption();
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
