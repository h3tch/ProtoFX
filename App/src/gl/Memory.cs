using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;

namespace protofx.gl
{
    class Memory : Object
    {
        #region FIELDS
        
        [FxField] public int Size { get; protected set; } = 0;
        public byte[] Data { get; protected set; } = null;
        private IntPtr Pointer = (IntPtr)0;
        public IntPtr DataIntPtr { get
            {
                if (Pointer == (IntPtr)0 && Data != null)
                {
                    Pointer = Marshal.AllocHGlobal(Data.Length);
                    Marshal.Copy(Data, 0, Pointer, Data.Length);
                }
                return Pointer;
            }
        }

        #endregion

        /// <summary>
        /// Generic constructor used to build the scene objects.
        /// </summary>
        /// <param name="params">A class containing all the parameters
        /// needed to instantiate the class. The GLMemory class requires a
        /// <code>Compiler.Block</code> object of the respective part in the code
        /// and a <code>Dictionary&lt;string, object&gt;</code> object containing
        /// the scene objects.</param>
        public Memory(object @params)
            : this(@params.GetFieldValue<Compiler.Block>(),
                   @params.GetFieldValue<Dictionary<string, object>>())
        {
        }

        /// <summary>
        /// Construct GLMemory object.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <param name="anno">Annotation of the object.</param>
        /// <param name="size">The memory size to be allocated in bytes.</param>
        /// <param name="data">Optionally initialize the buffer object with the specified data.</param>
        public Memory(string name, string anno, int size = 0, byte[] data = null)
            : base(name, anno)
        {
            Size = size;
            Data = data;
        }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        private Memory(Compiler.Block block, Dictionary<string, object> scene)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"memory '{block.Name}'");

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
            Data = iter.Take(Size == 0 ? iter.Count() : Size).ToArray();
            if (Size == 0)
                Size = Data.Length;

            // CONVERT DATA
            var clazz = block["class"].FirstOrDefault();
            if (clazz != null && Size > 0)
            {
                var converter = Csharp.GetMethod(clazz, scene, err);
                Data = (byte[])converter?.Invoke(null, new[] { Data });
            }

            if (HasErrorOrGlError(err, "", -1))
                throw err;
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            if (Pointer != (IntPtr)0)
            {
                Marshal.FreeHGlobal(Pointer);
                Pointer = (IntPtr)0;
            }
            base.Delete();
        }

        #region UTIL METHODS

        /// <summary>
        /// Get xml text from scene structure by processing the specified command.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        protected static byte[] LoadXml(Compiler.Command cmd, Dictionary<string, object> scene, CompileException err)
        {
            // Get text from file or text object
            string str = GetText(scene, cmd);
            if (str == null)
            {
                err.Error("Could not process command. Second argument must "
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
                        err.Error(ex.Message, cmd);
                    }
                }

                // Merge data
                if (!err.HasErrors)
                    return filedata.Cat().ToArray();
            }
            catch (Exception ex)
            {
                err.Error(ex.GetBaseException().Message, cmd);
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
        protected static byte[] LoadText(Compiler.Command cmd, Dictionary<string, object> scene, CompileException err)
        {
            // Get text from file or text object
            var str = GetText(scene, cmd);
            if (str == null)
            {
                err.Error("Could not process command. Second argument must "
                    + "be a name to a text object or a filename.", cmd);
                return null;
            }

            // Convert text to byte array
            return str.To<byte>();
        }

        /// <summary>
        /// Get text from scene objects.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected static string GetText(Dictionary<string, object> scene, Compiler.Command cmd)
        {
            string dir = Path.GetDirectoryName(cmd.File) + Path.DirectorySeparatorChar;
            if (scene.TryGetValue(cmd[0].Text, out object obj) && obj is Text)
                return ((Text)obj).Body.Trim();
            else if (File.Exists(cmd[0].Text))
                return File.ReadAllText(cmd[0].Text);
            else if (File.Exists(dir + cmd[0].Text))
                return File.ReadAllText(dir + cmd[0].Text);
            return null;
        }
        #endregion
    }
}
