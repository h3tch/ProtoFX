using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace App
{
    class GLVertinput : GLObject
    {
        public GLVertinput(string dir, string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            ErrorCollector err = new ErrorCollector();
            err.PushStack("vertinput '" + name + "'");

            // PARSE TEXT
            var args = Text2Cmds(text);

            // CREATE OPENGL OBJECT
            glname = GL.GenVertexArray();
            GL.BindVertexArray(glname);

            for (int i = 0; i < args.Length; i++)
            {
                err.PushStack("command " + i + 1);
                enable(err, i, args[i], name, classes);
                err.PopStack();
            }
            if (err.HasErrors())
                err.ThrowExeption();

            GL.BindVertexArray(0);
            if (GL.GetError() != ErrorCode.NoError)
                err.Throw("OpenGL error '" + GL.GetError()
                    + "' occurred during vertex input object creation.");
        }

        private void enable(ErrorCollector err, int attrIdx, string[] args, string name, Dictionary<string, GLObject> classes)
        {
            if (!args[0].Equals("attr"))
                return;
            if (args.Length < 4)
            {
                err.Add("Command attr needs at least 3 attributes (e.g. 'attr buff_name float 4')");
                return;
            }

            string bufname = args[1];
            string typename = args[2];
            int length  = int.Parse(args[3]);
            int stride  = args.Length > 4 ? int.Parse(args[4]) : 0;
            int offset  = args.Length > 5 ? int.Parse(args[5]) : 0;
            int divisor = args.Length > 6 ? int.Parse(args[6]) : 0;
            
            GLObject buff;
            if (classes.TryGetValue(bufname, out buff) == false || buff.GetType() != typeof(GLBuffer))
            {
                err.Add("Buffer '" + bufname + "' could not be found.");
                return;
            }
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, buff.glname);
            GL.EnableVertexAttribArray(attrIdx);

            VertexAttribIntegerType typei;
            VertexAttribPointerType typef;
            if (Enum.TryParse(typename, true, out typei))
                GL.VertexAttribIPointer(attrIdx, length, typei, stride, (IntPtr)offset);
            else if (Enum.TryParse(typename, true, out typef))
                GL.VertexAttribPointer(attrIdx, length, typef, false, stride, offset);
            else
            {
                err.Add("Type '" + typename + "' is not supported.");
                return;
            }
            
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
