using System;

#pragma warning disable IDE1006

namespace App.Glsl
{
    public struct vec2
    {
        public float x, y;
        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y };
        }

        #region vec2

        public vec2(float a) : this(a, a) { }
        public vec2(float x, float y) { this.x = x; this.y = y; }
        public vec2(float[] v) : this(v.Fetch(0), v.Fetch(1)) { }
        public vec2(byte[] data) : this((float[])data.To(typeof(float))) { }

        #endregion

        #region Operators

        public static vec2 operator +(vec2 a) => new vec2(a.x, a.y);
        public static vec2 operator -(vec2 a) => new vec2(-a.x, -a.y);
        public static vec2 operator +(vec2 a, vec2 b) => new vec2(a.x + b.x, a.y + b.y);
        public static vec2 operator +(vec2 a, float b) => new vec2(a.x + b, a.y + b);
        public static vec2 operator +(float a, vec2 b) => new vec2(a + b.x, a + b.y);
        public static vec2 operator -(vec2 a, vec2 b) => new vec2(a.x - b.x, a.y - b.y);
        public static vec2 operator -(vec2 a, ivec2 b) => new vec2(a.x - b.x, a.y - b.y);
        public static vec2 operator -(ivec2 a, vec2 b) => new vec2(a.x - b.x, a.y - b.y);
        public static vec2 operator -(vec2 a, float b) => new vec2(a.x - b, a.y - b);
        public static vec2 operator -(float a, vec2 b) => new vec2(a - b.x, a - b.y);
        public static vec2 operator -(vec2 a, int b) => new vec2(a.x - b, a.y - b);
        public static vec2 operator -(int a, vec2 b) => new vec2(a - b.x, a - b.y);
        public static vec2 operator *(vec2 a, vec2 b) => new vec2(a.x * b.x, a.y * b.y);
        public static vec2 operator *(vec2 a, ivec2 b) => new vec2(a.x * b.x, a.y * b.y);
        public static vec2 operator *(ivec2 a, vec2 b) => new vec2(a.x * b.x, a.y * b.y);
        public static vec2 operator *(vec2 a, float b) => new vec2(a.x * b, a.y * b);
        public static vec2 operator *(float a, vec2 b) => new vec2(a * b.x, a * b.y);
        public static vec2 operator *(vec2 a, int b) => new vec2(a.x * b, a.y * b);
        public static vec2 operator *(int a, vec2 b) => new vec2(a * b.x, a * b.y);
        public static vec2 operator /(vec2 a, vec2 b) => new vec2(a.x / b.x, a.y / b.y);
        public static vec2 operator /(vec2 a, ivec2 b) => new vec2(a.x / b.x, a.y / b.y);
        public static vec2 operator /(ivec2 a, vec2 b) => new vec2(a.x / b.x, a.y / b.y);
        public static vec2 operator /(vec2 a, float b) => new vec2(a.x / b, a.y / b);
        public static vec2 operator /(float a, vec2 b) => new vec2(a / b.x, a / b.y);
        public static vec2 operator /(vec2 a, int b) => new vec2(a.x / b, a.y / b);
        public static vec2 operator /(int a, vec2 b) => new vec2(a / b.x, a / b.y);

        #endregion

        #region Generated
        
        public vec2 xx { get => new vec2(x, x); set { x = value.x; x = value.y; } }
        public vec2 xy { get => new vec2(x, y); set { x = value.x; y = value.y; } }
        public vec2 yx { get => new vec2(y, x); set { y = value.x; x = value.y; } }
        public vec2 yy { get => new vec2(y, y); set { y = value.x; y = value.y; } }
        public vec3 xxx { get => new vec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public vec3 xxy { get => new vec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public vec3 xyx { get => new vec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public vec3 xyy { get => new vec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public vec3 yxx { get => new vec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public vec3 yxy { get => new vec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public vec3 yyx { get => new vec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public vec3 yyy { get => new vec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public vec4 xxxx { get => new vec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 xxxy { get => new vec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 xxyx { get => new vec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 xxyy { get => new vec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 xyxx { get => new vec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 xyxy { get => new vec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 xyyx { get => new vec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 xyyy { get => new vec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public vec4 yxxx { get => new vec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 yxxy { get => new vec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 yxyx { get => new vec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 yxyy { get => new vec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 yyxx { get => new vec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 yyxy { get => new vec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 yyyx { get => new vec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 yyyy { get => new vec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }

        #endregion
    }

    public struct dvec2
    {
        public double x, y;
        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y };
        }

        #region dvec2

        public dvec2(double a = 0) : this(a, a) { }
        public dvec2(double x, double y) { this.x = x; this.y = y; }
        public dvec2(double[] v) : this(v.Fetch(0), v.Fetch(1)) { }
        public dvec2(byte[] data) : this((double[])data.To(typeof(double))) { }

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
        
        public dvec2 xx { get => new dvec2(x, x); set { x = value.x; x = value.y; } }
        public dvec2 xy { get => new dvec2(x, y); set { x = value.x; y = value.y; } }
        public dvec2 yx { get => new dvec2(y, x); set { y = value.x; x = value.y; } }
        public dvec2 yy { get => new dvec2(y, y); set { y = value.x; y = value.y; } }
        public dvec3 xxx { get => new dvec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public dvec3 xxy { get => new dvec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public dvec3 xyx { get => new dvec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public dvec3 xyy { get => new dvec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public dvec3 yxx { get => new dvec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public dvec3 yxy { get => new dvec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public dvec3 yyx { get => new dvec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public dvec3 yyy { get => new dvec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public dvec4 xxxx { get => new dvec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 xxxy { get => new dvec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 xxyx { get => new dvec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 xxyy { get => new dvec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 xyxx { get => new dvec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 xyxy { get => new dvec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 xyyx { get => new dvec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 xyyy { get => new dvec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public dvec4 yxxx { get => new dvec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 yxxy { get => new dvec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 yxyx { get => new dvec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 yxyy { get => new dvec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 yyxx { get => new dvec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 yyxy { get => new dvec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 yyyx { get => new dvec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 yyyy { get => new dvec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }

        #endregion
    }

    public struct bvec2
    {
        public bool x, y;
        public bool this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y };
        }

        #region vec2

        public bvec2(bool a) : this(a, a) { }
        public bvec2(bool x, bool y) { this.x = x; this.y = y; }
        public bvec2(bool[] v) : this(v.Fetch(0), v.Fetch(1)) { }
        public bvec2(byte[] data) : this((bool[])data.To(typeof(bool))) { }

        #endregion

        #region Generated

        public bvec2 xx { get => new bvec2(x, x); set { x = value.x; x = value.y; } }
        public bvec2 xy { get => new bvec2(x, y); set { x = value.x; y = value.y; } }
        public bvec2 yx { get => new bvec2(y, x); set { y = value.x; x = value.y; } }
        public bvec2 yy { get => new bvec2(y, y); set { y = value.x; y = value.y; } }
        public bvec3 xxx { get => new bvec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public bvec3 xxy { get => new bvec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public bvec3 xyx { get => new bvec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public bvec3 xyy { get => new bvec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public bvec3 yxx { get => new bvec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public bvec3 yxy { get => new bvec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public bvec3 yyx { get => new bvec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public bvec3 yyy { get => new bvec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public bvec4 xxxx { get => new bvec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 xxxy { get => new bvec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 xxyx { get => new bvec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 xxyy { get => new bvec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 xyxx { get => new bvec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 xyxy { get => new bvec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 xyyx { get => new bvec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 xyyy { get => new bvec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public bvec4 yxxx { get => new bvec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 yxxy { get => new bvec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 yxyx { get => new bvec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 yxyy { get => new bvec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 yyxx { get => new bvec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 yyxy { get => new bvec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 yyyx { get => new bvec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 yyyy { get => new bvec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }

        #endregion
    }

    public struct ivec2
    {
        public int x, y;
        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y };
        }

        #region vec2

        public ivec2(int a) : this(a, a) { }
        public ivec2(int x, int y) { this.x = x; this.y = y; }
        public ivec2(int[] v) : this(v.Fetch(0), v.Fetch(1)) { }
        public ivec2(byte[] data) : this((int[])data.To(typeof(int))) { }

        #endregion

        #region Operators

        public static ivec2 operator +(ivec2 a) => new ivec2(a.x, a.y);
        public static ivec2 operator -(ivec2 a) => new ivec2(-a.x, -a.y);
        public static ivec2 operator +(ivec2 a, ivec2 b) => new ivec2(a.x + b.x, a.y + b.y);
        public static ivec2 operator +(ivec2 a, int b) => new ivec2(a.x + b, a.y + b);
        public static ivec2 operator +(int a, ivec2 b) => new ivec2(a + b.x, a + b.y);
        public static ivec2 operator -(ivec2 a, ivec2 b) => new ivec2(a.x - b.x, a.y - b.y);
        public static ivec2 operator -(ivec2 a, int b) => new ivec2(a.x - b, a.y - b);
        public static ivec2 operator -(int a, ivec2 b) => new ivec2(a - b.x, a - b.y);
        public static ivec2 operator *(ivec2 a, ivec2 b) => new ivec2(a.x * b.x, a.y * b.y);
        public static ivec2 operator *(ivec2 a, int b) => new ivec2(a.x * b, a.y * b);
        public static ivec2 operator *(int a, ivec2 b) => new ivec2(a * b.x, a * b.y);
        public static ivec2 operator /(ivec2 a, ivec2 b) => new ivec2(a.x / b.x, a.y / b.y);
        public static ivec2 operator /(ivec2 a, int b) => new ivec2(a.x / b, a.y / b);
        public static ivec2 operator /(int a, ivec2 b) => new ivec2(a / b.x, a / b.y);

        #endregion

        #region Generated

        public ivec2 xx { get => new ivec2(x, x); set { x = value.x; x = value.y; } }
        public ivec2 xy { get => new ivec2(x, y); set { x = value.x; y = value.y; } }
        public ivec2 yx { get => new ivec2(y, x); set { y = value.x; x = value.y; } }
        public ivec2 yy { get => new ivec2(y, y); set { y = value.x; y = value.y; } }
        public ivec3 xxx { get => new ivec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public ivec3 xxy { get => new ivec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public ivec3 xyx { get => new ivec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public ivec3 xyy { get => new ivec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public ivec3 yxx { get => new ivec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public ivec3 yxy { get => new ivec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public ivec3 yyx { get => new ivec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public ivec3 yyy { get => new ivec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public ivec4 xxxx { get => new ivec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 xxxy { get => new ivec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 xxyx { get => new ivec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 xxyy { get => new ivec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 xyxx { get => new ivec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 xyxy { get => new ivec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 xyyx { get => new ivec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 xyyy { get => new ivec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public ivec4 yxxx { get => new ivec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 yxxy { get => new ivec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 yxyx { get => new ivec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 yxyy { get => new ivec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 yyxx { get => new ivec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 yyxy { get => new ivec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 yyyx { get => new ivec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 yyyy { get => new ivec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }

        #endregion
    }

    public struct uvec2
    {
        public uint x, y;
        public uint this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y };
        }

        #region vec2

        public uvec2(uint a) : this(a, a) { }
        public uvec2(uint x, uint y) { this.x = x; this.y = y; }
        public uvec2(uint[] v) : this(v.Fetch(0), v.Fetch(1)) { }
        public uvec2(byte[] data) : this((uint[])data.To(typeof(uint))) { }

        #endregion

        #region Operators

        public static uvec2 operator +(uvec2 a) => new uvec2(a.x, a.y);
        public static ivec2 operator -(uvec2 a) => new ivec2((int)-a.x, (int)-a.y);
        public static uvec2 operator +(uvec2 a, uvec2 b) => new uvec2(a.x + b.x, a.y + b.y);
        public static uvec2 operator +(uvec2 a, uint b) => new uvec2(a.x + b, a.y + b);
        public static uvec2 operator +(uint a, uvec2 b) => new uvec2(a + b.x, a + b.y);
        public static uvec2 operator -(uvec2 a, uvec2 b) => new uvec2(a.x - b.x, a.y - b.y);
        public static uvec2 operator -(uvec2 a, uint b) => new uvec2(a.x - b, a.y - b);
        public static uvec2 operator -(uint a, uvec2 b) => new uvec2(a - b.x, a - b.y);
        public static uvec2 operator *(uvec2 a, uvec2 b) => new uvec2(a.x * b.x, a.y * b.y);
        public static uvec2 operator *(uvec2 a, uint b) => new uvec2(a.x * b, a.y * b);
        public static uvec2 operator *(uint a, uvec2 b) => new uvec2(a * b.x, a * b.y);
        public static uvec2 operator /(uvec2 a, uvec2 b) => new uvec2(a.x / b.x, a.y / b.y);
        public static uvec2 operator /(uvec2 a, uint b) => new uvec2(a.x / b, a.y / b);
        public static uvec2 operator /(uint a, uvec2 b) => new uvec2(a / b.x, a / b.y);

        #endregion

        #region Generated

        public uvec2 xx { get => new uvec2(x, x); set { x = value.x; x = value.y; } }
        public uvec2 xy { get => new uvec2(x, y); set { x = value.x; y = value.y; } }
        public uvec2 yx { get => new uvec2(y, x); set { y = value.x; x = value.y; } }
        public uvec2 yy { get => new uvec2(y, y); set { y = value.x; y = value.y; } }
        public uvec3 xxx { get => new uvec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public uvec3 xxy { get => new uvec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public uvec3 xyx { get => new uvec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public uvec3 xyy { get => new uvec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public uvec3 yxx { get => new uvec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public uvec3 yxy { get => new uvec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public uvec3 yyx { get => new uvec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public uvec3 yyy { get => new uvec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public uvec4 xxxx { get => new uvec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 xxxy { get => new uvec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 xxyx { get => new uvec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 xxyy { get => new uvec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 xyxx { get => new uvec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 xyxy { get => new uvec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 xyyx { get => new uvec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 xyyy { get => new uvec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public uvec4 yxxx { get => new uvec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 yxxy { get => new uvec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 yxyx { get => new uvec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 yxyy { get => new uvec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 yyxx { get => new uvec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 yyxy { get => new uvec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 yyyx { get => new uvec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 yyyy { get => new uvec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }

        #endregion
    }
}
