using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace gled
{
    class GLSampler : GLObject
    {
        public TextureMinFilter minfilter = TextureMinFilter.Nearest;
        public TextureMagFilter magfilter = TextureMagFilter.Nearest;
        public TextureWrapMode wrap = TextureWrapMode.ClampToEdge;

        public GLSampler(string dir, string name, string annotation, string text, GLDict classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS
            Cmds2Fields(this, ref cmds);

            // CREATE OPENGL OBJECT
            glname = GL.GenSampler();
            int mini = (int)minfilter;
            int magi = (int)magfilter;
            int wrapi = (int)wrap;
            GL.SamplerParameterI(glname, SamplerParameterName.TextureMinFilter, ref mini);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureMagFilter, ref magi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapR, ref wrapi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapS, ref wrapi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapT, ref wrapi);

            throwExceptionOnOpenGlError("sampler", name, "create sampler");
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteSampler(glname);
                glname = 0;
            }
        }
    }
}
