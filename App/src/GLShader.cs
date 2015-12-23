using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLShader : GLObject
    {
        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLShader(GLParams @params) : base(@params)
        {
            var err = new CompileException($"shader '{@params.name}'");

            // CREATE OPENGL OBJECT
            ShaderType type;
            switch (@params.anno)
            {
                case "vert": type = ShaderType.VertexShader; break;
                case "tess": type = ShaderType.TessControlShader; break;
                case "eval": type = ShaderType.TessEvaluationShader; break;
                case "geom": type = ShaderType.GeometryShader; break;
                case "frag": type = ShaderType.FragmentShader; break;
                case "comp": type = ShaderType.ComputeShader; break;
                default: throw err.Add($"Shader type '{@params.anno}' is not supported.");
            }

            // ADD DEBUG INFORMATION
            if (@params.debuging)
                @params.text = GLDebugger.AddDebugCode(@params.text, type).Merge("");

            // CREATE OPENGL OBJECT
            glname = GL.CreateShader(type);
            GL.ShaderSource(glname, @params.text);
            GL.CompileShader(glname);

            // CHECK FOR ERRORS
            int status;
            GL.GetShader(glname, ShaderParameter.CompileStatus, out status);
            if (status != 1)
            {
                var compilerErrors = GL.GetShaderInfoLog(glname);
                throw err.Add("\n" + compilerErrors);
            }
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
