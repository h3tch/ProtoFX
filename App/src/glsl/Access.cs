﻿using App.Glsl.SamplerTypes;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace App.Glsl
{
    public class Access : MathFunctions
    {
        #region Texture Access

        public static vec4 texture(sampler1D sampler, float P, float bias = 0)
            => texturef(sampler.i, P, 0, 0, 0, GetPName.TextureBinding1D);
        public static ivec4 texture(isampler1D sampler, float P, float bias = 0)
            => texturei(sampler.i, P, 0, 0, 0, GetPName.TextureBinding1D);
        public static uvec4 texture(usampler1D sampler, float P, float bias = 0)
            => textureu(sampler.i, P, 0, 0, 0, GetPName.TextureBinding1D);
        public static vec4 texture(sampler2D sampler, vec2 P, float bias = 0)
            => texturef(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        public static ivec4 texture(isampler2D sampler, vec2 P, float bias = 0)
            => texturei(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        public static uvec4 texture(usampler2D sampler, vec2 P, float bias = 0)
            => textureu(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        public static vec4 texture(sampler3D sampler, vec3 P, float bias = 0)
            => texturef(sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        public static ivec4 texture(isampler3D sampler, vec3 P, float bias = 0)
            => texturei(sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        public static uvec4 texture(usampler3D sampler, vec3 P, float bias = 0)
            => textureu(sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        public static vec4 texture(samplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static ivec4 texture(isamplerCube sampler, vec3 P, float bias = 0) { return new ivec4(0); }
        public static uvec4 texture(usamplerCube sampler, vec3 P, float bias = 0) { return new uvec4(0); }
        public static vec4 texture(sampler1DShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCubeShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DArray sampler, vec2 P, float bias = 0)
            => texturef(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        public static ivec4 texture(isampler1DArray sampler, vec2 P, float bias = 0)
            => texturei(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        public static uvec4 texture(usampler1DArray sampler, vec2 P, float bias = 0)
            => textureu(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        public static vec4 texture(sampler2DArray sampler, vec3 P, float bias = 0)
            => texturef(sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        public static ivec4 texture(isampler2DArray sampler, vec3 P, float bias = 0)
            => texturei(sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        public static uvec4 texture(usampler2DArray sampler, vec3 P, float bias = 0)
            => textureu(sampler.i, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        public static vec4 texture(samplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static ivec4 texture(isamplerCubeArray sampler, vec4 P, float bias = 0) { return new ivec4(0); }
        public static uvec4 texture(usamplerCubeArray sampler, vec4 P, float bias = 0) { return new uvec4(0); }
        public static vec4 texture(sampler1DArrayShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static ivec4 texture(isampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new ivec4(0); }
        public static uvec4 texture(usampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new uvec4(0); }
        public static vec4 texture(sampler2DRect sampler, vec2 P)
            => texturef(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        public static ivec4 texture(isampler2DRect sampler, vec2 P)
            => texturei(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        public static uvec4 texture(usampler2DRect sampler, vec2 P)
            => textureu(sampler.i, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        public static vec4 texture(sampler2DRectShadow sampler, vec3 P) { return new vec4(0); }
        public static vec4 texture(samplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }
        public static ivec4 texture(isamplerCubeArrayShadow sampler, vec4 P, float compare) { return new ivec4(0); }
        public static uvec4 texture(usamplerCubeArrayShadow sampler, vec4 P, float compare) { return new uvec4(0); }

        private static vec4 texturef(int sampler, float x, float y, float z, int lod, GetPName binding)
            => new vec4(texture<float>(sampler, x, y, z, lod, binding, PixelType.Float));
        private static ivec4 texturei(int sampler, float x, float y, float z, int lod, GetPName binding)
            => new ivec4(texture<int>(sampler, x, y, z, lod, binding, PixelType.Int));
        private static uvec4 textureu(int sampler, float x, float y, float z, int lod, GetPName binding)
            => new uvec4(texture<uint>(sampler, x, y, z, lod, binding, PixelType.UnsignedInt));

        private static T[] texture<T>(int sampler, float x, float y, float z, int lod, 
            GetPName binding, PixelType type)
            where T : struct
        {
            int ID, sID, minFilter, magFilter, w, h, d, wrapR, wrapS, wrapT;
            var p = new T[4*8];
            
            // get texture info
            GL.ActiveTexture(TextureUnit.Texture0 + sampler);
            GL.GetInteger(binding, out ID);
            GL.GetTextureLevelParameter(ID, lod, GetTextureParameter.TextureWidth, out w);
            GL.GetTextureLevelParameter(ID, lod, GetTextureParameter.TextureHeight, out h);
            GL.GetTextureLevelParameter(ID, lod, GetTextureParameter.TextureDepth, out d);

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
            GL.GetTextureSubImage(ID, lod, (int)x, (int)y, (int)z, w, h, d,
                PixelFormat.Rgba, type, p.Length, p);

            // get fraction from coordinates
            x -= (int)x;
            y -= (int)y;
            z -= (int)z;

            // interpolate between pixels
            if ((z > 0 ? minFilter : magFilter) != (int)TextureMinFilter.Nearest)
            {
                var mix = (MixFunc<T>)Mixers[typeof(T)];
                if (d > 1)
                {
                    for (int i = 0; i < w; i++)
                        for (int j = 0; j < h; j++)
                            mix(p, i, j, 0, i, j, 1, w, h, d, z);
                }

                if (h > 1)
                {
                    for (int i = 0; i < w; i++)
                        mix(p, i, 0, 0, i, 1, 0, w, h, d, y);
                }

                if (w > 1)
                    mix(p, 0, 0, 0, 1, 0, 0, w, h, d, x);
            }
            // get nearest pixel
            else
            {
                int X = (int)Math.Round(x);
                int Y = (int)Math.Round(y);
                int Z = (int)Math.Round(z);
                p[0] = p[Ptr(w, h, X, Y, Z, 0)];
                p[1] = p[Ptr(w, h, X, Y, Z, 1)];
                p[2] = p[Ptr(w, h, X, Y, Z, 2)];
                p[3] = p[Ptr(w, h, X, Y, Z, 3)];
            }

            return p;
        }

        static float Wrap(float a, TextureWrapMode mode)
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

        static int Ptr(int w, int h, int x, int y, int z, int c) => ((z * h + y) * w + x) * 4 + c;

        delegate void MixFunc<T>(T[] p, int x0, int y0, int z0, int x1, int y1, int z1, int w, int h, int d, float t);

        private static Dictionary<Type, Delegate> Mixers = new Dictionary<Type, Delegate>()
        {
            {
                typeof(int), (MixFunc<int>)
                ((int[] p, int x0, int y0, int z0, int x1, int y1, int z1, int w, int h, int d, float t) => {
                    p[Ptr(w, h, x0, y0, z0, 0)] = (int)((1 - t) * p[Ptr(w, h, x0, y0, z0, 0)] + t * p[Ptr(w, h, x1, y1, z1, 0)]);
                    p[Ptr(w, h, x0, y0, z0, 1)] = (int)((1 - t) * p[Ptr(w, h, x0, y0, z0, 1)] + t * p[Ptr(w, h, x1, y1, z1, 1)]);
                    p[Ptr(w, h, x0, y0, z0, 2)] = (int)((1 - t) * p[Ptr(w, h, x0, y0, z0, 2)] + t * p[Ptr(w, h, x1, y1, z1, 2)]);
                    p[Ptr(w, h, x0, y0, z0, 3)] = (int)((1 - t) * p[Ptr(w, h, x0, y0, z0, 3)] + t * p[Ptr(w, h, x1, y1, z1, 3)]);
                })
            },
            {
                typeof(uint), (MixFunc<uint>)
                ((uint[] p, int x0, int y0, int z0, int x1, int y1, int z1, int w, int h, int d, float t) => {
                    p[Ptr(w, h, x0, y0, z0, 0)] = (uint)((1 - t) * p[Ptr(w, h, x0, y0, z0, 0)] + t * p[Ptr(w, h, x1, y1, z1, 0)]);
                    p[Ptr(w, h, x0, y0, z0, 1)] = (uint)((1 - t) * p[Ptr(w, h, x0, y0, z0, 1)] + t * p[Ptr(w, h, x1, y1, z1, 1)]);
                    p[Ptr(w, h, x0, y0, z0, 2)] = (uint)((1 - t) * p[Ptr(w, h, x0, y0, z0, 2)] + t * p[Ptr(w, h, x1, y1, z1, 2)]);
                    p[Ptr(w, h, x0, y0, z0, 3)] = (uint)((1 - t) * p[Ptr(w, h, x0, y0, z0, 3)] + t * p[Ptr(w, h, x1, y1, z1, 3)]);
                })
            },
            {
                typeof(float), (MixFunc<float>)
                ((float[] p, int x0, int y0, int z0, int x1, int y1, int z1, int w, int h, int d, float t) => {
                    p[Ptr(w, h, x0, y0, z0, 0)] = (1 - t) * p[Ptr(w, h, x0, y0, z0, 0)] + t * p[Ptr(w, h, x1, y1, z1, 0)];
                    p[Ptr(w, h, x0, y0, z0, 1)] = (1 - t) * p[Ptr(w, h, x0, y0, z0, 1)] + t * p[Ptr(w, h, x1, y1, z1, 1)];
                    p[Ptr(w, h, x0, y0, z0, 2)] = (1 - t) * p[Ptr(w, h, x0, y0, z0, 2)] + t * p[Ptr(w, h, x1, y1, z1, 2)];
                    p[Ptr(w, h, x0, y0, z0, 3)] = (1 - t) * p[Ptr(w, h, x0, y0, z0, 3)] + t * p[Ptr(w, h, x1, y1, z1, 3)];
                })
            },
        };

        #endregion

        #region Texel Fetch

        public static vec4 texelFetch(sampler1D sampler, int P, int lod)
            => new vec4(texelFetch<float>(sampler.i, P, 0, 0, lod,
                GetPName.TextureBinding1D, PixelType.Float));
        public static vec4 texelFetch(sampler2D sampler, ivec2 P, int lod)
            => new vec4(texelFetch<float>(sampler.i, P.x, P.y, 0, lod,
                GetPName.TextureBinding2D, PixelType.Float));
        public static vec4 texelFetch(sampler3D sampler, ivec3 P, int lod)
            => new vec4(texelFetch<float>(sampler.i, P.x, P.y, P.z, lod,
                GetPName.TextureBinding3D, PixelType.Float));
        public static vec4 texelFetch(sampler2DRect sampler, ivec2 P)
            => new vec4(texelFetch<float>(sampler.i, P.x, P.y, 0, 0,
                GetPName.TextureBindingRectangle, PixelType.Float));
        public static vec4 texelFetch(sampler1DArray sampler, ivec2 P, int lod)
            => new vec4(texelFetch<float>(sampler.i, P.x, P.y, 0, lod,
                GetPName.TextureBinding1DArray, PixelType.Float));
        public static vec4 texelFetch(sampler2DArray sampler, ivec3 P, int lod)
            => new vec4(texelFetch<float>(sampler.i, P.x, P.y, P.z, lod,
                GetPName.TextureBinding2DArray, PixelType.Float));
        public static vec4 texelFetch(samplerBuffer sampler, int P)
            => new vec4(texelFetch<float>(sampler.i, P, 0, 0, 0,
                GetPName.TextureBindingBuffer, PixelType.Float));
        public static vec4 texelFetch(sampler2DMS sampler, ivec2 P, int sample) { return new vec4(0); }
        public static vec4 texelFetch(sampler2DMSArray sampler, ivec3 P, int sample) { return new vec4(0); }


        public static ivec4 texelFetch(isampler1D sampler, int P, int lod)
            => new ivec4(texelFetch<int>(sampler.i, P, 0, 0, lod,
                GetPName.TextureBinding1D, PixelType.Int));
        public static ivec4 texelFetch(isampler2D sampler, ivec2 P, int lod)
            => new ivec4(texelFetch<int>(sampler.i, P.x, P.y, 0, lod,
                GetPName.TextureBinding2D, PixelType.Int));
        public static ivec4 texelFetch(isampler3D sampler, ivec3 P, int lod)
            => new ivec4(texelFetch<int>(sampler.i, P.x, P.y, P.z, lod,
                GetPName.TextureBinding3D, PixelType.Int));
        public static ivec4 texelFetch(isampler2DRect sampler, ivec2 P)
            => new ivec4(texelFetch<int>(sampler.i, P.x, P.y, 0, 0,
                GetPName.TextureBindingRectangle, PixelType.Int));
        public static ivec4 texelFetch(isampler1DArray sampler, ivec2 P, int lod)
            => new ivec4(texelFetch<int>(sampler.i, P.x, P.y, 0, lod,
                GetPName.TextureBinding1DArray, PixelType.Int));
        public static ivec4 texelFetch(isampler2DArray sampler, ivec3 P, int lod)
            => new ivec4(texelFetch<int>(sampler.i, P.x, P.y, P.z, lod,
                GetPName.TextureBinding2DArray, PixelType.Int));
        public static ivec4 texelFetch(isamplerBuffer sampler, int P)
            => new ivec4(texelFetch<int>(sampler.i, P, 0, 0, 0,
                GetPName.TextureBindingBuffer, PixelType.Int));
        public static ivec4 texelFetch(isampler2DMS sampler, ivec2 P, int sample) { return new ivec4(0); }
        public static ivec4 texelFetch(isampler2DMSArray sampler, ivec3 P, int sample) { return new ivec4(0); }


        public static uvec4 texelFetch(usampler1D sampler, int P, int lod)
            => new uvec4(texelFetch<uint>(sampler.i, P, 0, 0, lod,
                GetPName.TextureBinding1D, PixelType.UnsignedInt));
        public static uvec4 texelFetch(usampler2D sampler, ivec2 P, int lod)
            => new uvec4(texelFetch<uint>(sampler.i, P.x, P.y, 0, lod,
                GetPName.TextureBinding2D, PixelType.UnsignedInt));
        public static uvec4 texelFetch(usampler3D sampler, ivec3 P, int lod)
            => new uvec4(texelFetch<uint>(sampler.i, P.x, P.y, P.z, lod,
                GetPName.TextureBinding3D, PixelType.UnsignedInt));
        public static uvec4 texelFetch(usampler2DRect sampler, ivec2 P)
            => new uvec4(texelFetch<uint>(sampler.i, P.x, P.y, 0, 0,
                GetPName.TextureBindingRectangle, PixelType.UnsignedInt));
        public static uvec4 texelFetch(usampler1DArray sampler, ivec2 P, int lod)
            => new uvec4(texelFetch<uint>(sampler.i, P.x, P.y, 0, lod,
                GetPName.TextureBinding1DArray, PixelType.UnsignedInt));
        public static uvec4 texelFetch(usampler2DArray sampler, ivec3 P, int lod)
            => new uvec4(texelFetch<uint>(sampler.i, P.x, P.y, P.z, lod,
                GetPName.TextureBinding2DArray, PixelType.UnsignedInt));
        public static uvec4 texelFetch(usamplerBuffer sampler, int P)
            => new uvec4(texelFetch<uint>(sampler.i, P, 0, 0, 0,
                GetPName.TextureBindingBuffer, PixelType.UnsignedInt));
        public static uvec4 texelFetch(usampler2DMS sampler, ivec2 P, int sample) { return new uvec4(0); }
        public static uvec4 texelFetch(usampler2DMSArray sampler, ivec3 P, int sample) { return new uvec4(0); }

        private static T[] texelFetch<T>(int sampler, int x, int y, int z, int lod, GetPName binding, PixelType type)
            where T : struct
        {
            int ID, w, h, d;
            var data = new T[4];
            GL.ActiveTexture(TextureUnit.Texture0 + sampler);
            GL.GetInteger(binding, out ID);
            GL.GetTextureLevelParameter(ID, lod, GetTextureParameter.TextureWidth, out w);
            GL.GetTextureLevelParameter(ID, lod, GetTextureParameter.TextureHeight, out h);
            GL.GetTextureLevelParameter(ID, lod, GetTextureParameter.TextureDepth, out d);
            if (0 <= x && x < w && 0 <= y && y < h && 0 <= z && z < d)
                GL.GetTextureSubImage(ID, lod, x, y, z, 1, 1, 1, PixelFormat.Rgba, type, data.Length, data);
            return data;
        }

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
