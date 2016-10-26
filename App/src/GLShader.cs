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
            var text = block.Body;

            // CREATE OPENGL OBJECT
            glname = GL.CreateShader(type);
            GL.ShaderSource(glname, text);
            GL.CompileShader(glname);

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

            // CREATE CSHARP DEBUG CODE
            if (debugging)
            {
                var body = Converter.Shader2Csharp(block.Body);
                var clazz = $"{char.ToUpper(anno.First()) + anno.Substring(1)}Shader";
                var code = "using System; "
                    + "namespace App.Glsl { "
                    + $"class {name} : {clazz} {{ "
                    + $"public {name}() : this(0) {{ }} "
                    + $"public {name}(int l) : base(l) {{ }} "
                    + $"{body}}}}}";
                var rs = GLCsharp.CompileFilesOrSource(new[] { code }, null, block, err, new[] { name });
                if (rs.Errors.Count == 0)
                    DebugShader = (Shader)rs.CompiledAssembly.CreateInstance(
                        $"App.Glsl.{name}", false, BindingFlags.Default, null,
                        new object[] { block.LineInFile }, CultureInfo.CurrentCulture, null);
            }

            // check for errors
            if (err.HasErrors())
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
