using App.Glsl.SamplerTypes;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static OpenTK.Graphics.OpenGL4.PixelFormat;
using Parameter = OpenTK.Graphics.OpenGL4.GetTextureParameter;

namespace App.Glsl
{
    public class Access : MathFunctions
    {
        #region Texture Access

        #region Helpers
        
        private delegate T MixFunc<T>(T a, T b, float t);

        private static Dictionary<Type, Delegate> Mixers = new Dictionary<Type, Delegate>()
        {
            { typeof(int), (MixFunc<int>)((int a, int b, float t) => { return (int)((1 - t) * a + t * b); }) },
            { typeof(uint), (MixFunc<uint>)((uint a, uint b, float t) => { return (uint)((1 - t) * a + t * b); }) },
            { typeof(float), (MixFunc<float>)((float a, float b, float t) => { return (1 - t) * a + t * b; }) }
        };

        private static PixelType Type2PixelType<T>() where T: struct
        {
            var type = PixelType.Float;
            if (typeof(T) == typeof(int))
                type = PixelType.Int;
            else if (typeof(T) == typeof(uint))
                type = PixelType.UnsignedInt;
            return type;
        }

        private static vec4 texturef(Location location, int sampler, float x, float y, float z, int lod, GetPName binding)
            => new vec4(texture<float>(location, sampler, x, y, z, lod, binding));

        private static ivec4 texturei(Location location, int sampler, float x, float y, float z, int lod, GetPName binding)
            => new ivec4(texture<int>(location, sampler, x, y, z, lod, binding));

        private static uvec4 textureu(Location location, int sampler, float x, float y, float z, int lod, GetPName binding)
            => new uvec4(texture<uint>(location, sampler, x, y, z, lod, binding));

        private static T texture<T>(Location location, int sampler, float x, float y, float z, int lod, GetPName binding)
            where T : struct
        {
            int ID, sID, minFilter, magFilter, w, h, d, wrapR, wrapS, wrapT;

            // get return type
            var type = Type2PixelType<T>();

            // get texture info
            GL.ActiveTexture(TextureUnit.Texture0 + sampler);
            GL.GetInteger(binding, out ID);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureWidth, out w);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureHeight, out h);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureDepth, out d);

            // get sampler info
            GL.GetInteger(GetPName.SamplerBinding, out sID);
            GL.GetSamplerParameter(ID, SamplerParameterName.TextureMinFilter, out minFilter);
            GL.GetSamplerParameter(ID, SamplerParameterName.TextureMagFilter, out magFilter);
            GL.GetSamplerParameter(ID, SamplerParameterName.TextureWrapR, out wrapR);
            GL.GetSamplerParameter(ID, SamplerParameterName.TextureWrapS, out wrapS);
            GL.GetSamplerParameter(ID, SamplerParameterName.TextureWrapT, out wrapT);

            // wrap texture coordinates
            if (x < 0 || 1 < x)
                x = Wrap(x, (TextureWrapMode)wrapR);
            if (y < 0 || 1 < y)
                y = Wrap(y, (TextureWrapMode)wrapS);
            if (z < 0 || 1 < z)
                z = Wrap(z, (TextureWrapMode)wrapT);

            // convert from [0,1] to [0,pixels]
            x *= w - 1;
            y *= h - 1;
            z *= d - 1;
            w = (int)x + 2 >= w ? 1 : 2;
            h = (int)y + 2 >= h ? 1 : 2;
            d = (int)z + 2 >= d ? 1 : 2;

            // get texture data
            var p = new T[w, h, d];
            if (binding == GetPName.TextureBindingBuffer)
                GL.GetNamedBufferSubData(ID, (IntPtr)(Marshal.SizeOf<T>() * x), p.Size(), p);
            else
                GL.GetTextureSubImage(ID, lod, (int)x, (int)y, (int)z, w, h, d, Rgba, type, p.Size(), p);

            // interpolate between pixels
            var mix = (MixFunc<T>)Mixers[typeof(T)];
            if (d > 1)
            {
                z -= (int)z;
                var Z = magFilter == (int)TextureMinFilter.Nearest ? (float)Math.Round(z) : z;
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        p[i, j, 0] = mix(p[i, j, 0], p[i, j, 1], Z);
            }
            if (h > 1)
            {
                y -= (int)y;
                var Y = minFilter == (int)TextureMinFilter.Nearest ? (float)Math.Round(y) : y;
                for (int i = 0; i < w; i++)
                    p[i, 0, 0] = mix(p[i, 0, 0], p[i, 1, 0], Y);
            }
            if (w > 1)
            {
                x -= (int)x;
                var X = minFilter == (int)TextureMinFilter.Nearest ? (float)Math.Round(x) : x;
                p[0, 0, 0] = mix(p[0, 0, 0], p[1, 1, 0], X);
            }

            // trace function
            Shader.TraceFunction(location, p[0, 0, 0], "texture");

            return p[0, 0, 0];
        }

        private static float Wrap(float a, TextureWrapMode mode)
        {
            switch (mode)
            {
                case TextureWrapMode.Repeat:
                    a = (int)Math.Floor(a);
                    if (a < 0)
                        a = 1 - a;
                    break;
                case TextureWrapMode.MirroredRepeat:
                    a = (int)Math.Floor(Math.Abs(a));
                    if ((int)a % 2 == 1)
                        a = 1 - a;
                    break;
                default:
                    a = Math.Min(Math.Max(a, 0), 1);
                    break;
            }
            return a;
        }

        #endregion

        public static vec4 texture(Location location, sampler1D sampler, float P, float bias = 0)
            => texturef(location, sampler.i, P, 0, 0, 0, GetPName.TextureBinding1D);
        public static ivec4 texture(Location location, isampler1D sampler, float P, float bias = 0)
            => texturei(location, sampler.i, P, 0, 0, 0, GetPName.TextureBinding1D);
        public static uvec4 texture(Location location, usampler1D sampler, float P, float bias = 0)
            => textureu(location, sampler.i, P, 0, 0, 0, GetPName.TextureBinding1D);
        public static vec4 texture(Location location, sampler2D sampler, vec2 P, float bias = 0)
            => texturef(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        public static ivec4 texture(Location location, isampler2D sampler, vec2 P, float bias = 0)
            => texturei(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        public static uvec4 texture(Location location, usampler2D sampler, vec2 P, float bias = 0)
            => textureu(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        public static vec4 texture(Location location, sampler3D sampler, vec3 P, float bias = 0)
            => texturef(location, sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        public static ivec4 texture(Location location, isampler3D sampler, vec3 P, float bias = 0)
            => texturei(location, sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        public static uvec4 texture(Location location, usampler3D sampler, vec3 P, float bias = 0)
            => textureu(location, sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        public static vec4 texture(Location location, samplerCube sampler, vec3 P, float bias = 0) 
            => new vec4(0);
        public static ivec4 texture(Location location, isamplerCube sampler, vec3 P, float bias = 0)
            => new ivec4(0);
        public static uvec4 texture(Location location, usamplerCube sampler, vec3 P, float bias = 0)
            => new uvec4(0);
        public static vec4 texture(Location location, sampler1DShadow sampler, vec3 P, float bias = 0)
            => new vec4(0);
        public static vec4 texture(Location location, sampler2DShadow sampler, vec3 P, float bias = 0)
            => new vec4(0);
        public static vec4 texture(Location location, samplerCubeShadow sampler, vec3 P, float bias = 0)
            => new vec4(0);
        public static vec4 texture(Location location, sampler1DArray sampler, vec2 P, float bias = 0)
            => texturef(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        public static ivec4 texture(Location location, isampler1DArray sampler, vec2 P, float bias = 0)
            => texturei(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        public static uvec4 texture(Location location, usampler1DArray sampler, vec2 P, float bias = 0)
            => textureu(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        public static vec4 texture(Location location, sampler2DArray sampler, vec3 P, float bias = 0)
            => texturef(location, sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        public static ivec4 texture(Location location, isampler2DArray sampler, vec3 P, float bias = 0)
            => texturei(location, sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        public static uvec4 texture(Location location, usampler2DArray sampler, vec3 P, float bias = 0)
            => textureu(location, sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        public static vec4 texture(Location location, samplerCubeArray sampler, vec4 P, float bias = 0)
            => new vec4(0);
        public static ivec4 texture(Location location, isamplerCubeArray sampler, vec4 P, float bias = 0)
            => new ivec4(0);
        public static uvec4 texture(Location location, usamplerCubeArray sampler, vec4 P, float bias = 0) 
            => new uvec4(0);
        public static vec4 texture(Location location, sampler1DArrayShadow sampler, vec3 P, float bias = 0)
            => new vec4(0);
        public static vec4 texture(Location location, sampler2DArrayShadow sampler, vec4 P, float bias = 0)
            => new vec4(0);
        public static ivec4 texture(Location location, isampler2DArrayShadow sampler, vec4 P, float bias = 0)
            => new ivec4(0);
        public static uvec4 texture(Location location, usampler2DArrayShadow sampler, vec4 P, float bias = 0)
            => new uvec4(0);
        public static vec4 texture(Location location, sampler2DRect sampler, vec2 P)
            => texturef(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        public static ivec4 texture(Location location, isampler2DRect sampler, vec2 P)
            => texturei(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        public static uvec4 texture(Location location, usampler2DRect sampler, vec2 P)
            => textureu(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        public static vec4 texture(Location location, sampler2DRectShadow sampler, vec3 P)
            => new vec4(0);
        public static vec4 texture(Location location, samplerCubeArrayShadow sampler, vec4 P, float compare)
            => new vec4(0);
        public static ivec4 texture(Location location, isamplerCubeArrayShadow sampler, vec4 P, float compare)
            => new ivec4(0);
        public static uvec4 texture(Location location, usamplerCubeArrayShadow sampler, vec4 P, float compare)
            => new uvec4(0);

        #endregion

        #region Texel Fetch

        #region Helpers

        private static T[] texelFetch<T>(Location location, int sampler, int x, int y, int z, int lod, GetPName binding)
            where T : struct
        {
            int ID, w, h, d;
            var p = new T[4];

            // get return type
            var type = Type2PixelType<T>();

            // get texture ID
            GL.ActiveTexture(TextureUnit.Texture0 + sampler);
            GL.GetInteger(binding, out ID);

            // get texture size
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureWidth, out w);

            // if valid texture coordinate
            if (0 <= x && x < w)
            {
                // if this is a buffer texture
                if (binding == GetPName.TextureBindingBuffer)
                    GL.GetNamedBufferSubData(ID, (IntPtr)(Marshal.SizeOf<T>() * x), p.Size(), p);
                // is a normal texture
                else
                {
                    // get rest of the texture size
                    GL.GetTextureLevelParameter(ID, lod, Parameter.TextureHeight, out h);
                    GL.GetTextureLevelParameter(ID, lod, Parameter.TextureDepth, out d);
                    // if valid texture coordinate
                    if (0 <= y && y < h && 0 <= z && z < d)
                        GL.GetTextureSubImage(ID, lod, x, y, z, 1, 1, 1, Rgba, type, p.Size(), p);

                    DebugGetError(new StackTrace(true));
                }
            }

            return p;
        }

        #endregion

        public static vec4 texelFetch(Location location, sampler1D sampler, int P, int lod)
            => Shader.TraceFunction(location, new vec4(texelFetch<float>(location, sampler.i, P, 0, 0, lod, GetPName.TextureBinding1D)), "texelFetch");
        public static vec4 texelFetch(Location location, sampler2D sampler, ivec2 P, int lod)
            => Shader.TraceFunction(location, new vec4(texelFetch<float>(location, sampler.i, P.x, P.y, 0, lod, GetPName.TextureBinding2D)), "texelFetch");
        public static vec4 texelFetch(Location location, sampler3D sampler, ivec3 P, int lod)
            => Shader.TraceFunction(location, new vec4(texelFetch<float>(location, sampler.i, P.x, P.y, P.z, lod, GetPName.TextureBinding3D)), "texelFetch");
        public static vec4 texelFetch(Location location, sampler2DRect sampler, ivec2 P)
            => Shader.TraceFunction(location, new vec4(texelFetch<float>(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle)), "texelFetch");
        public static vec4 texelFetch(Location location, sampler1DArray sampler, ivec2 P, int lod)
            => Shader.TraceFunction(location, new vec4(texelFetch<float>(location, sampler.i, P.x, P.y, 0, lod, GetPName.TextureBinding1DArray)), "texelFetch");
        public static vec4 texelFetch(Location location, sampler2DArray sampler, ivec3 P, int lod)
            => Shader.TraceFunction(location, new vec4(texelFetch<float>(location, sampler.i, P.x, P.y, P.z, lod, GetPName.TextureBinding2DArray)), "texelFetch");
        public static vec4 texelFetch(Location location, samplerBuffer sampler, int P)
            => Shader.TraceFunction(location, new vec4(texelFetch<float>(location, sampler.i, P, 0, 0, 0, GetPName.TextureBindingBuffer)), "texelFetch");
        public static vec4 texelFetch(Location location, sampler2DMS sampler, ivec2 P, int sample)
            => new vec4(0);
        public static vec4 texelFetch(Location location, sampler2DMSArray sampler, ivec3 P, int sample)
            => new vec4(0);
        public static ivec4 texelFetch(Location location, isampler1D sampler, int P, int lod)
            => Shader.TraceFunction(location, new ivec4(texelFetch<int>(location, sampler.i, P, 0, 0, lod, GetPName.TextureBinding1D)), "texelFetch");
        public static ivec4 texelFetch(Location location, isampler2D sampler, ivec2 P, int lod)
            => Shader.TraceFunction(location, new ivec4(texelFetch<int>(location, sampler.i, P.x, P.y, 0, lod, GetPName.TextureBinding2D)), "texelFetch");
        public static ivec4 texelFetch(Location location, isampler3D sampler, ivec3 P, int lod)
            => Shader.TraceFunction(location, new ivec4(texelFetch<int>(location, sampler.i, P.x, P.y, P.z, lod, GetPName.TextureBinding3D)), "texelFetch");
        public static ivec4 texelFetch(Location location, isampler2DRect sampler, ivec2 P)
            => Shader.TraceFunction(location, new ivec4(texelFetch<int>(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle)), "texelFetch");
        public static ivec4 texelFetch(Location location, isampler1DArray sampler, ivec2 P, int lod)
            => Shader.TraceFunction(location, new ivec4(texelFetch<int>(location, sampler.i, P.x, P.y, 0, lod, GetPName.TextureBinding1DArray)), "texelFetch");
        public static ivec4 texelFetch(Location location, isampler2DArray sampler, ivec3 P, int lod)
            => Shader.TraceFunction(location, new ivec4(texelFetch<int>(location, sampler.i, P.x, P.y, P.z, lod, GetPName.TextureBinding2DArray)), "texelFetch");
        public static ivec4 texelFetch(Location location, isamplerBuffer sampler, int P)
            => Shader.TraceFunction(location, new ivec4(texelFetch<int>(location, sampler.i, P, 0, 0, 0, GetPName.TextureBindingBuffer)), "texelFetch");
        public static ivec4 texelFetch(Location location, isampler2DMS sampler, ivec2 P, int sample)
            => new ivec4(0);
        public static ivec4 texelFetch(Location location, isampler2DMSArray sampler, ivec3 P, int sample)
            => new ivec4(0);
        public static uvec4 texelFetch(Location location, usampler1D sampler, int P, int lod)
            => Shader.TraceFunction(location, new uvec4(texelFetch<uint>(location, sampler.i, P, 0, 0, lod, GetPName.TextureBinding1D)), "texelFetch");
        public static uvec4 texelFetch(Location location, usampler2D sampler, ivec2 P, int lod)
            => Shader.TraceFunction(location, new uvec4(texelFetch<uint>(location, sampler.i, P.x, P.y, 0, lod, GetPName.TextureBinding2D)), "texelFetch");
        public static uvec4 texelFetch(Location location, usampler3D sampler, ivec3 P, int lod)
            => Shader.TraceFunction(location, new uvec4(texelFetch<uint>(location, sampler.i, P.x, P.y, P.z, lod, GetPName.TextureBinding3D)), "texelFetch");
        public static uvec4 texelFetch(Location location, usampler2DRect sampler, ivec2 P)
            => Shader.TraceFunction(location, new uvec4(texelFetch<uint>(location, sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle)), "texelFetch");
        public static uvec4 texelFetch(Location location, usampler1DArray sampler, ivec2 P, int lod)
            => Shader.TraceFunction(location, new uvec4(texelFetch<uint>(location, sampler.i, P.x, P.y, 0, lod, GetPName.TextureBinding1DArray)), "texelFetch");
        public static uvec4 texelFetch(Location location, usampler2DArray sampler, ivec3 P, int lod)
            => Shader.TraceFunction(location, new uvec4(texelFetch<uint>(location, sampler.i, P.x, P.y, P.z, lod, GetPName.TextureBinding2DArray)), "texelFetch");
        public static uvec4 texelFetch(Location location, usamplerBuffer sampler, int P)
            => Shader.TraceFunction(location, new uvec4(texelFetch<uint>(location, sampler.i, P, 0, 0, 0, GetPName.TextureBindingBuffer)), "texelFetch");
        public static uvec4 texelFetch(Location location, usampler2DMS sampler, ivec2 P, int sample)
            => new uvec4(0);
        public static uvec4 texelFetch(Location location, usampler2DMSArray sampler, ivec3 P, int sample)
            => new uvec4(0);

        #endregion

        #region Uniform Access

        internal static readonly Type[] BaseIntTypes = new[] { typeof(bool), typeof(int), typeof(uint) };
        internal static readonly Type[] BaseFloatTypes = new[] { typeof(float), typeof(double) };
        internal static readonly Type[] BaseTypes = BaseIntTypes.Concat(BaseFloatTypes).ToArray();

        protected static object GetUniform(string uniformName, Type uniformType, ProgramPipelineParameter shader)
        {
            int unit, glbuf, type, size, length, offset, stride;
            int[] locations = new int[1];

            // get current shader program pipeline
            var pipeline = GL.GetInteger(GetPName.ProgramPipelineBinding);
            if (pipeline <= 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get vertex shader
            int program;
            GL.GetProgramPipeline(pipeline, shader, out program);
            if (program <= 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get uniform buffer object block index
            int block = GL.GetUniformBlockIndex(program, uniformName.Substring(0, uniformName.IndexOf('.')));
            if (block < 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get bound buffer object
            GL.GetActiveUniformBlock(program, block, ActiveUniformBlockParameter.UniformBlockBinding, out unit);
            GL.GetInteger(GetIndexedPName.UniformBufferBinding, unit, out glbuf);
            if (glbuf <= 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get uniform indices in uniform block
            GL.GetUniformIndices(program, 1, new[] { uniformName }, locations);
            var location = locations[0];
            if (location < 0)
                return DebugGetError(uniformType, new StackTrace(true));

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
            var array = new byte[Math.Max(stride, size) * length];
            var src = GL.MapNamedBufferRange(glbuf, (IntPtr)offset, array.Length, BufferAccessMask.MapReadBit);
            Marshal.Copy(src, array, 0, array.Length);
            GL.UnmapNamedBuffer(glbuf);

            DebugGetError(new StackTrace(true));

            // if the return type is an array
            if (uniformType.IsArray && BaseTypes.Any(x => x == uniformType.GetElementType()))
                return array.To(uniformType.GetElementType());

            // if the return type is a base type
            if (BaseTypes.Any(x => x == uniformType))
                return array.To(uniformType).GetValue(0);

            // create new object from byte array
            return uniformType.GetConstructor(new[] { typeof(byte[]) })?.Invoke(new[] { array });
        }

        public static object DebugGetError(Type type, StackTrace trace = null)
        {
            DebugGetError(trace);
            return type ?? Activator.CreateInstance(type);
        }

        public static ErrorCode DebugGetError(StackTrace trace = null)
        {
            var error = GL.GetError();
            var frame = trace?.GetFrame(0);
            if (error != ErrorCode.NoError)
                Debug.Print("[" + frame.GetFileName()
                    + "(" + frame.GetFileLineNumber()
                    + ")] OpenGL error: " + error.ToString()
                    + " occurred during shader debugging.");
            return error;
        }

        #endregion
    }
}
