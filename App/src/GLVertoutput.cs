using OpenTK.Graphics.OpenGL4;
using System;
using PrimitiveType = OpenTK.Graphics.OpenGL4.TransformFeedbackPrimitiveType;

namespace App
{
    class GLVertoutput : GLObject
    {
        #region FIELDS
        public bool pause = false;
        public bool resume = false;
        #endregion

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLVertoutput(GLParams @params) : base(@params)
        {
            var err = new CompileException($"vertoutput '{@params.name}'");

            // PARSE TEXT
            var body = new Commands(@params.cmdText, @params.cmdPos, err);

            // PARSE ARGUMENTS
            body.Cmds2Fields(this, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenTransformFeedback();
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, glname);

            // parse commands
            int numbindings = 0;
            foreach (var cmd in body["buff"])
                Attach(err + $"command {cmd.idx} 'buff'", numbindings++, cmd, @params.scene);

            // if errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // unbind object and check for errors
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, 0);
            if (HasErrorOrGlError(err, @params.namePos))
                throw err;
        }

        /// <summary>
        /// Bind transform feedback object.
        /// </summary>
        /// <param name="primitive">Transform feedback primitive type.</param>
        public void Bind(PrimitiveType primitive)
        {
            // bind transform feedback object
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, glname);
            // resume or start transform feedback
            if (resume)
                GL.ResumeTransformFeedback();
            else
                GL.BeginTransformFeedback(primitive);
        }

        /// <summary>
        /// Unbind transform feedback object.
        /// </summary>
        public void Unbind()
        {
            // pause or end transform feedback
            if (pause)
                GL.PauseTransformFeedback();
            else
                GL.EndTransformFeedback();
            // undbind transform feedback
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, 0);
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTransformFeedback(glname);
                glname = 0;
            }
        }

        private void Attach(CompileException err, int unit, Commands.Cmd cmd, Dict<GLObject> classes)
        {
            if (cmd.args.Length == 0)
            {
                err.Add("Command buff needs at least one attribute (e.g. 'buff buff_name')");
                return;
            }

            // get buffer
            GLBuffer buf = classes.GetValue<GLBuffer>(cmd.args[0]);
            if (buf == null)
            {
                err.Add($"The name '{cmd.args[0]}' does not reference an object of type 'buffer'.");
                return;
            }

            // parse offset
            int offset = 0;
            if (cmd.args.Length > 1 && int.TryParse(cmd.args[1], out offset) == false)
            {
                err.Add($"The second parameter (offset) of buff {unit} is invalid.");
                return;
            }

            // parse size
            int size = buf.size;
            if (cmd.args.Length > 2 && int.TryParse(cmd.args[2], out size) == false)
            {
                err.Add($"The third parameter (size) of buff {unit} is invalid.");
                return;
            }

            // bind buffer to transform feedback
            GL.BindBufferRange(BufferRangeTarget.TransformFeedbackBuffer,
                unit, buf.glname, (IntPtr)offset, (IntPtr)size);
        }
    }
}
