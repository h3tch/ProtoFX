using App.Glsl.SamplerTypes;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static OpenTK.Graphics.OpenGL4.PixelFormat;
using Parameter = OpenTK.Graphics.OpenGL4.GetTextureParameter;

#pragma warning disable IDE1006

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
            { typeof(float), (MixFunc<float>)((float a, float b, float t) => { return (1 - t) * a + t * b; }) },
            { typeof(ivec4), (MixFunc<ivec4>)((ivec4 a, ivec4 b, float t) => { return ivec4((1 - t) * a + t * b); }) },
            { typeof(uvec4), (MixFunc<uvec4>)((uvec4 a, uvec4 b, float t) => { return uvec4((1 - t) * a + t * b); }) },
            { typeof(vec4), (MixFunc<vec4>)((vec4 a, vec4 b, float t) => { return (1 - t) * a + t * b; }) },
        };

        private static PixelType Type2PixelType<T>() where T: struct
        {
            var type = PixelType.Float;
            if (new[] { typeof(int), typeof(ivec2), typeof(ivec3), typeof(ivec4) }.Any(x => x == typeof(T)))
                type = PixelType.Int;
            else if (new[] { typeof(uint), typeof(uvec2), typeof(uvec3), typeof(uvec4) }.Any(x => x == typeof(T)))
                type = PixelType.UnsignedInt;
            return type;
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

        public static (int layer, vec2 uv) Vector2Cubemap(vec3 P)
        {
            var absP = abs(P);
            const int XFACE = 0;
            const int YFACE = 1;
            const int ZFACE = 2;

            const int POSITIVE_X = 0;
            const int NEGATIVE_X = 1;
            const int POSITIVE_Y = 2;
            const int NEGATIVE_Y = 3;
            const int POSITIVE_Z = 4;
            const int NEGATIVE_Z = 5;

            switch (absP.x > absP.y
                ? absP.x > absP.z ? XFACE : ZFACE
                : absP.y > absP.z ? YFACE : ZFACE)
            {
                case XFACE:
                    if (P.x < 0)
                    {
                        var u = (absP.x - P.z) / (2 * absP.x);
                        var v = (absP.x + P.y) / (2 * absP.x);
                        return (NEGATIVE_X, vec2(u, v));
                    }
                    else
                    {
                        var u = (absP.x + P.z) / (2 * absP.x);
                        var v = (absP.x + P.y) / (2 * absP.x);
                        return (POSITIVE_X, vec2(u, v));
                    }
                case YFACE:
                    if (P.y < 0)
                    {
                        var u = (absP.y + P.x) / (2 * absP.y);
                        var v = (absP.y - P.z) / (2 * absP.y);
                        return (NEGATIVE_Y, vec2(u, v));
                    }
                    else
                    {
                        var u = (absP.y + P.x) / (2 * absP.y);
                        var v = (absP.y + P.z) / (2 * absP.y);
                        return (POSITIVE_Y, vec2(u, v));
                    }
                case ZFACE:
                    if (P.z < 0)
                    {
                        var u = (absP.z + P.x) / (2 * absP.z);
                        var v = (absP.z + P.y) / (2 * absP.z);
                        return (NEGATIVE_Z, vec2(u, v));
                    }
                    else
                    {
                        var u = (absP.z - P.x) / (2 * absP.z);
                        var v = (absP.z + P.y) / (2 * absP.z);
                        return (POSITIVE_Z, vec2(u, v));
                    }
            }

            return (POSITIVE_Z, vec2(0, 0));
        }

        private static vec4 texturef(Location location, int sampler, float x, float y, object z, int lod, GetPName binding)
        {
            return new vec4(texture<float>(location, sampler, x, y, z, lod, binding));
        }

        private static ivec4 texturei(Location location, int sampler, float x, float y, object z, int lod, GetPName binding)
        {
            return new ivec4(texture<int>(location, sampler, x, y, z, lod, binding));
        }

        private static uvec4 textureu(Location location, int sampler, float x, float y, object z, int lod, GetPName binding)
        {
            return new uvec4(texture<uint>(location, sampler, x, y, z, lod, binding));
        }

        private static T[] texture<T>(Location location, int sampler, float x, float y, object _z, int lod, GetPName binding)
            where T : struct
        {
            // get return type
            var type = Type2PixelType<T>();

            // get texture info
            GL.ActiveTexture(TextureUnit.Texture0 + sampler);
            GL.GetInteger(binding, out int ID);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureWidth, out int texw);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureHeight, out int texh);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureDepth, out int texd);
            GL.GetTextureParameter(ID, Parameter.TextureMinFilter, out int minFilter);
            GL.GetTextureParameter(ID, Parameter.TextureMagFilter, out int magFilter);
            GL.GetTextureParameter(ID, Parameter.TextureWrapR, out int wrapR);
            GL.GetTextureParameter(ID, Parameter.TextureWrapS, out int wrapS);
            GL.GetTextureParameter(ID, Parameter.TextureWrapT, out int wrapT);

            // get sampler info
            GL.GetInteger(GetPName.SamplerBinding, out int sID);
            if (sID > 0)
            {
                GL.GetSamplerParameter(sID, SamplerParameterName.TextureMinFilter, out minFilter);
                GL.GetSamplerParameter(sID, SamplerParameterName.TextureMagFilter, out magFilter);
                GL.GetSamplerParameter(sID, SamplerParameterName.TextureWrapR, out wrapR);
                GL.GetSamplerParameter(sID, SamplerParameterName.TextureWrapS, out wrapS);
                GL.GetSamplerParameter(sID, SamplerParameterName.TextureWrapT, out wrapT);
            }

            // wrap texture coordinates
            if (x < 0 || 1 < x)
                x = Wrap(x, (TextureWrapMode)wrapR);
            if (y < 0 || 1 < y)
                y = Wrap(y, (TextureWrapMode)wrapS);

            // convert from [0,1] to [0,pixels]
            x *= texw - 1;
            y *= texh - 1;
            float z = 0;
            switch (_z)
            {
                case int i: z = i; break;
                case uint i: z = i; break;
                case float i: z = i; break;
                default: throw new NotImplementedException();
            }

            // how much pixel should be reserved for interpolation
            var w = (int)x + 2 >= texw ? 1 : 2;
            var h = (int)y + 2 >= texh ? 1 : 2;
            var d = 1;

            // if z is not the layer (not int but float)
            if (_z is float f)
            {
                if (f < 0 || 1 < f)
                    f = Wrap(f, (TextureWrapMode)wrapT);
                z = f * (d - 1);
                d = (int)f + 2 >= d ? 1 : 2;
            }

            // get texture data
            var p = (T[,,,])regionFetch<T>(sampler, (int)x, (int)y, (int)z, w, h, d, lod,
                                           binding)
                .Reshape(d, h, w, 4);

            // interpolate between pixels
            var mix = (MixFunc<T>)Mixers[typeof(T)];
            if (d > 1)
            {
                z -= (int)z;
                var Z = magFilter == (int)TextureMinFilter.Nearest ? (float)Math.Round(z) : z;
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        for (int k = 0; k < 4; k++)
                            p[0, j, i, k] = mix(p[0, j, i, k], p[1, j, i, k], Z);
            }
            if (h > 1)
            {
                y -= (int)y;
                var Y = minFilter == (int)TextureMinFilter.Nearest ? (float)Math.Round(y) : y;
                for (int i = 0; i < w; i++)
                    for (int k = 0; k < 4; k++)
                        p[0, 0, i, k] = mix(p[0, 0, i, k], p[0, 1, i, k], Y);
            }
            if (w > 1)
            {
                x -= (int)x;
                var X = minFilter == (int)TextureMinFilter.Nearest ? (float)Math.Round(x) : x;
                for (int k = 0; k < 4; k++)
                    p[0, 0, 0, k] = mix(p[0, 0, 0, k], p[0, 1, 1, k], X);
            }

            // get result
            var rs = new T[4];
            for (int k = 0; k < 4; k++)
                rs[k] = p[0, 0, 0, k];
            
            // trace function
            Shader.TraceFunction(location, rs, "texture");

            return rs;
        }

        public static T[] textureCube<T>(Location location, int sampler, vec3 P, int index, int lod, GetPName binding)
            where T : struct
        {
            var (layer, uv) = Vector2Cubemap(P);
            if ((All)binding == All.TextureCubeMapArray)
                layer += index * 6;
            return texture<T>(location, sampler, uv.x, uv.y, layer, lod, binding);
        }

        #endregion

        public static vec4 texture(Location location, sampler1D sampler, float P, float bias = 0)
        {
            return texturef(location, sampler, P, 0, 0, 0, GetPName.TextureBinding1D);
        }

        public static ivec4 texture(Location location, isampler1D sampler, float P, float bias = 0)
        {
            return texturei(location, sampler, P, 0, 0, 0, GetPName.TextureBinding1D);
        }

        public static uvec4 texture(Location location, usampler1D sampler, float P, float bias = 0)
        {
            return textureu(location, sampler, P, 0, 0, 0, GetPName.TextureBinding1D);
        }

        public static vec4 texture(Location location, sampler2D sampler, vec2 P, float bias = 0)
        {
            return texturef(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        }

        public static ivec4 texture(Location location, isampler2D sampler, vec2 P, float bias = 0)
        {
            return texturei(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        }

        public static uvec4 texture(Location location, usampler2D sampler, vec2 P, float bias = 0)
        {
            return textureu(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBinding2D);
        }

        public static vec4 texture(Location location, sampler3D sampler, vec3 P, float bias = 0)
        {
            return texturef(location, sampler, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        }

        public static ivec4 texture(Location location, isampler3D sampler, vec3 P, float bias = 0)
        {
            return texturei(location, sampler, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        }

        public static uvec4 texture(Location location, usampler3D sampler, vec3 P, float bias = 0)
        {
            return textureu(location, sampler, P.x, P.y, P.z, 0, GetPName.TextureBinding3D);
        }

        public static vec4 texture(Location location, samplerCube sampler, vec3 P, float bias = 0)
        {
            return new vec4(textureCube<float>(location, sampler, P, 0, 0, GetPName.TextureCubeMap));
        }

        public static ivec4 texture(Location location, isamplerCube sampler, vec3 P, float bias = 0)
        {
            return new ivec4(textureCube<int>(location, sampler, P, 0, 0, GetPName.TextureCubeMap));
        }

        public static uvec4 texture(Location location, usamplerCube sampler, vec3 P, float bias = 0)
        {
            return new uvec4(textureCube<uint>(location, sampler, P, 0, 0, GetPName.TextureCubeMap));
        }

        public static vec4 texture(Location location, sampler1DShadow sampler, vec3 P, float bias = 0)
        {
            throw new NotImplementedException();
        }

        public static vec4 texture(Location location, sampler2DShadow sampler, vec3 P, float bias = 0)
        {
            throw new NotImplementedException();
        }

        public static vec4 texture(Location location, samplerCubeShadow sampler, vec3 P, float bias = 0)
        {
            throw new NotImplementedException();
        }

        public static vec4 texture(Location location, sampler1DArray sampler, vec2 P, float bias = 0)
        {
            return texturef(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        }

        public static ivec4 texture(Location location, isampler1DArray sampler, vec2 P, float bias = 0)
        {
            return texturei(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        }

        public static uvec4 texture(Location location, usampler1DArray sampler, vec2 P, float bias = 0)
        {
            return textureu(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBinding1DArray);
        }

        public static vec4 texture(Location location, sampler2DArray sampler, vec3 P, float bias = 0)
        {
            return texturef(location, sampler, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        }

        public static ivec4 texture(Location location, isampler2DArray sampler, vec3 P, float bias = 0)
        {
            return texturei(location, sampler, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        }

        public static uvec4 texture(Location location, usampler2DArray sampler, vec3 P, float bias = 0)
        {
            return textureu(location, sampler, P.x, P.y, P.z, 0, GetPName.TextureBinding2DArray);
        }

        public static vec4 texture(Location location, samplerCubeArray sampler, vec4 P, float bias = 0)
        {
            return new vec4(textureCube<float>(location, sampler, P.xyz, (int)P.w, 0, (GetPName)All.TextureCubeMapArray));
        }

        public static ivec4 texture(Location location, isamplerCubeArray sampler, vec4 P, float bias = 0)
        {
            return new ivec4(textureCube<int>(location, sampler, P.xyz, (int)P.w, 0, (GetPName)All.TextureCubeMapArray));
        }

        public static uvec4 texture(Location location, usamplerCubeArray sampler, vec4 P, float bias = 0)
        {
            return new uvec4(textureCube<uint>(location, sampler, P.xyz, (int)P.w, 0, (GetPName)All.TextureCubeMapArray));
        }

        public static vec4 texture(Location location, sampler1DArrayShadow sampler, vec3 P, float bias = 0)
        {
            throw new NotImplementedException();
        }

        public static vec4 texture(Location location, sampler2DArrayShadow sampler, vec4 P, float bias = 0)
        {
            throw new NotImplementedException();
        }

        public static ivec4 texture(Location location, isampler2DArrayShadow sampler, vec4 P, float bias = 0)
        {
            throw new NotImplementedException();
        }

        public static uvec4 texture(Location location, usampler2DArrayShadow sampler, vec4 P, float bias = 0)
        {
            throw new NotImplementedException();
        }

        public static vec4 texture(Location location, sampler2DRect sampler, vec2 P)
        {
            return texturef(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        }

        public static ivec4 texture(Location location, isampler2DRect sampler, vec2 P)
        {
            return texturei(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        }

        public static uvec4 texture(Location location, usampler2DRect sampler, vec2 P)
        {
            return textureu(location, sampler, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle);
        }

        public static vec4 texture(Location location, sampler2DRectShadow sampler, vec3 P)
        {
            throw new NotImplementedException();
        }

        public static vec4 texture(Location location, samplerCubeArrayShadow sampler, vec4 P, float compare)
        {
            throw new NotImplementedException();
        }

        public static ivec4 texture(Location location, isamplerCubeArrayShadow sampler, vec4 P, float compare)
        {
            throw new NotImplementedException();
        }

        public static uvec4 texture(Location location, usamplerCubeArrayShadow sampler, vec4 P, float compare)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Texel Fetch

        #region Helpers

        private static T[] regionFetch<T>(int sampler, int x, int y, int z, int w, int h, int d, int lod, GetPName binding)
            where T : struct
        {
            // get return type
            var type = Type2PixelType<T>();

            // get texture ID
            GL.ActiveTexture(TextureUnit.Texture0 + sampler);
            GL.GetInteger(binding, out int ID);

            // get texture format
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureInternalFormat, out int f);
            var format = (PixelInternalFormat)f;
            var isdepth = format.ToString().StartsWith("Depth");

            var n = w * h * d;
            var p = new T[4 * n];
            var rgba = isdepth ? DepthComponent : Rgba;
            type = isdepth ? PixelType.Float : type;

            // get texture size
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureWidth, out int width);

            // if valid texture coordinate
            if (0 <= x && x < width)
            {
                // if this is a buffer texture
                if (binding == GetPName.TextureBindingBuffer)
                    GL.GetNamedBufferSubData(ID, (IntPtr)(Marshal.SizeOf<T>() * x), p.Size(), p);
                // is a normal texture
                else
                {
                    // get rest of the texture size
                    GL.GetTextureLevelParameter(ID, lod, Parameter.TextureHeight, out int height);
                    GL.GetTextureLevelParameter(ID, lod, Parameter.TextureDepth, out int depth);
                    // if valid texture coordinate
                    if (0 <= y && y < height && 0 <= z && z < depth)
                        GL.GetTextureSubImage(ID, lod, x, y, z, w, h, d, rgba, type, p.Size(), p);

                    DebugGetError(new StackTrace(true));
                }
            }

            if (isdepth)
            {
                for (int i = n - 1; 0 <= i; i--)
                {
                    p[4 * i + 0] = p[i];
                    p[4 * i + 1] = default(T);
                    p[4 * i + 2] = default(T);
                    p[4 * i + 3] = default(T);
                }
            }

            return p;
        }

        private static T[] texelFetch<T>(int sampler, int x, int y, int z, int lod, GetPName binding)
            where T : struct
        {
            return regionFetch<T>(sampler, x, y, z, 1, 1, 1, lod, binding);
        }

        #endregion

        public static vec4 texelFetch(Location location, sampler1D sampler, int P, int lod)
        {
            return Shader.TraceFunction(location, new vec4(texelFetch<float>(sampler, P, 0, 0, lod, GetPName.TextureBinding1D)), "texelFetch");
        }

        public static vec4 texelFetch(Location location, sampler2D sampler, ivec2 P, int lod)
        {
            return Shader.TraceFunction(location, new vec4(texelFetch<float>(sampler, P.x, P.y, 0, lod, GetPName.TextureBinding2D)), "texelFetch");
        }

        public static vec4 texelFetch(Location location, sampler3D sampler, ivec3 P, int lod)
        {
            return Shader.TraceFunction(location, new vec4(texelFetch<float>(sampler, P.x, P.y, P.z, lod, GetPName.TextureBinding3D)), "texelFetch");
        }

        public static vec4 texelFetch(Location location, sampler2DRect sampler, ivec2 P)
        {
            return Shader.TraceFunction(location, new vec4(texelFetch<float>(sampler, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle)), "texelFetch");
        }

        public static vec4 texelFetch(Location location, sampler1DArray sampler, ivec2 P, int lod)
        {
            return Shader.TraceFunction(location, new vec4(texelFetch<float>(sampler, P.x, P.y, 0, lod, GetPName.TextureBinding1DArray)), "texelFetch");
        }

        public static vec4 texelFetch(Location location, sampler2DArray sampler, ivec3 P, int lod)
        {
            return Shader.TraceFunction(location, new vec4(texelFetch<float>(sampler, P.x, P.y, P.z, lod, GetPName.TextureBinding2DArray)), "texelFetch");
        }

        public static vec4 texelFetch(Location location, samplerBuffer sampler, int P)
        {
            return Shader.TraceFunction(location, new vec4(texelFetch<float>(sampler, P, 0, 0, 0, GetPName.TextureBindingBuffer)), "texelFetch");
        }

        public static vec4 texelFetch(Location location, sampler2DMS sampler, ivec2 P, int sample)
        {
            throw new NotImplementedException();
        }

        public static vec4 texelFetch(Location location, sampler2DMSArray sampler, ivec3 P, int sample)
        {
            throw new NotImplementedException();
        }

        public static ivec4 texelFetch(Location location, isampler1D sampler, int P, int lod)
        {
            return Shader.TraceFunction(location, new ivec4(texelFetch<int>(sampler, P, 0, 0, lod, GetPName.TextureBinding1D)), "texelFetch");
        }

        public static ivec4 texelFetch(Location location, isampler2D sampler, ivec2 P, int lod)
        {
            return Shader.TraceFunction(location, new ivec4(texelFetch<int>(sampler, P.x, P.y, 0, lod, GetPName.TextureBinding2D)), "texelFetch");
        }

        public static ivec4 texelFetch(Location location, isampler3D sampler, ivec3 P, int lod)
        {
            return Shader.TraceFunction(location, new ivec4(texelFetch<int>(sampler, P.x, P.y, P.z, lod, GetPName.TextureBinding3D)), "texelFetch");
        }

        public static ivec4 texelFetch(Location location, isampler2DRect sampler, ivec2 P)
        {
            return Shader.TraceFunction(location, new ivec4(texelFetch<int>(sampler, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle)), "texelFetch");
        }

        public static ivec4 texelFetch(Location location, isampler1DArray sampler, ivec2 P, int lod)
        {
            return Shader.TraceFunction(location, new ivec4(texelFetch<int>(sampler, P.x, P.y, 0, lod, GetPName.TextureBinding1DArray)), "texelFetch");
        }

        public static ivec4 texelFetch(Location location, isampler2DArray sampler, ivec3 P, int lod)
        {
            return Shader.TraceFunction(location, new ivec4(texelFetch<int>(sampler, P.x, P.y, P.z, lod, GetPName.TextureBinding2DArray)), "texelFetch");
        }

        public static ivec4 texelFetch(Location location, isamplerBuffer sampler, int P)
        {
            return Shader.TraceFunction(location, new ivec4(texelFetch<int>(sampler, P, 0, 0, 0, GetPName.TextureBindingBuffer)), "texelFetch");
        }

        public static ivec4 texelFetch(Location location, isampler2DMS sampler, ivec2 P, int sample)
        {
            throw new NotImplementedException();
        }

        public static ivec4 texelFetch(Location location, isampler2DMSArray sampler, ivec3 P, int sample)
        {
            throw new NotImplementedException();
        }

        public static uvec4 texelFetch(Location location, usampler1D sampler, int P, int lod)
        {
            return Shader.TraceFunction(location, new uvec4(texelFetch<uint>(sampler, P, 0, 0, lod, GetPName.TextureBinding1D)), "texelFetch");
        }

        public static uvec4 texelFetch(Location location, usampler2D sampler, ivec2 P, int lod)
        {
            return Shader.TraceFunction(location, new uvec4(texelFetch<uint>(sampler, P.x, P.y, 0, lod, GetPName.TextureBinding2D)), "texelFetch");
        }

        public static uvec4 texelFetch(Location location, usampler3D sampler, ivec3 P, int lod)
        {
            return Shader.TraceFunction(location, new uvec4(texelFetch<uint>(sampler, P.x, P.y, P.z, lod, GetPName.TextureBinding3D)), "texelFetch");
        }

        public static uvec4 texelFetch(Location location, usampler2DRect sampler, ivec2 P)
        {
            return Shader.TraceFunction(location, new uvec4(texelFetch<uint>(sampler, P.x, P.y, 0, 0, GetPName.TextureBindingRectangle)), "texelFetch");
        }

        public static uvec4 texelFetch(Location location, usampler1DArray sampler, ivec2 P, int lod)
        {
            return Shader.TraceFunction(location, new uvec4(texelFetch<uint>(sampler, P.x, P.y, 0, lod, GetPName.TextureBinding1DArray)), "texelFetch");
        }

        public static uvec4 texelFetch(Location location, usampler2DArray sampler, ivec3 P, int lod)
        {
            return Shader.TraceFunction(location, new uvec4(texelFetch<uint>(sampler, P.x, P.y, P.z, lod, GetPName.TextureBinding2DArray)), "texelFetch");
        }

        public static uvec4 texelFetch(Location location, usamplerBuffer sampler, int P)
        {
            return Shader.TraceFunction(location, new uvec4(texelFetch<uint>(sampler, P, 0, 0, 0, GetPName.TextureBindingBuffer)), "texelFetch");
        }

        public static uvec4 texelFetch(Location location, usampler2DMS sampler, ivec2 P, int sample)
        {
            throw new NotImplementedException();
        }

        public static uvec4 texelFetch(Location location, usampler2DMSArray sampler, ivec3 P, int sample)
        {
            throw new NotImplementedException();
        }

        #endregion

        public static ivec3 textureSize(int sampler, int lod, GetPName binding)
        {
            // get texture ID
            GL.ActiveTexture(TextureUnit.Texture0 + sampler);
            GL.GetInteger(binding, out int ID);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureWidth, out int width);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureHeight, out int height);
            GL.GetTextureLevelParameter(ID, lod, Parameter.TextureDepth, out int depth);
            return new ivec3(width, height, depth);
        }

        public static int textureSize(Location location, sampler1D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding1D).x, "textureSize");
        }

        public static ivec2 textureSize(Location location, sampler2D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding2D).xy, "textureSize");
        }

        public static ivec3 textureSize(Location location, sampler3D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding3D), "textureSize");
        }

        public static ivec2 textureSize(Location location, sampler2DRect sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBindingRectangle).xy, "textureSize");
        }

        public static ivec2 textureSize(Location location, sampler1DArray sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding1DArray).xy, "textureSize");
        }

        public static ivec3 textureSize(Location location, sampler2DArray sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding2DArray), "textureSize");
        }

        public static int textureSize(Location location, samplerBuffer sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBindingBuffer).x, "textureSize");
        }

        public static ivec2 textureSize(Location location, sampler2DMS sampler, int sample)
        {
            throw new NotImplementedException();
        }

        public static ivec3 textureSize(Location location, sampler2DMSArray sampler, int sample)
        {
            throw new NotImplementedException();
        }

        public static int textureSize(Location location, isampler1D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding1D).x, "textureSize");
        }

        public static ivec2 textureSize(Location location, isampler2D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding2D).xy, "textureSize");
        }

        public static ivec3 textureSize(Location location, isampler3D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding3D), "textureSize");
        }

        public static ivec2 textureSize(Location location, isampler2DRect sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBindingRectangle).xy, "textureSize");
        }

        public static ivec2 textureSize(Location location, isampler1DArray sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding1DArray).xy, "textureSize");
        }

        public static ivec3 textureSize(Location location, isampler2DArray sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding2DArray), "textureSize");
        }

        public static int textureSize(Location location, isamplerBuffer sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBindingBuffer).x, "textureSize");
        }

        public static ivec4 textureSize(Location location, isampler2DMS sampler, int lod)
        {
            throw new NotImplementedException();
        }

        public static ivec4 textureSize(Location location, isampler2DMSArray sampler, int lod)
        {
            throw new NotImplementedException();
        }

        public static int textureSize(Location location, usampler1D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding1D).x, "textureSize");
        }

        public static ivec2 textureSize(Location location, usampler2D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding2D).xy, "textureSize");
        }

        public static ivec3 textureSize(Location location, usampler3D sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding3D), "textureSize");
        }

        public static ivec2 textureSize(Location location, usampler2DRect sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBindingRectangle).xy, "textureSize");
        }

        public static ivec2 textureSize(Location location, usampler1DArray sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding1DArray).xy, "textureSize");
        }

        public static ivec3 textureSize(Location location, usampler2DArray sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBinding2DArray), "textureSize");
        }

        public static int textureSize(Location location, usamplerBuffer sampler, int lod)
        {
            return Shader.TraceFunction(location, textureSize(sampler, lod, GetPName.TextureBindingBuffer).x, "textureSize");
        }

        public static ivec2 textureSize(Location location, usampler2DMS sampler, int lod)
        {
            throw new NotImplementedException();
        }

        public static ivec2 texelFetch(Location location, usampler2DMSArray sampler, int lod)
        {
            throw new NotImplementedException();
        }

        #region Uniform Access

        internal static readonly Type[] BaseIntTypes = new[] { typeof(bool), typeof(int), typeof(uint) };
        internal static readonly Type[] BaseFloatTypes = new[] { typeof(float), typeof(double) };
        internal static readonly Type[] BaseTypes = BaseIntTypes.Concat(BaseFloatTypes).ToArray();

        protected static object GetUniform(string uniformName, Type uniformType, ProgramPipelineParameter shader)
        {
            int size;
            var locations = new int[1];

            // get current shader program pipeline
            var pipeline = GL.GetInteger(GetPName.ProgramPipelineBinding);
            if (pipeline <= 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get vertex shader
            GL.GetProgramPipeline(pipeline, shader, out int program);
            if (program <= 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get uniform buffer object block index
            int block = GL.GetUniformBlockIndex(program, uniformName.Substring(0, uniformName.IndexOf('.')));
            if (block < 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get bound buffer object
            GL.GetActiveUniformBlock(program, block, ActiveUniformBlockParameter.UniformBlockBinding, out int unit);
            GL.GetInteger(GetIndexedPName.UniformBufferBinding, unit, out int glbuf);
            if (glbuf <= 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get uniform indices in uniform block
            GL.GetUniformIndices(program, 1, new[] { uniformName }, locations);
            var location = locations[0];
            if (location < 0)
                return DebugGetError(uniformType, new StackTrace(true));

            // get uniform information
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformType, out int type);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformSize, out int length);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformOffset, out int offset);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformArrayStride, out int stride);

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
