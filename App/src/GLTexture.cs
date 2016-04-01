using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLTexture : GLObject
    {
        #region FIELDS
        [FxField] private string Buff = null;
        [FxField] private string Img = null;
        [FxField] private GpuFormat Format = 0;
        private GLBuffer glBuff = null;
        private GLImage glImg = null;
        #endregion

        /// <summary>
        /// Create OpenGL object specifying the texture
        /// format and referenced scene objects directly.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="anno"></param>
        /// <param name="format"></param>
        /// <param name="glbuff"></param>
        /// <param name="glimg"></param>
        public GLTexture(string name, string anno, GpuFormat format, GLBuffer glbuff, GLImage glimg)
            : base(name, anno)
        {
            var err = new CompileException($"texture '{name}'");

            // set name
            this.Format = format;
            this.glBuff = glbuff;
            this.glImg = glimg;

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
        /// <param name="glbuff"></param>
        /// <param name="glimg"></param>
        public GLTexture(Compiler.Block block, Dict scene, GLBuffer glbuff, GLImage glimg)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"texture '{name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

            // set name
            this.glBuff = glbuff;
            this.glImg = glimg;

            // GET REFERENCES
            if (Buff != null)
                scene.TryGetValue(Buff, out this.glBuff, block, err);
            if (Img != null)
                scene.TryGetValue(Img, out this.glImg, block, err);
            if (this.glBuff != null && this.glImg != null)
                err.Add("Only an image or a buffer can be bound to a texture object.", block);
            if (this.glBuff == null && this.glImg == null)
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
            : this(block, scene, null, null)
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
        /// <param name="tex">Texture object.</param>
        public static void BindTex(int unit, GLTexture tex)
            => FxDebugger.BindTex(unit, tex?.glImg?.Target ?? TextureTarget.TextureBuffer, tex?.glname ?? 0);

        /// <summary>
        /// Bind texture to compute-image unit.
        /// </summary>
        /// <param name="unit">Image unit.</param>
        /// <param name="tex">Texture object.</param>
        /// <param name="level">Texture mipmap level.</param>
        /// <param name="layer">Texture array index or texture depth.</param>
        /// <param name="access">How the texture will be accessed by the shader.</param>
        /// <param name="format">Pixel format of texture pixels.</param>
        public static void BindImg(int unit, GLTexture tex, int level = 0, int layer = 0,
            TextureAccess access = TextureAccess.ReadOnly, GpuFormat format = GpuFormat.Rgba8)
            => FxDebugger.BindImg(unit, level, tex?.glImg?.Length > 0, layer, access, format, tex?.glname ?? 0);
        
        /// <summary>
        /// Link image or buffer object to the texture.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="line"></param>
        /// <param name="err"></param>
        private void Link(string file, int line, CompileException err)
        {
            // INCASE THIS IS A TEXTURE OBJECT
            if (glImg != null)
            {
                glname = glImg.glname;
                // get internal format
                int f;
                GL.GetTextureLevelParameter(glname, 0, GetTextureParameter.TextureInternalFormat, out f);
                Format = (GpuFormat)f;
            }
            // INCASE THIS IS A BUGGER OBJECT
            else if (glBuff != null)
            {
                if (Format == 0)
                    throw err.Add($"No texture buffer format defined for " +
                        "buffer '{buff}' (e.g. format RGBA8).", file, line);
                // CREATE OPENGL OBJECT
                glname = GL.GenTexture();
                GL.BindTexture(TextureTarget.TextureBuffer, glname);
                GL.TexBuffer(TextureBufferTarget.TextureBuffer, (SizedInternalFormat)Format, glBuff.glname);
                GL.BindTexture(TextureTarget.TextureBuffer, 0);
            }
        }
    }
}
