using OpenTK.Graphics.OpenGL4;
using System;

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

        public GLFragoutput(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            var err = new GLException();
            err.PushCall("fragoutput '" + name + "'");

            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(this, ref cmds);

            // CREATE OPENGL OBJECT
            glname = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, glname);
            
            // PARSE COMMANDS
            for (int i = 0; i < cmds.Length; i++)
            {
                var cmd = cmds[i];

                // ignore already parsed commands
                if (cmd == null || cmd.Length < 2)
                    continue;

                // attach image
                err.PushCall("command " + (i + 1) + " '" + cmd[0] + "'");
                attatch(err, cmd, classes);
                err.PopCall();
            }

            // if any errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // CHECK FOR OPENGL ERRORS
            Bind();
            var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            Unbind();

            // final error checks
            if (GL.GetError() != ErrorCode.NoError)
                err.Throw("OpenGL error '" + GL.GetError()
                    + "' occurred during fragment output creation.");
            if (status != FramebufferErrorCode.FramebufferComplete)
                err.Throw("Could not be created due to an unknown error.");
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

        private void attatch(GLException err, string[] cmd, Dict classes)
        {
            // get OpenGL image
            GLImage glimg = classes.FindClass<GLImage>(cmd[1]);
            if (glimg == null)
            {
                err.Add("The name '" + cmd[1] + "' does not reference an object of type 'image'.");
                return;
            }

            // set width and height for GLPass to set the right viewport size
            if (width == 0 && height == 0)
            {
                width = glimg.width;
                height = glimg.height;
            }

            // get additional optional parameters
            int mipmap = cmd.Length >= 3 ? int.Parse(cmd[2]) : 0;
            int layer = cmd.Length >= 4 ? int.Parse(cmd[3]) : 0;

            // get attachment point
            FramebufferAttachment attachment;
            if (!Enum.TryParse(cmd[0] + "attachment" + (cmd[0].Equals("color") ? "" + numAttachments++ : ""),
                true, out attachment))
            {
                err.Add("Invalid attachment point '" + cmd[0] + "'.");
                return;
            }

            // attach texture to framebuffer
            switch (glimg.target)
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
                    err.Add("The texture type '" + glimg.target + "' of image '" + cmd[1] + "' is not supported.");
                    break;
            }
        }
    }
}
