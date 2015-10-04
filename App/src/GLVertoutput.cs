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
            ErrorCollector err = new ErrorCollector();
            err.PushStack("vertoutput '" + name + "'");

            // PARSE TEXT
            var cmds = Text2Cmds(text);

            // PARSE ARGUMENTS
            Cmds2Fields(this, ref cmds);

            // CREATE OPENGL OBJECT
            glname = GL.GenTransformFeedback();
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, glname);

            // parse commands
            int numbindings = 0;
            for (int i = 0; i < cmds.Length; i++)
            {
                var cmd = cmds[i];
                err.PushStack("command " + (i + 1));
                if (cmd != null && cmd.Length >= 2 && cmd[0] == "buff")
                    attachBuffer(err, numbindings++, cmd, classes);
                err.PopStack();
            }
            if (err.HasErrors())
                err.ThrowExeption();

            // unbind object and check for errors
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, 0);
            if (GL.GetError() != ErrorCode.NoError)
                err.Throw("OpenGL error '" + GL.GetError()
                    + "' occurred during vertex output object creation.");
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

        private void attachBuffer(ErrorCollector err, int unit, string[] cmd, Dict classes)
        {
            // get buffer
            GLBuffer buf = classes.FindClass<GLBuffer>(cmd[1]);
            if (buf == null)
            {
                err.Add("The name '" + cmd[1] + "' does not reference an object of type 'buffer'.");
                return;
            }

            // parse offset
            int offset = 0;
            if (cmd.Length >= 3 && int.TryParse(cmd[2], out offset) == false)
            {
                err.Add("The second parameter (offset) of buff " + unit + " is invalid.");
                return;
            }

            // parse size
            int size = buf.size;
            if (cmd.Length >= 4 && int.TryParse(cmd[3], out size) == false)
            {
                err.Add("The third parameter (size) of buff" + unit + " is invalid.");
                return;
            }

            // bind buffer to transform feedback
            GL.BindBufferRange(BufferRangeTarget.TransformFeedbackBuffer,
                unit, buf.glname, (IntPtr)offset, (IntPtr)size);
        }
    }
}
