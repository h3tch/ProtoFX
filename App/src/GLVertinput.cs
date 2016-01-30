using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;

namespace App
{
    class GLVertinput : GLObject
    {
        public GLVertinput(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"vertinput '{name}'");

            // CREATE OPENGL OBJECT
            glname = GL.GenVertexArray();
            GL.BindVertexArray(glname);

            int numAttr = 0;
            foreach (var cmd in block["attr"])
                Attach(err + $"command '{cmd.Text}'", numAttr++, cmd, name, scene);

            // if errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // unbind object and check for errors
            GL.BindVertexArray(0);
            if (HasErrorOrGlError(err, block))
                throw err;
        }
        
        private void Attach(CompileException err, int attrIdx, Compiler.Command cmd, string name, Dict classes)
        {
            // check commands for errors
            if (cmd.ArgCount < 3)
            {
                err.Add("Command attr needs at least 3 attributes (e.g. 'attr buff_name float 4')", cmd);
                return;
            }

            // parse command arguments
            string buffname = cmd[0].Text;
            string typename = cmd[1].Text;
            int length = int.Parse(cmd[2].Text);
            int stride = cmd.ArgCount > 3 ? int.Parse(cmd[3].Text) : 0;
            int offset = cmd.ArgCount > 4 ? int.Parse(cmd[4].Text) : 0;
            int divisor = cmd.ArgCount > 5 ? int.Parse(cmd[5].Text) : 0;

            GLBuffer buff;
            if (classes.TryGetValue(buffname, out buff, cmd, err) == false)
            {
                err.Add($"Buffer '{buffname}' could not be found.", cmd);
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
                err.Add($"Type '{typename}' is not supported.", cmd);

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
