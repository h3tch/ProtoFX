using OpenTK.Graphics.OpenGL4;
using System;

namespace App
{
    class GLTexture : GLObject
    {
        #region FIELDS
        [Field] private string samp = null;
        [Field] private string buff = null;
        [Field] private string img = null;
        [Field] private GpuFormat format = 0;
        private GLSampler glsamp = null;
        private GLBuffer glbuff = null;
        private GLImage glimg = null;
        #endregion

        /// <summary>
        /// </summary>
        /// <param name="params"></param>
        /// <param name="samp"></param>
        /// <param name="buff"></param>
        /// <param name="img"></param>
        public GLTexture(GLParams @params, GLSampler samp, GLBuffer buff, GLImage img)
            : base(@params)
        {
            var err = new CompileException($"texture '{@params.name}'");

            // set name
            this.glsamp = samp;
            this.glbuff = buff;
            this.glimg = img;

            // final constructor steps
            ctor(err);
        }

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLTexture(GLParams @params) : base(@params)
        {
            var err = new CompileException($"texture '{@params.name}'");

            // PARSE TEXT
            var body = new Commands(@params.text, err);

            // PARSE ARGUMENTS
            body.Cmds2Fields(this, err);

            // GET REFERENCES
            GetReferences(samp, buff, img, @params, err);

            // final constructor steps
            ctor(err);
        }

        private void ctor(CompileException err)
        {
            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;

            // INCASE THIS IS A TEXTURE OBJECT
            if (glimg != null)
            {
                glname = glimg.glname;
                // get internal format
                int f;
                GL.GetTextureLevelParameter(glname, 0, GetTextureParameter.TextureInternalFormat, out f);
                format = (GpuFormat)f;
            }
            else if (glbuff != null)
            {
                if (format == 0)
                    throw err.Add($"No texture buffer format defined " +
                        $"for buffer '{buff}' (e.g. format RGBA8).");
                // CREATE OPENGL OBJECT
                glname = GL.GenTexture();
                GL.BindTexture(TextureTarget.TextureBuffer, glname);
                GL.TexBuffer(TextureBufferTarget.TextureBuffer, (SizedInternalFormat)format, glbuff.glname);
                GL.BindTexture(TextureTarget.TextureBuffer, 0);
                HasErrorOrGlError(err);
            }

            if (err.HasErrors())
                throw err;
        }

        /// <summary>
        /// Bind texture to texture unit.
        /// </summary>
        /// <param name="unit">Texture unit.</param>
        public void BindTex(int unit)
        {
            if (glsamp != null)
                GL.BindSampler(unit, glsamp.glname);
            GLDebugger.BindTex(unit, glimg != null ? glimg.target : TextureTarget.TextureBuffer, glname);
        }

        /// <summary>
        /// Unbind texture from texture unit.
        /// </summary>
        /// <param name="unit">Texture unit.</param>
        public void UnbindTex(int unit)
        {
            if (glsamp != null)
                GL.BindSampler(unit, 0);
            GLDebugger.UnbindTex(unit, glimg != null ? glimg.target : TextureTarget.TextureBuffer);
        }

        /// <summary>
        /// Bind texture to compute image unit.
        /// </summary>
        /// <param name="unit">Image unit.</param>
        /// <param name="level">Texture mipmap level.</param>
        /// <param name="layer">Texture array index or texture depth.</param>
        /// <param name="access">How the texture will be accessed by the shader.</param>
        /// <param name="format">Pixel format of texture pixels.</param>
        public void BindImg(int unit, int level, int layer, TextureAccess access, GpuFormat format)
            => GLDebugger.BindImg(unit, level, glimg?.length > 0, layer, access, format, glname);

        public void UnbindImg(int unit) => GLDebugger.UnbindImg(unit);

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTexture(glname);
                glname = 0;
            }
        }
        
        public void GetReferences(string samp, string buff, string img, GLParams @params, CompileException err)
        {
            // GET REFERENCES
            if (samp != null)
                @params.scene.TryGetValue(samp, out glsamp, err);
            if (buff != null)
                @params.scene.TryGetValue(buff, out glbuff, err);
            if (img != null)
                @params.scene.TryGetValue(img, out glimg, err);
            if (glbuff != null && glimg != null)
                err.Add("Only an image or a buffer can be bound to a texture object.");
            if (glbuff == null && glimg == null)
                err.Add("Ether an image or a buffer has to be bound to a texture object.");
        }
    }
}
