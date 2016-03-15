using OpenTK.Graphics.OpenGL4;

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
        /// Create OpenGL object specifying the texture
        /// format and referenced scene objects directly.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="anno"></param>
        /// <param name="format"></param>
        /// <param name="glsamp"></param>
        /// <param name="glbuff"></param>
        /// <param name="glimg"></param>
        public GLTexture(string name, string anno, GpuFormat format, GLSampler glsamp, GLBuffer glbuff, GLImage glimg)
            : base(name, anno)
        {
            var err = new CompileException($"texture '{name}'");

            // set name
            this.format = format;
            this.glsamp = glsamp;
            this.glbuff = glbuff;
            this.glimg = glimg;

            // INCASE THIS IS A TEXTURE OBJECT
            Link("", -1, err);
            if (HasErrorOrGlError(err, "", -1))
                throw err;
        }

        /// <summary>
        /// Create OpenGL object specifying the referenced scene objects directly.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="glsamp"></param>
        /// <param name="glbuff"></param>
        /// <param name="glimg"></param>
        public GLTexture(Compiler.Block block, Dict scene, GLSampler glsamp, GLBuffer glbuff, GLImage glimg)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"texture '{name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

            // set name
            this.glsamp = glsamp;
            this.glbuff = glbuff;
            this.glimg = glimg;

            // GET REFERENCES
            if (samp != null)
                scene.TryGetValue(samp, out this.glsamp, block, err);
            if (buff != null)
                scene.TryGetValue(buff, out this.glbuff, block, err);
            if (img != null)
                scene.TryGetValue(img, out this.glimg, block, err);
            if (this.glbuff != null && this.glimg != null)
                err.Add("Only an image or a buffer can be bound to a texture object.", block);
            if (this.glbuff == null && this.glimg == null)
                err.Add("Ether an image or a buffer has to be bound to a texture object.", block);

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;

            // INCASE THIS IS A TEXTURE OBJECT
            Link(block.File, block.LineInFile, err);
            if (HasErrorOrGlError(err, block))
                throw err;
        }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLTexture(Compiler.Block block, Dict scene, bool debugging)
            : this(block, scene, null, null, null)
        {
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTexture(glname);
                glname = 0;
            }
        }

        /// <summary>
        /// Bind texture to texture unit.
        /// </summary>
        /// <param name="unit">Texture unit.</param>
        public void BindTex(int unit)
        {
            if (glsamp != null)
                GL.BindSampler(unit, glsamp.glname);
            GLDebugger.BindTex(unit, glimg != null ? glimg.Target : TextureTarget.TextureBuffer, glname);
        }

        /// <summary>
        /// Unbind texture from texture unit.
        /// </summary>
        /// <param name="unit">Texture unit.</param>
        public void UnbindTex(int unit)
        {
            if (glsamp != null)
                GL.BindSampler(unit, 0);
            GLDebugger.UnbindTex(unit, glimg != null ? glimg.Target : TextureTarget.TextureBuffer);
        }

        /// <summary>
        /// Bind texture to compute-image unit.
        /// </summary>
        /// <param name="unit">Image unit.</param>
        /// <param name="level">Texture mipmap level.</param>
        /// <param name="layer">Texture array index or texture depth.</param>
        /// <param name="access">How the texture will be accessed by the shader.</param>
        /// <param name="format">Pixel format of texture pixels.</param>
        public void BindImg(int unit, int level, int layer, TextureAccess access, GpuFormat format)
            => GLDebugger.BindImg(unit, level, glimg?.Length > 0, layer, access, format, glname);

        /// <summary>
        /// Unbind texture from compute-image unit.
        /// </summary>
        /// <param name="unit"></param>
        public void UnbindImg(int unit) => GLDebugger.UnbindImg(unit);

        /// <summary>
        /// Link image or buffer object to the texture.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="line"></param>
        /// <param name="err"></param>
        private void Link(string file, int line, CompileException err)
        {
            // INCASE THIS IS A TEXTURE OBJECT
            if (glimg != null)
            {
                glname = glimg.glname;
                // get internal format
                int f;
                GL.GetTextureLevelParameter(glname, 0, GetTextureParameter.TextureInternalFormat, out f);
                format = (GpuFormat)f;
            }
            // INCASE THIS IS A BUGGER OBJECT
            else if (glbuff != null)
            {
                if (format == 0)
                    throw err.Add($"No texture buffer format defined for " +
                        "buffer '{buff}' (e.g. format RGBA8).", file, line);
                // CREATE OPENGL OBJECT
                glname = GL.GenTexture();
                GL.BindTexture(TextureTarget.TextureBuffer, glname);
                GL.TexBuffer(TextureBufferTarget.TextureBuffer, (SizedInternalFormat)format, glbuff.glname);
                GL.BindTexture(TextureTarget.TextureBuffer, 0);
            }
        }
    }
}
