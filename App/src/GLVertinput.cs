using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;

namespace App
{
    class GLVertinput : GLObject
    {
        public GLVertinput(string dir, string name, string annotation, string text, Dict<GLObject> classes)
            : base(name, annotation)
        {
            var err = new CompileException($"vertinput '{name}'");

            // PARSE TEXT
            var body = new Commands(text, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenVertexArray();
            GL.BindVertexArray(glname);

            int numAttr = 0;
            foreach (var cmd in body["attr"])
                attach(err + $"command {cmd.idx} 'attr'", numAttr++, cmd.args, name, classes);

            // if errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // unbind object and check for errors
            GL.BindVertexArray(0);
            if (HasErrorOrGlError(err))
                throw err;
        }

        private void attach(CompileException err, int attrIdx, string[] args, string name, Dict<GLObject> classes)
        {
            // check commands for errors
            if (args.Length < 3)
            {
                err.Add("Command attr needs at least 3 attributes (e.g. 'attr buff_name float 4')");
                return;
            }

            // parse command arguments
            string buffname = args[0];
            string typename = args[1];
            int length  = int.Parse(args[2]);
            int stride  = args.Length > 3 ? int.Parse(args[3]) : 0;
            int offset  = args.Length > 4 ? int.Parse(args[4]) : 0;
            int divisor = args.Length > 5 ? int.Parse(args[5]) : 0;
            
            GLBuffer buff;
            if (classes.TryGetValue(buffname, out buff, err) == false)
            {
                err.Add($"Buffer '{buffname}' could not be found.");
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
                err.Add($"Type '{typename}' is not supported.");
            
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
