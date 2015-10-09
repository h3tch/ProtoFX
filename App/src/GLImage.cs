using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using SysImg = System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace App
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

        public GLImage(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            ErrorCollector err = new ErrorCollector();
            err.PushStack("image '" + name + "'");

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
                    err.Add("Texture type could not be derived from 'width', 'height', "
                        + "'depth' and 'length'. Please check these parameters "
                        + "or specify the type directly (e.g. 'type = texture2D').");
            }

            // LOAD IMAGE DATA
            var data = loadImageFiles(err, dir, file, ref width, ref height, ref depth, gpuformat,
                out pixelformat, out pixeltype, out pixelsize, out fileformat);

            // on errors throw an exception
            if (err.HasErrors())
                err.ThrowExeption();

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

            // ALLOCATE IMAGE MEMORY
            if (data != null)
            {
                var dataPtr = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, dataPtr, data.Length);
                TexImage(target, gpuformat, width, height, depth, length, pixelformat, pixeltype, dataPtr);
                Marshal.FreeHGlobal(dataPtr);
            }
            else
                TexImage(target, gpuformat, width, height, depth, length, pixelformat, pixeltype, IntPtr.Zero);

            // GENERATE MIPMAPS
            if (mipmaps > 0)
                GL.GenerateMipmap((GenerateMipmapTarget)target);

            GL.BindTexture(target, 0);
            if (GL.GetError() != ErrorCode.NoError)
                err.Throw("OpenGL error '" + GL.GetError() + "' occurred during image allocation.");
        }

        public Bitmap Read(int level)
        {
            IntPtr dataPtr = Marshal.AllocHGlobal(width * height * pixelsize);

            GL.BindTexture(this.target, this.glname);
            GL.GetTexImage(this.target, level, this.pixelformat, this.pixeltype, dataPtr);
            GL.BindTexture(this.target, 0);

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
        private static byte[] loadImageFiles(ErrorCollector err, string dir, string[] filenames,
            ref int w, ref int h, ref int d, PixelInternalFormat gpuformat, 
            out PixelFormat pixelformat, out PixelType pixeltype, out int pixelsize,
            out SysImg.PixelFormat fileformat)
        {
            // SET DEFAULT DATA FOR OUTPUTS
            byte[] data = null;
            bool isdepth = gpuformat.ToString().StartsWith("DepthComponent");
            // set default pixel data format and type
            pixelformat = isdepth ? PixelFormat.DepthComponent : PixelFormat.Bgra;
            pixeltype = isdepth ? PixelType.Float : PixelType.UnsignedByte;
            // set default file format and pixel size
            fileformat = SysImg.PixelFormat.Format32bppArgb;
            pixelsize = Image.GetPixelFormatSize(fileformat) / 8;

            // LOAD IMAGA DATA FROM FILES
            if (filenames != null && filenames.Length > 0 && !isdepth)
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
                    var bmpData = bmps[i].LockBits(
                        new Rectangle(0, 0, Math.Min(bmps[i].Width, w), Math.Min(bmps[i].Height, h)),
                        SysImg.ImageLockMode.ReadOnly, fileformat);
                    Marshal.Copy(bmpData.Scan0, data, pixelsize * w * h * i, bmpData.Stride * bmpData.Height);
                    bmps[i].UnlockBits(bmpData);
                }
            }

            return data;
        }

        public void TexImage(TextureTarget target, PixelInternalFormat internalformat,
            int width, int height, int depth, int length, PixelFormat format, PixelType type, IntPtr pixels)
        {
            switch (target)
            {
                case TextureTarget.Texture1D:
                    GL.TexImage1D(target, 0, gpuformat, width, 0, pixelformat, pixeltype, pixels);
                    break;
                case TextureTarget.Texture1DArray:
                    GL.TexImage2D(target, 0, gpuformat, width, length, 0, pixelformat, pixeltype, pixels);
                    break;
                case TextureTarget.Texture2D:
                    GL.TexImage2D(target, 0, gpuformat, width, height, 0, pixelformat, pixeltype, pixels);
                    break;
                case TextureTarget.Texture2DArray:
                    GL.TexImage3D(target, 0, gpuformat, width, height, length, 0, pixelformat, pixeltype, pixels);
                    break;
                case TextureTarget.Texture3D:
                    GL.TexImage3D(target, 0, gpuformat, width, height, depth, 0, pixelformat, pixeltype, pixels);
                    break;
            }
        }
        #endregion
    }
}
