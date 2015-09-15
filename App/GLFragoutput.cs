using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace App
{
    class GLFragoutput : GLObject
    {
        #region PROPERTIES

        public int width { get; protected set; }
        public int height { get; protected set; }

        #endregion

        #region FIELDS

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

        #endregion

        public GLFragoutput(string dir, string name, string annotation, string text, GLDict classes)
            : base(name, annotation)
        {
            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(this, ref cmds);

            // CREATE OPENGL OBJECT
            glname = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, glname);
            
            foreach (var cmd in cmds)
            {
                // ignore already parsed commands
                if (cmd == null || cmd.Length < 2)
                    continue;

                // get OpenGL image
                GLImage glimg = classes.FindClass<GLImage>(cmd[1]);
                if (glimg == null)
                    throw new Exception(GLDict.NotFoundMsg("fragoutput", name, "image", cmd[1]));

                // set width and height for GLPass to set the right viewport size
                if (width == 0 && height == 0)
                {
                    width  = glimg.width;
                    height = glimg.height;
                }

                // get additional optional parameters
                int mipmap = cmd.Length >= 3 ? int.Parse(cmd[2]) : 0;
                int layer = cmd.Length >= 4 ? int.Parse(cmd[3]) : 0;

                // get attachment point
                FramebufferAttachment attachment;
                if (!Enum.TryParse(cmd[0] + "attachment" + (cmd[0].Equals("color") ? ""+numAttachments++ : ""), true, out attachment))
                    throw new Exception("ERROR in fragoutput " + name + ": "
                        + "Invalid attachment point '" + cmd[0] + "'.");

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
                            + "The texture type '" + glimg.target + "' of image '" + cmd[1] + "' is not supported.");
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
        
        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, glname);
            // set draw buffers
            if(numAttachments > 0)
                GL.DrawBuffers(numAttachments, attachmentPoints);
            else
                GL.DrawBuffer(DrawBufferMode.None);
        }

        public void Unbind()
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
