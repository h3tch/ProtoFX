using App.Glsl;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using static OpenTK.Graphics.OpenGL4.ProgramStageMask;
using static OpenTK.Graphics.OpenGL4.TransformFeedbackMode;
using static System.Reflection.BindingFlags;
using ElementType = OpenTK.Graphics.OpenGL4.DrawElementsType;
using PrimType = OpenTK.Graphics.OpenGL4.PrimitiveType;
using VertoutPrimType = OpenTK.Graphics.OpenGL4.TransformFeedbackPrimitiveType;

namespace App
{
    class GLPass : FXPerf
    {
        #region FIELDS

        [FxField] private string Vert = null;
        [FxField] private string Tess = null;
        [FxField] private string Eval = null;
        [FxField] private string Geom = null;
        [FxField] private string Frag = null;
        [FxField] private string Comp = null;
        private GLShader glvert;
        private GLShader gltess;
        private GLShader gleval;
        private GLShader glgeom;
        private GLShader glfrag;
        private GLShader glcomp;
        private VertShader dbgvert;
        private TessShader dbgtess;
        private EvalShader dbgeval;
        private GeomShader dbggeom;
        private FragShader dbgfrag;
        private CompShader dbgcomp;
        private Vertoutput vertoutput;
        private GLFragoutput fragoutput;
        private List<MultiDrawCall> drawcalls = new List<MultiDrawCall>();
        private List<CompCall> compcalls = new List<CompCall>();
        private List<ResTexImg> texImages = new List<ResTexImg>();
        private List<Res<GLTexture>> textures = new List<Res<GLTexture>>();
        private List<Res<GLSampler>> sampler = new List<Res<GLSampler>>();
        private List<GLMethod> glfunc = new List<GLMethod>();
        private List<GLInstance> csexec = new List<GLInstance>();
        private bool GenDebugInfo;
        public bool TraceDebugInfo { get; set; } = false;

        #endregion

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="genDebugInfo"></param>
        public GLPass(Compiler.Block block, Dict scene, bool genDebugInfo)
            : base(block.Name, block.Anno, 309, genDebugInfo)
        {
            var err = new CompileException($"pass '{name}'");
            GenDebugInfo = genDebugInfo;

            /// PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS

            Cmds2Fields(block, err);

            /// PARSE COMMANDS

            foreach (var cmd in block)
            {
                // ignore class fields
                var field = GetType().GetField(cmd.Name, Instance | Public | NonPublic);
                var attr = field?.GetCustomAttributes(typeof(FxField), false);
                if (attr?.Length > 0)
                    continue;

                using (var e = err | $"command '{cmd.Name}' line {cmd.LineInFile}")
                {
                    switch (cmd.Name)
                    {
                        case "draw": ParseDrawCall(cmd, scene, e); break;
                        case "compute": ParseComputeCall(cmd, scene, e); break;
                        case "tex": ParseTexCmd(cmd, scene, e); break;
                        case "img": ParseImgCmd(cmd, scene, e); break;
                        case "samp": ParseSampCmd(cmd, scene, e); break;
                        case "exec": ParseCsharpExec(cmd, scene, e); break;
                        case "vertout": vertoutput = new Vertoutput(cmd, scene, e);  break;
                        case "fragout": scene.TryGetValue(cmd[0].Text, out fragoutput, cmd, e); break;
                        default: ParseOpenGLCall(cmd, e); break;
                    }
                }
            }

            if (err.HasErrors())
                throw err;

            /// CREATE OPENGL OBJECT

            if (Vert != null || Comp != null)
            {
                GL.CreateProgramPipelines(1, out glname);

                // Attach shader objects.
                // First try attaching a compute shader. If that
                // fails, try attaching the default shader pipeline.
                if ((glcomp = Attach(block, Comp, scene, err)) == null)
                {
                    glvert = Attach(block, Vert, scene, err);
                    gltess = Attach(block, Tess, scene, err);
                    gleval = Attach(block, Eval, scene, err);
                    glgeom = Attach(block, Geom, scene, err);
                    glfrag = Attach(block, Frag, scene, err);
                }

                // get debug shaders
                if (GenDebugInfo)
                {
                    if (glcomp != null)
                    {
                        dbgcomp = (CompShader)glcomp.DebugShader;
                    }
                    else
                    {
                        Shader prev =
                        dbgvert = (VertShader)glvert.DebugShader;
                        dbgtess = (TessShader)gltess?.DebugShader;
                        dbgeval = (EvalShader)gleval?.DebugShader;
                        dbggeom = (GeomShader)glgeom?.DebugShader;
                        dbgfrag = (FragShader)glfrag?.DebugShader;
                        if (dbgtess != null)
                        {
                            dbgtess.Prev = prev;
                            prev = dbgtess;
                        }
                        if (dbgeval != null)
                        {
                            dbgeval.Prev = prev;
                            prev = dbgeval;
                        }
                        if (dbggeom != null)
                        {
                            dbggeom.Prev = prev;
                            prev = dbggeom;
                        }
                    }
                }
            }

            /// CHECK FOR ERRORS

            if (GL.GetError() != ErrorCode.NoError)
                err.Add($"OpenGL error '{GL.GetError()}' occurred " +
                    "during shader program creation.", block);
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
                //GL.DeleteProgram(glname);
                GL.DeleteProgramPipeline(glname);
                glname = 0;
            }
        }

        /// <summary>
        /// Execute pass.
        /// </summary>
        /// <param name="width">Width of the OpenGL control.</param>
        /// <param name="height">Height of the OpenGL control.</param>
        /// <param name="frame">The ID of the current frame.</param>
        public void Exec(int width, int height, int frame)
        {
            // in debug mode check if the
            // OpenGL sate is valid
            ThrowOnGLError($"OpenGL error at the beginning of pass '{name}'.");

            int fbWidth = width, fbHeight = height;

            /// BIND FRAGMENT OUTPUT
            
            // (widthOut and heightOut must be 
            // computed before setting the glViewport)
            if (fragoutput != null)
            {
                fbWidth = fragoutput.Width;
                fbHeight = fragoutput.Height;
                fragoutput.Bind();
            }

            /// SET DEFAULT VIEWPORT
            
            GL.Viewport(0, 0, fbWidth, fbHeight);

            /// CALL USER SPECIFIED OPENGL FUNCTIONS

            foreach (var x in glfunc)
            {
                x.mtype.Invoke(null, x.inval);
                ThrowOnGLError($"OpenGL error in OpenGL function '{x.mtype.Name}' of pass '{name}'.");
            }

            /// BIND PROGRAM

            if (glname > 0)
                GL.BindProgramPipeline(glname);

            /// BIND VERTEX OUTPUT (transform feedback)
            /// must be done after glUseProgram

            vertoutput?.Bind();
            ThrowOnGLError($"OpenGL error vertex output binding of pass '{name}'.");

            /// BIND TEXTURES

            foreach (var x in textures)
            {
                GLTexture.BindTex(x.unit, x.obj);
                ThrowOnGLError($"OpenGL error in texture '{x.obj?.name}' of pass '{name}'.");
            }
            foreach (var x in texImages)
            {
                GLTexture.BindImg(x.unit, x.obj, x.level, x.layer, x.access, x.format);
                ThrowOnGLError($"OpenGL error in image '{x.obj?.name}' of pass '{name}'.");
            }
            foreach (var x in sampler)
            {
                GL.BindSampler(x.unit, x.obj?.glname ?? 0);
                ThrowOnGLError($"OpenGL error in sampler '{x.obj?.name}' of pass '{name}'.");
            }

            /// EXECUTE EXTERNAL CODE

            foreach (var x in csexec)
            {
                x.Update(glname, width, height, fbWidth, fbHeight);
                ThrowOnGLError($"OpenGL error in C# execution '{x.name}' of pass '{name}'.");
            }

            /// BIND DEBUGGER

            if (TraceDebugInfo && GenDebugInfo && drawcalls.Count > 0)
            {
                try
                {
                    Shader.DrawCall = drawcalls.First();
                    if (dbgcomp != null)
                    {
                        dbgcomp.Debug();
                    }
                    else if (dbgvert != null)
                    {
                        dbgvert.Debug();
                        dbgtess?.Debug();
                        dbgeval?.Debug();
                        dbggeom?.Debug();
                        dbgfrag?.Debug();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Debugger crashed with the following message: {e.Message}", e);
                }
                finally
                {
                    TraceDebugInfo = false;
                }
            }

            /// EXECUTE DRAW AND COMPUTE CALLS

            // begin timer query
            MeasureTime();
            StartTimer(frame);

            // execute draw calls
            drawcalls.ForEach(call => call.draw());
            compcalls.ForEach(call => call.compute());

            // end timer query
            EndTimer();

            /// UNBIND OPENGL OBJECTS

            csexec.ForEach(e => e.EndPass(glname));

            /// UNBIND OUTPUT BUFFERS

            fragoutput?.Unbind();
            vertoutput?.Unbind();

            /// UNBIND OPENGL RESOURCES

            GL.BindProgramPipeline(0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.DrawIndirectBuffer, 0);
            GL.BindBuffer(BufferTarget.DispatchIndirectBuffer, 0);
            GL.BindVertexArray(0);
            
            // in debug mode check if the pass
            // left a valid OpenGL sate
            ThrowOnGLError($"OpenGL error at the end of pass '{name}'.");
        }
        
        #region PARSE COMMANDS

        private void ParseDrawCall(Compiler.Command cmd, Dict classes, CompileException err)
        {
            List<int> args = new List<int>();
            GLVertinput vertexin = null;
            GLVertoutput vertout = null;
            GLBuffer indexbuf = null;
            GLBuffer indirect = null;
            bool modeIsSet = false;
            bool typeIsSet = false;
            PrimType primitive = 0;
            ElementType indextype = 0;

            // parse draw call arguments
            foreach (var arg in cmd)
            {
                if (classes.TryGetValue(arg.Text, ref vertexin))
                    continue;
                if (classes.TryGetValue(arg.Text, ref vertout))
                    continue;
                if (classes.TryGetValue(arg.Text, ref indexbuf))
                    continue;
                if (classes.TryGetValue(arg.Text, ref indirect))
                    continue;
                if (int.TryParse(arg.Text, out int val))
                    args.Add(val);
                else if (typeIsSet == false && Enum.TryParse(arg.Text, true, out indextype))
                    typeIsSet = true;
                else if (modeIsSet == false && Enum.TryParse(arg.Text, true, out primitive))
                    modeIsSet = true;
                else
                    err.Add($"Unable to process argument '{arg.Text}'.", cmd);
            }

            if (err.HasErrors())
                return;
            
            // a draw call must specify a primitive type
            if (modeIsSet == false)
            {
                err.Add("Draw call must specify a primitive type (e.g. triangles, "
                    + "trianglefan, lines, points, ...).", cmd);
                return;
            }

            // determine the right draw call function
            int bits = (vertout != null ? 1 : 0)
                | (indexbuf != null ? 2 : 0)
                | (indirect != null ? 4 : 0)
                | (typeIsSet ? 8 : 0);

            if (!Enum.IsDefined(typeof(DrawFunc), bits))
            {
                err.Add("Draw call function not recognized or ambiguous.", cmd);
                return;
            }

            DrawFunc drawfunc = (DrawFunc)bits;

            // get index buffer object (if present) and find existing MultiDraw class
            var multidrawcall = drawcalls.Find(
                x => x.vertexin == (vertexin != null ? vertexin.glname : 0)
                  && x.indexbuf == (indexbuf != null ? indexbuf.glname : 0)
                  && x.vertout == (vertout != null ? vertout.glname : 0)
                  && x.indirect == (indirect != null ? indirect.glname : 0))
                ?? new MultiDrawCall(drawfunc, vertexin, vertout, indexbuf, indirect);

            // add new draw command to the MultiDraw class
            multidrawcall.cmd.Add(new DrawCall(drawfunc, primitive, indextype, args));

            drawcalls.Add(multidrawcall);
        }
        
        private void ParseComputeCall(Compiler.Command cmd, Dict classes, CompileException err)
        {
            // check for errors
            if (cmd.ArgCount < 2 || cmd.ArgCount > 3)
            {
                err.Add("Compute command does not provide enough arguments "
                    + "(e.g., 'compute num_groups_X num_groups_y num_groups_z' or "
                    + "'compute buffer_name indirect_pointer').", cmd);
                return;
            }

            try
            {
                CompCall call = new CompCall();

                // this is an indirect compute call
                if (cmd.ArgCount == 2)
                {
                    // indirect compute call buffer
                    call.numGroupsX = (uint)classes.GetValue<GLBuffer>(cmd[0].Text,
                        "First argument of compute command must be a buffer name").glname;
                    // indirect compute call buffer pointer
                    call.numGroupsY = cmd[1].Text.To<uint>("Argument must be an unsigned integer, "
                        + "specifying a pointer into the indirect compute call buffer.");
                }
                // this is a normal compute call
                else
                {
                    // number of compute groups
                    call.numGroupsX = cmd[0].Text.To<uint>("Argument must be an unsigned integer, "
                        + "specifying the number of compute groups in X.");
                    call.numGroupsY = cmd[1].Text.To<uint>("Argument must be an unsigned integer, "
                        + "specifying the number of compute groups in Y.");
                    call.numGroupsZ = cmd[2].Text.To<uint>("Argument must be an unsigned integer, "
                        + "specifying the number of compute groups in Z.");
                }

                compcalls.Add(call);
            }
            catch (CompileException ex)
            {
                err.Add(ex.Message, cmd);
            }
        }
        
        private void ParseTexCmd(Compiler.Command cmd, Dict classes, CompileException err)
        {
            if (cmd.ArgCount != 1 && cmd.ArgCount != 2)
            {
                err.Add("Arguments of the 'tex' command are invalid.", cmd);
                return;
            }
            // specify argument types
            var types = new[] { typeof(GLTexture), typeof(int), typeof(string) };
            // specify mandatory arguments
            var mandatory = new[] { new[] { true, true, false }, new[] { false, true, false } };
            // parse command arguments
            (var values, var unused) = cmd.Parse(types, mandatory, classes, err);
            // if there are no errors, add the object to the pass
            if (!err.HasErrors())
                textures.Add(new Res<GLTexture>(values));
        }

        private void ParseImgCmd(Compiler.Command cmd, Dict classes, CompileException err)
        {
            if (cmd.ArgCount != 1 && cmd.ArgCount != 6)
            {
                err.Add("Arguments of the 'img' command are invalid.", cmd);
                return;
            }
            // specify argument types
            var types = new[] {
                typeof(GLTexture),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(TextureAccess),
                typeof(GpuFormat),
                typeof(string)
            };
            // specify mandatory arguments
            var mandatory = new[] {
                new[] { true, true, true, true, true, true, false },
                new[] { false, true, false, false, false, false, false },
            };
            // parse command arguments
            (var values, var unused) = cmd.Parse(types, mandatory, classes, err);
            // if there are no errors, add the object to the pass
            if (!err.HasErrors())
                texImages.Add(new ResTexImg(values));
        }

        private void ParseSampCmd(Compiler.Command cmd, Dict classes, CompileException err)
        {
            if (cmd.ArgCount != 1 && cmd.ArgCount != 2)
            {
                err.Add("Arguments of the 'samp' command are invalid.", cmd);
                return;
            }
            // specify argument types
            var types = new[] { typeof(GLSampler), typeof(int), typeof(string) };
            // specify mandatory arguments
            var mandatory = new[] { new[] { true, true, false }, new[] { false, true, false } };
            // parse command arguments
            (var values, var unused) = cmd.Parse(types, mandatory, classes, err);
            // if there are no errors, add the object to the pass
            if (!err.HasErrors())
                sampler.Add(new Res<GLSampler>(values));
        }
        
        private void ParseOpenGLCall(Compiler.Command cmd, CompileException err)
        {
            // find OpenGL method
            var mname = cmd.Name.StartsWith("gl") ? cmd.Name.Substring(2) : cmd.Name;
            var mtype = FindMethod(mname, cmd.ArgCount);
            if (mtype == null)
            {
                if (GetFxField(mname) == null)
                    err.Add("Unknown command '" + cmd.Text + "'", cmd);
                return;
            }

            // get method parameter types
            var param = mtype.GetParameters();
            object[] inval = new object[param.Length];
            // convert strings to parameter types
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i].ParameterType.IsEnum)
                    inval[i] = Convert.ChangeType(
                        Enum.Parse(param[i].ParameterType, cmd[i].Text, true),
                        param[i].ParameterType);
                else
                    inval[i] = Convert.ChangeType(cmd[i].Text, param[i].ParameterType, CultureInfo.CurrentCulture);
            }

            glfunc.Add(new GLMethod(mtype, inval));
        }
        
        private void ParseCsharpExec(Compiler.Command cmd, Dict classes, CompileException err)
        {
            // check if command provides the correct amount of parameters
            if (cmd.ArgCount == 0)
            {
                err.Add("Not enough arguments for exec command.", cmd);
                return;
            }

            // get instance
            if (!classes.TryGetValue(cmd[0].Text, out GLInstance instance, cmd, err))
                return;

            csexec.Add(instance);
        }

        #endregion

        #region UTIL METHODS

        private MethodInfo FindMethod(string name, int nparam)
            => (from method in typeof(GL).GetMethods()
                where method.Name == name && method.GetParameters().Length == nparam
                select method).FirstOrDefault();

        private GLShader Attach(Compiler.Block block, string shadername, Dict classes,
            CompileException err)
        {
            GLShader obj = null;

            // get shader from class list
            if (shadername != null && classes.TryGetValue(shadername, out obj, block, err))
                GL.UseProgramStages(glname, ShaderType2ShaderBit(obj.ShaderType), obj.glname);

            return obj;
        }

        private static ProgramStageMask ShaderType2ShaderBit(ShaderType type)
        {
            switch (type)
            {
                case ShaderType.VertexShader: return VertexShaderBit;
                case ShaderType.TessControlShader: return TessControlShaderBit;
                case ShaderType.TessEvaluationShader: return TessEvaluationShaderBit;
                case ShaderType.GeometryShader: return GeometryShaderBit;
                case ShaderType.FragmentShader: return FragmentShaderBit;
                case ShaderType.ComputeShader: return ComputeShaderBit;
            }
            return 0;
        }

        private static void ThrowOnGLError(string message)
        {
#if DEBUG
            ErrorCode errcode = GL.GetError();
            if (errcode != ErrorCode.NoError)
                throw new Exception($"{errcode}: {message}");
#endif
        }

        #endregion

        #region HELP STRUCT

        internal enum DrawFunc
        {
            ArraysIndirect = 0 | 2 | 0 | 0,
            ArraysInstanced = 0 | 0 | 0 | 0,
            ElementsIndirect = 0 | 2 | 4 | 8,
            ElementsInstanced = 0 | 2 | 0 | 8,
            TransformFeedback = 1 | 2 | 0 | 0,
        }

        internal class MultiDrawCall
        {
            public GLVertinput vertin;
            public GLBuffer indbuf;
            public DrawFunc drawfunc;
            public int vertexin;
            public int indexbuf;
            public int vertout;
            public int indirect;
            public List<DrawCall> cmd;

            public MultiDrawCall(
                DrawFunc drawfunc,
                GLVertinput vertexin,
                GLVertoutput vertout,
                GLBuffer indexbuf,
                GLBuffer indirect)
            {
                vertin = vertexin;
                indbuf = indexbuf;
                this.cmd = new List<DrawCall>();
                this.drawfunc = drawfunc;
                this.vertexin = vertexin != null ? vertexin.glname : 0;
                this.indexbuf = indexbuf != null ? indexbuf.glname : 0;
                this.vertout = vertout != null ? vertout.glname : 0;
                this.indirect = indirect != null ? indirect.glname : 0;
                if (drawfunc == DrawFunc.ArraysIndirect)
                {
                    this.indirect = this.indexbuf;
                    this.indexbuf = 0;
                }
            }

            public Array GetPatch(int primitiveID)
            {
                // get patch size
                var inum = GL.GetInteger(GetPName.PatchVertices);

                // no vertex element array bound
                // return the respective primitive
                if (indbuf == null)
                    return Enumerable.Range(inum * primitiveID, inum).ToArray();

                // get index type size
                var isize = 4;
                switch (cmd[0].indextype)
                {
                    case ElementType.UByte: isize = 1; break;
                    case ElementType.UShort: isize = 2; break;
                }

                // get data from the index buffer
                var data = new byte[isize * inum];
                indbuf.Read(ref data, data.Length * primitiveID);

                // convert data to index array
                switch (cmd[0].indextype)
                {
                    case ElementType.UByte: return data;
                    case ElementType.UShort: return data.To(typeof(ushort));
                    default: return data.To(typeof(uint));
                }
            }

            public void draw()
            {
                // bind vertex buffer to input stream
                // (needs to be done before binding an ElementArrayBuffer)
                GL.BindVertexArray(vertexin);

                switch (drawfunc)
                {
                    case DrawFunc.ArraysIndirect:
                        GL.BindBuffer(BufferTarget.DrawIndirectBuffer, indirect);
                        foreach (var draw in cmd)
                            GL.DrawArraysIndirect(draw.mode, draw.indirectPtr);
                        break;

                    case DrawFunc.ArraysInstanced:
                        foreach (var draw in cmd)
                            GL.DrawArraysInstancedBaseInstance(
                                draw.mode, draw.vBaseVertex, draw.vVertexCount,
                                draw.vInstanceCount, draw.vBaseInstance);
                        break;

                    case DrawFunc.ElementsIndirect:
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexbuf);
                        GL.BindBuffer(BufferTarget.DrawIndirectBuffer, indirect);
                        foreach (var draw in cmd)
                            GL.MultiDrawElementsIndirect((All)draw.mode, (All)draw.indextype,
                                draw.indirectPtr, draw.indirectCount, draw.indirectStride);
                        break;

                    case DrawFunc.ElementsInstanced:
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexbuf);
                        foreach (var draw in cmd)
                            GL.DrawElementsInstancedBaseVertexBaseInstance(
                                draw.mode, draw.iIndexCount, draw.indextype,
                                draw.iBaseIndex, draw.iInstanceCount,
                                draw.iBaseVertex, draw.iBaseInstance);
                        break;

                    case DrawFunc.TransformFeedback:
                        foreach (var draw in cmd)
                            GL.DrawTransformFeedbackStreamInstanced(draw.mode,
                                vertout, draw.voStream, draw.voInstanceCount);
                        break;
                }
            }
        }

        internal struct DrawCall
        {
            public PrimType mode;
            public ElementType indextype;
            private int arg0;
            private int arg1;
            private int arg2;
            private int arg3;
            private int arg4;

            public DrawCall(DrawFunc drawfunc, PrimType mode, ElementType indextype, List<int> arg)
            {
                this.mode = mode;
                this.indextype = indextype;
                // iBaseVertex, vBaseVertex, voStream, indirectPtr
                arg0 = arg.Count > 0 ? arg[0] : 0;
                // iBaseIndex, vVertexCount, indirectCount
                arg1 = arg.Count > 1 ? arg[1] : 
                    drawfunc == DrawFunc.TransformFeedback
                    || drawfunc == DrawFunc.ArraysIndirect
                    || drawfunc == DrawFunc.ElementsIndirect
                    ? 1
                    : 0;
                // iIndexCount, vBaseInstance, indirectStride
                arg2 = arg.Count > 2 ? arg[2] :
                    drawfunc == DrawFunc.TransformFeedback
                    ? 1
                    : drawfunc == DrawFunc.ArraysIndirect
                        ? 16
                        : drawfunc == DrawFunc.ElementsIndirect
                            ? 32
                            : 0;
                // iBaseInstance, vInstanceCount
                arg3 = arg.Count > 3 ? arg[3] :
                    drawfunc == DrawFunc.ArraysInstanced ? 1 : 0;
                // iInstanceCount
                arg4 = arg.Count > 4 ? arg[4] : 
                    drawfunc == DrawFunc.ElementsInstanced ? 1 : 0;
            }

            // arguments for indexed buffer drawing
            public int iBaseVertex { get { return arg0; } set { arg0 = value; } }
            public IntPtr iBaseIndex
            {
                get { return (IntPtr)(arg1 * Math.Max(1, (int)indextype - (int)ElementType.UByte)); }
                set { arg1 = (int)value; }
            }
            public int iIndexCount { get { return arg2; } set { arg2 = value; } }
            public int iBaseInstance { get { return arg3; } set { arg3 = value; } }
            public int iInstanceCount { get { return arg4; } set { arg4 = value; } }
            // get arguments for vertex buffer drawing
            public int vBaseVertex { get { return arg0; } set { arg0 = value; } }
            public int vVertexCount { get { return arg1; } set { arg1 = value; } }
            public int vBaseInstance { get { return arg2; } set { arg2 = value; } }
            public int vInstanceCount { get { return arg3; } set { arg3 = value; } }
            // get arguments for vertex output drawing
            public int voStream { get { return arg0; } set { arg0 = value; } }
            public int voInstanceCount { get { return arg1; } set { arg1 = value; } }
            // get arguments for indirect drawing
            public IntPtr indirectPtr { get { return (IntPtr)arg0; } set { arg0 = (int)value; } }
            public int indirectCount { get { return arg1; } set { arg1 = value; } }
            public int indirectStride { get { return arg2; } set { arg2 = value; } }
        }

        private struct CompCall
        {
            public uint numGroupsX;
            public uint numGroupsY;
            public uint numGroupsZ;
            public int indirect { get { return (int)numGroupsX; } }
            public IntPtr indirectPtr { get { return (IntPtr)numGroupsY; } }

            public void compute()
            {
                if (numGroupsZ > 0)
                {
                    // execute compute shader
                    GL.DispatchCompute(numGroupsX, numGroupsY, numGroupsZ);
                }
                else
                {
                    // bind indirect compute call buffer
                    GL.BindBuffer(BufferTarget.DispatchIndirectBuffer, indirect);
                    // execute compute shader
                    GL.DispatchComputeIndirect(indirectPtr);
                }
            }
        }

        private class Res<T>
        {
            public T obj;
            public int unit;

            public Res(object[] values)
            {
                if (values[0] != null)
                    obj = (T)values[0];
                if (values[1] != null)
                    unit = (int)values[1];
            }
        }

        private class ResTexImg : Res<GLTexture>
        {
            public int level;
            public int layer;
            public TextureAccess access;
            public GpuFormat format;

            public ResTexImg(object[] values) : base(values)
            {
                if (values[2] != null)
                    level = (int)values[2];
                if (values[3] != null)
                    layer = (int)values[3];
                if (values[4] != null)
                    access = (TextureAccess)values[4];
                if (values[5] != null)
                    format = (GpuFormat)values[5];
            }
        }

        private struct GLMethod
        {
            public MethodInfo mtype;
            public object[] inval;

            public GLMethod(MethodInfo mtype, object[] inval)
            {
                this.mtype = mtype;
                this.inval = inval;
            }
        }

        private class Vertoutput
        {
            enum ResumePause { None, Pause, Resume, }
            public GLVertoutput glvertout;
            public VertoutPrimType vertoutPrim;
            public TransformFeedbackMode vertoutMode;
            public bool pause;
            public bool resume;
            //public string[] outputVaryings;

            public Vertoutput(Compiler.Command cmd, Dict scene, CompileException err)
            {
                // specify argument types
                var types = new[] {
                    typeof(GLVertoutput),
                    typeof(VertoutPrimType),
                    typeof(ResumePause),
                    typeof(TransformFeedbackMode),
                };
                // specify mandatory arguments
                var mandatory = new[] {
                    new[] { false, false, false, false },
                };
                // parse command arguments
                (var values, var outputVaryings) = cmd.Parse(types, mandatory, scene, err);
                if (err.HasErrors())
                    return;

                // set transform feedback varyings in the shader program
                if (outputVaryings.Length == 0)
                    err.Add("vertout command does not specify shader output varying names "
                        + "(e.g. vertout vertout_name points [pause resume] varying_name).", cmd);
                if (err.HasErrors())
                    return;

                // set fields
                glvertout = (GLVertoutput)values[0];
                vertoutPrim = (VertoutPrimType)values[1];
                switch ((ResumePause)(values[2] ?? ResumePause.None))
                {
                    case ResumePause.Pause: pause = true; break;
                    case ResumePause.Resume: resume = true; break;
                }
                vertoutMode = (TransformFeedbackMode)(values[3] ?? InterleavedAttribs);
            }

            //public void SetProgramVaryings(int glname)
            //    => GL.TransformFeedbackVaryings(glname, outputVaryings.Length, outputVaryings, vertoutMode);

            public void Bind() => glvertout.Bind(vertoutPrim, resume);

            public void Unbind() => GLVertoutput.Unbind(pause);
        }

        #endregion
    }
}
