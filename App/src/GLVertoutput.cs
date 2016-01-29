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
        public GLVertoutput(Compiler.Block block, Dict<GLObject> scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"vertoutput '{name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(this, block, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenTransformFeedback();
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, glname);

            // parse commands
            int numbindings = 0;
            foreach (var cmd in block["buff"])
                Attach(err + $"command '{cmd.Text}'", numbindings++, cmd, scene);

            // if errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // unbind object and check for errors
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, 0);
            if (HasErrorOrGlError(err, block))
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

        private void Attach(CompileException err, int unit, Compiler.Command cmd, Dict<GLObject> classes)
        {
            if (cmd.ArgCount == 0)
            {
                err.Add("Command buff needs at least one attribute (e.g. 'buff buff_name')", cmd);
                return;
            }

            // get buffer
            GLBuffer buf = classes.GetValue<GLBuffer>(cmd[0].Text);
            if (buf == null)
            {
                err.Add($"The name '{cmd[0]}' does not reference an object of type 'buffer'.", cmd);
                return;
            }

            // parse offset
            int offset = 0;
            if (cmd.ArgCount > 1 && int.TryParse(cmd[1].Text, out offset) == false)
            {
                err.Add($"The second parameter (offset) of buff {unit} is invalid.", cmd);
                return;
            }

            // parse size
            int size = buf.size;
            if (cmd.ArgCount > 2 && int.TryParse(cmd[2].Text, out size) == false)
            {
                err.Add($"The third parameter (size) of buff {unit} is invalid.", cmd);
                return;
            }

            // bind buffer to transform feedback
            GL.BindBufferRange(BufferRangeTarget.TransformFeedbackBuffer,
                unit, buf.glname, (IntPtr)offset, (IntPtr)size);
        }
    }
}
