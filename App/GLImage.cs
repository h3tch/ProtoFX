using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using SysImg = System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace gled
{
    class GLImage : GLObject
    {
        #region FIELDS

        public string[] file = null;
        public int width = 1;
        public int height = 0;
        public int depth = 0;
        public int length = 0;
        public int mipmaps = 0;
        public TextureTarget type = 0;
        public PixelInternalFormat format = PixelInternalFormat.Rgba;
        private SysImg.PixelFormat fileformat = SysImg.PixelFormat.Format32bppArgb;
        private PixelType pixeltype = 0;
        private int pixelsize = 0;
        private PixelFormat pixelformat = 0;

        #endregion

        #region PROPERTIES

        public TextureTarget target { get { return type; } set { type = value; } }
        public PixelInternalFormat gpuformat { get { return format; } set { format = value; } }

        #endregion

        public GLImage(string name, string annotation, string text, GLDict classes)
            : base(name, annotation)
        {
            // PARSE TEXT TO COMMANDS
            var cmds = Text2Cmds(text);

            // PARSE COMMANDS AND CONVERT THEM TO CLASS FIELDS
            Cmds2Fields(this, ref cmds);

            // if type was not specified
            if (target == 0)
            {
                if (width > 0 && height == 1 && depth == 0 && length == 0)
                    target = TextureTarget.Texture1D;
                else if (width > 0 && height == 1 && depth == 0 && length > 0)
                    target = TextureTarget.Texture1DArray;
                else if (width > 0 && height > 1 && depth == 0 && length == 0)
                    target = TextureTarget.Texture2D;
                else if (width > 0 && height > 1 && depth == 0 && length > 0)
                    target = TextureTarget.Texture2DArray;
                else if (width > 0 && height > 1 && depth > 0 && length == 0)
                    target = TextureTarget.Texture3D;
                else
                    throw new Exception("ERROR in image " + name + ": " 
                        + "Texture type could not be derived from 'width', 'height', 'depth' and 'length'. "
                        + "Please check these parameters or specify the type directly (e.g. 'type = texture2D').");
            }

            // CREATE OPENGL OBJECT
            glname = GL.GenTexture();
            GL.BindTexture(target, glname);
            GL.TexParameter(target, TextureParameterName.TextureMinFilter,
                (int)(mipmaps > 0 ? TextureMinFilter.NearestMipmapNearest : TextureMinFilter.Nearest));
            GL.TexParameter(target, TextureParameterName.TextureMagFilter,
                (int)(mipmaps > 0 ? TextureMinFilter.NearestMipmapNearest : TextureMinFilter.Nearest));
            GL.TexParameter(target, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(target, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(target, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            // LOAD IMAGE DATA
            var data = loadImageFiles(file, width, height, depth, gpuformat,
                out pixelformat, out pixeltype, out pixelsize, out fileformat);
            var dataPtr = IntPtr.Zero;
            if (data != null)
            {
                dataPtr = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, dataPtr, data.Length);
            }

            // ALLOCATE GPU MEMORY
            switch (target)
            {
                case TextureTarget.Texture1D:
                    GL.TexImage1D(target, 0, gpuformat, width, 0, pixelformat, pixeltype, dataPtr);
                    break;
                case TextureTarget.Texture1DArray:
                    GL.TexImage2D(target, 0, gpuformat, width, length, 0, pixelformat, pixeltype, dataPtr);
                    break;
                case TextureTarget.Texture2D:
                    GL.TexImage2D(target, 0, gpuformat, width, height, 0, pixelformat, pixeltype, dataPtr);
                    break;
                case TextureTarget.Texture2DArray:
                    GL.TexImage3D(target, 0, gpuformat, width, height, length, 0, pixelformat, pixeltype, dataPtr);
                    break;
                case TextureTarget.Texture3D:
                    GL.TexImage3D(target, 0, gpuformat, width, height, depth, 0, pixelformat, pixeltype, dataPtr);
                    break;
            }

            // FREE IMAGE DATA
            if (dataPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(dataPtr);

            // GENERATE MIPMAPS
            if (mipmaps > 0)
                GL.GenerateMipmap((GenerateMipmapTarget)target);

            GL.BindTexture(target, 0);
            throwExceptionOnOpenGlError("image", name, "allocate (and write) texture");
        }

        public Bitmap Read(int level)
        {
            IntPtr dataPtr = Marshal.AllocHGlobal(width * height * pixelsize);

            GL.BindTexture(this.target, this.glname);
            GL.GetTexImage(this.target, level, this.pixelformat, this.pixeltype, dataPtr);
            GL.BindTexture(this.target, 0);
            throwExceptionOnOpenGlError("image", name, "read texture data from GPU");

            Bitmap bmp = new Bitmap(width, height, width * pixelsize, fileformat, dataPtr);
            
            return bmp;
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTexture(glname);
                glname = 0;
            }
        }

        #region UTIL METHODS

        private static byte[] loadImageFiles(string[] filenames, int w, int h, int d, PixelInternalFormat gpuformat, 
            out PixelFormat pixelformat, out PixelType pixeltype, out int pixelsize, out SysImg.PixelFormat fileformat)
        {
            if (gpuformat.ToString().StartsWith("DepthComponent"))
            {
                pixelformat = PixelFormat.DepthComponent;
                pixeltype = PixelType.Float;
            }
            else
            {
                pixelformat = PixelFormat.Bgra;
                pixeltype = PixelType.UnsignedByte;
            }
            
            fileformat = SysImg.PixelFormat.Format32bppArgb;
            pixelsize = Image.GetPixelFormatSize(fileformat) / 8;
            byte[] data = null;

            if (filenames != null && filenames.Length > 0)
            {
                data = new byte[pixelsize * w * h * (d > 0 ? d : filenames.Length)];

                for (int i = 0; i < filenames.Length; i++)
                {
                    var bmp = new Bitmap(filenames[i]);
                    var bmpData = bmp.LockBits(
                        new Rectangle(0, 0, Math.Min(bmp.Width, w), Math.Min(bmp.Height, h)),
                        SysImg.ImageLockMode.ReadOnly, fileformat);
                
                    Marshal.Copy(bmpData.Scan0, data, pixelsize * w * h * i, bmpData.Stride * bmpData.Height);

                    bmp.UnlockBits(bmpData);
                }
            }

            return data;
        }

        #endregion
    }
}
