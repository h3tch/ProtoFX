using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace csharp
{
    public class UniformBlock<Names>
    {
        private int glbuf;
        private IntPtr ptr;
        private int unit;
        private int size;
        private int[] location;
        private int[] length;
        private int[] offset;
        private int[] stride;
        public int Unit { get { return unit; } }
        public IntPtr Size { get { return (IntPtr)size; } }
        public int this[Names name] { get { return location[Convert.ToInt32(name)]; } }

        public UniformBlock(int program, string name, params object[] nameArrayPairs)
        {
            // get uniform block binding unit and size
            int block = GL.GetUniformBlockIndex(program, name);
            GL.GetActiveUniformBlock(program, block,
                ActiveUniformBlockParameter.UniformBlockBinding, out unit);
            GL.GetActiveUniformBlock(program, block,
                ActiveUniformBlockParameter.UniformBlockDataSize, out size);

            // allocate memory for uniform block uniforms
            string[] names = Enum.GetNames(typeof(Names)).Select(v => name + "." + v).ToArray();
            location = Enumerable.Repeat(-1, names.Length).ToArray();
            length = new int[names.Length];
            offset = new int[names.Length];
            stride = new int[names.Length];

            // get uniform indices in uniform block
            GL.GetUniformIndices(program, names.Length, names, location);

            // get additional information about uniforms
            // like array length, offset and stride
            for (int i = 0; i < location.Length; i++)
            {
                if (location[i] >= 0)
                {
                    GL.GetActiveUniforms(program, 1, ref location[i],
                        ActiveUniformParameter.UniformSize, out length[i]);
                    GL.GetActiveUniforms(program, 1, ref location[i],
                        ActiveUniformParameter.UniformOffset, out offset[i]);
                    GL.GetActiveUniforms(program, 1, ref location[i],
                        ActiveUniformParameter.UniformArrayStride, out stride[i]);
                }
            }

            // allocate GPU memory
            glbuf = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.UniformBuffer, glbuf);

            // allocate CPU memory
            ptr = Marshal.AllocHGlobal(size);
            for (int i = 0; i < nameArrayPairs.Length; i += 2)
                Copy((Names)nameArrayPairs[i], (Array)nameArrayPairs[i + 1]);

            // copy CPU data to GPU
            GL.BufferData(BufferTarget.UniformBuffer, Size,
                nameArrayPairs.Length > 0 ? ptr : IntPtr.Zero,
                BufferUsageHint.DynamicDraw);
        }

        public void Bind()
        {
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, unit, glbuf);
        }

        public void Update()
        {

            GL.BindBuffer(BufferTarget.UniformBuffer, glbuf);
            IntPtr ptr = GL.MapBuffer(BufferTarget.UniformBuffer, BufferAccess.WriteOnly);
            Mem.Copy(this.ptr, ptr, (uint)size);

            byte[] data = new byte[size];
            Marshal.Copy(this.ptr, data, 0, size);

            GL.UnmapBuffer(BufferTarget.UniformBuffer);
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);

            float[] mat = Enumerable.Range(0, size/4).Select(x => BitConverter.ToSingle(data, x)).ToArray();
        }

        public void Set(Names name, int[] src)
        {
            int idx = Convert.ToInt32(name);
            if (location[idx] < 0)
                return;
            Marshal.Copy(src, 0, ptr + stride[idx], src.Length);

            byte[] data = new byte[src.Length*4];
            Marshal.Copy(ptr, data, 0, size);
            float[] mat = Enumerable.Range(0, size / 4).Select(x => BitConverter.ToSingle(data, x)).ToArray();
        }

        public void Delete()
        {
            if (glbuf > 0)
            {
                GL.DeleteBuffer(glbuf);
                glbuf = 0;
            }
            // CPU memory no longer needed
            Marshal.FreeHGlobal(ptr);
        }

        private void Copy(Names name, Array array)
        {
            int idx = Convert.ToInt32(name);
            if (location[idx] < 0 || array == null)
                return;

            // gather some needed information
            int srcStride = Marshal.SizeOf(array.GetType().GetElementType())
                * (array.Rank == 2 ? array.GetLength(1) : 1);
            int dstStride = stride[idx];
            int len = Math.Min(array.GetLength(0), length[idx]);

            // convert array to byte array
            byte[] src = new byte[srcStride * array.GetLength(0)];
            Buffer.BlockCopy(array, 0, src, 0, src.Length);

            // change stride of src array
            src = ToStride(src, srcStride, dstStride);

            // copy to unmanaged memory
            Marshal.Copy(src, 0, ptr, dstStride * len);
        }

        private byte[] ToStride(byte[] src, int srcStride, int dstStride)
        {
            if (srcStride == dstStride || dstStride == 0)
                return src;

            byte[] dst = new byte[(src.Length / srcStride) * dstStride];

            int skip = dstStride - srcStride;
            for (int srci = 0, dsti = 0; srci < src.Length; dsti += skip)
                dst[dsti++] = src[srci++];

            return dst;
        }
    }

    static public class Mem
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void Copy(IntPtr dest, IntPtr src, uint count);
    }

    public static class SaveConverter
    {
        public static int[] ToInt32(this Vector4 v)
        {
            return new[] { v.X.AsInt32(), v.Y.AsInt32(), v.Z.AsInt32(), v.W.AsInt32() };
        }

        public static int[] ToInt32(this Matrix4 v)
        {
            return new[] {
                v.M11.AsInt32(), v.M12.AsInt32(), v.M13.AsInt32(), v.M14.AsInt32(),
                v.M21.AsInt32(), v.M22.AsInt32(), v.M23.AsInt32(), v.M24.AsInt32(),
                v.M31.AsInt32(), v.M32.AsInt32(), v.M33.AsInt32(), v.M34.AsInt32(),
                v.M41.AsInt32(), v.M42.AsInt32(), v.M43.AsInt32(), v.M44.AsInt32(),
            };
            //return new[] {
            //    v.M11.AsInt32(), v.M21.AsInt32(), v.M31.AsInt32(), v.M41.AsInt32(),
            //    v.M12.AsInt32(), v.M22.AsInt32(), v.M32.AsInt32(), v.M42.AsInt32(),
            //    v.M13.AsInt32(), v.M23.AsInt32(), v.M33.AsInt32(), v.M43.AsInt32(),
            //    v.M14.AsInt32(), v.M24.AsInt32(), v.M34.AsInt32(), v.M44.AsInt32(),
            //};
        }

        public static float[] ToFloat(this Matrix4 v)
        {
            return new[] {
                v.M11, v.M12, v.M13, v.M14,
                v.M21, v.M22, v.M23, v.M24,
                v.M31, v.M32, v.M33, v.M34,
                v.M41, v.M42, v.M43, v.M44,
            };
            //return new[] {
            //    v.M11, v.M21, v.M31, v.M41,
            //    v.M12, v.M22, v.M32, v.M42,
            //    v.M13, v.M23, v.M33, v.M43,
            //    v.M14, v.M24, v.M34, v.M44,
            //};
        }

        public static int AsInt32(this float v)
        {
            convert.Float = v;
            return convert.Int32;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Converter
        {
            [FieldOffset(0)]
            public int Int32;
            [FieldOffset(0)]
            public uint UInt32;
            [FieldOffset(0)]
            public float Float;
        }

        private static Converter convert = default(Converter);
    }
}
