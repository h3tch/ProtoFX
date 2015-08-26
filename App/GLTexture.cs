using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gled
{

    class GLTexture : GLObject
    {
        public string samp = null;
        public string buff = null;
        public string img = null;
        public SizedInternalFormat format = 0;
        private GLObject glsamp = null;
        private GLObject glbuff = null;
        private GLObject glimg = null;

        public GLTexture(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)

        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, ref args);

            if (samp != null && classes.TryGetValue(samp, out glsamp) && glsamp.GetType() != typeof(GLSampler))
                throw new Exception("ERROR in texture " + name + ": "
                        + "The specified sampler name '" + samp + "' does not reference an sampler object.");

            if (buff != null && classes.TryGetValue(buff, out glbuff) && glbuff.GetType() != typeof(GLBuffer))
                throw new Exception("ERROR in texture " + name + ": "
                        + "The specified buffer name '" + buff + "' does not reference a buffer object.");

            if (img != null && classes.TryGetValue(img, out glimg) && glimg.GetType() != typeof(GLImage))
                throw new Exception("ERROR in texture " + name + ": "
                        + "The specified image name '" + img + "' does not reference an image object.");

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
                throwExceptionOnOpenGlError("texture", name, "create texture buffer object");
            }
        }

        public void Bind(int unit)
        {
            if (samp != null)
                GL.BindSampler(unit, glsamp.glname);

            if (glimg != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + unit);
                GL.BindTexture(((GLImage)glimg).type, glimg.glname);
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
                GL.BindTexture(((GLImage)glimg).type, 0);
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
