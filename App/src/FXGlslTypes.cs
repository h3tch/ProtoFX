using System;

namespace App.Glsl
{
    #region Typedef

    struct sampler1D { public int i; public static implicit operator int(sampler1D a) => a.i; public static implicit operator sampler1D(int a) => new sampler1D { i = a }; }
    struct isampler1D { public int i; public static implicit operator int(isampler1D a) => a.i; public static implicit operator isampler1D(int a) => new isampler1D { i = a }; }
    struct usampler1D { public int i; public static implicit operator int(usampler1D a) => a.i; public static implicit operator usampler1D(int a) => new usampler1D { i = a }; }
    struct sampler2D { public int i; public static implicit operator int(sampler2D a) => a.i; public static implicit operator sampler2D(int a) => new sampler2D { i = a }; }
    struct isampler2D { public int i; public static implicit operator int(isampler2D a) => a.i; public static implicit operator isampler2D(int a) => new isampler2D { i = a }; }
    struct usampler2D { public int i; public static implicit operator int(usampler2D a) => a.i; public static implicit operator usampler2D(int a) => new usampler2D { i = a }; }
    struct sampler3D { public int i; public static implicit operator int(sampler3D a) => a.i; public static implicit operator sampler3D(int a) => new sampler3D { i = a }; }
    struct isampler3D { public int i; public static implicit operator int(isampler3D a) => a.i; public static implicit operator isampler3D(int a) => new isampler3D { i = a }; }
    struct usampler3D { public int i; public static implicit operator int(usampler3D a) => a.i; public static implicit operator usampler3D(int a) => new usampler3D { i = a }; }
    struct samplerCube { public int i; public static implicit operator int(samplerCube a) => a.i; public static implicit operator samplerCube(int a) => new samplerCube { i = a }; }
    struct isamplerCube { public int i; public static implicit operator int(isamplerCube a) => a.i; public static implicit operator isamplerCube(int a) => new isamplerCube { i = a }; }
    struct usamplerCube { public int i; public static implicit operator int(usamplerCube a) => a.i; public static implicit operator usamplerCube(int a) => new usamplerCube { i = a }; }
    struct sampler1DShadow { public int i; public static implicit operator int(sampler1DShadow a) => a.i; public static implicit operator sampler1DShadow(int a) => new sampler1DShadow { i = a }; }
    struct sampler2DShadow { public int i; public static implicit operator int(sampler2DShadow a) => a.i; public static implicit operator sampler2DShadow(int a) => new sampler2DShadow { i = a }; }
    struct samplerCubeShadow { public int i; public static implicit operator int(samplerCubeShadow a) => a.i; public static implicit operator samplerCubeShadow(int a) => new samplerCubeShadow { i = a }; }
    struct sampler1DArray { public int i; public static implicit operator int(sampler1DArray a) => a.i; public static implicit operator sampler1DArray(int a) => new sampler1DArray { i = a }; }
    struct isampler1DArray { public int i; public static implicit operator int(isampler1DArray a) => a.i; public static implicit operator isampler1DArray(int a) => new isampler1DArray { i = a }; }
    struct usampler1DArray { public int i; public static implicit operator int(usampler1DArray a) => a.i; public static implicit operator usampler1DArray(int a) => new usampler1DArray { i = a }; }
    struct sampler2DArray { public int i; public static implicit operator int(sampler2DArray a) => a.i; public static implicit operator sampler2DArray(int a) => new sampler2DArray { i = a }; }
    struct usampler2DArray { public int i; public static implicit operator int(usampler2DArray a) => a.i; public static implicit operator usampler2DArray(int a) => new usampler2DArray { i = a }; }
    struct isampler2DArray { public int i; public static implicit operator int(isampler2DArray a) => a.i; public static implicit operator isampler2DArray(int a) => new isampler2DArray { i = a }; }
    struct samplerCubeArray { public int i; public static implicit operator int(samplerCubeArray a) => a.i; public static implicit operator samplerCubeArray(int a) => new samplerCubeArray { i = a }; }
    struct usamplerCubeArray { public int i; public static implicit operator int(usamplerCubeArray a) => a.i; public static implicit operator usamplerCubeArray(int a) => new usamplerCubeArray { i = a }; }
    struct isamplerCubeArray { public int i; public static implicit operator int(isamplerCubeArray a) => a.i; public static implicit operator isamplerCubeArray(int a) => new isamplerCubeArray { i = a }; }
    struct sampler1DArrayShadow { public int i; public static implicit operator int(sampler1DArrayShadow a) => a.i; public static implicit operator sampler1DArrayShadow(int a) => new sampler1DArrayShadow { i = a }; }
    struct sampler2DArrayShadow { public int i; public static implicit operator int(sampler2DArrayShadow a) => a.i; public static implicit operator sampler2DArrayShadow(int a) => new sampler2DArrayShadow { i = a }; }
    struct usampler2DArrayShadow { public int i; public static implicit operator int(usampler2DArrayShadow a) => a.i; public static implicit operator usampler2DArrayShadow(int a) => new usampler2DArrayShadow { i = a }; }
    struct isampler2DArrayShadow { public int i; public static implicit operator int(isampler2DArrayShadow a) => a.i; public static implicit operator isampler2DArrayShadow(int a) => new isampler2DArrayShadow { i = a }; }
    struct sampler2DRect { public int i; public static implicit operator int(sampler2DRect a) => a.i; public static implicit operator sampler2DRect(int a) => new sampler2DRect { i = a }; }
    struct isampler2DRect { public int i; public static implicit operator int(isampler2DRect a) => a.i; public static implicit operator isampler2DRect(int a) => new isampler2DRect { i = a }; }
    struct usampler2DRect { public int i; public static implicit operator int(usampler2DRect a) => a.i; public static implicit operator usampler2DRect(int a) => new usampler2DRect { i = a }; }
    struct sampler2DRectShadow { public int i; public static implicit operator int(sampler2DRectShadow a) => a.i; public static implicit operator sampler2DRectShadow(int a) => new sampler2DRectShadow { i = a }; }
    struct samplerCubeArrayShadow { public int i; public static implicit operator int(samplerCubeArrayShadow a) => a.i; public static implicit operator samplerCubeArrayShadow(int a) => new samplerCubeArrayShadow { i = a }; }
    struct isamplerCubeArrayShadow { public int i; public static implicit operator int(isamplerCubeArrayShadow a) => a.i; public static implicit operator isamplerCubeArrayShadow(int a) => new isamplerCubeArrayShadow { i = a }; }
    struct usamplerCubeArrayShadow { public int i; public static implicit operator int(usamplerCubeArrayShadow a) => a.i; public static implicit operator usamplerCubeArrayShadow(int a) => new usamplerCubeArrayShadow { i = a }; }

    #endregion

    #region Data Types

    class tvec2<T>
    {
        #region tvec2

        internal T[] v = new T[2];
        public T this[int i] { get { return v[i]; } set { v[i] = value; } }
        public tvec2() { }
        public tvec2(T a) : this(a, a) { }
        public tvec2(T x, T y) : this() { v[0] = x; v[1] = y; }

        #endregion
    }

    class tvec3<T>
    {
        #region vec3

        internal T[] v = new T[3];
        public T this[int i] { get { return v[i]; } set { v[i] = value; } }
        public tvec3() { }
        public tvec3(T a) : this(a, a, a) { }
        public tvec3(T x, T y, T z) : this() { v[0] = x; v[1] = y; v[2] = z; }

        #endregion
    }

    class tvec4<T>
    {
        #region vec4

        internal T[] v = new T[4];
        public T this[int i] { get { return v[i]; } set { v[i] = value; } }
        public tvec4() { }
        public tvec4(T a) : this(a, a, a, a) { }
        public tvec4(T x, T y, T z, T w) : this() { v[0] = x; v[1] = y; v[2] = z; v[3] = w; }

        #endregion
    }

    class vec2 : tvec2<float>
    {
        #region vec2

        public vec2() : base() { }
        public vec2(float a) : base(a, a) { }
        public vec2(float x, float y) : base(x, y) { }

        #endregion

        #region Operators

        public static vec2 operator +(vec2 a) => new vec2(a.x, a.y);
        public static vec2 operator -(vec2 a) => new vec2(-a.x, -a.y);
        public static vec2 operator +(vec2 a, vec2 b) => new vec2(a.x + b.x, a.y + b.y);
        public static vec2 operator +(vec2 a, float b) => new vec2(a.x + b, a.y + b);
        public static vec2 operator +(float a, vec2 b) => new vec2(a + b.x, a + b.y);
        public static vec2 operator -(vec2 a, vec2 b) => new vec2(a.x - b.x, a.y - b.y);
        public static vec2 operator -(vec2 a, float b) => new vec2(a.x - b, a.y - b);
        public static vec2 operator -(float a, vec2 b) => new vec2(a - b.x, a - b.y);
        public static vec2 operator *(vec2 a, vec2 b) => new vec2(a.x * b.x, a.y * b.y);
        public static vec2 operator *(vec2 a, float b) => new vec2(a.x * b, a.y * b);
        public static vec2 operator *(float a, vec2 b) => new vec2(a * b.x, a * b.y);
        public static vec2 operator /(vec2 a, vec2 b) => new vec2(a.x / b.x, a.y / b.y);
        public static vec2 operator /(vec2 a, float b) => new vec2(a.x / b, a.y / b);
        public static vec2 operator /(float a, vec2 b) => new vec2(a / b.x, a / b.y);

        #endregion

        #region Generated

        public float x { get { return v[0]; } set { v[0] = value; } }
        public float y { get { return v[1]; } set { v[1] = value; } }
        public vec2 xx { get { return new vec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 xy { get { return new vec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public vec2 yx { get { return new vec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 yy { get { return new vec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public vec3 xxx { get { return new vec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xxy { get { return new vec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 xyx { get { return new vec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xyy { get { return new vec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 yxx { get { return new vec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 yxy { get { return new vec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 yyx { get { return new vec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 yyy { get { return new vec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec4 xxxx { get { return new vec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxxy { get { return new vec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xxyx { get { return new vec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxyy { get { return new vec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xyxx { get { return new vec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xyxy { get { return new vec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xyyx { get { return new vec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xyyy { get { return new vec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxxx { get { return new vec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxxy { get { return new vec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxyx { get { return new vec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxyy { get { return new vec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yyxx { get { return new vec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yyxy { get { return new vec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yyyx { get { return new vec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yyyy { get { return new vec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }

        #endregion
    }

    class dvec2 : tvec2<double>
    {
        #region dvec2

        public dvec2() : base() { }
        public dvec2(double a) : base(a, a) { }
        public dvec2(double x, double y) : base(x, y) { }

        #endregion

        #region Operators

        public static dvec2 operator +(dvec2 a) => new dvec2(a.x, a.y);
        public static dvec2 operator -(dvec2 a) => new dvec2(-a.x, -a.y);
        public static dvec2 operator +(dvec2 a, dvec2 b) => new dvec2(a.x + b.x, a.y + b.y);
        public static dvec2 operator +(dvec2 a, double b) => new dvec2(a.x + b, a.y + b);
        public static dvec2 operator +(double a, dvec2 b) => new dvec2(a + b.x, a + b.y);
        public static dvec2 operator -(dvec2 a, dvec2 b) => new dvec2(a.x - b.x, a.y - b.y);
        public static dvec2 operator -(dvec2 a, double b) => new dvec2(a.x - b, a.y - b);
        public static dvec2 operator -(double a, dvec2 b) => new dvec2(a - b.x, a - b.y);
        public static dvec2 operator *(dvec2 a, dvec2 b) => new dvec2(a.x * b.x, a.y * b.y);
        public static dvec2 operator *(dvec2 a, double b) => new dvec2(a.x * b, a.y * b);
        public static dvec2 operator *(double a, dvec2 b) => new dvec2(a * b.x, a * b.y);
        public static dvec2 operator /(dvec2 a, dvec2 b) => new dvec2(a.x / b.x, a.y / b.y);
        public static dvec2 operator /(dvec2 a, double b) => new dvec2(a.x / b, a.y / b);
        public static dvec2 operator /(double a, dvec2 b) => new dvec2(a / b.x, a / b.y);

        #endregion

        #region Generated

        public double x { get { return v[0]; } set { v[0] = value; } }
        public double y { get { return v[1]; } set { v[1] = value; } }
        public dvec2 xx { get { return new dvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 xy { get { return new dvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public dvec2 yx { get { return new dvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 yy { get { return new dvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public dvec3 xxx { get { return new dvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xxy { get { return new dvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 xyx { get { return new dvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xyy { get { return new dvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 yxx { get { return new dvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 yxy { get { return new dvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 yyx { get { return new dvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 yyy { get { return new dvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec4 xxxx { get { return new dvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxxy { get { return new dvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xxyx { get { return new dvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxyy { get { return new dvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xyxx { get { return new dvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xyxy { get { return new dvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xyyx { get { return new dvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xyyy { get { return new dvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxxx { get { return new dvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxxy { get { return new dvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxyx { get { return new dvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxyy { get { return new dvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yyxx { get { return new dvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yyxy { get { return new dvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yyyx { get { return new dvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yyyy { get { return new dvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }

        #endregion
    }

    class bvec2 : tvec2<bool>
    {
        #region bvec2

        public bvec2() : base() { }
        public bvec2(bool a) : base(a, a) { }
        public bvec2(bool x, bool y) : base(x, y) { }

        #endregion

        #region Generated

        public bool x { get { return v[0]; } set { v[0] = value; } }
        public bool y { get { return v[1]; } set { v[1] = value; } }
        public bvec2 xx { get { return new bvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 xy { get { return new bvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public bvec2 yx { get { return new bvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 yy { get { return new bvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public bvec3 xxx { get { return new bvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xxy { get { return new bvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 xyx { get { return new bvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xyy { get { return new bvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 yxx { get { return new bvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 yxy { get { return new bvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 yyx { get { return new bvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 yyy { get { return new bvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec4 xxxx { get { return new bvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxxy { get { return new bvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xxyx { get { return new bvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxyy { get { return new bvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xyxx { get { return new bvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xyxy { get { return new bvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xyyx { get { return new bvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xyyy { get { return new bvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxxx { get { return new bvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxxy { get { return new bvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxyx { get { return new bvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxyy { get { return new bvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yyxx { get { return new bvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yyxy { get { return new bvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yyyx { get { return new bvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yyyy { get { return new bvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }

        #endregion
    }

    class ivec2 : tvec2<int>
    {
        #region ivec2

        public ivec2() : base() { }
        public ivec2(int a) : base(a, a) { }
        public ivec2(int x, int y) : base(x, y) { }

        #endregion

        #region Generated

        public int x { get { return v[0]; } set { v[0] = value; } }
        public int y { get { return v[1]; } set { v[1] = value; } }
        public ivec2 xx { get { return new ivec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 xy { get { return new ivec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public ivec2 yx { get { return new ivec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 yy { get { return new ivec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public ivec3 xxx { get { return new ivec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xxy { get { return new ivec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 xyx { get { return new ivec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xyy { get { return new ivec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 yxx { get { return new ivec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 yxy { get { return new ivec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 yyx { get { return new ivec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 yyy { get { return new ivec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec4 xxxx { get { return new ivec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxxy { get { return new ivec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xxyx { get { return new ivec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxyy { get { return new ivec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xyxx { get { return new ivec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xyxy { get { return new ivec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xyyx { get { return new ivec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xyyy { get { return new ivec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxxx { get { return new ivec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxxy { get { return new ivec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxyx { get { return new ivec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxyy { get { return new ivec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yyxx { get { return new ivec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yyxy { get { return new ivec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yyyx { get { return new ivec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yyyy { get { return new ivec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }

        #endregion
    }

    class uvec2 : tvec2<uint>
    {
        #region uvec2

        public uvec2() : base() { }
        public uvec2(uint a) : base(a, a) { }
        public uvec2(uint x, uint y) : base(x, y) { }

        #endregion

        #region Generated

        public uint x { get { return v[0]; } set { v[0] = value; } }
        public uint y { get { return v[1]; } set { v[1] = value; } }
        public uvec2 xx { get { return new uvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 xy { get { return new uvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public uvec2 yx { get { return new uvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 yy { get { return new uvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public uvec3 xxx { get { return new uvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xxy { get { return new uvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 xyx { get { return new uvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xyy { get { return new uvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 yxx { get { return new uvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 yxy { get { return new uvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 yyx { get { return new uvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 yyy { get { return new uvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec4 xxxx { get { return new uvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxxy { get { return new uvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xxyx { get { return new uvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxyy { get { return new uvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xyxx { get { return new uvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xyxy { get { return new uvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xyyx { get { return new uvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xyyy { get { return new uvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxxx { get { return new uvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxxy { get { return new uvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxyx { get { return new uvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxyy { get { return new uvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yyxx { get { return new uvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yyxy { get { return new uvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yyyx { get { return new uvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yyyy { get { return new uvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }

        #endregion
    }

    class vec3 : tvec3<float>
    {
        #region vec3

        public vec3() : base() { }
        public vec3(float a) : base(a, a, a) { }
        public vec3(float x, float y, float z) : base(x, y, z) { }

        #endregion

        #region Operators

        public static vec3 operator +(vec3 a) => new vec3(a.x, a.y, a.z);
        public static vec3 operator -(vec3 a) => new vec3(-a.x, -a.y, -a.z);
        public static vec3 operator +(vec3 a, vec3 b) => new vec3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static vec3 operator +(vec3 a, float b) => new vec3(a.x + b, a.y + b, a.z + b);
        public static vec3 operator +(float a, vec3 b) => new vec3(a + b.x, a + b.y, a + b.z);
        public static vec3 operator -(vec3 a, vec3 b) => new vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static vec3 operator -(vec3 a, float b) => new vec3(a.x - b, a.y - b, a.z - b);
        public static vec3 operator -(float a, vec3 b) => new vec3(a - b.x, a - b.y, a - b.z);
        public static vec3 operator *(vec3 a, vec3 b) => new vec3(a.x * b.x, a.y * b.y, a.z * b.z);
        public static vec3 operator *(vec3 a, float b) => new vec3(a.x * b, a.y * b, a.z * b);
        public static vec3 operator *(float a, vec3 b) => new vec3(a * b.x, a * b.y, a * b.z);
        public static vec3 operator /(vec3 a, vec3 b) => new vec3(a.x / b.x, a.y / b.y, a.z / b.z);
        public static vec3 operator /(float a, vec3 b) => new vec3(a / b.x, a / b.y, a / b.z);
        public static vec3 operator /(vec3 a, float b) => new vec3(a.x / b, a.y / b, a.z / b);

        #endregion

        #region Generated

        public float x { get { return v[0]; } set { v[0] = value; } }
        public float y { get { return v[1]; } set { v[1] = value; } }
        public float z { get { return v[2]; } set { v[2] = value; } }
        public vec2 xx { get { return new vec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 xy { get { return new vec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public vec2 xz { get { return new vec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public vec2 yx { get { return new vec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 yy { get { return new vec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public vec2 yz { get { return new vec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public vec2 zx { get { return new vec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 zy { get { return new vec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public vec2 zz { get { return new vec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public vec3 xxx { get { return new vec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xxy { get { return new vec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 xxz { get { return new vec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 xyx { get { return new vec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xyy { get { return new vec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 xyz { get { return new vec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 xzx { get { return new vec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xzy { get { return new vec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 xzz { get { return new vec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 yxx { get { return new vec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 yxy { get { return new vec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 yxz { get { return new vec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 yyx { get { return new vec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 yyy { get { return new vec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 yyz { get { return new vec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 yzx { get { return new vec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 yzy { get { return new vec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 yzz { get { return new vec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 zxx { get { return new vec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 zxy { get { return new vec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 zxz { get { return new vec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 zyx { get { return new vec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 zyy { get { return new vec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 zyz { get { return new vec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 zzx { get { return new vec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 zzy { get { return new vec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 zzz { get { return new vec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public vec4 xxxx { get { return new vec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxxy { get { return new vec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xxxz { get { return new vec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xxyx { get { return new vec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxyy { get { return new vec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xxyz { get { return new vec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xxzx { get { return new vec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxzy { get { return new vec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xxzz { get { return new vec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xyxx { get { return new vec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xyxy { get { return new vec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xyxz { get { return new vec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xyyx { get { return new vec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xyyy { get { return new vec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xyyz { get { return new vec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xyzx { get { return new vec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xyzy { get { return new vec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xyzz { get { return new vec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xzxx { get { return new vec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xzxy { get { return new vec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xzxz { get { return new vec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xzyx { get { return new vec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xzyy { get { return new vec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xzyz { get { return new vec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xzzx { get { return new vec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xzzy { get { return new vec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xzzz { get { return new vec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yxxx { get { return new vec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxxy { get { return new vec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxxz { get { return new vec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yxyx { get { return new vec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxyy { get { return new vec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxyz { get { return new vec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yxzx { get { return new vec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxzy { get { return new vec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxzz { get { return new vec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yyxx { get { return new vec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yyxy { get { return new vec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yyxz { get { return new vec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yyyx { get { return new vec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yyyy { get { return new vec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yyyz { get { return new vec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yyzx { get { return new vec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yyzy { get { return new vec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yyzz { get { return new vec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yzxx { get { return new vec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yzxy { get { return new vec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yzxz { get { return new vec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yzyx { get { return new vec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yzyy { get { return new vec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yzyz { get { return new vec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yzzx { get { return new vec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yzzy { get { return new vec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yzzz { get { return new vec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zxxx { get { return new vec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zxxy { get { return new vec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zxxz { get { return new vec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zxyx { get { return new vec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zxyy { get { return new vec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zxyz { get { return new vec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zxzx { get { return new vec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zxzy { get { return new vec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zxzz { get { return new vec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zyxx { get { return new vec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zyxy { get { return new vec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zyxz { get { return new vec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zyyx { get { return new vec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zyyy { get { return new vec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zyyz { get { return new vec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zyzx { get { return new vec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zyzy { get { return new vec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zyzz { get { return new vec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zzxx { get { return new vec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zzxy { get { return new vec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zzxz { get { return new vec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zzyx { get { return new vec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zzyy { get { return new vec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zzyz { get { return new vec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zzzx { get { return new vec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zzzy { get { return new vec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zzzz { get { return new vec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }

        #endregion
    }

    class dvec3 : tvec3<double>
    {
        #region dvec3

        public dvec3() : base() { }
        public dvec3(double a) : base(a, a, a) { }
        public dvec3(double x, double y, double z) : base(x, y, z) { }

        #endregion

        #region Generated

        public double x { get { return v[0]; } set { v[0] = value; } }
        public double y { get { return v[1]; } set { v[1] = value; } }
        public double z { get { return v[2]; } set { v[2] = value; } }
        public dvec2 xx { get { return new dvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 xy { get { return new dvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public dvec2 xz { get { return new dvec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public dvec2 yx { get { return new dvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 yy { get { return new dvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public dvec2 yz { get { return new dvec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public dvec2 zx { get { return new dvec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 zy { get { return new dvec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public dvec2 zz { get { return new dvec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public dvec3 xxx { get { return new dvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xxy { get { return new dvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 xxz { get { return new dvec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 xyx { get { return new dvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xyy { get { return new dvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 xyz { get { return new dvec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 xzx { get { return new dvec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xzy { get { return new dvec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 xzz { get { return new dvec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 yxx { get { return new dvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 yxy { get { return new dvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 yxz { get { return new dvec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 yyx { get { return new dvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 yyy { get { return new dvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 yyz { get { return new dvec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 yzx { get { return new dvec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 yzy { get { return new dvec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 yzz { get { return new dvec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 zxx { get { return new dvec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 zxy { get { return new dvec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 zxz { get { return new dvec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 zyx { get { return new dvec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 zyy { get { return new dvec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 zyz { get { return new dvec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 zzx { get { return new dvec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 zzy { get { return new dvec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 zzz { get { return new dvec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public dvec4 xxxx { get { return new dvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxxy { get { return new dvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xxxz { get { return new dvec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xxyx { get { return new dvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxyy { get { return new dvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xxyz { get { return new dvec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xxzx { get { return new dvec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxzy { get { return new dvec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xxzz { get { return new dvec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xyxx { get { return new dvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xyxy { get { return new dvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xyxz { get { return new dvec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xyyx { get { return new dvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xyyy { get { return new dvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xyyz { get { return new dvec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xyzx { get { return new dvec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xyzy { get { return new dvec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xyzz { get { return new dvec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xzxx { get { return new dvec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xzxy { get { return new dvec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xzxz { get { return new dvec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xzyx { get { return new dvec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xzyy { get { return new dvec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xzyz { get { return new dvec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xzzx { get { return new dvec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xzzy { get { return new dvec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xzzz { get { return new dvec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yxxx { get { return new dvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxxy { get { return new dvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxxz { get { return new dvec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yxyx { get { return new dvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxyy { get { return new dvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxyz { get { return new dvec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yxzx { get { return new dvec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxzy { get { return new dvec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxzz { get { return new dvec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yyxx { get { return new dvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yyxy { get { return new dvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yyxz { get { return new dvec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yyyx { get { return new dvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yyyy { get { return new dvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yyyz { get { return new dvec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yyzx { get { return new dvec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yyzy { get { return new dvec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yyzz { get { return new dvec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yzxx { get { return new dvec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yzxy { get { return new dvec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yzxz { get { return new dvec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yzyx { get { return new dvec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yzyy { get { return new dvec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yzyz { get { return new dvec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yzzx { get { return new dvec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yzzy { get { return new dvec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yzzz { get { return new dvec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zxxx { get { return new dvec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zxxy { get { return new dvec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zxxz { get { return new dvec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zxyx { get { return new dvec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zxyy { get { return new dvec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zxyz { get { return new dvec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zxzx { get { return new dvec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zxzy { get { return new dvec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zxzz { get { return new dvec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zyxx { get { return new dvec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zyxy { get { return new dvec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zyxz { get { return new dvec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zyyx { get { return new dvec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zyyy { get { return new dvec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zyyz { get { return new dvec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zyzx { get { return new dvec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zyzy { get { return new dvec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zyzz { get { return new dvec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zzxx { get { return new dvec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zzxy { get { return new dvec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zzxz { get { return new dvec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zzyx { get { return new dvec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zzyy { get { return new dvec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zzyz { get { return new dvec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zzzx { get { return new dvec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zzzy { get { return new dvec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zzzz { get { return new dvec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }

        #endregion
    }

    class bvec3 : tvec3<bool>
    {
        #region dvec3

        public bvec3() : base() { }
        public bvec3(bool a) : base(a, a, a) { }
        public bvec3(bool x, bool y, bool z) : base(x, y, z) { }

        #endregion

        #region Generated

        public bool x { get { return v[0]; } set { v[0] = value; } }
        public bool y { get { return v[1]; } set { v[1] = value; } }
        public bool z { get { return v[2]; } set { v[2] = value; } }
        public bvec2 xx { get { return new bvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 xy { get { return new bvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public bvec2 xz { get { return new bvec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public bvec2 yx { get { return new bvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 yy { get { return new bvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public bvec2 yz { get { return new bvec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public bvec2 zx { get { return new bvec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 zy { get { return new bvec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public bvec2 zz { get { return new bvec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public bvec3 xxx { get { return new bvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xxy { get { return new bvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 xxz { get { return new bvec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 xyx { get { return new bvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xyy { get { return new bvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 xyz { get { return new bvec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 xzx { get { return new bvec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xzy { get { return new bvec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 xzz { get { return new bvec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 yxx { get { return new bvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 yxy { get { return new bvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 yxz { get { return new bvec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 yyx { get { return new bvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 yyy { get { return new bvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 yyz { get { return new bvec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 yzx { get { return new bvec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 yzy { get { return new bvec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 yzz { get { return new bvec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 zxx { get { return new bvec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 zxy { get { return new bvec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 zxz { get { return new bvec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 zyx { get { return new bvec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 zyy { get { return new bvec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 zyz { get { return new bvec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 zzx { get { return new bvec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 zzy { get { return new bvec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 zzz { get { return new bvec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public bvec4 xxxx { get { return new bvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxxy { get { return new bvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xxxz { get { return new bvec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xxyx { get { return new bvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxyy { get { return new bvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xxyz { get { return new bvec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xxzx { get { return new bvec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxzy { get { return new bvec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xxzz { get { return new bvec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xyxx { get { return new bvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xyxy { get { return new bvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xyxz { get { return new bvec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xyyx { get { return new bvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xyyy { get { return new bvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xyyz { get { return new bvec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xyzx { get { return new bvec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xyzy { get { return new bvec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xyzz { get { return new bvec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xzxx { get { return new bvec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xzxy { get { return new bvec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xzxz { get { return new bvec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xzyx { get { return new bvec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xzyy { get { return new bvec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xzyz { get { return new bvec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xzzx { get { return new bvec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xzzy { get { return new bvec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xzzz { get { return new bvec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yxxx { get { return new bvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxxy { get { return new bvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxxz { get { return new bvec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yxyx { get { return new bvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxyy { get { return new bvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxyz { get { return new bvec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yxzx { get { return new bvec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxzy { get { return new bvec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxzz { get { return new bvec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yyxx { get { return new bvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yyxy { get { return new bvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yyxz { get { return new bvec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yyyx { get { return new bvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yyyy { get { return new bvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yyyz { get { return new bvec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yyzx { get { return new bvec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yyzy { get { return new bvec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yyzz { get { return new bvec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yzxx { get { return new bvec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yzxy { get { return new bvec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yzxz { get { return new bvec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yzyx { get { return new bvec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yzyy { get { return new bvec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yzyz { get { return new bvec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yzzx { get { return new bvec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yzzy { get { return new bvec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yzzz { get { return new bvec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zxxx { get { return new bvec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zxxy { get { return new bvec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zxxz { get { return new bvec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zxyx { get { return new bvec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zxyy { get { return new bvec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zxyz { get { return new bvec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zxzx { get { return new bvec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zxzy { get { return new bvec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zxzz { get { return new bvec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zyxx { get { return new bvec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zyxy { get { return new bvec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zyxz { get { return new bvec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zyyx { get { return new bvec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zyyy { get { return new bvec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zyyz { get { return new bvec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zyzx { get { return new bvec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zyzy { get { return new bvec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zyzz { get { return new bvec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zzxx { get { return new bvec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zzxy { get { return new bvec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zzxz { get { return new bvec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zzyx { get { return new bvec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zzyy { get { return new bvec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zzyz { get { return new bvec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zzzx { get { return new bvec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zzzy { get { return new bvec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zzzz { get { return new bvec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }

        #endregion
    }

    class ivec3 : tvec3<int>
    {
        #region dvec3

        public ivec3() : base() { }
        public ivec3(int a) : base(a, a, a) { }
        public ivec3(int x, int y, int z) : base(x, y, z) { }

        #endregion

        #region Generated

        public int x { get { return v[0]; } set { v[0] = value; } }
        public int y { get { return v[1]; } set { v[1] = value; } }
        public int z { get { return v[2]; } set { v[2] = value; } }
        public ivec2 xx { get { return new ivec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 xy { get { return new ivec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public ivec2 xz { get { return new ivec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public ivec2 yx { get { return new ivec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 yy { get { return new ivec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public ivec2 yz { get { return new ivec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public ivec2 zx { get { return new ivec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 zy { get { return new ivec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public ivec2 zz { get { return new ivec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public ivec3 xxx { get { return new ivec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xxy { get { return new ivec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 xxz { get { return new ivec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 xyx { get { return new ivec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xyy { get { return new ivec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 xyz { get { return new ivec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 xzx { get { return new ivec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xzy { get { return new ivec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 xzz { get { return new ivec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 yxx { get { return new ivec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 yxy { get { return new ivec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 yxz { get { return new ivec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 yyx { get { return new ivec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 yyy { get { return new ivec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 yyz { get { return new ivec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 yzx { get { return new ivec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 yzy { get { return new ivec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 yzz { get { return new ivec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 zxx { get { return new ivec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 zxy { get { return new ivec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 zxz { get { return new ivec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 zyx { get { return new ivec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 zyy { get { return new ivec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 zyz { get { return new ivec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 zzx { get { return new ivec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 zzy { get { return new ivec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 zzz { get { return new ivec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public ivec4 xxxx { get { return new ivec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxxy { get { return new ivec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xxxz { get { return new ivec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xxyx { get { return new ivec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxyy { get { return new ivec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xxyz { get { return new ivec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xxzx { get { return new ivec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxzy { get { return new ivec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xxzz { get { return new ivec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xyxx { get { return new ivec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xyxy { get { return new ivec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xyxz { get { return new ivec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xyyx { get { return new ivec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xyyy { get { return new ivec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xyyz { get { return new ivec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xyzx { get { return new ivec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xyzy { get { return new ivec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xyzz { get { return new ivec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xzxx { get { return new ivec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xzxy { get { return new ivec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xzxz { get { return new ivec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xzyx { get { return new ivec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xzyy { get { return new ivec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xzyz { get { return new ivec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xzzx { get { return new ivec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xzzy { get { return new ivec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xzzz { get { return new ivec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yxxx { get { return new ivec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxxy { get { return new ivec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxxz { get { return new ivec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yxyx { get { return new ivec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxyy { get { return new ivec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxyz { get { return new ivec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yxzx { get { return new ivec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxzy { get { return new ivec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxzz { get { return new ivec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yyxx { get { return new ivec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yyxy { get { return new ivec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yyxz { get { return new ivec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yyyx { get { return new ivec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yyyy { get { return new ivec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yyyz { get { return new ivec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yyzx { get { return new ivec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yyzy { get { return new ivec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yyzz { get { return new ivec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yzxx { get { return new ivec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yzxy { get { return new ivec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yzxz { get { return new ivec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yzyx { get { return new ivec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yzyy { get { return new ivec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yzyz { get { return new ivec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yzzx { get { return new ivec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yzzy { get { return new ivec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yzzz { get { return new ivec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zxxx { get { return new ivec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zxxy { get { return new ivec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zxxz { get { return new ivec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zxyx { get { return new ivec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zxyy { get { return new ivec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zxyz { get { return new ivec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zxzx { get { return new ivec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zxzy { get { return new ivec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zxzz { get { return new ivec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zyxx { get { return new ivec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zyxy { get { return new ivec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zyxz { get { return new ivec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zyyx { get { return new ivec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zyyy { get { return new ivec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zyyz { get { return new ivec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zyzx { get { return new ivec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zyzy { get { return new ivec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zyzz { get { return new ivec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zzxx { get { return new ivec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zzxy { get { return new ivec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zzxz { get { return new ivec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zzyx { get { return new ivec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zzyy { get { return new ivec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zzyz { get { return new ivec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zzzx { get { return new ivec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zzzy { get { return new ivec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zzzz { get { return new ivec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }

        #endregion
    }

    class uvec3 : tvec3<uint>
    {
        #region dvec3

        public uvec3() : base() { }
        public uvec3(uint a) : base(a, a, a) { }
        public uvec3(uint x, uint y, uint z) : base(x, y, z) { }

        #endregion

        #region Generated

        public uint x { get { return v[0]; } set { v[0] = value; } }
        public uint y { get { return v[1]; } set { v[1] = value; } }
        public uint z { get { return v[2]; } set { v[2] = value; } }
        public uvec2 xx { get { return new uvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 xy { get { return new uvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public uvec2 xz { get { return new uvec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public uvec2 yx { get { return new uvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 yy { get { return new uvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public uvec2 yz { get { return new uvec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public uvec2 zx { get { return new uvec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 zy { get { return new uvec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public uvec2 zz { get { return new uvec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public uvec3 xxx { get { return new uvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xxy { get { return new uvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 xxz { get { return new uvec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 xyx { get { return new uvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xyy { get { return new uvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 xyz { get { return new uvec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 xzx { get { return new uvec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xzy { get { return new uvec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 xzz { get { return new uvec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 yxx { get { return new uvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 yxy { get { return new uvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 yxz { get { return new uvec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 yyx { get { return new uvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 yyy { get { return new uvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 yyz { get { return new uvec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 yzx { get { return new uvec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 yzy { get { return new uvec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 yzz { get { return new uvec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 zxx { get { return new uvec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 zxy { get { return new uvec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 zxz { get { return new uvec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 zyx { get { return new uvec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 zyy { get { return new uvec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 zyz { get { return new uvec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 zzx { get { return new uvec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 zzy { get { return new uvec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 zzz { get { return new uvec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public uvec4 xxxx { get { return new uvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxxy { get { return new uvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xxxz { get { return new uvec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xxyx { get { return new uvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxyy { get { return new uvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xxyz { get { return new uvec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xxzx { get { return new uvec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxzy { get { return new uvec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xxzz { get { return new uvec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xyxx { get { return new uvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xyxy { get { return new uvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xyxz { get { return new uvec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xyyx { get { return new uvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xyyy { get { return new uvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xyyz { get { return new uvec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xyzx { get { return new uvec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xyzy { get { return new uvec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xyzz { get { return new uvec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xzxx { get { return new uvec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xzxy { get { return new uvec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xzxz { get { return new uvec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xzyx { get { return new uvec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xzyy { get { return new uvec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xzyz { get { return new uvec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xzzx { get { return new uvec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xzzy { get { return new uvec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xzzz { get { return new uvec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yxxx { get { return new uvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxxy { get { return new uvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxxz { get { return new uvec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yxyx { get { return new uvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxyy { get { return new uvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxyz { get { return new uvec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yxzx { get { return new uvec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxzy { get { return new uvec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxzz { get { return new uvec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yyxx { get { return new uvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yyxy { get { return new uvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yyxz { get { return new uvec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yyyx { get { return new uvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yyyy { get { return new uvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yyyz { get { return new uvec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yyzx { get { return new uvec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yyzy { get { return new uvec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yyzz { get { return new uvec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yzxx { get { return new uvec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yzxy { get { return new uvec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yzxz { get { return new uvec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yzyx { get { return new uvec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yzyy { get { return new uvec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yzyz { get { return new uvec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yzzx { get { return new uvec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yzzy { get { return new uvec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yzzz { get { return new uvec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zxxx { get { return new uvec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zxxy { get { return new uvec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zxxz { get { return new uvec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zxyx { get { return new uvec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zxyy { get { return new uvec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zxyz { get { return new uvec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zxzx { get { return new uvec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zxzy { get { return new uvec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zxzz { get { return new uvec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zyxx { get { return new uvec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zyxy { get { return new uvec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zyxz { get { return new uvec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zyyx { get { return new uvec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zyyy { get { return new uvec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zyyz { get { return new uvec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zyzx { get { return new uvec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zyzy { get { return new uvec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zyzz { get { return new uvec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zzxx { get { return new uvec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zzxy { get { return new uvec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zzxz { get { return new uvec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zzyx { get { return new uvec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zzyy { get { return new uvec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zzyz { get { return new uvec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zzzx { get { return new uvec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zzzy { get { return new uvec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zzzz { get { return new uvec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }

        #endregion
    }

    class vec4 : tvec4<float>
    {
        #region vec4

        public vec4() { }
        public vec4(float a) : base(a, a, a, a) { }
        public vec4(float X, float Y, float Z, float W) : base(X, Y, Z, W) { }

        #endregion

        #region Operators

        public static vec4 operator +(vec4 a) => new vec4(a.x, a.y, a.z, a.w);
        public static vec4 operator -(vec4 a) => new vec4(-a.x, -a.y, -a.z, -a.w);
        public static vec4 operator +(vec4 a, vec4 b) => new vec4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static vec4 operator +(vec4 a, float b) => new vec4(a.x + b, a.y + b, a.z + b, a.w + b);
        public static vec4 operator +(float a, vec4 b) => new vec4(a + b.x, a + b.y, a + b.z, a + b.w);
        public static vec4 operator -(vec4 a, vec4 b) => new vec4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static vec4 operator -(vec4 a, float b) => new vec4(a.x - b, a.y - b, a.z - b, a.w - b);
        public static vec4 operator -(float a, vec4 b) => new vec4(a - b.x, a - b.y, a - b.z, a - b.w);
        public static vec4 operator *(vec4 a, vec4 b) => new vec4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        public static vec4 operator *(vec4 a, float b) => new vec4(a.x * b, a.y * b, a.z * b, a.w * b);
        public static vec4 operator *(float a, vec4 b) => new vec4(a * b.x, a * b.y, a * b.z, a * b.w);
        public static vec4 operator /(vec4 a, vec4 b) => new vec4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        public static vec4 operator /(vec4 a, float b) => new vec4(a.x / b, a.y / b, a.z / b, a.w / b);
        public static vec4 operator /(float a, vec4 b) => new vec4(a / b.x, a / b.y, a / b.z, a / b.w);

        #endregion

        #region Generated

        public float x { get { return v[0]; } set { v[0] = value; } }
        public float y { get { return v[1]; } set { v[1] = value; } }
        public float z { get { return v[2]; } set { v[2] = value; } }
        public float w { get { return v[3]; } set { v[3] = value; } }
        public vec2 xx { get { return new vec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 xy { get { return new vec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public vec2 xz { get { return new vec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public vec2 xw { get { return new vec2(v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; } }
        public vec2 yx { get { return new vec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 yy { get { return new vec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public vec2 yz { get { return new vec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public vec2 yw { get { return new vec2(v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; } }
        public vec2 zx { get { return new vec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 zy { get { return new vec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public vec2 zz { get { return new vec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public vec2 zw { get { return new vec2(v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; } }
        public vec2 wx { get { return new vec2(v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; } }
        public vec2 wy { get { return new vec2(v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; } }
        public vec2 wz { get { return new vec2(v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; } }
        public vec2 ww { get { return new vec2(v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; } }
        public vec3 xxx { get { return new vec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xxy { get { return new vec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 xxz { get { return new vec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 xxw { get { return new vec3(v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 xyx { get { return new vec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xyy { get { return new vec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 xyz { get { return new vec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 xyw { get { return new vec3(v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 xzx { get { return new vec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xzy { get { return new vec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 xzz { get { return new vec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 xzw { get { return new vec3(v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 xwx { get { return new vec3(v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 xwy { get { return new vec3(v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 xwz { get { return new vec3(v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 xww { get { return new vec3(v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 yxx { get { return new vec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 yxy { get { return new vec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 yxz { get { return new vec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 yxw { get { return new vec3(v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 yyx { get { return new vec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 yyy { get { return new vec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 yyz { get { return new vec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 yyw { get { return new vec3(v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 yzx { get { return new vec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 yzy { get { return new vec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 yzz { get { return new vec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 yzw { get { return new vec3(v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 ywx { get { return new vec3(v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 ywy { get { return new vec3(v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 ywz { get { return new vec3(v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 yww { get { return new vec3(v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 zxx { get { return new vec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 zxy { get { return new vec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 zxz { get { return new vec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 zxw { get { return new vec3(v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 zyx { get { return new vec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 zyy { get { return new vec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 zyz { get { return new vec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 zyw { get { return new vec3(v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 zzx { get { return new vec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 zzy { get { return new vec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 zzz { get { return new vec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 zzw { get { return new vec3(v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 zwx { get { return new vec3(v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 zwy { get { return new vec3(v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 zwz { get { return new vec3(v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 zww { get { return new vec3(v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 wxx { get { return new vec3(v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 wxy { get { return new vec3(v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 wxz { get { return new vec3(v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 wxw { get { return new vec3(v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 wyx { get { return new vec3(v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 wyy { get { return new vec3(v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 wyz { get { return new vec3(v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 wyw { get { return new vec3(v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 wzx { get { return new vec3(v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 wzy { get { return new vec3(v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 wzz { get { return new vec3(v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 wzw { get { return new vec3(v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public vec3 wwx { get { return new vec3(v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public vec3 wwy { get { return new vec3(v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public vec3 wwz { get { return new vec3(v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public vec3 www { get { return new vec3(v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public vec4 xxxx { get { return new vec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxxy { get { return new vec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xxxz { get { return new vec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xxxw { get { return new vec4(v[0], v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xxyx { get { return new vec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxyy { get { return new vec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xxyz { get { return new vec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xxyw { get { return new vec4(v[0], v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xxzx { get { return new vec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxzy { get { return new vec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xxzz { get { return new vec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xxzw { get { return new vec4(v[0], v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xxwx { get { return new vec4(v[0], v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xxwy { get { return new vec4(v[0], v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xxwz { get { return new vec4(v[0], v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xxww { get { return new vec4(v[0], v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xyxx { get { return new vec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xyxy { get { return new vec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xyxz { get { return new vec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xyxw { get { return new vec4(v[0], v[1], v[0], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xyyx { get { return new vec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xyyy { get { return new vec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xyyz { get { return new vec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xyyw { get { return new vec4(v[0], v[1], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xyzx { get { return new vec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xyzy { get { return new vec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xyzz { get { return new vec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xyzw { get { return new vec4(v[0], v[1], v[2], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xywx { get { return new vec4(v[0], v[1], v[3], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xywy { get { return new vec4(v[0], v[1], v[3], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xywz { get { return new vec4(v[0], v[1], v[3], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xyww { get { return new vec4(v[0], v[1], v[3], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xzxx { get { return new vec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xzxy { get { return new vec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xzxz { get { return new vec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xzxw { get { return new vec4(v[0], v[2], v[0], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xzyx { get { return new vec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xzyy { get { return new vec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xzyz { get { return new vec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xzyw { get { return new vec4(v[0], v[2], v[1], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xzzx { get { return new vec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xzzy { get { return new vec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xzzz { get { return new vec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xzzw { get { return new vec4(v[0], v[2], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xzwx { get { return new vec4(v[0], v[2], v[3], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xzwy { get { return new vec4(v[0], v[2], v[3], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xzwz { get { return new vec4(v[0], v[2], v[3], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xzww { get { return new vec4(v[0], v[2], v[3], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xwxx { get { return new vec4(v[0], v[3], v[0], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xwxy { get { return new vec4(v[0], v[3], v[0], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xwxz { get { return new vec4(v[0], v[3], v[0], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xwxw { get { return new vec4(v[0], v[3], v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xwyx { get { return new vec4(v[0], v[3], v[1], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xwyy { get { return new vec4(v[0], v[3], v[1], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xwyz { get { return new vec4(v[0], v[3], v[1], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xwyw { get { return new vec4(v[0], v[3], v[1], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xwzx { get { return new vec4(v[0], v[3], v[2], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xwzy { get { return new vec4(v[0], v[3], v[2], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xwzz { get { return new vec4(v[0], v[3], v[2], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xwzw { get { return new vec4(v[0], v[3], v[2], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 xwwx { get { return new vec4(v[0], v[3], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 xwwy { get { return new vec4(v[0], v[3], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 xwwz { get { return new vec4(v[0], v[3], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 xwww { get { return new vec4(v[0], v[3], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yxxx { get { return new vec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxxy { get { return new vec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxxz { get { return new vec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yxxw { get { return new vec4(v[1], v[0], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yxyx { get { return new vec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxyy { get { return new vec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxyz { get { return new vec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yxyw { get { return new vec4(v[1], v[0], v[1], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yxzx { get { return new vec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxzy { get { return new vec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxzz { get { return new vec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yxzw { get { return new vec4(v[1], v[0], v[2], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yxwx { get { return new vec4(v[1], v[0], v[3], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yxwy { get { return new vec4(v[1], v[0], v[3], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yxwz { get { return new vec4(v[1], v[0], v[3], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yxww { get { return new vec4(v[1], v[0], v[3], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yyxx { get { return new vec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yyxy { get { return new vec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yyxz { get { return new vec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yyxw { get { return new vec4(v[1], v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yyyx { get { return new vec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yyyy { get { return new vec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yyyz { get { return new vec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yyyw { get { return new vec4(v[1], v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yyzx { get { return new vec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yyzy { get { return new vec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yyzz { get { return new vec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yyzw { get { return new vec4(v[1], v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yywx { get { return new vec4(v[1], v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yywy { get { return new vec4(v[1], v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yywz { get { return new vec4(v[1], v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yyww { get { return new vec4(v[1], v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yzxx { get { return new vec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yzxy { get { return new vec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yzxz { get { return new vec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yzxw { get { return new vec4(v[1], v[2], v[0], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yzyx { get { return new vec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yzyy { get { return new vec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yzyz { get { return new vec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yzyw { get { return new vec4(v[1], v[2], v[1], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yzzx { get { return new vec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yzzy { get { return new vec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yzzz { get { return new vec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yzzw { get { return new vec4(v[1], v[2], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 yzwx { get { return new vec4(v[1], v[2], v[3], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 yzwy { get { return new vec4(v[1], v[2], v[3], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 yzwz { get { return new vec4(v[1], v[2], v[3], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 yzww { get { return new vec4(v[1], v[2], v[3], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 ywxx { get { return new vec4(v[1], v[3], v[0], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 ywxy { get { return new vec4(v[1], v[3], v[0], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 ywxz { get { return new vec4(v[1], v[3], v[0], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 ywxw { get { return new vec4(v[1], v[3], v[0], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 ywyx { get { return new vec4(v[1], v[3], v[1], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 ywyy { get { return new vec4(v[1], v[3], v[1], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 ywyz { get { return new vec4(v[1], v[3], v[1], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 ywyw { get { return new vec4(v[1], v[3], v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 ywzx { get { return new vec4(v[1], v[3], v[2], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 ywzy { get { return new vec4(v[1], v[3], v[2], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 ywzz { get { return new vec4(v[1], v[3], v[2], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 ywzw { get { return new vec4(v[1], v[3], v[2], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 ywwx { get { return new vec4(v[1], v[3], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 ywwy { get { return new vec4(v[1], v[3], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 ywwz { get { return new vec4(v[1], v[3], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 ywww { get { return new vec4(v[1], v[3], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zxxx { get { return new vec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zxxy { get { return new vec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zxxz { get { return new vec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zxxw { get { return new vec4(v[2], v[0], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zxyx { get { return new vec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zxyy { get { return new vec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zxyz { get { return new vec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zxyw { get { return new vec4(v[2], v[0], v[1], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zxzx { get { return new vec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zxzy { get { return new vec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zxzz { get { return new vec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zxzw { get { return new vec4(v[2], v[0], v[2], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zxwx { get { return new vec4(v[2], v[0], v[3], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zxwy { get { return new vec4(v[2], v[0], v[3], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zxwz { get { return new vec4(v[2], v[0], v[3], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zxww { get { return new vec4(v[2], v[0], v[3], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zyxx { get { return new vec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zyxy { get { return new vec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zyxz { get { return new vec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zyxw { get { return new vec4(v[2], v[1], v[0], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zyyx { get { return new vec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zyyy { get { return new vec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zyyz { get { return new vec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zyyw { get { return new vec4(v[2], v[1], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zyzx { get { return new vec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zyzy { get { return new vec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zyzz { get { return new vec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zyzw { get { return new vec4(v[2], v[1], v[2], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zywx { get { return new vec4(v[2], v[1], v[3], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zywy { get { return new vec4(v[2], v[1], v[3], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zywz { get { return new vec4(v[2], v[1], v[3], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zyww { get { return new vec4(v[2], v[1], v[3], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zzxx { get { return new vec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zzxy { get { return new vec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zzxz { get { return new vec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zzxw { get { return new vec4(v[2], v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zzyx { get { return new vec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zzyy { get { return new vec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zzyz { get { return new vec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zzyw { get { return new vec4(v[2], v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zzzx { get { return new vec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zzzy { get { return new vec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zzzz { get { return new vec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zzzw { get { return new vec4(v[2], v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zzwx { get { return new vec4(v[2], v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zzwy { get { return new vec4(v[2], v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zzwz { get { return new vec4(v[2], v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zzww { get { return new vec4(v[2], v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zwxx { get { return new vec4(v[2], v[3], v[0], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zwxy { get { return new vec4(v[2], v[3], v[0], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zwxz { get { return new vec4(v[2], v[3], v[0], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zwxw { get { return new vec4(v[2], v[3], v[0], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zwyx { get { return new vec4(v[2], v[3], v[1], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zwyy { get { return new vec4(v[2], v[3], v[1], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zwyz { get { return new vec4(v[2], v[3], v[1], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zwyw { get { return new vec4(v[2], v[3], v[1], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zwzx { get { return new vec4(v[2], v[3], v[2], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zwzy { get { return new vec4(v[2], v[3], v[2], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zwzz { get { return new vec4(v[2], v[3], v[2], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zwzw { get { return new vec4(v[2], v[3], v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 zwwx { get { return new vec4(v[2], v[3], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 zwwy { get { return new vec4(v[2], v[3], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 zwwz { get { return new vec4(v[2], v[3], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 zwww { get { return new vec4(v[2], v[3], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wxxx { get { return new vec4(v[3], v[0], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wxxy { get { return new vec4(v[3], v[0], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wxxz { get { return new vec4(v[3], v[0], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wxxw { get { return new vec4(v[3], v[0], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wxyx { get { return new vec4(v[3], v[0], v[1], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wxyy { get { return new vec4(v[3], v[0], v[1], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wxyz { get { return new vec4(v[3], v[0], v[1], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wxyw { get { return new vec4(v[3], v[0], v[1], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wxzx { get { return new vec4(v[3], v[0], v[2], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wxzy { get { return new vec4(v[3], v[0], v[2], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wxzz { get { return new vec4(v[3], v[0], v[2], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wxzw { get { return new vec4(v[3], v[0], v[2], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wxwx { get { return new vec4(v[3], v[0], v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wxwy { get { return new vec4(v[3], v[0], v[3], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wxwz { get { return new vec4(v[3], v[0], v[3], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wxww { get { return new vec4(v[3], v[0], v[3], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wyxx { get { return new vec4(v[3], v[1], v[0], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wyxy { get { return new vec4(v[3], v[1], v[0], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wyxz { get { return new vec4(v[3], v[1], v[0], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wyxw { get { return new vec4(v[3], v[1], v[0], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wyyx { get { return new vec4(v[3], v[1], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wyyy { get { return new vec4(v[3], v[1], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wyyz { get { return new vec4(v[3], v[1], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wyyw { get { return new vec4(v[3], v[1], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wyzx { get { return new vec4(v[3], v[1], v[2], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wyzy { get { return new vec4(v[3], v[1], v[2], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wyzz { get { return new vec4(v[3], v[1], v[2], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wyzw { get { return new vec4(v[3], v[1], v[2], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wywx { get { return new vec4(v[3], v[1], v[3], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wywy { get { return new vec4(v[3], v[1], v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wywz { get { return new vec4(v[3], v[1], v[3], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wyww { get { return new vec4(v[3], v[1], v[3], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wzxx { get { return new vec4(v[3], v[2], v[0], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wzxy { get { return new vec4(v[3], v[2], v[0], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wzxz { get { return new vec4(v[3], v[2], v[0], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wzxw { get { return new vec4(v[3], v[2], v[0], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wzyx { get { return new vec4(v[3], v[2], v[1], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wzyy { get { return new vec4(v[3], v[2], v[1], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wzyz { get { return new vec4(v[3], v[2], v[1], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wzyw { get { return new vec4(v[3], v[2], v[1], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wzzx { get { return new vec4(v[3], v[2], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wzzy { get { return new vec4(v[3], v[2], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wzzz { get { return new vec4(v[3], v[2], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wzzw { get { return new vec4(v[3], v[2], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wzwx { get { return new vec4(v[3], v[2], v[3], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wzwy { get { return new vec4(v[3], v[2], v[3], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wzwz { get { return new vec4(v[3], v[2], v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wzww { get { return new vec4(v[3], v[2], v[3], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wwxx { get { return new vec4(v[3], v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wwxy { get { return new vec4(v[3], v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wwxz { get { return new vec4(v[3], v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wwxw { get { return new vec4(v[3], v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wwyx { get { return new vec4(v[3], v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wwyy { get { return new vec4(v[3], v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wwyz { get { return new vec4(v[3], v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wwyw { get { return new vec4(v[3], v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wwzx { get { return new vec4(v[3], v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wwzy { get { return new vec4(v[3], v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wwzz { get { return new vec4(v[3], v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wwzw { get { return new vec4(v[3], v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public vec4 wwwx { get { return new vec4(v[3], v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public vec4 wwwy { get { return new vec4(v[3], v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public vec4 wwwz { get { return new vec4(v[3], v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public vec4 wwww { get { return new vec4(v[3], v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }

        #endregion
    }

    class dvec4 : tvec4<double>
    {
        #region dvec4

        public dvec4() { }
        public dvec4(double a) : base(a, a, a, a) { }
        public dvec4(double X, double Y, double Z, double W) : base(X, Y, Z, W) { }

        #endregion

        #region Generated

        public double x { get { return v[0]; } set { v[0] = value; } }
        public double y { get { return v[1]; } set { v[1] = value; } }
        public double z { get { return v[2]; } set { v[2] = value; } }
        public double w { get { return v[3]; } set { v[3] = value; } }
        public dvec2 xx { get { return new dvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 xy { get { return new dvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public dvec2 xz { get { return new dvec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public dvec2 xw { get { return new dvec2(v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; } }
        public dvec2 yx { get { return new dvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 yy { get { return new dvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public dvec2 yz { get { return new dvec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public dvec2 yw { get { return new dvec2(v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; } }
        public dvec2 zx { get { return new dvec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 zy { get { return new dvec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public dvec2 zz { get { return new dvec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public dvec2 zw { get { return new dvec2(v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; } }
        public dvec2 wx { get { return new dvec2(v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; } }
        public dvec2 wy { get { return new dvec2(v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; } }
        public dvec2 wz { get { return new dvec2(v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; } }
        public dvec2 ww { get { return new dvec2(v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; } }
        public dvec3 xxx { get { return new dvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xxy { get { return new dvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 xxz { get { return new dvec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 xxw { get { return new dvec3(v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 xyx { get { return new dvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xyy { get { return new dvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 xyz { get { return new dvec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 xyw { get { return new dvec3(v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 xzx { get { return new dvec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xzy { get { return new dvec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 xzz { get { return new dvec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 xzw { get { return new dvec3(v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 xwx { get { return new dvec3(v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 xwy { get { return new dvec3(v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 xwz { get { return new dvec3(v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 xww { get { return new dvec3(v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 yxx { get { return new dvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 yxy { get { return new dvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 yxz { get { return new dvec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 yxw { get { return new dvec3(v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 yyx { get { return new dvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 yyy { get { return new dvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 yyz { get { return new dvec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 yyw { get { return new dvec3(v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 yzx { get { return new dvec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 yzy { get { return new dvec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 yzz { get { return new dvec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 yzw { get { return new dvec3(v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 ywx { get { return new dvec3(v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 ywy { get { return new dvec3(v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 ywz { get { return new dvec3(v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 yww { get { return new dvec3(v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 zxx { get { return new dvec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 zxy { get { return new dvec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 zxz { get { return new dvec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 zxw { get { return new dvec3(v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 zyx { get { return new dvec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 zyy { get { return new dvec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 zyz { get { return new dvec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 zyw { get { return new dvec3(v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 zzx { get { return new dvec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 zzy { get { return new dvec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 zzz { get { return new dvec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 zzw { get { return new dvec3(v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 zwx { get { return new dvec3(v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 zwy { get { return new dvec3(v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 zwz { get { return new dvec3(v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 zww { get { return new dvec3(v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 wxx { get { return new dvec3(v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 wxy { get { return new dvec3(v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 wxz { get { return new dvec3(v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 wxw { get { return new dvec3(v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 wyx { get { return new dvec3(v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 wyy { get { return new dvec3(v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 wyz { get { return new dvec3(v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 wyw { get { return new dvec3(v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 wzx { get { return new dvec3(v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 wzy { get { return new dvec3(v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 wzz { get { return new dvec3(v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 wzw { get { return new dvec3(v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public dvec3 wwx { get { return new dvec3(v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public dvec3 wwy { get { return new dvec3(v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public dvec3 wwz { get { return new dvec3(v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public dvec3 www { get { return new dvec3(v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public dvec4 xxxx { get { return new dvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxxy { get { return new dvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xxxz { get { return new dvec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xxxw { get { return new dvec4(v[0], v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xxyx { get { return new dvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxyy { get { return new dvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xxyz { get { return new dvec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xxyw { get { return new dvec4(v[0], v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xxzx { get { return new dvec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxzy { get { return new dvec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xxzz { get { return new dvec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xxzw { get { return new dvec4(v[0], v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xxwx { get { return new dvec4(v[0], v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xxwy { get { return new dvec4(v[0], v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xxwz { get { return new dvec4(v[0], v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xxww { get { return new dvec4(v[0], v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xyxx { get { return new dvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xyxy { get { return new dvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xyxz { get { return new dvec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xyxw { get { return new dvec4(v[0], v[1], v[0], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xyyx { get { return new dvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xyyy { get { return new dvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xyyz { get { return new dvec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xyyw { get { return new dvec4(v[0], v[1], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xyzx { get { return new dvec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xyzy { get { return new dvec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xyzz { get { return new dvec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xyzw { get { return new dvec4(v[0], v[1], v[2], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xywx { get { return new dvec4(v[0], v[1], v[3], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xywy { get { return new dvec4(v[0], v[1], v[3], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xywz { get { return new dvec4(v[0], v[1], v[3], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xyww { get { return new dvec4(v[0], v[1], v[3], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xzxx { get { return new dvec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xzxy { get { return new dvec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xzxz { get { return new dvec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xzxw { get { return new dvec4(v[0], v[2], v[0], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xzyx { get { return new dvec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xzyy { get { return new dvec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xzyz { get { return new dvec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xzyw { get { return new dvec4(v[0], v[2], v[1], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xzzx { get { return new dvec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xzzy { get { return new dvec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xzzz { get { return new dvec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xzzw { get { return new dvec4(v[0], v[2], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xzwx { get { return new dvec4(v[0], v[2], v[3], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xzwy { get { return new dvec4(v[0], v[2], v[3], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xzwz { get { return new dvec4(v[0], v[2], v[3], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xzww { get { return new dvec4(v[0], v[2], v[3], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xwxx { get { return new dvec4(v[0], v[3], v[0], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xwxy { get { return new dvec4(v[0], v[3], v[0], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xwxz { get { return new dvec4(v[0], v[3], v[0], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xwxw { get { return new dvec4(v[0], v[3], v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xwyx { get { return new dvec4(v[0], v[3], v[1], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xwyy { get { return new dvec4(v[0], v[3], v[1], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xwyz { get { return new dvec4(v[0], v[3], v[1], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xwyw { get { return new dvec4(v[0], v[3], v[1], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xwzx { get { return new dvec4(v[0], v[3], v[2], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xwzy { get { return new dvec4(v[0], v[3], v[2], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xwzz { get { return new dvec4(v[0], v[3], v[2], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xwzw { get { return new dvec4(v[0], v[3], v[2], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 xwwx { get { return new dvec4(v[0], v[3], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 xwwy { get { return new dvec4(v[0], v[3], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 xwwz { get { return new dvec4(v[0], v[3], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 xwww { get { return new dvec4(v[0], v[3], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yxxx { get { return new dvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxxy { get { return new dvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxxz { get { return new dvec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yxxw { get { return new dvec4(v[1], v[0], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yxyx { get { return new dvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxyy { get { return new dvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxyz { get { return new dvec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yxyw { get { return new dvec4(v[1], v[0], v[1], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yxzx { get { return new dvec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxzy { get { return new dvec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxzz { get { return new dvec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yxzw { get { return new dvec4(v[1], v[0], v[2], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yxwx { get { return new dvec4(v[1], v[0], v[3], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yxwy { get { return new dvec4(v[1], v[0], v[3], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yxwz { get { return new dvec4(v[1], v[0], v[3], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yxww { get { return new dvec4(v[1], v[0], v[3], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yyxx { get { return new dvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yyxy { get { return new dvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yyxz { get { return new dvec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yyxw { get { return new dvec4(v[1], v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yyyx { get { return new dvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yyyy { get { return new dvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yyyz { get { return new dvec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yyyw { get { return new dvec4(v[1], v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yyzx { get { return new dvec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yyzy { get { return new dvec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yyzz { get { return new dvec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yyzw { get { return new dvec4(v[1], v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yywx { get { return new dvec4(v[1], v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yywy { get { return new dvec4(v[1], v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yywz { get { return new dvec4(v[1], v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yyww { get { return new dvec4(v[1], v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yzxx { get { return new dvec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yzxy { get { return new dvec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yzxz { get { return new dvec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yzxw { get { return new dvec4(v[1], v[2], v[0], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yzyx { get { return new dvec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yzyy { get { return new dvec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yzyz { get { return new dvec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yzyw { get { return new dvec4(v[1], v[2], v[1], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yzzx { get { return new dvec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yzzy { get { return new dvec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yzzz { get { return new dvec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yzzw { get { return new dvec4(v[1], v[2], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 yzwx { get { return new dvec4(v[1], v[2], v[3], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 yzwy { get { return new dvec4(v[1], v[2], v[3], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 yzwz { get { return new dvec4(v[1], v[2], v[3], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 yzww { get { return new dvec4(v[1], v[2], v[3], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 ywxx { get { return new dvec4(v[1], v[3], v[0], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 ywxy { get { return new dvec4(v[1], v[3], v[0], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 ywxz { get { return new dvec4(v[1], v[3], v[0], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 ywxw { get { return new dvec4(v[1], v[3], v[0], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 ywyx { get { return new dvec4(v[1], v[3], v[1], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 ywyy { get { return new dvec4(v[1], v[3], v[1], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 ywyz { get { return new dvec4(v[1], v[3], v[1], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 ywyw { get { return new dvec4(v[1], v[3], v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 ywzx { get { return new dvec4(v[1], v[3], v[2], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 ywzy { get { return new dvec4(v[1], v[3], v[2], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 ywzz { get { return new dvec4(v[1], v[3], v[2], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 ywzw { get { return new dvec4(v[1], v[3], v[2], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 ywwx { get { return new dvec4(v[1], v[3], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 ywwy { get { return new dvec4(v[1], v[3], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 ywwz { get { return new dvec4(v[1], v[3], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 ywww { get { return new dvec4(v[1], v[3], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zxxx { get { return new dvec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zxxy { get { return new dvec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zxxz { get { return new dvec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zxxw { get { return new dvec4(v[2], v[0], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zxyx { get { return new dvec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zxyy { get { return new dvec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zxyz { get { return new dvec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zxyw { get { return new dvec4(v[2], v[0], v[1], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zxzx { get { return new dvec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zxzy { get { return new dvec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zxzz { get { return new dvec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zxzw { get { return new dvec4(v[2], v[0], v[2], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zxwx { get { return new dvec4(v[2], v[0], v[3], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zxwy { get { return new dvec4(v[2], v[0], v[3], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zxwz { get { return new dvec4(v[2], v[0], v[3], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zxww { get { return new dvec4(v[2], v[0], v[3], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zyxx { get { return new dvec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zyxy { get { return new dvec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zyxz { get { return new dvec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zyxw { get { return new dvec4(v[2], v[1], v[0], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zyyx { get { return new dvec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zyyy { get { return new dvec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zyyz { get { return new dvec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zyyw { get { return new dvec4(v[2], v[1], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zyzx { get { return new dvec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zyzy { get { return new dvec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zyzz { get { return new dvec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zyzw { get { return new dvec4(v[2], v[1], v[2], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zywx { get { return new dvec4(v[2], v[1], v[3], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zywy { get { return new dvec4(v[2], v[1], v[3], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zywz { get { return new dvec4(v[2], v[1], v[3], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zyww { get { return new dvec4(v[2], v[1], v[3], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zzxx { get { return new dvec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zzxy { get { return new dvec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zzxz { get { return new dvec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zzxw { get { return new dvec4(v[2], v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zzyx { get { return new dvec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zzyy { get { return new dvec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zzyz { get { return new dvec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zzyw { get { return new dvec4(v[2], v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zzzx { get { return new dvec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zzzy { get { return new dvec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zzzz { get { return new dvec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zzzw { get { return new dvec4(v[2], v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zzwx { get { return new dvec4(v[2], v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zzwy { get { return new dvec4(v[2], v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zzwz { get { return new dvec4(v[2], v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zzww { get { return new dvec4(v[2], v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zwxx { get { return new dvec4(v[2], v[3], v[0], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zwxy { get { return new dvec4(v[2], v[3], v[0], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zwxz { get { return new dvec4(v[2], v[3], v[0], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zwxw { get { return new dvec4(v[2], v[3], v[0], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zwyx { get { return new dvec4(v[2], v[3], v[1], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zwyy { get { return new dvec4(v[2], v[3], v[1], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zwyz { get { return new dvec4(v[2], v[3], v[1], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zwyw { get { return new dvec4(v[2], v[3], v[1], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zwzx { get { return new dvec4(v[2], v[3], v[2], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zwzy { get { return new dvec4(v[2], v[3], v[2], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zwzz { get { return new dvec4(v[2], v[3], v[2], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zwzw { get { return new dvec4(v[2], v[3], v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 zwwx { get { return new dvec4(v[2], v[3], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 zwwy { get { return new dvec4(v[2], v[3], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 zwwz { get { return new dvec4(v[2], v[3], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 zwww { get { return new dvec4(v[2], v[3], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wxxx { get { return new dvec4(v[3], v[0], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wxxy { get { return new dvec4(v[3], v[0], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wxxz { get { return new dvec4(v[3], v[0], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wxxw { get { return new dvec4(v[3], v[0], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wxyx { get { return new dvec4(v[3], v[0], v[1], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wxyy { get { return new dvec4(v[3], v[0], v[1], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wxyz { get { return new dvec4(v[3], v[0], v[1], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wxyw { get { return new dvec4(v[3], v[0], v[1], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wxzx { get { return new dvec4(v[3], v[0], v[2], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wxzy { get { return new dvec4(v[3], v[0], v[2], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wxzz { get { return new dvec4(v[3], v[0], v[2], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wxzw { get { return new dvec4(v[3], v[0], v[2], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wxwx { get { return new dvec4(v[3], v[0], v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wxwy { get { return new dvec4(v[3], v[0], v[3], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wxwz { get { return new dvec4(v[3], v[0], v[3], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wxww { get { return new dvec4(v[3], v[0], v[3], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wyxx { get { return new dvec4(v[3], v[1], v[0], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wyxy { get { return new dvec4(v[3], v[1], v[0], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wyxz { get { return new dvec4(v[3], v[1], v[0], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wyxw { get { return new dvec4(v[3], v[1], v[0], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wyyx { get { return new dvec4(v[3], v[1], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wyyy { get { return new dvec4(v[3], v[1], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wyyz { get { return new dvec4(v[3], v[1], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wyyw { get { return new dvec4(v[3], v[1], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wyzx { get { return new dvec4(v[3], v[1], v[2], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wyzy { get { return new dvec4(v[3], v[1], v[2], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wyzz { get { return new dvec4(v[3], v[1], v[2], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wyzw { get { return new dvec4(v[3], v[1], v[2], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wywx { get { return new dvec4(v[3], v[1], v[3], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wywy { get { return new dvec4(v[3], v[1], v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wywz { get { return new dvec4(v[3], v[1], v[3], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wyww { get { return new dvec4(v[3], v[1], v[3], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wzxx { get { return new dvec4(v[3], v[2], v[0], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wzxy { get { return new dvec4(v[3], v[2], v[0], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wzxz { get { return new dvec4(v[3], v[2], v[0], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wzxw { get { return new dvec4(v[3], v[2], v[0], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wzyx { get { return new dvec4(v[3], v[2], v[1], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wzyy { get { return new dvec4(v[3], v[2], v[1], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wzyz { get { return new dvec4(v[3], v[2], v[1], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wzyw { get { return new dvec4(v[3], v[2], v[1], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wzzx { get { return new dvec4(v[3], v[2], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wzzy { get { return new dvec4(v[3], v[2], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wzzz { get { return new dvec4(v[3], v[2], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wzzw { get { return new dvec4(v[3], v[2], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wzwx { get { return new dvec4(v[3], v[2], v[3], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wzwy { get { return new dvec4(v[3], v[2], v[3], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wzwz { get { return new dvec4(v[3], v[2], v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wzww { get { return new dvec4(v[3], v[2], v[3], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wwxx { get { return new dvec4(v[3], v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wwxy { get { return new dvec4(v[3], v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wwxz { get { return new dvec4(v[3], v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wwxw { get { return new dvec4(v[3], v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wwyx { get { return new dvec4(v[3], v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wwyy { get { return new dvec4(v[3], v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wwyz { get { return new dvec4(v[3], v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wwyw { get { return new dvec4(v[3], v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wwzx { get { return new dvec4(v[3], v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wwzy { get { return new dvec4(v[3], v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wwzz { get { return new dvec4(v[3], v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wwzw { get { return new dvec4(v[3], v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public dvec4 wwwx { get { return new dvec4(v[3], v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public dvec4 wwwy { get { return new dvec4(v[3], v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public dvec4 wwwz { get { return new dvec4(v[3], v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public dvec4 wwww { get { return new dvec4(v[3], v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }

        #endregion
    }

    class bvec4 : tvec4<bool>
    {
        #region bvec4

        public bvec4() { }
        public bvec4(bool a) : base(a, a, a, a) { }
        public bvec4(bool X, bool Y, bool Z, bool W) : base(X, Y, Z, W) { }

        #endregion

        #region Generated

        public bool x { get { return v[0]; } set { v[0] = value; } }
        public bool y { get { return v[1]; } set { v[1] = value; } }
        public bool z { get { return v[2]; } set { v[2] = value; } }
        public bool w { get { return v[3]; } set { v[3] = value; } }
        public bvec2 xx { get { return new bvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 xy { get { return new bvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public bvec2 xz { get { return new bvec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public bvec2 xw { get { return new bvec2(v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; } }
        public bvec2 yx { get { return new bvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 yy { get { return new bvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public bvec2 yz { get { return new bvec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public bvec2 yw { get { return new bvec2(v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; } }
        public bvec2 zx { get { return new bvec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 zy { get { return new bvec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public bvec2 zz { get { return new bvec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public bvec2 zw { get { return new bvec2(v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; } }
        public bvec2 wx { get { return new bvec2(v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; } }
        public bvec2 wy { get { return new bvec2(v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; } }
        public bvec2 wz { get { return new bvec2(v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; } }
        public bvec2 ww { get { return new bvec2(v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; } }
        public bvec3 xxx { get { return new bvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xxy { get { return new bvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 xxz { get { return new bvec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 xxw { get { return new bvec3(v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 xyx { get { return new bvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xyy { get { return new bvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 xyz { get { return new bvec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 xyw { get { return new bvec3(v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 xzx { get { return new bvec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xzy { get { return new bvec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 xzz { get { return new bvec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 xzw { get { return new bvec3(v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 xwx { get { return new bvec3(v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 xwy { get { return new bvec3(v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 xwz { get { return new bvec3(v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 xww { get { return new bvec3(v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 yxx { get { return new bvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 yxy { get { return new bvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 yxz { get { return new bvec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 yxw { get { return new bvec3(v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 yyx { get { return new bvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 yyy { get { return new bvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 yyz { get { return new bvec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 yyw { get { return new bvec3(v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 yzx { get { return new bvec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 yzy { get { return new bvec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 yzz { get { return new bvec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 yzw { get { return new bvec3(v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 ywx { get { return new bvec3(v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 ywy { get { return new bvec3(v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 ywz { get { return new bvec3(v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 yww { get { return new bvec3(v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 zxx { get { return new bvec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 zxy { get { return new bvec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 zxz { get { return new bvec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 zxw { get { return new bvec3(v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 zyx { get { return new bvec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 zyy { get { return new bvec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 zyz { get { return new bvec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 zyw { get { return new bvec3(v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 zzx { get { return new bvec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 zzy { get { return new bvec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 zzz { get { return new bvec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 zzw { get { return new bvec3(v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 zwx { get { return new bvec3(v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 zwy { get { return new bvec3(v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 zwz { get { return new bvec3(v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 zww { get { return new bvec3(v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 wxx { get { return new bvec3(v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 wxy { get { return new bvec3(v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 wxz { get { return new bvec3(v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 wxw { get { return new bvec3(v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 wyx { get { return new bvec3(v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 wyy { get { return new bvec3(v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 wyz { get { return new bvec3(v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 wyw { get { return new bvec3(v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 wzx { get { return new bvec3(v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 wzy { get { return new bvec3(v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 wzz { get { return new bvec3(v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 wzw { get { return new bvec3(v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public bvec3 wwx { get { return new bvec3(v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public bvec3 wwy { get { return new bvec3(v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public bvec3 wwz { get { return new bvec3(v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public bvec3 www { get { return new bvec3(v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public bvec4 xxxx { get { return new bvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxxy { get { return new bvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xxxz { get { return new bvec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xxxw { get { return new bvec4(v[0], v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xxyx { get { return new bvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxyy { get { return new bvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xxyz { get { return new bvec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xxyw { get { return new bvec4(v[0], v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xxzx { get { return new bvec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxzy { get { return new bvec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xxzz { get { return new bvec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xxzw { get { return new bvec4(v[0], v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xxwx { get { return new bvec4(v[0], v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xxwy { get { return new bvec4(v[0], v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xxwz { get { return new bvec4(v[0], v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xxww { get { return new bvec4(v[0], v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xyxx { get { return new bvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xyxy { get { return new bvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xyxz { get { return new bvec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xyxw { get { return new bvec4(v[0], v[1], v[0], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xyyx { get { return new bvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xyyy { get { return new bvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xyyz { get { return new bvec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xyyw { get { return new bvec4(v[0], v[1], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xyzx { get { return new bvec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xyzy { get { return new bvec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xyzz { get { return new bvec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xyzw { get { return new bvec4(v[0], v[1], v[2], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xywx { get { return new bvec4(v[0], v[1], v[3], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xywy { get { return new bvec4(v[0], v[1], v[3], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xywz { get { return new bvec4(v[0], v[1], v[3], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xyww { get { return new bvec4(v[0], v[1], v[3], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xzxx { get { return new bvec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xzxy { get { return new bvec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xzxz { get { return new bvec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xzxw { get { return new bvec4(v[0], v[2], v[0], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xzyx { get { return new bvec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xzyy { get { return new bvec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xzyz { get { return new bvec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xzyw { get { return new bvec4(v[0], v[2], v[1], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xzzx { get { return new bvec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xzzy { get { return new bvec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xzzz { get { return new bvec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xzzw { get { return new bvec4(v[0], v[2], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xzwx { get { return new bvec4(v[0], v[2], v[3], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xzwy { get { return new bvec4(v[0], v[2], v[3], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xzwz { get { return new bvec4(v[0], v[2], v[3], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xzww { get { return new bvec4(v[0], v[2], v[3], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xwxx { get { return new bvec4(v[0], v[3], v[0], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xwxy { get { return new bvec4(v[0], v[3], v[0], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xwxz { get { return new bvec4(v[0], v[3], v[0], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xwxw { get { return new bvec4(v[0], v[3], v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xwyx { get { return new bvec4(v[0], v[3], v[1], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xwyy { get { return new bvec4(v[0], v[3], v[1], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xwyz { get { return new bvec4(v[0], v[3], v[1], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xwyw { get { return new bvec4(v[0], v[3], v[1], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xwzx { get { return new bvec4(v[0], v[3], v[2], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xwzy { get { return new bvec4(v[0], v[3], v[2], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xwzz { get { return new bvec4(v[0], v[3], v[2], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xwzw { get { return new bvec4(v[0], v[3], v[2], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 xwwx { get { return new bvec4(v[0], v[3], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 xwwy { get { return new bvec4(v[0], v[3], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 xwwz { get { return new bvec4(v[0], v[3], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 xwww { get { return new bvec4(v[0], v[3], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yxxx { get { return new bvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxxy { get { return new bvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxxz { get { return new bvec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yxxw { get { return new bvec4(v[1], v[0], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yxyx { get { return new bvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxyy { get { return new bvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxyz { get { return new bvec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yxyw { get { return new bvec4(v[1], v[0], v[1], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yxzx { get { return new bvec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxzy { get { return new bvec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxzz { get { return new bvec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yxzw { get { return new bvec4(v[1], v[0], v[2], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yxwx { get { return new bvec4(v[1], v[0], v[3], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yxwy { get { return new bvec4(v[1], v[0], v[3], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yxwz { get { return new bvec4(v[1], v[0], v[3], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yxww { get { return new bvec4(v[1], v[0], v[3], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yyxx { get { return new bvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yyxy { get { return new bvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yyxz { get { return new bvec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yyxw { get { return new bvec4(v[1], v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yyyx { get { return new bvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yyyy { get { return new bvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yyyz { get { return new bvec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yyyw { get { return new bvec4(v[1], v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yyzx { get { return new bvec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yyzy { get { return new bvec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yyzz { get { return new bvec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yyzw { get { return new bvec4(v[1], v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yywx { get { return new bvec4(v[1], v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yywy { get { return new bvec4(v[1], v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yywz { get { return new bvec4(v[1], v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yyww { get { return new bvec4(v[1], v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yzxx { get { return new bvec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yzxy { get { return new bvec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yzxz { get { return new bvec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yzxw { get { return new bvec4(v[1], v[2], v[0], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yzyx { get { return new bvec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yzyy { get { return new bvec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yzyz { get { return new bvec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yzyw { get { return new bvec4(v[1], v[2], v[1], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yzzx { get { return new bvec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yzzy { get { return new bvec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yzzz { get { return new bvec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yzzw { get { return new bvec4(v[1], v[2], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 yzwx { get { return new bvec4(v[1], v[2], v[3], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 yzwy { get { return new bvec4(v[1], v[2], v[3], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 yzwz { get { return new bvec4(v[1], v[2], v[3], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 yzww { get { return new bvec4(v[1], v[2], v[3], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 ywxx { get { return new bvec4(v[1], v[3], v[0], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 ywxy { get { return new bvec4(v[1], v[3], v[0], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 ywxz { get { return new bvec4(v[1], v[3], v[0], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 ywxw { get { return new bvec4(v[1], v[3], v[0], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 ywyx { get { return new bvec4(v[1], v[3], v[1], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 ywyy { get { return new bvec4(v[1], v[3], v[1], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 ywyz { get { return new bvec4(v[1], v[3], v[1], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 ywyw { get { return new bvec4(v[1], v[3], v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 ywzx { get { return new bvec4(v[1], v[3], v[2], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 ywzy { get { return new bvec4(v[1], v[3], v[2], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 ywzz { get { return new bvec4(v[1], v[3], v[2], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 ywzw { get { return new bvec4(v[1], v[3], v[2], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 ywwx { get { return new bvec4(v[1], v[3], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 ywwy { get { return new bvec4(v[1], v[3], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 ywwz { get { return new bvec4(v[1], v[3], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 ywww { get { return new bvec4(v[1], v[3], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zxxx { get { return new bvec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zxxy { get { return new bvec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zxxz { get { return new bvec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zxxw { get { return new bvec4(v[2], v[0], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zxyx { get { return new bvec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zxyy { get { return new bvec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zxyz { get { return new bvec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zxyw { get { return new bvec4(v[2], v[0], v[1], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zxzx { get { return new bvec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zxzy { get { return new bvec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zxzz { get { return new bvec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zxzw { get { return new bvec4(v[2], v[0], v[2], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zxwx { get { return new bvec4(v[2], v[0], v[3], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zxwy { get { return new bvec4(v[2], v[0], v[3], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zxwz { get { return new bvec4(v[2], v[0], v[3], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zxww { get { return new bvec4(v[2], v[0], v[3], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zyxx { get { return new bvec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zyxy { get { return new bvec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zyxz { get { return new bvec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zyxw { get { return new bvec4(v[2], v[1], v[0], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zyyx { get { return new bvec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zyyy { get { return new bvec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zyyz { get { return new bvec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zyyw { get { return new bvec4(v[2], v[1], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zyzx { get { return new bvec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zyzy { get { return new bvec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zyzz { get { return new bvec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zyzw { get { return new bvec4(v[2], v[1], v[2], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zywx { get { return new bvec4(v[2], v[1], v[3], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zywy { get { return new bvec4(v[2], v[1], v[3], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zywz { get { return new bvec4(v[2], v[1], v[3], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zyww { get { return new bvec4(v[2], v[1], v[3], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zzxx { get { return new bvec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zzxy { get { return new bvec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zzxz { get { return new bvec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zzxw { get { return new bvec4(v[2], v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zzyx { get { return new bvec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zzyy { get { return new bvec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zzyz { get { return new bvec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zzyw { get { return new bvec4(v[2], v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zzzx { get { return new bvec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zzzy { get { return new bvec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zzzz { get { return new bvec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zzzw { get { return new bvec4(v[2], v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zzwx { get { return new bvec4(v[2], v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zzwy { get { return new bvec4(v[2], v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zzwz { get { return new bvec4(v[2], v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zzww { get { return new bvec4(v[2], v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zwxx { get { return new bvec4(v[2], v[3], v[0], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zwxy { get { return new bvec4(v[2], v[3], v[0], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zwxz { get { return new bvec4(v[2], v[3], v[0], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zwxw { get { return new bvec4(v[2], v[3], v[0], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zwyx { get { return new bvec4(v[2], v[3], v[1], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zwyy { get { return new bvec4(v[2], v[3], v[1], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zwyz { get { return new bvec4(v[2], v[3], v[1], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zwyw { get { return new bvec4(v[2], v[3], v[1], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zwzx { get { return new bvec4(v[2], v[3], v[2], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zwzy { get { return new bvec4(v[2], v[3], v[2], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zwzz { get { return new bvec4(v[2], v[3], v[2], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zwzw { get { return new bvec4(v[2], v[3], v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 zwwx { get { return new bvec4(v[2], v[3], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 zwwy { get { return new bvec4(v[2], v[3], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 zwwz { get { return new bvec4(v[2], v[3], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 zwww { get { return new bvec4(v[2], v[3], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wxxx { get { return new bvec4(v[3], v[0], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wxxy { get { return new bvec4(v[3], v[0], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wxxz { get { return new bvec4(v[3], v[0], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wxxw { get { return new bvec4(v[3], v[0], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wxyx { get { return new bvec4(v[3], v[0], v[1], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wxyy { get { return new bvec4(v[3], v[0], v[1], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wxyz { get { return new bvec4(v[3], v[0], v[1], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wxyw { get { return new bvec4(v[3], v[0], v[1], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wxzx { get { return new bvec4(v[3], v[0], v[2], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wxzy { get { return new bvec4(v[3], v[0], v[2], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wxzz { get { return new bvec4(v[3], v[0], v[2], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wxzw { get { return new bvec4(v[3], v[0], v[2], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wxwx { get { return new bvec4(v[3], v[0], v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wxwy { get { return new bvec4(v[3], v[0], v[3], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wxwz { get { return new bvec4(v[3], v[0], v[3], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wxww { get { return new bvec4(v[3], v[0], v[3], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wyxx { get { return new bvec4(v[3], v[1], v[0], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wyxy { get { return new bvec4(v[3], v[1], v[0], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wyxz { get { return new bvec4(v[3], v[1], v[0], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wyxw { get { return new bvec4(v[3], v[1], v[0], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wyyx { get { return new bvec4(v[3], v[1], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wyyy { get { return new bvec4(v[3], v[1], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wyyz { get { return new bvec4(v[3], v[1], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wyyw { get { return new bvec4(v[3], v[1], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wyzx { get { return new bvec4(v[3], v[1], v[2], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wyzy { get { return new bvec4(v[3], v[1], v[2], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wyzz { get { return new bvec4(v[3], v[1], v[2], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wyzw { get { return new bvec4(v[3], v[1], v[2], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wywx { get { return new bvec4(v[3], v[1], v[3], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wywy { get { return new bvec4(v[3], v[1], v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wywz { get { return new bvec4(v[3], v[1], v[3], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wyww { get { return new bvec4(v[3], v[1], v[3], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wzxx { get { return new bvec4(v[3], v[2], v[0], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wzxy { get { return new bvec4(v[3], v[2], v[0], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wzxz { get { return new bvec4(v[3], v[2], v[0], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wzxw { get { return new bvec4(v[3], v[2], v[0], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wzyx { get { return new bvec4(v[3], v[2], v[1], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wzyy { get { return new bvec4(v[3], v[2], v[1], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wzyz { get { return new bvec4(v[3], v[2], v[1], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wzyw { get { return new bvec4(v[3], v[2], v[1], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wzzx { get { return new bvec4(v[3], v[2], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wzzy { get { return new bvec4(v[3], v[2], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wzzz { get { return new bvec4(v[3], v[2], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wzzw { get { return new bvec4(v[3], v[2], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wzwx { get { return new bvec4(v[3], v[2], v[3], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wzwy { get { return new bvec4(v[3], v[2], v[3], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wzwz { get { return new bvec4(v[3], v[2], v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wzww { get { return new bvec4(v[3], v[2], v[3], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wwxx { get { return new bvec4(v[3], v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wwxy { get { return new bvec4(v[3], v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wwxz { get { return new bvec4(v[3], v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wwxw { get { return new bvec4(v[3], v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wwyx { get { return new bvec4(v[3], v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wwyy { get { return new bvec4(v[3], v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wwyz { get { return new bvec4(v[3], v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wwyw { get { return new bvec4(v[3], v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wwzx { get { return new bvec4(v[3], v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wwzy { get { return new bvec4(v[3], v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wwzz { get { return new bvec4(v[3], v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wwzw { get { return new bvec4(v[3], v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public bvec4 wwwx { get { return new bvec4(v[3], v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public bvec4 wwwy { get { return new bvec4(v[3], v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public bvec4 wwwz { get { return new bvec4(v[3], v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public bvec4 wwww { get { return new bvec4(v[3], v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }

        #endregion
    }

    class ivec4 : tvec4<int>
    {
        #region ivec4

        public ivec4() { }
        public ivec4(int a) : base(a, a, a, a) { }
        public ivec4(int X, int Y, int Z, int W) : base(X, Y, Z, W) { }

        #endregion

        #region Generated

        public int x { get { return v[0]; } set { v[0] = value; } }
        public int y { get { return v[1]; } set { v[1] = value; } }
        public int z { get { return v[2]; } set { v[2] = value; } }
        public int w { get { return v[3]; } set { v[3] = value; } }
        public ivec2 xx { get { return new ivec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 xy { get { return new ivec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public ivec2 xz { get { return new ivec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public ivec2 xw { get { return new ivec2(v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; } }
        public ivec2 yx { get { return new ivec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 yy { get { return new ivec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public ivec2 yz { get { return new ivec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public ivec2 yw { get { return new ivec2(v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; } }
        public ivec2 zx { get { return new ivec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 zy { get { return new ivec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public ivec2 zz { get { return new ivec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public ivec2 zw { get { return new ivec2(v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; } }
        public ivec2 wx { get { return new ivec2(v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; } }
        public ivec2 wy { get { return new ivec2(v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; } }
        public ivec2 wz { get { return new ivec2(v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; } }
        public ivec2 ww { get { return new ivec2(v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; } }
        public ivec3 xxx { get { return new ivec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xxy { get { return new ivec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 xxz { get { return new ivec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 xxw { get { return new ivec3(v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 xyx { get { return new ivec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xyy { get { return new ivec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 xyz { get { return new ivec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 xyw { get { return new ivec3(v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 xzx { get { return new ivec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xzy { get { return new ivec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 xzz { get { return new ivec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 xzw { get { return new ivec3(v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 xwx { get { return new ivec3(v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 xwy { get { return new ivec3(v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 xwz { get { return new ivec3(v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 xww { get { return new ivec3(v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 yxx { get { return new ivec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 yxy { get { return new ivec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 yxz { get { return new ivec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 yxw { get { return new ivec3(v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 yyx { get { return new ivec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 yyy { get { return new ivec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 yyz { get { return new ivec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 yyw { get { return new ivec3(v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 yzx { get { return new ivec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 yzy { get { return new ivec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 yzz { get { return new ivec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 yzw { get { return new ivec3(v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 ywx { get { return new ivec3(v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 ywy { get { return new ivec3(v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 ywz { get { return new ivec3(v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 yww { get { return new ivec3(v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 zxx { get { return new ivec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 zxy { get { return new ivec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 zxz { get { return new ivec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 zxw { get { return new ivec3(v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 zyx { get { return new ivec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 zyy { get { return new ivec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 zyz { get { return new ivec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 zyw { get { return new ivec3(v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 zzx { get { return new ivec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 zzy { get { return new ivec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 zzz { get { return new ivec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 zzw { get { return new ivec3(v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 zwx { get { return new ivec3(v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 zwy { get { return new ivec3(v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 zwz { get { return new ivec3(v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 zww { get { return new ivec3(v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 wxx { get { return new ivec3(v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 wxy { get { return new ivec3(v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 wxz { get { return new ivec3(v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 wxw { get { return new ivec3(v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 wyx { get { return new ivec3(v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 wyy { get { return new ivec3(v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 wyz { get { return new ivec3(v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 wyw { get { return new ivec3(v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 wzx { get { return new ivec3(v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 wzy { get { return new ivec3(v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 wzz { get { return new ivec3(v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 wzw { get { return new ivec3(v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public ivec3 wwx { get { return new ivec3(v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public ivec3 wwy { get { return new ivec3(v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public ivec3 wwz { get { return new ivec3(v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public ivec3 www { get { return new ivec3(v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public ivec4 xxxx { get { return new ivec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxxy { get { return new ivec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xxxz { get { return new ivec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xxxw { get { return new ivec4(v[0], v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xxyx { get { return new ivec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxyy { get { return new ivec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xxyz { get { return new ivec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xxyw { get { return new ivec4(v[0], v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xxzx { get { return new ivec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxzy { get { return new ivec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xxzz { get { return new ivec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xxzw { get { return new ivec4(v[0], v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xxwx { get { return new ivec4(v[0], v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xxwy { get { return new ivec4(v[0], v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xxwz { get { return new ivec4(v[0], v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xxww { get { return new ivec4(v[0], v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xyxx { get { return new ivec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xyxy { get { return new ivec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xyxz { get { return new ivec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xyxw { get { return new ivec4(v[0], v[1], v[0], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xyyx { get { return new ivec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xyyy { get { return new ivec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xyyz { get { return new ivec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xyyw { get { return new ivec4(v[0], v[1], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xyzx { get { return new ivec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xyzy { get { return new ivec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xyzz { get { return new ivec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xyzw { get { return new ivec4(v[0], v[1], v[2], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xywx { get { return new ivec4(v[0], v[1], v[3], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xywy { get { return new ivec4(v[0], v[1], v[3], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xywz { get { return new ivec4(v[0], v[1], v[3], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xyww { get { return new ivec4(v[0], v[1], v[3], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xzxx { get { return new ivec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xzxy { get { return new ivec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xzxz { get { return new ivec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xzxw { get { return new ivec4(v[0], v[2], v[0], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xzyx { get { return new ivec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xzyy { get { return new ivec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xzyz { get { return new ivec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xzyw { get { return new ivec4(v[0], v[2], v[1], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xzzx { get { return new ivec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xzzy { get { return new ivec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xzzz { get { return new ivec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xzzw { get { return new ivec4(v[0], v[2], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xzwx { get { return new ivec4(v[0], v[2], v[3], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xzwy { get { return new ivec4(v[0], v[2], v[3], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xzwz { get { return new ivec4(v[0], v[2], v[3], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xzww { get { return new ivec4(v[0], v[2], v[3], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xwxx { get { return new ivec4(v[0], v[3], v[0], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xwxy { get { return new ivec4(v[0], v[3], v[0], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xwxz { get { return new ivec4(v[0], v[3], v[0], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xwxw { get { return new ivec4(v[0], v[3], v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xwyx { get { return new ivec4(v[0], v[3], v[1], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xwyy { get { return new ivec4(v[0], v[3], v[1], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xwyz { get { return new ivec4(v[0], v[3], v[1], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xwyw { get { return new ivec4(v[0], v[3], v[1], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xwzx { get { return new ivec4(v[0], v[3], v[2], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xwzy { get { return new ivec4(v[0], v[3], v[2], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xwzz { get { return new ivec4(v[0], v[3], v[2], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xwzw { get { return new ivec4(v[0], v[3], v[2], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 xwwx { get { return new ivec4(v[0], v[3], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 xwwy { get { return new ivec4(v[0], v[3], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 xwwz { get { return new ivec4(v[0], v[3], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 xwww { get { return new ivec4(v[0], v[3], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yxxx { get { return new ivec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxxy { get { return new ivec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxxz { get { return new ivec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yxxw { get { return new ivec4(v[1], v[0], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yxyx { get { return new ivec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxyy { get { return new ivec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxyz { get { return new ivec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yxyw { get { return new ivec4(v[1], v[0], v[1], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yxzx { get { return new ivec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxzy { get { return new ivec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxzz { get { return new ivec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yxzw { get { return new ivec4(v[1], v[0], v[2], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yxwx { get { return new ivec4(v[1], v[0], v[3], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yxwy { get { return new ivec4(v[1], v[0], v[3], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yxwz { get { return new ivec4(v[1], v[0], v[3], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yxww { get { return new ivec4(v[1], v[0], v[3], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yyxx { get { return new ivec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yyxy { get { return new ivec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yyxz { get { return new ivec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yyxw { get { return new ivec4(v[1], v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yyyx { get { return new ivec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yyyy { get { return new ivec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yyyz { get { return new ivec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yyyw { get { return new ivec4(v[1], v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yyzx { get { return new ivec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yyzy { get { return new ivec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yyzz { get { return new ivec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yyzw { get { return new ivec4(v[1], v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yywx { get { return new ivec4(v[1], v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yywy { get { return new ivec4(v[1], v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yywz { get { return new ivec4(v[1], v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yyww { get { return new ivec4(v[1], v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yzxx { get { return new ivec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yzxy { get { return new ivec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yzxz { get { return new ivec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yzxw { get { return new ivec4(v[1], v[2], v[0], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yzyx { get { return new ivec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yzyy { get { return new ivec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yzyz { get { return new ivec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yzyw { get { return new ivec4(v[1], v[2], v[1], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yzzx { get { return new ivec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yzzy { get { return new ivec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yzzz { get { return new ivec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yzzw { get { return new ivec4(v[1], v[2], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 yzwx { get { return new ivec4(v[1], v[2], v[3], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 yzwy { get { return new ivec4(v[1], v[2], v[3], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 yzwz { get { return new ivec4(v[1], v[2], v[3], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 yzww { get { return new ivec4(v[1], v[2], v[3], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 ywxx { get { return new ivec4(v[1], v[3], v[0], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 ywxy { get { return new ivec4(v[1], v[3], v[0], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 ywxz { get { return new ivec4(v[1], v[3], v[0], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 ywxw { get { return new ivec4(v[1], v[3], v[0], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 ywyx { get { return new ivec4(v[1], v[3], v[1], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 ywyy { get { return new ivec4(v[1], v[3], v[1], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 ywyz { get { return new ivec4(v[1], v[3], v[1], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 ywyw { get { return new ivec4(v[1], v[3], v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 ywzx { get { return new ivec4(v[1], v[3], v[2], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 ywzy { get { return new ivec4(v[1], v[3], v[2], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 ywzz { get { return new ivec4(v[1], v[3], v[2], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 ywzw { get { return new ivec4(v[1], v[3], v[2], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 ywwx { get { return new ivec4(v[1], v[3], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 ywwy { get { return new ivec4(v[1], v[3], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 ywwz { get { return new ivec4(v[1], v[3], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 ywww { get { return new ivec4(v[1], v[3], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zxxx { get { return new ivec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zxxy { get { return new ivec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zxxz { get { return new ivec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zxxw { get { return new ivec4(v[2], v[0], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zxyx { get { return new ivec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zxyy { get { return new ivec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zxyz { get { return new ivec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zxyw { get { return new ivec4(v[2], v[0], v[1], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zxzx { get { return new ivec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zxzy { get { return new ivec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zxzz { get { return new ivec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zxzw { get { return new ivec4(v[2], v[0], v[2], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zxwx { get { return new ivec4(v[2], v[0], v[3], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zxwy { get { return new ivec4(v[2], v[0], v[3], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zxwz { get { return new ivec4(v[2], v[0], v[3], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zxww { get { return new ivec4(v[2], v[0], v[3], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zyxx { get { return new ivec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zyxy { get { return new ivec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zyxz { get { return new ivec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zyxw { get { return new ivec4(v[2], v[1], v[0], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zyyx { get { return new ivec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zyyy { get { return new ivec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zyyz { get { return new ivec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zyyw { get { return new ivec4(v[2], v[1], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zyzx { get { return new ivec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zyzy { get { return new ivec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zyzz { get { return new ivec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zyzw { get { return new ivec4(v[2], v[1], v[2], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zywx { get { return new ivec4(v[2], v[1], v[3], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zywy { get { return new ivec4(v[2], v[1], v[3], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zywz { get { return new ivec4(v[2], v[1], v[3], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zyww { get { return new ivec4(v[2], v[1], v[3], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zzxx { get { return new ivec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zzxy { get { return new ivec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zzxz { get { return new ivec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zzxw { get { return new ivec4(v[2], v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zzyx { get { return new ivec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zzyy { get { return new ivec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zzyz { get { return new ivec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zzyw { get { return new ivec4(v[2], v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zzzx { get { return new ivec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zzzy { get { return new ivec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zzzz { get { return new ivec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zzzw { get { return new ivec4(v[2], v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zzwx { get { return new ivec4(v[2], v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zzwy { get { return new ivec4(v[2], v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zzwz { get { return new ivec4(v[2], v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zzww { get { return new ivec4(v[2], v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zwxx { get { return new ivec4(v[2], v[3], v[0], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zwxy { get { return new ivec4(v[2], v[3], v[0], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zwxz { get { return new ivec4(v[2], v[3], v[0], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zwxw { get { return new ivec4(v[2], v[3], v[0], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zwyx { get { return new ivec4(v[2], v[3], v[1], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zwyy { get { return new ivec4(v[2], v[3], v[1], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zwyz { get { return new ivec4(v[2], v[3], v[1], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zwyw { get { return new ivec4(v[2], v[3], v[1], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zwzx { get { return new ivec4(v[2], v[3], v[2], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zwzy { get { return new ivec4(v[2], v[3], v[2], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zwzz { get { return new ivec4(v[2], v[3], v[2], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zwzw { get { return new ivec4(v[2], v[3], v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 zwwx { get { return new ivec4(v[2], v[3], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 zwwy { get { return new ivec4(v[2], v[3], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 zwwz { get { return new ivec4(v[2], v[3], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 zwww { get { return new ivec4(v[2], v[3], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wxxx { get { return new ivec4(v[3], v[0], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wxxy { get { return new ivec4(v[3], v[0], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wxxz { get { return new ivec4(v[3], v[0], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wxxw { get { return new ivec4(v[3], v[0], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wxyx { get { return new ivec4(v[3], v[0], v[1], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wxyy { get { return new ivec4(v[3], v[0], v[1], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wxyz { get { return new ivec4(v[3], v[0], v[1], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wxyw { get { return new ivec4(v[3], v[0], v[1], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wxzx { get { return new ivec4(v[3], v[0], v[2], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wxzy { get { return new ivec4(v[3], v[0], v[2], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wxzz { get { return new ivec4(v[3], v[0], v[2], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wxzw { get { return new ivec4(v[3], v[0], v[2], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wxwx { get { return new ivec4(v[3], v[0], v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wxwy { get { return new ivec4(v[3], v[0], v[3], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wxwz { get { return new ivec4(v[3], v[0], v[3], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wxww { get { return new ivec4(v[3], v[0], v[3], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wyxx { get { return new ivec4(v[3], v[1], v[0], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wyxy { get { return new ivec4(v[3], v[1], v[0], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wyxz { get { return new ivec4(v[3], v[1], v[0], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wyxw { get { return new ivec4(v[3], v[1], v[0], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wyyx { get { return new ivec4(v[3], v[1], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wyyy { get { return new ivec4(v[3], v[1], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wyyz { get { return new ivec4(v[3], v[1], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wyyw { get { return new ivec4(v[3], v[1], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wyzx { get { return new ivec4(v[3], v[1], v[2], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wyzy { get { return new ivec4(v[3], v[1], v[2], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wyzz { get { return new ivec4(v[3], v[1], v[2], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wyzw { get { return new ivec4(v[3], v[1], v[2], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wywx { get { return new ivec4(v[3], v[1], v[3], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wywy { get { return new ivec4(v[3], v[1], v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wywz { get { return new ivec4(v[3], v[1], v[3], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wyww { get { return new ivec4(v[3], v[1], v[3], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wzxx { get { return new ivec4(v[3], v[2], v[0], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wzxy { get { return new ivec4(v[3], v[2], v[0], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wzxz { get { return new ivec4(v[3], v[2], v[0], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wzxw { get { return new ivec4(v[3], v[2], v[0], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wzyx { get { return new ivec4(v[3], v[2], v[1], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wzyy { get { return new ivec4(v[3], v[2], v[1], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wzyz { get { return new ivec4(v[3], v[2], v[1], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wzyw { get { return new ivec4(v[3], v[2], v[1], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wzzx { get { return new ivec4(v[3], v[2], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wzzy { get { return new ivec4(v[3], v[2], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wzzz { get { return new ivec4(v[3], v[2], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wzzw { get { return new ivec4(v[3], v[2], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wzwx { get { return new ivec4(v[3], v[2], v[3], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wzwy { get { return new ivec4(v[3], v[2], v[3], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wzwz { get { return new ivec4(v[3], v[2], v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wzww { get { return new ivec4(v[3], v[2], v[3], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wwxx { get { return new ivec4(v[3], v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wwxy { get { return new ivec4(v[3], v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wwxz { get { return new ivec4(v[3], v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wwxw { get { return new ivec4(v[3], v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wwyx { get { return new ivec4(v[3], v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wwyy { get { return new ivec4(v[3], v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wwyz { get { return new ivec4(v[3], v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wwyw { get { return new ivec4(v[3], v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wwzx { get { return new ivec4(v[3], v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wwzy { get { return new ivec4(v[3], v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wwzz { get { return new ivec4(v[3], v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wwzw { get { return new ivec4(v[3], v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public ivec4 wwwx { get { return new ivec4(v[3], v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public ivec4 wwwy { get { return new ivec4(v[3], v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public ivec4 wwwz { get { return new ivec4(v[3], v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public ivec4 wwww { get { return new ivec4(v[3], v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }

        #endregion
    }

    class uvec4 : tvec4<uint>
    {
        #region uvec4

        public uvec4() { }
        public uvec4(uint a) : base(a, a, a, a) { }
        public uvec4(uint X, uint Y, uint Z, uint W) : base(X, Y, Z, W) { }

        #endregion

        #region Generated

        public uint x { get { return v[0]; } set { v[0] = value; } }
        public uint y { get { return v[1]; } set { v[1] = value; } }
        public uint z { get { return v[2]; } set { v[2] = value; } }
        public uint w { get { return v[3]; } set { v[3] = value; } }
        public uvec2 xx { get { return new uvec2(v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 xy { get { return new uvec2(v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; } }
        public uvec2 xz { get { return new uvec2(v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; } }
        public uvec2 xw { get { return new uvec2(v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; } }
        public uvec2 yx { get { return new uvec2(v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 yy { get { return new uvec2(v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; } }
        public uvec2 yz { get { return new uvec2(v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; } }
        public uvec2 yw { get { return new uvec2(v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; } }
        public uvec2 zx { get { return new uvec2(v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 zy { get { return new uvec2(v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; } }
        public uvec2 zz { get { return new uvec2(v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; } }
        public uvec2 zw { get { return new uvec2(v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; } }
        public uvec2 wx { get { return new uvec2(v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; } }
        public uvec2 wy { get { return new uvec2(v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; } }
        public uvec2 wz { get { return new uvec2(v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; } }
        public uvec2 ww { get { return new uvec2(v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; } }
        public uvec3 xxx { get { return new uvec3(v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xxy { get { return new uvec3(v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 xxz { get { return new uvec3(v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 xxw { get { return new uvec3(v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 xyx { get { return new uvec3(v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xyy { get { return new uvec3(v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 xyz { get { return new uvec3(v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 xyw { get { return new uvec3(v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 xzx { get { return new uvec3(v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xzy { get { return new uvec3(v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 xzz { get { return new uvec3(v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 xzw { get { return new uvec3(v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 xwx { get { return new uvec3(v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 xwy { get { return new uvec3(v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 xwz { get { return new uvec3(v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 xww { get { return new uvec3(v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 yxx { get { return new uvec3(v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 yxy { get { return new uvec3(v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 yxz { get { return new uvec3(v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 yxw { get { return new uvec3(v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 yyx { get { return new uvec3(v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 yyy { get { return new uvec3(v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 yyz { get { return new uvec3(v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 yyw { get { return new uvec3(v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 yzx { get { return new uvec3(v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 yzy { get { return new uvec3(v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 yzz { get { return new uvec3(v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 yzw { get { return new uvec3(v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 ywx { get { return new uvec3(v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 ywy { get { return new uvec3(v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 ywz { get { return new uvec3(v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 yww { get { return new uvec3(v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 zxx { get { return new uvec3(v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 zxy { get { return new uvec3(v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 zxz { get { return new uvec3(v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 zxw { get { return new uvec3(v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 zyx { get { return new uvec3(v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 zyy { get { return new uvec3(v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 zyz { get { return new uvec3(v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 zyw { get { return new uvec3(v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 zzx { get { return new uvec3(v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 zzy { get { return new uvec3(v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 zzz { get { return new uvec3(v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 zzw { get { return new uvec3(v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 zwx { get { return new uvec3(v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 zwy { get { return new uvec3(v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 zwz { get { return new uvec3(v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 zww { get { return new uvec3(v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 wxx { get { return new uvec3(v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 wxy { get { return new uvec3(v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 wxz { get { return new uvec3(v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 wxw { get { return new uvec3(v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 wyx { get { return new uvec3(v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 wyy { get { return new uvec3(v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 wyz { get { return new uvec3(v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 wyw { get { return new uvec3(v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 wzx { get { return new uvec3(v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 wzy { get { return new uvec3(v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 wzz { get { return new uvec3(v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 wzw { get { return new uvec3(v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; } }
        public uvec3 wwx { get { return new uvec3(v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; } }
        public uvec3 wwy { get { return new uvec3(v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; } }
        public uvec3 wwz { get { return new uvec3(v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; } }
        public uvec3 www { get { return new uvec3(v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; } }
        public uvec4 xxxx { get { return new uvec4(v[0], v[0], v[0], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxxy { get { return new uvec4(v[0], v[0], v[0], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xxxz { get { return new uvec4(v[0], v[0], v[0], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xxxw { get { return new uvec4(v[0], v[0], v[0], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xxyx { get { return new uvec4(v[0], v[0], v[1], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxyy { get { return new uvec4(v[0], v[0], v[1], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xxyz { get { return new uvec4(v[0], v[0], v[1], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xxyw { get { return new uvec4(v[0], v[0], v[1], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xxzx { get { return new uvec4(v[0], v[0], v[2], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxzy { get { return new uvec4(v[0], v[0], v[2], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xxzz { get { return new uvec4(v[0], v[0], v[2], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xxzw { get { return new uvec4(v[0], v[0], v[2], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xxwx { get { return new uvec4(v[0], v[0], v[3], v[0]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xxwy { get { return new uvec4(v[0], v[0], v[3], v[1]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xxwz { get { return new uvec4(v[0], v[0], v[3], v[2]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xxww { get { return new uvec4(v[0], v[0], v[3], v[3]); } set { v[0] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xyxx { get { return new uvec4(v[0], v[1], v[0], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xyxy { get { return new uvec4(v[0], v[1], v[0], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xyxz { get { return new uvec4(v[0], v[1], v[0], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xyxw { get { return new uvec4(v[0], v[1], v[0], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xyyx { get { return new uvec4(v[0], v[1], v[1], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xyyy { get { return new uvec4(v[0], v[1], v[1], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xyyz { get { return new uvec4(v[0], v[1], v[1], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xyyw { get { return new uvec4(v[0], v[1], v[1], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xyzx { get { return new uvec4(v[0], v[1], v[2], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xyzy { get { return new uvec4(v[0], v[1], v[2], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xyzz { get { return new uvec4(v[0], v[1], v[2], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xyzw { get { return new uvec4(v[0], v[1], v[2], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xywx { get { return new uvec4(v[0], v[1], v[3], v[0]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xywy { get { return new uvec4(v[0], v[1], v[3], v[1]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xywz { get { return new uvec4(v[0], v[1], v[3], v[2]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xyww { get { return new uvec4(v[0], v[1], v[3], v[3]); } set { v[0] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xzxx { get { return new uvec4(v[0], v[2], v[0], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xzxy { get { return new uvec4(v[0], v[2], v[0], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xzxz { get { return new uvec4(v[0], v[2], v[0], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xzxw { get { return new uvec4(v[0], v[2], v[0], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xzyx { get { return new uvec4(v[0], v[2], v[1], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xzyy { get { return new uvec4(v[0], v[2], v[1], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xzyz { get { return new uvec4(v[0], v[2], v[1], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xzyw { get { return new uvec4(v[0], v[2], v[1], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xzzx { get { return new uvec4(v[0], v[2], v[2], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xzzy { get { return new uvec4(v[0], v[2], v[2], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xzzz { get { return new uvec4(v[0], v[2], v[2], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xzzw { get { return new uvec4(v[0], v[2], v[2], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xzwx { get { return new uvec4(v[0], v[2], v[3], v[0]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xzwy { get { return new uvec4(v[0], v[2], v[3], v[1]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xzwz { get { return new uvec4(v[0], v[2], v[3], v[2]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xzww { get { return new uvec4(v[0], v[2], v[3], v[3]); } set { v[0] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xwxx { get { return new uvec4(v[0], v[3], v[0], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xwxy { get { return new uvec4(v[0], v[3], v[0], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xwxz { get { return new uvec4(v[0], v[3], v[0], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xwxw { get { return new uvec4(v[0], v[3], v[0], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xwyx { get { return new uvec4(v[0], v[3], v[1], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xwyy { get { return new uvec4(v[0], v[3], v[1], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xwyz { get { return new uvec4(v[0], v[3], v[1], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xwyw { get { return new uvec4(v[0], v[3], v[1], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xwzx { get { return new uvec4(v[0], v[3], v[2], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xwzy { get { return new uvec4(v[0], v[3], v[2], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xwzz { get { return new uvec4(v[0], v[3], v[2], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xwzw { get { return new uvec4(v[0], v[3], v[2], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 xwwx { get { return new uvec4(v[0], v[3], v[3], v[0]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 xwwy { get { return new uvec4(v[0], v[3], v[3], v[1]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 xwwz { get { return new uvec4(v[0], v[3], v[3], v[2]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 xwww { get { return new uvec4(v[0], v[3], v[3], v[3]); } set { v[0] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yxxx { get { return new uvec4(v[1], v[0], v[0], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxxy { get { return new uvec4(v[1], v[0], v[0], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxxz { get { return new uvec4(v[1], v[0], v[0], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yxxw { get { return new uvec4(v[1], v[0], v[0], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yxyx { get { return new uvec4(v[1], v[0], v[1], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxyy { get { return new uvec4(v[1], v[0], v[1], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxyz { get { return new uvec4(v[1], v[0], v[1], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yxyw { get { return new uvec4(v[1], v[0], v[1], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yxzx { get { return new uvec4(v[1], v[0], v[2], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxzy { get { return new uvec4(v[1], v[0], v[2], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxzz { get { return new uvec4(v[1], v[0], v[2], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yxzw { get { return new uvec4(v[1], v[0], v[2], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yxwx { get { return new uvec4(v[1], v[0], v[3], v[0]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yxwy { get { return new uvec4(v[1], v[0], v[3], v[1]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yxwz { get { return new uvec4(v[1], v[0], v[3], v[2]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yxww { get { return new uvec4(v[1], v[0], v[3], v[3]); } set { v[1] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yyxx { get { return new uvec4(v[1], v[1], v[0], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yyxy { get { return new uvec4(v[1], v[1], v[0], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yyxz { get { return new uvec4(v[1], v[1], v[0], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yyxw { get { return new uvec4(v[1], v[1], v[0], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yyyx { get { return new uvec4(v[1], v[1], v[1], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yyyy { get { return new uvec4(v[1], v[1], v[1], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yyyz { get { return new uvec4(v[1], v[1], v[1], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yyyw { get { return new uvec4(v[1], v[1], v[1], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yyzx { get { return new uvec4(v[1], v[1], v[2], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yyzy { get { return new uvec4(v[1], v[1], v[2], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yyzz { get { return new uvec4(v[1], v[1], v[2], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yyzw { get { return new uvec4(v[1], v[1], v[2], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yywx { get { return new uvec4(v[1], v[1], v[3], v[0]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yywy { get { return new uvec4(v[1], v[1], v[3], v[1]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yywz { get { return new uvec4(v[1], v[1], v[3], v[2]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yyww { get { return new uvec4(v[1], v[1], v[3], v[3]); } set { v[1] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yzxx { get { return new uvec4(v[1], v[2], v[0], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yzxy { get { return new uvec4(v[1], v[2], v[0], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yzxz { get { return new uvec4(v[1], v[2], v[0], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yzxw { get { return new uvec4(v[1], v[2], v[0], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yzyx { get { return new uvec4(v[1], v[2], v[1], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yzyy { get { return new uvec4(v[1], v[2], v[1], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yzyz { get { return new uvec4(v[1], v[2], v[1], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yzyw { get { return new uvec4(v[1], v[2], v[1], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yzzx { get { return new uvec4(v[1], v[2], v[2], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yzzy { get { return new uvec4(v[1], v[2], v[2], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yzzz { get { return new uvec4(v[1], v[2], v[2], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yzzw { get { return new uvec4(v[1], v[2], v[2], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 yzwx { get { return new uvec4(v[1], v[2], v[3], v[0]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 yzwy { get { return new uvec4(v[1], v[2], v[3], v[1]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 yzwz { get { return new uvec4(v[1], v[2], v[3], v[2]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 yzww { get { return new uvec4(v[1], v[2], v[3], v[3]); } set { v[1] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 ywxx { get { return new uvec4(v[1], v[3], v[0], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 ywxy { get { return new uvec4(v[1], v[3], v[0], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 ywxz { get { return new uvec4(v[1], v[3], v[0], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 ywxw { get { return new uvec4(v[1], v[3], v[0], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 ywyx { get { return new uvec4(v[1], v[3], v[1], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 ywyy { get { return new uvec4(v[1], v[3], v[1], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 ywyz { get { return new uvec4(v[1], v[3], v[1], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 ywyw { get { return new uvec4(v[1], v[3], v[1], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 ywzx { get { return new uvec4(v[1], v[3], v[2], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 ywzy { get { return new uvec4(v[1], v[3], v[2], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 ywzz { get { return new uvec4(v[1], v[3], v[2], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 ywzw { get { return new uvec4(v[1], v[3], v[2], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 ywwx { get { return new uvec4(v[1], v[3], v[3], v[0]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 ywwy { get { return new uvec4(v[1], v[3], v[3], v[1]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 ywwz { get { return new uvec4(v[1], v[3], v[3], v[2]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 ywww { get { return new uvec4(v[1], v[3], v[3], v[3]); } set { v[1] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zxxx { get { return new uvec4(v[2], v[0], v[0], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zxxy { get { return new uvec4(v[2], v[0], v[0], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zxxz { get { return new uvec4(v[2], v[0], v[0], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zxxw { get { return new uvec4(v[2], v[0], v[0], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zxyx { get { return new uvec4(v[2], v[0], v[1], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zxyy { get { return new uvec4(v[2], v[0], v[1], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zxyz { get { return new uvec4(v[2], v[0], v[1], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zxyw { get { return new uvec4(v[2], v[0], v[1], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zxzx { get { return new uvec4(v[2], v[0], v[2], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zxzy { get { return new uvec4(v[2], v[0], v[2], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zxzz { get { return new uvec4(v[2], v[0], v[2], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zxzw { get { return new uvec4(v[2], v[0], v[2], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zxwx { get { return new uvec4(v[2], v[0], v[3], v[0]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zxwy { get { return new uvec4(v[2], v[0], v[3], v[1]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zxwz { get { return new uvec4(v[2], v[0], v[3], v[2]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zxww { get { return new uvec4(v[2], v[0], v[3], v[3]); } set { v[2] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zyxx { get { return new uvec4(v[2], v[1], v[0], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zyxy { get { return new uvec4(v[2], v[1], v[0], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zyxz { get { return new uvec4(v[2], v[1], v[0], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zyxw { get { return new uvec4(v[2], v[1], v[0], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zyyx { get { return new uvec4(v[2], v[1], v[1], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zyyy { get { return new uvec4(v[2], v[1], v[1], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zyyz { get { return new uvec4(v[2], v[1], v[1], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zyyw { get { return new uvec4(v[2], v[1], v[1], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zyzx { get { return new uvec4(v[2], v[1], v[2], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zyzy { get { return new uvec4(v[2], v[1], v[2], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zyzz { get { return new uvec4(v[2], v[1], v[2], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zyzw { get { return new uvec4(v[2], v[1], v[2], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zywx { get { return new uvec4(v[2], v[1], v[3], v[0]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zywy { get { return new uvec4(v[2], v[1], v[3], v[1]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zywz { get { return new uvec4(v[2], v[1], v[3], v[2]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zyww { get { return new uvec4(v[2], v[1], v[3], v[3]); } set { v[2] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zzxx { get { return new uvec4(v[2], v[2], v[0], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zzxy { get { return new uvec4(v[2], v[2], v[0], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zzxz { get { return new uvec4(v[2], v[2], v[0], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zzxw { get { return new uvec4(v[2], v[2], v[0], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zzyx { get { return new uvec4(v[2], v[2], v[1], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zzyy { get { return new uvec4(v[2], v[2], v[1], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zzyz { get { return new uvec4(v[2], v[2], v[1], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zzyw { get { return new uvec4(v[2], v[2], v[1], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zzzx { get { return new uvec4(v[2], v[2], v[2], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zzzy { get { return new uvec4(v[2], v[2], v[2], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zzzz { get { return new uvec4(v[2], v[2], v[2], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zzzw { get { return new uvec4(v[2], v[2], v[2], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zzwx { get { return new uvec4(v[2], v[2], v[3], v[0]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zzwy { get { return new uvec4(v[2], v[2], v[3], v[1]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zzwz { get { return new uvec4(v[2], v[2], v[3], v[2]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zzww { get { return new uvec4(v[2], v[2], v[3], v[3]); } set { v[2] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zwxx { get { return new uvec4(v[2], v[3], v[0], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zwxy { get { return new uvec4(v[2], v[3], v[0], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zwxz { get { return new uvec4(v[2], v[3], v[0], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zwxw { get { return new uvec4(v[2], v[3], v[0], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zwyx { get { return new uvec4(v[2], v[3], v[1], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zwyy { get { return new uvec4(v[2], v[3], v[1], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zwyz { get { return new uvec4(v[2], v[3], v[1], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zwyw { get { return new uvec4(v[2], v[3], v[1], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zwzx { get { return new uvec4(v[2], v[3], v[2], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zwzy { get { return new uvec4(v[2], v[3], v[2], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zwzz { get { return new uvec4(v[2], v[3], v[2], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zwzw { get { return new uvec4(v[2], v[3], v[2], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 zwwx { get { return new uvec4(v[2], v[3], v[3], v[0]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 zwwy { get { return new uvec4(v[2], v[3], v[3], v[1]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 zwwz { get { return new uvec4(v[2], v[3], v[3], v[2]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 zwww { get { return new uvec4(v[2], v[3], v[3], v[3]); } set { v[2] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wxxx { get { return new uvec4(v[3], v[0], v[0], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wxxy { get { return new uvec4(v[3], v[0], v[0], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wxxz { get { return new uvec4(v[3], v[0], v[0], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wxxw { get { return new uvec4(v[3], v[0], v[0], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wxyx { get { return new uvec4(v[3], v[0], v[1], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wxyy { get { return new uvec4(v[3], v[0], v[1], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wxyz { get { return new uvec4(v[3], v[0], v[1], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wxyw { get { return new uvec4(v[3], v[0], v[1], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wxzx { get { return new uvec4(v[3], v[0], v[2], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wxzy { get { return new uvec4(v[3], v[0], v[2], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wxzz { get { return new uvec4(v[3], v[0], v[2], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wxzw { get { return new uvec4(v[3], v[0], v[2], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wxwx { get { return new uvec4(v[3], v[0], v[3], v[0]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wxwy { get { return new uvec4(v[3], v[0], v[3], v[1]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wxwz { get { return new uvec4(v[3], v[0], v[3], v[2]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wxww { get { return new uvec4(v[3], v[0], v[3], v[3]); } set { v[3] = value.v[0]; v[0] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wyxx { get { return new uvec4(v[3], v[1], v[0], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wyxy { get { return new uvec4(v[3], v[1], v[0], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wyxz { get { return new uvec4(v[3], v[1], v[0], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wyxw { get { return new uvec4(v[3], v[1], v[0], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wyyx { get { return new uvec4(v[3], v[1], v[1], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wyyy { get { return new uvec4(v[3], v[1], v[1], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wyyz { get { return new uvec4(v[3], v[1], v[1], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wyyw { get { return new uvec4(v[3], v[1], v[1], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wyzx { get { return new uvec4(v[3], v[1], v[2], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wyzy { get { return new uvec4(v[3], v[1], v[2], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wyzz { get { return new uvec4(v[3], v[1], v[2], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wyzw { get { return new uvec4(v[3], v[1], v[2], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wywx { get { return new uvec4(v[3], v[1], v[3], v[0]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wywy { get { return new uvec4(v[3], v[1], v[3], v[1]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wywz { get { return new uvec4(v[3], v[1], v[3], v[2]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wyww { get { return new uvec4(v[3], v[1], v[3], v[3]); } set { v[3] = value.v[0]; v[1] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wzxx { get { return new uvec4(v[3], v[2], v[0], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wzxy { get { return new uvec4(v[3], v[2], v[0], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wzxz { get { return new uvec4(v[3], v[2], v[0], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wzxw { get { return new uvec4(v[3], v[2], v[0], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wzyx { get { return new uvec4(v[3], v[2], v[1], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wzyy { get { return new uvec4(v[3], v[2], v[1], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wzyz { get { return new uvec4(v[3], v[2], v[1], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wzyw { get { return new uvec4(v[3], v[2], v[1], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wzzx { get { return new uvec4(v[3], v[2], v[2], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wzzy { get { return new uvec4(v[3], v[2], v[2], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wzzz { get { return new uvec4(v[3], v[2], v[2], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wzzw { get { return new uvec4(v[3], v[2], v[2], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wzwx { get { return new uvec4(v[3], v[2], v[3], v[0]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wzwy { get { return new uvec4(v[3], v[2], v[3], v[1]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wzwz { get { return new uvec4(v[3], v[2], v[3], v[2]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wzww { get { return new uvec4(v[3], v[2], v[3], v[3]); } set { v[3] = value.v[0]; v[2] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wwxx { get { return new uvec4(v[3], v[3], v[0], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wwxy { get { return new uvec4(v[3], v[3], v[0], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wwxz { get { return new uvec4(v[3], v[3], v[0], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wwxw { get { return new uvec4(v[3], v[3], v[0], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[0] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wwyx { get { return new uvec4(v[3], v[3], v[1], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wwyy { get { return new uvec4(v[3], v[3], v[1], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wwyz { get { return new uvec4(v[3], v[3], v[1], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wwyw { get { return new uvec4(v[3], v[3], v[1], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[1] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wwzx { get { return new uvec4(v[3], v[3], v[2], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wwzy { get { return new uvec4(v[3], v[3], v[2], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wwzz { get { return new uvec4(v[3], v[3], v[2], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wwzw { get { return new uvec4(v[3], v[3], v[2], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[2] = value.v[2]; v[3] = value.v[3]; } }
        public uvec4 wwwx { get { return new uvec4(v[3], v[3], v[3], v[0]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[0] = value.v[3]; } }
        public uvec4 wwwy { get { return new uvec4(v[3], v[3], v[3], v[1]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[1] = value.v[3]; } }
        public uvec4 wwwz { get { return new uvec4(v[3], v[3], v[3], v[2]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[2] = value.v[3]; } }
        public uvec4 wwww { get { return new uvec4(v[3], v[3], v[3], v[3]); } set { v[3] = value.v[0]; v[3] = value.v[1]; v[3] = value.v[2]; v[3] = value.v[3]; } }

        #endregion
    }

    class mat2
    {
        #region mat2

        internal vec2[] C;
        public vec2 this[int i] { get { return C[i]; } set { C[i] = value; } }

        public mat2() { C = new vec2[] { new vec2(), new vec2() }; }
        public mat2(float a) : this(a, a, a, a) { }
        public mat2(vec2 a, vec2 b) { C = new vec2[] { a, b }; }
        public mat2(
            float _00, float _10,
            float _01, float _11) : this(
                new vec2(_00, _10),
                new vec2(_01, _11))
        { }

        #endregion

        #region Operators

        public static mat2 operator *(mat2 a, mat2 b)
        {
            return new mat2(
                a[0][0] * b[0][0] + a[1][0] * b[0][1],
                a[0][0] * b[1][0] + a[1][0] * b[1][1],
                a[0][1] * b[0][0] + a[1][1] * b[0][1],
                a[0][1] * b[1][0] + a[1][1] * b[1][1]);
        }

        public static vec2 operator *(mat2 a, vec2 b)
        {
            return new vec2(
                a[0][0] * b[0] + a[1][0] * b[1],
                a[0][1] * b[0] + a[1][1] * b[1]);
        }

        public static vec2 operator *(vec2 a, mat2 b)
        {
            return new vec2(
                a[0] * b[0][0] + a[1] * b[0][1],
                a[0] * b[1][0] + a[1] * b[1][1]);
        }

        #endregion
    }

    class mat3
    {
        #region mat3

        internal vec3[] C;
        public vec3 this[int i] { get { return C[i]; } set { C[i] = value; } }

        public mat3() { C = new vec3[] { new vec3(), new vec3(), new vec3() }; }
        public mat3(float a) : this(new vec3(a), new vec3(a), new vec3(a)) { }
        public mat3(vec3 a, vec3 b, vec3 c) { C = new vec3[] { a, b, c }; }
        public mat3(
            float _00, float _10, float _20,
            float _01, float _11, float _21,
            float _02, float _12, float _22) : this(
                new vec3(_00, _10, _20),
                new vec3(_01, _11, _21),
                new vec3(_02, _12, _22))
        { }

        #endregion

        #region Operators

        public static mat3 operator *(mat3 a, mat3 b)
        {
            return new mat3(
                a[0][0] * b[0][0] + a[1][0] * b[0][1] + a[2][0] * b[0][2],
                a[0][0] * b[1][0] + a[1][0] * b[1][1] + a[2][0] * b[1][2],
                a[0][0] * b[2][0] + a[1][0] * b[2][1] + a[2][0] * b[2][2],
                a[0][1] * b[0][0] + a[1][1] * b[0][1] + a[2][1] * b[0][2],
                a[0][1] * b[1][0] + a[1][1] * b[1][1] + a[2][1] * b[1][2],
                a[0][1] * b[2][0] + a[1][1] * b[2][1] + a[2][1] * b[2][2],
                a[0][2] * b[0][0] + a[1][2] * b[0][1] + a[2][2] * b[0][2],
                a[0][2] * b[1][0] + a[1][2] * b[1][1] + a[2][2] * b[1][2],
                a[0][2] * b[2][0] + a[1][2] * b[2][1] + a[2][2] * b[2][2]);
        }

        #endregion
    }

    class mat4
    {
        #region mat4

        internal vec4[] C;
        public vec4 this[int i] { get { return C[i]; } set { C[i] = value; } }

        public mat4() { C = new vec4[] { new vec4(), new vec4(), new vec4(), new vec4() }; }
        public mat4(float a) : this(new vec4(a), new vec4(a), new vec4(a), new vec4(a)) { }
        public mat4(vec4 a, vec4 b, vec4 c, vec4 d) { C = new vec4[] { a, b, c, d }; }
        public mat4(
            float _00, float _10, float _20, float _30,
            float _01, float _11, float _21, float _31,
            float _02, float _12, float _22, float _32,
            float _03, float _13, float _23, float _33) : this(
                new vec4(_00, _10, _20, _30),
                new vec4(_01, _11, _21, _31),
                new vec4(_02, _12, _22, _32),
                new vec4(_03, _13, _23, _33))
        { }

        #endregion

        #region Operator

        public static mat4 operator *(mat4 a, mat4 b)
        {
            return new mat4(
                a[0][0] * b[0][0] + a[1][0] * b[0][1] + a[2][0] * b[0][2] + a[3][0] * b[0][3],
                a[0][0] * b[1][0] + a[1][0] * b[1][1] + a[2][0] * b[1][2] + a[3][0] * b[1][3],
                a[0][0] * b[2][0] + a[1][0] * b[2][1] + a[2][0] * b[2][2] + a[3][0] * b[2][3],
                a[0][0] * b[3][0] + a[1][0] * b[3][1] + a[2][0] * b[3][2] + a[3][0] * b[3][3],
                a[0][1] * b[0][0] + a[1][1] * b[0][1] + a[2][1] * b[0][2] + a[3][1] * b[0][3],
                a[0][1] * b[1][0] + a[1][1] * b[1][1] + a[2][1] * b[1][2] + a[3][1] * b[1][3],
                a[0][1] * b[2][0] + a[1][1] * b[2][1] + a[2][1] * b[2][2] + a[3][1] * b[2][3],
                a[0][1] * b[3][0] + a[1][1] * b[3][1] + a[2][1] * b[3][2] + a[3][1] * b[3][3],
                a[0][2] * b[0][0] + a[1][2] * b[0][1] + a[2][2] * b[0][2] + a[3][2] * b[0][3],
                a[0][2] * b[1][0] + a[1][2] * b[1][1] + a[2][2] * b[1][2] + a[3][2] * b[1][3],
                a[0][2] * b[2][0] + a[1][2] * b[2][1] + a[2][2] * b[2][2] + a[3][2] * b[2][3],
                a[0][2] * b[3][0] + a[1][3] * b[3][1] + a[2][2] * b[3][2] + a[3][2] * b[3][3],
                a[0][3] * b[0][0] + a[1][3] * b[0][1] + a[2][3] * b[0][2] + a[3][3] * b[0][3],
                a[0][3] * b[1][0] + a[1][3] * b[1][1] + a[2][3] * b[1][2] + a[3][3] * b[1][3],
                a[0][3] * b[2][0] + a[1][3] * b[2][1] + a[2][3] * b[2][2] + a[3][3] * b[2][3],
                a[0][3] * b[3][0] + a[1][3] * b[3][1] + a[2][3] * b[3][2] + a[3][3] * b[3][3]);
        }

        public static vec4 operator *(mat4 a, vec4 b)
        {
            return new vec4(
                a[0][0] * b[0] + a[1][0] * b[1] + a[2][0] * b[2] + a[3][0] * b[3],
                a[0][1] * b[0] + a[1][1] * b[1] + a[2][1] * b[2] + a[3][1] * b[3],
                a[0][2] * b[0] + a[1][2] * b[1] + a[2][2] * b[2] + a[3][2] * b[3],
                a[0][3] * b[0] + a[1][3] * b[1] + a[2][3] * b[2] + a[3][3] * b[3]);
        }

        public static vec4 operator *(vec4 a, mat4 b)
        {
            return new vec4(
                a[0] * b[0][0] + a[1] * b[0][1] + a[2] * b[0][2] + a[3] * b[0][3],
                a[0] * b[1][0] + a[1] * b[1][1] + a[2] * b[1][2] + a[3] * b[1][3],
                a[0] * b[2][0] + a[1] * b[2][1] + a[2] * b[2][2] + a[3] * b[2][3],
                a[0] * b[3][0] + a[1] * b[3][1] + a[2] * b[3][2] + a[3] * b[3][3]);
        }

        #endregion
    }
    
    #endregion

    #region Math

    class Math
    {
        #region 

        public static float abs(float a) => System.Math.Abs(a);
        public static double abs(double a) => System.Math.Abs(a);
        public static vec2 abs(vec2 a) => new vec2(System.Math.Abs(a.x), System.Math.Abs(a.y));
        public static vec3 abs(vec3 a) => new vec3(System.Math.Abs(a.x), System.Math.Abs(a.y), System.Math.Abs(a.z));
        public static vec4 abs(vec4 a) => new vec4(System.Math.Abs(a.x), System.Math.Abs(a.y), System.Math.Abs(a.z), System.Math.Abs(a.w));
        public static float fract(float a) => a - (long)a;
        public static double fract(double a) => a - (long)a;
        public static vec2 fract(vec2 a) => new vec2(a.x - (long)a.x, a.y - (long)a.y);
        public static vec3 fract(vec3 a) => new vec3(a.x - (long)a.x, a.y - (long)a.y, a.z - (long)a.z);
        public static vec4 fract(vec4 a) => new vec4(a.x - (long)a.x, a.y - (long)a.y, a.z - (long)a.z, a.w - (long)a.w);
        public static float sin(float a) => (float)System.Math.Sin(a);
        public static double sin(double a) => System.Math.Sin(a);
        public static vec2 sin(vec2 a) => new vec2((float)System.Math.Sin(a.x), (float)System.Math.Sin(a.y));
        public static vec3 sin(vec3 a) => new vec3((float)System.Math.Sin(a.x), (float)System.Math.Sin(a.y), (float)System.Math.Sin(a.z));
        public static vec4 sin(vec4 a) => new vec4((float)System.Math.Sin(a.x), (float)System.Math.Sin(a.y), (float)System.Math.Sin(a.z), (float)System.Math.Sin(a.w));
        public static float cos(float a) => (float)System.Math.Cos(a);
        public static double cos(double a) => System.Math.Cos(a);
        public static vec2 cos(vec2 a) => new vec2((float)System.Math.Cos(a.x), (float)System.Math.Cos(a.y));
        public static vec3 cos(vec3 a) => new vec3((float)System.Math.Cos(a.x), (float)System.Math.Cos(a.y), (float)System.Math.Cos(a.z));
        public static vec4 cos(vec4 a) => new vec4((float)System.Math.Cos(a.x), (float)System.Math.Cos(a.y), (float)System.Math.Cos(a.z), (float)System.Math.Cos(a.w));
        public static float tan(float a) => (float)System.Math.Tan(a);
        public static double tan(double a) => System.Math.Tan(a);
        public static vec2 tan(vec2 a) => new vec2((float)System.Math.Tan(a.x), (float)System.Math.Tan(a.y));
        public static vec3 tan(vec3 a) => new vec3((float)System.Math.Tan(a.x), (float)System.Math.Tan(a.y), (float)System.Math.Tan(a.z));
        public static vec4 tan(vec4 a) => new vec4((float)System.Math.Tan(a.x), (float)System.Math.Tan(a.y), (float)System.Math.Tan(a.z), (float)System.Math.Tan(a.w));
        public static float dot(vec2 a, vec2 b) => a.x * b.x + a.y * b.y;
        public static float dot(vec3 a, vec3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
        public static float dot(vec4 a, vec4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static vec3 cross(vec3 a, vec3 b) => new vec3(a.y * b.z - a.z * b.y, a.x * b.z - a.z * b.x, a.x * b.y - a.y * b.x);
        public static bool isnan(float a) => float.IsNaN(a);
        public static bool isnan(double a) => double.IsNaN(a);
        public static float dFdx(float a) { throw new NotImplementedException(); }
        public static double dFdx(double a) { throw new NotImplementedException(); }
        public static vec2 dFdx(vec2 a) { throw new NotImplementedException(); }
        public static vec3 dFdx(vec3 a) { throw new NotImplementedException(); }
        public static vec4 dFdx(vec4 a) { throw new NotImplementedException(); }
        public static float dFdy(float a) { throw new NotImplementedException(); }
        public static double dFdy(double a) { throw new NotImplementedException(); }
        public static vec2 dFdy(vec2 a) { throw new NotImplementedException(); }
        public static vec3 dFdy(vec3 a) { throw new NotImplementedException(); }
        public static vec4 dFdy(vec4 a) { throw new NotImplementedException(); }
        public static bvec2 lessThan(vec2 a, vec2 b) => new bvec2(a.x < b.x, a.y < b.y);
        public static bvec3 lessThan(vec3 a, vec3 b) => new bvec3(a.x < b.x, a.y < b.y, a.z < b.z);
        public static bvec4 lessThan(vec4 a, vec4 b) => new bvec4(a.x < b.x, a.y < b.y, a.z < b.z, a.w < b.w);
        public static bvec2 lessThanEqual(vec2 a, vec2 b) => new bvec2(a.x <= b.x, a.y <= b.y);
        public static bvec3 lessThanEqual(vec3 a, vec3 b) => new bvec3(a.x <= b.x, a.y <= b.y, a.z <= b.z);
        public static bvec4 lessThanEqual(vec4 a, vec4 b) => new bvec4(a.x <= b.x, a.y <= b.y, a.z <= b.z, a.w <= b.w);
        public static bvec2 greaterThan(vec2 a, vec2 b) => new bvec2(a.x > b.x, a.y > b.y);
        public static bvec3 greaterThan(vec3 a, vec3 b) => new bvec3(a.x > b.x, a.y > b.y, a.z > b.z);
        public static bvec4 greaterThan(vec4 a, vec4 b) => new bvec4(a.x > b.x, a.y > b.y, a.z > b.z, a.w > b.w);
        public static bvec2 greaterThanEqual(vec2 a, vec2 b) => new bvec2(a.x >= b.x, a.y >= b.y);
        public static bvec3 greaterThanEqual(vec3 a, vec3 b) => new bvec3(a.x >= b.x, a.y >= b.y, a.z >= b.z);
        public static bvec4 greaterThanEqual(vec4 a, vec4 b) => new bvec4(a.x >= b.x, a.y >= b.y, a.z >= b.z, a.w >= b.w);
        public static bool any(bvec2 a) => a.x || a.y;
        public static bool any(bvec3 a) => a.x || a.y || a.z;
        public static bool any(bvec4 a) => a.x || a.y || a.z || a.w;
        public static bool all(bvec2 a) => a.x && a.y;
        public static bool all(bvec3 a) => a.x && a.y && a.z;
        public static bool all(bvec4 a) => a.x && a.y && a.z && a.w;

        #endregion

        #region Constructors

        public static vec2 vec2(float a) => new vec2(a);
        public static vec2 vec2(float x, float y) => new vec2(x, y);
        public static vec3 vec3(float a) => new vec3(a);
        public static vec3 vec3(float x, float y, float z) => new vec3(x, y, z);
        public static vec4 vec4(float a) => new vec4(a);
        public static vec4 vec4(float x, float y, float z, float w) => new vec4(x, y, z, w);
        public static mat2 mat2(float a) => new mat2(a);
        public static mat2 mat2(vec2 a, vec2 b) => new mat2(a, b);
        public static mat2 mat2(
            float _00, float _10,
            float _01, float _11) => new mat2(
                _00, _10,
                _01, _11);
        public static mat3 mat3(float a) => new mat3(a);
        public static mat3 mat3(vec3 a, vec3 b, vec3 c) => new mat3(a, b, c);
        public static mat3 mat3(
            float _00, float _10, float _20,
            float _01, float _11, float _21,
            float _02, float _12, float _22) => new mat3(
                _00, _10, _20,
                _01, _11, _21,
                _02, _12, _22);
        public static mat4 mat4(float a) => new mat4(a);
        public static mat4 mat4(vec4 a, vec4 b, vec4 c, vec4 d) => new mat4(a, b, c, d);
        public static mat4 mat4(
            float _00, float _10, float _20, float _30,
            float _01, float _11, float _21, float _31,
            float _02, float _12, float _22, float _32,
            float _03, float _13, float _23, float _33) => new mat4(
                _00, _10, _20, _30,
                _01, _11, _21, _31,
                _02, _12, _22, _32,
                _03, _13, _23, _33);

        #endregion
    }

    #endregion

    #region Debug Super Classes

    class Shader : Math
    {
        public const int std430 = 430;

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

        public static void Init() { }
    }

    class vert : Shader
    {
        public static int gl_VertexID;
        public static int gl_InstanceID;
        public static vec4 gl_Position;
        public static float gl_PointSize;
        public static float[] gl_ClipDistance = new float[4];

        public static new void Init()
        {
            Shader.Init();
        }
    }

    class tess : Shader
    {
        public static new void Init()
        {
            Shader.Init();
        }
    }

    class eval : Shader
    {
        public static new void Init()
        {
            Shader.Init();
        }
    }

    class geom : Shader
    {
        public static new void Init()
        {
            Shader.Init();
        }
    }

    class frag : Shader
    {
        public static new void Init()
        {
            Shader.Init();
        }
    }

    class comp : Shader
    {
        public static new void Init()
        {
            Shader.Init();
        }
    }

    #endregion
}