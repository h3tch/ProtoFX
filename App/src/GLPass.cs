using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace App
{
    class GLPass : GLObject
    {
        #region FIELDS
        private static CultureInfo culture = new CultureInfo("en");
        public string vert = null;
        public string tess = null;
        public string eval = null;
        public string geom = null;
        public string frag = null;
        public string comp = null;
        public string[] vertout = null;
        public string fragout = null;
        public GLObject glvert = null;
        public GLObject gltess = null;
        public GLObject gleval = null;
        public GLObject glgeom = null;
        public GLObject glfrag = null;
        public GLObject glcomp = null;
        public GLVertoutput glvertout = null;
        public TransformFeedbackPrimitiveType vertoutPrimitive = TransformFeedbackPrimitiveType.Points;
        public GLFragoutput glfragout = null;
        public List<MultiDrawCall> drawcalls = new List<MultiDrawCall>();
        public List<CompCall> compcalls = new List<CompCall>();
        public List<Res<GLTexture>> textures = new List<Res<GLTexture>>();
        public List<Res<GLSampler>> sampler = new List<Res<GLSampler>>();
        public List<GLMethod> invoke = new List<GLMethod>();
        public List<CsharpClass> csexec = new List<CsharpClass>();
        #endregion

        #region HELP STRUCT
        public enum DrawFunc
        {
            ArraysIndirect    = 0 | 2 | 0 | 0,
            ArraysInstanced   = 0 | 0 | 0 | 0,
            ElementsIndirect  = 0 | 2 | 4 | 8,
            ElementsInstanced = 0 | 2 | 0 | 8,
            TransformFeedback = 1 | 2 | 0 | 0,
        }

        public class MultiDrawCall
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
                GL.BindVertexArray(this.vertexin);

                switch (this.drawfunc)
                {
                    case DrawFunc.ArraysIndirect:
                        GL.BindBuffer(BufferTarget.DrawIndirectBuffer, this.indirect);
                        foreach (var draw in this.cmd)
                            GL.DrawArraysIndirect(draw.mode, draw.indirectPtr);
                        break;

                    case DrawFunc.ArraysInstanced:
                        foreach (var draw in this.cmd)
                            GL.DrawArraysInstancedBaseInstance(
                                draw.mode, draw.vBaseVertex, draw.vVertexCount,
                                draw.vInstanceCount, draw.vBaseInstance);
                        break;

                    case DrawFunc.ElementsIndirect:
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexbuf);
                        GL.BindBuffer(BufferTarget.DrawIndirectBuffer, this.indirect);
                        foreach (var draw in this.cmd)
                            GL.DrawElementsIndirect(draw.mode, (All)draw.indextype, draw.indirectPtr);
                        break;

                    case DrawFunc.ElementsInstanced:
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexbuf);
                        foreach (var draw in this.cmd)
                            GL.DrawElementsInstancedBaseVertexBaseInstance(
                                draw.mode, draw.iIndexCount, draw.indextype,
                                draw.iBaseIndex, draw.iInstanceCount,
                                draw.iBaseVertex, draw.iBaseInstance);
                        break;

                    case DrawFunc.TransformFeedback:
                        foreach (var draw in this.cmd)
                            GL.DrawTransformFeedbackStreamInstanced(draw.mode,
                                this.vertout, draw.voStream, draw.voInstanceCount);
                        break;
                }
            }
        }

        public struct DrawCall
        {
            public PrimitiveType mode;
            public DrawElementsType indextype;
            private int arg0;
            private int arg1;
            private int arg2;
            private int arg3;
            private int arg4;

            public DrawCall(DrawFunc drawfunc, PrimitiveType mode, DrawElementsType indextype, List<int> arg)
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
            public IntPtr iBaseIndex { get { return (IntPtr)(arg1*Math.Max(1, (int)indextype - (int)DrawElementsType.UByte)); } set { arg1 = (int)value; } }
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

        public struct CompCall
        {
            public uint numGroupsX;
            public uint numGroupsY;
            public uint numGroupsZ;
            public int indirect { get { return (int)numGroupsX; } }
            public IntPtr indirectPtr { get { return (IntPtr)numGroupsY; } }

            public void compute()
            {
                if (this.indirect > 0)
                {
                    // bind indirect compute call buffer
                    GL.BindBuffer(BufferTarget.DispatchIndirectBuffer, this.indirect);
                    // execute compute shader
                    GL.DispatchComputeIndirect(this.indirectPtr);
                }
                else
                    // execute compute shader
                    GL.DispatchCompute(this.numGroupsX, this.numGroupsY, this.numGroupsZ);
            }
        }

        public struct Res<T>
        {
            public T obj;
            public int unit;

            public Res(T obj, int unit)
            {
                this.obj = obj;
                this.unit = unit;
            }
        }
        
        public struct GLMethod
        {
            public MethodInfo mtype;
            public object[] inval;

            public GLMethod(MethodInfo mtype, object[] inval)
            {
                this.mtype = mtype;
                this.inval = inval;
            }
        }

        public class CsharpClass
        {
            private object instance = null;
            private MethodInfo bind = null;
            private MethodInfo unbind = null;

            public CsharpClass(object instance, GLControl glControl)
            {
                this.instance = instance;

                // get bind method from main class instance
                bind = instance.GetType().GetMethod("Bind", new Type[] {
                    typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)
                });

                // get unbind method from main class instance
                unbind = instance.GetType().GetMethod("Unbind", new Type[] { typeof(int) });

                #region SEARCH FOR EVENT HANDLERS AND ADD THEM TO GLCONTROL

                // get all public methods and check whether they can be used as event handlers for glControl
                var methods = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (var method in methods)
                {
                    EventInfo eventInfo = glControl.GetType().GetEvent(method.Name);
                    if (eventInfo != null)
                    {
                        Delegate csmethod = Delegate.CreateDelegate(eventInfo.EventHandlerType, instance, method.Name);
                        eventInfo.AddEventHandler(glControl, csmethod);
                    }
                }

                #endregion
            }

            public void Bind(int program, int width, int height, int widthTex, int heightTex)
            {
                if (bind != null)
                    bind.Invoke(instance, new object[] { program, width, height, widthTex, heightTex });
            }

            public void Unbind(int program)
            {
                if (unbind != null)
                    unbind.Invoke(instance, new object[] { program });
            }
        }
        #endregion

        public GLPass(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(this, ref cmds);

            // PARSE COMMANDS
            for (int i = 0; i < cmds.Length; i++)
            {
                var cmd = cmds[i];

                // skip if already processed commands
                if (cmd == null)
                    continue;
                switch(cmd[0])
                {
                    case "draw": ParseDrawCall(i+1, cmd, classes); break;
                    case "compute": ParseComputeCall(i+1, cmd, classes);  break;
                    case "tex": textures.Add(ParseTexCmd<GLTexture>(i + 1, cmd, classes)); break;
                    case "samp": sampler.Add(ParseTexCmd<GLSampler>(i + 1, cmd, classes)); break;
                    case "exec": csexec.Add(ParseCsharpExec(i + 1, cmd, classes)); break;
                    default: invoke.Add(ParseOpenGLCall(cmd)); break;
                }
            }

            // GET VERTEX AND FRAGMENT OUTPUT BINDINGS
            if (fragout != null && (glfragout = classes.FindClass<GLFragoutput>(fragout)) == null)
                throw new Exception(Dict.NotFoundMsg("pass", name, "fragout", fragout));
            if (vertout != null && vertout.Length > 0 && (glvertout = classes.FindClass<GLVertoutput>(vertout[0])) == null)
                throw new Exception(Dict.NotFoundMsg("pass", name, "vertout", vertout[0]));

            // CREATE OPENGL OBJECT
            glname = GL.CreateProgram();

            // Attach shader objects.
            // First try attaching a compute shader. If that
            // fails, try attaching the default shader pipeline.
            if ((glcomp = attach(comp, classes)) == null)
            {
                glvert = attach(vert, classes);
                gltess = attach(tess, classes);
                gleval = attach(eval, classes);
                glgeom = attach(geom, classes);
                glfrag = attach(frag, classes);
            }

            // specify vertex output varyings of the shader program
            if (glvertout != null)
                setVertexOutputVaryings(vertout);

            // link program
            GL.LinkProgram(glname);

            // detach shader objects
            if (glcomp != null)
                GL.DetachShader(glname, glcomp.glname);
            else
            {
                if (glvert != null) GL.DetachShader(glname, glvert.glname);
                if (gltess != null) GL.DetachShader(glname, gltess.glname);
                if (gleval != null) GL.DetachShader(glname, gleval.glname);
                if (glgeom != null) GL.DetachShader(glname, glgeom.glname);
                if (glfrag != null) GL.DetachShader(glname, glfrag.glname);
            }

            // check for link errors
            int status;
            GL.GetProgram(glname, GetProgramParameterName.LinkStatus, out status);
            if (status != 1)
                throw new Exception("ERROR in pass " + name + ":\n" + GL.GetProgramInfoLog(glname));
        }

        public void Exec(int width, int height)
        {
            int widthOut = width;
            int heightOut = height;

            // BIND FRAGMENT OUTPUT
            // (widthOut and heightOut must be 
            // computed before setting the glViewport)
            if (glfragout != null)
            {
                // bind framebuffer output
                widthOut = glfragout.width;
                heightOut = glfragout.height;
                glfragout.Bind();
            }

            // SET DEFAULT VIEWPORT
            GL.Viewport(0, 0, widthOut, heightOut);

            // CALL USER SPECIFIED OPENGL FUNCTIONS
            foreach (var glcall in invoke)
                glcall.mtype.Invoke(null, glcall.inval);

            // BIND PROGRAM
            GL.UseProgram(glname);

            // BIND VERTEX OUTPUT (transform feedback)
            // (must be done after glUseProgram)
            if (glvertout != null)
                glvertout.Bind(vertoutPrimitive);

            // BIND TEXTURES
            foreach (var t in textures)
                t.obj.Bind(t.unit);
            foreach (var s in sampler)
                GL.BindSampler(s.unit, s.obj.glname);
            foreach (var e in csexec)
                e.Bind(glname, width, height, widthOut, heightOut);

            // EXECUTE DRAW CALLS
            foreach (var call in drawcalls)
                call.draw();

            // EXECUTE COMPUTE CALLS
            foreach (var call in compcalls)
                call.compute();

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
                t.obj.Unbind(t.unit);
            foreach (var s in sampler)
                GL.BindSampler(s.unit, 0);
            foreach (var e in csexec)
                e.Unbind(glname);
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
        private void ParseDrawCall(int cmdidx, string[] cmd, Dictionary<string, GLObject> classes)
        {
            List<int> arg = new List<int>();
            GLVertinput vertexin = null;
            GLVertoutput vertout = null;
            GLBuffer indexbuf = null;
            GLBuffer indirect = null;
            bool modeIsSet = false;
            bool typeIsSet = false;
            PrimitiveType primitive = 0;
            DrawElementsType indextype = 0;
            int val;

            // parse draw call arguments
            for (var i = 1; i < cmd.Length; i++)
            {
                if (TryParseObject(classes, cmd[i], ref vertexin)) continue;
                if (TryParseObject(classes, cmd[i], ref vertout)) continue;
                if (TryParseObject(classes, cmd[i], ref indexbuf)) continue;
                if (TryParseObject(classes, cmd[i], ref indirect)) continue;
                if (int.TryParse(cmd[i], out val)) arg.Add(val);
                else if (typeIsSet == false && Enum.TryParse(cmd[i], true, out indextype))
                    typeIsSet = true;
                else if (modeIsSet == false && Enum.TryParse(cmd[i], true, out primitive))
                    modeIsSet = true;
            }

            // -) a draw call must specify a primitive type
            if (modeIsSet == false)
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0],
                    ": Draw call must specify a primitive type "
                    + "(e.g. triangles, trianglefan, lines, points, ...)."));

            // determine the right draw call function
            int bits = (vertout != null ? 1 : 0) 
                | (indexbuf != null ? 2 : 0)
                | (indirect != null ? 4 : 0)
                | (typeIsSet ? 8 : 0);

            if (!Enum.IsDefined(typeof(DrawFunc), bits))
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0],
                    "Draw call function not recognized or ambiguous."));

            DrawFunc drawfunc = (DrawFunc)bits;
                 
            // get index buffer object (if present) and find existing MultiDraw class
            MultiDrawCall multidrawcall = drawcalls.Find(x
                => x.vertexin == (vertexin != null ? vertexin.glname : 0)
                && x.indexbuf == (indexbuf != null ? indexbuf.glname : 0)
                && x.vertout  == (vertout  != null ?  vertout.glname : 0)
                && x.indirect == (indirect != null ? indirect.glname : 0));

            if (multidrawcall == null)
            {
                multidrawcall = new MultiDrawCall(drawfunc, vertexin, vertout, indexbuf, indirect);
                drawcalls.Add(multidrawcall);
            }

            // add new draw command to the MultiDraw class
            multidrawcall.cmd.Add(new DrawCall(drawfunc, primitive, indextype, arg));
        }

        private void ParseComputeCall(int cmdidx, string[] cmd, Dictionary<string, GLObject> classes)
        {
            // check for errors
            if (cmd.Length != 3 || cmd.Length != 4)
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0],
                    "Compute command does not provide enough arguments "
                    + "(e.g., 'compute num_groups_X num_groups_y num_groups_z' or "
                    + "'compute buffer_name indirect_pointer')."));

            CompCall call = new CompCall();

            // this is an indirect compute call
            if (cmd.Length == 3)
            {
                // indirect compute call buffer
                call.numGroupsX = (uint)ParseObject<GLBuffer>(classes, cmdidx, cmd, 1,
                    ": First argument of compute command must be a buffer name").glname;

                // indirect compute call buffer pointer
                call.numGroupsY = ParseType<uint>(cmdidx, cmd, 2,
                    "Argument must be an unsigned integer, specifying a pointer into the indirect compute call buffer.");
            }
            // this is a normal compute call
            else
            {
                // number of compute groups
                call.numGroupsX = ParseType<uint>(cmdidx, cmd, 1,
                    "Argument must be an unsigned integer, specifying the number of compute groups in X.");
                call.numGroupsY = ParseType<uint>(cmdidx, cmd, 2,
                    "Argument must be an unsigned integer, specifying the number of compute groups in Y.");
                call.numGroupsZ = ParseType<uint>(cmdidx, cmd, 3,
                    "Argument must be an unsigned integer, specifying the number of compute groups in Z.");
            }
            
            compcalls.Add(call);
        }

        private Res<T> ParseTexCmd<T>(int cmdidx, string[] cmd, Dictionary<string, GLObject> classes)
        {
            GLObject obj = null;
            int unit = -1;

            // parse command arguments
            for (var i = 1; i < cmd.Length; i++)
            {
                if (obj == null && classes.TryGetValue(cmd[i], out obj) && obj.GetType() == typeof(T))
                    continue;
                int.TryParse(cmd[i], out unit);
            }

            // check for errors
            if (obj == null)
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0],
                    "Texture name could not be found."));
            if (unit < 0)
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0],
                    "tex command must specify a unit (e.g. tex tex_name 0)."));
            
            // add to texture list
            return new Res<T>((T)Convert.ChangeType(obj, typeof(T)), unit);
        }

        private GLMethod ParseOpenGLCall(string[] cmd)
        {
            // find OpenGL method
            var mtype = FindMethod(cmd[0], cmd.Length - 1);
            if (mtype == null)
                throw new Exception("ERROR in pass " + name 
                    + ": Unknown command " + string.Join(" ", cmd) + ".");
            // get method parameter types
            var param = mtype.GetParameters();
            object[] inval = new object[param.Length];
            // convert strings to parameter types
            for (int i = 0; i < param.Length; i++)
                if (param[i].ParameterType.IsEnum)
                    inval[i] = Convert.ChangeType(Enum.Parse(param[i].ParameterType, cmd[i + 1], true), param[i].ParameterType);
                else
                    inval[i] = Convert.ChangeType(cmd[i + 1], param[i].ParameterType, App.culture);
            
            return new GLMethod(mtype, inval);
        }

        private CsharpClass ParseCsharpExec(int cmdidx, string[] cmd, Dictionary<string, GLObject> classes)
        {
            // check if command provides the correct amount of parameters
            if (cmd.Length < 3)
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0],
                    "Not enough arguments for exec command."));

            // get csharp object
            GLObject obj;
            if (classes.TryGetValue(cmd[1], out obj) == false
                || obj.GetType() != typeof(GLCsharp))
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0],
                    "Could not find csharp code '" + cmd[1] + "' of command '"
                    + string.Join(" ", cmd) + "'."));

            GLCsharp clazz = (GLCsharp)obj;
            
            // get GLControl
            if (classes.TryGetValue(GraphicControl.nullname, out obj) == false
                || obj.GetType() != typeof(GraphicControl))
                throw new Exception("INTERNAL_ERROR in pass " + name
                    + ": Could not find default GLControl.");
            GraphicControl glControl = (GraphicControl)obj;

            // create instance of defined main class
            var instance = clazz.CreateInstance(cmd[2], cmd);
            if (instance == null)
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0],
                    "Main class '" + cmd[2] + "' could not be found."));

            return new CsharpClass(instance, glControl.control);
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

        private GLObject attach(string sh, Dictionary<string, GLObject> classes)
        {
            if (sh == null)
                return null;
            GLObject glsh;
            // get shader from class list
            if (classes.TryGetValue(sh, out glsh) && glsh.GetType() == typeof(GLShader))
                // attach to program
                GL.AttachShader(glname, glsh.glname);
            else
                throw new Exception("ERROR in pass " + name + ": Invalid name '" + sh + "'.");
            return glsh;
        }

        private void setVertexOutputVaryings(string[] varyings)
        {
            // the vertout command needs at least 3 arguments
            if (varyings.Length < 3)
                throw new Exception("ERROR in pass " + name + ": vertout command does not have "
                    + "enough arguments (e.g. vertout vertout_name points varying_name).");

            // parse vertex output primitive type
            if (!Enum.TryParse(varyings[1], true, out vertoutPrimitive))
                throw new Exception("ERROR in pass " + name + ": vertout command does not support "
                    + "the specified primitive type '" + varyings[1] + "' "
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
                throw new Exception("ERROR in pass " + name + ": vertout command does not specify "
                    + "shader output varying names (e.g. vertout vertout_name points varying_name).");
        }
        
        private T ParseType<T>(int cmdidx, string[] cmd, int arg, string info)
        {
            try
            {
                return (T)Convert.ChangeType(cmd[arg], typeof(T), App.culture);
            }
            catch
            {
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0], arg, info));
            }
        }

        private bool TryParseType<T>(object obj, ref T output)
        {
            try
            {
                output = (T)Convert.ChangeType(obj, typeof(T), App.culture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private T ParseObject<T>(Dictionary<string, GLObject> classes, int cmdidx, string[] cmd, int arg, string info)
            where T : GLObject
        {
            GLObject tmp;
            if (classes.TryGetValue(cmd[arg], out tmp) && tmp.GetType() == typeof(T))
                throw new Exception(ErrorMsg(name, cmdidx, cmd[0], arg, info));
            return (T)tmp;
        }

        private bool TryParseObject<T>(Dictionary<string, GLObject> classes, string name, ref T obj)
            where T : GLObject
        {
            GLObject tmp;
            if (obj == null && classes.TryGetValue(name, out tmp) && tmp.GetType() == typeof(T))
            {
                obj = (T)tmp;
                return true;
            }
            return false;
        }
        
        private static string ErrorMsg(string passname, int cmdidx, string cmdname, int arg, string info)
        {
            return "ERROR in pass " + passname
                + ", command " + cmdidx + " '" + cmdname + "'"
                + (arg >= 0 ? (", argument " + arg) : "")
                + (info != null ? (": " + info) : ".");
        }

        private static string ErrorMsg(string passname, int cmdidx, string cmdname, string info)
        {
            return ErrorMsg(passname, cmdidx, cmdname, -1, info);
        }
        #endregion
    }
}
