using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using SysImg = System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using FilePixelFormat = System.Drawing.Imaging.PixelFormat;
using TexTarget = OpenTK.Graphics.OpenGL4.TextureTarget;
using TexParamName = OpenTK.Graphics.OpenGL4.TextureParameterName;
using TexMinFilter = OpenTK.Graphics.OpenGL4.TextureMinFilter;
using GpuFormat = OpenTK.Graphics.OpenGL4.PixelInternalFormat;

namespace App
{
    class GLImage : GLObject
    {
        #region FIELDS
        [GLField]
        private string[] file = null;
        [GLField]
        public int width = 1;
        [GLField]
        public int height = 0;
        [GLField]
        public int depth = 0;
        [GLField]
        public int length = 0;
        [GLField]
        public int mipmaps = 0;
        [GLField]
        private TexTarget type = 0;
        [GLField]
        private GpuFormat format = GpuFormat.Rgba;
        private FilePixelFormat fileFormat = FilePixelFormat.Format32bppArgb;
        private PixelType pxType = 0;
        private int pxSize = 0;
        private PixelFormat pxFormat = 0;
        #endregion

        #region PROPERTIES
        public TexTarget target { get { return type; } private set { type = value; } }
        public GpuFormat gpuFormat { get { return format; } private set { format = value; } }
        #endregion

        public GLImage(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            var err = new GLException($"image '{name}'");

            // PARSE TEXT
            var cmds = new Commands(text, err);

            // PARSE ARGUMENTS
            cmds.Cmds2Fields(this, err);

            // if type was not specified
            if (target == 0)
            {
                if (width > 0 && height == 1 && depth == 0 && length == 0)
                    target = TexTarget.Texture1D;
                else if (width > 0 && height == 1 && depth == 0 && length > 0)
                    target = TexTarget.Texture1DArray;
                else if (width > 0 && height > 1 && depth == 0 && length == 0)
                    target = TexTarget.Texture2D;
                else if (width > 0 && height > 1 && depth == 0 && length > 0)
                    target = TexTarget.Texture2DArray;
                else if (width > 0 && height > 1 && depth > 0 && length == 0)
                    target = TexTarget.Texture3D;
                else
                    err.Add("Texture type could not be derived from 'width', 'height', "
                        + "'depth' and 'length'. Please check these parameters "
                        + "or specify the type directly (e.g. 'type = texture2D').");
            }

            // LOAD IMAGE DATA
            var data = loadImageFiles(err, dir, file, ref width, ref height, ref depth, gpuFormat,
                out pxFormat, out pxType, out pxSize, out fileFormat);

            // on errors throw an exception
            if (err.HasErrors())
                throw err;

            // CREATE OPENGL OBJECT
            glname = GL.GenTexture();
            GL.BindTexture(target, glname);
            GL.TexParameter(target, TexParamName.TextureMinFilter,
                (int)(mipmaps > 0 ? TexMinFilter.NearestMipmapNearest : TexMinFilter.Nearest));
            GL.TexParameter(target, TexParamName.TextureMagFilter,
                (int)(mipmaps > 0 ? TexMinFilter.NearestMipmapNearest : TexMinFilter.Nearest));
            GL.TexParameter(target, TexParamName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(target, TexParamName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(target, TexParamName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            // ALLOCATE IMAGE MEMORY
            if (data != null)
            {
                var dataPtr = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, dataPtr, data.Length);
                TexImage(target, gpuFormat, width, height, depth, length, pxFormat, pxType, dataPtr);
                Marshal.FreeHGlobal(dataPtr);
            }
            else
                TexImage(target, gpuFormat, width, height, depth, length, pxFormat, pxType, IntPtr.Zero);

            // GENERATE MIPMAPS
            if (mipmaps > 0)
                GL.GenerateMipmap((GenerateMipmapTarget)target);

            GL.BindTexture(target, 0);
            if (GL.GetError() != ErrorCode.NoError)
                err.Throw($"OpenGL error '{GL.GetError()}' occurred during image allocation.");
        }

        public Bitmap Read(int level)
        {
            // compute mipmap level size
            int w = width, h = height, l = level;
            while (l-- > 0)
            {
                w /= 2;
                h /= 2;
            }

            // allocate memory
            IntPtr dataPtr = Marshal.AllocHGlobal(w * h * pxSize);

            // get image data
            GL.BindTexture(target, glname);
            GL.GetTexImage(target, level, pxFormat, pxType, dataPtr);
            GL.BindTexture(target, 0);

            // create bitmap from data
            return new Bitmap(w, h, w * pxSize, fileFormat, dataPtr);
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
        private static byte[] loadImageFiles(GLException err, string dir, string[] filenames,
            ref int w, ref int h, ref int d, GpuFormat gpuformat, 
            out PixelFormat pixelformat, out PixelType pixeltype, out int pixelsize,
            out FilePixelFormat fileformat)
        {
            // SET DEFAULT DATA FOR OUTPUTS
            byte[] data = null;
            bool isdepth = gpuformat.ToString().StartsWith("DepthComponent");
            // set default pixel data format and type
            pixelformat = isdepth ? PixelFormat.DepthComponent : PixelFormat.Bgra;
            pixeltype = isdepth ? PixelType.Float : PixelType.UnsignedByte;
            // set default file format and pixel size
            fileformat = FilePixelFormat.Format32bppArgb;
            pixelsize = Image.GetPixelFormatSize(fileformat) / 8;

            // LOAD IMAGA DATA FROM FILES
            if (filenames?.Length > 0 && !isdepth)
            {
                // preload all files to get information
                // like minimal width and height
                var bmps = new Bitmap[filenames.Length];
                int imgW = int.MaxValue;
                int imgH = int.MaxValue;
                int imgD = d > 0 ? Math.Min(filenames.Length, d) : filenames.Length;

                for (int i = 0; i < imgD; i++)
                {
                    var path = Path.IsPathRooted(filenames[i]) ? filenames[i] : dir + filenames[i];
                    bmps[i] = new Bitmap(path);
                    imgW = Math.Min(bmps[i].Width, imgW);
                    imgH = Math.Min(bmps[i].Height, imgH);
                }

                // if w, h and d where not set by the user,
                // use the minimal image size
                if (w == 1 && h == 0 && d == 0)
                {
                    w = imgW;
                    h = imgH;
                    d = imgD;
                }

                // allocate texture memory
                data = new byte[pixelsize * w * h * d];

                // copy data to texture memory
                for (int i = 0; i < imgD; i++)
                {
                    var bits = bmps[i].LockBits(
                        new Rectangle(0, 0, Math.Min(bmps[i].Width, w), Math.Min(bmps[i].Height, h)),
                        SysImg.ImageLockMode.ReadOnly, fileformat);
                    Marshal.Copy(bits.Scan0, data, pixelsize * w * h * i, bits.Stride * bits.Height);
                    bmps[i].UnlockBits(bits);
                }
            }

            return data;
        }

        public void TexImage(TexTarget target, GpuFormat internalformat, int width, int height,
            int depth, int length, PixelFormat format, PixelType type, IntPtr pixels)
        {
            switch (target)
            {
                case TexTarget.Texture1D:
                    GL.TexImage1D(target, 0, gpuFormat, width,
                        0, pxFormat, pxType, pixels);
                    break;
                case TexTarget.Texture1DArray:
                    GL.TexImage2D(target, 0, gpuFormat, width, length,
                        0, pxFormat, pxType, pixels);
                    break;
                case TexTarget.Texture2D:
                    GL.TexImage2D(target, 0, gpuFormat, width, height,
                        0, pxFormat, pxType, pixels);
                    break;
                case TexTarget.Texture2DArray:
                    GL.TexImage3D(target, 0, gpuFormat, width, height, length,
                        0, pxFormat, pxType, pixels);
                    break;
                case TexTarget.Texture3D:
                    GL.TexImage3D(target, 0, gpuFormat, width, height, depth,
                        0, pxFormat, pxType, pixels);
                    break;
            }
        }
        #endregion
    }
}
