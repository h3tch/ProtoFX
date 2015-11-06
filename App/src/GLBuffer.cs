using System;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace App
{
    class GLBuffer : GLObject
    {
        #region FIELDS
        [GLField]
        public int size { get; private set; } = 0;
        [GLField]
        public BufferUsageHint usage { get; private set; } = BufferUsageHint.StaticDraw;
        #endregion

        public GLBuffer(string dir, string name, string annotation, string text, Dict<GLObject> classes)
            : base(name, annotation)
        {
            var err = new GLException($"buffer '{name}'");

            // PARSE TEXT TO COMMANDS
            var cmds = new Commands(text, err);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            cmds.Cmds2Fields(this, err);
            
            // PARSE COMMANDS
            List<byte[]> datalist = new List<byte[]>();

            foreach (var cmd in cmds["txt"])
                datalist.Add(loadText(err + $"command {cmd.cmd} 'txt'", dir, cmd.args, classes));

            foreach (var cmd in cmds["xml"])
                datalist.Add(LoadXml(err + $"command {cmd.cmd} 'xml'", dir, cmd.args, classes));

            if (err.HasErrors())
                throw err;

            // merge data into a single array
            byte[] data = Data.Join(datalist.ToArray(), size);
            size = data.Length;

            // CREATE OPENGL OBJECT
            glname = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, glname);
            
            // ALLOCATE (AND WRITE) GPU MEMORY
            if (size > 0)
            {
                if (data != null)
                {
                    size = data.Length;
                    var dataPtr = Marshal.AllocHGlobal(size);
                    Marshal.Copy(data, 0, dataPtr, size);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)size, dataPtr, usage);
                    Marshal.FreeHGlobal(dataPtr);
                }
                else
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)size, IntPtr.Zero, usage);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            if (HasErrorOrGlError(err))
                throw err;
        }

        public byte[] Read()
        {
            // allocate buffer memory
            byte[] data = new byte[size];

            if (size > 0)
            {
                // map buffer and copy data to CPU memory
                GL.BindBuffer(BufferTarget.ArrayBuffer, glname);
                IntPtr dataPtr = GL.MapBuffer(BufferTarget.ArrayBuffer, BufferAccess.ReadOnly);
                Marshal.Copy(dataPtr, data, 0, size);
                GL.UnmapBuffer(BufferTarget.ArrayBuffer);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }
            
            return data;
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteBuffer(glname);
                glname = 0;
            }
        }

        #region UTIL METHODS
        private static byte[] LoadXml(GLException err, string dir, string[] cmd, Dict<GLObject> classes)
        {
            // Get text from file or text object
            string str = getText(dir, cmd[0], classes);
            if (str == null)
            {
                err.Add("Could not process command. Second argument "
                    + "must be a name to a text object or a filename.");
                return null;
            }

            try
            {
                // Parse XML string
                var document = new XmlDocument();
                document.LoadXml(str);

                // Load data from XML
                byte[][] filedata = new byte[cmd.Length - 1][];
                for (int i = 1; i < cmd.Length; i++)
                {
                    try
                    {
                        filedata[i - 1] = DataXml.Load(document, cmd[i]);
                    }
                    catch (GLException ex)
                    {
                        err.Add(ex.GetBaseException().Message);
                    }
                }

                // Merge data
                if (!err.HasErrors())
                    return Data.Join(filedata);
            }
            catch (Exception ex)
            {
                err.Add(ex.GetBaseException().Message);
            }

            return null;
        }

        private static byte[] loadText(GLException err, string dir, string[] cmd, Dict<GLObject> classes)
        {
            // Get text from file or text object
            string str = getText(dir, cmd[0], classes);
            if (str == null)
            {
                err.Add("Could not process command. Second argument "
                    + "must be a name to a text object or a filename.");
                return null;
            }

            // Convert text to byte array
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string getText(string dir, string name, Dict<GLObject> classes)
        {
            GLText text;
            string str = null;
            if (classes.TryFindClass(name, out text))
                str = text.text.Trim();
            else if (File.Exists(name))
                str = File.ReadAllText(name);
            else if (File.Exists(dir + name))
                str = File.ReadAllText(dir + name);
            return str;
        }
        #endregion
    }
}
