using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using SysImg = System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using TexTarget = OpenTK.Graphics.OpenGL4.TextureTarget;
using TexParamName = OpenTK.Graphics.OpenGL4.TextureParameterName;
using TexMinFilter = OpenTK.Graphics.OpenGL4.TextureMinFilter;
using CpuFormat = System.Drawing.Imaging.PixelFormat;
using GpuFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using GpuColorFormat = OpenTK.Graphics.OpenGL4.PixelInternalFormat;

namespace App
{
    class GLImage : GLObject
    {
        #region FIELDS
        [Field] private string[] file = null;
        [Field] public int width = 1;
        [Field] public int height = 0;
        [Field] public int depth = 0;
        [Field] public int length = 0;
        [Field] public int mipmaps = 0;
        [Field] private TexTarget type = 0;
        [Field] private GpuColorFormat format = GpuColorFormat.Rgba;
        private CpuFormat fileFormat = CpuFormat.Format32bppArgb;
        private PixelType pxType = 0;
        private int pxSize = 0;
        private GpuFormat pxFormat = 0;
        #endregion

        #region PROPERTIES
        public TexTarget target { get { return type; } private set { type = value; } }
        public GpuColorFormat gpuFormat { get { return format; } private set { format = value; } }
        #endregion

        /// <summary>
        /// Link GLBuffer to existing OpenGL image. Used
        /// to provide debug information in the debug view.
        /// </summary>
        /// <param name="name">Name the object.</param>
        /// <param name="annotation">Annotate the object.</param>
        /// <param name="glname">OpenGL object to like to.</param>
        public GLImage(string name, string anno, int glname)
            : base(name, anno)
        {
            int f, t;
            this.glname = glname;
            GL.GetTextureParameter(glname, GetTextureParameter.TextureTarget, out t);
            GL.GetTextureLevelParameter(glname, 0, GetTextureParameter.TextureInternalFormat, out f);
            GL.GetTextureLevelParameter(glname, 0, GetTextureParameter.TextureWidth, out width);
            GL.GetTextureLevelParameter(glname, 0, GetTextureParameter.TextureHeight, out height);
            GL.GetTextureLevelParameter(glname, 0, GetTextureParameter.TextureDepth, out depth);
            type = (TexTarget)t;
            format = (GpuColorFormat)f;
            if (type != TexTarget.Texture3D)
            {
                length = depth;
                depth = 0;
            }
        }

        /// <summary>
        /// Create OpenGL image object.
        /// </summary>
        /// <param name="dir">Directory of the tech-file.</param>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="annotation">Annotation used for special initialization.</param>
        /// <param name="text">Text block specifying the object commands.</param>
        /// <param name="classes">Collection of scene objects.</param>
        public GLImage(string dir, string name, string anno, string text, Dict<GLObject> classes)
            : base(name, anno)
        {
            var err = new CompileException($"image '{name}'");

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
            var data = loadImageFiles(err, dir, file, ref width, ref height, ref depth, format,
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
                TexImage(target, width, height, depth, length, pxFormat, pxType, dataPtr);
                Marshal.FreeHGlobal(dataPtr);
            }
            else
                TexImage(target, width, height, depth, length, pxFormat, pxType, IntPtr.Zero);

            // GENERATE MIPMAPS
            if (mipmaps > 0)
                GL.GenerateMipmap((GenerateMipmapTarget)target);

            GL.BindTexture(target, 0);
            if (HasErrorOrGlError(err))
                throw err;
        }

        /// <summary>
        /// Read whole GPU image data.
        /// </summary>
        /// <param name="level">Mipmap level.</param>
        /// <param name="index">Array index or texture depth index.</param>
        /// <returns>Return GPU image data as bitmap.</returns>
        public Bitmap Read(int level, int index)
        {
            return Read(glname, level, index);
        }

        /// <summary>
        /// Read whole GPU image data.
        /// </summary>
        /// <param name="ID">OpenGL image ID.</param>
        /// <param name="level">Mipmap level.</param>
        /// <param name="index">Array index or texture depth index.</param>
        /// <returns>Return GPU image data as bitmap.</returns>
        public static Bitmap Read(int ID, int level, int index)
        {
            int w, h, d, f;
            GL.GetTextureLevelParameter(ID, level, GetTextureParameter.TextureInternalFormat, out f);
            GL.GetTextureLevelParameter(ID, level, GetTextureParameter.TextureWidth, out w);
            GL.GetTextureLevelParameter(ID, level, GetTextureParameter.TextureHeight, out h);
            GL.GetTextureLevelParameter(ID, level, GetTextureParameter.TextureDepth, out d);

            // convert to actual types
            var format = (GpuColorFormat)f;
            var isdepth = format.ToString().StartsWith("DepthComponent");
            var pxSize = 4;
            var bufSize = w * h * pxSize;
            index = Math.Min(index, d);
            
            // allocate memory
            IntPtr dataPtr = Marshal.AllocHGlobal(bufSize);

            // get image data
            GL.GetTextureSubImage(ID, level, 0, 0, index, w, h, 1,
                isdepth ? GpuFormat.DepthComponent : GpuFormat.Bgra,
                isdepth ? PixelType.Float : PixelType.UnsignedByte, bufSize, dataPtr);

            // create bitmap from data
            return new Bitmap(w, h, w * pxSize, CpuFormat.Format32bppArgb, dataPtr);
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
        private static byte[] loadImageFiles(CompileException err, string dir, string[] filenames,
            ref int w, ref int h, ref int d, GpuColorFormat gpuformat, 
            out GpuFormat pixelformat, out PixelType pixeltype, out int pixelsize,
            out CpuFormat fileformat)
        {
            // SET DEFAULT DATA FOR OUTPUTS
            byte[] data = null;
            bool isdepth = gpuformat.ToString().StartsWith("DepthComponent");
            // set default pixel data format and type
            pixelformat = isdepth ? GpuFormat.DepthComponent : GpuFormat.Bgra;
            pixeltype = isdepth ? PixelType.Float : PixelType.UnsignedByte;
            // set default file format and pixel size
            fileformat = CpuFormat.Format32bppArgb;
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
                
                //// swap R and B color channel
                //for (int r = 1, b = 3; r < data.Length; r += 4, b += 4)
                //{
                //    var tmp = data[r];
                //    data[r] = data[b];
                //    data[b] = tmp;
                //}
            }

            return data;
        }

        private void TexImage(TexTarget target, int width, int height,
            int depth, int length, GpuFormat format, PixelType type, IntPtr pixels)
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
