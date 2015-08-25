using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace gled
{
    class GLPass : GLObject
    {
        public string vert = null;
        public string tess = null;
        public string eval = null;
        public string geom = null;
        public string frag = null;
        public string geomout = null;
        public string fragout = null;
        public string[] option = null;
        public GLObject glvert = null;
        public GLObject gltess = null;
        public GLObject gleval = null;
        public GLObject glgeom = null;
        public GLObject glfrag = null;
        public GLObject glgeomout = null;
        public GLObject glfragout = null;
        public GLCamera glcamera = null;
        public List<MultiDraw> calls = new List<MultiDraw>();
        public int g_view = -1;
        public int g_proj = -1;
        public int g_viewproj = -1;
        public int g_info = -1;

        public class MultiDraw
        {
            public int vi;
            public int ib;
            public PrimitiveType mode;
            public VertexAttribPointerType type;
            public List<DrawIndirectCmd> cmd;
            public DrawIndirectCmd[] glcmd;
        }

        public struct DrawIndirectCmd
        {
            public uint count;
            public uint instanceCount;
            public uint firstIndex;
            public uint baseVertex;
            public uint baseInstance;
        }

        public GLPass(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, args);

            // parse draw call arguments
            List<uint> arg = new List<uint>();
            foreach (var call in args)
            {
                // skip if arg is not a draw command
                if (!call[0].Equals("draw") || call.Length < 5)
                    continue;

                // parse draw call arguments
                arg.Clear();
                GLVertinput vi = null;
                GLBuffer ib = null;
                GLObject obj;
                PrimitiveType mode = 0;
                VertexAttribPointerType type = 0;
                uint val;

                for (var i = 1; i < call.Length; i++)
                {
                    if (vi == null && classes.TryGetValue(call[i], out obj) && obj.GetType() == typeof(GLVertinput))
                        vi = (GLVertinput)obj;
                    else if (ib == null && classes.TryGetValue(call[i], out obj) && obj.GetType() == typeof(GLBuffer))
                        ib = (GLBuffer)obj;
                    else if (type == 0 && Enum.TryParse(call[i], true, out type)) { }
                    else if (mode == 0 && Enum.TryParse(call[i], true, out mode)) { }
                    else if (UInt32.TryParse(call[i], out val))
                        arg.Add(val);
                }

                // check for validity of the draw call
                if (vi == null)
                    throw new Exception("");
                if (mode == 0)
                    throw new Exception("");
                if (ib != null && type == 0)
                    throw new Exception("");
                if (arg.Count == 0)
                    throw new Exception("");

                // get index buffer object (if present) and find existing MultiDraw class
                MultiDraw draw = calls.Find(x => x.vi == vi.glname && x.ib == (ib != null ? ib.glname : 0));
                if (draw == null)
                {
                    draw = new MultiDraw();
                    draw.cmd = new List<DrawIndirectCmd>();
                    draw.vi = vi.glname;
                    draw.ib = ib != null ? ib.glname : 0;
                    draw.mode = mode;
                    draw.type = type;
                    calls.Add(draw);
                }

                // add new draw command to the MultiDraw class
                DrawIndirectCmd cmd = new DrawIndirectCmd();
                cmd.count         = arg.Count >= 1 ? arg[0] : 0;
                cmd.instanceCount = arg.Count >= 2 ? arg[1] : 0;
                cmd.firstIndex    = arg.Count >= 3 ? arg[2] : 0;
                cmd.baseVertex    = arg.Count >= 4 ? arg[3] : 0;
                cmd.baseInstance  = arg.Count >= 5 ? arg[4] : 0;
                draw.cmd.Add(cmd);
            }
            
            // convert draw command list to array so it can be used by OpenGL
            foreach (var call in calls)
                call.glcmd = call.cmd.ToArray();

            // GET CAMERA OBJECT
            GLObject cam;
            classes.TryGetValue(GLCamera.cameraname, out cam);
            if (cam.GetType() == typeof(GLCamera))
                glcamera = (GLCamera)cam;

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
            
            // GET PROGRAM UNIFORMS
            g_view = GL.GetUniformLocation(glname, "g_view");
            g_proj = GL.GetUniformLocation(glname, "g_proj");
            g_viewproj = GL.GetUniformLocation(glname, "g_viewproj");
            g_info = GL.GetUniformLocation(glname, "g_info");
            
        }

        private GLObject attach(string sh, Dictionary<string, GLObject> classes)
        {
            if (sh == null)
                return null;
            GLObject glsh;
            if (classes.TryGetValue(sh, out glsh) && glsh.GetType() == typeof(GLShader))
                GL.AttachShader(glname, glsh.glname);
            else
                throw new Exception("ERROR in pass " + name + ": Invalid name '" + sh + "'.");
            return glsh;
        }

        public void Exec()
        {
            GL.ClearColor(Color.SkyBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(glname);
            if (g_view >= 0)
            {
                Matrix4 view = glcamera.view;
                GL.ProgramUniformMatrix4(glname, g_view, 1, false, ref view.Row0.W);
            }
            if (g_proj >= 0)
            {
                Matrix4 proj = glcamera.proj;
                GL.ProgramUniformMatrix4(glname, g_proj, 1, false, ref proj.Row0.W);
            }
            if (g_viewproj >= 0)
            {
                Matrix4 viewproj = glcamera.viewproj;
                GL.ProgramUniformMatrix4(glname, g_proj, 1, false, ref viewproj.Row0.W);
            }
            if (g_info >= 0)
            {
                Vector4 info = glcamera.info;
                GL.ProgramUniformMatrix4(glname, g_proj, 1, false, ref info.W);
            }

            foreach (var call in calls)
            {
                // bind vertex buffer to input stream
                // (needs to be done before binding an ElementArrayBuffer)
                GL.BindVertexArray(call.vi);
                // bin index buffer to ElementArrayBuffer target
                if (call.ib != 0)
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, call.ib);
                // execute multiple indirect draw commands
                GL.MultiDrawElementsIndirect((All)call.mode, (All)call.type, call.glcmd, call.glcmd.Length, Marshal.SizeOf(typeof(DrawIndirectCmd)));
            }

            GL.UseProgram(0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteProgram(glname);
                glname = 0;
            }
        }
    }
}
