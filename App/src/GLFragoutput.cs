using OpenTK.Graphics.OpenGL4;
using System;

namespace App
{
    class GLFragoutput : GLObject
    {
        #region PROPERTIES
        [Field] public int width { get; protected set; }
        [Field] public int height { get; protected set; }
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

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLFragoutput(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"fragoutput '{name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, glname);

            // PARSE COMMANDS
            foreach (var cmd in block)
                Attach(cmd, scene, err | $"command '{cmd.Name}'");

            // if any errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // CHECK FOR OPENGL ERRORS
            Bind();
            var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            Unbind();

            // final error checks
            if (HasErrorOrGlError(err, block))
                throw err;
            if (status != FramebufferErrorCode.FramebufferComplete)
                throw err.Add("Could not be created due to an unknown error.", block);
        }
        
        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteFramebuffer(glname);
                glname = 0;
            }
        }

        /// <summary>
        /// Bind fragment output object as a render target (framebuffer).
        /// </summary>
        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, glname);
            // set draw buffers
            if(numAttachments > 0)
                GL.DrawBuffers(numAttachments, attachmentPoints);
            else
                GL.DrawBuffer(DrawBufferMode.None);
        }

        /// <summary>
        /// Unbind fragment output object as a render target (framebuffer).
        /// </summary>
        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            // set draw buffer
            GL.DrawBuffer(DrawBufferMode.BackLeft);
        }

        /// <summary>
        /// Attach image to the fragment output object.
        /// </summary>
        /// <param name="cmd">Command to process.</param>
        /// <param name="scene">Dictionary of all objects in the scene.</param>
        /// <param name="err">Compilation error collector.</param>
        private void Attach(Compiler.Command cmd, Dict scene, CompileException err)
        {
            // get OpenGL image
            GLImage glimg = scene.GetValueOrDefault<GLImage>(cmd[0].Text);
            if (glimg == null)
            {
                err.Add($"The name '{cmd[0].Text}' does not reference an object of type 'image'.", cmd);
                return;
            }

            // set width and height for GLPass to set the right viewport size
            if (width == 0 && height == 0)
            {
                width = glimg.width;
                height = glimg.height;
            }

            // get additional optional parameters
            int mipmap = cmd.ArgCount > 1 ? int.Parse(cmd[1].Text) : 0;
            int layer = cmd.ArgCount > 2 ? int.Parse(cmd[2].Text) : 0;

            // get attachment point
            FramebufferAttachment attachment;
            if (!Enum.TryParse(
                $"{cmd.Name}attachment{(cmd.Name.Equals("color") ? "" + numAttachments++ : "")}",
                true, out attachment))
            {
                err.Add($"Invalid attachment point '{cmd.Name}'.", cmd);
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
                    err.Add($"The texture type '{glimg.target}' of image " +
                        $"'{cmd[0].Text}' is not supported.", cmd);
                    break;
            }
        }
    }
}
