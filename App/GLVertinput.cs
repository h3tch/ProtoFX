using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace gled
{
    class GLVertinput : GLObject
    {
        public string[] attr0 = null;
        public string[] attr1 = null;
        public string[] attr2 = null;
        public string[] attr3 = null;
        public string[] attr4 = null;
        public string[] attr5 = null;
        public string[] attr6 = null;
        public string[] attr7 = null;
        public string[] attr8 = null;
        public string[] attr9 = null;
        public string[] attr10 = null;
        public string[] attr11 = null;
        public string[] attr12 = null;
        public string[] attr13 = null;
        public string[] attr14 = null;
        public string[] attr15 = null;

        public GLVertinput(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, args);

            // CREATE OPENGL OBJECT
            glname = GL.GenVertexArray();
            GL.BindVertexArray(glname);

            for (int i = 0; i < 16; i++)
                enable(name, i, classes);

            GL.BindVertexArray(0);
            throwExceptionOnOpenGlError("vertinput", name, "could not create OpenGL vertex array object");
        }

        private void enable(string name, int attrIdx, Dictionary<string, GLObject> classes)
        {
            var field = this.GetType().GetField("attr" + attrIdx);
            string[] args = (string[])field.GetValue(this);
            if (args == null)
                return;
            if (args.Length < 3)
                throw new Exception("ERROR in vertinput " + name + ": attr" + attrIdx + " needs at least 3 attributes (e.g. 'attrX buff_name float 4')");

            GLObject buff;
            if (classes.TryGetValue(args[0], out buff) == false)
                throw new Exception("ERROR in vertinput " + name + " attr" + attrIdx + ": Buffer '" + args[0] + "' could not be found.");

            int length  = int.Parse(args[2]);
            int stride  = args.Length > 3 ? int.Parse(args[3]) : 0;
            int offset  = args.Length > 4 ? int.Parse(args[4]) : 0;
            int divisor = args.Length > 5 ? int.Parse(args[5]) : 0;

            GL.BindBuffer(BufferTarget.ArrayBuffer, buff.glname);
            GL.EnableVertexAttribArray(attrIdx);
            
            try
            {
                GL.VertexAttribIPointer(attrIdx, length, (VertexAttribIntegerType)Enum.Parse(typeof(VertexAttribIntegerType), args[1], true), stride, (IntPtr)offset);
            }
            catch
            {
                try
                {
                    GL.VertexAttribPointer(attrIdx, length, (VertexAttribPointerType)Enum.Parse(typeof(VertexAttribPointerType), args[1], true), false, stride, offset);
                }
                catch
                {
                    throw new Exception("ERROR in vertinput " + name + " attr" + attrIdx + ": Type '" + args[1] + "' is not supported.");
                }
            }


            if (divisor > 0)
                GL.VertexAttribDivisor(attrIdx, divisor);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public override void Bind(int unit)
        {
            GL.BindVertexArray(glname);
        }

        public override void Unbind(int unit)
        {
            GL.BindVertexArray(0);
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
