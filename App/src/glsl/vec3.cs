using System;

namespace App.Glsl
{
    public struct vec3
    {
        public float x, y, z;
        public float this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString() => "(" + x + ", " + y + ", " + z + ")";
        public Array ToArray() => new[] { x, y, z };

        #region vec3

        public vec3(float a = 0f) : this(a, a, a) { }
        public vec3(float[] v) : this(v[0], v[1], v[2]) { }
        public vec3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public vec3(vec2 xy, float z) : this(xy.x, xy.y, z) { }
        public vec3(float x, vec2 yz) : this(x, yz.x, yz.y) { }
        public vec3(byte[] data) : this((float[])data.To(typeof(float))) { }

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
        
        public vec2 xx { get { return new vec2(x, x); } set { x = value.x; x = value.y; } }
        public vec2 xy { get { return new vec2(x, y); } set { x = value.x; y = value.y; } }
        public vec2 xz { get { return new vec2(x, z); } set { x = value.x; z = value.y; } }
        public vec2 yx { get { return new vec2(y, x); } set { y = value.x; x = value.y; } }
        public vec2 yy { get { return new vec2(y, y); } set { y = value.x; y = value.y; } }
        public vec2 yz { get { return new vec2(y, z); } set { y = value.x; z = value.y; } }
        public vec2 zx { get { return new vec2(z, x); } set { z = value.x; x = value.y; } }
        public vec2 zy { get { return new vec2(z, y); } set { z = value.x; y = value.y; } }
        public vec2 zz { get { return new vec2(z, z); } set { z = value.x; z = value.y; } }
        public vec3 xxx { get { return new vec3(x, x, x); } set { x = value.x; x = value.y; x = value.z; } }
        public vec3 xxy { get { return new vec3(x, x, y); } set { x = value.x; x = value.y; y = value.z; } }
        public vec3 xxz { get { return new vec3(x, x, z); } set { x = value.x; x = value.y; z = value.z; } }
        public vec3 xyx { get { return new vec3(x, y, x); } set { x = value.x; y = value.y; x = value.z; } }
        public vec3 xyy { get { return new vec3(x, y, y); } set { x = value.x; y = value.y; y = value.z; } }
        public vec3 xyz { get { return new vec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public vec3 xzx { get { return new vec3(x, z, x); } set { x = value.x; z = value.y; x = value.z; } }
        public vec3 xzy { get { return new vec3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }
        public vec3 xzz { get { return new vec3(x, z, z); } set { x = value.x; z = value.y; z = value.z; } }
        public vec3 yxx { get { return new vec3(y, x, x); } set { y = value.x; x = value.y; x = value.z; } }
        public vec3 yxy { get { return new vec3(y, x, y); } set { y = value.x; x = value.y; y = value.z; } }
        public vec3 yxz { get { return new vec3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }
        public vec3 yyx { get { return new vec3(y, y, x); } set { y = value.x; y = value.y; x = value.z; } }
        public vec3 yyy { get { return new vec3(y, y, y); } set { y = value.x; y = value.y; y = value.z; } }
        public vec3 yyz { get { return new vec3(y, y, z); } set { y = value.x; y = value.y; z = value.z; } }
        public vec3 yzx { get { return new vec3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }
        public vec3 yzy { get { return new vec3(y, z, y); } set { y = value.x; z = value.y; y = value.z; } }
        public vec3 yzz { get { return new vec3(y, z, z); } set { y = value.x; z = value.y; z = value.z; } }
        public vec3 zxx { get { return new vec3(z, x, x); } set { z = value.x; x = value.y; x = value.z; } }
        public vec3 zxy { get { return new vec3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }
        public vec3 zxz { get { return new vec3(z, x, z); } set { z = value.x; x = value.y; z = value.z; } }
        public vec3 zyx { get { return new vec3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }
        public vec3 zyy { get { return new vec3(z, y, y); } set { z = value.x; y = value.y; y = value.z; } }
        public vec3 zyz { get { return new vec3(z, y, z); } set { z = value.x; y = value.y; z = value.z; } }
        public vec3 zzx { get { return new vec3(z, z, x); } set { z = value.x; z = value.y; x = value.z; } }
        public vec3 zzy { get { return new vec3(z, z, y); } set { z = value.x; z = value.y; y = value.z; } }
        public vec3 zzz { get { return new vec3(z, z, z); } set { z = value.x; z = value.y; z = value.z; } }
        public vec4 xxxx { get { return new vec4(x, x, x, x); } set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 xxxy { get { return new vec4(x, x, x, y); } set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 xxxz { get { return new vec4(x, x, x, z); } set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public vec4 xxyx { get { return new vec4(x, x, y, x); } set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 xxyy { get { return new vec4(x, x, y, y); } set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 xxyz { get { return new vec4(x, x, y, z); } set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public vec4 xxzx { get { return new vec4(x, x, z, x); } set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public vec4 xxzy { get { return new vec4(x, x, z, y); } set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public vec4 xxzz { get { return new vec4(x, x, z, z); } set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public vec4 xyxx { get { return new vec4(x, y, x, x); } set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 xyxy { get { return new vec4(x, y, x, y); } set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 xyxz { get { return new vec4(x, y, x, z); } set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public vec4 xyyx { get { return new vec4(x, y, y, x); } set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 xyyy { get { return new vec4(x, y, y, y); } set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public vec4 xyyz { get { return new vec4(x, y, y, z); } set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public vec4 xyzx { get { return new vec4(x, y, z, x); } set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public vec4 xyzy { get { return new vec4(x, y, z, y); } set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public vec4 xyzz { get { return new vec4(x, y, z, z); } set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public vec4 xzxx { get { return new vec4(x, z, x, x); } set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public vec4 xzxy { get { return new vec4(x, z, x, y); } set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public vec4 xzxz { get { return new vec4(x, z, x, z); } set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public vec4 xzyx { get { return new vec4(x, z, y, x); } set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public vec4 xzyy { get { return new vec4(x, z, y, y); } set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public vec4 xzyz { get { return new vec4(x, z, y, z); } set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public vec4 xzzx { get { return new vec4(x, z, z, x); } set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public vec4 xzzy { get { return new vec4(x, z, z, y); } set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public vec4 xzzz { get { return new vec4(x, z, z, z); } set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public vec4 yxxx { get { return new vec4(y, x, x, x); } set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 yxxy { get { return new vec4(y, x, x, y); } set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 yxxz { get { return new vec4(y, x, x, z); } set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public vec4 yxyx { get { return new vec4(y, x, y, x); } set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 yxyy { get { return new vec4(y, x, y, y); } set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 yxyz { get { return new vec4(y, x, y, z); } set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public vec4 yxzx { get { return new vec4(y, x, z, x); } set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public vec4 yxzy { get { return new vec4(y, x, z, y); } set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public vec4 yxzz { get { return new vec4(y, x, z, z); } set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public vec4 yyxx { get { return new vec4(y, y, x, x); } set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 yyxy { get { return new vec4(y, y, x, y); } set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 yyxz { get { return new vec4(y, y, x, z); } set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public vec4 yyyx { get { return new vec4(y, y, y, x); } set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 yyyy { get { return new vec4(y, y, y, y); } set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public vec4 yyyz { get { return new vec4(y, y, y, z); } set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public vec4 yyzx { get { return new vec4(y, y, z, x); } set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public vec4 yyzy { get { return new vec4(y, y, z, y); } set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public vec4 yyzz { get { return new vec4(y, y, z, z); } set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public vec4 yzxx { get { return new vec4(y, z, x, x); } set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public vec4 yzxy { get { return new vec4(y, z, x, y); } set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public vec4 yzxz { get { return new vec4(y, z, x, z); } set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public vec4 yzyx { get { return new vec4(y, z, y, x); } set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public vec4 yzyy { get { return new vec4(y, z, y, y); } set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public vec4 yzyz { get { return new vec4(y, z, y, z); } set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public vec4 yzzx { get { return new vec4(y, z, z, x); } set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public vec4 yzzy { get { return new vec4(y, z, z, y); } set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public vec4 yzzz { get { return new vec4(y, z, z, z); } set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public vec4 zxxx { get { return new vec4(z, x, x, x); } set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 zxxy { get { return new vec4(z, x, x, y); } set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 zxxz { get { return new vec4(z, x, x, z); } set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public vec4 zxyx { get { return new vec4(z, x, y, x); } set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 zxyy { get { return new vec4(z, x, y, y); } set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 zxyz { get { return new vec4(z, x, y, z); } set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public vec4 zxzx { get { return new vec4(z, x, z, x); } set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public vec4 zxzy { get { return new vec4(z, x, z, y); } set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public vec4 zxzz { get { return new vec4(z, x, z, z); } set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public vec4 zyxx { get { return new vec4(z, y, x, x); } set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 zyxy { get { return new vec4(z, y, x, y); } set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 zyxz { get { return new vec4(z, y, x, z); } set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public vec4 zyyx { get { return new vec4(z, y, y, x); } set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 zyyy { get { return new vec4(z, y, y, y); } set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public vec4 zyyz { get { return new vec4(z, y, y, z); } set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public vec4 zyzx { get { return new vec4(z, y, z, x); } set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public vec4 zyzy { get { return new vec4(z, y, z, y); } set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public vec4 zyzz { get { return new vec4(z, y, z, z); } set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public vec4 zzxx { get { return new vec4(z, z, x, x); } set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public vec4 zzxy { get { return new vec4(z, z, x, y); } set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public vec4 zzxz { get { return new vec4(z, z, x, z); } set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public vec4 zzyx { get { return new vec4(z, z, y, x); } set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public vec4 zzyy { get { return new vec4(z, z, y, y); } set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public vec4 zzyz { get { return new vec4(z, z, y, z); } set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public vec4 zzzx { get { return new vec4(z, z, z, x); } set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public vec4 zzzy { get { return new vec4(z, z, z, y); } set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public vec4 zzzz { get { return new vec4(z, z, z, z); } set { z = value.x; z = value.y; z = value.z; z = value.w; } }

        #endregion
    }

    public struct dvec3
    {
        public double x, y, z;
        public double this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString() => "(" + x + ", " + y + ", " + z + ")";
        public Array ToArray() => new[] { x, y, z };

        #region vec3

        public dvec3(double a) : this(a, a, a) { }
        public dvec3(double[] v) : this(v[0], v[1], v[2]) { }
        public dvec3(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }
        public dvec3(dvec2 xy, double z) : this(xy.x, xy.y, z) { }
        public dvec3(double x, dvec2 yz) : this(x, yz.x, yz.y) { }
        public dvec3(byte[] data) : this((double[])data.To(typeof(double))) { }

        #endregion

        #region Operators

        public static dvec3 operator +(dvec3 a) => new dvec3(a.x, a.y, a.z);
        public static dvec3 operator -(dvec3 a) => new dvec3(-a.x, -a.y, -a.z);
        public static dvec3 operator +(dvec3 a, dvec3 b) => new dvec3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static dvec3 operator +(dvec3 a, double b) => new dvec3(a.x + b, a.y + b, a.z + b);
        public static dvec3 operator +(double a, dvec3 b) => new dvec3(a + b.x, a + b.y, a + b.z);
        public static dvec3 operator -(dvec3 a, dvec3 b) => new dvec3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static dvec3 operator -(dvec3 a, double b) => new dvec3(a.x - b, a.y - b, a.z - b);
        public static dvec3 operator -(double a, dvec3 b) => new dvec3(a - b.x, a - b.y, a - b.z);
        public static dvec3 operator *(dvec3 a, dvec3 b) => new dvec3(a.x * b.x, a.y * b.y, a.z * b.z);
        public static dvec3 operator *(dvec3 a, double b) => new dvec3(a.x * b, a.y * b, a.z * b);
        public static dvec3 operator *(double a, dvec3 b) => new dvec3(a * b.x, a * b.y, a * b.z);
        public static dvec3 operator /(dvec3 a, dvec3 b) => new dvec3(a.x / b.x, a.y / b.y, a.z / b.z);
        public static dvec3 operator /(double a, dvec3 b) => new dvec3(a / b.x, a / b.y, a / b.z);
        public static dvec3 operator /(dvec3 a, double b) => new dvec3(a.x / b, a.y / b, a.z / b);

        #endregion

        #region Generated
        
        public dvec2 xx { get { return new dvec2(x, x); } set { x = value.x; x = value.y; } }
        public dvec2 xy { get { return new dvec2(x, y); } set { x = value.x; y = value.y; } }
        public dvec2 xz { get { return new dvec2(x, z); } set { x = value.x; z = value.y; } }
        public dvec2 yx { get { return new dvec2(y, x); } set { y = value.x; x = value.y; } }
        public dvec2 yy { get { return new dvec2(y, y); } set { y = value.x; y = value.y; } }
        public dvec2 yz { get { return new dvec2(y, z); } set { y = value.x; z = value.y; } }
        public dvec2 zx { get { return new dvec2(z, x); } set { z = value.x; x = value.y; } }
        public dvec2 zy { get { return new dvec2(z, y); } set { z = value.x; y = value.y; } }
        public dvec2 zz { get { return new dvec2(z, z); } set { z = value.x; z = value.y; } }
        public dvec3 xxx { get { return new dvec3(x, x, x); } set { x = value.x; x = value.y; x = value.z; } }
        public dvec3 xxy { get { return new dvec3(x, x, y); } set { x = value.x; x = value.y; y = value.z; } }
        public dvec3 xxz { get { return new dvec3(x, x, z); } set { x = value.x; x = value.y; z = value.z; } }
        public dvec3 xyx { get { return new dvec3(x, y, x); } set { x = value.x; y = value.y; x = value.z; } }
        public dvec3 xyy { get { return new dvec3(x, y, y); } set { x = value.x; y = value.y; y = value.z; } }
        public dvec3 xyz { get { return new dvec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public dvec3 xzx { get { return new dvec3(x, z, x); } set { x = value.x; z = value.y; x = value.z; } }
        public dvec3 xzy { get { return new dvec3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }
        public dvec3 xzz { get { return new dvec3(x, z, z); } set { x = value.x; z = value.y; z = value.z; } }
        public dvec3 yxx { get { return new dvec3(y, x, x); } set { y = value.x; x = value.y; x = value.z; } }
        public dvec3 yxy { get { return new dvec3(y, x, y); } set { y = value.x; x = value.y; y = value.z; } }
        public dvec3 yxz { get { return new dvec3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }
        public dvec3 yyx { get { return new dvec3(y, y, x); } set { y = value.x; y = value.y; x = value.z; } }
        public dvec3 yyy { get { return new dvec3(y, y, y); } set { y = value.x; y = value.y; y = value.z; } }
        public dvec3 yyz { get { return new dvec3(y, y, z); } set { y = value.x; y = value.y; z = value.z; } }
        public dvec3 yzx { get { return new dvec3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }
        public dvec3 yzy { get { return new dvec3(y, z, y); } set { y = value.x; z = value.y; y = value.z; } }
        public dvec3 yzz { get { return new dvec3(y, z, z); } set { y = value.x; z = value.y; z = value.z; } }
        public dvec3 zxx { get { return new dvec3(z, x, x); } set { z = value.x; x = value.y; x = value.z; } }
        public dvec3 zxy { get { return new dvec3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }
        public dvec3 zxz { get { return new dvec3(z, x, z); } set { z = value.x; x = value.y; z = value.z; } }
        public dvec3 zyx { get { return new dvec3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }
        public dvec3 zyy { get { return new dvec3(z, y, y); } set { z = value.x; y = value.y; y = value.z; } }
        public dvec3 zyz { get { return new dvec3(z, y, z); } set { z = value.x; y = value.y; z = value.z; } }
        public dvec3 zzx { get { return new dvec3(z, z, x); } set { z = value.x; z = value.y; x = value.z; } }
        public dvec3 zzy { get { return new dvec3(z, z, y); } set { z = value.x; z = value.y; y = value.z; } }
        public dvec3 zzz { get { return new dvec3(z, z, z); } set { z = value.x; z = value.y; z = value.z; } }
        public dvec4 xxxx { get { return new dvec4(x, x, x, x); } set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 xxxy { get { return new dvec4(x, x, x, y); } set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 xxxz { get { return new dvec4(x, x, x, z); } set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public dvec4 xxyx { get { return new dvec4(x, x, y, x); } set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 xxyy { get { return new dvec4(x, x, y, y); } set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 xxyz { get { return new dvec4(x, x, y, z); } set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public dvec4 xxzx { get { return new dvec4(x, x, z, x); } set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public dvec4 xxzy { get { return new dvec4(x, x, z, y); } set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public dvec4 xxzz { get { return new dvec4(x, x, z, z); } set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public dvec4 xyxx { get { return new dvec4(x, y, x, x); } set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 xyxy { get { return new dvec4(x, y, x, y); } set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 xyxz { get { return new dvec4(x, y, x, z); } set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public dvec4 xyyx { get { return new dvec4(x, y, y, x); } set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 xyyy { get { return new dvec4(x, y, y, y); } set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public dvec4 xyyz { get { return new dvec4(x, y, y, z); } set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public dvec4 xyzx { get { return new dvec4(x, y, z, x); } set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public dvec4 xyzy { get { return new dvec4(x, y, z, y); } set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public dvec4 xyzz { get { return new dvec4(x, y, z, z); } set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public dvec4 xzxx { get { return new dvec4(x, z, x, x); } set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public dvec4 xzxy { get { return new dvec4(x, z, x, y); } set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public dvec4 xzxz { get { return new dvec4(x, z, x, z); } set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public dvec4 xzyx { get { return new dvec4(x, z, y, x); } set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public dvec4 xzyy { get { return new dvec4(x, z, y, y); } set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public dvec4 xzyz { get { return new dvec4(x, z, y, z); } set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public dvec4 xzzx { get { return new dvec4(x, z, z, x); } set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public dvec4 xzzy { get { return new dvec4(x, z, z, y); } set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public dvec4 xzzz { get { return new dvec4(x, z, z, z); } set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public dvec4 yxxx { get { return new dvec4(y, x, x, x); } set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 yxxy { get { return new dvec4(y, x, x, y); } set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 yxxz { get { return new dvec4(y, x, x, z); } set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public dvec4 yxyx { get { return new dvec4(y, x, y, x); } set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 yxyy { get { return new dvec4(y, x, y, y); } set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 yxyz { get { return new dvec4(y, x, y, z); } set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public dvec4 yxzx { get { return new dvec4(y, x, z, x); } set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public dvec4 yxzy { get { return new dvec4(y, x, z, y); } set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public dvec4 yxzz { get { return new dvec4(y, x, z, z); } set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public dvec4 yyxx { get { return new dvec4(y, y, x, x); } set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 yyxy { get { return new dvec4(y, y, x, y); } set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 yyxz { get { return new dvec4(y, y, x, z); } set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public dvec4 yyyx { get { return new dvec4(y, y, y, x); } set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 yyyy { get { return new dvec4(y, y, y, y); } set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public dvec4 yyyz { get { return new dvec4(y, y, y, z); } set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public dvec4 yyzx { get { return new dvec4(y, y, z, x); } set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public dvec4 yyzy { get { return new dvec4(y, y, z, y); } set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public dvec4 yyzz { get { return new dvec4(y, y, z, z); } set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public dvec4 yzxx { get { return new dvec4(y, z, x, x); } set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public dvec4 yzxy { get { return new dvec4(y, z, x, y); } set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public dvec4 yzxz { get { return new dvec4(y, z, x, z); } set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public dvec4 yzyx { get { return new dvec4(y, z, y, x); } set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public dvec4 yzyy { get { return new dvec4(y, z, y, y); } set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public dvec4 yzyz { get { return new dvec4(y, z, y, z); } set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public dvec4 yzzx { get { return new dvec4(y, z, z, x); } set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public dvec4 yzzy { get { return new dvec4(y, z, z, y); } set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public dvec4 yzzz { get { return new dvec4(y, z, z, z); } set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public dvec4 zxxx { get { return new dvec4(z, x, x, x); } set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 zxxy { get { return new dvec4(z, x, x, y); } set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 zxxz { get { return new dvec4(z, x, x, z); } set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public dvec4 zxyx { get { return new dvec4(z, x, y, x); } set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 zxyy { get { return new dvec4(z, x, y, y); } set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 zxyz { get { return new dvec4(z, x, y, z); } set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public dvec4 zxzx { get { return new dvec4(z, x, z, x); } set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public dvec4 zxzy { get { return new dvec4(z, x, z, y); } set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public dvec4 zxzz { get { return new dvec4(z, x, z, z); } set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public dvec4 zyxx { get { return new dvec4(z, y, x, x); } set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 zyxy { get { return new dvec4(z, y, x, y); } set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 zyxz { get { return new dvec4(z, y, x, z); } set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public dvec4 zyyx { get { return new dvec4(z, y, y, x); } set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 zyyy { get { return new dvec4(z, y, y, y); } set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public dvec4 zyyz { get { return new dvec4(z, y, y, z); } set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public dvec4 zyzx { get { return new dvec4(z, y, z, x); } set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public dvec4 zyzy { get { return new dvec4(z, y, z, y); } set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public dvec4 zyzz { get { return new dvec4(z, y, z, z); } set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public dvec4 zzxx { get { return new dvec4(z, z, x, x); } set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public dvec4 zzxy { get { return new dvec4(z, z, x, y); } set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public dvec4 zzxz { get { return new dvec4(z, z, x, z); } set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public dvec4 zzyx { get { return new dvec4(z, z, y, x); } set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public dvec4 zzyy { get { return new dvec4(z, z, y, y); } set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public dvec4 zzyz { get { return new dvec4(z, z, y, z); } set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public dvec4 zzzx { get { return new dvec4(z, z, z, x); } set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public dvec4 zzzy { get { return new dvec4(z, z, z, y); } set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public dvec4 zzzz { get { return new dvec4(z, z, z, z); } set { z = value.x; z = value.y; z = value.z; z = value.w; } }

        #endregion
    }

    public struct bvec3
    {
        public bool x, y, z;
        public bool this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString() => "(" + x + ", " + y + ", " + z + ")";
        public Array ToArray() => new[] { x, y, z };

        #region vec3

        public bvec3(bool a) : this(a, a, a) { }
        public bvec3(bool[] v) : this(v[0], v[1], v[2]) { }
        public bvec3(bool x, bool y, bool z) { this.x = x; this.y = y; this.z = z; }
        public bvec3(bvec2 xy, bool z) : this(xy.x, xy.y, z) { }
        public bvec3(bool x, bvec2 yz) : this(x, yz.x, yz.y) { }
        public bvec3(byte[] data) : this((bool[])data.To(typeof(bool))) { }

        #endregion

        #region Generated
        
        public bvec2 xx { get { return new bvec2(x, x); } set { x = value.x; x = value.y; } }
        public bvec2 xy { get { return new bvec2(x, y); } set { x = value.x; y = value.y; } }
        public bvec2 xz { get { return new bvec2(x, z); } set { x = value.x; z = value.y; } }
        public bvec2 yx { get { return new bvec2(y, x); } set { y = value.x; x = value.y; } }
        public bvec2 yy { get { return new bvec2(y, y); } set { y = value.x; y = value.y; } }
        public bvec2 yz { get { return new bvec2(y, z); } set { y = value.x; z = value.y; } }
        public bvec2 zx { get { return new bvec2(z, x); } set { z = value.x; x = value.y; } }
        public bvec2 zy { get { return new bvec2(z, y); } set { z = value.x; y = value.y; } }
        public bvec2 zz { get { return new bvec2(z, z); } set { z = value.x; z = value.y; } }
        public bvec3 xxx { get { return new bvec3(x, x, x); } set { x = value.x; x = value.y; x = value.z; } }
        public bvec3 xxy { get { return new bvec3(x, x, y); } set { x = value.x; x = value.y; y = value.z; } }
        public bvec3 xxz { get { return new bvec3(x, x, z); } set { x = value.x; x = value.y; z = value.z; } }
        public bvec3 xyx { get { return new bvec3(x, y, x); } set { x = value.x; y = value.y; x = value.z; } }
        public bvec3 xyy { get { return new bvec3(x, y, y); } set { x = value.x; y = value.y; y = value.z; } }
        public bvec3 xyz { get { return new bvec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public bvec3 xzx { get { return new bvec3(x, z, x); } set { x = value.x; z = value.y; x = value.z; } }
        public bvec3 xzy { get { return new bvec3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }
        public bvec3 xzz { get { return new bvec3(x, z, z); } set { x = value.x; z = value.y; z = value.z; } }
        public bvec3 yxx { get { return new bvec3(y, x, x); } set { y = value.x; x = value.y; x = value.z; } }
        public bvec3 yxy { get { return new bvec3(y, x, y); } set { y = value.x; x = value.y; y = value.z; } }
        public bvec3 yxz { get { return new bvec3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }
        public bvec3 yyx { get { return new bvec3(y, y, x); } set { y = value.x; y = value.y; x = value.z; } }
        public bvec3 yyy { get { return new bvec3(y, y, y); } set { y = value.x; y = value.y; y = value.z; } }
        public bvec3 yyz { get { return new bvec3(y, y, z); } set { y = value.x; y = value.y; z = value.z; } }
        public bvec3 yzx { get { return new bvec3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }
        public bvec3 yzy { get { return new bvec3(y, z, y); } set { y = value.x; z = value.y; y = value.z; } }
        public bvec3 yzz { get { return new bvec3(y, z, z); } set { y = value.x; z = value.y; z = value.z; } }
        public bvec3 zxx { get { return new bvec3(z, x, x); } set { z = value.x; x = value.y; x = value.z; } }
        public bvec3 zxy { get { return new bvec3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }
        public bvec3 zxz { get { return new bvec3(z, x, z); } set { z = value.x; x = value.y; z = value.z; } }
        public bvec3 zyx { get { return new bvec3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }
        public bvec3 zyy { get { return new bvec3(z, y, y); } set { z = value.x; y = value.y; y = value.z; } }
        public bvec3 zyz { get { return new bvec3(z, y, z); } set { z = value.x; y = value.y; z = value.z; } }
        public bvec3 zzx { get { return new bvec3(z, z, x); } set { z = value.x; z = value.y; x = value.z; } }
        public bvec3 zzy { get { return new bvec3(z, z, y); } set { z = value.x; z = value.y; y = value.z; } }
        public bvec3 zzz { get { return new bvec3(z, z, z); } set { z = value.x; z = value.y; z = value.z; } }
        public bvec4 xxxx { get { return new bvec4(x, x, x, x); } set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 xxxy { get { return new bvec4(x, x, x, y); } set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 xxxz { get { return new bvec4(x, x, x, z); } set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public bvec4 xxyx { get { return new bvec4(x, x, y, x); } set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 xxyy { get { return new bvec4(x, x, y, y); } set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 xxyz { get { return new bvec4(x, x, y, z); } set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public bvec4 xxzx { get { return new bvec4(x, x, z, x); } set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public bvec4 xxzy { get { return new bvec4(x, x, z, y); } set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public bvec4 xxzz { get { return new bvec4(x, x, z, z); } set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public bvec4 xyxx { get { return new bvec4(x, y, x, x); } set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 xyxy { get { return new bvec4(x, y, x, y); } set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 xyxz { get { return new bvec4(x, y, x, z); } set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public bvec4 xyyx { get { return new bvec4(x, y, y, x); } set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 xyyy { get { return new bvec4(x, y, y, y); } set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public bvec4 xyyz { get { return new bvec4(x, y, y, z); } set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public bvec4 xyzx { get { return new bvec4(x, y, z, x); } set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public bvec4 xyzy { get { return new bvec4(x, y, z, y); } set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public bvec4 xyzz { get { return new bvec4(x, y, z, z); } set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public bvec4 xzxx { get { return new bvec4(x, z, x, x); } set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public bvec4 xzxy { get { return new bvec4(x, z, x, y); } set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public bvec4 xzxz { get { return new bvec4(x, z, x, z); } set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public bvec4 xzyx { get { return new bvec4(x, z, y, x); } set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public bvec4 xzyy { get { return new bvec4(x, z, y, y); } set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public bvec4 xzyz { get { return new bvec4(x, z, y, z); } set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public bvec4 xzzx { get { return new bvec4(x, z, z, x); } set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public bvec4 xzzy { get { return new bvec4(x, z, z, y); } set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public bvec4 xzzz { get { return new bvec4(x, z, z, z); } set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public bvec4 yxxx { get { return new bvec4(y, x, x, x); } set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 yxxy { get { return new bvec4(y, x, x, y); } set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 yxxz { get { return new bvec4(y, x, x, z); } set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public bvec4 yxyx { get { return new bvec4(y, x, y, x); } set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 yxyy { get { return new bvec4(y, x, y, y); } set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 yxyz { get { return new bvec4(y, x, y, z); } set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public bvec4 yxzx { get { return new bvec4(y, x, z, x); } set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public bvec4 yxzy { get { return new bvec4(y, x, z, y); } set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public bvec4 yxzz { get { return new bvec4(y, x, z, z); } set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public bvec4 yyxx { get { return new bvec4(y, y, x, x); } set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 yyxy { get { return new bvec4(y, y, x, y); } set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 yyxz { get { return new bvec4(y, y, x, z); } set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public bvec4 yyyx { get { return new bvec4(y, y, y, x); } set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 yyyy { get { return new bvec4(y, y, y, y); } set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public bvec4 yyyz { get { return new bvec4(y, y, y, z); } set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public bvec4 yyzx { get { return new bvec4(y, y, z, x); } set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public bvec4 yyzy { get { return new bvec4(y, y, z, y); } set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public bvec4 yyzz { get { return new bvec4(y, y, z, z); } set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public bvec4 yzxx { get { return new bvec4(y, z, x, x); } set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public bvec4 yzxy { get { return new bvec4(y, z, x, y); } set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public bvec4 yzxz { get { return new bvec4(y, z, x, z); } set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public bvec4 yzyx { get { return new bvec4(y, z, y, x); } set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public bvec4 yzyy { get { return new bvec4(y, z, y, y); } set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public bvec4 yzyz { get { return new bvec4(y, z, y, z); } set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public bvec4 yzzx { get { return new bvec4(y, z, z, x); } set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public bvec4 yzzy { get { return new bvec4(y, z, z, y); } set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public bvec4 yzzz { get { return new bvec4(y, z, z, z); } set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public bvec4 zxxx { get { return new bvec4(z, x, x, x); } set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 zxxy { get { return new bvec4(z, x, x, y); } set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 zxxz { get { return new bvec4(z, x, x, z); } set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public bvec4 zxyx { get { return new bvec4(z, x, y, x); } set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 zxyy { get { return new bvec4(z, x, y, y); } set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 zxyz { get { return new bvec4(z, x, y, z); } set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public bvec4 zxzx { get { return new bvec4(z, x, z, x); } set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public bvec4 zxzy { get { return new bvec4(z, x, z, y); } set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public bvec4 zxzz { get { return new bvec4(z, x, z, z); } set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public bvec4 zyxx { get { return new bvec4(z, y, x, x); } set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 zyxy { get { return new bvec4(z, y, x, y); } set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 zyxz { get { return new bvec4(z, y, x, z); } set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public bvec4 zyyx { get { return new bvec4(z, y, y, x); } set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 zyyy { get { return new bvec4(z, y, y, y); } set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public bvec4 zyyz { get { return new bvec4(z, y, y, z); } set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public bvec4 zyzx { get { return new bvec4(z, y, z, x); } set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public bvec4 zyzy { get { return new bvec4(z, y, z, y); } set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public bvec4 zyzz { get { return new bvec4(z, y, z, z); } set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public bvec4 zzxx { get { return new bvec4(z, z, x, x); } set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public bvec4 zzxy { get { return new bvec4(z, z, x, y); } set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public bvec4 zzxz { get { return new bvec4(z, z, x, z); } set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public bvec4 zzyx { get { return new bvec4(z, z, y, x); } set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public bvec4 zzyy { get { return new bvec4(z, z, y, y); } set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public bvec4 zzyz { get { return new bvec4(z, z, y, z); } set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public bvec4 zzzx { get { return new bvec4(z, z, z, x); } set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public bvec4 zzzy { get { return new bvec4(z, z, z, y); } set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public bvec4 zzzz { get { return new bvec4(z, z, z, z); } set { z = value.x; z = value.y; z = value.z; z = value.w; } }

        #endregion
    }

    public struct ivec3
    {
        public int x, y, z;
        public int this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString() => "(" + x + ", " + y + ", " + z + ")";
        public Array ToArray() => new[] { x, y, z };

        #region vec3

        public ivec3(int a) : this(a, a, a) { }
        public ivec3(int[] v) : this(v[0], v[1], v[2]) { }
        public ivec3(int x, int y, int z) { this.x = x; this.y = y; this.z = z; }
        public ivec3(ivec2 xy, int z) : this(xy.x, xy.y, z) { }
        public ivec3(int x, ivec2 yz) : this(x, yz.x, yz.y) { }
        public ivec3(byte[] data) : this((int[])data.To(typeof(int))) { }

        #endregion

        #region Generated
        
        public ivec2 xx { get { return new ivec2(x, x); } set { x = value.x; x = value.y; } }
        public ivec2 xy { get { return new ivec2(x, y); } set { x = value.x; y = value.y; } }
        public ivec2 xz { get { return new ivec2(x, z); } set { x = value.x; z = value.y; } }
        public ivec2 yx { get { return new ivec2(y, x); } set { y = value.x; x = value.y; } }
        public ivec2 yy { get { return new ivec2(y, y); } set { y = value.x; y = value.y; } }
        public ivec2 yz { get { return new ivec2(y, z); } set { y = value.x; z = value.y; } }
        public ivec2 zx { get { return new ivec2(z, x); } set { z = value.x; x = value.y; } }
        public ivec2 zy { get { return new ivec2(z, y); } set { z = value.x; y = value.y; } }
        public ivec2 zz { get { return new ivec2(z, z); } set { z = value.x; z = value.y; } }
        public ivec3 xxx { get { return new ivec3(x, x, x); } set { x = value.x; x = value.y; x = value.z; } }
        public ivec3 xxy { get { return new ivec3(x, x, y); } set { x = value.x; x = value.y; y = value.z; } }
        public ivec3 xxz { get { return new ivec3(x, x, z); } set { x = value.x; x = value.y; z = value.z; } }
        public ivec3 xyx { get { return new ivec3(x, y, x); } set { x = value.x; y = value.y; x = value.z; } }
        public ivec3 xyy { get { return new ivec3(x, y, y); } set { x = value.x; y = value.y; y = value.z; } }
        public ivec3 xyz { get { return new ivec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public ivec3 xzx { get { return new ivec3(x, z, x); } set { x = value.x; z = value.y; x = value.z; } }
        public ivec3 xzy { get { return new ivec3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }
        public ivec3 xzz { get { return new ivec3(x, z, z); } set { x = value.x; z = value.y; z = value.z; } }
        public ivec3 yxx { get { return new ivec3(y, x, x); } set { y = value.x; x = value.y; x = value.z; } }
        public ivec3 yxy { get { return new ivec3(y, x, y); } set { y = value.x; x = value.y; y = value.z; } }
        public ivec3 yxz { get { return new ivec3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }
        public ivec3 yyx { get { return new ivec3(y, y, x); } set { y = value.x; y = value.y; x = value.z; } }
        public ivec3 yyy { get { return new ivec3(y, y, y); } set { y = value.x; y = value.y; y = value.z; } }
        public ivec3 yyz { get { return new ivec3(y, y, z); } set { y = value.x; y = value.y; z = value.z; } }
        public ivec3 yzx { get { return new ivec3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }
        public ivec3 yzy { get { return new ivec3(y, z, y); } set { y = value.x; z = value.y; y = value.z; } }
        public ivec3 yzz { get { return new ivec3(y, z, z); } set { y = value.x; z = value.y; z = value.z; } }
        public ivec3 zxx { get { return new ivec3(z, x, x); } set { z = value.x; x = value.y; x = value.z; } }
        public ivec3 zxy { get { return new ivec3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }
        public ivec3 zxz { get { return new ivec3(z, x, z); } set { z = value.x; x = value.y; z = value.z; } }
        public ivec3 zyx { get { return new ivec3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }
        public ivec3 zyy { get { return new ivec3(z, y, y); } set { z = value.x; y = value.y; y = value.z; } }
        public ivec3 zyz { get { return new ivec3(z, y, z); } set { z = value.x; y = value.y; z = value.z; } }
        public ivec3 zzx { get { return new ivec3(z, z, x); } set { z = value.x; z = value.y; x = value.z; } }
        public ivec3 zzy { get { return new ivec3(z, z, y); } set { z = value.x; z = value.y; y = value.z; } }
        public ivec3 zzz { get { return new ivec3(z, z, z); } set { z = value.x; z = value.y; z = value.z; } }
        public ivec4 xxxx { get { return new ivec4(x, x, x, x); } set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 xxxy { get { return new ivec4(x, x, x, y); } set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 xxxz { get { return new ivec4(x, x, x, z); } set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public ivec4 xxyx { get { return new ivec4(x, x, y, x); } set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 xxyy { get { return new ivec4(x, x, y, y); } set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 xxyz { get { return new ivec4(x, x, y, z); } set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public ivec4 xxzx { get { return new ivec4(x, x, z, x); } set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public ivec4 xxzy { get { return new ivec4(x, x, z, y); } set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public ivec4 xxzz { get { return new ivec4(x, x, z, z); } set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public ivec4 xyxx { get { return new ivec4(x, y, x, x); } set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 xyxy { get { return new ivec4(x, y, x, y); } set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 xyxz { get { return new ivec4(x, y, x, z); } set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public ivec4 xyyx { get { return new ivec4(x, y, y, x); } set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 xyyy { get { return new ivec4(x, y, y, y); } set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public ivec4 xyyz { get { return new ivec4(x, y, y, z); } set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public ivec4 xyzx { get { return new ivec4(x, y, z, x); } set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public ivec4 xyzy { get { return new ivec4(x, y, z, y); } set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public ivec4 xyzz { get { return new ivec4(x, y, z, z); } set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public ivec4 xzxx { get { return new ivec4(x, z, x, x); } set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public ivec4 xzxy { get { return new ivec4(x, z, x, y); } set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public ivec4 xzxz { get { return new ivec4(x, z, x, z); } set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public ivec4 xzyx { get { return new ivec4(x, z, y, x); } set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public ivec4 xzyy { get { return new ivec4(x, z, y, y); } set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public ivec4 xzyz { get { return new ivec4(x, z, y, z); } set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public ivec4 xzzx { get { return new ivec4(x, z, z, x); } set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public ivec4 xzzy { get { return new ivec4(x, z, z, y); } set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public ivec4 xzzz { get { return new ivec4(x, z, z, z); } set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public ivec4 yxxx { get { return new ivec4(y, x, x, x); } set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 yxxy { get { return new ivec4(y, x, x, y); } set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 yxxz { get { return new ivec4(y, x, x, z); } set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public ivec4 yxyx { get { return new ivec4(y, x, y, x); } set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 yxyy { get { return new ivec4(y, x, y, y); } set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 yxyz { get { return new ivec4(y, x, y, z); } set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public ivec4 yxzx { get { return new ivec4(y, x, z, x); } set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public ivec4 yxzy { get { return new ivec4(y, x, z, y); } set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public ivec4 yxzz { get { return new ivec4(y, x, z, z); } set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public ivec4 yyxx { get { return new ivec4(y, y, x, x); } set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 yyxy { get { return new ivec4(y, y, x, y); } set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 yyxz { get { return new ivec4(y, y, x, z); } set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public ivec4 yyyx { get { return new ivec4(y, y, y, x); } set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 yyyy { get { return new ivec4(y, y, y, y); } set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public ivec4 yyyz { get { return new ivec4(y, y, y, z); } set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public ivec4 yyzx { get { return new ivec4(y, y, z, x); } set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public ivec4 yyzy { get { return new ivec4(y, y, z, y); } set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public ivec4 yyzz { get { return new ivec4(y, y, z, z); } set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public ivec4 yzxx { get { return new ivec4(y, z, x, x); } set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public ivec4 yzxy { get { return new ivec4(y, z, x, y); } set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public ivec4 yzxz { get { return new ivec4(y, z, x, z); } set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public ivec4 yzyx { get { return new ivec4(y, z, y, x); } set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public ivec4 yzyy { get { return new ivec4(y, z, y, y); } set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public ivec4 yzyz { get { return new ivec4(y, z, y, z); } set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public ivec4 yzzx { get { return new ivec4(y, z, z, x); } set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public ivec4 yzzy { get { return new ivec4(y, z, z, y); } set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public ivec4 yzzz { get { return new ivec4(y, z, z, z); } set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public ivec4 zxxx { get { return new ivec4(z, x, x, x); } set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 zxxy { get { return new ivec4(z, x, x, y); } set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 zxxz { get { return new ivec4(z, x, x, z); } set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public ivec4 zxyx { get { return new ivec4(z, x, y, x); } set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 zxyy { get { return new ivec4(z, x, y, y); } set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 zxyz { get { return new ivec4(z, x, y, z); } set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public ivec4 zxzx { get { return new ivec4(z, x, z, x); } set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public ivec4 zxzy { get { return new ivec4(z, x, z, y); } set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public ivec4 zxzz { get { return new ivec4(z, x, z, z); } set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public ivec4 zyxx { get { return new ivec4(z, y, x, x); } set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 zyxy { get { return new ivec4(z, y, x, y); } set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 zyxz { get { return new ivec4(z, y, x, z); } set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public ivec4 zyyx { get { return new ivec4(z, y, y, x); } set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 zyyy { get { return new ivec4(z, y, y, y); } set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public ivec4 zyyz { get { return new ivec4(z, y, y, z); } set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public ivec4 zyzx { get { return new ivec4(z, y, z, x); } set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public ivec4 zyzy { get { return new ivec4(z, y, z, y); } set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public ivec4 zyzz { get { return new ivec4(z, y, z, z); } set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public ivec4 zzxx { get { return new ivec4(z, z, x, x); } set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public ivec4 zzxy { get { return new ivec4(z, z, x, y); } set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public ivec4 zzxz { get { return new ivec4(z, z, x, z); } set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public ivec4 zzyx { get { return new ivec4(z, z, y, x); } set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public ivec4 zzyy { get { return new ivec4(z, z, y, y); } set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public ivec4 zzyz { get { return new ivec4(z, z, y, z); } set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public ivec4 zzzx { get { return new ivec4(z, z, z, x); } set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public ivec4 zzzy { get { return new ivec4(z, z, z, y); } set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public ivec4 zzzz { get { return new ivec4(z, z, z, z); } set { z = value.x; z = value.y; z = value.z; z = value.w; } }

        #endregion
    }

    public struct uvec3
    {
        public uint x, y, z;
        public uint this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString() => "(" + x + ", " + y + ", " + z + ")";
        public Array ToArray() => new[] { x, y, z };

        #region vec3

        public uvec3(uint a) : this(a, a, a) { }
        public uvec3(uint[] v) : this(v[0], v[1], v[2]) { }
        public uvec3(uint x, uint y, uint z) { this.x = x; this.y = y; this.z = z; }
        public uvec3(uvec2 xy, uint z) : this(xy.x, xy.y, z) { }
        public uvec3(uint x, uvec2 yz) : this(x, yz.x, yz.y) { }
        public uvec3(byte[] data) : this((uint[])data.To(typeof(uint))) { }

        #endregion

        #region Generated
        
        public uvec2 xx { get { return new uvec2(x, x); } set { x = value.x; x = value.y; } }
        public uvec2 xy { get { return new uvec2(x, y); } set { x = value.x; y = value.y; } }
        public uvec2 xz { get { return new uvec2(x, z); } set { x = value.x; z = value.y; } }
        public uvec2 yx { get { return new uvec2(y, x); } set { y = value.x; x = value.y; } }
        public uvec2 yy { get { return new uvec2(y, y); } set { y = value.x; y = value.y; } }
        public uvec2 yz { get { return new uvec2(y, z); } set { y = value.x; z = value.y; } }
        public uvec2 zx { get { return new uvec2(z, x); } set { z = value.x; x = value.y; } }
        public uvec2 zy { get { return new uvec2(z, y); } set { z = value.x; y = value.y; } }
        public uvec2 zz { get { return new uvec2(z, z); } set { z = value.x; z = value.y; } }
        public uvec3 xxx { get { return new uvec3(x, x, x); } set { x = value.x; x = value.y; x = value.z; } }
        public uvec3 xxy { get { return new uvec3(x, x, y); } set { x = value.x; x = value.y; y = value.z; } }
        public uvec3 xxz { get { return new uvec3(x, x, z); } set { x = value.x; x = value.y; z = value.z; } }
        public uvec3 xyx { get { return new uvec3(x, y, x); } set { x = value.x; y = value.y; x = value.z; } }
        public uvec3 xyy { get { return new uvec3(x, y, y); } set { x = value.x; y = value.y; y = value.z; } }
        public uvec3 xyz { get { return new uvec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public uvec3 xzx { get { return new uvec3(x, z, x); } set { x = value.x; z = value.y; x = value.z; } }
        public uvec3 xzy { get { return new uvec3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }
        public uvec3 xzz { get { return new uvec3(x, z, z); } set { x = value.x; z = value.y; z = value.z; } }
        public uvec3 yxx { get { return new uvec3(y, x, x); } set { y = value.x; x = value.y; x = value.z; } }
        public uvec3 yxy { get { return new uvec3(y, x, y); } set { y = value.x; x = value.y; y = value.z; } }
        public uvec3 yxz { get { return new uvec3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }
        public uvec3 yyx { get { return new uvec3(y, y, x); } set { y = value.x; y = value.y; x = value.z; } }
        public uvec3 yyy { get { return new uvec3(y, y, y); } set { y = value.x; y = value.y; y = value.z; } }
        public uvec3 yyz { get { return new uvec3(y, y, z); } set { y = value.x; y = value.y; z = value.z; } }
        public uvec3 yzx { get { return new uvec3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }
        public uvec3 yzy { get { return new uvec3(y, z, y); } set { y = value.x; z = value.y; y = value.z; } }
        public uvec3 yzz { get { return new uvec3(y, z, z); } set { y = value.x; z = value.y; z = value.z; } }
        public uvec3 zxx { get { return new uvec3(z, x, x); } set { z = value.x; x = value.y; x = value.z; } }
        public uvec3 zxy { get { return new uvec3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }
        public uvec3 zxz { get { return new uvec3(z, x, z); } set { z = value.x; x = value.y; z = value.z; } }
        public uvec3 zyx { get { return new uvec3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }
        public uvec3 zyy { get { return new uvec3(z, y, y); } set { z = value.x; y = value.y; y = value.z; } }
        public uvec3 zyz { get { return new uvec3(z, y, z); } set { z = value.x; y = value.y; z = value.z; } }
        public uvec3 zzx { get { return new uvec3(z, z, x); } set { z = value.x; z = value.y; x = value.z; } }
        public uvec3 zzy { get { return new uvec3(z, z, y); } set { z = value.x; z = value.y; y = value.z; } }
        public uvec3 zzz { get { return new uvec3(z, z, z); } set { z = value.x; z = value.y; z = value.z; } }
        public uvec4 xxxx { get { return new uvec4(x, x, x, x); } set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 xxxy { get { return new uvec4(x, x, x, y); } set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 xxxz { get { return new uvec4(x, x, x, z); } set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public uvec4 xxyx { get { return new uvec4(x, x, y, x); } set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 xxyy { get { return new uvec4(x, x, y, y); } set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 xxyz { get { return new uvec4(x, x, y, z); } set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public uvec4 xxzx { get { return new uvec4(x, x, z, x); } set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public uvec4 xxzy { get { return new uvec4(x, x, z, y); } set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public uvec4 xxzz { get { return new uvec4(x, x, z, z); } set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public uvec4 xyxx { get { return new uvec4(x, y, x, x); } set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 xyxy { get { return new uvec4(x, y, x, y); } set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 xyxz { get { return new uvec4(x, y, x, z); } set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public uvec4 xyyx { get { return new uvec4(x, y, y, x); } set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 xyyy { get { return new uvec4(x, y, y, y); } set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public uvec4 xyyz { get { return new uvec4(x, y, y, z); } set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public uvec4 xyzx { get { return new uvec4(x, y, z, x); } set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public uvec4 xyzy { get { return new uvec4(x, y, z, y); } set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public uvec4 xyzz { get { return new uvec4(x, y, z, z); } set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public uvec4 xzxx { get { return new uvec4(x, z, x, x); } set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public uvec4 xzxy { get { return new uvec4(x, z, x, y); } set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public uvec4 xzxz { get { return new uvec4(x, z, x, z); } set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public uvec4 xzyx { get { return new uvec4(x, z, y, x); } set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public uvec4 xzyy { get { return new uvec4(x, z, y, y); } set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public uvec4 xzyz { get { return new uvec4(x, z, y, z); } set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public uvec4 xzzx { get { return new uvec4(x, z, z, x); } set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public uvec4 xzzy { get { return new uvec4(x, z, z, y); } set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public uvec4 xzzz { get { return new uvec4(x, z, z, z); } set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public uvec4 yxxx { get { return new uvec4(y, x, x, x); } set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 yxxy { get { return new uvec4(y, x, x, y); } set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 yxxz { get { return new uvec4(y, x, x, z); } set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public uvec4 yxyx { get { return new uvec4(y, x, y, x); } set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 yxyy { get { return new uvec4(y, x, y, y); } set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 yxyz { get { return new uvec4(y, x, y, z); } set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public uvec4 yxzx { get { return new uvec4(y, x, z, x); } set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public uvec4 yxzy { get { return new uvec4(y, x, z, y); } set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public uvec4 yxzz { get { return new uvec4(y, x, z, z); } set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public uvec4 yyxx { get { return new uvec4(y, y, x, x); } set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 yyxy { get { return new uvec4(y, y, x, y); } set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 yyxz { get { return new uvec4(y, y, x, z); } set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public uvec4 yyyx { get { return new uvec4(y, y, y, x); } set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 yyyy { get { return new uvec4(y, y, y, y); } set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public uvec4 yyyz { get { return new uvec4(y, y, y, z); } set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public uvec4 yyzx { get { return new uvec4(y, y, z, x); } set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public uvec4 yyzy { get { return new uvec4(y, y, z, y); } set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public uvec4 yyzz { get { return new uvec4(y, y, z, z); } set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public uvec4 yzxx { get { return new uvec4(y, z, x, x); } set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public uvec4 yzxy { get { return new uvec4(y, z, x, y); } set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public uvec4 yzxz { get { return new uvec4(y, z, x, z); } set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public uvec4 yzyx { get { return new uvec4(y, z, y, x); } set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public uvec4 yzyy { get { return new uvec4(y, z, y, y); } set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public uvec4 yzyz { get { return new uvec4(y, z, y, z); } set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public uvec4 yzzx { get { return new uvec4(y, z, z, x); } set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public uvec4 yzzy { get { return new uvec4(y, z, z, y); } set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public uvec4 yzzz { get { return new uvec4(y, z, z, z); } set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public uvec4 zxxx { get { return new uvec4(z, x, x, x); } set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 zxxy { get { return new uvec4(z, x, x, y); } set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 zxxz { get { return new uvec4(z, x, x, z); } set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public uvec4 zxyx { get { return new uvec4(z, x, y, x); } set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 zxyy { get { return new uvec4(z, x, y, y); } set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 zxyz { get { return new uvec4(z, x, y, z); } set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public uvec4 zxzx { get { return new uvec4(z, x, z, x); } set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public uvec4 zxzy { get { return new uvec4(z, x, z, y); } set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public uvec4 zxzz { get { return new uvec4(z, x, z, z); } set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public uvec4 zyxx { get { return new uvec4(z, y, x, x); } set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 zyxy { get { return new uvec4(z, y, x, y); } set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 zyxz { get { return new uvec4(z, y, x, z); } set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public uvec4 zyyx { get { return new uvec4(z, y, y, x); } set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 zyyy { get { return new uvec4(z, y, y, y); } set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public uvec4 zyyz { get { return new uvec4(z, y, y, z); } set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public uvec4 zyzx { get { return new uvec4(z, y, z, x); } set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public uvec4 zyzy { get { return new uvec4(z, y, z, y); } set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public uvec4 zyzz { get { return new uvec4(z, y, z, z); } set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public uvec4 zzxx { get { return new uvec4(z, z, x, x); } set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public uvec4 zzxy { get { return new uvec4(z, z, x, y); } set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public uvec4 zzxz { get { return new uvec4(z, z, x, z); } set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public uvec4 zzyx { get { return new uvec4(z, z, y, x); } set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public uvec4 zzyy { get { return new uvec4(z, z, y, y); } set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public uvec4 zzyz { get { return new uvec4(z, z, y, z); } set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public uvec4 zzzx { get { return new uvec4(z, z, z, x); } set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public uvec4 zzzy { get { return new uvec4(z, z, z, y); } set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public uvec4 zzzz { get { return new uvec4(z, z, z, z); } set { z = value.x; z = value.y; z = value.z; z = value.w; } }

        #endregion
    }
}
