using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace App
{
    class GLShader : GLObject
    {
        public GLShader(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            // CREATE OPENGL OBJECT
            ShaderType type;
            switch (annotation)
            {
                case "vert": type = ShaderType.VertexShader; break;
                case "tess": type = ShaderType.TessControlShader; break;
                case "eval": type = ShaderType.TessEvaluationShader; break;
                case "geom": type = ShaderType.GeometryShader; break;
                case "frag": type = ShaderType.FragmentShader; break;
                default:
                    throw new Exception("ERROR in shader " + name + ": Shader type '" + annotation + "' is not supported.");
            }

            // CREATE OPENGL OBJECT
            glname = GL.CreateShader(type);
            GL.ShaderSource(glname, text);
            GL.CompileShader(glname);
            throwExceptionOnOpenGlError("shader", name, "problem compiling shader");

            // CHECK FOR ERRORS
            int status;
            GL.GetShader(glname, ShaderParameter.CompileStatus, out status);
            if (status != 1)
                throw new Exception("ERROR in shader " + name + ":\n" + GL.GetShaderInfoLog(glname));
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
