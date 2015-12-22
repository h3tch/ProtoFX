using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App
{
    class GLDebugger
    {
        #region FIELDS
        // debug resources definitions
        private static string dbgImgKey = "__protogl__dbgimg";
        private static string dbgTexKey = "__protogl__dbgtex";
        private static string dbgImgDef = "type = texture2D\n" +
                                          "format = RGBA32f\n" +
                                          $"width = {1024}\n" +
                                          $"height = {6}\n";
        // allocate GPU resources
        private static GLImage img = new GLImage(new GLParams(dbgImgKey, "dbg", dbgImgDef));
        private static GLTexture tex = new GLTexture(new GLParams(dbgTexKey, "dbg"), null, null, img);
        // allocate arrays for texture and image units
        private static int[] texUnits = new int[GL.GetInteger((GetPName)All.MaxTextureImageUnits)];
        private static int[] imgUnits = new int[GL.GetInteger((GetPName)All.MaxImageUnits)];
        private static Dictionary<int, Uniforms> passes = new Dictionary<int, Uniforms>();
        #endregion

        public static void Initilize(Dict<GLObject> scene)
        {
            // reset scene
            scene.Add(dbgImgKey, img);
            scene.Add(dbgTexKey, tex);
            passes.Clear();
        }

        public static void Bind(GLPass pass)
        {
            // get debug uniforms for the pass
            Uniforms unif;
            if (!passes.TryGetValue(pass.glname, out unif))
                passes.Add(pass.glname, unif = new Uniforms(pass));
            // set shader debug uniforms
            unif.Bind(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
        }

        #region TEXTURE AND IMAGE BINDING
        public static void BindImg(int unit, int level, bool layered, int layer, TextureAccess access,
            GpuFormat format, int name)
        {
            // bind image to image load/store unit
            imgUnits[unit] = name;
            GL.BindImageTexture(unit, name, level, layered, Math.Max(layer, 0), access,
                (SizedInternalFormat)format);
        }
        
        public static void BindTex(int unit, TextureTarget target, int name)
        {
            // bind texture to texture unit
            texUnits[unit] = name;
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(target, name);
        }

        public static void UnbindImg(int unit) => imgUnits[unit] = 0;

        public static void UnbindTex(int unit, TextureTarget target) => BindTex(unit, target, 0);

        public static int[] GetBoundTextures() => texUnits;

        public static int[] GetBoundImages() => imgUnits;
        #endregion

        #region DEBUG CODE GENERATION FOR GLSL SHADERS
        public static IEnumerable<string> AddDebugCode(string glsl, ShaderType type)
        {
            // find main function and body
            var main = Regex.Match(glsl, @"void\s+main\s*\(\s*\)");
            var body = glsl.Substring(main.Index).MatchBrace('{', '}');

            // return everything up until the main function
            yield return glsl.Substring(0, main.Index);
            // return debug header
            yield return '\n' + Properties.Resources.dbg + '\n';
            // return everything up until the body of the main function
            yield return glsl.Substring(main.Index, body.Index);

            // count number of lines before the main function body
            var count = Regex.Matches(glsl.Substring(0, main.Index), "\n").Count;

            // add debug code to each line in the shader
            var lines = Regex.Split(body.Value, "\n");
            for (int l = 0; l < lines.Length; l++)
            {
                yield return (l == 0 ? lines[l].Insert(1, "int _dbgIdx = 0;") : lines[l]) + '\n';
                yield return GetDebugLine(lines[l], count + l, type) + '\n';
            }

            // return the rest of the shader code
            yield return glsl.Substring(main.Index + body.Index + body.Length);
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
        #endregion

        private struct Uniforms
        {
            public int dbgOut;
            public int dbgVert;
            public int dbgTess;
            public int dbgEval;
            public int dbgGeom;
            public int dbgFrag;
            public int dbgComp;

            public Uniforms(GLPass pass)
            {
                dbgOut = GL.GetUniformLocation(pass.glname, "_dbgOut");
                dbgVert = GL.GetUniformLocation(pass.glname, "_dbgVert");
                dbgTess = GL.GetUniformLocation(pass.glname, "_dbgTess");
                dbgEval = GL.GetUniformLocation(pass.glname, "_dbgEval");
                dbgGeom = GL.GetUniformLocation(pass.glname, "_dbgGeom");
                dbgFrag = GL.GetUniformLocation(pass.glname, "_dbgFrag");
                dbgComp = GL.GetUniformLocation(pass.glname, "_dbgComp");
            }

            public void Bind(
                int vInstID, int vVertID,
                int tPrimID, int tInvocID,
                int ePrimID, int eInvocID,
                int gPrimID, int gInvocID,
                int fFragX, int fFragY)
            {
                if (dbgOut == 0)
                    return;

                int freeUnit = GetBoundImages().LastIndexOf(x => x == 0);
                if (freeUnit < 0)
                    return;

                tex.BindTex(freeUnit);
                GL.Uniform1(dbgOut, freeUnit);

                if (dbgVert >= 0)
                    GL.Uniform2(dbgVert, vInstID, vVertID);
                if (dbgTess >= 0)
                    GL.Uniform2(dbgTess, tPrimID, tInvocID);
                if (dbgEval >= 0)
                    GL.Uniform2(dbgEval, ePrimID, eInvocID);
                if (dbgGeom >= 0)
                    GL.Uniform2(dbgGeom, gPrimID, gInvocID);
                if (dbgFrag >= 0)
                    GL.Uniform2(dbgFrag, fFragX, fFragY);
                if (dbgComp >= 0)
                    GL.Uniform2(dbgComp, -1, -1);
            }
        }
    }
}
