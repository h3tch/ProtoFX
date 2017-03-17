using OpenTK.Graphics.OpenGL4;
using System;
using PrimitiveType = OpenTK.Graphics.OpenGL4.TransformFeedbackPrimitiveType;

namespace App
{
    class GLVertoutput : GLObject
    {
        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLVertoutput(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"vertoutput '{Name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenTransformFeedback();
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, glname);

            // parse commands
            int numbindings = 0;
            foreach (var cmd in block["buff"])
                Attach(numbindings++, cmd, scene, err | $"command '{cmd.Text}'");

            // if errors occurred throw exception
            if (err.HasErrors)
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
        public void Bind(PrimitiveType primitive, bool resume)
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
        public static void Unbind(bool pause)
        {
            // pause or end transform feedback
            if (pause)
                GL.PauseTransformFeedback();
            else
                GL.EndTransformFeedback();
            // undbind transform feedback
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, 0);
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            if (glname > 0)
            {
                GL.DeleteTransformFeedback(glname);
                glname = 0;
            }
        }

        /// <summary>
        /// Parse command line and attach the buffer object
        /// to the specified unit (output stream).
        /// </summary>
        /// <param name="unit">Vertex output stream unit.</param>
        /// <param name="cmd">Command line to process.</param>
        /// <param name="scene">Dictionary of scene objects.</param>
        /// <param name="err">Compiler exception collector.</param>
        private void Attach(int unit, Compiler.Command cmd, Dict scene, CompileException err)
        {
            if (cmd.ArgCount == 0)
            {
                err.Error("Command buff needs at least one attribute (e.g. 'buff buff_name')", cmd);
                return;
            }

            // get buffer
            var buf = scene.GetValueOrDefault<GLBuffer>(cmd[0].Text);
            if (buf == null)
            {
                err.Error($"The name '{cmd[0]}' does not reference an object of type 'buffer'.", cmd);
                return;
            }

            // parse offset
            int offset = 0;
            if (cmd.ArgCount > 1 && int.TryParse(cmd[1].Text, out offset) == false)
            {
                err.Error($"The second parameter (offset) of buff {unit} is invalid.", cmd);
                return;
            }

            // parse size
            int size = buf.Size;
            if (cmd.ArgCount > 2 && int.TryParse(cmd[2].Text, out size) == false)
            {
                err.Error($"The third parameter (size) of buff {unit} is invalid.", cmd);
                return;
            }

            // bind buffer to transform feedback
            GL.BindBufferRange(BufferRangeTarget.TransformFeedbackBuffer,
                unit, buf.glname, (IntPtr)offset, (IntPtr)size);
        }
    }
}
