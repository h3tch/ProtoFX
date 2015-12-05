using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLSampler : GLObject
    {
        #region FIELDS
        [Field] private TextureMinFilter minfilter = TextureMinFilter.Nearest;
        [Field] private TextureMagFilter magfilter = TextureMagFilter.Nearest;
        [Field] private TextureWrapMode wrap = TextureWrapMode.ClampToEdge;
        #endregion

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="dir">Directory of the tech-file.</param>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="anno">Annotation used for special initialization.</param>
        /// <param name="text">Text block specifying the object commands.</param>
        /// <param name="classes">Collection of scene objects.</param>
        public GLSampler(string dir, string name, string anno, string text, Dict<GLObject> classes)
            : base(name, anno)
        {
            var err = new CompileException($"sampler '{name}'");

            // PARSE TEXT
            var body = new Commands(text, err);

            // PARSE ARGUMENTS
            body.Cmds2Fields(this, err);

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

            HasErrorOrGlError(err);
            if (err.HasErrors())
                throw err;
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
