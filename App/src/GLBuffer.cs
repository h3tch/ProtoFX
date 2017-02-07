using System;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;
using System.Reflection;
using UsageHint = OpenTK.Graphics.OpenGL4.BufferUsageHint;
using ParameterName = OpenTK.Graphics.OpenGL4.BufferParameterName;

namespace App
{
    class GLBuffer : GLObject
    {
        #region FIELDS
        [FxField] public int Size { get; private set; } = 0;
        [FxField] public UsageHint Usage { get; private set; } = UsageHint.StaticDraw;
        #endregion

        /// <summary>
        /// Construct GLBuffer object.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <param name="anno">Annotation of the object.</param>
        /// <param name="usage">How the buffer should be used by the program.</param>
        /// <param name="size">The memory size to be allocated in bytes.</param>
        /// <param name="data">Optionally initialize the buffer object with the specified data.</param>
        public GLBuffer(string name, string anno, UsageHint usage, int size, byte[] data = null)
            : base(name, anno)
        {
            var err = new CompileException($"buffer '{name}'");

            Size = size;
            Usage = usage;

            // CREATE OPENGL OBJECT
            CreateBuffer(data);
            if (HasErrorOrGlError(err, "", -1))
                throw err;
        }

        /// <summary>
        /// Link GLBuffer to existing OpenGL buffer. Used
        /// to provide debug information in the debug view.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <param name="anno">Annotation of the object.</param>
        /// <param name="glname">OpenGL object to like to.</param>
        public GLBuffer(string name, string anno, int glname) : base(name, anno)
        {
            int s, u;
            this.glname = glname;
            GL.GetNamedBufferParameter(glname, ParameterName.BufferSize, out s);
            GL.GetNamedBufferParameter(glname, ParameterName.BufferUsage, out u);
            Size = s;
            Usage = (UsageHint)u;
        }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLBuffer(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"buffer '{block.Name}'");

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(block, err);

            // PARSE COMMANDS
            List<byte[]> datalist = new List<byte[]>();

            foreach (var cmd in block["txt"])
                datalist.Add(LoadText(cmd, scene, err | $"command {cmd.Name} 'txt'"));

            foreach (var cmd in block["xml"])
                datalist.Add(LoadXml(cmd, scene, err | $"command {cmd.Name} 'xml'"));

            // merge data into a single array
            var iter = datalist.Cat();
            var data = iter.Take(Size == 0 ? iter.Count() : Size).ToArray();
            if (Size == 0)
                Size = data.Length;

            // CONVERT DATA
            var clazz = block["class"].FirstOrDefault();
            if (clazz != null && Size > 0)
            {
                var converter = GLCsharp.GetMethod(clazz, scene, err);
                data = (byte[])converter?.Invoke(null, new[] { data });
            }

            // CREATE OPENGL OBJECT
            CreateBuffer(data);
            if (HasErrorOrGlError(err, block))
                throw err;
        }
        
        /// <summary>
        /// Read whole GPU buffer data.
        /// </summary>
        /// <returns>Return GPU buffer data as byte array.</returns>
        public byte[] Read()
        {
            // allocate buffer memory
            byte[] data = null;
            Read(ref data);
            return data;
        }

        /// <summary>
        /// Read GPU buffer data.
        /// </summary>
        /// <returns>Return GPU buffer data as byte array.</returns>
        public void Read(ref byte[] data, int offset = 0)
        {
            // check buffer size
            if (Size == 0)
            {
                var rs = "Buffer is empty".ToCharArray().ToBytes();
                if (data == null) data = rs; else rs.CopyTo(data, 0);
                return;
            }

            // check read flag of the buffer
            int flags;
            GL.GetNamedBufferParameter(glname, ParameterName.BufferStorageFlags, out flags);
            if ((flags & (int)BufferStorageFlags.MapReadBit) == 0)
            {
                var rs = "Buffer cannot be read".ToCharArray().ToBytes();
                if (data == null) data = rs; else rs.CopyTo(data, 0);
                return;
            }

            // if necessary allocate bytes
            if (data == null)
                data = new byte[Size];

            // map buffer and copy data to CPU memory
            var mapSize = Math.Min(Size, data.Length);
            var dataPtr = GL.MapNamedBufferRange(glname, (IntPtr)offset, mapSize, BufferAccessMask.MapReadBit);
            Marshal.Copy(dataPtr, data, 0, mapSize);
            GL.UnmapNamedBuffer(glname);
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            if (glname > 0)
            {
                GL.DeleteBuffer(glname);
                glname = 0;
            }
        }

        /// <summary>
        /// Get the OpenGL object label of the buffer object.
        /// </summary>
        /// <param name="glname"></param>
        /// <returns></returns>
        public static string GetLabel(int glname) => GetLabel(ObjectLabelIdentifier.Buffer, glname);

        /// <summary>
        /// Find OpenGL buffers.
        /// </summary>
        /// <param name="existing">List of objects already existent in the scene.</param>
        /// <param name="range">Range in which to search for external objects (starting with 0).</param>
        /// <returns></returns>
        public static IEnumerable<GLObject> FindBuffers(GLObject[] existing, int range = 64)
            => FindObjects(existing, new[] { typeof(GLBuffer) }, GLIsBufMethod, GLBufLabel, range);
        private static MethodInfo GLIsBufMethod = typeof(GL).GetMethod("IsBuffer", new[] { typeof(int) });
        private static MethodInfo GLBufLabel = typeof(GLBuffer).GetMethod("GetLabel", new[] { typeof(int) });

        #region UTIL METHODS
        /// <summary>
        /// Create OpenGL buffer from data array.
        /// </summary>
        /// <param name="data"></param>
        private void CreateBuffer(byte[] data)
        {
            // CREATE OPENGL OBJECT
            glname = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, glname);

            // ALLOCATE (AND WRITE) GPU MEMORY
            if (Size > 0)
            {
                if (data != null && data.Length > 0)
                {
                    Size = data.Length;
                    var dataPtr = Marshal.AllocHGlobal(Size);
                    Marshal.Copy(data, 0, dataPtr, Size);
                    GL.NamedBufferData(glname, Size, dataPtr, Usage);
                    Marshal.FreeHGlobal(dataPtr);
                }
                else
                    GL.NamedBufferData(glname, Size, IntPtr.Zero, Usage);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// Get xml text from scene structure by processing the specified command.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private static byte[] LoadXml(Compiler.Command cmd, Dict scene, CompileException err)
        {
            // Get text from file or text object
            string str = GetText(scene, cmd);
            if (str == null)
            {
                err.Add("Could not process command. Second argument must "
                    + "be a name to a text object or a filename.", cmd);
                return null;
            }

            try
            {
                // Parse XML string
                var document = new XmlDocument();
                document.LoadXml(str);

                // Load data from XML
                var filedata = new byte[cmd.ArgCount - 1][];
                for (int i = 1; i < cmd.ArgCount; i++)
                {
                    try
                    {
                        filedata[i - 1] = DataXml.Load(document, cmd[i].Text);
                    }
                    catch (XmlException ex)
                    {
                        err.Add(ex.Message, cmd);
                    }
                }

                // Merge data
                if (!err.HasErrors())
                    return filedata.Cat().ToArray();
            }
            catch (Exception ex)
            {
                err.Add(ex.GetBaseException().Message, cmd);
            }

            return null;
        }
        
        /// <summary>
        /// Get text from scene structure by processing the specified command.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private static byte[] LoadText(Compiler.Command cmd, Dict scene, CompileException err)
        {
            // Get text from file or text object
            var str = GetText(scene, cmd);
            if (str == null)
            {
                err.Add("Could not process command. Second argument must "
                    + "be a name to a text object or a filename.", cmd);
                return null;
            }

            // Convert text to byte array
            return str.ToCharArray().ToBytes();
        }
        
        /// <summary>
        /// Get text from scene objects.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static string GetText(Dict scene, Compiler.Command cmd)
        {
            GLText text = null;
            string dir = Path.GetDirectoryName(cmd.File) + Path.DirectorySeparatorChar;
            if (scene.TryGetValue(cmd[0].Text, ref text))
                return text.text.Trim();
            else if (File.Exists(cmd[0].Text))
                return File.ReadAllText(cmd[0].Text);
            else if (File.Exists(dir + cmd[0].Text))
                return File.ReadAllText(dir + cmd[0].Text);
            return null;
        }
        #endregion
    }
}
