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
        private int unit;
        private int size;
        private int[] location;
        private int[] length;
        private int[] offset;
        private int[] stride;
        private int[] matstride;
        private int[] data;
        public int Unit { get { return unit; } }
        public IntPtr Size { get { return (IntPtr)size; } }
        public int this[Names name] { get { return location[Convert.ToInt32(name)]; } }

        public UniformBlock(int program, string name)
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
            matstride = new int[names.Length];

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
                    GL.GetActiveUniforms(program, 1, ref location[i],
                        ActiveUniformParameter.UniformMatrixStride, out matstride[i]);
                    if (matstride[i] == 0 && stride[i] == 0)
                        matstride[i] = Math.Max(matstride[i], 4);
                }
            }

            // allocate GPU memory
            glbuf = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.UniformBuffer, glbuf);
            GL.BufferData(BufferTarget.UniformBuffer, Size, IntPtr.Zero, BufferUsageHint.DynamicDraw);

            // allocate CPU memory
            data = new int[size / Marshal.SizeOf<int>()];
        }

        public void Bind()
        {
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, unit, glbuf);
        }

        public void Update()
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, glbuf);
            IntPtr ptr = GL.MapBuffer(BufferTarget.UniformBuffer, BufferAccess.WriteOnly);
            Marshal.Copy(data, 0, ptr, data.Length);
            GL.UnmapBuffer(BufferTarget.UniformBuffer);
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);
        }

        public void Set(Names name, Array src)
        {
            int idx = Convert.ToInt32(name);
            if (location[idx] < 0 || src == null)
                return;
            int elementSize = Marshal.SizeOf(src.GetType().GetElementType());
            int size = Math.Min(
                length[idx] * Math.Max(stride[idx], 4*matstride[idx]),
                elementSize * src.Length);
            Buffer.BlockCopy(src, 0, data, offset[idx], size);
        }

        public void Delete()
        {
            if (glbuf > 0)
            {
                GL.DeleteBuffer(glbuf);
                glbuf = 0;
            }
        }
    }

    public static class SaveConverter
    {
        public static int[] ToInt32(float[] from)
        {
            Converter con = default(Converter);
            return from.Select(x => { con.Float = x; return con.Int; }).ToArray();
        }

        public static float[] ToFloat(int[] from)
        {
            Converter con = default(Converter);
            return from.Select(x => { con.Int = x; return con.Float; }).ToArray();
        }

        public static int[] ToInt32(this Vector4 v)
        {
            Converter con = default(Converter);
            return new[] { con.ToInt(v.X), con.ToInt(v.Y), con.ToInt(v.Z), con.ToInt(v.W) };
        }

        public static int[] ToInt32(this Matrix4 v)
        {
            Converter con = default(Converter);
            return new int[] {
                con.ToInt(v.M11), con.ToInt(v.M12), con.ToInt(v.M13), con.ToInt(v.M14),
                con.ToInt(v.M21), con.ToInt(v.M22), con.ToInt(v.M23), con.ToInt(v.M24),
                con.ToInt(v.M31), con.ToInt(v.M32), con.ToInt(v.M33), con.ToInt(v.M34),
                con.ToInt(v.M41), con.ToInt(v.M42), con.ToInt(v.M43), con.ToInt(v.M44),
            };
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)]
            public int Int;
            [FieldOffset(0)]
            public float Float;

            public float ToFloat(int v)
            {
                Int = v;
                return Float;
            }
            public int ToInt(float v)
            {
                Float = v;
                return Int;
            }
        }
    }
}
