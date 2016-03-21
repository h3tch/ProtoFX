using System;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;

namespace App
{
    class GLBuffer : GLObject
    {
        #region FIELDS
        [FxField] public int Size { get; private set; } = 0;
        [FxField] public BufferUsageHint Usage { get; private set; } = BufferUsageHint.StaticDraw;
        #endregion

        /// <summary>
        /// Construct GLBuffer object.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <param name="anno">Annotation of the object.</param>
        /// <param name="usage">How the buffer should be used by the program.</param>
        /// <param name="size">The memory size to be allocated in bytes.</param>
        /// <param name="data">Optionally initialize the buffer object with the specified data.</param>
        public GLBuffer(string name, string anno, BufferUsageHint usage, int size, byte[] data = null)
            : base(name, anno)
        {
            var err = new CompileException($"buffer '{name}'");

            this.Size = size;
            this.Usage = usage;

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
            GL.GetNamedBufferParameter(glname, BufferParameterName.BufferSize, out s);
            GL.GetNamedBufferParameter(glname, BufferParameterName.BufferUsage, out u);
            Size = s;
            Usage = (BufferUsageHint)u;
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
                datalist.Add(loadText(err | $"command {cmd.Name} 'txt'", cmd, scene));

            foreach (var cmd in block["xml"])
                datalist.Add(LoadXml(err | $"command {cmd.Name} 'xml'", cmd, scene));

            // merge data into a single array
            var iter = datalist.Cat();
            var data = iter.Take(Size == 0 ? iter.Count() : Size).ToArray();
            if (Size == 0)
                Size = data.Length;

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
        /// Read whole GPU buffer data.
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
            GL.GetNamedBufferParameter(glname, BufferParameterName.BufferStorageFlags, out flags);
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
            IntPtr dataPtr = GL.MapNamedBuffer(glname, BufferAccess.ReadOnly);
            Marshal.Copy(dataPtr, data, offset, Math.Min(Size, data.Length));
            GL.UnmapNamedBuffer(glname);
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
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

        #region UTIL METHODS
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
        
        private static byte[] LoadXml(CompileException err, Compiler.Command cmd, Dict scene)
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
                byte[][] filedata = new byte[cmd.ArgCount - 1][];
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
        
        private static byte[] loadText(CompileException err, Compiler.Command cmd, Dict scene)
        {
            // Get text from file or text object
            string str = GetText(scene, cmd);
            if (str == null)
            {
                err.Add("Could not process command. Second argument must "
                    + "be a name to a text object or a filename.", cmd);
                return null;
            }

            // Convert text to byte array
            return str.ToCharArray().ToBytes();
        }
        
        private static string GetText(Dict scene, Compiler.Command cmd)
        {
            GLText text;
            string dir = Path.GetDirectoryName(cmd.File) + Path.DirectorySeparatorChar;
            if (scene.TryGetValue(cmd[0].Text, out text, cmd))
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
