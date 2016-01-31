using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using LockMode = System.Drawing.Imaging.ImageLockMode;
using TexTarget = OpenTK.Graphics.OpenGL4.TextureTarget;
using TexParamName = OpenTK.Graphics.OpenGL4.TextureParameterName;
using TexMinFilter = OpenTK.Graphics.OpenGL4.TextureMinFilter;
using CpuFormat = System.Drawing.Imaging.PixelFormat;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using GpuColorFormat = OpenTK.Graphics.OpenGL4.PixelInternalFormat;
using TexParameter = OpenTK.Graphics.OpenGL4.GetTextureParameter;

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
        [Field] private GpuFormat format = GpuFormat.Rgba8;
        #endregion

        #region PROPERTIES
        public TexTarget target { get { return type; } private set { type = value; } }
        public GpuFormat gpuFormat { get { return format; } private set { format = value; } }
        #endregion

        /// <summary>
        /// Link GLBuffer to existing OpenGL image. Used
        /// to provide debug information in the debug view.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="anno"></param>
        /// <param name="glname">OpenGL object to like to.</param>
        public GLImage(string name, string anno, int glname) : base(name, anno)
        {
            int f, t;
            this.glname = glname;
            GL.GetTextureParameter(glname, TexParameter.TextureTarget, out t);
            GL.GetTextureLevelParameter(glname, 0, TexParameter.TextureInternalFormat, out f);
            GL.GetTextureLevelParameter(glname, 0, TexParameter.TextureWidth, out width);
            GL.GetTextureLevelParameter(glname, 0, TexParameter.TextureHeight, out height);
            GL.GetTextureLevelParameter(glname, 0, TexParameter.TextureDepth, out depth);
            type = (TexTarget)t;
            format = (GpuFormat)f;
            if (type != TexTarget.Texture3D)
            {
                length = depth;
                depth = 0;
            }
        }

        /// <summary>
        /// Create OpenGL image object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLImage(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"image '{name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

            // on errors throw an exception
            if (err.HasErrors())
                throw err;

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
                        + "'depth' and 'length'. Please check these parameters or specify "
                        + "the type directly (e.g. 'type = texture2D').", block);
            }

            // LOAD IMAGE DATA
            string dir = Path.GetDirectoryName(block.File) + Path.DirectorySeparatorChar;
            var data = LoadImageFiles(dir, file, ref width, ref height, ref depth, format);

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
                TexImage(target, width, height, depth, length, PixelFormat.Bgra, PixelType.UnsignedByte, dataPtr);
                Marshal.FreeHGlobal(dataPtr);
            }
            else
                TexImage(target, width, height, depth, length, mipmaps);

            // GENERATE MIPMAPS
            if (mipmaps > 0)
                GL.GenerateMipmap((GenerateMipmapTarget)target);

            GL.BindTexture(target, 0);
            if (HasErrorOrGlError(err, block))
                throw err;
        }
        
        /// <summary>
        /// Read whole GPU image data.
        /// </summary>
        /// <param name="level">Mipmap level.</param>
        /// <param name="index">Array index or texture depth index.</param>
        /// <returns>Return GPU image data as bitmap.</returns>
        public Bitmap Read(int level, int index) => ReadBmp(glname, level, index);

        /// <summary>
        /// Read whole GPU image data.
        /// </summary>
        /// <param name="ID">OpenGL image ID.</param>
        /// <param name="level">Mipmap level.</param>
        /// <param name="index">Array index or texture depth index.</param>
        /// <returns>Return GPU image data as bitmap.</returns>
        public static Bitmap ReadBmp(int ID, int level, int index)
        {
            // get texture format, width, height and depth on the GPU
            int w, h, d, f;
            GL.GetTextureLevelParameter(ID, level, TexParameter.TextureInternalFormat, out f);
            GL.GetTextureLevelParameter(ID, level, TexParameter.TextureWidth, out w);
            GL.GetTextureLevelParameter(ID, level, TexParameter.TextureHeight, out h);
            GL.GetTextureLevelParameter(ID, level, TexParameter.TextureDepth, out d);

            // convert to actual types
            var format = (GpuColorFormat)f;
            var isdepth = format.ToString().StartsWith("Depth");
            index = Math.Min(index, d);

            // allocate memory
            int size;
            var data = ReadSubImage(ID, level, 0, 0, index, w, h, 1,
                isdepth ? PixelFormat.DepthComponent : PixelFormat.Bgra,
                isdepth ? PixelType.Float : PixelType.UnsignedByte, out size);

            // create bitmap from data
            var bmp = new Bitmap(w, h, CpuFormat.Format32bppArgb);
            var px = bmp.LockBits(new Rectangle(0, 0, w, h), LockMode.WriteOnly, CpuFormat.Format32bppRgb);
            data.CopyTo(px.Scan0, size);
            bmp.UnlockBits(px);

            // free memory allocated by OpenGL
            Marshal.FreeHGlobal(data);
            return bmp;
        }

        /// <summary>
        /// Read GPU sub-image data.
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="ID">OpenGL texture name</param>
        /// <param name="level">texture mipmap level</param>
        /// <param name="x">sub-image x offset</param>
        /// <param name="y">sub-image y offset</param>
        /// <param name="z">sub-image z offset (for texture arrays and 3D textures)</param>
        /// <param name="w">sub-image width</param>
        /// <param name="h">sub-image height</param>
        /// <param name="d">sub-image depth</param>
        /// <param name="format">returned pixel format (RGBA, BRGA, Red, Depth, ...)</param>
        /// <param name="type">returned pixel type (UnsignedByte, Short, UnsignedInt, ...)</param>
        /// <returns></returns>
        public static T[] Read<T>(int ID, int level, int x, int y, int z, int w, int h, int d,
            PixelFormat format, PixelType type) where T : struct
        {
            int size;

            // read sub-image data and convert it to T[]
            var data = ReadSubImage(ID, level, x, y, z, w, h, d, format, type, out size);
            var ptr = data.To<T>(size);

            // free memory allocated by OpenGL
            Marshal.FreeHGlobal(data);
            return ptr;
        }

        private static IntPtr ReadSubImage(int ID, int level, int x, int y, int z, int w, int h, int d,
            PixelFormat format, PixelType type, out int size)
        {
            // compute size of the sub-image
            size = w * h * d * ColorChannels(format) * ColorBits(type) / 8;

            // read image data from GPU
            var data = Marshal.AllocHGlobal(size);
            GL.GetTextureSubImage(ID, level, x, y, z, w, h, d, format, type, size, data);
            return data;
        }

        private static int ColorBits(PixelType type)
        {
            switch (type)
            {
                case PixelType.Byte:
                case PixelType.UnsignedByte:
                case PixelType.UnsignedByte233Reversed:
                case PixelType.UnsignedByte332:
                    return 8;
                case PixelType.Short:
                case PixelType.HalfFloat:
                case PixelType.UnsignedShort:
                case PixelType.UnsignedShort1555Reversed:
                case PixelType.UnsignedShort4444:
                case PixelType.UnsignedShort4444Reversed:
                case PixelType.UnsignedShort5551:
                case PixelType.UnsignedShort565:
                case PixelType.UnsignedShort565Reversed:
                    return 16;
                default:
                    return 32;
            }
        }

        private static int ColorChannels(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.Red:
                case PixelFormat.RedInteger:
                case PixelFormat.Green:
                case PixelFormat.GreenInteger:
                case PixelFormat.Blue:
                case PixelFormat.BlueInteger:
                case PixelFormat.Alpha:
                case PixelFormat.AlphaInteger:
                case PixelFormat.StencilIndex:
                case PixelFormat.UnsignedInt:
                case PixelFormat.UnsignedShort:
                case PixelFormat.DepthComponent:
                case PixelFormat.ColorIndex:
                case PixelFormat.Luminance:
                    return 1;
                case PixelFormat.Rg:
                case PixelFormat.RgInteger:
                case PixelFormat.DepthStencil:
                    return 2;
                case PixelFormat.Rgb:
                case PixelFormat.Bgr:
                case PixelFormat.BgrInteger:
                    return 3;
                default:
                    return 4;
            }
        }
        
        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTexture(glname);
                glname = 0;
            }
        }

        public string GetLable() => GetLable(glname);

        public static string GetLable(int glname) => GetLable(ObjectLabelIdentifier.Texture, glname);

        #region UTIL METHODS
        private static byte[] LoadImageFiles(string dir, string[] filenames,
            ref int w, ref int h, ref int d, GpuFormat gpuformat)
        {
            // SET DEFAULT DATA FOR OUTPUTS
            byte[] data = null;
            bool isdepth = gpuformat.ToString().StartsWith("Depth");
            // set default file format and pixel size
            var fileformat = CpuFormat.Format32bppArgb;
            var pixelsize = Image.GetPixelFormatSize(fileformat) / 8;

            // LOAD IMAGA DATA FROM FILES
            if (filenames?.Length > 0 && !isdepth)
            {
                // pre-load all files to get information
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
                    bmps[i].RotateFlip(RotateFlipType.RotateNoneFlipY);
                    var bits = bmps[i].LockBits(
                        new Rectangle(0, 0, Math.Min(bmps[i].Width, w), Math.Min(bmps[i].Height, h)),
                        LockMode.ReadOnly, fileformat);
                    Marshal.Copy(bits.Scan0, data, pixelsize * w * h * i, bits.Stride * bits.Height);
                    bmps[i].UnlockBits(bits);
                }
            }

            return data;
        }

        private void TexImage(TexTarget target, int width, int height, int depth, int length, int levels)
        {
            levels = levels <= 0 ? MaxMipmapLevels(width, height) : 1;
            var colFormat = (SizedInternalFormat)gpuFormat;
            switch (target)
            {
                case TexTarget.Texture1D:
                    GL.TexStorage1D((TextureTarget1d)target, levels, colFormat, width);
                    break;
                case TexTarget.Texture1DArray:
                    GL.TexStorage2D((TextureTarget2d)target, levels, colFormat, width, height);
                    break;
                case TexTarget.Texture2D:
                    GL.TexStorage2D((TextureTarget2d)target, levels, colFormat, width, height);
                    break;
                case TexTarget.Texture2DArray:
                    GL.TexStorage3D((TextureTarget3d)target, levels, colFormat, width, height, length);
                    break;
                case TexTarget.Texture3D:
                    GL.TexStorage3D((TextureTarget3d)target, levels, colFormat, width, height, depth);
                    break;
            }
        }

        private void TexImage(TexTarget target, int width, int height, int depth, int length,
            PixelFormat format, PixelType type, IntPtr pixels)
        {
            var colFormat = (GpuColorFormat)gpuFormat;
            switch (target)
            {
                case TexTarget.Texture1D:
                    GL.TexImage1D(target, 0, colFormat, width, 0, format, type, pixels);
                    break;
                case TexTarget.Texture1DArray:
                    GL.TexImage2D(target, 0, colFormat, width, length, 0, format, type, pixels);
                    break;
                case TexTarget.Texture2D:
                    GL.TexImage2D(target, 0, colFormat, width, height, 0, format, type, pixels);
                    break;
                case TexTarget.Texture2DArray:
                    GL.TexImage3D(target, 0, colFormat, width, height, length, 0, format, type, pixels);
                    break;
                case TexTarget.Texture3D:
                    GL.TexImage3D(target, 0, colFormat, width, height, depth, 0, format, type, pixels);
                    break;
            }
        }

        private static int MaxMipmapLevels(int width, int height)
        {
            int n = 0;
            while (width > 1 || height > 1)
            {
                width = Math.Max(width / 2, 1);
                height = Math.Max(height / 2, 1);
                n++;
            }
            return n;
        }
        #endregion
    }
    
    public enum GpuFormat
    {
        Rgba8 = 32856,
        Rgba16 = 32859,
        R8 = 33321,
        R16 = 33322,
        Rg8 = 33323,
        Rg16 = 33324,
        R16f = 33325,
        R32f = 33326,
        Rg16f = 33327,
        Rg32f = 33328,
        R8i = 33329,
        R8ui = 33330,
        R16i = 33331,
        R16ui = 33332,
        R32i = 33333,
        R32ui = 33334,
        Rg8i = 33335,
        Rg8ui = 33336,
        Rg16i = 33337,
        Rg16ui = 33338,
        Rg32i = 33339,
        Rg32ui = 33340,
        Rgba32f = 34836,
        Rgba16f = 34842,
        Rgba32ui = 36208,
        Rgba16ui = 36214,
        Rgba8ui = 36220,
        Rgba32i = 36226,
        Rgba16i = 36232,
        Rgba8i = 36238,
        Depth16 = 33189,
        Depth24 = 33190,
        Depth32 = 33191,
        Depth32f = 36012,
        Depth24Stencil8 = 35056,
        R3G3B2 = 10768,
        Rgb2 = 32846,
        Rgb4 = 32847,
        Rgb5 = 32848,
        Rgb8 = 32849,
        Rgb10 = 32850,
        Rgb12 = 32851,
        Rgb16 = 32852,
        Rgba2 = 32853,
        Rgba4 = 32854,
        Rgb5A1 = 32855,
        Rgb10A2 = 32857,
        Rgba12 = 32858,
        Deptht24 = 33190,
        Rgb32f = 34837,
        Rgb16f = 34843,
        R11fG11fB10f = 35898,
        Rgb9E5 = 35901,
        Depth32fStencil8 = 36013,
        Rgb32ui = 36209,
        Rgb16ui = 36215,
        Rgb8ui = 36221,
        Rgb32i = 36227,
        Rgb16i = 36233,
        Rgb8i = 36239,
        R8Snorm = 36756,
        Rg8Snorm = 36757,
        Rgb8Snorm = 36758,
        Rgba8Snorm = 36759,
        R16Snorm = 36760,
        Rg16Snorm = 36761,
        Rgb16Snorm = 36762,
        Rgba16Snorm = 36763,
        Rgb10A2ui = 36975
    }
}
