using OpenTK.Graphics.OpenGL4;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static App.Properties.Resources;

namespace App
{
    class FxDebugger
    {
        #region FIELDS

        public static Regex RegexDbgVar = new Regex($@"{DBG_OPEN}[\s\w\d_\.\[\] ]*{DBG_CLOSE}");
        public static Regex RegexMain = new Regex(@"void\s+main\s*\(\s*\)");
        public static Regex RegexBranch1 = new Regex(@"(if|while|for)\s*\(.*\)\s*\{");
        public static Regex RegexBranch2 = new Regex(@"(if|while|for)\s*\(.*\)\s*(?!\{).*;");
        // debug resources definitions
        private static string DbgBufKey;
        private static string DbgTexKey;
        // allocate GPU resources
        private static GLBuffer buf;
        private static GLTexture tex;
        // allocate arrays for texture and image units
        private static TexUnit[] texUnits;
        private static ImgUnit[] imgUnits;
        private static Dictionary<int, Uniforms> passes;
        public static DebugSettings Settings;
        // watch count for indexing
        private const int stage_size = 128;
        private static int OPEN_INDIC_LEN = DBG_OPEN.Length;
        private static int CLOSE_INDIC_LEN = DBG_CLOSE.Length;
        private static string[] dbgUniforms = DBG_UNIFORMS.Split('\n').Select(x => x.Trim()).ToArray();
        private static string[] dbgConditions = DBG_CONDITIONS.Split('\n').Select(x => x.Trim()).ToArray();
        private static int dbgVarCount;

        #endregion

        /// <summary>
        /// Setup debugger. Needs to be called 
        /// after the OpenGL context was created.
        /// </summary>
        public static void Instantiate()
        {
            // debug resources definitions
            DbgBufKey = "__dbgbuf__";
            DbgTexKey = "__dbgtex__";
            // allocate arrays for texture and image units
            texUnits = new TexUnit[GL.GetInteger((GetPName)All.MaxTextureImageUnits)];
            imgUnits = new ImgUnit[GL.GetInteger((GetPName)All.MaxImageUnits)];
            passes = new Dictionary<int, Uniforms>();
            Settings = new DebugSettings();
        }
        
        /// <summary>
        /// (Re)initialize the debugger. This method must
        /// be called whenever a program is compiled.
        /// </summary>
        /// <param name="scene"></param>
        public static void Initilize(Dict scene)
        {
            // allocate GPU resources
            buf = new GLBuffer(DbgBufKey, "dbg", BufferUsageHint.DynamicRead, stage_size * 6 * 16);
            tex = new GLTexture(DbgTexKey, "dbg", GpuFormat.Rgba32f, buf, null);

#if DEBUG   // add to scene for debug inspection
            scene.Add(DbgBufKey, buf);
            scene.Add(DbgTexKey, tex);
#endif
            passes.Clear();
            // reset watch count for indexing in debug mode
            dbgVarCount = 0;
        }

        /// <summary>
        /// Bind <code>GLPass</code> for debugging.
        /// </summary>
        /// <param name="pass">The pass to bind for debugging.</param>
        /// <param name="frame">The current frame ID to generate debug information for.</param>
        public static void Bind(GLPass pass, int frame)
        {
            // get debug uniforms for the pass
            Uniforms unif;
            if (!passes.TryGetValue(pass.glname, out unif))
                passes.Add(pass.glname, unif = new Uniforms(pass));
            // set shader debug uniforms
            unif.Bind(Settings, frame);
        }

        /// <summary>
        /// End debugging of the specified pass. Must be
        /// the same as when calling <code>Bind</code>.
        /// </summary>
        /// <param name="pass">The pass specified when calling <code>Bind</code>.</param>
        public static void Unbind(GLPass pass)
        {
            Uniforms unif;
            if (passes.TryGetValue(pass.glname, out unif))
                unif.Unbind();
        }

        #region TEXTURE AND IMAGE BINDING

        /// <summary>
        /// Bind image/texture to image load/store unit.
        /// </summary>
        /// <param name="unit">binding unit</param>
        /// <param name="level">level of the texture/image to be bound</param>
        /// <param name="layered">is the texture layered (e.g. like a cube map is)</param>
        /// <param name="layer">layer of the layered texture to be bound</param>
        /// <param name="access">how shaders can access the texture resource</param>
        /// <param name="format">texture format of the texture</param>
        /// <param name="glname">texture/image to be bound</param>
        public static void BindImg(int unit, int level, bool layered, int layer,
            TextureAccess access, GpuFormat format, int glname)
        {
            // bind image to image load/store unit
            if ((imgUnits[unit].glname = glname) > 0)
            {
                imgUnits[unit].access = access;
                imgUnits[unit].format = (SizedInternalFormat)format;
            }
            GL.BindImageTexture(unit, glname, level, layered, Math.Max(layer, 0),
                imgUnits[unit].access, imgUnits[unit].format);
        }

        /// <summary>
        /// Bind image/texture to texture unit.
        /// </summary>
        /// <param name="unit">binding unit</param>
        /// <param name="target">texture target type</param>
        /// <param name="glname">texture/image to be bound</param>
        public static void BindTex(int unit, TextureTarget target, int glname)
        {
            // bind texture to texture unit
            if ((texUnits[unit].glname = glname) > 0)
                texUnits[unit].target = target;
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            if (texUnits[unit].target != 0)
                GL.BindTexture(texUnits[unit].target, glname);
        }

        #endregion

        #region DEBUG VARIABLE

        /// <summary>
        /// Convert debug variable into a readable string.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string DebugVariableToString(Array array, string floatFomat = "{0:0.0000}")
        {
            string[] titles = null;
            if (array.Rank == 3)
                titles = new[] { "Depth {0}\n" };

            // is the element type a floating point type
            var arrayType = array.GetType().GetElementType();
            var isFloat = arrayType == typeof(float) || arrayType == typeof(double);

            // convert array to string array
            var strArray = array.ToStringArray(isFloat ? floatFomat : "{0}");
            var max = strArray.ToEnumerable().Select(x => ((string)x).Length).Max();

            // recursively convert each dimension of the array into a string
            var str = new StringBuilder((max + 5) * array.Length);
            DebugVariableToString(str, max + 4, strArray, new int[array.Rank], titles);
            return str.ToString();
        }

        /// <summary>
        /// Recursive method to convert a (possibly) multi
        /// dimensional debug variable into a readable string.
        /// </summary>
        /// <param name="output">Output string.</param>
        /// <param name="colWidth">Minimum column with of the strings.</param>
        /// <param name="array">Debug variable to be converted.</param>
        /// <param name="idx">Current index vector into the multi dim. array.</param>
        /// <param name="titles">Titles for each dimension.</param>
        /// <param name="curDim">Current dimension to convert.</param>
        private static void DebugVariableToString(StringBuilder output, int colWidth, Array array,
            int[] idx, string[] titles = null, int curDim = 0)
        {
            // get size of current dimension
            int dimSize = array.GetLength(curDim);

            // for each element in this dimension
            for (int i = 0; i < dimSize; i++)
            {
                // add a title to the dimension if one has been specified
                if (titles != null && curDim < titles.Length && titles[curDim] != null)
                    output.Append(string.Format(titles[curDim], i));

                // set index of current dimension
                idx[curDim] = i;

                // if the array has another dimension
                if (array.Rank > curDim + 1)
                {
                    // output all values of this dimension
                    DebugVariableToString(output, colWidth, array, idx, titles, curDim + 1);
                    if (i + 1 < dimSize)
                        output.Append('\n');
                }
                else
                {
                    // write value to output
                    string val = (string)array.GetValue(idx);
                    output.Append(new string(' ', i != 0 ? colWidth - val.Length : 0) + val);
                }
            }
        }

        /// <summary>
        /// Get debug variable from the specified text position in the code editor.
        /// </summary>
        /// <param name="editor">Source code editor.</param>
        /// <param name="position">Position in the code.</param>
        /// <returns>Returns the debug variable at the specified position or
        /// the default value if no debug variable could be found.</returns>
        public static DbgVar GetDebugVariableFromPosition(CodeEditor editor, int position)
        {
            // find all debug variables
            var vars = RegexDbgVar.Matches(editor.Text);

            for (int i = 0; i < vars.Count; i++)
            {
                var varLine = editor.LineFromPosition(vars[i].Index);
                // is the debug variable in the same line
                if (vars[i].Index <= position && position <= vars[i].Index + vars[i].Length)
                    return new DbgVar(i, vars[i].Index, varLine, vars[i].Value);
            }
            return null;
        }

        /// <summary>
        /// Get debug variable from the specified text line in the code editor.
        /// </summary>
        /// <param name="editor">Source code editor.</param>
        /// <param name="line">Zero-based line number in the code.</param>
        /// <returns>Returns all debug variables in the specified line or
        /// <code>null</code> if no debug variable could be found.</returns>
        public static IEnumerable<DbgVar> GetDebugVariablesFromLine(CodeEditor editor, int line, int length = 1)
        {
            // find all debug variables
            var vars = RegexDbgVar.Matches(editor.Text);
            var from = line;
            var to = line + length;

            for (int i = 0; i < vars.Count; i++)
            {
                var varLine = editor.LineFromPosition(vars[i].Index);
                // is the debug variable in the same line
                if (from <= varLine && varLine < to)
                    yield return new DbgVar(i, vars[i].Index, varLine, vars[i].Value);
            }
        }

        /// <summary>
        /// Get debug variable value from the specified frame.
        /// </summary>
        /// <param name="ID">Debug variable ID.</param>
        /// <param name="frame">[OPTIONAL] The frame in which the debug variable was
        /// created. If <code>frame</code> is negative this value will be ignored.</param>
        /// <returns>Returns the debug variable as a multi-dimensional array.</returns>
        public static Array GetDebugVariableValue(int ID, int frame = -1)
        {
            // for each pass
            foreach (Uniforms unif in passes.Values)
            {
                // not debug output in this pass?
                if (unif.data == null)
                    continue;

                // to be able to read basic types the binary array
                // needs to be converted into a BinaryReader
                var mem = new BinaryReader(new MemoryStream(unif.data));

                // for each shader stage
                for (int stage = 0; stage < 6; stage++)
                {
                    // offset of the current stage
                    mem.Seek(16 * stage_size * stage, SeekOrigin.Begin);

                    // READ DEBUG BUFFER HEADER
                    // number of debug rows (vec4)
                    int numDbgRows = mem.ReadInt32();
                    int dbgFrame = mem.ReadInt32();
                    mem.Seek(8);

                    // no debug information for
                    // this stage in this frame
                    if (frame >= 0 && frame != dbgFrame)
                        continue;

                    // for each debug variable in this stage
                    // -- i += 1 + rows ... one header row + <rows> data rows
                    for (int i = 0, rows; i < numDbgRows; i += 1 + rows)
                    {
                        // READ DEBUG VAR HEADER
                        int type = mem.ReadInt32();
                        rows = mem.ReadInt32();
                        int cols = mem.ReadInt32();
                        int varID = mem.ReadInt32();

                        // this is not the debug variable we want
                        if (varID != ID)
                        {
                            // skip debug variable
                            mem.Seek(16 * rows);
                            continue;
                        }

                        // convert and return the debug variable
                        switch (type)
                        {
                            case 1/*BOOL */:
                            case 2/*INT  */: return mem?.ReadArray<int>(rows, cols, 16);
                            case 3/*UINT */: return mem?.ReadArray<uint>(rows, cols, 16);
                            case 4/*FLOAT*/: return mem?.ReadArray<float>(rows, cols, 16);
                        }
                    }
                }
            }
            
            return null;
        }

        #endregion

        #region DEBUG CODE GENERATION FOR GLSL SHADERS

        /// <summary>
        /// Add debug code to GLSL shader.
        /// </summary>
        /// <param name="glsl">GLSL shader code.</param>
        /// <param name="type">GLSL shader type.</param>
        /// <param name="debug">Add debug information if true.
        /// Otherwise remove debug indicators.</param>
        /// <returns>Returns the new GLSL code with debug code added.</returns>
        public static string AddDebugCode(Compiler.Block block, ShaderType type, bool debug,
            CompileException err)
        {
            var glsl = block.Text.BraceMatch('{', '}').Value;
            glsl = glsl.Substring(1, glsl.Length - 2);

            // find main function and body
            var main = RegexMain.Match(glsl);
            var head = glsl.Substring(0, main.Index);
            var body = glsl.Substring(main.Index).BraceMatch('{', '}');

            // replace WATCH functions
            var watch = RegexDbgVar.Matches(body.Value);
            var invalid = new[] {
                RegexBranch1.Matches(body.Value).Cast<Match>(),
                RegexBranch2.Matches(body.Value).Cast<Match>()
            }.Cat();
            if (watch.Count == 0)
                return glsl;

            // remove WATCH indicators for runtime code
            var runBody = body.Value.Replace(DBG_OPEN, "").Replace(DBG_CLOSE, "");

            // if debugging is disabled, there is no need to generate debug code
            if (debug == false)
                return head + main.Value + runBody;

            var dbgBody = string.Copy(body.Value);

            int insertOffset = 0;
            foreach (Match v in watch)
            {
                // if the watch variable lies within an invalid area, ignore it
                if (invalid.Any(x => x.Index <= v.Index + v.Length && v.Index <= x.Index + x.Length))
                {
                    dbgVarCount++;
                    continue;
                }
                // get debug variable name
                var varname = v.Value.Substring(DBG_OPEN.Length,
                    v.Value.Length - DBG_OPEN.Length - DBG_CLOSE.Length);
                // get next newline-indicator
                int newcmd = dbgBody.IndexOf(';', v.Index + insertOffset);
                // insert debug code before newline-indicator
                var insertString = $";_dbgIdx = _dbgStoreVar(_dbgIdx, {varname}, {dbgVarCount++})";
                dbgBody = dbgBody.Insert(newcmd, insertString);
                insertOffset += insertString.Length;
            }
            dbgBody = dbgBody.Replace(DBG_OPEN, "").Replace(DBG_CLOSE, "");

            // replace 'return' keyword with '{store(info); return;}'
            dbgBody = dbgBody.Replace("return", "{_dbgStore(0, ivec2(_dbgIdx-1, _dbgFrame));return;}");

            // replace 'discard' keyword with '{store(discard info); return;}'
            dbgBody = dbgBody.Replace("discard", "{_dbgStore(0, ivec2(0, _dbgFrame));discard;}");

            // gather debug information
            int stage_index;
            switch (type)
            {
                case ShaderType.VertexShader: stage_index = 0; break;
                case ShaderType.TessControlShader: stage_index = 1; break;
                case ShaderType.TessEvaluationShader: stage_index = 2; break;
                case ShaderType.GeometryShader: stage_index = 3; break;
                case ShaderType.FragmentShader: stage_index = 4; break;
                case ShaderType.ComputeShader: stage_index = 5; break;
                default: throw new InvalidEnumArgumentException();
            }

            // insert debug information
            var rsHead = Properties.Resources.dbg
                .Replace($"{DBG_OPEN}stage offset{DBG_CLOSE}", (stage_size * stage_index).ToString());
            var rsBody = Properties.Resources.dbgBody
                .Replace($"{DBG_OPEN}debug uniform{DBG_CLOSE}", dbgUniforms[stage_index])
                .Replace($"{DBG_OPEN}debug frame{DBG_CLOSE}", "int _dbgFrame")
                .Replace($"{DBG_OPEN}debug condition{DBG_CLOSE}", dbgConditions[stage_index])
                .Replace($"{DBG_OPEN}debug code{DBG_CLOSE}", dbgBody)
                .Replace($"{DBG_OPEN}runtime code{DBG_CLOSE}", runBody);
            
            return head + '\n' + rsHead + '\n' + rsBody;
        }

        #endregion

        #region DEBUG UNIFORMS

        private class Uniforms
        {
            // output image store unit
            public int dbgOut;
            // element selection in shaders
            public int dbgVert;
            public int dbgTess;
            public int dbgEval;
            public int dbgGeom;
            public int dbgFrag;
            public int dbgComp;
            // render frame index
            public int dbgFrame;
            // debug data output
            public byte[] data;
            // unused image unit
            private int unit;

            /// <summary>
            /// Create debug uniform class for the specified pass.
            /// </summary>
            /// <param name="pass"></param>
            public Uniforms(GLPass pass)
            {
                dbgOut = GL.GetUniformLocation(pass.glname, "_dbgOut");
                dbgVert = GL.GetUniformLocation(pass.glname, "_dbgVert");
                dbgTess = GL.GetUniformLocation(pass.glname, "_dbgTess");
                dbgEval = GL.GetUniformLocation(pass.glname, "_dbgEval");
                dbgGeom = GL.GetUniformLocation(pass.glname, "_dbgGeom");
                dbgFrag = GL.GetUniformLocation(pass.glname, "_dbgFrag");
                dbgComp = GL.GetUniformLocation(pass.glname, "_dbgComp");
                dbgFrame = GL.GetUniformLocation(pass.glname, "_dbgFrame");
                unit = -1;
                data = null;
            }

            /// <summary>
            /// Bind the debug uniform class.
            /// </summary>
            /// <param name="settings"></param>
            /// <param name="frame"></param>
            public void Bind(DebugSettings settings, int frame)
            {
                if (dbgOut <= 0)
                    return;
                
                // get last free unused image unit
                unit = imgUnits.LastIndexOf(x => x.glname == 0);
                if (unit < 0)
                    return;
                
                // bind texture to image unit
                GLTexture.BindImg(unit, tex, 0, 0, TextureAccess.WriteOnly, GpuFormat.Rgba32f);
                GL.Uniform1(dbgOut, unit);
                
                // set debug uniform
                if (dbgVert >= 0)
                    GL.Uniform2(dbgVert, settings.vs_InstanceID, settings.vs_VertexID);
                if (dbgTess >= 0)
                    GL.Uniform2(dbgTess, settings.ts_InvocationID, settings.ts_PrimitiveID);
                if (dbgEval >= 0)
                    GL.Uniform1(dbgEval, settings.ts_PrimitiveID);
                if (dbgGeom >= 0)
                    GL.Uniform2(dbgGeom, settings.gs_InvocationID, settings.gs_PrimitiveIDIn);
                if (dbgFrag >= 0)
                    GL.Uniform4(dbgFrag, settings.fs_FragCoord[0], settings.fs_FragCoord[1],
                        settings.fs_Layer, settings.fs_ViewportIndex);
                if (dbgComp >= 0)
                    GL.Uniform3(dbgComp,
                        (uint)settings.cs_GlobalInvocationID[0],
                        (uint)settings.cs_GlobalInvocationID[1],
                        (uint)settings.cs_GlobalInvocationID[2]);
                if (dbgFrame >= 0)
                    GL.Uniform1(dbgFrame, frame);
            }

            /// <summary>
            /// Unbind debug uniform class and read debug variable data.
            /// </summary>
            public void Unbind()
            {
                // if the texture buffer was bound to a unit
                if (unit < 0)
                    return;
                // unbind texture buffer
                GLTexture.BindImg(unit, null);
                // read generated debug information
                buf.Read(ref data);
            }
        }

        #endregion

        #region DEBUG SETTINGS

        public class DebugSettings
        {
            [Category("Vertex Shader"), DisplayName("InstanceID"),
             Description("the index of the current instance when doing some form of instanced " +
                "rendering. The instance count always starts at 0, even when using base instance " +
                "calls. When not using instanced rendering, this value will be 0.")]
            public int vs_InstanceID { get; set; } = 0;

            [Category("Vertex Shader"), DisplayName("VertexID"),
             Description("the index of the vertex currently being processed. When using non-indexed " +
                "rendering, it is the effective index of the current vertex (the number of vertices " +
                "processed + the first​ value). For indexed rendering, it is the index used to fetch " +
                "this vertex from the buffer.")]
            public int vs_VertexID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("InvocationID"),
             Description("the index of the shader invocation within this patch. An invocation " +
                "writes to per-vertex output variables by using this to index them.")]
            public int ts_InvocationID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("PrimitiveID"),
             Description("the index of the current patch within this rendering command.")]
            public int ts_PrimitiveID { get; set; } = 0;

            [Category("Geometry Shader"), DisplayName("InvocationID"),
             Description("the current instance, as defined when instancing geometry shaders.")]
            public int gs_InvocationID { get; set; } = 0;

            [Category("Geometry Shader"), DisplayName("PrimitiveIDIn"),
             Description("the current input primitive's ID, based on the number of primitives " +
                "processed by the GS since the current drawing command started.")]
            public int gs_PrimitiveIDIn { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("FragCoord"),
             Description("The location of the fragment in window space. The X and Y components " +
                "are the window-space position of the fragment.")]
            public int[] fs_FragCoord { get; set; } = new int[2] { 0, 0 };

            [Category("Fragment Shader"), DisplayName("Layer"),
             Description("is either 0 or the layer number for this primitive output by the Geometry Shader.")]
            public int fs_Layer { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("ViewportIndex"),
             Description("is either 0 or the viewport index for this primitive output by the Geometry Shader.")]
            public int fs_ViewportIndex { get; set; } = 0;

            [Category("Compute Shader"), DisplayName("GlobalInvocationID"),
             Description("uniquely identifies this particular invocation of the compute shader " +
                "among all invocations of this compute dispatch call. It's a short-hand for the " +
                "math computation: gl_WorkGroupID * gl_WorkGroupSize + gl_LocalInvocationID;")]
            public int[] cs_GlobalInvocationID { get; set; } = new int[3] { 0, 0, 0 };
        }

        #endregion

        #region UTIL STRUCTURE

        public class DbgVar
        {
            public int ID;
            public int Position;
            public int Line;
            public string Name;

            public DbgVar(int id, int position, int line, string match)
            {
                ID = id;
                Position = position + OPEN_INDIC_LEN;
                Line = line;
                Name = match.Substring(OPEN_INDIC_LEN, match.Length - OPEN_INDIC_LEN - CLOSE_INDIC_LEN);
            }
        }

        private struct TexUnit
        {
            public int glname;
            public TextureTarget target;
        }

        private struct ImgUnit
        {
            public int glname;
            public TextureAccess access;
            public SizedInternalFormat format;
        }

        #endregion
    }
}
