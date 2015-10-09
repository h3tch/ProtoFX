using OpenTK.Graphics.OpenGL4;
using System;

namespace App
{
    class GLShader : GLObject
    {
        public GLShader(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            string errstr = "shader '" + name + "': ";
            // CREATE OPENGL OBJECT
            ShaderType type;
            switch (annotation)
            {
                case "vert": type = ShaderType.VertexShader; break;
                case "tess": type = ShaderType.TessControlShader; break;
                case "eval": type = ShaderType.TessEvaluationShader; break;
                case "geom": type = ShaderType.GeometryShader; break;
                case "frag": type = ShaderType.FragmentShader; break;
                case "comp": type = ShaderType.ComputeShader; break;
                default:
                    throw new GLException(errstr + "Shader type '"
                        + annotation + "' is not supported.");
            }

            // CREATE OPENGL OBJECT
            glname = GL.CreateShader(type);
            GL.ShaderSource(glname, text);
            GL.CompileShader(glname);

            // CHECK FOR ERRORS
            int status;
            GL.GetShader(glname, ShaderParameter.CompileStatus, out status);
            if (status != 1)
                throw new GLException(errstr + "\n" + GL.GetShaderInfoLog(glname));
            if (GL.GetError() != ErrorCode.NoError)
                throw new GLException(errstr + "OpenGL error '"
                    + GL.GetError() + "' occurred during shader creation.");
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteShader(glname);
                glname = 0;
            }
        }
    }
}
