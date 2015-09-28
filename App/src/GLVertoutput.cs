using OpenTK.Graphics.OpenGL4;
using System;

namespace App
{
    class GLVertoutput : GLObject
    {
        public bool pause = false;
        public bool resume = false;

        public GLVertoutput(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var cmds = Text2Cmds(text);

            // PARSE ARGUMENTS
            Cmds2Fields(this, ref cmds);

            // CREATE OPENGL OBJECT
            glname = GL.GenTransformFeedback();
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, glname);

            // parse commands
            int numbindings = 0;
            foreach (var cmd in cmds)
                if (cmd != null && cmd.Length >= 2 && cmd[0] == "buff")
                    attachBuffer(numbindings++, cmd, classes);

            // unbind object and check for errors
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, 0);
            throwExceptionOnOpenGlError("vertinput", name, "could not create OpenGL vertex array object");
        }

        public void Bind(TransformFeedbackPrimitiveType primitive)
        {
            // bind transform feedback object
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, glname);
            // resume or start transform feedback
            if (resume)
                GL.ResumeTransformFeedback();
            else
                GL.BeginTransformFeedback(primitive);
        }

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

        private void attachBuffer(int unit, string[] cmd, Dict classes)
        {
            // get buffer
            GLBuffer buf = classes.FindClass<GLBuffer>(cmd[1]);
            if (buf == null)
                throw new Exception(Dict.NotFoundMsg("vertoutput", name, "buffer", cmd[1]));

            // parse offset
            int offset = 0;
            if (cmd.Length >= 3 && int.TryParse(cmd[2], out offset) == false)
                throw new Exception("ERROR in sampler " + name + ": "
                    + "The second parameter (offset) of buff" + unit + " is invalid.");

            // parse size
            int size = buf.size;
            if (cmd.Length >= 4 && int.TryParse(cmd[3], out size) == false)
                throw new Exception("ERROR in sampler " + name + ": "
                    + "The third parameter (size) of buff" + unit + " is invalid.");

            // bind buffer to transform feedback
            GL.BindBufferRange(BufferRangeTarget.TransformFeedbackBuffer, unit, buf.glname, (IntPtr)offset, (IntPtr)size);
        }
    }
}
