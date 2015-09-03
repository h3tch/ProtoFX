using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace gled
{
    class GLFragoutput : GLObject
    {
        public int width = 0;
        public int height = 0;
        private int numAttachments = 0;
        private DrawBuffersEnum[] attachmentPoints = new DrawBuffersEnum[]
        {
            DrawBuffersEnum.ColorAttachment0,
            DrawBuffersEnum.ColorAttachment1,
            DrawBuffersEnum.ColorAttachment2,
            DrawBuffersEnum.ColorAttachment3,
            DrawBuffersEnum.ColorAttachment4,
            DrawBuffersEnum.ColorAttachment5,
            DrawBuffersEnum.ColorAttachment6,
            DrawBuffersEnum.ColorAttachment7,
            DrawBuffersEnum.ColorAttachment8,
            DrawBuffersEnum.ColorAttachment9,
            DrawBuffersEnum.ColorAttachment10,
            DrawBuffersEnum.ColorAttachment11,
            DrawBuffersEnum.ColorAttachment12,
            DrawBuffersEnum.ColorAttachment13,
            DrawBuffersEnum.ColorAttachment14,
            DrawBuffersEnum.ColorAttachment15,
        };

        public GLFragoutput(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, ref args);

            // CREATE OPENGL OBJECT
            glname = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, glname);

            GLObject globj;
            foreach (var arg in args)
            {
                // ignore already parsed commands
                if (arg == null || arg.Length < 2)
                    continue;

                // get OpenGL image
                if (classes.TryGetValue(arg[1], out globj) == false || globj.GetType() != typeof(GLImage))
                    throw new Exception("ERROR in fragoutput " + name + ": Could not find image '" + arg[1] + "'.");
                GLImage glimg = (GLImage)globj;

                // set width and height for GLPass to set the right viewport size
                if (width == 0 && height == 0)
                {
                    width  = glimg.width;
                    height = glimg.height;
                }

                // get additional optional parameters
                int mipmap = arg.Length >= 3 ? int.Parse(arg[2]) : 0;
                int layer = arg.Length >= 4 ? int.Parse(arg[3]) : 0;

                // get attachment point
                FramebufferAttachment attachment;
                if (!Enum.TryParse(arg[0] + "attachment" + (arg[0].Equals("color") ? ""+numAttachments++ : ""), true, out attachment))
                    throw new Exception("ERROR in fragoutput " + name + ": "
                        + "Invalid attachment point '" + arg[0] + "'.");

                // attach texture to framebuffer
                switch(glimg.target)
                {
                    case TextureTarget.Texture2DArray:
                    case TextureTarget.Texture3D:
                        GL.FramebufferTexture3D(FramebufferTarget.Framebuffer,
                            attachment, glimg.target, glimg.glname, mipmap, layer);
                        break;
                    case TextureTarget.Texture1DArray:
                    case TextureTarget.Texture2D:
                        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,
                            attachment, glimg.target, glimg.glname, mipmap);
                        break;
                    case TextureTarget.Texture1D:
                        GL.FramebufferTexture1D(FramebufferTarget.Framebuffer,
                            attachment, glimg.target, glimg.glname, mipmap);
                        break;
                    default:
                        throw new Exception("ERROR in fragoutput " + name + ": "
                            + "The texture type '" + glimg.target + "' of image '" + arg[1] + "' is not supported.");
                }
            }

            // CHECK FOR OPENGL ERRORS
            Bind();
            var status = GL.CheckNamedFramebufferStatus(glname, FramebufferTarget.Framebuffer);
            Unbind();
            if (status != All.FramebufferComplete)
                throw new Exception("ERROR in fragoutput " + name + ": Could not be created due to an unknown error.");
            throwExceptionOnOpenGlError("image", name, "allocate (and write) texture");
        }
        
        private void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, glname);
            // set draw buffers
            if(numAttachments > 0)
                GL.DrawBuffers(numAttachments, attachmentPoints);
            else
                GL.DrawBuffer(DrawBufferMode.None);
        }

        private void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            // set draw buffer
            GL.DrawBuffer(DrawBufferMode.BackLeft);
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteFramebuffer(glname);
                glname = 0;
            }
        }
    }
}
