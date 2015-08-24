using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace gled
{
    class GLSampler : GLObject
    {
        public TextureMinFilter minfilter = TextureMinFilter.Nearest;
        public TextureMagFilter magfilter = TextureMagFilter.Nearest;
        public TextureWrapMode wrap = TextureWrapMode.Repeat;

        public GLSampler(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, args);

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

        public override void Bind(int unit)
        {

        }

        public override void Unbind(int unit)
        {

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
