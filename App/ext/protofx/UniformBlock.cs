using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace protofx
{
    public class UniformBlock<Names>
    {
        #region FIELDS
        public int program;
        private int glbuf;
        private int unit;
        private int size;
        private int[] location;
        private int[] length;
        private int[] offset;
        private int[] stride;
        private int[] matstride;
        private int[] data;
        #endregion

        public Info this[Names name] { get {
            return new Info {
                location = location[Convert.ToInt32(name)],
                length = length[Convert.ToInt32(name)],
                offset = offset[Convert.ToInt32(name)],
                stride = stride[Convert.ToInt32(name)],
                matstride = matstride[Convert.ToInt32(name)],
            };
        } }

        public UniformBlock(int pipeline, string name)
        {
            int block = -1;
            if (!TryGetUnifromBlock(pipeline, name, out program, out block))
            {
                if (program <= 0)
                    throw new Exception("No shader program active in the current pipeline bound.");
                if (block < 0)
                    throw new Exception("Could not find uniform block '" + name + "'.");
            }

            GL.GetActiveUniformBlock(program, block,
                ActiveUniformBlockParameter.UniformBlockBinding, out unit);
            GL.GetActiveUniformBlock(program, block,
                ActiveUniformBlockParameter.UniformBlockDataSize, out size);
            if (size <= 0)
                throw new Exception("Uniform block '" + name + "' size of '" + size + "' is invalid.");

            // allocate memory for uniform block uniforms
            var names = Enum.GetNames(typeof(Names)).Select(v => name + "." + v).ToArray();
            location = Enumerable.Repeat(-1, names.Length).ToArray();
            length = new int[names.Length];
            offset = new int[names.Length];
            stride = new int[names.Length];
            matstride = new int[names.Length];
            data = new int[size / Marshal.SizeOf<int>()];

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
                    // if no stride information is provided,
                    // we have a default stride of 4 (vec4)
                    if (matstride[i] == 0 && stride[i] == 0)
                        matstride[i] = 4;
                    // convert to byte size
                    matstride[i] *= 4;
                }
            }

            // allocate GPU memory
            var flags = BufferStorageFlags.MapWriteBit;
#if DEBUG
            flags |= BufferStorageFlags.MapReadBit;
#endif
            glbuf = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.UniformBuffer, glbuf); // Note: needs to be bound once!
            GL.NamedBufferStorage(glbuf, size, IntPtr.Zero, flags);
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);

            // attach OpenGL object label
            name = "P" + program + ": " + name;
            GL.ObjectLabel(ObjectLabelIdentifier.Buffer, glbuf, name.Length, name);
        }

        public void Bind()
        {
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, unit, glbuf);
        }

        public void Update()
        {
            // map GPU memory
            IntPtr ptr = GL.MapNamedBuffer(glbuf, BufferAccess.WriteOnly);
            // copy buffer to maped buffer
            Marshal.Copy(data, 0, ptr, data.Length);
            // upload data
            GL.UnmapNamedBuffer(glbuf);
        }

        public bool Has(Names name)
        {
            return location[Convert.ToInt32(name)] >= 0;
        }

        public void Set(Names name, Array src)
        {
            int idx = Convert.ToInt32(name);
            if (location[idx] < 0 || src == null)
                return;
            // get the size of an array element
            int elementSize = Marshal.SizeOf(src.GetType().GetElementType());
            // get the size of the whole array, but
            // make sure it is not bigger than the buffer
            int size = Math.Min(this.size - offset[idx], elementSize * src.Length);
            // copy array to buffer
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

        public static bool HasUnifromBlock(int pipeline, string name)
        {
            int program = -1;
            int block = -1;
            return TryGetUnifromBlock(pipeline, name, out program, out block);
        }

        public static bool TryGetUnifromBlock(int pipeline, string name, out int program, out int block)
        {
            program = -1;
            block = -1;

            var stages = new[] {
                ProgramPipelineParameter.ComputeShader,
                ProgramPipelineParameter.VertexShader,
                ProgramPipelineParameter.TessControlShader,
                ProgramPipelineParameter.TessEvaluationShader,
                ProgramPipelineParameter.GeometryShader,
                ProgramPipelineParameter.FragmentShader,
            };
            
            foreach (var stage in stages)
            {
                // get the shader program of the pipeline
                GL.GetProgramPipeline(pipeline, stage, out program);
                if (program > 0 && (block = GL.GetUniformBlockIndex(program, name)) >= 0)
                    break;
            }

            return program != -1 && block != -1;
        }

        public struct Info
        {
            public int location;
            public int length;
            public int offset;
            public int stride;
            public int matstride;
        }
    }

    public static class SaveConverter
    {
        public static int[] AsInt32(this float[] from)
        {
            BitConverter bit = default(BitConverter);
            return from.Select(x => { bit.Float = x; return bit.Int32; }).ToArray();
        }

        public static float[] AsFloat(int[] from)
        {
            BitConverter bit = default(BitConverter);
            return from.Select(x => { bit.Int32 = x; return bit.Float; }).ToArray();
        }

        public static int[] AsInt32(this Vector4 v)
        {
            BitConverter bit = default(BitConverter);
            return new[] {
                bit.AsInt32(v.X), bit.AsInt32(v.Y), bit.AsInt32(v.Z), bit.AsInt32(v.W)
            };
        }

        public static int[] AsInt32(this Matrix4 v)
        {
            BitConverter bit = default(BitConverter);
            return new int[] {
                bit.AsInt32(v.M11), bit.AsInt32(v.M12), bit.AsInt32(v.M13), bit.AsInt32(v.M14),
                bit.AsInt32(v.M21), bit.AsInt32(v.M22), bit.AsInt32(v.M23), bit.AsInt32(v.M24),
                bit.AsInt32(v.M31), bit.AsInt32(v.M32), bit.AsInt32(v.M33), bit.AsInt32(v.M34),
                bit.AsInt32(v.M41), bit.AsInt32(v.M42), bit.AsInt32(v.M43), bit.AsInt32(v.M44),
            };
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct BitConverter
        {
            [FieldOffset(0)]
            public int Int32;
            [FieldOffset(0)]
            public float Float;

            public float AsFloat(int v)
            {
                Int32 = v;
                return Float;
            }
            public int AsInt32(float v)
            {
                Float = v;
                return Int32;
            }
        }
    }
}
