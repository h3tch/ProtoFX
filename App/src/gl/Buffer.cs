using System;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UsageHint = OpenTK.Graphics.OpenGL4.BufferUsageHint;
using ParameterName = OpenTK.Graphics.OpenGL4.BufferParameterName;

namespace protofx.gl
{
    class Buffer : Memory
    {
        #region FIELDS

        [FxField] public BufferTarget Target { get; private set; } = BufferTarget.ArrayBuffer;
        [FxField] public UsageHint Usage { get; private set; } = UsageHint.StaticDraw;

        #endregion

        /// <summary>
        /// Generic constructor used to build the scene objects.
        /// </summary>
        /// <param name="params">A class containing all the parameters
        /// needed to instantiate the class. The GLBuffer class requires a
        /// <code>Compiler.Block</code> object of the respective part in the code
        /// and a <code>Dictionary&lt;string, object&gt;</code> object containing
        /// the scene objects.</param>
        public Buffer(object @params)
            : this(@params.GetFieldValue<Compiler.Block>(),
                   @params.GetFieldValue<Dictionary<string, object>>())
        {
        }

        /// <summary>
        /// Construct GLBuffer object.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <param name="anno">Annotation of the object.</param>
        /// <param name="usage">How the buffer should be used by the program.</param>
        /// <param name="size">The memory size to be allocated in bytes.</param>
        /// <param name="data">Optionally initialize the buffer object with the specified data.</param>
        public Buffer(string name, string anno, BufferTarget target, UsageHint usage, int size, byte[] data = null)
            : base(name, anno, size, data)
        {
            var err = new CompileException($"buffer '{name}'");
            
            Usage = usage;
            Target = target;

            // CREATE OPENGL OBJECT
            CreateBuffer(Data);
            Data = null;
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
        public Buffer(string name, string anno, int glname) : base(name, anno)
        {
            this.glname = glname;
            GL.GetNamedBufferParameter(glname, ParameterName.BufferSize, out int s);
            GL.GetNamedBufferParameter(glname, ParameterName.BufferUsage, out int u);
            Size = s;
            Usage = (UsageHint)u;
        }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        private Buffer(Compiler.Block block, Dictionary<string, object> scene)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"buffer '{block.Name}'");

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(block, err);

            // PARSE COMMANDS
            var datalist = new List<byte[]>();

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
                var converter = Csharp.GetMethod(clazz, scene, err);
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
                //var rs = "Buffer is empty".ToCharArray().ToBytes();
                var rs = "Buffer is empty".To<byte>();
                if (data == null) data = rs; else rs.CopyTo(data, 0);
                return;
            }

            // check read flag of the buffer
            GL.GetNamedBufferParameter(glname, ParameterName.BufferStorageFlags, out int flags);
            if ((flags & (int)BufferStorageFlags.MapReadBit) == 0)
            {
                var rs = "Buffer cannot be read".To<byte>();
                if (data == null) data = rs; else rs.CopyTo(data, 0);
                return;
            }

            // if necessary allocate bytes
            if (data == null)
                data = new byte[Size];

            // map buffer and copy data to CPU memory
            var mapSize = Math.Min(Size, data.Length);
            var dataPtr = GL.MapNamedBufferRange(glname, (IntPtr)offset, mapSize,
                                                 BufferAccessMask.MapReadBit);
            Marshal.Copy(dataPtr, data, 0, mapSize);
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
            base.Delete();
        }

        /// <summary>
        /// Get the OpenGL object label of the buffer object.
        /// </summary>
        /// <param name="glname"></param>
        /// <returns></returns>
        public static string GetLabel(int glname)
        {
            return GetLabel(ObjectLabelIdentifier.Buffer, glname);
        }

        /// <summary>
        /// Find OpenGL buffers.
        /// </summary>
        /// <param name="existing">List of objects already existent in the scene.</param>
        /// <param name="range">Range in which to search for external objects (starting with 0).</param>
        /// <returns></returns>
        public static IEnumerable<Object> FindBuffers(Object[] existing, int range = 64)
        {
            return FindObjects(existing, new[] { typeof(Buffer) }, GLIsBufMethod, GLBufLabel, range);
        }

        private static MethodInfo GLIsBufMethod = typeof(GL).GetMethod("IsBuffer", new[] { typeof(int) });
        private static MethodInfo GLBufLabel = typeof(Buffer).GetMethod("GetLabel", new[] { typeof(int) });

        #region UTIL METHODS
        /// <summary>
        /// Create OpenGL buffer from data array.
        /// </summary>
        /// <param name="data"></param>
        protected void CreateBuffer(byte[] data)
        {
            // CREATE OPENGL OBJECT
            glname = GL.GenBuffer();
            GL.BindBuffer(Target, glname);

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

            GL.BindBuffer(Target, 0);
        }
        #endregion
    }
}
