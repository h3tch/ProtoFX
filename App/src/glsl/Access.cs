using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace App.Glsl
{
    public class Access : MathFunctions
    {
        #region Texture Access

        public static vec4 texture(sampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isamplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usamplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCubeShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isamplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usamplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DArrayShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(isampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(usampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(sampler2DRectShadow sampler, vec3 P) { return new vec4(0); }
        public static vec4 texture(samplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }
        public static vec4 texture(isamplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }
        public static vec4 texture(usamplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }

        #endregion

        #region Uniform Access

        internal static readonly Type[] BaseIntTypes = new[] { typeof(bool), typeof(int), typeof(uint) };
        internal static readonly Type[] BaseFloatTypes = new[] { typeof(float), typeof(double) };
        internal static readonly Type[] BaseTypes = BaseIntTypes.Concat(BaseFloatTypes).ToArray();

        protected static object GetUniform<T>(string uniformName, ProgramPipelineParameter shader)
        {
            int unit, glbuf, type, size, length, offset, stride;
            int[] locations = new int[1];

            // get current shader program pipeline
            var pipeline = GL.GetInteger(GetPName.ProgramPipelineBinding);
            if (pipeline <= 0)
                return default(T);

            // get vertex shader
            int program;
            GL.GetProgramPipeline(pipeline, shader, out program);
            if (program <= 0)
                return default(T);

            // get uniform buffer object block index
            int block = GL.GetUniformBlockIndex(program, uniformName.Substring(0, uniformName.IndexOf('.')));
            if (block < 0)
                return default(T);

            // get bound buffer object
            GL.GetActiveUniformBlock(program, block, ActiveUniformBlockParameter.UniformBlockBinding, out unit);
            GL.GetInteger(GetIndexedPName.UniformBufferBinding, unit, out glbuf);
            if (glbuf <= 0)
                return default(T);

            // get uniform indices in uniform block
            GL.GetUniformIndices(program, 1, new[] { uniformName }, locations);
            var location = locations[0];
            if (location < 0)
                return default(T);

            // get uniform information
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformType, out type);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformSize, out length);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformOffset, out offset);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformArrayStride, out stride);

            // get size of the uniform type
            switch ((All)type)
            {
                case All.IntVec2:
                case All.UnsignedIntVec2:
                case All.FloatVec2:
                case All.Double:
                    size = 8;
                    break;
                case All.IntVec3:
                case All.UnsignedIntVec3:
                case All.FloatVec3:
                    size = 12;
                    break;
                case All.IntVec4:
                case All.UnsignedIntVec4:
                case All.FloatVec4:
                case All.DoubleVec2:
                case All.FloatMat2:
                    size = 16;
                    break;
                case All.DoubleVec3:
                    size = 24;
                    break;
                case All.DoubleVec4:
                    size = 32;
                    break;
                case All.FloatMat3:
                    size = 36;
                    break;
                case All.FloatMat4:
                    size = 64;
                    break;
                default:
                    size = 4;
                    break;
            }

            // read uniform buffer data
            byte[] array = new byte[Math.Max(stride, size) * length];
            var src = GL.MapNamedBufferRange(glbuf, (IntPtr)offset, array.Length, BufferAccessMask.MapReadBit);
            Marshal.Copy(src, array, 0, array.Length);
            GL.UnmapNamedBuffer(glbuf);

            // if the return type is an array
            if (typeof(T).IsArray && BaseTypes.Any(x => x == typeof(T).GetElementType()))
                return array.To(typeof(T).GetElementType());

            // if the return type is a base type
            if (BaseTypes.Any(x => x == typeof(T)))
                return array.To(typeof(T)).GetValue(0);

            // create new object from byte array
            return typeof(T).GetConstructor(new[] { typeof(byte[]) })?.Invoke(new[] { array });
        }

        #endregion
    }
}
