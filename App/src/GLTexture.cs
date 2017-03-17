using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Format = format;
            glBuff = glbuff;
            glImg = glimg;

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
            var err = new CompileException($"texture '{Name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

            // set name
            glBuff = glbuff;
            glImg = glimg;

            // GET REFERENCES
            if (Buff != null)
                scene.TryGetValue(Buff, out glBuff, block, err);
            if (Img != null)
                scene.TryGetValue(Img, out glImg, block, err);
            if (glBuff != null && glImg != null)
                err.Error("Only an image or a buffer can be bound to a texture object.", block);
            if (glBuff == null && glImg == null)
                err.Error("Ether an image or a buffer has to be bound to a texture object.", block);

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors)
                throw err;

            // INCASE THIS IS A TEXTURE OBJECT
            Link(block.Filename, block.LineInFile, err);
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
            base.Delete();
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
        {
            var target = tex?.glImg?.Target ?? TextureTarget.TextureBuffer;
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            if (target != 0)
                GL.BindTexture(target, tex?.glname ?? 0);
        }

        public static int FirstUnusedTexUnit(GetIndexedPName target, int start = 0, int count = 16)
        {
            return Enumerable.Range(start, count).FirstOr(i => {
                GL.GetInteger(target, i, out int u);
                return u == 0;
            }, -1);
        }

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
        {
            GL.BindImageTexture(unit, tex?.glname ?? 0, level, tex?.glImg?.Length > 0,
                                Math.Max(layer, 0), access, (SizedInternalFormat)format);
        }

        public static int FirstUnusedImgUnit(int start = 0, int count = 16)
        {
            return Enumerable.Range(start, count).FirstOr(i => {
                GL.GetInteger((GetIndexedPName)All.ImageBindingName, i, out int u);
                return u == 0;
            }, -1);
        }

        /// <summary>
        /// Link image or buffer object to the texture.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="line"></param>
        /// <param name="err"></param>
        private void Link(string file, int line, CompileException err)
        {
            // IN CASE THIS IS A TEXTURE OBJECT
            if (glImg != null)
            {
                glname = glImg.glname;
                // get internal format
                GL.GetTextureLevelParameter(glname, 0, GetTextureParameter.TextureInternalFormat, out int f);
                Format = (GpuFormat)f;
            }
            // IN CASE THIS IS A BUFFER OBJECT
            else if (glBuff != null)
            {
                if (Format == 0)
                    throw err.Error($"No texture buffer format defined for " +
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
