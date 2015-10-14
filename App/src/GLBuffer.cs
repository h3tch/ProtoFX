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
        public int size = 0;
        public BufferUsageHint usage = BufferUsageHint.StaticDraw;
        #endregion

        public GLBuffer(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            ErrorCollector err = new ErrorCollector();
            err.PushCall("buffer '" + name + "'");

            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(this, ref cmds);

            // PARSE COMMANDS
            List<byte[]> datalist = new List<byte[]>();
            for (int i = 0; i < cmds.Length; i++)
            {
                var cmd = cmds[i];

                // skip if already processed commands
                if (cmd == null)
                    continue;

                err.PushCall("command " + i + " '" + cmd[0] + "'");

                switch (cmd[0])
                {
                    case "txt":
                        datalist.Add(loadText(err, dir, cmd, classes));
                        break;
                    case "xml":
                        datalist.Add(LoadXml(err, dir, cmd, classes));
                        break;
                }
                
                err.PopCall();
            }

            if (err.HasErrors())
                err.ThrowExeption();

            // merge data into a single array
            byte[] data = Data.Join(datalist.ToArray(), size);
            size = data.Length;

            // CREATE OPENGL OBJECT
            glname = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, glname);
            
            // ALLOCATE (AND WRITE) GPU MEMORY
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

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            if (GL.GetError() != ErrorCode.NoError)
                err.Add("OpenGL error '" + GL.GetError() + "' occurred during buffer allocation.");
            if (err.HasErrors())
                err.ThrowExeption();
        }

        public byte[] Read()
        {
            // allocate buffer memory
            byte[] data = new byte[size];

            // map buffer and copy data to CPU memory
            GL.BindBuffer(BufferTarget.ArrayBuffer, glname);
            IntPtr dataPtr = GL.MapBuffer(BufferTarget.ArrayBuffer, BufferAccess.ReadOnly);
            Marshal.Copy(dataPtr, data, 0, size);
            GL.UnmapBuffer(BufferTarget.ArrayBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            
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
        private static byte[] LoadXml(ErrorCollector err, string dir, string[] cmd, Dict classes)
        {
            // Check for a valid command
            if (cmd.Length < 2)
            {
                err.Add("Do not know how to process this command.");
                return null;
            }

            // Get text from file or text object
            string str = getText(dir, cmd[1], classes);
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
                byte[][] filedata = new byte[cmd.Length - 2][];
                for (int i = 2; i < cmd.Length; i++)
                {
                    try
                    {
                        filedata[i - 2] = DataXml.Load(document, cmd[i]);
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

        private static byte[] loadText(ErrorCollector err, string dir, string[] cmd, Dict classes)
        {
            // Check for a valid command
            if (cmd.Length < 2)
            {
                err.Add("Do not know how to process this command.");
                return null;
            }

            // Get text from file or text object
            string str = getText(dir, cmd[1], classes);
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

        private static string getText(string dir, string name, Dict classes)
        {
            GLObject text;
            string str = null;
            if (classes.TryGetValue(name, out text) && text.GetType() == typeof(GLText))
                str = ((GLText)text).text.Trim();
            else if (File.Exists(name))
                str = File.ReadAllText(name);
            else if (File.Exists(dir + name))
                str = File.ReadAllText(dir + name);
            return str;
        }
        #endregion
    }
}
