using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using PrimType = OpenTK.Graphics.OpenGL4.PrimitiveType;
using VertoutPrimType = OpenTK.Graphics.OpenGL4.TransformFeedbackPrimitiveType;
using ElementType = OpenTK.Graphics.OpenGL4.DrawElementsType;

namespace App
{
    class GLPass : GLObject
    {
        #region FIELDS
        private static CultureInfo culture = new CultureInfo("en");
        [Field] private string vert = null;
        [Field] private string tess = null;
        [Field] private string eval = null;
        [Field] private string geom = null;
        [Field] private string frag = null;
        [Field] private string comp = null;
        [Field] private string[] vertout = null;
        [Field] private string fragout = null;
        private GLObject glvert = null;
        private GLObject gltess = null;
        private GLObject gleval = null;
        private GLObject glgeom = null;
        private GLObject glfrag = null;
        private GLObject glcomp = null;
        private GLVertoutput glvertout = null;
        private VertoutPrimType vertoutPrim = VertoutPrimType.Points;
        private GLFragoutput glfragout = null;
        private List<MultiDrawCall> drawcalls = new List<MultiDrawCall>();
        private List<CompCall> compcalls = new List<CompCall>();
        private List<ResTexImg> texImages = new List<ResTexImg>();
        private List<Res<GLTexture>> textures = new List<Res<GLTexture>>();
        private List<Res<GLSampler>> sampler = new List<Res<GLSampler>>();
        private List<GLMethod> glfunc = new List<GLMethod>();
        private List<GLInstance> csexec = new List<GLInstance>();
        #endregion

        /// <summary>
        /// Create pass object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLPass(GLParams @params) : base(@params)
        {
            var err = new CompileException($"pass '{name}'");

            // PARSE TEXT
            var body = new Commands(@params.cmdText, @params.cmdPos, err);

            // PARSE ARGUMENTS
            body.Cmds2Fields(this, err);

            // PARSE COMMANDS
            foreach (var cmd in body)
            {
                err.PushCall($"command {cmd.idx} '{cmd.cmd}'");
                switch (cmd.cmd)
                {
                    case "draw": ParseDrawCall(err, cmd.args, @params.scene); break;
                    case "compute": ParseComputeCall(err, cmd.args, @params.scene); break;
                    case "tex": ParseTexCmd(err, cmd.args, @params.scene); break;
                    case "samp": ParseSampCmd(err, cmd.args, @params.scene); break;
                    case "exec": ParseCsharpExec(err, cmd.cmd, cmd.args, @params.scene); break;
                    default: ParseOpenGLCall(err, cmd.cmd, cmd.args); break;
                }
                err.PopCall();
            }

            // GET VERTEX AND FRAGMENT OUTPUT BINDINGS
            
            if (fragout != null && !@params.scene.TryGetValue(fragout, out glfragout))
                err.Add($"The name '{fragout}' does not reference an object of type 'fragout'.");
            if (vertout != null && vertout.Length > 0 && !@params.scene.TryGetValue(vertout[0], out glvertout))
                err.Add($"The name '{vertout[0]}' does not reference an object of type 'vertout'.");
            if (err.HasErrors())
                throw err;

            // CREATE OPENGL OBJECT
            if (vert != null || comp != null)
            {
                glname = GL.CreateProgram();

                // Attach shader objects.
                // First try attaching a compute shader. If that
                // fails, try attaching the default shader pipeline.
                if ((glcomp = attach(err, comp, @params.scene)) == null)
                {
                    glvert = attach(err, vert, @params.scene);
                    gltess = attach(err, tess, @params.scene);
                    gleval = attach(err, eval, @params.scene);
                    glgeom = attach(err, geom, @params.scene);
                    glfrag = attach(err, frag, @params.scene);
                }

                // specify vertex output varyings of the shader program
                if (glvertout != null)
                    setVertexOutputVaryings(err, vertout);

                // link program
                GL.LinkProgram(glname);

                // detach shader objects
                if (glcomp != null)
                    GL.DetachShader(glname, glcomp.glname);
                else
                {
                    if (glvert != null)
                        GL.DetachShader(glname, glvert.glname);
                    if (gltess != null)
                        GL.DetachShader(glname, gltess.glname);
                    if (gleval != null)
                        GL.DetachShader(glname, gleval.glname);
                    if (glgeom != null)
                        GL.DetachShader(glname, glgeom.glname);
                    if (glfrag != null)
                        GL.DetachShader(glname, glfrag.glname);
                }

                // check for link errors
                int status;
                GL.GetProgram(glname, GetProgramParameterName.LinkStatus, out status);
                if (status != 1)
                {
                    var msg = GL.GetProgramInfoLog(glname);
                    if (msg != null && msg.Length > 0)
                        err.Add("\n" + msg);
                }
            }

            if (GL.GetError() != ErrorCode.NoError)
                err.Add($"OpenGL error '{GL.GetError()}' occurred during shader program creation.");
            if (err.HasErrors())
                throw err;
        }

        /// <summary>
        /// Execute pass.
        /// </summary>
        /// <param name="width">Width of the OpenGL control.</param>
        /// <param name="height">Height of the OpenGL control.</param>
        public void Exec(int width, int height, int frame)
        {
#if DEBUG
            // in debug mode check if the
            // OpenGL sate is valid
            var errcode = GL.GetError();
            if (errcode != ErrorCode.NoError)
                throw new Exception("OpenGL error.");
#endif

            int fbWidth = width;
            int fbHeight = height;

            // BIND FRAGMENT OUTPUT
            // (widthOut and heightOut must be 
            // computed before setting the glViewport)
            if (glfragout != null)
            {
                fbWidth = glfragout.width;
                fbHeight = glfragout.height;
                glfragout.Bind();
            }

            // SET DEFAULT VIEWPORT
            GL.Viewport(0, 0, fbWidth, fbHeight);

            // CALL USER SPECIFIED OPENGL FUNCTIONS
            foreach (var glcall in glfunc)
                glcall.mtype.Invoke(null, glcall.inval);

            // BIND PROGRAM
            if (glname > 0)
                GL.UseProgram(glname);

            // BIND VERTEX OUTPUT (transform feedback)
            // (must be done after glUseProgram)
            if (glvertout != null)
                glvertout.Bind(vertoutPrim);

            // BIND TEXTURES
            foreach (var t in textures)
                t.obj.BindTex(t.unit);
            foreach (var t in texImages)
                t.obj.BindImg(t.unit, t.level, t.layer, t.access, t.format);
            foreach (var s in sampler)
                GL.BindSampler(s.unit, s.obj.glname);

            // EXECUTE EXTERNAL CODE
            foreach (var e in csexec)
                e.Update(glname, width, height, fbWidth, fbHeight);
            
            // BIND DEBUGGER
            if (glname > 0)
                GLDebugger.Bind(this, frame);
            
            // EXECUTE DRAW CALLS
            foreach (var call in drawcalls)
                call.draw();

            // EXECUTE COMPUTE CALLS
            foreach (var call in compcalls)
                call.compute();

            // UNBIND DEBUGGER
            if (glname > 0)
                GLDebugger.Unbind(this);

            // UNBIND OUTPUT BUFFERS
            if (glfragout != null)
                glfragout.Unbind();
            if (glvertout != null)
                glvertout.Unbind();

            // UNBIND OPENGL RESOURCES
            GL.UseProgram(0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.DrawIndirectBuffer, 0);
            GL.BindBuffer(BufferTarget.DispatchIndirectBuffer, 0);
            GL.BindVertexArray(0);

            // UNBIND OPENGL OBJECTS
            foreach (var t in textures)
                t.obj.UnbindTex(t.unit);
            foreach (var t in texImages)
                t.obj.UnbindImg(t.unit);
            foreach (var s in sampler)
                GL.BindSampler(s.unit, 0);
            foreach (var e in csexec)
                e.EndPass(glname);
#if DEBUG
            // in debug mode check if the pass
            // left a valid OpenGL sate
            if ((errcode = GL.GetError()) != ErrorCode.NoError)
                throw new Exception("OpenGL error.");
#endif
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteProgram(glname);
                glname = 0;
            }
        }

        #region PARSE COMMANDS
        private void ParseDrawCall(CompileException err, string[] cmd, Dict<GLObject> classes)
        {
            List<int> arg = new List<int>();
            GLVertinput vertexin = null;
            GLVertoutput vertout = null;
            GLBuffer indexbuf = null;
            GLBuffer indirect = null;
            bool modeIsSet = false;
            bool typeIsSet = false;
            PrimType primitive = 0;
            ElementType indextype = 0;
            int val;

            // parse draw call arguments
            for (var i = 0; i < cmd.Length; i++)
            {
                if (classes.TryGetValue(cmd[i], ref vertexin)) continue;
                if (classes.TryGetValue(cmd[i], ref vertout)) continue;
                if (classes.TryGetValue(cmd[i], ref indexbuf)) continue;
                if (classes.TryGetValue(cmd[i], ref indirect)) continue;
                if (int.TryParse(cmd[i], out val))
                    arg.Add(val);
                else if (typeIsSet == false && Enum.TryParse(cmd[i], true, out indextype))
                    typeIsSet = true;
                else if (modeIsSet == false && Enum.TryParse(cmd[i], true, out primitive))
                    modeIsSet = true;
            }

            // a draw call must specify a primitive type
            if (modeIsSet == false)
            {
                err.Add("Draw call must specify a primitive type "
                    + "(e.g. triangles, trianglefan, lines, points, ...).");
                return;
            }

            // determine the right draw call function
            int bits = (vertout != null ? 1 : 0) 
                | (indexbuf != null ? 2 : 0)
                | (indirect != null ? 4 : 0)
                | (typeIsSet ? 8 : 0);

            if (!Enum.IsDefined(typeof(DrawFunc), bits))
            {
                err.Add("Draw call function not recognized or ambiguous.");
                return;
            }

            DrawFunc drawfunc = (DrawFunc)bits;
                 
            // get index buffer object (if present) and find existing MultiDraw class
            MultiDrawCall multidrawcall = drawcalls.Find(
                x => x.vertexin == (vertexin != null ? vertexin.glname : 0)
                  && x.indexbuf == (indexbuf != null ? indexbuf.glname : 0)
                  && x.vertout  == (vertout  != null ?  vertout.glname : 0)
                  && x.indirect == (indirect != null ? indirect.glname : 0))
                ?? new MultiDrawCall(drawfunc, vertexin, vertout, indexbuf, indirect);

            // add new draw command to the MultiDraw class
            multidrawcall.cmd.Add(new DrawCall(drawfunc, primitive, indextype, arg));
            
            drawcalls.Add(multidrawcall);
        }

        private void ParseComputeCall(CompileException err, string[] cmd, Dict<GLObject> classes)
        {
            // check for errors
            if (cmd.Length < 2 || cmd.Length > 3)
            {
                err.Add("Compute command does not provide enough arguments "
                    + "(e.g., 'compute num_groups_X num_groups_y num_groups_z' or "
                    + "'compute buffer_name indirect_pointer').");
                return;
            }

            try
            {
                CompCall call = new CompCall();

                // this is an indirect compute call
                if (cmd.Length == 2)
                {
                    // indirect compute call buffer
                    call.numGroupsX = (uint)classes.GetValue<GLBuffer>(cmd[0],
                        "First argument of compute command must be a buffer name").glname;
                    // indirect compute call buffer pointer
                    call.numGroupsY = cmd[1].To<uint>("Argument must be an unsigned integer, "
                        + "specifying a pointer into the indirect compute call buffer.");
                }
                // this is a normal compute call
                else
                {
                    // number of compute groups
                    call.numGroupsX = cmd[0].To<uint>("Argument must be an unsigned integer, "
                        + "specifying the number of compute groups in X.");
                    call.numGroupsY = cmd[1].To<uint>("Argument must be an unsigned integer, "
                        + "specifying the number of compute groups in Y.");
                    call.numGroupsZ = cmd[2].To<uint>("Argument must be an unsigned integer, "
                        + "specifying the number of compute groups in Z.");
                }

                compcalls.Add(call);
            }
            catch (CompileException ex)
            {
                err.Add(ex.Message);
            }
        }

        private void ParseTexCmd(CompileException err, string[] args, Dict<GLObject> classes)
        {
            var types = new[] {
                typeof(GLTexture),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(TextureAccess),
                typeof(GpuFormat)
            };
            var values = ParseCmd(args, types, args.Length == 6 ? 6 : 2, classes, err);
            if (!err.HasErrors())
            {
                if (args.Length == 6)
                    texImages.Add(new ResTexImg(values));
                else
                    textures.Add(new Res<GLTexture>(values));
            }
        }

        private void ParseSampCmd(CompileException err, string[] args, Dict<GLObject> classes)
        {
            var types = new[] {
                typeof(GLSampler),
                typeof(int),
            };
            var values = ParseCmd(args, types, 2, classes, err);
            if (!err.HasErrors())
                sampler.Add(new Res<GLSampler>(values));
        }

        private object[] ParseCmd(string[] args, Type[] types, int numMandatory,
            Dict<GLObject> classes, CompileException err)
        {
            object[] values = new object[types.Length];

            // parse command arguments
            foreach (string arg in args)
            {
                for (int i = values.IndexOf(x => x == null); i < 0 || i < types.Length; i++)
                {
                    try
                    {
                        values[i] = types[i].IsSubclassOf(typeof(GLObject))
                            ? classes.GetValue<GLObject>(arg)
                            : types[i].IsEnum
                                ? Enum.Parse(types[i], arg, true)
                                : Convert.ChangeType(arg, types[i], App.culture);
                        if (values[i] != null)
                            break;
                    }
                    catch { }
                }
            }
            
            // check for errors
            for (int i = 0; i < Math.Min(numMandatory, values.Length); i++)
                if (values[i] == null)
                    err?.Add($"Error parsing argument {i}.");

            return values;
        }

        private void ParseOpenGLCall(CompileException err, string cmd, string[] args)
        {
            // find OpenGL method
            var mtype = FindMethod(cmd, args.Length);
            if (mtype == null)
            {
                err.Add("Unknown command " + string.Join(" ", args) + ".");
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
                        Enum.Parse(param[i].ParameterType, args[i], true),
                        param[i].ParameterType);
                else
                    inval[i] = Convert.ChangeType(args[i], param[i].ParameterType, App.culture);
            }
            
            glfunc.Add(new GLMethod(mtype, inval));
        }

        private void ParseCsharpExec(CompileException err, string cmd, string[] args, Dict<GLObject> classes)
        {
            // check if command provides the correct amount of parameters
            if (args.Length == 0)
            {
                err.Add("Not enough arguments for exec command.");
                return;
            }

            // get instance
            GLInstance instance;
            if (classes.TryGetValue(args[0], out instance, err) == false)
                return;

            csexec.Add(instance);
        }
        #endregion

        #region UTIL METHODS
        private MethodInfo FindMethod(string name, int nparam)
        {
            var methods = from method in typeof(GL).GetMethods()
                          where method.Name == name && method.GetParameters().Length == nparam
                          select method;
            return methods.Count() > 0 ? methods.First() : null;
        }

        private GLShader attach(CompileException err, string sh, Dict<GLObject> classes)
        {
            GLShader glsh = null;

            // get shader from class list
            if (sh != null && classes.TryGetValue(sh, out glsh, err))
                GL.AttachShader(glname, glsh.glname);

            return glsh;
        }

        private void setVertexOutputVaryings(CompileException err, string[] varyings)
        {
            // the vertout command needs at least 3 arguments
            if (varyings.Length < 3)
                throw err.Add("vertout command does not have "
                    + "enough arguments (e.g. vertout vertout_name points varying_name).");

            // parse vertex output primitive type
            if (!Enum.TryParse(varyings[1], true, out vertoutPrim))
                throw err.Add("vertout command does not support "
                    + $"the specified primitive type '{varyings[1]}' "
                    + "(must be 'points', 'lines' or 'triangles').");

            // get vertex output varying specification
            int skip = 2;
            TransformFeedbackMode vertoutMode = TransformFeedbackMode.InterleavedAttribs;

            // write output varyings into separate buffers
            if (varyings[2] == "gl_SeparateAttribs")
            {
                vertoutMode = TransformFeedbackMode.SeparateAttribs;
                skip = 3;
            }
            // write output varyings into the same buffer except
            // if 'gl_NextBuffer' is specified in the varyings list
            else if (varyings[2] == "gl_InterleavedAttribs")
                skip = 3;

            // set transform feedback varyings in the shader program
            if (varyings.Length - skip > 0)
            {
                string[] outputVaryings = new string[varyings.Length - skip];
                Array.Copy(varyings, skip, outputVaryings, 0, varyings.Length - skip);
                GL.TransformFeedbackVaryings(glname, outputVaryings.Length, outputVaryings, vertoutMode);
            }
            else
                throw err.Add("vertout command does not specify shader output varying names "
                    + "(e.g. vertout vertout_name points varying_name).");
        }
        #endregion

        #region HELP STRUCT
        public enum DrawFunc
        {
            ArraysIndirect = 0 | 2 | 0 | 0,
            ArraysInstanced = 0 | 0 | 0 | 0,
            ElementsIndirect = 0 | 2 | 4 | 8,
            ElementsInstanced = 0 | 2 | 0 | 8,
            TransformFeedback = 1 | 2 | 0 | 0,
        }

        private class MultiDrawCall
        {
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
                            GL.DrawElementsIndirect(draw.mode, (All)draw.indextype,
                                draw.indirectPtr);
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

        public struct DrawCall
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
                arg0 = arg.Count > 0 ? arg[0] : 0;
                arg1 = arg.Count > 1 ? arg[1] : (drawfunc == DrawFunc.TransformFeedback ? 1 : 0);
                arg2 = arg.Count > 2 ? arg[2] : 0;
                arg3 = arg.Count > 3 ? arg[3] : (drawfunc == DrawFunc.ArraysInstanced ? 1 : 0);
                arg4 = arg.Count > 4 ? arg[4] : (drawfunc == DrawFunc.ElementsInstanced ? 1 : 0);
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
        }

        private struct CompCall
        {
            public uint numGroupsX;
            public uint numGroupsY;
            public uint numGroupsZ;
            public int indirect
            { get { return (int)numGroupsX; } }
            public IntPtr indirectPtr
            { get { return (IntPtr)numGroupsY; } }

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
        #endregion
    }
}
