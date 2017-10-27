using App.Glsl;
using OpenTK.Graphics.OpenGL4;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

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
        public GLShader(Compiler.Block block, Dictionary<string, object> scene, bool debugging)
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

            CompileShader(err, block, block.Body, scene);

            /// CREATE CSHARP DEBUG CODE
            
            CompilerResults rs;
            if (debugging)
            {
                if (ShaderType == ShaderType.FragmentShader)
                    InitializeFragmentShaderDebugging(err, block, scene);

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

        private void CompileShader(CompileException err, Compiler.Block block, string code, Dictionary<string, object> scene)
        {
            ParseMessage GetVendorSpecificLogParser()
            {
                var vendor = GL.GetString(StringName.Vendor);
                if (vendor.IndexOf("nvidia", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return NvidiaParser;
                return DefaultParser;
            }

            // delete existing objects
            Delete();

            // remove #fold keywords
            code = Regex.Replace(code, @"#fold.*",
                x => new string(x.Value.Select(c => (c != '\n' && c != '\r') ? ' ' : c).ToArray()));
            code = Regex.Replace(code, @"#endfold.*",
                x => new string(x.Value.Select(c => (c != '\n' && c != '\r') ? ' ' : c).ToArray()));

            // replace global variables with their actual value
            foreach (Match match in Regex.Matches(code, @"\bglobal\s*\.[\w\d]+\.[\w\d]+", RegexOptions.RightToLeft))
            {
                var global = match.Value.Split(new[] { '.' });
                var blockName = global[1];
                var propName = global[2];
                try
                {
                    var obj = (GLObject)scene.GetValue(blockName);
                    var value = obj.GetMemberValue($"Instance.{propName}");
                    code = code.Remove(match.Index, match.Length).Insert(match.Index, value.ToString());
                }
                catch
                {
                    var row = code.LineFromPosition(match.Index);
                    err.Error($"Could not process '{match.Value}'.", block.Filename, block.LineInFile + row);
                }
            }
            if (HasErrorOrGlError(err, block))
                throw err;

            // compile shader code
            glname = GL.CreateShaderProgram(ShaderType, 1, new[] { code });

            // check for errors

            GL.GetProgram(glname, GetProgramParameterName.LinkStatus, out int status);
            if (status != 1)
            {
                var log = GL.GetProgramInfoLog(glname);
                var parser = GetVendorSpecificLogParser();
                foreach (var line in log.Split('\n'))
                {
                    (var col, var row, var message) = parser(line);
                    err.Error(message, block.Filename, block.LineInFile + row);
                }
            }
            if (HasErrorOrGlError(err, block))
                throw err;
        }

        private void InitializeFragmentShaderDebugging(CompileException err, Compiler.Block block, Dictionary<string, object> scene)
        {
            // convert fragment shader into a debug shader
            var (_, dict) = GLU.InputLocationMappings(glname);
            var shaderCode = Converter.FragmentDebugShader(block.Body, dict);
            CompileShader(err, block, shaderCode, scene);
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

        delegate (int col, int row, string message) ParseMessage(string line);

        private static (int col, int row, string message) DefaultParser(string line)
        {
            return (0, 0, line);
        }

        private static (int col, int row, string message) NvidiaParser(string line)
        {
            var match = Regex.Match(line, @"\d+\(\d+\)\s*:");
            if (match.Success)
            {
                var message = line.Substring(match.Index + match.Length);
                var matches = Regex.Matches(match.Value, @"\d+");
                var col = int.Parse(matches[0].Value);
                var row = int.Parse(matches[1].Value);
                return (col, row - 1, message);
            }
            return (0, 0, line);
        }
    }
}
