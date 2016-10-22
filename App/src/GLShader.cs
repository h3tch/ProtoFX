using App.Glsl;
using OpenTK.Graphics.OpenGL4;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace App
{
    class GLShader : GLObject
    {
        public Shader DebugShader { get; private set; }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLShader(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
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
                default: throw err.Add($"Shader type '{anno}' is not supported.", block);
            }

            // ADD OR REMOVE DEBUG INFORMATION
            var text = FxDebugger.AddDebugCode(block, type, debugging, err);

            // CREATE OPENGL OBJECT
            glname = GL.CreateShader(type);
            GL.ShaderSource(glname, text);
            GL.CompileShader(glname);

            // CREATE CSHARP DEBUG CODE
            if (debugging)
            {
                var body = Converter.Shader2Csharp(block.Body);
                var clazz = $"{char.ToUpper(anno.First()) + anno.Skip(1).ToString()}Shader";
                var code = $"using System; namespace App.Glsl {{ class {name} : {clazz} {{{body}}}}}";
                var rs = GLCsharp.CompileFilesOrSource(new[] { code }, null, block, err);
                DebugShader = (Shader)rs.CompiledAssembly.CreateInstance(
                    block.Name, false, BindingFlags.Default, null,
                    null, CultureInfo.CurrentCulture, null);
            }

            // CHECK FOR ERRORS
            int status;
            GL.GetShader(glname, ShaderParameter.CompileStatus, out status);
            if (status != 1)
            {
                var compilerErrors = GL.GetShaderInfoLog(glname);
                throw err.Add("\n" + compilerErrors, block);
            }
            if (HasErrorOrGlError(err, block))
                throw err;
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            if (glname > 0)
            {
                GL.DeleteShader(glname);
                glname = 0;
            }
        }
    }
}
