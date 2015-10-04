using OpenTK.Graphics.OpenGL4;
using System;

namespace App
{
    class GLTexture : GLObject
    {
        #region FIELDS

        public string samp = null;
        public string buff = null;
        public string img = null;
        public SizedInternalFormat format = 0;
        private GLObject glsamp = null;
        private GLObject glbuff = null;
        private GLObject glimg = null;

        #endregion

        public GLTexture(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)

        {
            // PARSE TEXT
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS
            Cmds2Fields(this, ref cmds);
            
            // GET REFERENCES
            if (samp != null && (glsamp = classes.FindClass<GLSampler>(samp)) == null)
                throw new Exception(Dict.NotFoundMsg("texture", name, "sampler", samp));
            if (buff != null && (glbuff = classes.FindClass<GLBuffer>(buff)) == null)
                throw new Exception(Dict.NotFoundMsg("texture", name, "buffer", buff));
            if (img != null && (glimg = classes.FindClass<GLImage>(img)) == null)
                throw new Exception(Dict.NotFoundMsg("texture", name, "image", img));

            // INCASE THIS IS A TEXTURE OBJECT
            if (glbuff != null && glimg == null)
            {
                if (format == 0)
                    throw new Exception("ERROR in texture " + name + ": "
                            + "No texture buffer format defined for buffer '" + buff + "' (e.g. format RGBA8).");
                // CREATE OPENGL OBJECT
                glname = GL.GenTexture();
                GL.BindTexture(TextureTarget.TextureBuffer, glname);
                GL.TexBuffer(TextureBufferTarget.TextureBuffer, format, glbuff.glname);
                GL.BindTexture(TextureTarget.TextureBuffer, 0);
                if (GL.GetError() != ErrorCode.NoError)
                    throw new Exception("OpenGL error '" + GL.GetError() + "' occurred during texture creation.");
            }
        }

        public void Bind(int unit)
        {
            if (samp != null)
                GL.BindSampler(unit, glsamp.glname);

            if (glimg != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + unit);
                GL.BindTexture(((GLImage)glimg).target, glimg.glname);
            }
            else if (glbuff != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + unit);
                GL.BindTexture(TextureTarget.TextureBuffer, glname);
            }
        }

        public void Unbind(int unit)
        {
            if (samp != null)
                GL.BindSampler(unit, 0);

            if (glimg != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + unit);
                GL.BindTexture(((GLImage)glimg).target, 0);
            }
            else if (glbuff != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + unit);
                GL.BindTexture(TextureTarget.TextureBuffer, 0);
            }
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTexture(glname);
                glname = 0;
            }
        }
        
    }

}
