using App.Glsl;
using OpenTK.Graphics.OpenGL4;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace App
{
    class GLShader : GLObject
    {
        #region Fields

        internal Shader DebugShader { get; private set; }
        internal int GlDebugShader { get; private set; }
        internal ShaderType ShaderType;

        #endregion

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLShader(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"shader '{Name}'");

            /// COMPILE AND LINK SHADER INTO A SHADER PROGRAM

            switch (Anno)
            {
                case "comp": ShaderType = ShaderType.ComputeShader; break;
                case "vert": ShaderType = ShaderType.VertexShader; break;
                case "tess": ShaderType = ShaderType.TessControlShader; break;
                case "eval": ShaderType = ShaderType.TessEvaluationShader; break;
                case "geom": ShaderType = ShaderType.GeometryShader; break;
                case "frag": ShaderType = ShaderType.FragmentShader; break;
                default: throw err.Error($"Shader type '{Anno}' is not supported.", block);
            }

            CompileShader(err, block, block.Body);

            /// CREATE CSHARP DEBUG CODE
            
            CompilerResults rs;
            if (debugging)
            {
                if (ShaderType == ShaderType.FragmentShader)
                    InitializeFragmentShaderDebugging(err, block);

                var code = Converter.Shader2Class(ShaderType, Name, block.Body, block.BodyIndex);
                rs = GLCsharp.CompileFilesOrSource(new[] { code }, null, block, err, new[] { Name });
                if (rs.Errors.Count == 0)
                    DebugShader = (Shader)rs.CompiledAssembly.CreateInstance(
                        $"App.Glsl.{Name}", false, BindingFlags.Default, null,
                        new object[] { block.LineInFile },
                        CultureInfo.CurrentCulture, null);
            }

            // check for errors
            if (err.HasErrors)
                throw err;
        }

        private void CompileShader(CompileException err, Compiler.Block block, string code)
        {
            // delete existing objects
            Delete();

            // compile shader code
            glname = GL.CreateShaderProgram(ShaderType, 1, new[] { code });

            // check for errors

            GL.GetProgram(glname, GetProgramParameterName.LinkStatus, out int status);
            if (status != 1)
                err.Error($"\n{GL.GetProgramInfoLog(glname)}", block);
            if (HasErrorOrGlError(err, block))
                throw err;
        }

        private void InitializeFragmentShaderDebugging(CompileException err, Compiler.Block block)
        {
            // convert fragment shader into a debug shader
            var (_, dict) = GLU.InputLocationMappings(glname);
            var shaderCode = Converter.FragmentDebugShader(block.Body, dict);
            CompileShader(err, block, shaderCode);
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            if (glname > 0)
            {
                GL.DeleteProgram(glname);
                glname = 0;
            }
            if (DebugShader != null)
            {
                DebugShader.Delete();
                DebugShader = null;
            }
        }
    }
}
