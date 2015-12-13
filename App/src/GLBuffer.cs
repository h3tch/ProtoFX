﻿using System;
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
        [Field] public int size { get; private set; } = 0;
        [Field] public BufferUsageHint usage { get; private set; } = BufferUsageHint.StaticDraw;
        #endregion

        /// <summary>
        /// Link GLBuffer to existing OpenGL buffer. Used
        /// to provide debug information in the debug view.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        /// <param name="glname">OpenGL buffer object to like to.</param>
        public GLBuffer(GLParams @params, int glname) : base(@params)
        {
            int s, u;
            this.glname = glname;
            GL.GetNamedBufferParameter(glname, BufferParameterName.BufferSize, out s);
            GL.GetNamedBufferParameter(glname, BufferParameterName.BufferUsage, out u);
            size = s;
            usage = (BufferUsageHint)u;
        }

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLBuffer(GLParams @params) : base(@params)
        {
            var err = new CompileException($"buffer '{@params.name}'");

            // PARSE TEXT TO COMMANDS
            var cmds = new Commands(@params.text, err);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            cmds.Cmds2Fields(this, err);
            
            // PARSE COMMANDS
            List<byte[]> datalist = new List<byte[]>();

            foreach (var cmd in cmds["txt"])
                datalist.Add(loadText(err + $"command {cmd.cmd} 'txt'", @params.dir, cmd.args, @params.scene));

            foreach (var cmd in cmds["xml"])
                datalist.Add(LoadXml(err + $"command {cmd.cmd} 'xml'", @params.dir, cmd.args, @params.scene));

            if (err.HasErrors())
                throw err;

            // merge data into a single array
            var iter = datalist.Join();
            var data = iter.Take(size == 0 ? iter.Count() : size).ToArray();
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

        /// <summary>
        /// Read whole GPU buffer data.
        /// </summary>
        /// <returns>Return GPU buffer data as byte array.</returns>
        public byte[] Read()
        {
            if (size == 0)
                return "Buffer is empty".ToCharArray().ToBytes();

            int flags;
            GL.GetNamedBufferParameter(glname, BufferParameterName.BufferStorageFlags, out flags);
            if ((flags & (int)BufferStorageFlags.MapReadBit) == 0)
                return "Buffer cannot be read".ToCharArray().ToBytes();

            // allocate buffer memory
            byte[] data = new byte[size];
            
            // map buffer and copy data to CPU memory
            IntPtr dataPtr = GL.MapNamedBuffer(glname, BufferAccess.ReadOnly);
            Marshal.Copy(dataPtr, data, 0, size);
            GL.UnmapNamedBuffer(glname);
            
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
        private static byte[] LoadXml(CompileException err, string dir, string[] cmd, Dict<GLObject> classes)
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
                    catch (CompileException ex)
                    {
                        err.Add(ex.GetBaseException().Message);
                    }
                }

                // Merge data
                if (!err.HasErrors())
                    return filedata.Join().ToArray();
            }
            catch (Exception ex)
            {
                err.Add(ex.GetBaseException().Message);
            }

            return null;
        }

        private static byte[] loadText(CompileException err, string dir, string[] cmd, Dict<GLObject> classes)
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
            return str.ToCharArray().ToBytes();
        }

        private static string getText(string dir, string name, Dict<GLObject> classes)
        {
            GLText text;
            if (classes.TryGetValue(name, out text))
                return text.text.Trim();
            else if (File.Exists(name))
                return File.ReadAllText(name);
            else if (File.Exists(dir + name))
                return File.ReadAllText(dir + name);
            return null;
        }
        #endregion
    }
}
