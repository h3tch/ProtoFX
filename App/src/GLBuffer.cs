using System;
using System.Collections.Generic;
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
            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(this, ref cmds);

            // CREATE OPENGL OBJECT
            glname = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, glname);

            // LOAD BUFFER DATA
            var data = loadBufferFiles(dir, file, size);
            var dataPtr = IntPtr.Zero;
            if (data != null)
            {
                size = data.Length;
                dataPtr = Marshal.AllocHGlobal(size);
                Marshal.Copy(data, 0, dataPtr, size);
            }

            // ALLOCATE (AND WRITE) GPU MEMORY
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)size, dataPtr, usage);
            //GL.NamedBufferData(glname, size, dataPtr, usage);
            
            // FREE BUFFER DATA
            if (dataPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(dataPtr);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            throwExceptionOnOpenGlError("buffer", name, "allocate buffer");
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

        private static byte[] loadBufferFiles(string dir, string[] filenames, int size)
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
                if (filename.Length == 1)
                    filedata[i] = File.ReadAllBytes(path);
                else if (filename.Length == 2)
                    filedata[i] = new DataXml(path, filename[1]).data;
                else
                    throw new Exception("");
            }

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
