using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace gled
{
    class GLVertoutput : GLObject
    {
        public GLVertoutput(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, ref args);

            // CREATE OPENGL OBJECT
            glname = GL.GenTransformFeedback();
            GL.BindTransformFeedback(TransformFeedbackTarget.TransformFeedback, glname);

            // parse commands
            GLObject obj;
            int numbindings = 0;
            foreach (var call in args)
            {
                // skip if already processed
                if (call == null || call.Length < 2)
                    continue;
                if (call[0] == "buff")
                {
                    // get buffer
                    GLBuffer buf = null;
                    if (classes.TryGetValue(call[1], out obj) == false || obj.GetType() != typeof(GLBuffer))
                        throw new Exception("ERROR in sampler " + name + ": "
                            + "The buffer name '" + call[1] + "' of buff" + numbindings + " is invalid.");
                    buf = (GLBuffer)obj;

                    // parse offset
                    int offset = 0;
                    if (call.Length >= 3 && int.TryParse(call[2], out offset) == false)
                        throw new Exception("ERROR in sampler " + name + ": "
                            + "The second parameter (offset) of buff" + numbindings + " is invalid.");

                    // parse size
                    int size = buf.size;
                    if (call.Length >= 4 && int.TryParse(call[3], out size) == false)
                        throw new Exception("ERROR in sampler " + name + ": "
                            + "The third parameter (size) of buff" + numbindings + " is invalid.");

                    // bind buffer to transform feedback
                    if (classes.TryGetValue(call[1], out obj) && obj.GetType() == typeof(GLBuffer))
                        GL.BindBufferRange(BufferRangeTarget.TransformFeedbackBuffer, numbindings++, obj.glname, (IntPtr)offset, size);
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
