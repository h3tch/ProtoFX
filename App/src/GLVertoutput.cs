using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace App
{
    class GLVertoutput : GLObject
    {
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
            {
                // skip if already processed
                if (cmd == null || cmd.Length < 2)
                    continue;
                if (cmd[0] == "buff")
                {
                    // get buffer
                    GLBuffer buf = classes.FindClass<GLBuffer>(cmd[1]);
                    if (buf == null)
                        throw new Exception(Dict.NotFoundMsg("vertoutput", name, "buffer", cmd[1]));

                    // parse offset
                    int offset = 0;
                    if (cmd.Length >= 3 && int.TryParse(cmd[2], out offset) == false)
                        throw new Exception("ERROR in sampler " + name + ": "
                            + "The second parameter (offset) of buff" + numbindings + " is invalid.");

                    // parse size
                    int size = buf.size;
                    if (cmd.Length >= 4 && int.TryParse(cmd[3], out size) == false)
                        throw new Exception("ERROR in sampler " + name + ": "
                            + "The third parameter (size) of buff" + numbindings + " is invalid.");

                    // bind buffer to transform feedback
                    GL.BindBufferRange(BufferRangeTarget.TransformFeedbackBuffer, numbindings++, buf.glname, (IntPtr)offset, (IntPtr)size);
                }
            }

            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, 0);
            throwExceptionOnOpenGlError("vertinput", name, "could not create OpenGL vertex array object");
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTransformFeedback(glname);
                glname = 0;
            }
        }
    }
}
