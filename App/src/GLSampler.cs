using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace App
{
    class GLSampler : GLObject
    {
        #region FIELDS

        [FxField] private TextureMinFilter Minfilter = TextureMinFilter.Nearest;
        [FxField] private TextureMagFilter Magfilter = TextureMagFilter.Nearest;
        [FxField] private TextureWrapMode Wrap = TextureWrapMode.ClampToEdge;

        #endregion

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLSampler(Compiler.Block block, Dictionary<string, object> scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"sampler '{Name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenSampler();
            int mini = (int)Minfilter;
            int magi = (int)Magfilter;
            int wrapi = (int)Wrap;
            GL.SamplerParameterI(glname, SamplerParameterName.TextureMinFilter, ref mini);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureMagFilter, ref magi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapR, ref wrapi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapS, ref wrapi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapT, ref wrapi);

            HasErrorOrGlError(err, block);
            if (err.HasErrors)
                throw err;
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            if (glname > 0)
            {
                GL.DeleteSampler(glname);
                glname = 0;
            }
        }
    }
}
