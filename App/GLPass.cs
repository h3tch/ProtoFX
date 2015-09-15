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
        public string vertout = null;
        public string fragout = null;
        public GLObject glvert = null;
        public GLObject gltess = null;
        public GLObject gleval = null;
        public GLObject glgeom = null;
        public GLObject glfrag = null;
        public GLVertoutput glvertout = null;
        public GLFragoutput glfragout = null;
        public List<MultiDrawCall> calls = new List<MultiDrawCall>();
        public List<Res<GLTexture>> textures = new List<Res<GLTexture>>();
        public List<Res<GLSampler>> sampler = new List<Res<GLSampler>>();
        public List<GLMethod> invoke = new List<GLMethod>();
        public List<CsharpClass> csexec = new List<CsharpClass>();

        #endregion

        #region HELP STRUCT

        public class MultiDrawCall
        {
            public int vi;
            public int ib;
            public List<DrawCall> cmd;
        }

        public struct DrawCall
        {
            public PrimitiveType mode;
            public int indexcount;
            public DrawElementsType indextype;
            public IntPtr baseindex;
            public int instancecount;
            public int basevertex;
            public int baseinstance;
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

        public GLPass(string dir, string name, string annotation, string text, GLDict classes)
            : base(name, annotation)
        {
            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(this, ref cmds);

            // PARSE COMMANDS
            foreach (var cmd in cmds)
            {
                // skip if already processed commands
                if (cmd == null)
                    continue;
                switch(cmd[0])
                {
                    case "draw": ParseDrawCall(cmd, classes); break;
                    case "tex": textures.Add(ParseTexCmd<GLTexture>(cmd, classes)); break;
                    case "samp": sampler.Add(ParseTexCmd<GLSampler>(cmd, classes)); break;
                    case "exec": csexec.Add(ParseCsharpExec(cmd, classes)); break;
                    default: invoke.Add(ParseOpenGLCall(cmd)); break;
                }
            }

            // GET VERTEX AND FRAGMENT OUTPUT BINDINGS
            if (fragout != null && (glfragout = classes.FindClass<GLFragoutput>(fragout)) == null)
                throw new Exception(GLDict.NotFoundMsg("pass", name, "fragout", fragout));
            if (vertout != null && (glvertout = classes.FindClass<GLVertoutput>(vertout)) == null)
                throw new Exception(GLDict.NotFoundMsg("pass", name, "vertout", vertout));

            // CREATE OPENGL OBJECT
            glname = GL.CreateProgram();

            // attach shader objects
            glvert = attach(vert, classes);
            gltess = attach(tess, classes);
            gleval = attach(eval, classes);
            glgeom = attach(geom, classes);
            glfrag = attach(frag, classes);

            // link program
            GL.LinkProgram(glname);

            // detach shader objects
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

            // BIND FRAMEBUFFER
            if (glfragout != null)
            {
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

            // BIND TEXTURES
            foreach (var t in textures)
                t.obj.Bind(t.unit);
            foreach (var s in sampler)
                GL.BindSampler(s.unit, s.obj.glname);
            foreach (var e in csexec)
                e.Bind(glname, width, height, widthOut, heightOut);

            // EXECUTE DRAW CALLS
            foreach (var call in calls)
            {
                // bind vertex buffer to input stream
                // (needs to be done before binding an ElementArrayBuffer)
                GL.BindVertexArray(call.vi);
                if (call.ib != 0)
                {
                    // bin index buffer to ElementArrayBuffer target
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, call.ib);
                    // execute multiple indirect draw commands
                    foreach (var draw in call.cmd)
                        GL.DrawElementsInstancedBaseVertexBaseInstance(
                            draw.mode, draw.indexcount, draw.indextype,
                            draw.baseindex, draw.instancecount,
                            draw.basevertex, draw.baseinstance);
                }
                else
                {
                    // execute multiple indirect draw commands
                    foreach (var draw in call.cmd)
                        GL.DrawArraysInstancedBaseInstance(
                            draw.mode, draw.baseindex.ToInt32(), draw.indexcount,
                            draw.baseinstance, draw.instancecount);
                }
            }

            // UNBIND OPENGL RESOURCES
            GL.UseProgram(0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            if (glfragout != null)
                glfragout.Unbind();
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

        #region PARSE GLED COMMANDS

        private void ParseDrawCall(string[] cmd, Dictionary<string, GLObject> classes)
        {
            List<int> arg = new List<int>();
            GLVertinput vi = null;
            GLBuffer ib = null;
            GLObject obj;
            PrimitiveType mode = 0;
            DrawElementsType type = 0;
            int val;

            // parse draw call arguments
            for (var i = 1; i < cmd.Length; i++)
            {
                if (vi == null && classes.TryGetValue(cmd[i], out obj) && obj.GetType() == typeof(GLVertinput))
                    vi = (GLVertinput)obj;
                else if (ib == null && classes.TryGetValue(cmd[i], out obj) && obj.GetType() == typeof(GLBuffer))
                    ib = (GLBuffer)obj;
                else if (Int32.TryParse(cmd[i], out val))
                    arg.Add(val);
                else if (type == 0 && Enum.TryParse(cmd[i], true, out type)) { }
                else if (mode == 0 && Enum.TryParse(cmd[i], true, out mode)) { }
            }
            
            #region CHECK VALIDITY OF DRAW CALL
            // -) a draw call must specify a primitive type
            if (mode == 0)
                throw new Exception("ERROR in pass " + name
                    + ": Draw call " + calls.Count + " must specify a primitive type (e.g. triangles, trianglefan, lines, ...).");
            // -) a draw call mast specify the number of vertices to draw
            if (arg.Count == 0)
                throw new Exception("ERROR in pass " + name
                    + ": Draw call " + calls.Count + " must specify the number of indices/vertices to draw.");
            // -) a draw call needs to specify a vertex input even if no vertices are drawn (NV BUG?)
            if (vi == null)
                throw new Exception("ERROR in pass " + name
                    + ": Draw call " + calls.Count + " must specify a vertinput name.");
            // -) if indexed drawing is used, the draw call must specify an index buffer
            if (ib != null && type == 0)
                throw new Exception("ERROR in pass " + name
                    + ": Draw call " + calls.Count + " uses index vertices and must therefore specify an index type "
                    + "(e.g. unsignedshort, unsignedin).");
            #endregion

            #region ADD DRAW CALL TO LIST
            // get index buffer object (if present) and find existing MultiDraw class
            MultiDrawCall multidrawcall = calls.Find(x => x.vi == vi.glname && x.ib == (ib != null ? ib.glname : 0));
            if (multidrawcall == null)
            {
                multidrawcall = new MultiDrawCall();
                multidrawcall.cmd = new List<DrawCall>();
                multidrawcall.vi = vi != null ? vi.glname : 0;
                multidrawcall.ib = ib != null ? ib.glname : 0;
                calls.Add(multidrawcall);
            }

            // add new draw command to the MultiDraw class
            DrawCall drawcall = new DrawCall();
            drawcall.mode = mode;
            drawcall.indextype = type;
            drawcall.indexcount = arg.Count >= 1 ? arg[0] : 0;
            drawcall.baseindex = (IntPtr)(arg.Count >= 2 ? arg[1] : 0);
            drawcall.instancecount = arg.Count >= 3 ? arg[2] : 1;
            drawcall.baseinstance = arg.Count >= 4 ? arg[3] : 0;
            drawcall.basevertex = arg.Count >= 5 ? arg[4] : 0;
            multidrawcall.cmd.Add(drawcall);
            #endregion
        }

        private Res<T> ParseTexCmd<T>(string[] cmd, Dictionary<string, GLObject> classes)
        {
            GLObject obj = null;
            int unit = -1;

            // parse command arguments
            for (var i = 1; i < cmd.Length; i++)
            {
                if (obj == null && classes.TryGetValue(cmd[i], out obj) && obj.GetType() == typeof(T))
                    continue;
                Int32.TryParse(cmd[i], out unit);
            }

            // check for errors
            if (obj == null)
                throw new Exception("ERROR in pass " + name
                    + ": Texture name of tex command " + textures.Count + " could not be found.");
            if (unit < 0)
                throw new Exception("ERROR in pass " + name
                    + ": tex command " + textures.Count + " must specify a unit (e.g. tex tex_name 0).");

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

        private CsharpClass ParseCsharpExec(string[] cmd, Dictionary<string, GLObject> classes)
        {
            // check if command provides the correct amount of paramenters
            if (cmd.Length < 3)
                throw new Exception("ERROR in pass " + name + ": "
                    + "Not enough arguments for exec command '"+ string.Join(" ", cmd) + "'.");

            // get csharp object
            GLObject obj;
            if (classes.TryGetValue(cmd[1], out obj) == false || obj.GetType() != typeof(GLCsharp))
                throw new Exception("ERROR in pass " + name + ": Could not find csharp code '" + cmd[1]
                    + "' of command '" + string.Join(" ", cmd) + "'.");
            GLCsharp clazz = (GLCsharp)obj;
            
            // get GLControl
            if (classes.TryGetValue(GledControl.nullname, out obj) == false || obj.GetType() != typeof(GledControl))
                throw new Exception("INTERNAL_ERROR in pass " + name + ": Cound not find default GLControl.");
            GledControl glControl = (GledControl)obj;

            // create instance of defined main class
            var instance = clazz.CreateInstance(cmd[2], cmd);
            if (instance == null)
                throw new Exception("ERROR in pass " + name + ": "
                    + "Main class '" + cmd[2] + "' of command '"
                    + string.Join(" ", cmd) + "' could not be found.");

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

        #endregion
    }
}
