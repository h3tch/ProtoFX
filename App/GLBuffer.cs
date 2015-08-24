using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using System.Runtime.InteropServices;

namespace gled
{
    class GLBuffer : GLObject
    {
        public int size = 0;
        public All type = All.UnsignedInt;
        public BufferUsageHint usage = BufferUsageHint.StaticDraw;
        public string[] file = null;

        public GLBuffer(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, args);

            // CREATE OPENGL OBJECT
            glname = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, glname); //GL.NamedBufferData(glname, size, System.IntPtr.Zero, usage);

            // LOAD BUFFER DATA
            var data = loadBufferFiles(file, size);
            var dataPtr = IntPtr.Zero;
            if (data != null)
            {
                dataPtr = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, dataPtr, data.Length);
            }

            // ALLOCATE (AND WRITE) GPU MEMORY
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), dataPtr, usage);
            
            // FREE BUFFER DATA
            if (dataPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(dataPtr);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            throwExceptionOnOpenGlError("buffer", name, "allocate buffer");
        }

        private static byte[] loadBufferFiles(string[] filenames, int size)
        {
            if (filenames == null || filenames.Length == 0)
                return null;

            // load data from all files
            byte[][] filedata = new byte[filenames.Length][];
            for (int i = 0; i < filenames.Length; i++)
                filedata[i] = File.ReadAllBytes(filenames[i]);

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

        public override void Bind(int unit)
        {

        }

        public override void Unbind(int unit)
        {

        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteBuffer(glname);
                glname = 0;
            }
        }
    }
}
