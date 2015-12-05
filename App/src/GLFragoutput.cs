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
        /// Create OpenGL framebuffer object for fragment output.
        /// </summary>
        /// <param name="dir">Directory of the tech-file.</param>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="annotation">Annotation used for special initialization.</param>
        /// <param name="text">Text block specifying the object commands.</param>
        /// <param name="classes">Collection of scene objects.</param>
        public GLFragoutput(string dir, string name, string annotation, string text, Dict<GLObject> classes)
            : base(name, annotation)
        {
            var err = new CompileException($"fragoutput '{name}'");

            // PARSE TEXT
            var body = new Commands(text, err);

            // PARSE ARGUMENTS
            body.Cmds2Fields(this, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, glname);
            
            // PARSE COMMANDS
            foreach (var cmd in body)
                attatch(err + $"command {cmd.idx} '{cmd.cmd}'", cmd.cmd, cmd.args, classes);

            // if any errors occurred throw exception
            if (err.HasErrors())
                throw err;

            // CHECK FOR OPENGL ERRORS
            Bind();
            var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            Unbind();

            // final error checks
            if (HasErrorOrGlError(err))
                throw err;
            if (status != FramebufferErrorCode.FramebufferComplete)
                throw err.Add("Could not be created due to an unknown error.");
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

        private void attatch(CompileException err, string cmd, string[] args, Dict<GLObject> classes)
        {
            // get OpenGL image
            GLImage glimg = classes.GetValue<GLImage>(args[0]);
            if (glimg == null)
            {
                err.Add($"The name '{args[0]}' does not reference an object of type 'image'.");
                return;
            }

            // set width and height for GLPass to set the right viewport size
            if (width == 0 && height == 0)
            {
                width = glimg.width;
                height = glimg.height;
            }

            // get additional optional parameters
            int mipmap = args.Length > 1 ? int.Parse(args[1]) : 0;
            int layer = args.Length > 2 ? int.Parse(args[2]) : 0;

            // get attachment point
            FramebufferAttachment attachment;
            if (!Enum.TryParse($"{cmd}attachment{(cmd.Equals("color") ? ""+numAttachments++ : "")}",
                true, out attachment))
            {
                err.Add($"Invalid attachment point '{cmd}'.");
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
                    err.Add($"The texture type '{glimg.target}' of image '{args[0]}' is not supported.");
                    break;
            }
        }
    }
}
