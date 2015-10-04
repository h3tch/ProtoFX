using System;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using System.Runtime.InteropServices;

namespace App
{
    class GLBuffer : GLObject
    {
        #region FIELDS
        public int size = 0;
        public BufferUsageHint usage = BufferUsageHint.StaticDraw;
        public string[] file = null;
        #endregion

        public GLBuffer(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            ErrorCollector err = new ErrorCollector();
            err.PushStack("buffer '" + name + "'");

            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(this, ref cmds);

            // CREATE OPENGL OBJECT
            glname = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, glname);

            // LOAD BUFFER DATA
            var data = loadBufferFiles(err, dir, file, size);

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
        private static byte[] loadBufferFiles(ErrorCollector err, string dir, string[] filenames, int size)
        {
            if (filenames == null || filenames.Length == 0)
                return null;

            // load data from all files
            byte[][] filedata = new byte[filenames.Length][];
            for (int i = 0; i < filenames.Length; i++)
            {
                // get path and node
                var filename = filenames[i].Split(new char[] { '|' });
                var path = Path.IsPathRooted(filename[0]) ? filename[0] : dir + filename[0];
                try
                {
                    if (filename.Length == 1)
                        filedata[i] = File.ReadAllBytes(path);
                    else if (filename.Length == 2)
                        filedata[i] = DataXml.Load(path, filename[1]);
                    else
                        err.Add("Do not know how to load file '" + filenames[i] + "'.");
                }
                catch(Exception ex)
                {
                    err.Add(ex.GetBaseException().Message);
                }
            }

            if (err.HasErrors())
                err.ThrowExeption();

            // if size has not been specified,
            // compute the summed size of all file data
            if (size == 0)
            {
                size = 0;
                foreach (byte[] b in filedata)
                    size += b.Length;
            }

            // copy file data to byte array
            byte[] data = new byte[size];
            for (int i = 0, start = 0; i < filedata.Length && start < data.Length; start += filedata[i++].Length)
                Array.Copy(filedata[i], 0, data, start, Math.Min(data.Length - start, filedata[i].Length));

            return data;
        }
        #endregion
    }
}
