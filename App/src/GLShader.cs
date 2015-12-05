using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLShader : GLObject
    {
        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="dir">Directory of the tech-file.</param>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="anno">Annotation used for special initialization.</param>
        /// <param name="text">Text block specifying the object commands.</param>
        /// <param name="classes">Collection of scene objects.</param>
        public GLShader(string dir, string name, string anno, string text, Dict<GLObject> classes)
            : base(name, anno)
        {
            var err = new CompileException($"shader '{name}'");

            // CREATE OPENGL OBJECT
            ShaderType type;
            switch (anno)
            {
                case "vert": type = ShaderType.VertexShader; break;
                case "tess": type = ShaderType.TessControlShader; break;
                case "eval": type = ShaderType.TessEvaluationShader; break;
                case "geom": type = ShaderType.GeometryShader; break;
                case "frag": type = ShaderType.FragmentShader; break;
                case "comp": type = ShaderType.ComputeShader; break;
                default:
                    throw err.Add($"Shader type '{anno}' is not supported.");
            }

            // CREATE OPENGL OBJECT
            glname = GL.CreateShader(type);
            GL.ShaderSource(glname, text);
            GL.CompileShader(glname);

            // CHECK FOR ERRORS
            int status;
            GL.GetShader(glname, ShaderParameter.CompileStatus, out status);
            if (status != 1)
                throw err.Add("\n" + GL.GetShaderInfoLog(glname));
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
