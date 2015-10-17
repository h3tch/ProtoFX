using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace App
{
    class GLVertinput : GLObject
    {
        public GLVertinput(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            var err = new GLException();
            err.PushCall("vertinput '" + name + "'");

            // PARSE TEXT
            var cmds = Text2Cmds(text);

            // CREATE OPENGL OBJECT
            glname = GL.GenVertexArray();
            GL.BindVertexArray(glname);

            for (int i = 0; i < cmds.Length; i++)
            {
                var cmd = cmds[i];

                // ignore already parsed commands
                if (cmd == null || cmd.Length < 2)
                    continue;

                // attach buffer
                err.PushCall("command " + (i + 1) + " '" + cmd[0] + "'");
                attach(err, i, cmd, name, classes);
                err.PopCall();
            }

            // if errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // unbind object and check for errors
            GL.BindVertexArray(0);
            if (GL.GetError() != ErrorCode.NoError)
                err.Throw("OpenGL error '" + GL.GetError()
                    + "' occurred during vertex input object creation.");
        }

        private void attach(GLException err, int attrIdx, string[] args, string name, Dict classes)
        {
            // check commands for errors
            if (!args[0].Equals("attr"))
            {
                err.Add("Command '" + args[0] + "' not supported.");
                return;
            }
            if (args.Length < 4)
            {
                err.Add("Command attr needs at least 3 attributes (e.g. 'attr buff_name float 4')");
                return;
            }

            // parse command arguments
            string buffname = args[1];
            string typename = args[2];
            int length  = int.Parse(args[3]);
            int stride  = args.Length > 4 ? int.Parse(args[4]) : 0;
            int offset  = args.Length > 5 ? int.Parse(args[5]) : 0;
            int divisor = args.Length > 6 ? int.Parse(args[6]) : 0;
            
            GLBuffer buff;
            if (classes.TryFindClass(buffname, out buff, err) == false)
            {
                err.Add("Buffer '" + buffname + "' could not be found.");
                return;
            }

            // enable vertex array attribute
            GL.BindBuffer(BufferTarget.ArrayBuffer, buff.glname);
            GL.EnableVertexAttribArray(attrIdx);

            // bind buffer to vertex array attribute
            VertexAttribIntegerType typei;
            VertexAttribPointerType typef;
            if (Enum.TryParse(typename, true, out typei))
                GL.VertexAttribIPointer(attrIdx, length, typei, stride, (IntPtr)offset);
            else if (Enum.TryParse(typename, true, out typef))
                GL.VertexAttribPointer(attrIdx, length, typef, false, stride, offset);
            else
                err.Add("Type '" + typename + "' is not supported.");
            
            if (divisor > 0)
                GL.VertexAttribDivisor(attrIdx, divisor);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteVertexArray(glname);
                glname = 0;
            }
        }
    }
}
