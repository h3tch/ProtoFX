using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLShader : GLObject
    {
        public GLShader(string dir, string name, string annotation, string text, Dict<GLObject> classes)
            : base(name, annotation)
        {
            var err = new GLException($"shader '{name}'");

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
                    err.Throw($"Shader type '{annotation}' is not supported.");
                    return;
            }

            // CREATE OPENGL OBJECT
            glname = GL.CreateShader(type);
            GL.ShaderSource(glname, text);
            GL.CompileShader(glname);

            // CHECK FOR ERRORS
            int status;
            GL.GetShader(glname, ShaderParameter.CompileStatus, out status);
            if (status != 1)
                err.Throw("\n" + GL.GetShaderInfoLog(glname));
            if (HasErrorOrGlError(err))
                throw err;
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
