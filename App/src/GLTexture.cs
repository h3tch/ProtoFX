using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLTexture : GLObject
    {
        #region FIELDS
        [GLField]
        private string samp = null;
        [GLField]
        private string buff = null;
        [GLField]
        private string img = null;
        [GLField]
        private SizedInternalFormat format = 0;
        private GLSampler glsamp = null;
        private GLBuffer glbuff = null;
        private GLImage glimg = null;
        #endregion

        public GLTexture(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)

        {
            var err = new GLException($"texture '{name}'");

            // PARSE TEXT
            var body = new Commands(text, err);

            // PARSE ARGUMENTS
            body.Cmds2Fields(this, err);

            // GET REFERENCES
            if (samp != null)
                classes.TryFindClass(samp, out glsamp, err);
            if (buff != null)
                classes.TryFindClass(buff, out glbuff, err);
            if (img != null)
                classes.TryFindClass(img, out glimg, err);
            if (glbuff != null && glimg != null)
                err.Add("Only an image or a buffer can be bound to a texture object.");
            if (glbuff == null && glimg == null)
                err.Add("Ether an image or a buffer has to be bound to a texture object.");

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;

            // INCASE THIS IS A TEXTURE OBJECT
            if (glbuff != null && glimg == null)
            {
                if (format == 0)
                    err.Throw($"No texture buffer format defined for buffer '{buff}' (e.g. format RGBA8).");
                // CREATE OPENGL OBJECT
                glname = GL.GenTexture();
                GL.BindTexture(TextureTarget.TextureBuffer, glname);
                GL.TexBuffer(TextureBufferTarget.TextureBuffer, format, glbuff.glname);
                GL.BindTexture(TextureTarget.TextureBuffer, 0);
                GlErrorCheck(err);
            }

            if (err.HasErrors())
                throw err;
        }

        public void Bind(int unit)
        {
            if (samp != null)
                GL.BindSampler(unit, glsamp.glname);

            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            if (glimg != null)
                GL.BindTexture(glimg.target, glimg.glname);
            else
                GL.BindTexture(TextureTarget.TextureBuffer, glname);
        }

        public void Unbind(int unit)
        {
            if (samp != null)
                GL.BindSampler(unit, 0);

            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            if (glimg != null)
                GL.BindTexture(glimg.target, 0);
            else
                GL.BindTexture(TextureTarget.TextureBuffer, 0);
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
