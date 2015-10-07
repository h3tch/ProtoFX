using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System;

namespace App
{
    class GLSampler : GLObject
    {
        #region FIELDS
        public TextureMinFilter minfilter = TextureMinFilter.Nearest;
        public TextureMagFilter magfilter = TextureMagFilter.Nearest;
        public TextureWrapMode wrap = TextureWrapMode.ClampToEdge;
        #endregion

        public GLSampler(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS
            Cmds2Fields(this, ref cmds);

            // CREATE OPENGL OBJECT
            glname = GL.GenSampler();
            int mini = (int)minfilter;
            int magi = (int)magfilter;
            int wrapi = (int)wrap;
            GL.SamplerParameterI(glname, SamplerParameterName.TextureMinFilter, ref mini);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureMagFilter, ref magi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapR, ref wrapi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapS, ref wrapi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapT, ref wrapi);
            
            if (GL.GetError() != ErrorCode.NoError)
                throw new Exception("OpenGL error '" + GL.GetError() + "' occurred during sampler creation.");
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteSampler(glname);
                glname = 0;
            }
        }
    }
}
