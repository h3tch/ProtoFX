using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;

namespace App
{
    class GLVertinput : GLObject
    {
        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLVertinput(GLParams @params) : base(@params)
        {
            var err = new CompileException($"vertinput '{@params.name}'");

            // PARSE TEXT
            var body = new Commands(@params.text, @params.file, @params.cmdLine, @params.cmdPos, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenVertexArray();
            GL.BindVertexArray(glname);

            int numAttr = 0;
            foreach (var cmd in body["attr"])
                Attach(err + $"command {cmd.idx} 'attr'", numAttr++, cmd, @params.name, @params.scene);

            // if errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // unbind object and check for errors
            GL.BindVertexArray(0);
            if (HasErrorOrGlError(err, @params.file, @params.nameLine, @params.namePos))
                throw err;
        }

        private void Attach(CompileException err, int attrIdx, Commands.Cmd cmd, string name, Dict<GLObject> classes)
        {
            // check commands for errors
            if (cmd.args.Length < 3)
            {
                err.Add("Command attr needs at least 3 attributes (e.g. 'attr buff_name float 4')",
                    cmd.file, cmd.line, cmd.pos);
                return;
            }

            // parse command arguments
            string buffname = cmd.args[0];
            string typename = cmd.args[1];
            int length  = int.Parse(cmd.args[2]);
            int stride  = cmd.args.Length > 3 ? int.Parse(cmd.args[3]) : 0;
            int offset  = cmd.args.Length > 4 ? int.Parse(cmd.args[4]) : 0;
            int divisor = cmd.args.Length > 5 ? int.Parse(cmd.args[5]) : 0;
            
            GLBuffer buff;
            if (classes.TryGetValue(buffname, out buff, cmd.file, cmd.line, cmd.pos, err) == false)
            {
                err.Add($"Buffer '{buffname}' could not be found.", cmd.file, cmd.line, cmd.pos);
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
                err.Add($"Type '{typename}' is not supported.", cmd.file, cmd.line, cmd.pos);
            
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
