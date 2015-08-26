using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace gled
{
    class GLImage : GLObject
    {
        public int width = 1;
        public int height = 0;
        public int depth = 0;
        public int length = 0;
        public int mipmaps = 0;
        public TextureTarget type = 0;
        public PixelInternalFormat format = PixelInternalFormat.Rgba8;
        public string[] file = null;

        public GLImage(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, ref args);

            // if type was not specified
            if (type == 0)
            {
                if (width > 0 && height == 1 && depth == 0 && length == 0)
                    type = TextureTarget.Texture1D;
                else if (width > 0 && height == 1 && depth == 0 && length > 0)
                    type = TextureTarget.Texture1DArray;
                else if (width > 0 && height > 1 && depth == 0 && length == 0)
                    type = TextureTarget.Texture2D;
                else if (width > 0 && height > 1 && depth == 0 && length > 0)
                    type = TextureTarget.Texture2DArray;
                else if (width > 0 && height > 1 && depth > 0 && length == 0)
                    type = TextureTarget.Texture3D;
                else
                    throw new Exception("ERROR in image " + name + ": " 
                        + "Texture type could not be derived from 'width', 'height', 'depth' and 'length'. "
                        + "Please check these parameters or specify the type directly (e.g. 'type = texture2D').");
            }

            // CREATE OPENGL OBJECT
            glname = GL.GenTexture();
            GL.BindTexture(type, glname);

            // LOAD IMAGE DATA
            var data = loadImageFiles(file, width, height, depth, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var dataPtr = IntPtr.Zero;
            if (data != null)
            {
                dataPtr = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, dataPtr, data.Length);
            }

            // ALLOCATE GPU MEMORY
            switch (type)
            {
                case TextureTarget.Texture1D:
                    GL.TexImage1D(type, 0, format, width, 0,
                        OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                        PixelType.UnsignedByte, dataPtr);
                    break;
                case TextureTarget.Texture1DArray:
                case TextureTarget.Texture2D:
                    GL.TexImage2D(type, 0, format, width, height, 0,
                        OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                        PixelType.UnsignedByte, dataPtr);
                    break;
                case TextureTarget.Texture2DArray:
                case TextureTarget.Texture3D:
                    GL.TexImage3D(type, 0, format, width, height, depth, 0,
                        OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                        PixelType.UnsignedByte, dataPtr);
                    break;
            }

            // FREE IMAGE DATA
            if (dataPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(dataPtr);

            // GENERATE MIPMAPS
            if (mipmaps > 0)
                GL.GenerateMipmap((GenerateMipmapTarget)type);

            GL.BindTexture(type, 0);
            throwExceptionOnOpenGlError("image", name, "allocate (and write) texture");
        }

        private static byte[] loadImageFiles(string[] filenames, int w, int h, int d, System.Drawing.Imaging.PixelFormat format)
        {
            int pxBytes = Image.GetPixelFormatSize(format) / 8;
            byte[] data = null;

            if (filenames != null && filenames.Length > 0)
            {
                data = new byte[pxBytes * w * h * (d > 0 ? d : filenames.Length)];

                for (int i = 0; i < filenames.Length; i++)
                {
                    var bmp = new Bitmap(filenames[i]);
                    var bmpData = bmp.LockBits(
                        new Rectangle(0, 0, Math.Min(bmp.Width, w), Math.Min(bmp.Height, h)),
                        ImageLockMode.ReadOnly, format);
                
                    Marshal.Copy(bmpData.Scan0, data, pxBytes * w * h * i, bmpData.Stride * bmpData.Height);

                    bmp.UnlockBits(bmpData);
                }
            }

            return data;
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTexture(glname);
                glname = 0;
            }
        }
    }
}
