using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace gled
{
    class GLPass : GLObject
    {
        public string vert = null;
        public string tess = null;
        public string eval = null;
        public string geom = null;
        public string frag = null;
        public string geomout = null;
        public string fragout = null;
        public string[] option = null;
        public GLObject glvert = null;
        public GLObject gltess = null;
        public GLObject gleval = null;
        public GLObject glgeom = null;
        public GLObject glfrag = null;
        public GLObject glgeomout = null;
        public GLObject glfragout = null;
        public GLCamera glcamera = null;
        public int g_view = -1;
        public int g_proj = -1;
        public int g_viewproj = -1;
        public int g_info = -1;

        public GLPass(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, args);

            GLObject obj;
            classes.TryGetValue(GLCamera.cameraname, out obj);
            if (obj.GetType() == typeof(GLCamera))
                glcamera = (GLCamera)obj;

            // CREATE OPENGL OBJECT
            glname = GL.CreateProgram();

            if (vert != null)
                if (classes.TryGetValue(vert, out glvert) && glvert.GetType() != typeof(GLSampler))
                    GL.AttachShader(glname, glvert.glname);
                else
                    throw new Exception("ERROR in pass " + name + ": Invalid name '" + vert + "' for 'vert'.");

            if (tess != null)
                if (classes.TryGetValue(tess, out gltess) && gltess.GetType() != typeof(GLSampler))
                    GL.AttachShader(glname, gltess.glname);
                else
                    throw new Exception("ERROR in pass " + name + ": Invalid name '" + tess + "' for 'tess'.");

            if (eval != null)
                if (classes.TryGetValue(eval, out gleval) && gleval.GetType() != typeof(GLSampler))
                    GL.AttachShader(glname, gleval.glname);
                else
                    throw new Exception("ERROR in pass " + name + ": Invalid name '" + eval + "' for 'eval'.");

            if (geom != null)
                if (classes.TryGetValue(geom, out glgeom) && glgeom.GetType() != typeof(GLSampler))
                    GL.AttachShader(glname, glgeom.glname);
                else
                    throw new Exception("ERROR in pass " + name + ": Invalid name '" + geom + "' for 'geom'.");

            if (frag != null)
                if (classes.TryGetValue(frag, out glfrag) && glfrag.GetType() != typeof(GLSampler))
                    GL.AttachShader(glname, glfrag.glname);
                else
                    throw new Exception("ERROR in pass " + name + ": Invalid name '" + frag + "' for 'frag'.");

            GL.LinkProgram(glname);

            if (glvert != null)
                GL.DetachShader(glname, glvert.glname);
            if (gltess != null)
                GL.DetachShader(glname, gltess.glname);
            if (gleval != null)
                GL.DetachShader(glname, gleval.glname);
            if (glgeom != null)
                GL.DetachShader(glname, glgeom.glname);
            if (glfrag != null)
                GL.DetachShader(glname, glfrag.glname);

            int status;
            GL.GetProgram(glname, GetProgramParameterName.LinkStatus, out status);
            if (status != 1)
                throw new Exception("ERROR in pass " + name + ":\n" + GL.GetProgramInfoLog(glname));
            
            g_view = GL.GetUniformLocation(glname, "g_view");
            g_proj = GL.GetUniformLocation(glname, "g_proj");
            g_viewproj = GL.GetUniformLocation(glname, "g_viewproj");
            g_info = GL.GetUniformLocation(glname, "g_info");
            
        }

        public override void Bind(int unit)
        {
            GL.UseProgram(glname);
            if (g_view >= 0)
            {
                Matrix4 view = glcamera.view;
                GL.ProgramUniformMatrix4(glname, g_view, 1, false, ref view.Row0.W);
            }
            if (g_proj >= 0)
            {
                Matrix4 proj = glcamera.proj;
                GL.ProgramUniformMatrix4(glname, g_proj, 1, false, ref proj.Row0.W);
            }
            if (g_viewproj >= 0)
            {
                Matrix4 viewproj = glcamera.viewproj;
                GL.ProgramUniformMatrix4(glname, g_proj, 1, false, ref viewproj.Row0.W);
            }
            if (g_info >= 0)
            {
                Vector4 info = glcamera.info;
                GL.ProgramUniformMatrix4(glname, g_proj, 1, false, ref info.W);
            }
        }

        public override void Unbind(int unit)
        {
            GL.UseProgram(0);
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteProgram(glname);
                glname = 0;
            }
        }
    }
}
