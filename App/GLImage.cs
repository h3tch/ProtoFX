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
        public PixelInternalFormat format = PixelInternalFormat.Rgba8ui;
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
            GL.TexParameter(type, TextureParameterName.TextureMinFilter,
                (int)(mipmaps > 0 ? TextureMinFilter.NearestMipmapNearest : TextureMinFilter.Nearest));
            GL.TexParameter(type, TextureParameterName.TextureMagFilter,
                (int)(mipmaps > 0 ? TextureMinFilter.NearestMipmapNearest : TextureMinFilter.Nearest));
            GL.TexParameter(type, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(type, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(type, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            // LOAD IMAGE DATA
            OpenTK.Graphics.OpenGL4.PixelFormat dataformat;
            PixelType datatype;
            var data = loadImageFiles(file, width, height, depth, format, out dataformat, out datatype);
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
                    GL.TexImage1D(type, 0, format, width, 0, dataformat, datatype, dataPtr);
                    break;
                case TextureTarget.Texture1DArray:
                    GL.TexImage2D(type, 0, format, width, length, 0, dataformat, datatype, dataPtr);
                    break;
                case TextureTarget.Texture2D:
                    GL.TexImage2D(type, 0, format, width, height, 0, dataformat, datatype, dataPtr);
                    break;
                case TextureTarget.Texture2DArray:
                    GL.TexImage3D(type, 0, format, width, height, length, 0, dataformat, datatype, dataPtr);
                    break;
                case TextureTarget.Texture3D:
                    GL.TexImage3D(type, 0, format, width, height, depth, 0, dataformat, datatype, dataPtr);
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

        private static byte[] loadImageFiles(string[] filenames, int w, int h, int d, PixelInternalFormat format, 
            out OpenTK.Graphics.OpenGL4.PixelFormat dataformat, out PixelType datatype)
        {
            if (format.ToString().StartsWith("DepthComponent"))
            {
                dataformat = OpenTK.Graphics.OpenGL4.PixelFormat.DepthComponent;
                datatype = PixelType.Float;
            }
            else
            {
                dataformat = OpenTK.Graphics.OpenGL4.PixelFormat.Bgra;
                datatype = PixelType.UnsignedByte;
            }

            System.Drawing.Imaging.PixelFormat fileformat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            int pxBytes = Image.GetPixelFormatSize(fileformat) / 8;
            byte[] data = null;

            if (filenames != null && filenames.Length > 0)
            {
                data = new byte[pxBytes * w * h * (d > 0 ? d : filenames.Length)];

                for (int i = 0; i < filenames.Length; i++)
                {
                    var bmp = new Bitmap(filenames[i]);
                    var bmpData = bmp.LockBits(
                        new Rectangle(0, 0, Math.Min(bmp.Width, w), Math.Min(bmp.Height, h)),
                        ImageLockMode.ReadOnly, fileformat);
                
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
