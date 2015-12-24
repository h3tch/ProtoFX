using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace App
{
    class GLDebugger
    {
        #region FIELDS
        // debug resources definitions
        private static string dbgImgKey;
        private static string dbgTexKey;
        private static string dbgImgDef;
        // allocate GPU resources
        private static GLImage img;
        private static GLTexture tex;
        // allocate arrays for texture and image units
        private static int[] texUnits;
        private static int[] imgUnits;
        private static Dictionary<int, Uniforms> passes;
        public static DebugSettings settings;
        #endregion

        public static void Instantiate()
        {
            // debug resources definitions
            dbgImgKey = "__protogl__dbgimg";
            dbgTexKey = "__protogl__dbgtex";
            dbgImgDef = "type = texture2D\n" +
                        "format = RGBA32f\n" +
                        $"width = {1024}\n" +
                        $"height = {6}\n";
            // allocate GPU resources
            img = new GLImage(new GLParams(dbgImgKey, "dbg", dbgImgDef));
            tex = new GLTexture(new GLParams(dbgTexKey, "dbg"), null, null, img);
            // allocate arrays for texture and image units
            texUnits = new int[GL.GetInteger((GetPName)All.MaxTextureImageUnits)];
            imgUnits = new int[GL.GetInteger((GetPName)All.MaxImageUnits)];
            passes = new Dictionary<int, Uniforms>();
            settings = new DebugSettings();
        }

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

        #region DEBUG SETTINGS
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

        public class DebugSettings
        {
            [Category("Vertex Shader"), DisplayName("gl_InstanceID"),
             Description("the index of the current instance when doing some form of instanced " +
                "rendering. The instance count always starts at 0, even when using base instance " +
                "calls. When not using instanced rendering, this value will be 0.")]
            public int vs_InstanceID { get; set; } = 0;

            [Category("Vertex Shader"), DisplayName("gl_VertexID"),
             Description("the index of the vertex currently being processed. When using non-indexed " +
                "rendering, it is the effective index of the current vertex (the number of vertices " +
                "processed + the first​ value). For indexed rendering, it is the index used to fetch " +
                "this vertex from the buffer.")]
            public int vs_VertexID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("gl_InvocationID"),
             Description("the index of the shader invocation within this patch. An invocation " +
                "writes to per-vertex output variables by using this to index them.")]
            public int ts_InvocationID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("gl_PrimitiveID"),
             Description("the index of the current patch within this rendering command.")]
            public int ts_PrimitiveID { get; set; } = 0;

            [Category("Geometry Shader"), DisplayName("gl_InvocationID"),
             Description("the current instance, as defined when instancing geometry shaders.")]
            public int gs_InvocationID { get; set; } = 0;

            [Category("Geometry Shader"), DisplayName("gl_PrimitiveIDIn"),
             Description("the current input primitive's ID, based on the number of primitives " +
                "processed by the GS since the current drawing command started.")]
            public int gs_PrimitiveIDIn { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("gl_FragCoord"),
             Description("The location of the fragment in window space. The X and Y components " +
                "are the window-space position of the fragment.")]
            public int[] fs_FragCoord { get; set; } = new int[2] { 0, 0 };

            [Category("Fragment Shader"), DisplayName("gl_PrimitiveID"),
             Description("This value is the index of the current primitive being rendered by this " +
                "drawing command. This includes any tessellation applied to the mesh, so each " +
                "individual primitive will have a unique index. However, if a Geometry Shader is " +
                "active, then the gl_PrimitiveID​ is exactly and only what the GS provided as output. " +
                "Normally, gl_PrimitiveID​ is guaranteed to be unique, so if two FS invocations have " +
                "the same primitive ID, they come from the same primitive. But if a GS is active and " +
                "outputs non - unique values, then different fragment shader invocations for different " +
                "primitives will get the same value.If the GS did not output a value for gl_PrimitiveID​, " +
                "then the fragment shader gets an undefined value.")]
            public int fs_PrimitiveID { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("gl_Layer"),
             Description("is either 0 or the layer number for this primitive output by the Geometry Shader.")]
            public int fs_Layer { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("gl_ViewportIndex"),
             Description("is either 0 or the viewport index for this primitive output by the Geometry Shader.")]
            public int fs_ViewportIndex { get; set; } = 0;

            [Category("Compute Shader"), DisplayName("gl_GlobalInvocationID"),
             Description("uniquely identifies this particular invocation of the compute shader " +
                "among all invocations of this compute dispatch call. It's a short-hand for the " +
                "math computation: gl_WorkGroupID * gl_WorkGroupSize + gl_LocalInvocationID;")]
            public int[] cs_GlobalInvocationID { get; set; } = new int[3] { 0, 0, 0 };
        }
        #endregion
    }
}
