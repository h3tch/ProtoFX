using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App
{
    class GLDebugger
    {
        public static string dbgImgKey = "__protogl__dbgimg";
        public static string dbgTexKey = "__protogl__dbgtex";
        public static GLPass activePass;
        private static GLImage img;
        private static GLTexture tex;
        private static int[] texUnits;
        private static int[] imgUnits;
        private static int _dbgOut;
        private static int _dbgVert;
        private static int _dbgTess;
        private static int _dbgEval;
        private static int _dbgGeom;
        private static int _dbgFrag;
        private static int _dbgComp;

        public static void InitilizeDebuging(Dict<GLObject> scene)
        {
            var imgDef =
                "type = texture2D\n" +
                "format = RGBA32f\n" +
                $"width = {1024}\n" +
                $"height = {6}\n";
            var texDef =
                $"img = {dbgImgKey}\n";
            img = new GLImage(new GLParams(dbgImgKey, "dbg", imgDef, null, scene));
            scene.Add(dbgImgKey, img);
            tex = new GLTexture(new GLParams(dbgTexKey, "dbg", texDef, null, scene));
            scene.Add(dbgTexKey, tex);
            texUnits = new int[GL.GetInteger((GetPName)All.MaxTextureImageUnits)];
            imgUnits = new int[GL.GetInteger((GetPName)All.MaxImageUnits)];
        }

        public static void Activate(GLPass pass)
        {
            activePass = pass;
            _dbgOut = GL.GetUniformLocation(pass.glname, "_dbgOut");
            _dbgVert = GL.GetUniformLocation(pass.glname, "_dbgVert");
            _dbgTess = GL.GetUniformLocation(pass.glname, "_dbgTess");
            _dbgEval = GL.GetUniformLocation(pass.glname, "_dbgEval");
            _dbgGeom = GL.GetUniformLocation(pass.glname, "_dbgGeom");
            _dbgFrag = GL.GetUniformLocation(pass.glname, "_dbgFrag");
            _dbgComp = GL.GetUniformLocation(pass.glname, "_dbgComp");
        }

        public static void Bind(GLPass caller)
        {
            if (caller != activePass || _dbgOut == 0)
                return;

            int freeUnit = GetBoundImages().LastIndexOf(x => x == 0);
            if (freeUnit < 0)
                return;

            tex.BindTex(freeUnit);
            GL.Uniform1(_dbgOut, freeUnit);

            if (_dbgVert >= 0)
                GL.Uniform2(_dbgVert, -1, -1);
            if (_dbgTess >= 0)
                GL.Uniform2(_dbgTess, -1, -1);
            if (_dbgEval >= 0)
                GL.Uniform2(_dbgEval, -1, -1);
            if (_dbgGeom >= 0)
                GL.Uniform2(_dbgGeom, -1, -1);
            if (_dbgFrag >= 0)
                GL.Uniform2(_dbgFrag, -1, -1);
            if (_dbgComp >= 0)
                GL.Uniform2(_dbgComp, -1, -1);
        }

        public static IEnumerable<string> AddDebugCode(string text, ShaderType type)
        {
            // find main function and body
            var main = Regex.Match(text, @"void\s+main\s*\(\s*\)");
            var body = text.Substring(main.Index).MatchBrace('{', '}');

            // return everything up until the main function
            yield return text.Substring(0, main.Index);
            // return debug header
            yield return '\n' + Properties.Resources.dbg + '\n';
            // return everything up until the body of the main function
            yield return text.Substring(main.Index, body.Index);

            // count number of lines before the main function body
            var count = Regex.Matches(text.Substring(0, main.Index), "\n").Count;

            // add debug code to each line in the shader
            var lines = Regex.Split(body.Value, "\n");
            for (int l = 0; l < lines.Length; l++)
            {
                yield return (l == 0 ? lines[l].Insert(1, "int _dbgIdx = 0;") : lines[l]) + '\n';
                yield return GetDebugLine(lines[l], count + l, type) + '\n';
            }

            // return the rest of the shader code
            yield return text.Substring(main.Index + body.Index + body.Length);
        }

        private static string GetDebugLine(string line, int linenumber, ShaderType type)
        {
            var ignore = new HashSet<string>(new[] {
                "bool", "int", "uint", "float", "double", "bvec2", "ivec2", "uvec2", "vec2", "dvec2",
                "bvec3", "ivec3", "uvec3", "vec3", "dvec3", "bvec4", "ivec4", "uvec4", "vec4", "dvec4",
                "mat2", "mat3", "mat4", "mat2x3", "mat2x4", "mat3x2", "mat3x4", "mat4x2", "mat4x3",
                "true", "false",
            });

            // var: word = \w[\w\d]*
            // array: <var> [...] [...] [...] [...] = <var>(\s*\[.*\])*
            // class: <array>.<array> = <array>(\s*\.\s*<array>)*
            // ignore func: <var> ( ... ) = <var>\s*\(.*\)

            // find all variables (TODO: ignore functions)
            var regexWord = @"\b\w[\w\d]*\b";
            var regexArray = regexWord + @"(\b*\[[\b\w\d\[\]]*\])*";
            var words = Regex.Matches(line, regexArray);
            if (words.Count == 0)
                return "";

            // if statement
            int shaderIdx = -1;
            string dbgLine = "if (all(equal(";
            switch (type)
            {
                case ShaderType.VertexShader:
                    shaderIdx = 0;
                    dbgLine += "_dbgVert, ivec2(gl_InstanceID, gl_VertexID)";
                    break;
                case ShaderType.TessControlShader:
                    shaderIdx = 1;
                    break;
                case ShaderType.TessEvaluationShader:
                    shaderIdx = 2;
                    break;
                case ShaderType.GeometryShader:
                    shaderIdx = 3;
                    dbgLine += "_dbgGeom, ivec2(gl_PrimitiveIDIn, gl_InvocationID)";
                    break;
                case ShaderType.FragmentShader:
                    shaderIdx = 4;
                    dbgLine += "_dbgFrag, ivec2(int(gl_FragCoord.x), int(gl_FragCoord.y))";
                    break;
                case ShaderType.ComputeShader:
                    shaderIdx = 5;
                    break;
                default:
                    return "";
            }
            dbgLine += "))) {";

            // store debug variables
            foreach (Match word in words)
                if (!ignore.Contains(word.Value))
                    dbgLine += $"_dbgStoreVar({shaderIdx}, _dbgIdx, {word.Value}, {linenumber});";
            return dbgLine + "}";
        }

        public static void BindImg(int unit, int level, bool layered, int layer, TextureAccess access,
            GpuFormat format, int name)
        {
            imgUnits[unit] = name;
            if (name > 0)
                GL.BindImageTexture(unit, name, level, layered, Math.Max(layer, 0),
                    access, (SizedInternalFormat)format);
        }

        public static void UnindImg(int unit)
        {
            imgUnits[unit] = 0;
        }

        public static void BindTex(int unit, TextureTarget target, int name)
        {
            texUnits[unit] = name;
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(target, name);
        }

        public static void UnbindTex(int unit, TextureTarget target)
        {
            BindTex(unit, target, 0);
        }

        public static int[] GetBoundTextures()
        {
            return texUnits;
        }

        public static int[] GetBoundImages()
        {
            return imgUnits;
        }
    }
}
