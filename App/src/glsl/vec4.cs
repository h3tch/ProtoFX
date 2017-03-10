using System;

#pragma warning disable IDE1006

namespace App.Glsl
{
    public struct vec4
    {
        public float x, y, z, w;
        public float this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
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
                    case 3: w = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ", " + w + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y, z, w };
        }

        #region vec4

        public vec4(float a) : this(a, a, a, a) { }
        public vec4(float[] v) : this(v[0], v[1], v[2], v[3]) { }
        public vec4(float X, float Y, float Z, float W) { x = X; y = Y; z = Z; w = W; }
        public vec4(vec2 xy, float z, float w) : this(xy.x, xy.y, z, w) { }
        public vec4(float x, vec2 yz, float w) : this(x, yz.x, yz.y, w) { }
        public vec4(float x, float y, vec2 zw) : this(x, y, zw.x, zw.y) { }
        public vec4(vec2 xy, vec2 zw) : this(xy.x, xy.y, zw.x, zw.y) { }
        public vec4(vec3 xyz, float w) : this(xyz.x, xyz.y, xyz.z, w) { }
        public vec4(float x, vec3 yzw) : this(x, yzw.x, yzw.y, yzw.z) { }
        public vec4(byte[] data) : this((float[])data.To(typeof(float))) { }

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
        
        public vec2 xx { get => new vec2(x, x); set { x = value.x; x = value.y; } }
        public vec2 xy { get => new vec2(x, y); set { x = value.x; y = value.y; } }
        public vec2 xz { get => new vec2(x, z); set { x = value.x; z = value.y; } }
        public vec2 xw { get => new vec2(x, w); set { x = value.x; w = value.y; } }
        public vec2 yx { get => new vec2(y, x); set { y = value.x; x = value.y; } }
        public vec2 yy { get => new vec2(y, y); set { y = value.x; y = value.y; } }
        public vec2 yz { get => new vec2(y, z); set { y = value.x; z = value.y; } }
        public vec2 yw { get => new vec2(y, w); set { y = value.x; w = value.y; } }
        public vec2 zx { get => new vec2(z, x); set { z = value.x; x = value.y; } }
        public vec2 zy { get => new vec2(z, y); set { z = value.x; y = value.y; } }
        public vec2 zz { get => new vec2(z, z); set { z = value.x; z = value.y; } }
        public vec2 zw { get => new vec2(z, w); set { z = value.x; w = value.y; } }
        public vec2 wx { get => new vec2(w, x); set { w = value.x; x = value.y; } }
        public vec2 wy { get => new vec2(w, y); set { w = value.x; y = value.y; } }
        public vec2 wz { get => new vec2(w, z); set { w = value.x; z = value.y; } }
        public vec2 ww { get => new vec2(w, w); set { w = value.x; w = value.y; } }
        public vec3 xxx { get => new vec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public vec3 xxy { get => new vec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public vec3 xxz { get => new vec3(x, x, z); set { x = value.x; x = value.y; z = value.z; } }
        public vec3 xxw { get => new vec3(x, x, w); set { x = value.x; x = value.y; w = value.z; } }
        public vec3 xyx { get => new vec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public vec3 xyy { get => new vec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public vec3 xyz { get => new vec3(x, y, z); set { x = value.x; y = value.y; z = value.z; } }
        public vec3 xyw { get => new vec3(x, y, w); set { x = value.x; y = value.y; w = value.z; } }
        public vec3 xzx { get => new vec3(x, z, x); set { x = value.x; z = value.y; x = value.z; } }
        public vec3 xzy { get => new vec3(x, z, y); set { x = value.x; z = value.y; y = value.z; } }
        public vec3 xzz { get => new vec3(x, z, z); set { x = value.x; z = value.y; z = value.z; } }
        public vec3 xzw { get => new vec3(x, z, w); set { x = value.x; z = value.y; w = value.z; } }
        public vec3 xwx { get => new vec3(x, w, x); set { x = value.x; w = value.y; x = value.z; } }
        public vec3 xwy { get => new vec3(x, w, y); set { x = value.x; w = value.y; y = value.z; } }
        public vec3 xwz { get => new vec3(x, w, z); set { x = value.x; w = value.y; z = value.z; } }
        public vec3 xww { get => new vec3(x, w, w); set { x = value.x; w = value.y; w = value.z; } }
        public vec3 yxx { get => new vec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public vec3 yxy { get => new vec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public vec3 yxz { get => new vec3(y, x, z); set { y = value.x; x = value.y; z = value.z; } }
        public vec3 yxw { get => new vec3(y, x, w); set { y = value.x; x = value.y; w = value.z; } }
        public vec3 yyx { get => new vec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public vec3 yyy { get => new vec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public vec3 yyz { get => new vec3(y, y, z); set { y = value.x; y = value.y; z = value.z; } }
        public vec3 yyw { get => new vec3(y, y, w); set { y = value.x; y = value.y; w = value.z; } }
        public vec3 yzx { get => new vec3(y, z, x); set { y = value.x; z = value.y; x = value.z; } }
        public vec3 yzy { get => new vec3(y, z, y); set { y = value.x; z = value.y; y = value.z; } }
        public vec3 yzz { get => new vec3(y, z, z); set { y = value.x; z = value.y; z = value.z; } }
        public vec3 yzw { get => new vec3(y, z, w); set { y = value.x; z = value.y; w = value.z; } }
        public vec3 ywx { get => new vec3(y, w, x); set { y = value.x; w = value.y; x = value.z; } }
        public vec3 ywy { get => new vec3(y, w, y); set { y = value.x; w = value.y; y = value.z; } }
        public vec3 ywz { get => new vec3(y, w, z); set { y = value.x; w = value.y; z = value.z; } }
        public vec3 yww { get => new vec3(y, w, w); set { y = value.x; w = value.y; w = value.z; } }
        public vec3 zxx { get => new vec3(z, x, x); set { z = value.x; x = value.y; x = value.z; } }
        public vec3 zxy { get => new vec3(z, x, y); set { z = value.x; x = value.y; y = value.z; } }
        public vec3 zxz { get => new vec3(z, x, z); set { z = value.x; x = value.y; z = value.z; } }
        public vec3 zxw { get => new vec3(z, x, w); set { z = value.x; x = value.y; w = value.z; } }
        public vec3 zyx { get => new vec3(z, y, x); set { z = value.x; y = value.y; x = value.z; } }
        public vec3 zyy { get => new vec3(z, y, y); set { z = value.x; y = value.y; y = value.z; } }
        public vec3 zyz { get => new vec3(z, y, z); set { z = value.x; y = value.y; z = value.z; } }
        public vec3 zyw { get => new vec3(z, y, w); set { z = value.x; y = value.y; w = value.z; } }
        public vec3 zzx { get => new vec3(z, z, x); set { z = value.x; z = value.y; x = value.z; } }
        public vec3 zzy { get => new vec3(z, z, y); set { z = value.x; z = value.y; y = value.z; } }
        public vec3 zzz { get => new vec3(z, z, z); set { z = value.x; z = value.y; z = value.z; } }
        public vec3 zzw { get => new vec3(z, z, w); set { z = value.x; z = value.y; w = value.z; } }
        public vec3 zwx { get => new vec3(z, w, x); set { z = value.x; w = value.y; x = value.z; } }
        public vec3 zwy { get => new vec3(z, w, y); set { z = value.x; w = value.y; y = value.z; } }
        public vec3 zwz { get => new vec3(z, w, z); set { z = value.x; w = value.y; z = value.z; } }
        public vec3 zww { get => new vec3(z, w, w); set { z = value.x; w = value.y; w = value.z; } }
        public vec3 wxx { get => new vec3(w, x, x); set { w = value.x; x = value.y; x = value.z; } }
        public vec3 wxy { get => new vec3(w, x, y); set { w = value.x; x = value.y; y = value.z; } }
        public vec3 wxz { get => new vec3(w, x, z); set { w = value.x; x = value.y; z = value.z; } }
        public vec3 wxw { get => new vec3(w, x, w); set { w = value.x; x = value.y; w = value.z; } }
        public vec3 wyx { get => new vec3(w, y, x); set { w = value.x; y = value.y; x = value.z; } }
        public vec3 wyy { get => new vec3(w, y, y); set { w = value.x; y = value.y; y = value.z; } }
        public vec3 wyz { get => new vec3(w, y, z); set { w = value.x; y = value.y; z = value.z; } }
        public vec3 wyw { get => new vec3(w, y, w); set { w = value.x; y = value.y; w = value.z; } }
        public vec3 wzx { get => new vec3(w, z, x); set { w = value.x; z = value.y; x = value.z; } }
        public vec3 wzy { get => new vec3(w, z, y); set { w = value.x; z = value.y; y = value.z; } }
        public vec3 wzz { get => new vec3(w, z, z); set { w = value.x; z = value.y; z = value.z; } }
        public vec3 wzw { get => new vec3(w, z, w); set { w = value.x; z = value.y; w = value.z; } }
        public vec3 wwx { get => new vec3(w, w, x); set { w = value.x; w = value.y; x = value.z; } }
        public vec3 wwy { get => new vec3(w, w, y); set { w = value.x; w = value.y; y = value.z; } }
        public vec3 wwz { get => new vec3(w, w, z); set { w = value.x; w = value.y; z = value.z; } }
        public vec3 www { get => new vec3(w, w, w); set { w = value.x; w = value.y; w = value.z; } }
        public vec4 xxxx { get => new vec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 xxxy { get => new vec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 xxxz { get => new vec4(x, x, x, z); set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public vec4 xxxw { get => new vec4(x, x, x, w); set { x = value.x; x = value.y; x = value.z; w = value.w; } }
        public vec4 xxyx { get => new vec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 xxyy { get => new vec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 xxyz { get => new vec4(x, x, y, z); set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public vec4 xxyw { get => new vec4(x, x, y, w); set { x = value.x; x = value.y; y = value.z; w = value.w; } }
        public vec4 xxzx { get => new vec4(x, x, z, x); set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public vec4 xxzy { get => new vec4(x, x, z, y); set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public vec4 xxzz { get => new vec4(x, x, z, z); set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public vec4 xxzw { get => new vec4(x, x, z, w); set { x = value.x; x = value.y; z = value.z; w = value.w; } }
        public vec4 xxwx { get => new vec4(x, x, w, x); set { x = value.x; x = value.y; w = value.z; x = value.w; } }
        public vec4 xxwy { get => new vec4(x, x, w, y); set { x = value.x; x = value.y; w = value.z; y = value.w; } }
        public vec4 xxwz { get => new vec4(x, x, w, z); set { x = value.x; x = value.y; w = value.z; z = value.w; } }
        public vec4 xxww { get => new vec4(x, x, w, w); set { x = value.x; x = value.y; w = value.z; w = value.w; } }
        public vec4 xyxx { get => new vec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 xyxy { get => new vec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 xyxz { get => new vec4(x, y, x, z); set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public vec4 xyxw { get => new vec4(x, y, x, w); set { x = value.x; y = value.y; x = value.z; w = value.w; } }
        public vec4 xyyx { get => new vec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 xyyy { get => new vec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public vec4 xyyz { get => new vec4(x, y, y, z); set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public vec4 xyyw { get => new vec4(x, y, y, w); set { x = value.x; y = value.y; y = value.z; w = value.w; } }
        public vec4 xyzx { get => new vec4(x, y, z, x); set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public vec4 xyzy { get => new vec4(x, y, z, y); set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public vec4 xyzz { get => new vec4(x, y, z, z); set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public vec4 xyzw { get => new vec4(x, y, z, w); set { x = value.x; y = value.y; z = value.z; w = value.w; } }
        public vec4 xywx { get => new vec4(x, y, w, x); set { x = value.x; y = value.y; w = value.z; x = value.w; } }
        public vec4 xywy { get => new vec4(x, y, w, y); set { x = value.x; y = value.y; w = value.z; y = value.w; } }
        public vec4 xywz { get => new vec4(x, y, w, z); set { x = value.x; y = value.y; w = value.z; z = value.w; } }
        public vec4 xyww { get => new vec4(x, y, w, w); set { x = value.x; y = value.y; w = value.z; w = value.w; } }
        public vec4 xzxx { get => new vec4(x, z, x, x); set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public vec4 xzxy { get => new vec4(x, z, x, y); set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public vec4 xzxz { get => new vec4(x, z, x, z); set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public vec4 xzxw { get => new vec4(x, z, x, w); set { x = value.x; z = value.y; x = value.z; w = value.w; } }
        public vec4 xzyx { get => new vec4(x, z, y, x); set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public vec4 xzyy { get => new vec4(x, z, y, y); set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public vec4 xzyz { get => new vec4(x, z, y, z); set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public vec4 xzyw { get => new vec4(x, z, y, w); set { x = value.x; z = value.y; y = value.z; w = value.w; } }
        public vec4 xzzx { get => new vec4(x, z, z, x); set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public vec4 xzzy { get => new vec4(x, z, z, y); set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public vec4 xzzz { get => new vec4(x, z, z, z); set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public vec4 xzzw { get => new vec4(x, z, z, w); set { x = value.x; z = value.y; z = value.z; w = value.w; } }
        public vec4 xzwx { get => new vec4(x, z, w, x); set { x = value.x; z = value.y; w = value.z; x = value.w; } }
        public vec4 xzwy { get => new vec4(x, z, w, y); set { x = value.x; z = value.y; w = value.z; y = value.w; } }
        public vec4 xzwz { get => new vec4(x, z, w, z); set { x = value.x; z = value.y; w = value.z; z = value.w; } }
        public vec4 xzww { get => new vec4(x, z, w, w); set { x = value.x; z = value.y; w = value.z; w = value.w; } }
        public vec4 xwxx { get => new vec4(x, w, x, x); set { x = value.x; w = value.y; x = value.z; x = value.w; } }
        public vec4 xwxy { get => new vec4(x, w, x, y); set { x = value.x; w = value.y; x = value.z; y = value.w; } }
        public vec4 xwxz { get => new vec4(x, w, x, z); set { x = value.x; w = value.y; x = value.z; z = value.w; } }
        public vec4 xwxw { get => new vec4(x, w, x, w); set { x = value.x; w = value.y; x = value.z; w = value.w; } }
        public vec4 xwyx { get => new vec4(x, w, y, x); set { x = value.x; w = value.y; y = value.z; x = value.w; } }
        public vec4 xwyy { get => new vec4(x, w, y, y); set { x = value.x; w = value.y; y = value.z; y = value.w; } }
        public vec4 xwyz { get => new vec4(x, w, y, z); set { x = value.x; w = value.y; y = value.z; z = value.w; } }
        public vec4 xwyw { get => new vec4(x, w, y, w); set { x = value.x; w = value.y; y = value.z; w = value.w; } }
        public vec4 xwzx { get => new vec4(x, w, z, x); set { x = value.x; w = value.y; z = value.z; x = value.w; } }
        public vec4 xwzy { get => new vec4(x, w, z, y); set { x = value.x; w = value.y; z = value.z; y = value.w; } }
        public vec4 xwzz { get => new vec4(x, w, z, z); set { x = value.x; w = value.y; z = value.z; z = value.w; } }
        public vec4 xwzw { get => new vec4(x, w, z, w); set { x = value.x; w = value.y; z = value.z; w = value.w; } }
        public vec4 xwwx { get => new vec4(x, w, w, x); set { x = value.x; w = value.y; w = value.z; x = value.w; } }
        public vec4 xwwy { get => new vec4(x, w, w, y); set { x = value.x; w = value.y; w = value.z; y = value.w; } }
        public vec4 xwwz { get => new vec4(x, w, w, z); set { x = value.x; w = value.y; w = value.z; z = value.w; } }
        public vec4 xwww { get => new vec4(x, w, w, w); set { x = value.x; w = value.y; w = value.z; w = value.w; } }
        public vec4 yxxx { get => new vec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 yxxy { get => new vec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 yxxz { get => new vec4(y, x, x, z); set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public vec4 yxxw { get => new vec4(y, x, x, w); set { y = value.x; x = value.y; x = value.z; w = value.w; } }
        public vec4 yxyx { get => new vec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 yxyy { get => new vec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 yxyz { get => new vec4(y, x, y, z); set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public vec4 yxyw { get => new vec4(y, x, y, w); set { y = value.x; x = value.y; y = value.z; w = value.w; } }
        public vec4 yxzx { get => new vec4(y, x, z, x); set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public vec4 yxzy { get => new vec4(y, x, z, y); set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public vec4 yxzz { get => new vec4(y, x, z, z); set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public vec4 yxzw { get => new vec4(y, x, z, w); set { y = value.x; x = value.y; z = value.z; w = value.w; } }
        public vec4 yxwx { get => new vec4(y, x, w, x); set { y = value.x; x = value.y; w = value.z; x = value.w; } }
        public vec4 yxwy { get => new vec4(y, x, w, y); set { y = value.x; x = value.y; w = value.z; y = value.w; } }
        public vec4 yxwz { get => new vec4(y, x, w, z); set { y = value.x; x = value.y; w = value.z; z = value.w; } }
        public vec4 yxww { get => new vec4(y, x, w, w); set { y = value.x; x = value.y; w = value.z; w = value.w; } }
        public vec4 yyxx { get => new vec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 yyxy { get => new vec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 yyxz { get => new vec4(y, y, x, z); set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public vec4 yyxw { get => new vec4(y, y, x, w); set { y = value.x; y = value.y; x = value.z; w = value.w; } }
        public vec4 yyyx { get => new vec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 yyyy { get => new vec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public vec4 yyyz { get => new vec4(y, y, y, z); set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public vec4 yyyw { get => new vec4(y, y, y, w); set { y = value.x; y = value.y; y = value.z; w = value.w; } }
        public vec4 yyzx { get => new vec4(y, y, z, x); set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public vec4 yyzy { get => new vec4(y, y, z, y); set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public vec4 yyzz { get => new vec4(y, y, z, z); set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public vec4 yyzw { get => new vec4(y, y, z, w); set { y = value.x; y = value.y; z = value.z; w = value.w; } }
        public vec4 yywx { get => new vec4(y, y, w, x); set { y = value.x; y = value.y; w = value.z; x = value.w; } }
        public vec4 yywy { get => new vec4(y, y, w, y); set { y = value.x; y = value.y; w = value.z; y = value.w; } }
        public vec4 yywz { get => new vec4(y, y, w, z); set { y = value.x; y = value.y; w = value.z; z = value.w; } }
        public vec4 yyww { get => new vec4(y, y, w, w); set { y = value.x; y = value.y; w = value.z; w = value.w; } }
        public vec4 yzxx { get => new vec4(y, z, x, x); set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public vec4 yzxy { get => new vec4(y, z, x, y); set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public vec4 yzxz { get => new vec4(y, z, x, z); set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public vec4 yzxw { get => new vec4(y, z, x, w); set { y = value.x; z = value.y; x = value.z; w = value.w; } }
        public vec4 yzyx { get => new vec4(y, z, y, x); set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public vec4 yzyy { get => new vec4(y, z, y, y); set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public vec4 yzyz { get => new vec4(y, z, y, z); set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public vec4 yzyw { get => new vec4(y, z, y, w); set { y = value.x; z = value.y; y = value.z; w = value.w; } }
        public vec4 yzzx { get => new vec4(y, z, z, x); set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public vec4 yzzy { get => new vec4(y, z, z, y); set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public vec4 yzzz { get => new vec4(y, z, z, z); set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public vec4 yzzw { get => new vec4(y, z, z, w); set { y = value.x; z = value.y; z = value.z; w = value.w; } }
        public vec4 yzwx { get => new vec4(y, z, w, x); set { y = value.x; z = value.y; w = value.z; x = value.w; } }
        public vec4 yzwy { get => new vec4(y, z, w, y); set { y = value.x; z = value.y; w = value.z; y = value.w; } }
        public vec4 yzwz { get => new vec4(y, z, w, z); set { y = value.x; z = value.y; w = value.z; z = value.w; } }
        public vec4 yzww { get => new vec4(y, z, w, w); set { y = value.x; z = value.y; w = value.z; w = value.w; } }
        public vec4 ywxx { get => new vec4(y, w, x, x); set { y = value.x; w = value.y; x = value.z; x = value.w; } }
        public vec4 ywxy { get => new vec4(y, w, x, y); set { y = value.x; w = value.y; x = value.z; y = value.w; } }
        public vec4 ywxz { get => new vec4(y, w, x, z); set { y = value.x; w = value.y; x = value.z; z = value.w; } }
        public vec4 ywxw { get => new vec4(y, w, x, w); set { y = value.x; w = value.y; x = value.z; w = value.w; } }
        public vec4 ywyx { get => new vec4(y, w, y, x); set { y = value.x; w = value.y; y = value.z; x = value.w; } }
        public vec4 ywyy { get => new vec4(y, w, y, y); set { y = value.x; w = value.y; y = value.z; y = value.w; } }
        public vec4 ywyz { get => new vec4(y, w, y, z); set { y = value.x; w = value.y; y = value.z; z = value.w; } }
        public vec4 ywyw { get => new vec4(y, w, y, w); set { y = value.x; w = value.y; y = value.z; w = value.w; } }
        public vec4 ywzx { get => new vec4(y, w, z, x); set { y = value.x; w = value.y; z = value.z; x = value.w; } }
        public vec4 ywzy { get => new vec4(y, w, z, y); set { y = value.x; w = value.y; z = value.z; y = value.w; } }
        public vec4 ywzz { get => new vec4(y, w, z, z); set { y = value.x; w = value.y; z = value.z; z = value.w; } }
        public vec4 ywzw { get => new vec4(y, w, z, w); set { y = value.x; w = value.y; z = value.z; w = value.w; } }
        public vec4 ywwx { get => new vec4(y, w, w, x); set { y = value.x; w = value.y; w = value.z; x = value.w; } }
        public vec4 ywwy { get => new vec4(y, w, w, y); set { y = value.x; w = value.y; w = value.z; y = value.w; } }
        public vec4 ywwz { get => new vec4(y, w, w, z); set { y = value.x; w = value.y; w = value.z; z = value.w; } }
        public vec4 ywww { get => new vec4(y, w, w, w); set { y = value.x; w = value.y; w = value.z; w = value.w; } }
        public vec4 zxxx { get => new vec4(z, x, x, x); set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 zxxy { get => new vec4(z, x, x, y); set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 zxxz { get => new vec4(z, x, x, z); set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public vec4 zxxw { get => new vec4(z, x, x, w); set { z = value.x; x = value.y; x = value.z; w = value.w; } }
        public vec4 zxyx { get => new vec4(z, x, y, x); set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 zxyy { get => new vec4(z, x, y, y); set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 zxyz { get => new vec4(z, x, y, z); set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public vec4 zxyw { get => new vec4(z, x, y, w); set { z = value.x; x = value.y; y = value.z; w = value.w; } }
        public vec4 zxzx { get => new vec4(z, x, z, x); set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public vec4 zxzy { get => new vec4(z, x, z, y); set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public vec4 zxzz { get => new vec4(z, x, z, z); set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public vec4 zxzw { get => new vec4(z, x, z, w); set { z = value.x; x = value.y; z = value.z; w = value.w; } }
        public vec4 zxwx { get => new vec4(z, x, w, x); set { z = value.x; x = value.y; w = value.z; x = value.w; } }
        public vec4 zxwy { get => new vec4(z, x, w, y); set { z = value.x; x = value.y; w = value.z; y = value.w; } }
        public vec4 zxwz { get => new vec4(z, x, w, z); set { z = value.x; x = value.y; w = value.z; z = value.w; } }
        public vec4 zxww { get => new vec4(z, x, w, w); set { z = value.x; x = value.y; w = value.z; w = value.w; } }
        public vec4 zyxx { get => new vec4(z, y, x, x); set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 zyxy { get => new vec4(z, y, x, y); set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 zyxz { get => new vec4(z, y, x, z); set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public vec4 zyxw { get => new vec4(z, y, x, w); set { z = value.x; y = value.y; x = value.z; w = value.w; } }
        public vec4 zyyx { get => new vec4(z, y, y, x); set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 zyyy { get => new vec4(z, y, y, y); set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public vec4 zyyz { get => new vec4(z, y, y, z); set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public vec4 zyyw { get => new vec4(z, y, y, w); set { z = value.x; y = value.y; y = value.z; w = value.w; } }
        public vec4 zyzx { get => new vec4(z, y, z, x); set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public vec4 zyzy { get => new vec4(z, y, z, y); set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public vec4 zyzz { get => new vec4(z, y, z, z); set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public vec4 zyzw { get => new vec4(z, y, z, w); set { z = value.x; y = value.y; z = value.z; w = value.w; } }
        public vec4 zywx { get => new vec4(z, y, w, x); set { z = value.x; y = value.y; w = value.z; x = value.w; } }
        public vec4 zywy { get => new vec4(z, y, w, y); set { z = value.x; y = value.y; w = value.z; y = value.w; } }
        public vec4 zywz { get => new vec4(z, y, w, z); set { z = value.x; y = value.y; w = value.z; z = value.w; } }
        public vec4 zyww { get => new vec4(z, y, w, w); set { z = value.x; y = value.y; w = value.z; w = value.w; } }
        public vec4 zzxx { get => new vec4(z, z, x, x); set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public vec4 zzxy { get => new vec4(z, z, x, y); set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public vec4 zzxz { get => new vec4(z, z, x, z); set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public vec4 zzxw { get => new vec4(z, z, x, w); set { z = value.x; z = value.y; x = value.z; w = value.w; } }
        public vec4 zzyx { get => new vec4(z, z, y, x); set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public vec4 zzyy { get => new vec4(z, z, y, y); set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public vec4 zzyz { get => new vec4(z, z, y, z); set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public vec4 zzyw { get => new vec4(z, z, y, w); set { z = value.x; z = value.y; y = value.z; w = value.w; } }
        public vec4 zzzx { get => new vec4(z, z, z, x); set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public vec4 zzzy { get => new vec4(z, z, z, y); set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public vec4 zzzz { get => new vec4(z, z, z, z); set { z = value.x; z = value.y; z = value.z; z = value.w; } }
        public vec4 zzzw { get => new vec4(z, z, z, w); set { z = value.x; z = value.y; z = value.z; w = value.w; } }
        public vec4 zzwx { get => new vec4(z, z, w, x); set { z = value.x; z = value.y; w = value.z; x = value.w; } }
        public vec4 zzwy { get => new vec4(z, z, w, y); set { z = value.x; z = value.y; w = value.z; y = value.w; } }
        public vec4 zzwz { get => new vec4(z, z, w, z); set { z = value.x; z = value.y; w = value.z; z = value.w; } }
        public vec4 zzww { get => new vec4(z, z, w, w); set { z = value.x; z = value.y; w = value.z; w = value.w; } }
        public vec4 zwxx { get => new vec4(z, w, x, x); set { z = value.x; w = value.y; x = value.z; x = value.w; } }
        public vec4 zwxy { get => new vec4(z, w, x, y); set { z = value.x; w = value.y; x = value.z; y = value.w; } }
        public vec4 zwxz { get => new vec4(z, w, x, z); set { z = value.x; w = value.y; x = value.z; z = value.w; } }
        public vec4 zwxw { get => new vec4(z, w, x, w); set { z = value.x; w = value.y; x = value.z; w = value.w; } }
        public vec4 zwyx { get => new vec4(z, w, y, x); set { z = value.x; w = value.y; y = value.z; x = value.w; } }
        public vec4 zwyy { get => new vec4(z, w, y, y); set { z = value.x; w = value.y; y = value.z; y = value.w; } }
        public vec4 zwyz { get => new vec4(z, w, y, z); set { z = value.x; w = value.y; y = value.z; z = value.w; } }
        public vec4 zwyw { get => new vec4(z, w, y, w); set { z = value.x; w = value.y; y = value.z; w = value.w; } }
        public vec4 zwzx { get => new vec4(z, w, z, x); set { z = value.x; w = value.y; z = value.z; x = value.w; } }
        public vec4 zwzy { get => new vec4(z, w, z, y); set { z = value.x; w = value.y; z = value.z; y = value.w; } }
        public vec4 zwzz { get => new vec4(z, w, z, z); set { z = value.x; w = value.y; z = value.z; z = value.w; } }
        public vec4 zwzw { get => new vec4(z, w, z, w); set { z = value.x; w = value.y; z = value.z; w = value.w; } }
        public vec4 zwwx { get => new vec4(z, w, w, x); set { z = value.x; w = value.y; w = value.z; x = value.w; } }
        public vec4 zwwy { get => new vec4(z, w, w, y); set { z = value.x; w = value.y; w = value.z; y = value.w; } }
        public vec4 zwwz { get => new vec4(z, w, w, z); set { z = value.x; w = value.y; w = value.z; z = value.w; } }
        public vec4 zwww { get => new vec4(z, w, w, w); set { z = value.x; w = value.y; w = value.z; w = value.w; } }
        public vec4 wxxx { get => new vec4(w, x, x, x); set { w = value.x; x = value.y; x = value.z; x = value.w; } }
        public vec4 wxxy { get => new vec4(w, x, x, y); set { w = value.x; x = value.y; x = value.z; y = value.w; } }
        public vec4 wxxz { get => new vec4(w, x, x, z); set { w = value.x; x = value.y; x = value.z; z = value.w; } }
        public vec4 wxxw { get => new vec4(w, x, x, w); set { w = value.x; x = value.y; x = value.z; w = value.w; } }
        public vec4 wxyx { get => new vec4(w, x, y, x); set { w = value.x; x = value.y; y = value.z; x = value.w; } }
        public vec4 wxyy { get => new vec4(w, x, y, y); set { w = value.x; x = value.y; y = value.z; y = value.w; } }
        public vec4 wxyz { get => new vec4(w, x, y, z); set { w = value.x; x = value.y; y = value.z; z = value.w; } }
        public vec4 wxyw { get => new vec4(w, x, y, w); set { w = value.x; x = value.y; y = value.z; w = value.w; } }
        public vec4 wxzx { get => new vec4(w, x, z, x); set { w = value.x; x = value.y; z = value.z; x = value.w; } }
        public vec4 wxzy { get => new vec4(w, x, z, y); set { w = value.x; x = value.y; z = value.z; y = value.w; } }
        public vec4 wxzz { get => new vec4(w, x, z, z); set { w = value.x; x = value.y; z = value.z; z = value.w; } }
        public vec4 wxzw { get => new vec4(w, x, z, w); set { w = value.x; x = value.y; z = value.z; w = value.w; } }
        public vec4 wxwx { get => new vec4(w, x, w, x); set { w = value.x; x = value.y; w = value.z; x = value.w; } }
        public vec4 wxwy { get => new vec4(w, x, w, y); set { w = value.x; x = value.y; w = value.z; y = value.w; } }
        public vec4 wxwz { get => new vec4(w, x, w, z); set { w = value.x; x = value.y; w = value.z; z = value.w; } }
        public vec4 wxww { get => new vec4(w, x, w, w); set { w = value.x; x = value.y; w = value.z; w = value.w; } }
        public vec4 wyxx { get => new vec4(w, y, x, x); set { w = value.x; y = value.y; x = value.z; x = value.w; } }
        public vec4 wyxy { get => new vec4(w, y, x, y); set { w = value.x; y = value.y; x = value.z; y = value.w; } }
        public vec4 wyxz { get => new vec4(w, y, x, z); set { w = value.x; y = value.y; x = value.z; z = value.w; } }
        public vec4 wyxw { get => new vec4(w, y, x, w); set { w = value.x; y = value.y; x = value.z; w = value.w; } }
        public vec4 wyyx { get => new vec4(w, y, y, x); set { w = value.x; y = value.y; y = value.z; x = value.w; } }
        public vec4 wyyy { get => new vec4(w, y, y, y); set { w = value.x; y = value.y; y = value.z; y = value.w; } }
        public vec4 wyyz { get => new vec4(w, y, y, z); set { w = value.x; y = value.y; y = value.z; z = value.w; } }
        public vec4 wyyw { get => new vec4(w, y, y, w); set { w = value.x; y = value.y; y = value.z; w = value.w; } }
        public vec4 wyzx { get => new vec4(w, y, z, x); set { w = value.x; y = value.y; z = value.z; x = value.w; } }
        public vec4 wyzy { get => new vec4(w, y, z, y); set { w = value.x; y = value.y; z = value.z; y = value.w; } }
        public vec4 wyzz { get => new vec4(w, y, z, z); set { w = value.x; y = value.y; z = value.z; z = value.w; } }
        public vec4 wyzw { get => new vec4(w, y, z, w); set { w = value.x; y = value.y; z = value.z; w = value.w; } }
        public vec4 wywx { get => new vec4(w, y, w, x); set { w = value.x; y = value.y; w = value.z; x = value.w; } }
        public vec4 wywy { get => new vec4(w, y, w, y); set { w = value.x; y = value.y; w = value.z; y = value.w; } }
        public vec4 wywz { get => new vec4(w, y, w, z); set { w = value.x; y = value.y; w = value.z; z = value.w; } }
        public vec4 wyww { get => new vec4(w, y, w, w); set { w = value.x; y = value.y; w = value.z; w = value.w; } }
        public vec4 wzxx { get => new vec4(w, z, x, x); set { w = value.x; z = value.y; x = value.z; x = value.w; } }
        public vec4 wzxy { get => new vec4(w, z, x, y); set { w = value.x; z = value.y; x = value.z; y = value.w; } }
        public vec4 wzxz { get => new vec4(w, z, x, z); set { w = value.x; z = value.y; x = value.z; z = value.w; } }
        public vec4 wzxw { get => new vec4(w, z, x, w); set { w = value.x; z = value.y; x = value.z; w = value.w; } }
        public vec4 wzyx { get => new vec4(w, z, y, x); set { w = value.x; z = value.y; y = value.z; x = value.w; } }
        public vec4 wzyy { get => new vec4(w, z, y, y); set { w = value.x; z = value.y; y = value.z; y = value.w; } }
        public vec4 wzyz { get => new vec4(w, z, y, z); set { w = value.x; z = value.y; y = value.z; z = value.w; } }
        public vec4 wzyw { get => new vec4(w, z, y, w); set { w = value.x; z = value.y; y = value.z; w = value.w; } }
        public vec4 wzzx { get => new vec4(w, z, z, x); set { w = value.x; z = value.y; z = value.z; x = value.w; } }
        public vec4 wzzy { get => new vec4(w, z, z, y); set { w = value.x; z = value.y; z = value.z; y = value.w; } }
        public vec4 wzzz { get => new vec4(w, z, z, z); set { w = value.x; z = value.y; z = value.z; z = value.w; } }
        public vec4 wzzw { get => new vec4(w, z, z, w); set { w = value.x; z = value.y; z = value.z; w = value.w; } }
        public vec4 wzwx { get => new vec4(w, z, w, x); set { w = value.x; z = value.y; w = value.z; x = value.w; } }
        public vec4 wzwy { get => new vec4(w, z, w, y); set { w = value.x; z = value.y; w = value.z; y = value.w; } }
        public vec4 wzwz { get => new vec4(w, z, w, z); set { w = value.x; z = value.y; w = value.z; z = value.w; } }
        public vec4 wzww { get => new vec4(w, z, w, w); set { w = value.x; z = value.y; w = value.z; w = value.w; } }
        public vec4 wwxx { get => new vec4(w, w, x, x); set { w = value.x; w = value.y; x = value.z; x = value.w; } }
        public vec4 wwxy { get => new vec4(w, w, x, y); set { w = value.x; w = value.y; x = value.z; y = value.w; } }
        public vec4 wwxz { get => new vec4(w, w, x, z); set { w = value.x; w = value.y; x = value.z; z = value.w; } }
        public vec4 wwxw { get => new vec4(w, w, x, w); set { w = value.x; w = value.y; x = value.z; w = value.w; } }
        public vec4 wwyx { get => new vec4(w, w, y, x); set { w = value.x; w = value.y; y = value.z; x = value.w; } }
        public vec4 wwyy { get => new vec4(w, w, y, y); set { w = value.x; w = value.y; y = value.z; y = value.w; } }
        public vec4 wwyz { get => new vec4(w, w, y, z); set { w = value.x; w = value.y; y = value.z; z = value.w; } }
        public vec4 wwyw { get => new vec4(w, w, y, w); set { w = value.x; w = value.y; y = value.z; w = value.w; } }
        public vec4 wwzx { get => new vec4(w, w, z, x); set { w = value.x; w = value.y; z = value.z; x = value.w; } }
        public vec4 wwzy { get => new vec4(w, w, z, y); set { w = value.x; w = value.y; z = value.z; y = value.w; } }
        public vec4 wwzz { get => new vec4(w, w, z, z); set { w = value.x; w = value.y; z = value.z; z = value.w; } }
        public vec4 wwzw { get => new vec4(w, w, z, w); set { w = value.x; w = value.y; z = value.z; w = value.w; } }
        public vec4 wwwx { get => new vec4(w, w, w, x); set { w = value.x; w = value.y; w = value.z; x = value.w; } }
        public vec4 wwwy { get => new vec4(w, w, w, y); set { w = value.x; w = value.y; w = value.z; y = value.w; } }
        public vec4 wwwz { get => new vec4(w, w, w, z); set { w = value.x; w = value.y; w = value.z; z = value.w; } }
        public vec4 wwww { get => new vec4(w, w, w, w); set { w = value.x; w = value.y; w = value.z; w = value.w; } }

        #endregion
    }

    public struct dvec4
    {
        public double x, y, z, w;
        public double this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
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
                    case 3: w = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ", " + w + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y, z, w };
        }

        #region vec4

        public dvec4(double a) : this(a, a, a, a) { }
        public dvec4(double[] v) : this(v[0], v[1], v[2], v[3]) { }
        public dvec4(double X, double Y, double Z, double W) { x = X; y = Y; z = Z; w = W; }
        public dvec4(dvec2 xy, double z, double w) : this(xy.x, xy.y, z, w) { }
        public dvec4(double x, dvec2 yz, double w) : this(x, yz.x, yz.y, w) { }
        public dvec4(double x, double y, dvec2 zw) : this(x, y, zw.x, zw.y) { }
        public dvec4(dvec2 xy, dvec2 zw) : this(xy.x, xy.y, zw.x, zw.y) { }
        public dvec4(dvec3 xyz, double w) : this(xyz.x, xyz.y, xyz.z, w) { }
        public dvec4(double x, dvec3 yzw) : this(x, yzw.x, yzw.y, yzw.z) { }
        public dvec4(byte[] data) : this((double[])data.To(typeof(double))) { }

        #endregion

        #region Operators

        public static dvec4 operator +(dvec4 a) => new dvec4(a.x, a.y, a.z, a.w);
        public static dvec4 operator -(dvec4 a) => new dvec4(-a.x, -a.y, -a.z, -a.w);
        public static dvec4 operator +(dvec4 a, dvec4 b) => new dvec4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static dvec4 operator +(dvec4 a, double b) => new dvec4(a.x + b, a.y + b, a.z + b, a.w + b);
        public static dvec4 operator +(double a, dvec4 b) => new dvec4(a + b.x, a + b.y, a + b.z, a + b.w);
        public static dvec4 operator -(dvec4 a, dvec4 b) => new dvec4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static dvec4 operator -(dvec4 a, double b) => new dvec4(a.x - b, a.y - b, a.z - b, a.w - b);
        public static dvec4 operator -(double a, dvec4 b) => new dvec4(a - b.x, a - b.y, a - b.z, a - b.w);
        public static dvec4 operator *(dvec4 a, dvec4 b) => new dvec4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        public static dvec4 operator *(dvec4 a, double b) => new dvec4(a.x * b, a.y * b, a.z * b, a.w * b);
        public static dvec4 operator *(double a, dvec4 b) => new dvec4(a * b.x, a * b.y, a * b.z, a * b.w);
        public static dvec4 operator /(dvec4 a, dvec4 b) => new dvec4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        public static dvec4 operator /(dvec4 a, double b) => new dvec4(a.x / b, a.y / b, a.z / b, a.w / b);
        public static dvec4 operator /(double a, dvec4 b) => new dvec4(a / b.x, a / b.y, a / b.z, a / b.w);

        #endregion

        #region Generated
        
        public dvec2 xx { get => new dvec2(x, x); set { x = value.x; x = value.y; } }
        public dvec2 xy { get => new dvec2(x, y); set { x = value.x; y = value.y; } }
        public dvec2 xz { get => new dvec2(x, z); set { x = value.x; z = value.y; } }
        public dvec2 xw { get => new dvec2(x, w); set { x = value.x; w = value.y; } }
        public dvec2 yx { get => new dvec2(y, x); set { y = value.x; x = value.y; } }
        public dvec2 yy { get => new dvec2(y, y); set { y = value.x; y = value.y; } }
        public dvec2 yz { get => new dvec2(y, z); set { y = value.x; z = value.y; } }
        public dvec2 yw { get => new dvec2(y, w); set { y = value.x; w = value.y; } }
        public dvec2 zx { get => new dvec2(z, x); set { z = value.x; x = value.y; } }
        public dvec2 zy { get => new dvec2(z, y); set { z = value.x; y = value.y; } }
        public dvec2 zz { get => new dvec2(z, z); set { z = value.x; z = value.y; } }
        public dvec2 zw { get => new dvec2(z, w); set { z = value.x; w = value.y; } }
        public dvec2 wx { get => new dvec2(w, x); set { w = value.x; x = value.y; } }
        public dvec2 wy { get => new dvec2(w, y); set { w = value.x; y = value.y; } }
        public dvec2 wz { get => new dvec2(w, z); set { w = value.x; z = value.y; } }
        public dvec2 ww { get => new dvec2(w, w); set { w = value.x; w = value.y; } }
        public dvec3 xxx { get => new dvec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public dvec3 xxy { get => new dvec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public dvec3 xxz { get => new dvec3(x, x, z); set { x = value.x; x = value.y; z = value.z; } }
        public dvec3 xxw { get => new dvec3(x, x, w); set { x = value.x; x = value.y; w = value.z; } }
        public dvec3 xyx { get => new dvec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public dvec3 xyy { get => new dvec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public dvec3 xyz { get => new dvec3(x, y, z); set { x = value.x; y = value.y; z = value.z; } }
        public dvec3 xyw { get => new dvec3(x, y, w); set { x = value.x; y = value.y; w = value.z; } }
        public dvec3 xzx { get => new dvec3(x, z, x); set { x = value.x; z = value.y; x = value.z; } }
        public dvec3 xzy { get => new dvec3(x, z, y); set { x = value.x; z = value.y; y = value.z; } }
        public dvec3 xzz { get => new dvec3(x, z, z); set { x = value.x; z = value.y; z = value.z; } }
        public dvec3 xzw { get => new dvec3(x, z, w); set { x = value.x; z = value.y; w = value.z; } }
        public dvec3 xwx { get => new dvec3(x, w, x); set { x = value.x; w = value.y; x = value.z; } }
        public dvec3 xwy { get => new dvec3(x, w, y); set { x = value.x; w = value.y; y = value.z; } }
        public dvec3 xwz { get => new dvec3(x, w, z); set { x = value.x; w = value.y; z = value.z; } }
        public dvec3 xww { get => new dvec3(x, w, w); set { x = value.x; w = value.y; w = value.z; } }
        public dvec3 yxx { get => new dvec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public dvec3 yxy { get => new dvec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public dvec3 yxz { get => new dvec3(y, x, z); set { y = value.x; x = value.y; z = value.z; } }
        public dvec3 yxw { get => new dvec3(y, x, w); set { y = value.x; x = value.y; w = value.z; } }
        public dvec3 yyx { get => new dvec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public dvec3 yyy { get => new dvec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public dvec3 yyz { get => new dvec3(y, y, z); set { y = value.x; y = value.y; z = value.z; } }
        public dvec3 yyw { get => new dvec3(y, y, w); set { y = value.x; y = value.y; w = value.z; } }
        public dvec3 yzx { get => new dvec3(y, z, x); set { y = value.x; z = value.y; x = value.z; } }
        public dvec3 yzy { get => new dvec3(y, z, y); set { y = value.x; z = value.y; y = value.z; } }
        public dvec3 yzz { get => new dvec3(y, z, z); set { y = value.x; z = value.y; z = value.z; } }
        public dvec3 yzw { get => new dvec3(y, z, w); set { y = value.x; z = value.y; w = value.z; } }
        public dvec3 ywx { get => new dvec3(y, w, x); set { y = value.x; w = value.y; x = value.z; } }
        public dvec3 ywy { get => new dvec3(y, w, y); set { y = value.x; w = value.y; y = value.z; } }
        public dvec3 ywz { get => new dvec3(y, w, z); set { y = value.x; w = value.y; z = value.z; } }
        public dvec3 yww { get => new dvec3(y, w, w); set { y = value.x; w = value.y; w = value.z; } }
        public dvec3 zxx { get => new dvec3(z, x, x); set { z = value.x; x = value.y; x = value.z; } }
        public dvec3 zxy { get => new dvec3(z, x, y); set { z = value.x; x = value.y; y = value.z; } }
        public dvec3 zxz { get => new dvec3(z, x, z); set { z = value.x; x = value.y; z = value.z; } }
        public dvec3 zxw { get => new dvec3(z, x, w); set { z = value.x; x = value.y; w = value.z; } }
        public dvec3 zyx { get => new dvec3(z, y, x); set { z = value.x; y = value.y; x = value.z; } }
        public dvec3 zyy { get => new dvec3(z, y, y); set { z = value.x; y = value.y; y = value.z; } }
        public dvec3 zyz { get => new dvec3(z, y, z); set { z = value.x; y = value.y; z = value.z; } }
        public dvec3 zyw { get => new dvec3(z, y, w); set { z = value.x; y = value.y; w = value.z; } }
        public dvec3 zzx { get => new dvec3(z, z, x); set { z = value.x; z = value.y; x = value.z; } }
        public dvec3 zzy { get => new dvec3(z, z, y); set { z = value.x; z = value.y; y = value.z; } }
        public dvec3 zzz { get => new dvec3(z, z, z); set { z = value.x; z = value.y; z = value.z; } }
        public dvec3 zzw { get => new dvec3(z, z, w); set { z = value.x; z = value.y; w = value.z; } }
        public dvec3 zwx { get => new dvec3(z, w, x); set { z = value.x; w = value.y; x = value.z; } }
        public dvec3 zwy { get => new dvec3(z, w, y); set { z = value.x; w = value.y; y = value.z; } }
        public dvec3 zwz { get => new dvec3(z, w, z); set { z = value.x; w = value.y; z = value.z; } }
        public dvec3 zww { get => new dvec3(z, w, w); set { z = value.x; w = value.y; w = value.z; } }
        public dvec3 wxx { get => new dvec3(w, x, x); set { w = value.x; x = value.y; x = value.z; } }
        public dvec3 wxy { get => new dvec3(w, x, y); set { w = value.x; x = value.y; y = value.z; } }
        public dvec3 wxz { get => new dvec3(w, x, z); set { w = value.x; x = value.y; z = value.z; } }
        public dvec3 wxw { get => new dvec3(w, x, w); set { w = value.x; x = value.y; w = value.z; } }
        public dvec3 wyx { get => new dvec3(w, y, x); set { w = value.x; y = value.y; x = value.z; } }
        public dvec3 wyy { get => new dvec3(w, y, y); set { w = value.x; y = value.y; y = value.z; } }
        public dvec3 wyz { get => new dvec3(w, y, z); set { w = value.x; y = value.y; z = value.z; } }
        public dvec3 wyw { get => new dvec3(w, y, w); set { w = value.x; y = value.y; w = value.z; } }
        public dvec3 wzx { get => new dvec3(w, z, x); set { w = value.x; z = value.y; x = value.z; } }
        public dvec3 wzy { get => new dvec3(w, z, y); set { w = value.x; z = value.y; y = value.z; } }
        public dvec3 wzz { get => new dvec3(w, z, z); set { w = value.x; z = value.y; z = value.z; } }
        public dvec3 wzw { get => new dvec3(w, z, w); set { w = value.x; z = value.y; w = value.z; } }
        public dvec3 wwx { get => new dvec3(w, w, x); set { w = value.x; w = value.y; x = value.z; } }
        public dvec3 wwy { get => new dvec3(w, w, y); set { w = value.x; w = value.y; y = value.z; } }
        public dvec3 wwz { get => new dvec3(w, w, z); set { w = value.x; w = value.y; z = value.z; } }
        public dvec3 www { get => new dvec3(w, w, w); set { w = value.x; w = value.y; w = value.z; } }
        public dvec4 xxxx { get => new dvec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 xxxy { get => new dvec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 xxxz { get => new dvec4(x, x, x, z); set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public dvec4 xxxw { get => new dvec4(x, x, x, w); set { x = value.x; x = value.y; x = value.z; w = value.w; } }
        public dvec4 xxyx { get => new dvec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 xxyy { get => new dvec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 xxyz { get => new dvec4(x, x, y, z); set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public dvec4 xxyw { get => new dvec4(x, x, y, w); set { x = value.x; x = value.y; y = value.z; w = value.w; } }
        public dvec4 xxzx { get => new dvec4(x, x, z, x); set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public dvec4 xxzy { get => new dvec4(x, x, z, y); set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public dvec4 xxzz { get => new dvec4(x, x, z, z); set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public dvec4 xxzw { get => new dvec4(x, x, z, w); set { x = value.x; x = value.y; z = value.z; w = value.w; } }
        public dvec4 xxwx { get => new dvec4(x, x, w, x); set { x = value.x; x = value.y; w = value.z; x = value.w; } }
        public dvec4 xxwy { get => new dvec4(x, x, w, y); set { x = value.x; x = value.y; w = value.z; y = value.w; } }
        public dvec4 xxwz { get => new dvec4(x, x, w, z); set { x = value.x; x = value.y; w = value.z; z = value.w; } }
        public dvec4 xxww { get => new dvec4(x, x, w, w); set { x = value.x; x = value.y; w = value.z; w = value.w; } }
        public dvec4 xyxx { get => new dvec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 xyxy { get => new dvec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 xyxz { get => new dvec4(x, y, x, z); set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public dvec4 xyxw { get => new dvec4(x, y, x, w); set { x = value.x; y = value.y; x = value.z; w = value.w; } }
        public dvec4 xyyx { get => new dvec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 xyyy { get => new dvec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public dvec4 xyyz { get => new dvec4(x, y, y, z); set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public dvec4 xyyw { get => new dvec4(x, y, y, w); set { x = value.x; y = value.y; y = value.z; w = value.w; } }
        public dvec4 xyzx { get => new dvec4(x, y, z, x); set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public dvec4 xyzy { get => new dvec4(x, y, z, y); set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public dvec4 xyzz { get => new dvec4(x, y, z, z); set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public dvec4 xyzw { get => new dvec4(x, y, z, w); set { x = value.x; y = value.y; z = value.z; w = value.w; } }
        public dvec4 xywx { get => new dvec4(x, y, w, x); set { x = value.x; y = value.y; w = value.z; x = value.w; } }
        public dvec4 xywy { get => new dvec4(x, y, w, y); set { x = value.x; y = value.y; w = value.z; y = value.w; } }
        public dvec4 xywz { get => new dvec4(x, y, w, z); set { x = value.x; y = value.y; w = value.z; z = value.w; } }
        public dvec4 xyww { get => new dvec4(x, y, w, w); set { x = value.x; y = value.y; w = value.z; w = value.w; } }
        public dvec4 xzxx { get => new dvec4(x, z, x, x); set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public dvec4 xzxy { get => new dvec4(x, z, x, y); set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public dvec4 xzxz { get => new dvec4(x, z, x, z); set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public dvec4 xzxw { get => new dvec4(x, z, x, w); set { x = value.x; z = value.y; x = value.z; w = value.w; } }
        public dvec4 xzyx { get => new dvec4(x, z, y, x); set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public dvec4 xzyy { get => new dvec4(x, z, y, y); set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public dvec4 xzyz { get => new dvec4(x, z, y, z); set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public dvec4 xzyw { get => new dvec4(x, z, y, w); set { x = value.x; z = value.y; y = value.z; w = value.w; } }
        public dvec4 xzzx { get => new dvec4(x, z, z, x); set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public dvec4 xzzy { get => new dvec4(x, z, z, y); set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public dvec4 xzzz { get => new dvec4(x, z, z, z); set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public dvec4 xzzw { get => new dvec4(x, z, z, w); set { x = value.x; z = value.y; z = value.z; w = value.w; } }
        public dvec4 xzwx { get => new dvec4(x, z, w, x); set { x = value.x; z = value.y; w = value.z; x = value.w; } }
        public dvec4 xzwy { get => new dvec4(x, z, w, y); set { x = value.x; z = value.y; w = value.z; y = value.w; } }
        public dvec4 xzwz { get => new dvec4(x, z, w, z); set { x = value.x; z = value.y; w = value.z; z = value.w; } }
        public dvec4 xzww { get => new dvec4(x, z, w, w); set { x = value.x; z = value.y; w = value.z; w = value.w; } }
        public dvec4 xwxx { get => new dvec4(x, w, x, x); set { x = value.x; w = value.y; x = value.z; x = value.w; } }
        public dvec4 xwxy { get => new dvec4(x, w, x, y); set { x = value.x; w = value.y; x = value.z; y = value.w; } }
        public dvec4 xwxz { get => new dvec4(x, w, x, z); set { x = value.x; w = value.y; x = value.z; z = value.w; } }
        public dvec4 xwxw { get => new dvec4(x, w, x, w); set { x = value.x; w = value.y; x = value.z; w = value.w; } }
        public dvec4 xwyx { get => new dvec4(x, w, y, x); set { x = value.x; w = value.y; y = value.z; x = value.w; } }
        public dvec4 xwyy { get => new dvec4(x, w, y, y); set { x = value.x; w = value.y; y = value.z; y = value.w; } }
        public dvec4 xwyz { get => new dvec4(x, w, y, z); set { x = value.x; w = value.y; y = value.z; z = value.w; } }
        public dvec4 xwyw { get => new dvec4(x, w, y, w); set { x = value.x; w = value.y; y = value.z; w = value.w; } }
        public dvec4 xwzx { get => new dvec4(x, w, z, x); set { x = value.x; w = value.y; z = value.z; x = value.w; } }
        public dvec4 xwzy { get => new dvec4(x, w, z, y); set { x = value.x; w = value.y; z = value.z; y = value.w; } }
        public dvec4 xwzz { get => new dvec4(x, w, z, z); set { x = value.x; w = value.y; z = value.z; z = value.w; } }
        public dvec4 xwzw { get => new dvec4(x, w, z, w); set { x = value.x; w = value.y; z = value.z; w = value.w; } }
        public dvec4 xwwx { get => new dvec4(x, w, w, x); set { x = value.x; w = value.y; w = value.z; x = value.w; } }
        public dvec4 xwwy { get => new dvec4(x, w, w, y); set { x = value.x; w = value.y; w = value.z; y = value.w; } }
        public dvec4 xwwz { get => new dvec4(x, w, w, z); set { x = value.x; w = value.y; w = value.z; z = value.w; } }
        public dvec4 xwww { get => new dvec4(x, w, w, w); set { x = value.x; w = value.y; w = value.z; w = value.w; } }
        public dvec4 yxxx { get => new dvec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 yxxy { get => new dvec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 yxxz { get => new dvec4(y, x, x, z); set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public dvec4 yxxw { get => new dvec4(y, x, x, w); set { y = value.x; x = value.y; x = value.z; w = value.w; } }
        public dvec4 yxyx { get => new dvec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 yxyy { get => new dvec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 yxyz { get => new dvec4(y, x, y, z); set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public dvec4 yxyw { get => new dvec4(y, x, y, w); set { y = value.x; x = value.y; y = value.z; w = value.w; } }
        public dvec4 yxzx { get => new dvec4(y, x, z, x); set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public dvec4 yxzy { get => new dvec4(y, x, z, y); set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public dvec4 yxzz { get => new dvec4(y, x, z, z); set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public dvec4 yxzw { get => new dvec4(y, x, z, w); set { y = value.x; x = value.y; z = value.z; w = value.w; } }
        public dvec4 yxwx { get => new dvec4(y, x, w, x); set { y = value.x; x = value.y; w = value.z; x = value.w; } }
        public dvec4 yxwy { get => new dvec4(y, x, w, y); set { y = value.x; x = value.y; w = value.z; y = value.w; } }
        public dvec4 yxwz { get => new dvec4(y, x, w, z); set { y = value.x; x = value.y; w = value.z; z = value.w; } }
        public dvec4 yxww { get => new dvec4(y, x, w, w); set { y = value.x; x = value.y; w = value.z; w = value.w; } }
        public dvec4 yyxx { get => new dvec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 yyxy { get => new dvec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 yyxz { get => new dvec4(y, y, x, z); set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public dvec4 yyxw { get => new dvec4(y, y, x, w); set { y = value.x; y = value.y; x = value.z; w = value.w; } }
        public dvec4 yyyx { get => new dvec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 yyyy { get => new dvec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public dvec4 yyyz { get => new dvec4(y, y, y, z); set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public dvec4 yyyw { get => new dvec4(y, y, y, w); set { y = value.x; y = value.y; y = value.z; w = value.w; } }
        public dvec4 yyzx { get => new dvec4(y, y, z, x); set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public dvec4 yyzy { get => new dvec4(y, y, z, y); set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public dvec4 yyzz { get => new dvec4(y, y, z, z); set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public dvec4 yyzw { get => new dvec4(y, y, z, w); set { y = value.x; y = value.y; z = value.z; w = value.w; } }
        public dvec4 yywx { get => new dvec4(y, y, w, x); set { y = value.x; y = value.y; w = value.z; x = value.w; } }
        public dvec4 yywy { get => new dvec4(y, y, w, y); set { y = value.x; y = value.y; w = value.z; y = value.w; } }
        public dvec4 yywz { get => new dvec4(y, y, w, z); set { y = value.x; y = value.y; w = value.z; z = value.w; } }
        public dvec4 yyww { get => new dvec4(y, y, w, w); set { y = value.x; y = value.y; w = value.z; w = value.w; } }
        public dvec4 yzxx { get => new dvec4(y, z, x, x); set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public dvec4 yzxy { get => new dvec4(y, z, x, y); set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public dvec4 yzxz { get => new dvec4(y, z, x, z); set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public dvec4 yzxw { get => new dvec4(y, z, x, w); set { y = value.x; z = value.y; x = value.z; w = value.w; } }
        public dvec4 yzyx { get => new dvec4(y, z, y, x); set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public dvec4 yzyy { get => new dvec4(y, z, y, y); set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public dvec4 yzyz { get => new dvec4(y, z, y, z); set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public dvec4 yzyw { get => new dvec4(y, z, y, w); set { y = value.x; z = value.y; y = value.z; w = value.w; } }
        public dvec4 yzzx { get => new dvec4(y, z, z, x); set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public dvec4 yzzy { get => new dvec4(y, z, z, y); set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public dvec4 yzzz { get => new dvec4(y, z, z, z); set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public dvec4 yzzw { get => new dvec4(y, z, z, w); set { y = value.x; z = value.y; z = value.z; w = value.w; } }
        public dvec4 yzwx { get => new dvec4(y, z, w, x); set { y = value.x; z = value.y; w = value.z; x = value.w; } }
        public dvec4 yzwy { get => new dvec4(y, z, w, y); set { y = value.x; z = value.y; w = value.z; y = value.w; } }
        public dvec4 yzwz { get => new dvec4(y, z, w, z); set { y = value.x; z = value.y; w = value.z; z = value.w; } }
        public dvec4 yzww { get => new dvec4(y, z, w, w); set { y = value.x; z = value.y; w = value.z; w = value.w; } }
        public dvec4 ywxx { get => new dvec4(y, w, x, x); set { y = value.x; w = value.y; x = value.z; x = value.w; } }
        public dvec4 ywxy { get => new dvec4(y, w, x, y); set { y = value.x; w = value.y; x = value.z; y = value.w; } }
        public dvec4 ywxz { get => new dvec4(y, w, x, z); set { y = value.x; w = value.y; x = value.z; z = value.w; } }
        public dvec4 ywxw { get => new dvec4(y, w, x, w); set { y = value.x; w = value.y; x = value.z; w = value.w; } }
        public dvec4 ywyx { get => new dvec4(y, w, y, x); set { y = value.x; w = value.y; y = value.z; x = value.w; } }
        public dvec4 ywyy { get => new dvec4(y, w, y, y); set { y = value.x; w = value.y; y = value.z; y = value.w; } }
        public dvec4 ywyz { get => new dvec4(y, w, y, z); set { y = value.x; w = value.y; y = value.z; z = value.w; } }
        public dvec4 ywyw { get => new dvec4(y, w, y, w); set { y = value.x; w = value.y; y = value.z; w = value.w; } }
        public dvec4 ywzx { get => new dvec4(y, w, z, x); set { y = value.x; w = value.y; z = value.z; x = value.w; } }
        public dvec4 ywzy { get => new dvec4(y, w, z, y); set { y = value.x; w = value.y; z = value.z; y = value.w; } }
        public dvec4 ywzz { get => new dvec4(y, w, z, z); set { y = value.x; w = value.y; z = value.z; z = value.w; } }
        public dvec4 ywzw { get => new dvec4(y, w, z, w); set { y = value.x; w = value.y; z = value.z; w = value.w; } }
        public dvec4 ywwx { get => new dvec4(y, w, w, x); set { y = value.x; w = value.y; w = value.z; x = value.w; } }
        public dvec4 ywwy { get => new dvec4(y, w, w, y); set { y = value.x; w = value.y; w = value.z; y = value.w; } }
        public dvec4 ywwz { get => new dvec4(y, w, w, z); set { y = value.x; w = value.y; w = value.z; z = value.w; } }
        public dvec4 ywww { get => new dvec4(y, w, w, w); set { y = value.x; w = value.y; w = value.z; w = value.w; } }
        public dvec4 zxxx { get => new dvec4(z, x, x, x); set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 zxxy { get => new dvec4(z, x, x, y); set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 zxxz { get => new dvec4(z, x, x, z); set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public dvec4 zxxw { get => new dvec4(z, x, x, w); set { z = value.x; x = value.y; x = value.z; w = value.w; } }
        public dvec4 zxyx { get => new dvec4(z, x, y, x); set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 zxyy { get => new dvec4(z, x, y, y); set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 zxyz { get => new dvec4(z, x, y, z); set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public dvec4 zxyw { get => new dvec4(z, x, y, w); set { z = value.x; x = value.y; y = value.z; w = value.w; } }
        public dvec4 zxzx { get => new dvec4(z, x, z, x); set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public dvec4 zxzy { get => new dvec4(z, x, z, y); set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public dvec4 zxzz { get => new dvec4(z, x, z, z); set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public dvec4 zxzw { get => new dvec4(z, x, z, w); set { z = value.x; x = value.y; z = value.z; w = value.w; } }
        public dvec4 zxwx { get => new dvec4(z, x, w, x); set { z = value.x; x = value.y; w = value.z; x = value.w; } }
        public dvec4 zxwy { get => new dvec4(z, x, w, y); set { z = value.x; x = value.y; w = value.z; y = value.w; } }
        public dvec4 zxwz { get => new dvec4(z, x, w, z); set { z = value.x; x = value.y; w = value.z; z = value.w; } }
        public dvec4 zxww { get => new dvec4(z, x, w, w); set { z = value.x; x = value.y; w = value.z; w = value.w; } }
        public dvec4 zyxx { get => new dvec4(z, y, x, x); set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 zyxy { get => new dvec4(z, y, x, y); set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 zyxz { get => new dvec4(z, y, x, z); set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public dvec4 zyxw { get => new dvec4(z, y, x, w); set { z = value.x; y = value.y; x = value.z; w = value.w; } }
        public dvec4 zyyx { get => new dvec4(z, y, y, x); set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 zyyy { get => new dvec4(z, y, y, y); set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public dvec4 zyyz { get => new dvec4(z, y, y, z); set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public dvec4 zyyw { get => new dvec4(z, y, y, w); set { z = value.x; y = value.y; y = value.z; w = value.w; } }
        public dvec4 zyzx { get => new dvec4(z, y, z, x); set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public dvec4 zyzy { get => new dvec4(z, y, z, y); set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public dvec4 zyzz { get => new dvec4(z, y, z, z); set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public dvec4 zyzw { get => new dvec4(z, y, z, w); set { z = value.x; y = value.y; z = value.z; w = value.w; } }
        public dvec4 zywx { get => new dvec4(z, y, w, x); set { z = value.x; y = value.y; w = value.z; x = value.w; } }
        public dvec4 zywy { get => new dvec4(z, y, w, y); set { z = value.x; y = value.y; w = value.z; y = value.w; } }
        public dvec4 zywz { get => new dvec4(z, y, w, z); set { z = value.x; y = value.y; w = value.z; z = value.w; } }
        public dvec4 zyww { get => new dvec4(z, y, w, w); set { z = value.x; y = value.y; w = value.z; w = value.w; } }
        public dvec4 zzxx { get => new dvec4(z, z, x, x); set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public dvec4 zzxy { get => new dvec4(z, z, x, y); set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public dvec4 zzxz { get => new dvec4(z, z, x, z); set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public dvec4 zzxw { get => new dvec4(z, z, x, w); set { z = value.x; z = value.y; x = value.z; w = value.w; } }
        public dvec4 zzyx { get => new dvec4(z, z, y, x); set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public dvec4 zzyy { get => new dvec4(z, z, y, y); set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public dvec4 zzyz { get => new dvec4(z, z, y, z); set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public dvec4 zzyw { get => new dvec4(z, z, y, w); set { z = value.x; z = value.y; y = value.z; w = value.w; } }
        public dvec4 zzzx { get => new dvec4(z, z, z, x); set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public dvec4 zzzy { get => new dvec4(z, z, z, y); set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public dvec4 zzzz { get => new dvec4(z, z, z, z); set { z = value.x; z = value.y; z = value.z; z = value.w; } }
        public dvec4 zzzw { get => new dvec4(z, z, z, w); set { z = value.x; z = value.y; z = value.z; w = value.w; } }
        public dvec4 zzwx { get => new dvec4(z, z, w, x); set { z = value.x; z = value.y; w = value.z; x = value.w; } }
        public dvec4 zzwy { get => new dvec4(z, z, w, y); set { z = value.x; z = value.y; w = value.z; y = value.w; } }
        public dvec4 zzwz { get => new dvec4(z, z, w, z); set { z = value.x; z = value.y; w = value.z; z = value.w; } }
        public dvec4 zzww { get => new dvec4(z, z, w, w); set { z = value.x; z = value.y; w = value.z; w = value.w; } }
        public dvec4 zwxx { get => new dvec4(z, w, x, x); set { z = value.x; w = value.y; x = value.z; x = value.w; } }
        public dvec4 zwxy { get => new dvec4(z, w, x, y); set { z = value.x; w = value.y; x = value.z; y = value.w; } }
        public dvec4 zwxz { get => new dvec4(z, w, x, z); set { z = value.x; w = value.y; x = value.z; z = value.w; } }
        public dvec4 zwxw { get => new dvec4(z, w, x, w); set { z = value.x; w = value.y; x = value.z; w = value.w; } }
        public dvec4 zwyx { get => new dvec4(z, w, y, x); set { z = value.x; w = value.y; y = value.z; x = value.w; } }
        public dvec4 zwyy { get => new dvec4(z, w, y, y); set { z = value.x; w = value.y; y = value.z; y = value.w; } }
        public dvec4 zwyz { get => new dvec4(z, w, y, z); set { z = value.x; w = value.y; y = value.z; z = value.w; } }
        public dvec4 zwyw { get => new dvec4(z, w, y, w); set { z = value.x; w = value.y; y = value.z; w = value.w; } }
        public dvec4 zwzx { get => new dvec4(z, w, z, x); set { z = value.x; w = value.y; z = value.z; x = value.w; } }
        public dvec4 zwzy { get => new dvec4(z, w, z, y); set { z = value.x; w = value.y; z = value.z; y = value.w; } }
        public dvec4 zwzz { get => new dvec4(z, w, z, z); set { z = value.x; w = value.y; z = value.z; z = value.w; } }
        public dvec4 zwzw { get => new dvec4(z, w, z, w); set { z = value.x; w = value.y; z = value.z; w = value.w; } }
        public dvec4 zwwx { get => new dvec4(z, w, w, x); set { z = value.x; w = value.y; w = value.z; x = value.w; } }
        public dvec4 zwwy { get => new dvec4(z, w, w, y); set { z = value.x; w = value.y; w = value.z; y = value.w; } }
        public dvec4 zwwz { get => new dvec4(z, w, w, z); set { z = value.x; w = value.y; w = value.z; z = value.w; } }
        public dvec4 zwww { get => new dvec4(z, w, w, w); set { z = value.x; w = value.y; w = value.z; w = value.w; } }
        public dvec4 wxxx { get => new dvec4(w, x, x, x); set { w = value.x; x = value.y; x = value.z; x = value.w; } }
        public dvec4 wxxy { get => new dvec4(w, x, x, y); set { w = value.x; x = value.y; x = value.z; y = value.w; } }
        public dvec4 wxxz { get => new dvec4(w, x, x, z); set { w = value.x; x = value.y; x = value.z; z = value.w; } }
        public dvec4 wxxw { get => new dvec4(w, x, x, w); set { w = value.x; x = value.y; x = value.z; w = value.w; } }
        public dvec4 wxyx { get => new dvec4(w, x, y, x); set { w = value.x; x = value.y; y = value.z; x = value.w; } }
        public dvec4 wxyy { get => new dvec4(w, x, y, y); set { w = value.x; x = value.y; y = value.z; y = value.w; } }
        public dvec4 wxyz { get => new dvec4(w, x, y, z); set { w = value.x; x = value.y; y = value.z; z = value.w; } }
        public dvec4 wxyw { get => new dvec4(w, x, y, w); set { w = value.x; x = value.y; y = value.z; w = value.w; } }
        public dvec4 wxzx { get => new dvec4(w, x, z, x); set { w = value.x; x = value.y; z = value.z; x = value.w; } }
        public dvec4 wxzy { get => new dvec4(w, x, z, y); set { w = value.x; x = value.y; z = value.z; y = value.w; } }
        public dvec4 wxzz { get => new dvec4(w, x, z, z); set { w = value.x; x = value.y; z = value.z; z = value.w; } }
        public dvec4 wxzw { get => new dvec4(w, x, z, w); set { w = value.x; x = value.y; z = value.z; w = value.w; } }
        public dvec4 wxwx { get => new dvec4(w, x, w, x); set { w = value.x; x = value.y; w = value.z; x = value.w; } }
        public dvec4 wxwy { get => new dvec4(w, x, w, y); set { w = value.x; x = value.y; w = value.z; y = value.w; } }
        public dvec4 wxwz { get => new dvec4(w, x, w, z); set { w = value.x; x = value.y; w = value.z; z = value.w; } }
        public dvec4 wxww { get => new dvec4(w, x, w, w); set { w = value.x; x = value.y; w = value.z; w = value.w; } }
        public dvec4 wyxx { get => new dvec4(w, y, x, x); set { w = value.x; y = value.y; x = value.z; x = value.w; } }
        public dvec4 wyxy { get => new dvec4(w, y, x, y); set { w = value.x; y = value.y; x = value.z; y = value.w; } }
        public dvec4 wyxz { get => new dvec4(w, y, x, z); set { w = value.x; y = value.y; x = value.z; z = value.w; } }
        public dvec4 wyxw { get => new dvec4(w, y, x, w); set { w = value.x; y = value.y; x = value.z; w = value.w; } }
        public dvec4 wyyx { get => new dvec4(w, y, y, x); set { w = value.x; y = value.y; y = value.z; x = value.w; } }
        public dvec4 wyyy { get => new dvec4(w, y, y, y); set { w = value.x; y = value.y; y = value.z; y = value.w; } }
        public dvec4 wyyz { get => new dvec4(w, y, y, z); set { w = value.x; y = value.y; y = value.z; z = value.w; } }
        public dvec4 wyyw { get => new dvec4(w, y, y, w); set { w = value.x; y = value.y; y = value.z; w = value.w; } }
        public dvec4 wyzx { get => new dvec4(w, y, z, x); set { w = value.x; y = value.y; z = value.z; x = value.w; } }
        public dvec4 wyzy { get => new dvec4(w, y, z, y); set { w = value.x; y = value.y; z = value.z; y = value.w; } }
        public dvec4 wyzz { get => new dvec4(w, y, z, z); set { w = value.x; y = value.y; z = value.z; z = value.w; } }
        public dvec4 wyzw { get => new dvec4(w, y, z, w); set { w = value.x; y = value.y; z = value.z; w = value.w; } }
        public dvec4 wywx { get => new dvec4(w, y, w, x); set { w = value.x; y = value.y; w = value.z; x = value.w; } }
        public dvec4 wywy { get => new dvec4(w, y, w, y); set { w = value.x; y = value.y; w = value.z; y = value.w; } }
        public dvec4 wywz { get => new dvec4(w, y, w, z); set { w = value.x; y = value.y; w = value.z; z = value.w; } }
        public dvec4 wyww { get => new dvec4(w, y, w, w); set { w = value.x; y = value.y; w = value.z; w = value.w; } }
        public dvec4 wzxx { get => new dvec4(w, z, x, x); set { w = value.x; z = value.y; x = value.z; x = value.w; } }
        public dvec4 wzxy { get => new dvec4(w, z, x, y); set { w = value.x; z = value.y; x = value.z; y = value.w; } }
        public dvec4 wzxz { get => new dvec4(w, z, x, z); set { w = value.x; z = value.y; x = value.z; z = value.w; } }
        public dvec4 wzxw { get => new dvec4(w, z, x, w); set { w = value.x; z = value.y; x = value.z; w = value.w; } }
        public dvec4 wzyx { get => new dvec4(w, z, y, x); set { w = value.x; z = value.y; y = value.z; x = value.w; } }
        public dvec4 wzyy { get => new dvec4(w, z, y, y); set { w = value.x; z = value.y; y = value.z; y = value.w; } }
        public dvec4 wzyz { get => new dvec4(w, z, y, z); set { w = value.x; z = value.y; y = value.z; z = value.w; } }
        public dvec4 wzyw { get => new dvec4(w, z, y, w); set { w = value.x; z = value.y; y = value.z; w = value.w; } }
        public dvec4 wzzx { get => new dvec4(w, z, z, x); set { w = value.x; z = value.y; z = value.z; x = value.w; } }
        public dvec4 wzzy { get => new dvec4(w, z, z, y); set { w = value.x; z = value.y; z = value.z; y = value.w; } }
        public dvec4 wzzz { get => new dvec4(w, z, z, z); set { w = value.x; z = value.y; z = value.z; z = value.w; } }
        public dvec4 wzzw { get => new dvec4(w, z, z, w); set { w = value.x; z = value.y; z = value.z; w = value.w; } }
        public dvec4 wzwx { get => new dvec4(w, z, w, x); set { w = value.x; z = value.y; w = value.z; x = value.w; } }
        public dvec4 wzwy { get => new dvec4(w, z, w, y); set { w = value.x; z = value.y; w = value.z; y = value.w; } }
        public dvec4 wzwz { get => new dvec4(w, z, w, z); set { w = value.x; z = value.y; w = value.z; z = value.w; } }
        public dvec4 wzww { get => new dvec4(w, z, w, w); set { w = value.x; z = value.y; w = value.z; w = value.w; } }
        public dvec4 wwxx { get => new dvec4(w, w, x, x); set { w = value.x; w = value.y; x = value.z; x = value.w; } }
        public dvec4 wwxy { get => new dvec4(w, w, x, y); set { w = value.x; w = value.y; x = value.z; y = value.w; } }
        public dvec4 wwxz { get => new dvec4(w, w, x, z); set { w = value.x; w = value.y; x = value.z; z = value.w; } }
        public dvec4 wwxw { get => new dvec4(w, w, x, w); set { w = value.x; w = value.y; x = value.z; w = value.w; } }
        public dvec4 wwyx { get => new dvec4(w, w, y, x); set { w = value.x; w = value.y; y = value.z; x = value.w; } }
        public dvec4 wwyy { get => new dvec4(w, w, y, y); set { w = value.x; w = value.y; y = value.z; y = value.w; } }
        public dvec4 wwyz { get => new dvec4(w, w, y, z); set { w = value.x; w = value.y; y = value.z; z = value.w; } }
        public dvec4 wwyw { get => new dvec4(w, w, y, w); set { w = value.x; w = value.y; y = value.z; w = value.w; } }
        public dvec4 wwzx { get => new dvec4(w, w, z, x); set { w = value.x; w = value.y; z = value.z; x = value.w; } }
        public dvec4 wwzy { get => new dvec4(w, w, z, y); set { w = value.x; w = value.y; z = value.z; y = value.w; } }
        public dvec4 wwzz { get => new dvec4(w, w, z, z); set { w = value.x; w = value.y; z = value.z; z = value.w; } }
        public dvec4 wwzw { get => new dvec4(w, w, z, w); set { w = value.x; w = value.y; z = value.z; w = value.w; } }
        public dvec4 wwwx { get => new dvec4(w, w, w, x); set { w = value.x; w = value.y; w = value.z; x = value.w; } }
        public dvec4 wwwy { get => new dvec4(w, w, w, y); set { w = value.x; w = value.y; w = value.z; y = value.w; } }
        public dvec4 wwwz { get => new dvec4(w, w, w, z); set { w = value.x; w = value.y; w = value.z; z = value.w; } }
        public dvec4 wwww { get => new dvec4(w, w, w, w); set { w = value.x; w = value.y; w = value.z; w = value.w; } }

        #endregion
    }

    public struct bvec4
    {
        public bool x, y, z, w;
        public bool this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
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
                    case 3: w = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ", " + w + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y, z, w };
        }

        #region vec4

        public bvec4(bool a) : this(a, a, a, a) { }
        public bvec4(bool[] v) : this(v[0], v[1], v[2], v[3]) { }
        public bvec4(bool X, bool Y, bool Z, bool W) { x = X; y = Y; z = Z; w = W; }
        public bvec4(bvec2 xy, bool z, bool w) : this(xy.x, xy.y, z, w) { }
        public bvec4(bool x, bvec2 yz, bool w) : this(x, yz.x, yz.y, w) { }
        public bvec4(bool x, bool y, bvec2 zw) : this(x, y, zw.x, zw.y) { }
        public bvec4(bvec2 xy, bvec2 zw) : this(xy.x, xy.y, zw.x, zw.y) { }
        public bvec4(bvec3 xyz, bool w) : this(xyz.x, xyz.y, xyz.z, w) { }
        public bvec4(bool x, bvec3 yzw) : this(x, yzw.x, yzw.y, yzw.z) { }
        public bvec4(byte[] data) : this((bool[])data.To(typeof(bool))) { }

        #endregion

        #region Generated
        
        public bvec2 xx { get => new bvec2(x, x); set { x = value.x; x = value.y; } }
        public bvec2 xy { get => new bvec2(x, y); set { x = value.x; y = value.y; } }
        public bvec2 xz { get => new bvec2(x, z); set { x = value.x; z = value.y; } }
        public bvec2 xw { get => new bvec2(x, w); set { x = value.x; w = value.y; } }
        public bvec2 yx { get => new bvec2(y, x); set { y = value.x; x = value.y; } }
        public bvec2 yy { get => new bvec2(y, y); set { y = value.x; y = value.y; } }
        public bvec2 yz { get => new bvec2(y, z); set { y = value.x; z = value.y; } }
        public bvec2 yw { get => new bvec2(y, w); set { y = value.x; w = value.y; } }
        public bvec2 zx { get => new bvec2(z, x); set { z = value.x; x = value.y; } }
        public bvec2 zy { get => new bvec2(z, y); set { z = value.x; y = value.y; } }
        public bvec2 zz { get => new bvec2(z, z); set { z = value.x; z = value.y; } }
        public bvec2 zw { get => new bvec2(z, w); set { z = value.x; w = value.y; } }
        public bvec2 wx { get => new bvec2(w, x); set { w = value.x; x = value.y; } }
        public bvec2 wy { get => new bvec2(w, y); set { w = value.x; y = value.y; } }
        public bvec2 wz { get => new bvec2(w, z); set { w = value.x; z = value.y; } }
        public bvec2 ww { get => new bvec2(w, w); set { w = value.x; w = value.y; } }
        public bvec3 xxx { get => new bvec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public bvec3 xxy { get => new bvec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public bvec3 xxz { get => new bvec3(x, x, z); set { x = value.x; x = value.y; z = value.z; } }
        public bvec3 xxw { get => new bvec3(x, x, w); set { x = value.x; x = value.y; w = value.z; } }
        public bvec3 xyx { get => new bvec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public bvec3 xyy { get => new bvec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public bvec3 xyz { get => new bvec3(x, y, z); set { x = value.x; y = value.y; z = value.z; } }
        public bvec3 xyw { get => new bvec3(x, y, w); set { x = value.x; y = value.y; w = value.z; } }
        public bvec3 xzx { get => new bvec3(x, z, x); set { x = value.x; z = value.y; x = value.z; } }
        public bvec3 xzy { get => new bvec3(x, z, y); set { x = value.x; z = value.y; y = value.z; } }
        public bvec3 xzz { get => new bvec3(x, z, z); set { x = value.x; z = value.y; z = value.z; } }
        public bvec3 xzw { get => new bvec3(x, z, w); set { x = value.x; z = value.y; w = value.z; } }
        public bvec3 xwx { get => new bvec3(x, w, x); set { x = value.x; w = value.y; x = value.z; } }
        public bvec3 xwy { get => new bvec3(x, w, y); set { x = value.x; w = value.y; y = value.z; } }
        public bvec3 xwz { get => new bvec3(x, w, z); set { x = value.x; w = value.y; z = value.z; } }
        public bvec3 xww { get => new bvec3(x, w, w); set { x = value.x; w = value.y; w = value.z; } }
        public bvec3 yxx { get => new bvec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public bvec3 yxy { get => new bvec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public bvec3 yxz { get => new bvec3(y, x, z); set { y = value.x; x = value.y; z = value.z; } }
        public bvec3 yxw { get => new bvec3(y, x, w); set { y = value.x; x = value.y; w = value.z; } }
        public bvec3 yyx { get => new bvec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public bvec3 yyy { get => new bvec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public bvec3 yyz { get => new bvec3(y, y, z); set { y = value.x; y = value.y; z = value.z; } }
        public bvec3 yyw { get => new bvec3(y, y, w); set { y = value.x; y = value.y; w = value.z; } }
        public bvec3 yzx { get => new bvec3(y, z, x); set { y = value.x; z = value.y; x = value.z; } }
        public bvec3 yzy { get => new bvec3(y, z, y); set { y = value.x; z = value.y; y = value.z; } }
        public bvec3 yzz { get => new bvec3(y, z, z); set { y = value.x; z = value.y; z = value.z; } }
        public bvec3 yzw { get => new bvec3(y, z, w); set { y = value.x; z = value.y; w = value.z; } }
        public bvec3 ywx { get => new bvec3(y, w, x); set { y = value.x; w = value.y; x = value.z; } }
        public bvec3 ywy { get => new bvec3(y, w, y); set { y = value.x; w = value.y; y = value.z; } }
        public bvec3 ywz { get => new bvec3(y, w, z); set { y = value.x; w = value.y; z = value.z; } }
        public bvec3 yww { get => new bvec3(y, w, w); set { y = value.x; w = value.y; w = value.z; } }
        public bvec3 zxx { get => new bvec3(z, x, x); set { z = value.x; x = value.y; x = value.z; } }
        public bvec3 zxy { get => new bvec3(z, x, y); set { z = value.x; x = value.y; y = value.z; } }
        public bvec3 zxz { get => new bvec3(z, x, z); set { z = value.x; x = value.y; z = value.z; } }
        public bvec3 zxw { get => new bvec3(z, x, w); set { z = value.x; x = value.y; w = value.z; } }
        public bvec3 zyx { get => new bvec3(z, y, x); set { z = value.x; y = value.y; x = value.z; } }
        public bvec3 zyy { get => new bvec3(z, y, y); set { z = value.x; y = value.y; y = value.z; } }
        public bvec3 zyz { get => new bvec3(z, y, z); set { z = value.x; y = value.y; z = value.z; } }
        public bvec3 zyw { get => new bvec3(z, y, w); set { z = value.x; y = value.y; w = value.z; } }
        public bvec3 zzx { get => new bvec3(z, z, x); set { z = value.x; z = value.y; x = value.z; } }
        public bvec3 zzy { get => new bvec3(z, z, y); set { z = value.x; z = value.y; y = value.z; } }
        public bvec3 zzz { get => new bvec3(z, z, z); set { z = value.x; z = value.y; z = value.z; } }
        public bvec3 zzw { get => new bvec3(z, z, w); set { z = value.x; z = value.y; w = value.z; } }
        public bvec3 zwx { get => new bvec3(z, w, x); set { z = value.x; w = value.y; x = value.z; } }
        public bvec3 zwy { get => new bvec3(z, w, y); set { z = value.x; w = value.y; y = value.z; } }
        public bvec3 zwz { get => new bvec3(z, w, z); set { z = value.x; w = value.y; z = value.z; } }
        public bvec3 zww { get => new bvec3(z, w, w); set { z = value.x; w = value.y; w = value.z; } }
        public bvec3 wxx { get => new bvec3(w, x, x); set { w = value.x; x = value.y; x = value.z; } }
        public bvec3 wxy { get => new bvec3(w, x, y); set { w = value.x; x = value.y; y = value.z; } }
        public bvec3 wxz { get => new bvec3(w, x, z); set { w = value.x; x = value.y; z = value.z; } }
        public bvec3 wxw { get => new bvec3(w, x, w); set { w = value.x; x = value.y; w = value.z; } }
        public bvec3 wyx { get => new bvec3(w, y, x); set { w = value.x; y = value.y; x = value.z; } }
        public bvec3 wyy { get => new bvec3(w, y, y); set { w = value.x; y = value.y; y = value.z; } }
        public bvec3 wyz { get => new bvec3(w, y, z); set { w = value.x; y = value.y; z = value.z; } }
        public bvec3 wyw { get => new bvec3(w, y, w); set { w = value.x; y = value.y; w = value.z; } }
        public bvec3 wzx { get => new bvec3(w, z, x); set { w = value.x; z = value.y; x = value.z; } }
        public bvec3 wzy { get => new bvec3(w, z, y); set { w = value.x; z = value.y; y = value.z; } }
        public bvec3 wzz { get => new bvec3(w, z, z); set { w = value.x; z = value.y; z = value.z; } }
        public bvec3 wzw { get => new bvec3(w, z, w); set { w = value.x; z = value.y; w = value.z; } }
        public bvec3 wwx { get => new bvec3(w, w, x); set { w = value.x; w = value.y; x = value.z; } }
        public bvec3 wwy { get => new bvec3(w, w, y); set { w = value.x; w = value.y; y = value.z; } }
        public bvec3 wwz { get => new bvec3(w, w, z); set { w = value.x; w = value.y; z = value.z; } }
        public bvec3 www { get => new bvec3(w, w, w); set { w = value.x; w = value.y; w = value.z; } }
        public bvec4 xxxx { get => new bvec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 xxxy { get => new bvec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 xxxz { get => new bvec4(x, x, x, z); set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public bvec4 xxxw { get => new bvec4(x, x, x, w); set { x = value.x; x = value.y; x = value.z; w = value.w; } }
        public bvec4 xxyx { get => new bvec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 xxyy { get => new bvec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 xxyz { get => new bvec4(x, x, y, z); set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public bvec4 xxyw { get => new bvec4(x, x, y, w); set { x = value.x; x = value.y; y = value.z; w = value.w; } }
        public bvec4 xxzx { get => new bvec4(x, x, z, x); set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public bvec4 xxzy { get => new bvec4(x, x, z, y); set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public bvec4 xxzz { get => new bvec4(x, x, z, z); set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public bvec4 xxzw { get => new bvec4(x, x, z, w); set { x = value.x; x = value.y; z = value.z; w = value.w; } }
        public bvec4 xxwx { get => new bvec4(x, x, w, x); set { x = value.x; x = value.y; w = value.z; x = value.w; } }
        public bvec4 xxwy { get => new bvec4(x, x, w, y); set { x = value.x; x = value.y; w = value.z; y = value.w; } }
        public bvec4 xxwz { get => new bvec4(x, x, w, z); set { x = value.x; x = value.y; w = value.z; z = value.w; } }
        public bvec4 xxww { get => new bvec4(x, x, w, w); set { x = value.x; x = value.y; w = value.z; w = value.w; } }
        public bvec4 xyxx { get => new bvec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 xyxy { get => new bvec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 xyxz { get => new bvec4(x, y, x, z); set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public bvec4 xyxw { get => new bvec4(x, y, x, w); set { x = value.x; y = value.y; x = value.z; w = value.w; } }
        public bvec4 xyyx { get => new bvec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 xyyy { get => new bvec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public bvec4 xyyz { get => new bvec4(x, y, y, z); set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public bvec4 xyyw { get => new bvec4(x, y, y, w); set { x = value.x; y = value.y; y = value.z; w = value.w; } }
        public bvec4 xyzx { get => new bvec4(x, y, z, x); set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public bvec4 xyzy { get => new bvec4(x, y, z, y); set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public bvec4 xyzz { get => new bvec4(x, y, z, z); set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public bvec4 xyzw { get => new bvec4(x, y, z, w); set { x = value.x; y = value.y; z = value.z; w = value.w; } }
        public bvec4 xywx { get => new bvec4(x, y, w, x); set { x = value.x; y = value.y; w = value.z; x = value.w; } }
        public bvec4 xywy { get => new bvec4(x, y, w, y); set { x = value.x; y = value.y; w = value.z; y = value.w; } }
        public bvec4 xywz { get => new bvec4(x, y, w, z); set { x = value.x; y = value.y; w = value.z; z = value.w; } }
        public bvec4 xyww { get => new bvec4(x, y, w, w); set { x = value.x; y = value.y; w = value.z; w = value.w; } }
        public bvec4 xzxx { get => new bvec4(x, z, x, x); set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public bvec4 xzxy { get => new bvec4(x, z, x, y); set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public bvec4 xzxz { get => new bvec4(x, z, x, z); set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public bvec4 xzxw { get => new bvec4(x, z, x, w); set { x = value.x; z = value.y; x = value.z; w = value.w; } }
        public bvec4 xzyx { get => new bvec4(x, z, y, x); set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public bvec4 xzyy { get => new bvec4(x, z, y, y); set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public bvec4 xzyz { get => new bvec4(x, z, y, z); set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public bvec4 xzyw { get => new bvec4(x, z, y, w); set { x = value.x; z = value.y; y = value.z; w = value.w; } }
        public bvec4 xzzx { get => new bvec4(x, z, z, x); set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public bvec4 xzzy { get => new bvec4(x, z, z, y); set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public bvec4 xzzz { get => new bvec4(x, z, z, z); set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public bvec4 xzzw { get => new bvec4(x, z, z, w); set { x = value.x; z = value.y; z = value.z; w = value.w; } }
        public bvec4 xzwx { get => new bvec4(x, z, w, x); set { x = value.x; z = value.y; w = value.z; x = value.w; } }
        public bvec4 xzwy { get => new bvec4(x, z, w, y); set { x = value.x; z = value.y; w = value.z; y = value.w; } }
        public bvec4 xzwz { get => new bvec4(x, z, w, z); set { x = value.x; z = value.y; w = value.z; z = value.w; } }
        public bvec4 xzww { get => new bvec4(x, z, w, w); set { x = value.x; z = value.y; w = value.z; w = value.w; } }
        public bvec4 xwxx { get => new bvec4(x, w, x, x); set { x = value.x; w = value.y; x = value.z; x = value.w; } }
        public bvec4 xwxy { get => new bvec4(x, w, x, y); set { x = value.x; w = value.y; x = value.z; y = value.w; } }
        public bvec4 xwxz { get => new bvec4(x, w, x, z); set { x = value.x; w = value.y; x = value.z; z = value.w; } }
        public bvec4 xwxw { get => new bvec4(x, w, x, w); set { x = value.x; w = value.y; x = value.z; w = value.w; } }
        public bvec4 xwyx { get => new bvec4(x, w, y, x); set { x = value.x; w = value.y; y = value.z; x = value.w; } }
        public bvec4 xwyy { get => new bvec4(x, w, y, y); set { x = value.x; w = value.y; y = value.z; y = value.w; } }
        public bvec4 xwyz { get => new bvec4(x, w, y, z); set { x = value.x; w = value.y; y = value.z; z = value.w; } }
        public bvec4 xwyw { get => new bvec4(x, w, y, w); set { x = value.x; w = value.y; y = value.z; w = value.w; } }
        public bvec4 xwzx { get => new bvec4(x, w, z, x); set { x = value.x; w = value.y; z = value.z; x = value.w; } }
        public bvec4 xwzy { get => new bvec4(x, w, z, y); set { x = value.x; w = value.y; z = value.z; y = value.w; } }
        public bvec4 xwzz { get => new bvec4(x, w, z, z); set { x = value.x; w = value.y; z = value.z; z = value.w; } }
        public bvec4 xwzw { get => new bvec4(x, w, z, w); set { x = value.x; w = value.y; z = value.z; w = value.w; } }
        public bvec4 xwwx { get => new bvec4(x, w, w, x); set { x = value.x; w = value.y; w = value.z; x = value.w; } }
        public bvec4 xwwy { get => new bvec4(x, w, w, y); set { x = value.x; w = value.y; w = value.z; y = value.w; } }
        public bvec4 xwwz { get => new bvec4(x, w, w, z); set { x = value.x; w = value.y; w = value.z; z = value.w; } }
        public bvec4 xwww { get => new bvec4(x, w, w, w); set { x = value.x; w = value.y; w = value.z; w = value.w; } }
        public bvec4 yxxx { get => new bvec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 yxxy { get => new bvec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 yxxz { get => new bvec4(y, x, x, z); set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public bvec4 yxxw { get => new bvec4(y, x, x, w); set { y = value.x; x = value.y; x = value.z; w = value.w; } }
        public bvec4 yxyx { get => new bvec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 yxyy { get => new bvec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 yxyz { get => new bvec4(y, x, y, z); set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public bvec4 yxyw { get => new bvec4(y, x, y, w); set { y = value.x; x = value.y; y = value.z; w = value.w; } }
        public bvec4 yxzx { get => new bvec4(y, x, z, x); set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public bvec4 yxzy { get => new bvec4(y, x, z, y); set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public bvec4 yxzz { get => new bvec4(y, x, z, z); set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public bvec4 yxzw { get => new bvec4(y, x, z, w); set { y = value.x; x = value.y; z = value.z; w = value.w; } }
        public bvec4 yxwx { get => new bvec4(y, x, w, x); set { y = value.x; x = value.y; w = value.z; x = value.w; } }
        public bvec4 yxwy { get => new bvec4(y, x, w, y); set { y = value.x; x = value.y; w = value.z; y = value.w; } }
        public bvec4 yxwz { get => new bvec4(y, x, w, z); set { y = value.x; x = value.y; w = value.z; z = value.w; } }
        public bvec4 yxww { get => new bvec4(y, x, w, w); set { y = value.x; x = value.y; w = value.z; w = value.w; } }
        public bvec4 yyxx { get => new bvec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 yyxy { get => new bvec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 yyxz { get => new bvec4(y, y, x, z); set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public bvec4 yyxw { get => new bvec4(y, y, x, w); set { y = value.x; y = value.y; x = value.z; w = value.w; } }
        public bvec4 yyyx { get => new bvec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 yyyy { get => new bvec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public bvec4 yyyz { get => new bvec4(y, y, y, z); set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public bvec4 yyyw { get => new bvec4(y, y, y, w); set { y = value.x; y = value.y; y = value.z; w = value.w; } }
        public bvec4 yyzx { get => new bvec4(y, y, z, x); set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public bvec4 yyzy { get => new bvec4(y, y, z, y); set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public bvec4 yyzz { get => new bvec4(y, y, z, z); set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public bvec4 yyzw { get => new bvec4(y, y, z, w); set { y = value.x; y = value.y; z = value.z; w = value.w; } }
        public bvec4 yywx { get => new bvec4(y, y, w, x); set { y = value.x; y = value.y; w = value.z; x = value.w; } }
        public bvec4 yywy { get => new bvec4(y, y, w, y); set { y = value.x; y = value.y; w = value.z; y = value.w; } }
        public bvec4 yywz { get => new bvec4(y, y, w, z); set { y = value.x; y = value.y; w = value.z; z = value.w; } }
        public bvec4 yyww { get => new bvec4(y, y, w, w); set { y = value.x; y = value.y; w = value.z; w = value.w; } }
        public bvec4 yzxx { get => new bvec4(y, z, x, x); set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public bvec4 yzxy { get => new bvec4(y, z, x, y); set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public bvec4 yzxz { get => new bvec4(y, z, x, z); set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public bvec4 yzxw { get => new bvec4(y, z, x, w); set { y = value.x; z = value.y; x = value.z; w = value.w; } }
        public bvec4 yzyx { get => new bvec4(y, z, y, x); set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public bvec4 yzyy { get => new bvec4(y, z, y, y); set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public bvec4 yzyz { get => new bvec4(y, z, y, z); set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public bvec4 yzyw { get => new bvec4(y, z, y, w); set { y = value.x; z = value.y; y = value.z; w = value.w; } }
        public bvec4 yzzx { get => new bvec4(y, z, z, x); set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public bvec4 yzzy { get => new bvec4(y, z, z, y); set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public bvec4 yzzz { get => new bvec4(y, z, z, z); set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public bvec4 yzzw { get => new bvec4(y, z, z, w); set { y = value.x; z = value.y; z = value.z; w = value.w; } }
        public bvec4 yzwx { get => new bvec4(y, z, w, x); set { y = value.x; z = value.y; w = value.z; x = value.w; } }
        public bvec4 yzwy { get => new bvec4(y, z, w, y); set { y = value.x; z = value.y; w = value.z; y = value.w; } }
        public bvec4 yzwz { get => new bvec4(y, z, w, z); set { y = value.x; z = value.y; w = value.z; z = value.w; } }
        public bvec4 yzww { get => new bvec4(y, z, w, w); set { y = value.x; z = value.y; w = value.z; w = value.w; } }
        public bvec4 ywxx { get => new bvec4(y, w, x, x); set { y = value.x; w = value.y; x = value.z; x = value.w; } }
        public bvec4 ywxy { get => new bvec4(y, w, x, y); set { y = value.x; w = value.y; x = value.z; y = value.w; } }
        public bvec4 ywxz { get => new bvec4(y, w, x, z); set { y = value.x; w = value.y; x = value.z; z = value.w; } }
        public bvec4 ywxw { get => new bvec4(y, w, x, w); set { y = value.x; w = value.y; x = value.z; w = value.w; } }
        public bvec4 ywyx { get => new bvec4(y, w, y, x); set { y = value.x; w = value.y; y = value.z; x = value.w; } }
        public bvec4 ywyy { get => new bvec4(y, w, y, y); set { y = value.x; w = value.y; y = value.z; y = value.w; } }
        public bvec4 ywyz { get => new bvec4(y, w, y, z); set { y = value.x; w = value.y; y = value.z; z = value.w; } }
        public bvec4 ywyw { get => new bvec4(y, w, y, w); set { y = value.x; w = value.y; y = value.z; w = value.w; } }
        public bvec4 ywzx { get => new bvec4(y, w, z, x); set { y = value.x; w = value.y; z = value.z; x = value.w; } }
        public bvec4 ywzy { get => new bvec4(y, w, z, y); set { y = value.x; w = value.y; z = value.z; y = value.w; } }
        public bvec4 ywzz { get => new bvec4(y, w, z, z); set { y = value.x; w = value.y; z = value.z; z = value.w; } }
        public bvec4 ywzw { get => new bvec4(y, w, z, w); set { y = value.x; w = value.y; z = value.z; w = value.w; } }
        public bvec4 ywwx { get => new bvec4(y, w, w, x); set { y = value.x; w = value.y; w = value.z; x = value.w; } }
        public bvec4 ywwy { get => new bvec4(y, w, w, y); set { y = value.x; w = value.y; w = value.z; y = value.w; } }
        public bvec4 ywwz { get => new bvec4(y, w, w, z); set { y = value.x; w = value.y; w = value.z; z = value.w; } }
        public bvec4 ywww { get => new bvec4(y, w, w, w); set { y = value.x; w = value.y; w = value.z; w = value.w; } }
        public bvec4 zxxx { get => new bvec4(z, x, x, x); set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 zxxy { get => new bvec4(z, x, x, y); set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 zxxz { get => new bvec4(z, x, x, z); set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public bvec4 zxxw { get => new bvec4(z, x, x, w); set { z = value.x; x = value.y; x = value.z; w = value.w; } }
        public bvec4 zxyx { get => new bvec4(z, x, y, x); set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 zxyy { get => new bvec4(z, x, y, y); set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 zxyz { get => new bvec4(z, x, y, z); set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public bvec4 zxyw { get => new bvec4(z, x, y, w); set { z = value.x; x = value.y; y = value.z; w = value.w; } }
        public bvec4 zxzx { get => new bvec4(z, x, z, x); set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public bvec4 zxzy { get => new bvec4(z, x, z, y); set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public bvec4 zxzz { get => new bvec4(z, x, z, z); set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public bvec4 zxzw { get => new bvec4(z, x, z, w); set { z = value.x; x = value.y; z = value.z; w = value.w; } }
        public bvec4 zxwx { get => new bvec4(z, x, w, x); set { z = value.x; x = value.y; w = value.z; x = value.w; } }
        public bvec4 zxwy { get => new bvec4(z, x, w, y); set { z = value.x; x = value.y; w = value.z; y = value.w; } }
        public bvec4 zxwz { get => new bvec4(z, x, w, z); set { z = value.x; x = value.y; w = value.z; z = value.w; } }
        public bvec4 zxww { get => new bvec4(z, x, w, w); set { z = value.x; x = value.y; w = value.z; w = value.w; } }
        public bvec4 zyxx { get => new bvec4(z, y, x, x); set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 zyxy { get => new bvec4(z, y, x, y); set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 zyxz { get => new bvec4(z, y, x, z); set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public bvec4 zyxw { get => new bvec4(z, y, x, w); set { z = value.x; y = value.y; x = value.z; w = value.w; } }
        public bvec4 zyyx { get => new bvec4(z, y, y, x); set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 zyyy { get => new bvec4(z, y, y, y); set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public bvec4 zyyz { get => new bvec4(z, y, y, z); set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public bvec4 zyyw { get => new bvec4(z, y, y, w); set { z = value.x; y = value.y; y = value.z; w = value.w; } }
        public bvec4 zyzx { get => new bvec4(z, y, z, x); set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public bvec4 zyzy { get => new bvec4(z, y, z, y); set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public bvec4 zyzz { get => new bvec4(z, y, z, z); set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public bvec4 zyzw { get => new bvec4(z, y, z, w); set { z = value.x; y = value.y; z = value.z; w = value.w; } }
        public bvec4 zywx { get => new bvec4(z, y, w, x); set { z = value.x; y = value.y; w = value.z; x = value.w; } }
        public bvec4 zywy { get => new bvec4(z, y, w, y); set { z = value.x; y = value.y; w = value.z; y = value.w; } }
        public bvec4 zywz { get => new bvec4(z, y, w, z); set { z = value.x; y = value.y; w = value.z; z = value.w; } }
        public bvec4 zyww { get => new bvec4(z, y, w, w); set { z = value.x; y = value.y; w = value.z; w = value.w; } }
        public bvec4 zzxx { get => new bvec4(z, z, x, x); set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public bvec4 zzxy { get => new bvec4(z, z, x, y); set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public bvec4 zzxz { get => new bvec4(z, z, x, z); set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public bvec4 zzxw { get => new bvec4(z, z, x, w); set { z = value.x; z = value.y; x = value.z; w = value.w; } }
        public bvec4 zzyx { get => new bvec4(z, z, y, x); set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public bvec4 zzyy { get => new bvec4(z, z, y, y); set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public bvec4 zzyz { get => new bvec4(z, z, y, z); set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public bvec4 zzyw { get => new bvec4(z, z, y, w); set { z = value.x; z = value.y; y = value.z; w = value.w; } }
        public bvec4 zzzx { get => new bvec4(z, z, z, x); set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public bvec4 zzzy { get => new bvec4(z, z, z, y); set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public bvec4 zzzz { get => new bvec4(z, z, z, z); set { z = value.x; z = value.y; z = value.z; z = value.w; } }
        public bvec4 zzzw { get => new bvec4(z, z, z, w); set { z = value.x; z = value.y; z = value.z; w = value.w; } }
        public bvec4 zzwx { get => new bvec4(z, z, w, x); set { z = value.x; z = value.y; w = value.z; x = value.w; } }
        public bvec4 zzwy { get => new bvec4(z, z, w, y); set { z = value.x; z = value.y; w = value.z; y = value.w; } }
        public bvec4 zzwz { get => new bvec4(z, z, w, z); set { z = value.x; z = value.y; w = value.z; z = value.w; } }
        public bvec4 zzww { get => new bvec4(z, z, w, w); set { z = value.x; z = value.y; w = value.z; w = value.w; } }
        public bvec4 zwxx { get => new bvec4(z, w, x, x); set { z = value.x; w = value.y; x = value.z; x = value.w; } }
        public bvec4 zwxy { get => new bvec4(z, w, x, y); set { z = value.x; w = value.y; x = value.z; y = value.w; } }
        public bvec4 zwxz { get => new bvec4(z, w, x, z); set { z = value.x; w = value.y; x = value.z; z = value.w; } }
        public bvec4 zwxw { get => new bvec4(z, w, x, w); set { z = value.x; w = value.y; x = value.z; w = value.w; } }
        public bvec4 zwyx { get => new bvec4(z, w, y, x); set { z = value.x; w = value.y; y = value.z; x = value.w; } }
        public bvec4 zwyy { get => new bvec4(z, w, y, y); set { z = value.x; w = value.y; y = value.z; y = value.w; } }
        public bvec4 zwyz { get => new bvec4(z, w, y, z); set { z = value.x; w = value.y; y = value.z; z = value.w; } }
        public bvec4 zwyw { get => new bvec4(z, w, y, w); set { z = value.x; w = value.y; y = value.z; w = value.w; } }
        public bvec4 zwzx { get => new bvec4(z, w, z, x); set { z = value.x; w = value.y; z = value.z; x = value.w; } }
        public bvec4 zwzy { get => new bvec4(z, w, z, y); set { z = value.x; w = value.y; z = value.z; y = value.w; } }
        public bvec4 zwzz { get => new bvec4(z, w, z, z); set { z = value.x; w = value.y; z = value.z; z = value.w; } }
        public bvec4 zwzw { get => new bvec4(z, w, z, w); set { z = value.x; w = value.y; z = value.z; w = value.w; } }
        public bvec4 zwwx { get => new bvec4(z, w, w, x); set { z = value.x; w = value.y; w = value.z; x = value.w; } }
        public bvec4 zwwy { get => new bvec4(z, w, w, y); set { z = value.x; w = value.y; w = value.z; y = value.w; } }
        public bvec4 zwwz { get => new bvec4(z, w, w, z); set { z = value.x; w = value.y; w = value.z; z = value.w; } }
        public bvec4 zwww { get => new bvec4(z, w, w, w); set { z = value.x; w = value.y; w = value.z; w = value.w; } }
        public bvec4 wxxx { get => new bvec4(w, x, x, x); set { w = value.x; x = value.y; x = value.z; x = value.w; } }
        public bvec4 wxxy { get => new bvec4(w, x, x, y); set { w = value.x; x = value.y; x = value.z; y = value.w; } }
        public bvec4 wxxz { get => new bvec4(w, x, x, z); set { w = value.x; x = value.y; x = value.z; z = value.w; } }
        public bvec4 wxxw { get => new bvec4(w, x, x, w); set { w = value.x; x = value.y; x = value.z; w = value.w; } }
        public bvec4 wxyx { get => new bvec4(w, x, y, x); set { w = value.x; x = value.y; y = value.z; x = value.w; } }
        public bvec4 wxyy { get => new bvec4(w, x, y, y); set { w = value.x; x = value.y; y = value.z; y = value.w; } }
        public bvec4 wxyz { get => new bvec4(w, x, y, z); set { w = value.x; x = value.y; y = value.z; z = value.w; } }
        public bvec4 wxyw { get => new bvec4(w, x, y, w); set { w = value.x; x = value.y; y = value.z; w = value.w; } }
        public bvec4 wxzx { get => new bvec4(w, x, z, x); set { w = value.x; x = value.y; z = value.z; x = value.w; } }
        public bvec4 wxzy { get => new bvec4(w, x, z, y); set { w = value.x; x = value.y; z = value.z; y = value.w; } }
        public bvec4 wxzz { get => new bvec4(w, x, z, z); set { w = value.x; x = value.y; z = value.z; z = value.w; } }
        public bvec4 wxzw { get => new bvec4(w, x, z, w); set { w = value.x; x = value.y; z = value.z; w = value.w; } }
        public bvec4 wxwx { get => new bvec4(w, x, w, x); set { w = value.x; x = value.y; w = value.z; x = value.w; } }
        public bvec4 wxwy { get => new bvec4(w, x, w, y); set { w = value.x; x = value.y; w = value.z; y = value.w; } }
        public bvec4 wxwz { get => new bvec4(w, x, w, z); set { w = value.x; x = value.y; w = value.z; z = value.w; } }
        public bvec4 wxww { get => new bvec4(w, x, w, w); set { w = value.x; x = value.y; w = value.z; w = value.w; } }
        public bvec4 wyxx { get => new bvec4(w, y, x, x); set { w = value.x; y = value.y; x = value.z; x = value.w; } }
        public bvec4 wyxy { get => new bvec4(w, y, x, y); set { w = value.x; y = value.y; x = value.z; y = value.w; } }
        public bvec4 wyxz { get => new bvec4(w, y, x, z); set { w = value.x; y = value.y; x = value.z; z = value.w; } }
        public bvec4 wyxw { get => new bvec4(w, y, x, w); set { w = value.x; y = value.y; x = value.z; w = value.w; } }
        public bvec4 wyyx { get => new bvec4(w, y, y, x); set { w = value.x; y = value.y; y = value.z; x = value.w; } }
        public bvec4 wyyy { get => new bvec4(w, y, y, y); set { w = value.x; y = value.y; y = value.z; y = value.w; } }
        public bvec4 wyyz { get => new bvec4(w, y, y, z); set { w = value.x; y = value.y; y = value.z; z = value.w; } }
        public bvec4 wyyw { get => new bvec4(w, y, y, w); set { w = value.x; y = value.y; y = value.z; w = value.w; } }
        public bvec4 wyzx { get => new bvec4(w, y, z, x); set { w = value.x; y = value.y; z = value.z; x = value.w; } }
        public bvec4 wyzy { get => new bvec4(w, y, z, y); set { w = value.x; y = value.y; z = value.z; y = value.w; } }
        public bvec4 wyzz { get => new bvec4(w, y, z, z); set { w = value.x; y = value.y; z = value.z; z = value.w; } }
        public bvec4 wyzw { get => new bvec4(w, y, z, w); set { w = value.x; y = value.y; z = value.z; w = value.w; } }
        public bvec4 wywx { get => new bvec4(w, y, w, x); set { w = value.x; y = value.y; w = value.z; x = value.w; } }
        public bvec4 wywy { get => new bvec4(w, y, w, y); set { w = value.x; y = value.y; w = value.z; y = value.w; } }
        public bvec4 wywz { get => new bvec4(w, y, w, z); set { w = value.x; y = value.y; w = value.z; z = value.w; } }
        public bvec4 wyww { get => new bvec4(w, y, w, w); set { w = value.x; y = value.y; w = value.z; w = value.w; } }
        public bvec4 wzxx { get => new bvec4(w, z, x, x); set { w = value.x; z = value.y; x = value.z; x = value.w; } }
        public bvec4 wzxy { get => new bvec4(w, z, x, y); set { w = value.x; z = value.y; x = value.z; y = value.w; } }
        public bvec4 wzxz { get => new bvec4(w, z, x, z); set { w = value.x; z = value.y; x = value.z; z = value.w; } }
        public bvec4 wzxw { get => new bvec4(w, z, x, w); set { w = value.x; z = value.y; x = value.z; w = value.w; } }
        public bvec4 wzyx { get => new bvec4(w, z, y, x); set { w = value.x; z = value.y; y = value.z; x = value.w; } }
        public bvec4 wzyy { get => new bvec4(w, z, y, y); set { w = value.x; z = value.y; y = value.z; y = value.w; } }
        public bvec4 wzyz { get => new bvec4(w, z, y, z); set { w = value.x; z = value.y; y = value.z; z = value.w; } }
        public bvec4 wzyw { get => new bvec4(w, z, y, w); set { w = value.x; z = value.y; y = value.z; w = value.w; } }
        public bvec4 wzzx { get => new bvec4(w, z, z, x); set { w = value.x; z = value.y; z = value.z; x = value.w; } }
        public bvec4 wzzy { get => new bvec4(w, z, z, y); set { w = value.x; z = value.y; z = value.z; y = value.w; } }
        public bvec4 wzzz { get => new bvec4(w, z, z, z); set { w = value.x; z = value.y; z = value.z; z = value.w; } }
        public bvec4 wzzw { get => new bvec4(w, z, z, w); set { w = value.x; z = value.y; z = value.z; w = value.w; } }
        public bvec4 wzwx { get => new bvec4(w, z, w, x); set { w = value.x; z = value.y; w = value.z; x = value.w; } }
        public bvec4 wzwy { get => new bvec4(w, z, w, y); set { w = value.x; z = value.y; w = value.z; y = value.w; } }
        public bvec4 wzwz { get => new bvec4(w, z, w, z); set { w = value.x; z = value.y; w = value.z; z = value.w; } }
        public bvec4 wzww { get => new bvec4(w, z, w, w); set { w = value.x; z = value.y; w = value.z; w = value.w; } }
        public bvec4 wwxx { get => new bvec4(w, w, x, x); set { w = value.x; w = value.y; x = value.z; x = value.w; } }
        public bvec4 wwxy { get => new bvec4(w, w, x, y); set { w = value.x; w = value.y; x = value.z; y = value.w; } }
        public bvec4 wwxz { get => new bvec4(w, w, x, z); set { w = value.x; w = value.y; x = value.z; z = value.w; } }
        public bvec4 wwxw { get => new bvec4(w, w, x, w); set { w = value.x; w = value.y; x = value.z; w = value.w; } }
        public bvec4 wwyx { get => new bvec4(w, w, y, x); set { w = value.x; w = value.y; y = value.z; x = value.w; } }
        public bvec4 wwyy { get => new bvec4(w, w, y, y); set { w = value.x; w = value.y; y = value.z; y = value.w; } }
        public bvec4 wwyz { get => new bvec4(w, w, y, z); set { w = value.x; w = value.y; y = value.z; z = value.w; } }
        public bvec4 wwyw { get => new bvec4(w, w, y, w); set { w = value.x; w = value.y; y = value.z; w = value.w; } }
        public bvec4 wwzx { get => new bvec4(w, w, z, x); set { w = value.x; w = value.y; z = value.z; x = value.w; } }
        public bvec4 wwzy { get => new bvec4(w, w, z, y); set { w = value.x; w = value.y; z = value.z; y = value.w; } }
        public bvec4 wwzz { get => new bvec4(w, w, z, z); set { w = value.x; w = value.y; z = value.z; z = value.w; } }
        public bvec4 wwzw { get => new bvec4(w, w, z, w); set { w = value.x; w = value.y; z = value.z; w = value.w; } }
        public bvec4 wwwx { get => new bvec4(w, w, w, x); set { w = value.x; w = value.y; w = value.z; x = value.w; } }
        public bvec4 wwwy { get => new bvec4(w, w, w, y); set { w = value.x; w = value.y; w = value.z; y = value.w; } }
        public bvec4 wwwz { get => new bvec4(w, w, w, z); set { w = value.x; w = value.y; w = value.z; z = value.w; } }
        public bvec4 wwww { get => new bvec4(w, w, w, w); set { w = value.x; w = value.y; w = value.z; w = value.w; } }

        #endregion
    }

    public struct ivec4
    {
        public int x, y, z, w;
        public int this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
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
                    case 3: w = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ", " + w + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y, z, w };
        }

        #region vec4

        public ivec4(int a) : this(a, a, a, a) { }
        public ivec4(int[] v) : this(v[0], v[1], v[2], v[3]) { }
        public ivec4(int X, int Y, int Z, int W) { x = X; y = Y; z = Z; w = W; }
        public ivec4(ivec2 xy, int z, int w) : this(xy.x, xy.y, z, w) { }
        public ivec4(int x, ivec2 yz, int w) : this(x, yz.x, yz.y, w) { }
        public ivec4(int x, int y, ivec2 zw) : this(x, y, zw.x, zw.y) { }
        public ivec4(ivec2 xy, ivec2 zw) : this(xy.x, xy.y, zw.x, zw.y) { }
        public ivec4(ivec3 xyz, int w) : this(xyz.x, xyz.y, xyz.z, w) { }
        public ivec4(int x, ivec3 yzw) : this(x, yzw.x, yzw.y, yzw.z) { }
        public ivec4(byte[] data) : this((int[])data.To(typeof(int))) { }

        #endregion

        #region Generated
        
        public ivec2 xx { get => new ivec2(x, x); set { x = value.x; x = value.y; } }
        public ivec2 xy { get => new ivec2(x, y); set { x = value.x; y = value.y; } }
        public ivec2 xz { get => new ivec2(x, z); set { x = value.x; z = value.y; } }
        public ivec2 xw { get => new ivec2(x, w); set { x = value.x; w = value.y; } }
        public ivec2 yx { get => new ivec2(y, x); set { y = value.x; x = value.y; } }
        public ivec2 yy { get => new ivec2(y, y); set { y = value.x; y = value.y; } }
        public ivec2 yz { get => new ivec2(y, z); set { y = value.x; z = value.y; } }
        public ivec2 yw { get => new ivec2(y, w); set { y = value.x; w = value.y; } }
        public ivec2 zx { get => new ivec2(z, x); set { z = value.x; x = value.y; } }
        public ivec2 zy { get => new ivec2(z, y); set { z = value.x; y = value.y; } }
        public ivec2 zz { get => new ivec2(z, z); set { z = value.x; z = value.y; } }
        public ivec2 zw { get => new ivec2(z, w); set { z = value.x; w = value.y; } }
        public ivec2 wx { get => new ivec2(w, x); set { w = value.x; x = value.y; } }
        public ivec2 wy { get => new ivec2(w, y); set { w = value.x; y = value.y; } }
        public ivec2 wz { get => new ivec2(w, z); set { w = value.x; z = value.y; } }
        public ivec2 ww { get => new ivec2(w, w); set { w = value.x; w = value.y; } }
        public ivec3 xxx { get => new ivec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public ivec3 xxy { get => new ivec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public ivec3 xxz { get => new ivec3(x, x, z); set { x = value.x; x = value.y; z = value.z; } }
        public ivec3 xxw { get => new ivec3(x, x, w); set { x = value.x; x = value.y; w = value.z; } }
        public ivec3 xyx { get => new ivec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public ivec3 xyy { get => new ivec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public ivec3 xyz { get => new ivec3(x, y, z); set { x = value.x; y = value.y; z = value.z; } }
        public ivec3 xyw { get => new ivec3(x, y, w); set { x = value.x; y = value.y; w = value.z; } }
        public ivec3 xzx { get => new ivec3(x, z, x); set { x = value.x; z = value.y; x = value.z; } }
        public ivec3 xzy { get => new ivec3(x, z, y); set { x = value.x; z = value.y; y = value.z; } }
        public ivec3 xzz { get => new ivec3(x, z, z); set { x = value.x; z = value.y; z = value.z; } }
        public ivec3 xzw { get => new ivec3(x, z, w); set { x = value.x; z = value.y; w = value.z; } }
        public ivec3 xwx { get => new ivec3(x, w, x); set { x = value.x; w = value.y; x = value.z; } }
        public ivec3 xwy { get => new ivec3(x, w, y); set { x = value.x; w = value.y; y = value.z; } }
        public ivec3 xwz { get => new ivec3(x, w, z); set { x = value.x; w = value.y; z = value.z; } }
        public ivec3 xww { get => new ivec3(x, w, w); set { x = value.x; w = value.y; w = value.z; } }
        public ivec3 yxx { get => new ivec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public ivec3 yxy { get => new ivec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public ivec3 yxz { get => new ivec3(y, x, z); set { y = value.x; x = value.y; z = value.z; } }
        public ivec3 yxw { get => new ivec3(y, x, w); set { y = value.x; x = value.y; w = value.z; } }
        public ivec3 yyx { get => new ivec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public ivec3 yyy { get => new ivec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public ivec3 yyz { get => new ivec3(y, y, z); set { y = value.x; y = value.y; z = value.z; } }
        public ivec3 yyw { get => new ivec3(y, y, w); set { y = value.x; y = value.y; w = value.z; } }
        public ivec3 yzx { get => new ivec3(y, z, x); set { y = value.x; z = value.y; x = value.z; } }
        public ivec3 yzy { get => new ivec3(y, z, y); set { y = value.x; z = value.y; y = value.z; } }
        public ivec3 yzz { get => new ivec3(y, z, z); set { y = value.x; z = value.y; z = value.z; } }
        public ivec3 yzw { get => new ivec3(y, z, w); set { y = value.x; z = value.y; w = value.z; } }
        public ivec3 ywx { get => new ivec3(y, w, x); set { y = value.x; w = value.y; x = value.z; } }
        public ivec3 ywy { get => new ivec3(y, w, y); set { y = value.x; w = value.y; y = value.z; } }
        public ivec3 ywz { get => new ivec3(y, w, z); set { y = value.x; w = value.y; z = value.z; } }
        public ivec3 yww { get => new ivec3(y, w, w); set { y = value.x; w = value.y; w = value.z; } }
        public ivec3 zxx { get => new ivec3(z, x, x); set { z = value.x; x = value.y; x = value.z; } }
        public ivec3 zxy { get => new ivec3(z, x, y); set { z = value.x; x = value.y; y = value.z; } }
        public ivec3 zxz { get => new ivec3(z, x, z); set { z = value.x; x = value.y; z = value.z; } }
        public ivec3 zxw { get => new ivec3(z, x, w); set { z = value.x; x = value.y; w = value.z; } }
        public ivec3 zyx { get => new ivec3(z, y, x); set { z = value.x; y = value.y; x = value.z; } }
        public ivec3 zyy { get => new ivec3(z, y, y); set { z = value.x; y = value.y; y = value.z; } }
        public ivec3 zyz { get => new ivec3(z, y, z); set { z = value.x; y = value.y; z = value.z; } }
        public ivec3 zyw { get => new ivec3(z, y, w); set { z = value.x; y = value.y; w = value.z; } }
        public ivec3 zzx { get => new ivec3(z, z, x); set { z = value.x; z = value.y; x = value.z; } }
        public ivec3 zzy { get => new ivec3(z, z, y); set { z = value.x; z = value.y; y = value.z; } }
        public ivec3 zzz { get => new ivec3(z, z, z); set { z = value.x; z = value.y; z = value.z; } }
        public ivec3 zzw { get => new ivec3(z, z, w); set { z = value.x; z = value.y; w = value.z; } }
        public ivec3 zwx { get => new ivec3(z, w, x); set { z = value.x; w = value.y; x = value.z; } }
        public ivec3 zwy { get => new ivec3(z, w, y); set { z = value.x; w = value.y; y = value.z; } }
        public ivec3 zwz { get => new ivec3(z, w, z); set { z = value.x; w = value.y; z = value.z; } }
        public ivec3 zww { get => new ivec3(z, w, w); set { z = value.x; w = value.y; w = value.z; } }
        public ivec3 wxx { get => new ivec3(w, x, x); set { w = value.x; x = value.y; x = value.z; } }
        public ivec3 wxy { get => new ivec3(w, x, y); set { w = value.x; x = value.y; y = value.z; } }
        public ivec3 wxz { get => new ivec3(w, x, z); set { w = value.x; x = value.y; z = value.z; } }
        public ivec3 wxw { get => new ivec3(w, x, w); set { w = value.x; x = value.y; w = value.z; } }
        public ivec3 wyx { get => new ivec3(w, y, x); set { w = value.x; y = value.y; x = value.z; } }
        public ivec3 wyy { get => new ivec3(w, y, y); set { w = value.x; y = value.y; y = value.z; } }
        public ivec3 wyz { get => new ivec3(w, y, z); set { w = value.x; y = value.y; z = value.z; } }
        public ivec3 wyw { get => new ivec3(w, y, w); set { w = value.x; y = value.y; w = value.z; } }
        public ivec3 wzx { get => new ivec3(w, z, x); set { w = value.x; z = value.y; x = value.z; } }
        public ivec3 wzy { get => new ivec3(w, z, y); set { w = value.x; z = value.y; y = value.z; } }
        public ivec3 wzz { get => new ivec3(w, z, z); set { w = value.x; z = value.y; z = value.z; } }
        public ivec3 wzw { get => new ivec3(w, z, w); set { w = value.x; z = value.y; w = value.z; } }
        public ivec3 wwx { get => new ivec3(w, w, x); set { w = value.x; w = value.y; x = value.z; } }
        public ivec3 wwy { get => new ivec3(w, w, y); set { w = value.x; w = value.y; y = value.z; } }
        public ivec3 wwz { get => new ivec3(w, w, z); set { w = value.x; w = value.y; z = value.z; } }
        public ivec3 www { get => new ivec3(w, w, w); set { w = value.x; w = value.y; w = value.z; } }
        public ivec4 xxxx { get => new ivec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 xxxy { get => new ivec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 xxxz { get => new ivec4(x, x, x, z); set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public ivec4 xxxw { get => new ivec4(x, x, x, w); set { x = value.x; x = value.y; x = value.z; w = value.w; } }
        public ivec4 xxyx { get => new ivec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 xxyy { get => new ivec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 xxyz { get => new ivec4(x, x, y, z); set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public ivec4 xxyw { get => new ivec4(x, x, y, w); set { x = value.x; x = value.y; y = value.z; w = value.w; } }
        public ivec4 xxzx { get => new ivec4(x, x, z, x); set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public ivec4 xxzy { get => new ivec4(x, x, z, y); set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public ivec4 xxzz { get => new ivec4(x, x, z, z); set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public ivec4 xxzw { get => new ivec4(x, x, z, w); set { x = value.x; x = value.y; z = value.z; w = value.w; } }
        public ivec4 xxwx { get => new ivec4(x, x, w, x); set { x = value.x; x = value.y; w = value.z; x = value.w; } }
        public ivec4 xxwy { get => new ivec4(x, x, w, y); set { x = value.x; x = value.y; w = value.z; y = value.w; } }
        public ivec4 xxwz { get => new ivec4(x, x, w, z); set { x = value.x; x = value.y; w = value.z; z = value.w; } }
        public ivec4 xxww { get => new ivec4(x, x, w, w); set { x = value.x; x = value.y; w = value.z; w = value.w; } }
        public ivec4 xyxx { get => new ivec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 xyxy { get => new ivec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 xyxz { get => new ivec4(x, y, x, z); set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public ivec4 xyxw { get => new ivec4(x, y, x, w); set { x = value.x; y = value.y; x = value.z; w = value.w; } }
        public ivec4 xyyx { get => new ivec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 xyyy { get => new ivec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public ivec4 xyyz { get => new ivec4(x, y, y, z); set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public ivec4 xyyw { get => new ivec4(x, y, y, w); set { x = value.x; y = value.y; y = value.z; w = value.w; } }
        public ivec4 xyzx { get => new ivec4(x, y, z, x); set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public ivec4 xyzy { get => new ivec4(x, y, z, y); set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public ivec4 xyzz { get => new ivec4(x, y, z, z); set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public ivec4 xyzw { get => new ivec4(x, y, z, w); set { x = value.x; y = value.y; z = value.z; w = value.w; } }
        public ivec4 xywx { get => new ivec4(x, y, w, x); set { x = value.x; y = value.y; w = value.z; x = value.w; } }
        public ivec4 xywy { get => new ivec4(x, y, w, y); set { x = value.x; y = value.y; w = value.z; y = value.w; } }
        public ivec4 xywz { get => new ivec4(x, y, w, z); set { x = value.x; y = value.y; w = value.z; z = value.w; } }
        public ivec4 xyww { get => new ivec4(x, y, w, w); set { x = value.x; y = value.y; w = value.z; w = value.w; } }
        public ivec4 xzxx { get => new ivec4(x, z, x, x); set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public ivec4 xzxy { get => new ivec4(x, z, x, y); set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public ivec4 xzxz { get => new ivec4(x, z, x, z); set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public ivec4 xzxw { get => new ivec4(x, z, x, w); set { x = value.x; z = value.y; x = value.z; w = value.w; } }
        public ivec4 xzyx { get => new ivec4(x, z, y, x); set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public ivec4 xzyy { get => new ivec4(x, z, y, y); set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public ivec4 xzyz { get => new ivec4(x, z, y, z); set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public ivec4 xzyw { get => new ivec4(x, z, y, w); set { x = value.x; z = value.y; y = value.z; w = value.w; } }
        public ivec4 xzzx { get => new ivec4(x, z, z, x); set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public ivec4 xzzy { get => new ivec4(x, z, z, y); set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public ivec4 xzzz { get => new ivec4(x, z, z, z); set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public ivec4 xzzw { get => new ivec4(x, z, z, w); set { x = value.x; z = value.y; z = value.z; w = value.w; } }
        public ivec4 xzwx { get => new ivec4(x, z, w, x); set { x = value.x; z = value.y; w = value.z; x = value.w; } }
        public ivec4 xzwy { get => new ivec4(x, z, w, y); set { x = value.x; z = value.y; w = value.z; y = value.w; } }
        public ivec4 xzwz { get => new ivec4(x, z, w, z); set { x = value.x; z = value.y; w = value.z; z = value.w; } }
        public ivec4 xzww { get => new ivec4(x, z, w, w); set { x = value.x; z = value.y; w = value.z; w = value.w; } }
        public ivec4 xwxx { get => new ivec4(x, w, x, x); set { x = value.x; w = value.y; x = value.z; x = value.w; } }
        public ivec4 xwxy { get => new ivec4(x, w, x, y); set { x = value.x; w = value.y; x = value.z; y = value.w; } }
        public ivec4 xwxz { get => new ivec4(x, w, x, z); set { x = value.x; w = value.y; x = value.z; z = value.w; } }
        public ivec4 xwxw { get => new ivec4(x, w, x, w); set { x = value.x; w = value.y; x = value.z; w = value.w; } }
        public ivec4 xwyx { get => new ivec4(x, w, y, x); set { x = value.x; w = value.y; y = value.z; x = value.w; } }
        public ivec4 xwyy { get => new ivec4(x, w, y, y); set { x = value.x; w = value.y; y = value.z; y = value.w; } }
        public ivec4 xwyz { get => new ivec4(x, w, y, z); set { x = value.x; w = value.y; y = value.z; z = value.w; } }
        public ivec4 xwyw { get => new ivec4(x, w, y, w); set { x = value.x; w = value.y; y = value.z; w = value.w; } }
        public ivec4 xwzx { get => new ivec4(x, w, z, x); set { x = value.x; w = value.y; z = value.z; x = value.w; } }
        public ivec4 xwzy { get => new ivec4(x, w, z, y); set { x = value.x; w = value.y; z = value.z; y = value.w; } }
        public ivec4 xwzz { get => new ivec4(x, w, z, z); set { x = value.x; w = value.y; z = value.z; z = value.w; } }
        public ivec4 xwzw { get => new ivec4(x, w, z, w); set { x = value.x; w = value.y; z = value.z; w = value.w; } }
        public ivec4 xwwx { get => new ivec4(x, w, w, x); set { x = value.x; w = value.y; w = value.z; x = value.w; } }
        public ivec4 xwwy { get => new ivec4(x, w, w, y); set { x = value.x; w = value.y; w = value.z; y = value.w; } }
        public ivec4 xwwz { get => new ivec4(x, w, w, z); set { x = value.x; w = value.y; w = value.z; z = value.w; } }
        public ivec4 xwww { get => new ivec4(x, w, w, w); set { x = value.x; w = value.y; w = value.z; w = value.w; } }
        public ivec4 yxxx { get => new ivec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 yxxy { get => new ivec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 yxxz { get => new ivec4(y, x, x, z); set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public ivec4 yxxw { get => new ivec4(y, x, x, w); set { y = value.x; x = value.y; x = value.z; w = value.w; } }
        public ivec4 yxyx { get => new ivec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 yxyy { get => new ivec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 yxyz { get => new ivec4(y, x, y, z); set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public ivec4 yxyw { get => new ivec4(y, x, y, w); set { y = value.x; x = value.y; y = value.z; w = value.w; } }
        public ivec4 yxzx { get => new ivec4(y, x, z, x); set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public ivec4 yxzy { get => new ivec4(y, x, z, y); set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public ivec4 yxzz { get => new ivec4(y, x, z, z); set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public ivec4 yxzw { get => new ivec4(y, x, z, w); set { y = value.x; x = value.y; z = value.z; w = value.w; } }
        public ivec4 yxwx { get => new ivec4(y, x, w, x); set { y = value.x; x = value.y; w = value.z; x = value.w; } }
        public ivec4 yxwy { get => new ivec4(y, x, w, y); set { y = value.x; x = value.y; w = value.z; y = value.w; } }
        public ivec4 yxwz { get => new ivec4(y, x, w, z); set { y = value.x; x = value.y; w = value.z; z = value.w; } }
        public ivec4 yxww { get => new ivec4(y, x, w, w); set { y = value.x; x = value.y; w = value.z; w = value.w; } }
        public ivec4 yyxx { get => new ivec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 yyxy { get => new ivec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 yyxz { get => new ivec4(y, y, x, z); set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public ivec4 yyxw { get => new ivec4(y, y, x, w); set { y = value.x; y = value.y; x = value.z; w = value.w; } }
        public ivec4 yyyx { get => new ivec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 yyyy { get => new ivec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public ivec4 yyyz { get => new ivec4(y, y, y, z); set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public ivec4 yyyw { get => new ivec4(y, y, y, w); set { y = value.x; y = value.y; y = value.z; w = value.w; } }
        public ivec4 yyzx { get => new ivec4(y, y, z, x); set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public ivec4 yyzy { get => new ivec4(y, y, z, y); set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public ivec4 yyzz { get => new ivec4(y, y, z, z); set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public ivec4 yyzw { get => new ivec4(y, y, z, w); set { y = value.x; y = value.y; z = value.z; w = value.w; } }
        public ivec4 yywx { get => new ivec4(y, y, w, x); set { y = value.x; y = value.y; w = value.z; x = value.w; } }
        public ivec4 yywy { get => new ivec4(y, y, w, y); set { y = value.x; y = value.y; w = value.z; y = value.w; } }
        public ivec4 yywz { get => new ivec4(y, y, w, z); set { y = value.x; y = value.y; w = value.z; z = value.w; } }
        public ivec4 yyww { get => new ivec4(y, y, w, w); set { y = value.x; y = value.y; w = value.z; w = value.w; } }
        public ivec4 yzxx { get => new ivec4(y, z, x, x); set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public ivec4 yzxy { get => new ivec4(y, z, x, y); set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public ivec4 yzxz { get => new ivec4(y, z, x, z); set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public ivec4 yzxw { get => new ivec4(y, z, x, w); set { y = value.x; z = value.y; x = value.z; w = value.w; } }
        public ivec4 yzyx { get => new ivec4(y, z, y, x); set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public ivec4 yzyy { get => new ivec4(y, z, y, y); set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public ivec4 yzyz { get => new ivec4(y, z, y, z); set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public ivec4 yzyw { get => new ivec4(y, z, y, w); set { y = value.x; z = value.y; y = value.z; w = value.w; } }
        public ivec4 yzzx { get => new ivec4(y, z, z, x); set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public ivec4 yzzy { get => new ivec4(y, z, z, y); set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public ivec4 yzzz { get => new ivec4(y, z, z, z); set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public ivec4 yzzw { get => new ivec4(y, z, z, w); set { y = value.x; z = value.y; z = value.z; w = value.w; } }
        public ivec4 yzwx { get => new ivec4(y, z, w, x); set { y = value.x; z = value.y; w = value.z; x = value.w; } }
        public ivec4 yzwy { get => new ivec4(y, z, w, y); set { y = value.x; z = value.y; w = value.z; y = value.w; } }
        public ivec4 yzwz { get => new ivec4(y, z, w, z); set { y = value.x; z = value.y; w = value.z; z = value.w; } }
        public ivec4 yzww { get => new ivec4(y, z, w, w); set { y = value.x; z = value.y; w = value.z; w = value.w; } }
        public ivec4 ywxx { get => new ivec4(y, w, x, x); set { y = value.x; w = value.y; x = value.z; x = value.w; } }
        public ivec4 ywxy { get => new ivec4(y, w, x, y); set { y = value.x; w = value.y; x = value.z; y = value.w; } }
        public ivec4 ywxz { get => new ivec4(y, w, x, z); set { y = value.x; w = value.y; x = value.z; z = value.w; } }
        public ivec4 ywxw { get => new ivec4(y, w, x, w); set { y = value.x; w = value.y; x = value.z; w = value.w; } }
        public ivec4 ywyx { get => new ivec4(y, w, y, x); set { y = value.x; w = value.y; y = value.z; x = value.w; } }
        public ivec4 ywyy { get => new ivec4(y, w, y, y); set { y = value.x; w = value.y; y = value.z; y = value.w; } }
        public ivec4 ywyz { get => new ivec4(y, w, y, z); set { y = value.x; w = value.y; y = value.z; z = value.w; } }
        public ivec4 ywyw { get => new ivec4(y, w, y, w); set { y = value.x; w = value.y; y = value.z; w = value.w; } }
        public ivec4 ywzx { get => new ivec4(y, w, z, x); set { y = value.x; w = value.y; z = value.z; x = value.w; } }
        public ivec4 ywzy { get => new ivec4(y, w, z, y); set { y = value.x; w = value.y; z = value.z; y = value.w; } }
        public ivec4 ywzz { get => new ivec4(y, w, z, z); set { y = value.x; w = value.y; z = value.z; z = value.w; } }
        public ivec4 ywzw { get => new ivec4(y, w, z, w); set { y = value.x; w = value.y; z = value.z; w = value.w; } }
        public ivec4 ywwx { get => new ivec4(y, w, w, x); set { y = value.x; w = value.y; w = value.z; x = value.w; } }
        public ivec4 ywwy { get => new ivec4(y, w, w, y); set { y = value.x; w = value.y; w = value.z; y = value.w; } }
        public ivec4 ywwz { get => new ivec4(y, w, w, z); set { y = value.x; w = value.y; w = value.z; z = value.w; } }
        public ivec4 ywww { get => new ivec4(y, w, w, w); set { y = value.x; w = value.y; w = value.z; w = value.w; } }
        public ivec4 zxxx { get => new ivec4(z, x, x, x); set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 zxxy { get => new ivec4(z, x, x, y); set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 zxxz { get => new ivec4(z, x, x, z); set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public ivec4 zxxw { get => new ivec4(z, x, x, w); set { z = value.x; x = value.y; x = value.z; w = value.w; } }
        public ivec4 zxyx { get => new ivec4(z, x, y, x); set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 zxyy { get => new ivec4(z, x, y, y); set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 zxyz { get => new ivec4(z, x, y, z); set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public ivec4 zxyw { get => new ivec4(z, x, y, w); set { z = value.x; x = value.y; y = value.z; w = value.w; } }
        public ivec4 zxzx { get => new ivec4(z, x, z, x); set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public ivec4 zxzy { get => new ivec4(z, x, z, y); set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public ivec4 zxzz { get => new ivec4(z, x, z, z); set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public ivec4 zxzw { get => new ivec4(z, x, z, w); set { z = value.x; x = value.y; z = value.z; w = value.w; } }
        public ivec4 zxwx { get => new ivec4(z, x, w, x); set { z = value.x; x = value.y; w = value.z; x = value.w; } }
        public ivec4 zxwy { get => new ivec4(z, x, w, y); set { z = value.x; x = value.y; w = value.z; y = value.w; } }
        public ivec4 zxwz { get => new ivec4(z, x, w, z); set { z = value.x; x = value.y; w = value.z; z = value.w; } }
        public ivec4 zxww { get => new ivec4(z, x, w, w); set { z = value.x; x = value.y; w = value.z; w = value.w; } }
        public ivec4 zyxx { get => new ivec4(z, y, x, x); set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 zyxy { get => new ivec4(z, y, x, y); set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 zyxz { get => new ivec4(z, y, x, z); set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public ivec4 zyxw { get => new ivec4(z, y, x, w); set { z = value.x; y = value.y; x = value.z; w = value.w; } }
        public ivec4 zyyx { get => new ivec4(z, y, y, x); set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 zyyy { get => new ivec4(z, y, y, y); set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public ivec4 zyyz { get => new ivec4(z, y, y, z); set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public ivec4 zyyw { get => new ivec4(z, y, y, w); set { z = value.x; y = value.y; y = value.z; w = value.w; } }
        public ivec4 zyzx { get => new ivec4(z, y, z, x); set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public ivec4 zyzy { get => new ivec4(z, y, z, y); set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public ivec4 zyzz { get => new ivec4(z, y, z, z); set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public ivec4 zyzw { get => new ivec4(z, y, z, w); set { z = value.x; y = value.y; z = value.z; w = value.w; } }
        public ivec4 zywx { get => new ivec4(z, y, w, x); set { z = value.x; y = value.y; w = value.z; x = value.w; } }
        public ivec4 zywy { get => new ivec4(z, y, w, y); set { z = value.x; y = value.y; w = value.z; y = value.w; } }
        public ivec4 zywz { get => new ivec4(z, y, w, z); set { z = value.x; y = value.y; w = value.z; z = value.w; } }
        public ivec4 zyww { get => new ivec4(z, y, w, w); set { z = value.x; y = value.y; w = value.z; w = value.w; } }
        public ivec4 zzxx { get => new ivec4(z, z, x, x); set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public ivec4 zzxy { get => new ivec4(z, z, x, y); set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public ivec4 zzxz { get => new ivec4(z, z, x, z); set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public ivec4 zzxw { get => new ivec4(z, z, x, w); set { z = value.x; z = value.y; x = value.z; w = value.w; } }
        public ivec4 zzyx { get => new ivec4(z, z, y, x); set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public ivec4 zzyy { get => new ivec4(z, z, y, y); set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public ivec4 zzyz { get => new ivec4(z, z, y, z); set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public ivec4 zzyw { get => new ivec4(z, z, y, w); set { z = value.x; z = value.y; y = value.z; w = value.w; } }
        public ivec4 zzzx { get => new ivec4(z, z, z, x); set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public ivec4 zzzy { get => new ivec4(z, z, z, y); set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public ivec4 zzzz { get => new ivec4(z, z, z, z); set { z = value.x; z = value.y; z = value.z; z = value.w; } }
        public ivec4 zzzw { get => new ivec4(z, z, z, w); set { z = value.x; z = value.y; z = value.z; w = value.w; } }
        public ivec4 zzwx { get => new ivec4(z, z, w, x); set { z = value.x; z = value.y; w = value.z; x = value.w; } }
        public ivec4 zzwy { get => new ivec4(z, z, w, y); set { z = value.x; z = value.y; w = value.z; y = value.w; } }
        public ivec4 zzwz { get => new ivec4(z, z, w, z); set { z = value.x; z = value.y; w = value.z; z = value.w; } }
        public ivec4 zzww { get => new ivec4(z, z, w, w); set { z = value.x; z = value.y; w = value.z; w = value.w; } }
        public ivec4 zwxx { get => new ivec4(z, w, x, x); set { z = value.x; w = value.y; x = value.z; x = value.w; } }
        public ivec4 zwxy { get => new ivec4(z, w, x, y); set { z = value.x; w = value.y; x = value.z; y = value.w; } }
        public ivec4 zwxz { get => new ivec4(z, w, x, z); set { z = value.x; w = value.y; x = value.z; z = value.w; } }
        public ivec4 zwxw { get => new ivec4(z, w, x, w); set { z = value.x; w = value.y; x = value.z; w = value.w; } }
        public ivec4 zwyx { get => new ivec4(z, w, y, x); set { z = value.x; w = value.y; y = value.z; x = value.w; } }
        public ivec4 zwyy { get => new ivec4(z, w, y, y); set { z = value.x; w = value.y; y = value.z; y = value.w; } }
        public ivec4 zwyz { get => new ivec4(z, w, y, z); set { z = value.x; w = value.y; y = value.z; z = value.w; } }
        public ivec4 zwyw { get => new ivec4(z, w, y, w); set { z = value.x; w = value.y; y = value.z; w = value.w; } }
        public ivec4 zwzx { get => new ivec4(z, w, z, x); set { z = value.x; w = value.y; z = value.z; x = value.w; } }
        public ivec4 zwzy { get => new ivec4(z, w, z, y); set { z = value.x; w = value.y; z = value.z; y = value.w; } }
        public ivec4 zwzz { get => new ivec4(z, w, z, z); set { z = value.x; w = value.y; z = value.z; z = value.w; } }
        public ivec4 zwzw { get => new ivec4(z, w, z, w); set { z = value.x; w = value.y; z = value.z; w = value.w; } }
        public ivec4 zwwx { get => new ivec4(z, w, w, x); set { z = value.x; w = value.y; w = value.z; x = value.w; } }
        public ivec4 zwwy { get => new ivec4(z, w, w, y); set { z = value.x; w = value.y; w = value.z; y = value.w; } }
        public ivec4 zwwz { get => new ivec4(z, w, w, z); set { z = value.x; w = value.y; w = value.z; z = value.w; } }
        public ivec4 zwww { get => new ivec4(z, w, w, w); set { z = value.x; w = value.y; w = value.z; w = value.w; } }
        public ivec4 wxxx { get => new ivec4(w, x, x, x); set { w = value.x; x = value.y; x = value.z; x = value.w; } }
        public ivec4 wxxy { get => new ivec4(w, x, x, y); set { w = value.x; x = value.y; x = value.z; y = value.w; } }
        public ivec4 wxxz { get => new ivec4(w, x, x, z); set { w = value.x; x = value.y; x = value.z; z = value.w; } }
        public ivec4 wxxw { get => new ivec4(w, x, x, w); set { w = value.x; x = value.y; x = value.z; w = value.w; } }
        public ivec4 wxyx { get => new ivec4(w, x, y, x); set { w = value.x; x = value.y; y = value.z; x = value.w; } }
        public ivec4 wxyy { get => new ivec4(w, x, y, y); set { w = value.x; x = value.y; y = value.z; y = value.w; } }
        public ivec4 wxyz { get => new ivec4(w, x, y, z); set { w = value.x; x = value.y; y = value.z; z = value.w; } }
        public ivec4 wxyw { get => new ivec4(w, x, y, w); set { w = value.x; x = value.y; y = value.z; w = value.w; } }
        public ivec4 wxzx { get => new ivec4(w, x, z, x); set { w = value.x; x = value.y; z = value.z; x = value.w; } }
        public ivec4 wxzy { get => new ivec4(w, x, z, y); set { w = value.x; x = value.y; z = value.z; y = value.w; } }
        public ivec4 wxzz { get => new ivec4(w, x, z, z); set { w = value.x; x = value.y; z = value.z; z = value.w; } }
        public ivec4 wxzw { get => new ivec4(w, x, z, w); set { w = value.x; x = value.y; z = value.z; w = value.w; } }
        public ivec4 wxwx { get => new ivec4(w, x, w, x); set { w = value.x; x = value.y; w = value.z; x = value.w; } }
        public ivec4 wxwy { get => new ivec4(w, x, w, y); set { w = value.x; x = value.y; w = value.z; y = value.w; } }
        public ivec4 wxwz { get => new ivec4(w, x, w, z); set { w = value.x; x = value.y; w = value.z; z = value.w; } }
        public ivec4 wxww { get => new ivec4(w, x, w, w); set { w = value.x; x = value.y; w = value.z; w = value.w; } }
        public ivec4 wyxx { get => new ivec4(w, y, x, x); set { w = value.x; y = value.y; x = value.z; x = value.w; } }
        public ivec4 wyxy { get => new ivec4(w, y, x, y); set { w = value.x; y = value.y; x = value.z; y = value.w; } }
        public ivec4 wyxz { get => new ivec4(w, y, x, z); set { w = value.x; y = value.y; x = value.z; z = value.w; } }
        public ivec4 wyxw { get => new ivec4(w, y, x, w); set { w = value.x; y = value.y; x = value.z; w = value.w; } }
        public ivec4 wyyx { get => new ivec4(w, y, y, x); set { w = value.x; y = value.y; y = value.z; x = value.w; } }
        public ivec4 wyyy { get => new ivec4(w, y, y, y); set { w = value.x; y = value.y; y = value.z; y = value.w; } }
        public ivec4 wyyz { get => new ivec4(w, y, y, z); set { w = value.x; y = value.y; y = value.z; z = value.w; } }
        public ivec4 wyyw { get => new ivec4(w, y, y, w); set { w = value.x; y = value.y; y = value.z; w = value.w; } }
        public ivec4 wyzx { get => new ivec4(w, y, z, x); set { w = value.x; y = value.y; z = value.z; x = value.w; } }
        public ivec4 wyzy { get => new ivec4(w, y, z, y); set { w = value.x; y = value.y; z = value.z; y = value.w; } }
        public ivec4 wyzz { get => new ivec4(w, y, z, z); set { w = value.x; y = value.y; z = value.z; z = value.w; } }
        public ivec4 wyzw { get => new ivec4(w, y, z, w); set { w = value.x; y = value.y; z = value.z; w = value.w; } }
        public ivec4 wywx { get => new ivec4(w, y, w, x); set { w = value.x; y = value.y; w = value.z; x = value.w; } }
        public ivec4 wywy { get => new ivec4(w, y, w, y); set { w = value.x; y = value.y; w = value.z; y = value.w; } }
        public ivec4 wywz { get => new ivec4(w, y, w, z); set { w = value.x; y = value.y; w = value.z; z = value.w; } }
        public ivec4 wyww { get => new ivec4(w, y, w, w); set { w = value.x; y = value.y; w = value.z; w = value.w; } }
        public ivec4 wzxx { get => new ivec4(w, z, x, x); set { w = value.x; z = value.y; x = value.z; x = value.w; } }
        public ivec4 wzxy { get => new ivec4(w, z, x, y); set { w = value.x; z = value.y; x = value.z; y = value.w; } }
        public ivec4 wzxz { get => new ivec4(w, z, x, z); set { w = value.x; z = value.y; x = value.z; z = value.w; } }
        public ivec4 wzxw { get => new ivec4(w, z, x, w); set { w = value.x; z = value.y; x = value.z; w = value.w; } }
        public ivec4 wzyx { get => new ivec4(w, z, y, x); set { w = value.x; z = value.y; y = value.z; x = value.w; } }
        public ivec4 wzyy { get => new ivec4(w, z, y, y); set { w = value.x; z = value.y; y = value.z; y = value.w; } }
        public ivec4 wzyz { get => new ivec4(w, z, y, z); set { w = value.x; z = value.y; y = value.z; z = value.w; } }
        public ivec4 wzyw { get => new ivec4(w, z, y, w); set { w = value.x; z = value.y; y = value.z; w = value.w; } }
        public ivec4 wzzx { get => new ivec4(w, z, z, x); set { w = value.x; z = value.y; z = value.z; x = value.w; } }
        public ivec4 wzzy { get => new ivec4(w, z, z, y); set { w = value.x; z = value.y; z = value.z; y = value.w; } }
        public ivec4 wzzz { get => new ivec4(w, z, z, z); set { w = value.x; z = value.y; z = value.z; z = value.w; } }
        public ivec4 wzzw { get => new ivec4(w, z, z, w); set { w = value.x; z = value.y; z = value.z; w = value.w; } }
        public ivec4 wzwx { get => new ivec4(w, z, w, x); set { w = value.x; z = value.y; w = value.z; x = value.w; } }
        public ivec4 wzwy { get => new ivec4(w, z, w, y); set { w = value.x; z = value.y; w = value.z; y = value.w; } }
        public ivec4 wzwz { get => new ivec4(w, z, w, z); set { w = value.x; z = value.y; w = value.z; z = value.w; } }
        public ivec4 wzww { get => new ivec4(w, z, w, w); set { w = value.x; z = value.y; w = value.z; w = value.w; } }
        public ivec4 wwxx { get => new ivec4(w, w, x, x); set { w = value.x; w = value.y; x = value.z; x = value.w; } }
        public ivec4 wwxy { get => new ivec4(w, w, x, y); set { w = value.x; w = value.y; x = value.z; y = value.w; } }
        public ivec4 wwxz { get => new ivec4(w, w, x, z); set { w = value.x; w = value.y; x = value.z; z = value.w; } }
        public ivec4 wwxw { get => new ivec4(w, w, x, w); set { w = value.x; w = value.y; x = value.z; w = value.w; } }
        public ivec4 wwyx { get => new ivec4(w, w, y, x); set { w = value.x; w = value.y; y = value.z; x = value.w; } }
        public ivec4 wwyy { get => new ivec4(w, w, y, y); set { w = value.x; w = value.y; y = value.z; y = value.w; } }
        public ivec4 wwyz { get => new ivec4(w, w, y, z); set { w = value.x; w = value.y; y = value.z; z = value.w; } }
        public ivec4 wwyw { get => new ivec4(w, w, y, w); set { w = value.x; w = value.y; y = value.z; w = value.w; } }
        public ivec4 wwzx { get => new ivec4(w, w, z, x); set { w = value.x; w = value.y; z = value.z; x = value.w; } }
        public ivec4 wwzy { get => new ivec4(w, w, z, y); set { w = value.x; w = value.y; z = value.z; y = value.w; } }
        public ivec4 wwzz { get => new ivec4(w, w, z, z); set { w = value.x; w = value.y; z = value.z; z = value.w; } }
        public ivec4 wwzw { get => new ivec4(w, w, z, w); set { w = value.x; w = value.y; z = value.z; w = value.w; } }
        public ivec4 wwwx { get => new ivec4(w, w, w, x); set { w = value.x; w = value.y; w = value.z; x = value.w; } }
        public ivec4 wwwy { get => new ivec4(w, w, w, y); set { w = value.x; w = value.y; w = value.z; y = value.w; } }
        public ivec4 wwwz { get => new ivec4(w, w, w, z); set { w = value.x; w = value.y; w = value.z; z = value.w; } }
        public ivec4 wwww { get => new ivec4(w, w, w, w); set { w = value.x; w = value.y; w = value.z; w = value.w; } }

        #endregion
    }

    public struct uvec4
    {
        public uint x, y, z, w;
        public uint this[int i] {
            get {
                switch (i) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
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
                    case 3: w = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ", " + w + ")";
        }

        public Array ToArray()
        {
            return new[] { x, y, z, w };
        }

        #region vec4

        public uvec4(uint a) : this(a, a, a, a) { }
        public uvec4(uint[] v) : this(v[0], v[1], v[2], v[3]) { }
        public uvec4(uint X, uint Y, uint Z, uint W) { x = X; y = Y; z = Z; w = W; }
        public uvec4(uvec2 xy, uint z, uint w) : this(xy.x, xy.y, z, w) { }
        public uvec4(uint x, uvec2 yz, uint w) : this(x, yz.x, yz.y, w) { }
        public uvec4(uint x, uint y, uvec2 zw) : this(x, y, zw.x, zw.y) { }
        public uvec4(uvec2 xy, uvec2 zw) : this(xy.x, xy.y, zw.x, zw.y) { }
        public uvec4(uvec3 xyz, uint w) : this(xyz.x, xyz.y, xyz.z, w) { }
        public uvec4(uint x, uvec3 yzw) : this(x, yzw.x, yzw.y, yzw.z) { }
        public uvec4(byte[] data) : this((uint[])data.To(typeof(uint))) { }

        #endregion

        #region Generated
        
        public uvec2 xx { get => new uvec2(x, x); set { x = value.x; x = value.y; } }
        public uvec2 xy { get => new uvec2(x, y); set { x = value.x; y = value.y; } }
        public uvec2 xz { get => new uvec2(x, z); set { x = value.x; z = value.y; } }
        public uvec2 xw { get => new uvec2(x, w); set { x = value.x; w = value.y; } }
        public uvec2 yx { get => new uvec2(y, x); set { y = value.x; x = value.y; } }
        public uvec2 yy { get => new uvec2(y, y); set { y = value.x; y = value.y; } }
        public uvec2 yz { get => new uvec2(y, z); set { y = value.x; z = value.y; } }
        public uvec2 yw { get => new uvec2(y, w); set { y = value.x; w = value.y; } }
        public uvec2 zx { get => new uvec2(z, x); set { z = value.x; x = value.y; } }
        public uvec2 zy { get => new uvec2(z, y); set { z = value.x; y = value.y; } }
        public uvec2 zz { get => new uvec2(z, z); set { z = value.x; z = value.y; } }
        public uvec2 zw { get => new uvec2(z, w); set { z = value.x; w = value.y; } }
        public uvec2 wx { get => new uvec2(w, x); set { w = value.x; x = value.y; } }
        public uvec2 wy { get => new uvec2(w, y); set { w = value.x; y = value.y; } }
        public uvec2 wz { get => new uvec2(w, z); set { w = value.x; z = value.y; } }
        public uvec2 ww { get => new uvec2(w, w); set { w = value.x; w = value.y; } }
        public uvec3 xxx { get => new uvec3(x, x, x); set { x = value.x; x = value.y; x = value.z; } }
        public uvec3 xxy { get => new uvec3(x, x, y); set { x = value.x; x = value.y; y = value.z; } }
        public uvec3 xxz { get => new uvec3(x, x, z); set { x = value.x; x = value.y; z = value.z; } }
        public uvec3 xxw { get => new uvec3(x, x, w); set { x = value.x; x = value.y; w = value.z; } }
        public uvec3 xyx { get => new uvec3(x, y, x); set { x = value.x; y = value.y; x = value.z; } }
        public uvec3 xyy { get => new uvec3(x, y, y); set { x = value.x; y = value.y; y = value.z; } }
        public uvec3 xyz { get => new uvec3(x, y, z); set { x = value.x; y = value.y; z = value.z; } }
        public uvec3 xyw { get => new uvec3(x, y, w); set { x = value.x; y = value.y; w = value.z; } }
        public uvec3 xzx { get => new uvec3(x, z, x); set { x = value.x; z = value.y; x = value.z; } }
        public uvec3 xzy { get => new uvec3(x, z, y); set { x = value.x; z = value.y; y = value.z; } }
        public uvec3 xzz { get => new uvec3(x, z, z); set { x = value.x; z = value.y; z = value.z; } }
        public uvec3 xzw { get => new uvec3(x, z, w); set { x = value.x; z = value.y; w = value.z; } }
        public uvec3 xwx { get => new uvec3(x, w, x); set { x = value.x; w = value.y; x = value.z; } }
        public uvec3 xwy { get => new uvec3(x, w, y); set { x = value.x; w = value.y; y = value.z; } }
        public uvec3 xwz { get => new uvec3(x, w, z); set { x = value.x; w = value.y; z = value.z; } }
        public uvec3 xww { get => new uvec3(x, w, w); set { x = value.x; w = value.y; w = value.z; } }
        public uvec3 yxx { get => new uvec3(y, x, x); set { y = value.x; x = value.y; x = value.z; } }
        public uvec3 yxy { get => new uvec3(y, x, y); set { y = value.x; x = value.y; y = value.z; } }
        public uvec3 yxz { get => new uvec3(y, x, z); set { y = value.x; x = value.y; z = value.z; } }
        public uvec3 yxw { get => new uvec3(y, x, w); set { y = value.x; x = value.y; w = value.z; } }
        public uvec3 yyx { get => new uvec3(y, y, x); set { y = value.x; y = value.y; x = value.z; } }
        public uvec3 yyy { get => new uvec3(y, y, y); set { y = value.x; y = value.y; y = value.z; } }
        public uvec3 yyz { get => new uvec3(y, y, z); set { y = value.x; y = value.y; z = value.z; } }
        public uvec3 yyw { get => new uvec3(y, y, w); set { y = value.x; y = value.y; w = value.z; } }
        public uvec3 yzx { get => new uvec3(y, z, x); set { y = value.x; z = value.y; x = value.z; } }
        public uvec3 yzy { get => new uvec3(y, z, y); set { y = value.x; z = value.y; y = value.z; } }
        public uvec3 yzz { get => new uvec3(y, z, z); set { y = value.x; z = value.y; z = value.z; } }
        public uvec3 yzw { get => new uvec3(y, z, w); set { y = value.x; z = value.y; w = value.z; } }
        public uvec3 ywx { get => new uvec3(y, w, x); set { y = value.x; w = value.y; x = value.z; } }
        public uvec3 ywy { get => new uvec3(y, w, y); set { y = value.x; w = value.y; y = value.z; } }
        public uvec3 ywz { get => new uvec3(y, w, z); set { y = value.x; w = value.y; z = value.z; } }
        public uvec3 yww { get => new uvec3(y, w, w); set { y = value.x; w = value.y; w = value.z; } }
        public uvec3 zxx { get => new uvec3(z, x, x); set { z = value.x; x = value.y; x = value.z; } }
        public uvec3 zxy { get => new uvec3(z, x, y); set { z = value.x; x = value.y; y = value.z; } }
        public uvec3 zxz { get => new uvec3(z, x, z); set { z = value.x; x = value.y; z = value.z; } }
        public uvec3 zxw { get => new uvec3(z, x, w); set { z = value.x; x = value.y; w = value.z; } }
        public uvec3 zyx { get => new uvec3(z, y, x); set { z = value.x; y = value.y; x = value.z; } }
        public uvec3 zyy { get => new uvec3(z, y, y); set { z = value.x; y = value.y; y = value.z; } }
        public uvec3 zyz { get => new uvec3(z, y, z); set { z = value.x; y = value.y; z = value.z; } }
        public uvec3 zyw { get => new uvec3(z, y, w); set { z = value.x; y = value.y; w = value.z; } }
        public uvec3 zzx { get => new uvec3(z, z, x); set { z = value.x; z = value.y; x = value.z; } }
        public uvec3 zzy { get => new uvec3(z, z, y); set { z = value.x; z = value.y; y = value.z; } }
        public uvec3 zzz { get => new uvec3(z, z, z); set { z = value.x; z = value.y; z = value.z; } }
        public uvec3 zzw { get => new uvec3(z, z, w); set { z = value.x; z = value.y; w = value.z; } }
        public uvec3 zwx { get => new uvec3(z, w, x); set { z = value.x; w = value.y; x = value.z; } }
        public uvec3 zwy { get => new uvec3(z, w, y); set { z = value.x; w = value.y; y = value.z; } }
        public uvec3 zwz { get => new uvec3(z, w, z); set { z = value.x; w = value.y; z = value.z; } }
        public uvec3 zww { get => new uvec3(z, w, w); set { z = value.x; w = value.y; w = value.z; } }
        public uvec3 wxx { get => new uvec3(w, x, x); set { w = value.x; x = value.y; x = value.z; } }
        public uvec3 wxy { get => new uvec3(w, x, y); set { w = value.x; x = value.y; y = value.z; } }
        public uvec3 wxz { get => new uvec3(w, x, z); set { w = value.x; x = value.y; z = value.z; } }
        public uvec3 wxw { get => new uvec3(w, x, w); set { w = value.x; x = value.y; w = value.z; } }
        public uvec3 wyx { get => new uvec3(w, y, x); set { w = value.x; y = value.y; x = value.z; } }
        public uvec3 wyy { get => new uvec3(w, y, y); set { w = value.x; y = value.y; y = value.z; } }
        public uvec3 wyz { get => new uvec3(w, y, z); set { w = value.x; y = value.y; z = value.z; } }
        public uvec3 wyw { get => new uvec3(w, y, w); set { w = value.x; y = value.y; w = value.z; } }
        public uvec3 wzx { get => new uvec3(w, z, x); set { w = value.x; z = value.y; x = value.z; } }
        public uvec3 wzy { get => new uvec3(w, z, y); set { w = value.x; z = value.y; y = value.z; } }
        public uvec3 wzz { get => new uvec3(w, z, z); set { w = value.x; z = value.y; z = value.z; } }
        public uvec3 wzw { get => new uvec3(w, z, w); set { w = value.x; z = value.y; w = value.z; } }
        public uvec3 wwx { get => new uvec3(w, w, x); set { w = value.x; w = value.y; x = value.z; } }
        public uvec3 wwy { get => new uvec3(w, w, y); set { w = value.x; w = value.y; y = value.z; } }
        public uvec3 wwz { get => new uvec3(w, w, z); set { w = value.x; w = value.y; z = value.z; } }
        public uvec3 www { get => new uvec3(w, w, w); set { w = value.x; w = value.y; w = value.z; } }
        public uvec4 xxxx { get => new uvec4(x, x, x, x); set { x = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 xxxy { get => new uvec4(x, x, x, y); set { x = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 xxxz { get => new uvec4(x, x, x, z); set { x = value.x; x = value.y; x = value.z; z = value.w; } }
        public uvec4 xxxw { get => new uvec4(x, x, x, w); set { x = value.x; x = value.y; x = value.z; w = value.w; } }
        public uvec4 xxyx { get => new uvec4(x, x, y, x); set { x = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 xxyy { get => new uvec4(x, x, y, y); set { x = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 xxyz { get => new uvec4(x, x, y, z); set { x = value.x; x = value.y; y = value.z; z = value.w; } }
        public uvec4 xxyw { get => new uvec4(x, x, y, w); set { x = value.x; x = value.y; y = value.z; w = value.w; } }
        public uvec4 xxzx { get => new uvec4(x, x, z, x); set { x = value.x; x = value.y; z = value.z; x = value.w; } }
        public uvec4 xxzy { get => new uvec4(x, x, z, y); set { x = value.x; x = value.y; z = value.z; y = value.w; } }
        public uvec4 xxzz { get => new uvec4(x, x, z, z); set { x = value.x; x = value.y; z = value.z; z = value.w; } }
        public uvec4 xxzw { get => new uvec4(x, x, z, w); set { x = value.x; x = value.y; z = value.z; w = value.w; } }
        public uvec4 xxwx { get => new uvec4(x, x, w, x); set { x = value.x; x = value.y; w = value.z; x = value.w; } }
        public uvec4 xxwy { get => new uvec4(x, x, w, y); set { x = value.x; x = value.y; w = value.z; y = value.w; } }
        public uvec4 xxwz { get => new uvec4(x, x, w, z); set { x = value.x; x = value.y; w = value.z; z = value.w; } }
        public uvec4 xxww { get => new uvec4(x, x, w, w); set { x = value.x; x = value.y; w = value.z; w = value.w; } }
        public uvec4 xyxx { get => new uvec4(x, y, x, x); set { x = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 xyxy { get => new uvec4(x, y, x, y); set { x = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 xyxz { get => new uvec4(x, y, x, z); set { x = value.x; y = value.y; x = value.z; z = value.w; } }
        public uvec4 xyxw { get => new uvec4(x, y, x, w); set { x = value.x; y = value.y; x = value.z; w = value.w; } }
        public uvec4 xyyx { get => new uvec4(x, y, y, x); set { x = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 xyyy { get => new uvec4(x, y, y, y); set { x = value.x; y = value.y; y = value.z; y = value.w; } }
        public uvec4 xyyz { get => new uvec4(x, y, y, z); set { x = value.x; y = value.y; y = value.z; z = value.w; } }
        public uvec4 xyyw { get => new uvec4(x, y, y, w); set { x = value.x; y = value.y; y = value.z; w = value.w; } }
        public uvec4 xyzx { get => new uvec4(x, y, z, x); set { x = value.x; y = value.y; z = value.z; x = value.w; } }
        public uvec4 xyzy { get => new uvec4(x, y, z, y); set { x = value.x; y = value.y; z = value.z; y = value.w; } }
        public uvec4 xyzz { get => new uvec4(x, y, z, z); set { x = value.x; y = value.y; z = value.z; z = value.w; } }
        public uvec4 xyzw { get => new uvec4(x, y, z, w); set { x = value.x; y = value.y; z = value.z; w = value.w; } }
        public uvec4 xywx { get => new uvec4(x, y, w, x); set { x = value.x; y = value.y; w = value.z; x = value.w; } }
        public uvec4 xywy { get => new uvec4(x, y, w, y); set { x = value.x; y = value.y; w = value.z; y = value.w; } }
        public uvec4 xywz { get => new uvec4(x, y, w, z); set { x = value.x; y = value.y; w = value.z; z = value.w; } }
        public uvec4 xyww { get => new uvec4(x, y, w, w); set { x = value.x; y = value.y; w = value.z; w = value.w; } }
        public uvec4 xzxx { get => new uvec4(x, z, x, x); set { x = value.x; z = value.y; x = value.z; x = value.w; } }
        public uvec4 xzxy { get => new uvec4(x, z, x, y); set { x = value.x; z = value.y; x = value.z; y = value.w; } }
        public uvec4 xzxz { get => new uvec4(x, z, x, z); set { x = value.x; z = value.y; x = value.z; z = value.w; } }
        public uvec4 xzxw { get => new uvec4(x, z, x, w); set { x = value.x; z = value.y; x = value.z; w = value.w; } }
        public uvec4 xzyx { get => new uvec4(x, z, y, x); set { x = value.x; z = value.y; y = value.z; x = value.w; } }
        public uvec4 xzyy { get => new uvec4(x, z, y, y); set { x = value.x; z = value.y; y = value.z; y = value.w; } }
        public uvec4 xzyz { get => new uvec4(x, z, y, z); set { x = value.x; z = value.y; y = value.z; z = value.w; } }
        public uvec4 xzyw { get => new uvec4(x, z, y, w); set { x = value.x; z = value.y; y = value.z; w = value.w; } }
        public uvec4 xzzx { get => new uvec4(x, z, z, x); set { x = value.x; z = value.y; z = value.z; x = value.w; } }
        public uvec4 xzzy { get => new uvec4(x, z, z, y); set { x = value.x; z = value.y; z = value.z; y = value.w; } }
        public uvec4 xzzz { get => new uvec4(x, z, z, z); set { x = value.x; z = value.y; z = value.z; z = value.w; } }
        public uvec4 xzzw { get => new uvec4(x, z, z, w); set { x = value.x; z = value.y; z = value.z; w = value.w; } }
        public uvec4 xzwx { get => new uvec4(x, z, w, x); set { x = value.x; z = value.y; w = value.z; x = value.w; } }
        public uvec4 xzwy { get => new uvec4(x, z, w, y); set { x = value.x; z = value.y; w = value.z; y = value.w; } }
        public uvec4 xzwz { get => new uvec4(x, z, w, z); set { x = value.x; z = value.y; w = value.z; z = value.w; } }
        public uvec4 xzww { get => new uvec4(x, z, w, w); set { x = value.x; z = value.y; w = value.z; w = value.w; } }
        public uvec4 xwxx { get => new uvec4(x, w, x, x); set { x = value.x; w = value.y; x = value.z; x = value.w; } }
        public uvec4 xwxy { get => new uvec4(x, w, x, y); set { x = value.x; w = value.y; x = value.z; y = value.w; } }
        public uvec4 xwxz { get => new uvec4(x, w, x, z); set { x = value.x; w = value.y; x = value.z; z = value.w; } }
        public uvec4 xwxw { get => new uvec4(x, w, x, w); set { x = value.x; w = value.y; x = value.z; w = value.w; } }
        public uvec4 xwyx { get => new uvec4(x, w, y, x); set { x = value.x; w = value.y; y = value.z; x = value.w; } }
        public uvec4 xwyy { get => new uvec4(x, w, y, y); set { x = value.x; w = value.y; y = value.z; y = value.w; } }
        public uvec4 xwyz { get => new uvec4(x, w, y, z); set { x = value.x; w = value.y; y = value.z; z = value.w; } }
        public uvec4 xwyw { get => new uvec4(x, w, y, w); set { x = value.x; w = value.y; y = value.z; w = value.w; } }
        public uvec4 xwzx { get => new uvec4(x, w, z, x); set { x = value.x; w = value.y; z = value.z; x = value.w; } }
        public uvec4 xwzy { get => new uvec4(x, w, z, y); set { x = value.x; w = value.y; z = value.z; y = value.w; } }
        public uvec4 xwzz { get => new uvec4(x, w, z, z); set { x = value.x; w = value.y; z = value.z; z = value.w; } }
        public uvec4 xwzw { get => new uvec4(x, w, z, w); set { x = value.x; w = value.y; z = value.z; w = value.w; } }
        public uvec4 xwwx { get => new uvec4(x, w, w, x); set { x = value.x; w = value.y; w = value.z; x = value.w; } }
        public uvec4 xwwy { get => new uvec4(x, w, w, y); set { x = value.x; w = value.y; w = value.z; y = value.w; } }
        public uvec4 xwwz { get => new uvec4(x, w, w, z); set { x = value.x; w = value.y; w = value.z; z = value.w; } }
        public uvec4 xwww { get => new uvec4(x, w, w, w); set { x = value.x; w = value.y; w = value.z; w = value.w; } }
        public uvec4 yxxx { get => new uvec4(y, x, x, x); set { y = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 yxxy { get => new uvec4(y, x, x, y); set { y = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 yxxz { get => new uvec4(y, x, x, z); set { y = value.x; x = value.y; x = value.z; z = value.w; } }
        public uvec4 yxxw { get => new uvec4(y, x, x, w); set { y = value.x; x = value.y; x = value.z; w = value.w; } }
        public uvec4 yxyx { get => new uvec4(y, x, y, x); set { y = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 yxyy { get => new uvec4(y, x, y, y); set { y = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 yxyz { get => new uvec4(y, x, y, z); set { y = value.x; x = value.y; y = value.z; z = value.w; } }
        public uvec4 yxyw { get => new uvec4(y, x, y, w); set { y = value.x; x = value.y; y = value.z; w = value.w; } }
        public uvec4 yxzx { get => new uvec4(y, x, z, x); set { y = value.x; x = value.y; z = value.z; x = value.w; } }
        public uvec4 yxzy { get => new uvec4(y, x, z, y); set { y = value.x; x = value.y; z = value.z; y = value.w; } }
        public uvec4 yxzz { get => new uvec4(y, x, z, z); set { y = value.x; x = value.y; z = value.z; z = value.w; } }
        public uvec4 yxzw { get => new uvec4(y, x, z, w); set { y = value.x; x = value.y; z = value.z; w = value.w; } }
        public uvec4 yxwx { get => new uvec4(y, x, w, x); set { y = value.x; x = value.y; w = value.z; x = value.w; } }
        public uvec4 yxwy { get => new uvec4(y, x, w, y); set { y = value.x; x = value.y; w = value.z; y = value.w; } }
        public uvec4 yxwz { get => new uvec4(y, x, w, z); set { y = value.x; x = value.y; w = value.z; z = value.w; } }
        public uvec4 yxww { get => new uvec4(y, x, w, w); set { y = value.x; x = value.y; w = value.z; w = value.w; } }
        public uvec4 yyxx { get => new uvec4(y, y, x, x); set { y = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 yyxy { get => new uvec4(y, y, x, y); set { y = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 yyxz { get => new uvec4(y, y, x, z); set { y = value.x; y = value.y; x = value.z; z = value.w; } }
        public uvec4 yyxw { get => new uvec4(y, y, x, w); set { y = value.x; y = value.y; x = value.z; w = value.w; } }
        public uvec4 yyyx { get => new uvec4(y, y, y, x); set { y = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 yyyy { get => new uvec4(y, y, y, y); set { y = value.x; y = value.y; y = value.z; y = value.w; } }
        public uvec4 yyyz { get => new uvec4(y, y, y, z); set { y = value.x; y = value.y; y = value.z; z = value.w; } }
        public uvec4 yyyw { get => new uvec4(y, y, y, w); set { y = value.x; y = value.y; y = value.z; w = value.w; } }
        public uvec4 yyzx { get => new uvec4(y, y, z, x); set { y = value.x; y = value.y; z = value.z; x = value.w; } }
        public uvec4 yyzy { get => new uvec4(y, y, z, y); set { y = value.x; y = value.y; z = value.z; y = value.w; } }
        public uvec4 yyzz { get => new uvec4(y, y, z, z); set { y = value.x; y = value.y; z = value.z; z = value.w; } }
        public uvec4 yyzw { get => new uvec4(y, y, z, w); set { y = value.x; y = value.y; z = value.z; w = value.w; } }
        public uvec4 yywx { get => new uvec4(y, y, w, x); set { y = value.x; y = value.y; w = value.z; x = value.w; } }
        public uvec4 yywy { get => new uvec4(y, y, w, y); set { y = value.x; y = value.y; w = value.z; y = value.w; } }
        public uvec4 yywz { get => new uvec4(y, y, w, z); set { y = value.x; y = value.y; w = value.z; z = value.w; } }
        public uvec4 yyww { get => new uvec4(y, y, w, w); set { y = value.x; y = value.y; w = value.z; w = value.w; } }
        public uvec4 yzxx { get => new uvec4(y, z, x, x); set { y = value.x; z = value.y; x = value.z; x = value.w; } }
        public uvec4 yzxy { get => new uvec4(y, z, x, y); set { y = value.x; z = value.y; x = value.z; y = value.w; } }
        public uvec4 yzxz { get => new uvec4(y, z, x, z); set { y = value.x; z = value.y; x = value.z; z = value.w; } }
        public uvec4 yzxw { get => new uvec4(y, z, x, w); set { y = value.x; z = value.y; x = value.z; w = value.w; } }
        public uvec4 yzyx { get => new uvec4(y, z, y, x); set { y = value.x; z = value.y; y = value.z; x = value.w; } }
        public uvec4 yzyy { get => new uvec4(y, z, y, y); set { y = value.x; z = value.y; y = value.z; y = value.w; } }
        public uvec4 yzyz { get => new uvec4(y, z, y, z); set { y = value.x; z = value.y; y = value.z; z = value.w; } }
        public uvec4 yzyw { get => new uvec4(y, z, y, w); set { y = value.x; z = value.y; y = value.z; w = value.w; } }
        public uvec4 yzzx { get => new uvec4(y, z, z, x); set { y = value.x; z = value.y; z = value.z; x = value.w; } }
        public uvec4 yzzy { get => new uvec4(y, z, z, y); set { y = value.x; z = value.y; z = value.z; y = value.w; } }
        public uvec4 yzzz { get => new uvec4(y, z, z, z); set { y = value.x; z = value.y; z = value.z; z = value.w; } }
        public uvec4 yzzw { get => new uvec4(y, z, z, w); set { y = value.x; z = value.y; z = value.z; w = value.w; } }
        public uvec4 yzwx { get => new uvec4(y, z, w, x); set { y = value.x; z = value.y; w = value.z; x = value.w; } }
        public uvec4 yzwy { get => new uvec4(y, z, w, y); set { y = value.x; z = value.y; w = value.z; y = value.w; } }
        public uvec4 yzwz { get => new uvec4(y, z, w, z); set { y = value.x; z = value.y; w = value.z; z = value.w; } }
        public uvec4 yzww { get => new uvec4(y, z, w, w); set { y = value.x; z = value.y; w = value.z; w = value.w; } }
        public uvec4 ywxx { get => new uvec4(y, w, x, x); set { y = value.x; w = value.y; x = value.z; x = value.w; } }
        public uvec4 ywxy { get => new uvec4(y, w, x, y); set { y = value.x; w = value.y; x = value.z; y = value.w; } }
        public uvec4 ywxz { get => new uvec4(y, w, x, z); set { y = value.x; w = value.y; x = value.z; z = value.w; } }
        public uvec4 ywxw { get => new uvec4(y, w, x, w); set { y = value.x; w = value.y; x = value.z; w = value.w; } }
        public uvec4 ywyx { get => new uvec4(y, w, y, x); set { y = value.x; w = value.y; y = value.z; x = value.w; } }
        public uvec4 ywyy { get => new uvec4(y, w, y, y); set { y = value.x; w = value.y; y = value.z; y = value.w; } }
        public uvec4 ywyz { get => new uvec4(y, w, y, z); set { y = value.x; w = value.y; y = value.z; z = value.w; } }
        public uvec4 ywyw { get => new uvec4(y, w, y, w); set { y = value.x; w = value.y; y = value.z; w = value.w; } }
        public uvec4 ywzx { get => new uvec4(y, w, z, x); set { y = value.x; w = value.y; z = value.z; x = value.w; } }
        public uvec4 ywzy { get => new uvec4(y, w, z, y); set { y = value.x; w = value.y; z = value.z; y = value.w; } }
        public uvec4 ywzz { get => new uvec4(y, w, z, z); set { y = value.x; w = value.y; z = value.z; z = value.w; } }
        public uvec4 ywzw { get => new uvec4(y, w, z, w); set { y = value.x; w = value.y; z = value.z; w = value.w; } }
        public uvec4 ywwx { get => new uvec4(y, w, w, x); set { y = value.x; w = value.y; w = value.z; x = value.w; } }
        public uvec4 ywwy { get => new uvec4(y, w, w, y); set { y = value.x; w = value.y; w = value.z; y = value.w; } }
        public uvec4 ywwz { get => new uvec4(y, w, w, z); set { y = value.x; w = value.y; w = value.z; z = value.w; } }
        public uvec4 ywww { get => new uvec4(y, w, w, w); set { y = value.x; w = value.y; w = value.z; w = value.w; } }
        public uvec4 zxxx { get => new uvec4(z, x, x, x); set { z = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 zxxy { get => new uvec4(z, x, x, y); set { z = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 zxxz { get => new uvec4(z, x, x, z); set { z = value.x; x = value.y; x = value.z; z = value.w; } }
        public uvec4 zxxw { get => new uvec4(z, x, x, w); set { z = value.x; x = value.y; x = value.z; w = value.w; } }
        public uvec4 zxyx { get => new uvec4(z, x, y, x); set { z = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 zxyy { get => new uvec4(z, x, y, y); set { z = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 zxyz { get => new uvec4(z, x, y, z); set { z = value.x; x = value.y; y = value.z; z = value.w; } }
        public uvec4 zxyw { get => new uvec4(z, x, y, w); set { z = value.x; x = value.y; y = value.z; w = value.w; } }
        public uvec4 zxzx { get => new uvec4(z, x, z, x); set { z = value.x; x = value.y; z = value.z; x = value.w; } }
        public uvec4 zxzy { get => new uvec4(z, x, z, y); set { z = value.x; x = value.y; z = value.z; y = value.w; } }
        public uvec4 zxzz { get => new uvec4(z, x, z, z); set { z = value.x; x = value.y; z = value.z; z = value.w; } }
        public uvec4 zxzw { get => new uvec4(z, x, z, w); set { z = value.x; x = value.y; z = value.z; w = value.w; } }
        public uvec4 zxwx { get => new uvec4(z, x, w, x); set { z = value.x; x = value.y; w = value.z; x = value.w; } }
        public uvec4 zxwy { get => new uvec4(z, x, w, y); set { z = value.x; x = value.y; w = value.z; y = value.w; } }
        public uvec4 zxwz { get => new uvec4(z, x, w, z); set { z = value.x; x = value.y; w = value.z; z = value.w; } }
        public uvec4 zxww { get => new uvec4(z, x, w, w); set { z = value.x; x = value.y; w = value.z; w = value.w; } }
        public uvec4 zyxx { get => new uvec4(z, y, x, x); set { z = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 zyxy { get => new uvec4(z, y, x, y); set { z = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 zyxz { get => new uvec4(z, y, x, z); set { z = value.x; y = value.y; x = value.z; z = value.w; } }
        public uvec4 zyxw { get => new uvec4(z, y, x, w); set { z = value.x; y = value.y; x = value.z; w = value.w; } }
        public uvec4 zyyx { get => new uvec4(z, y, y, x); set { z = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 zyyy { get => new uvec4(z, y, y, y); set { z = value.x; y = value.y; y = value.z; y = value.w; } }
        public uvec4 zyyz { get => new uvec4(z, y, y, z); set { z = value.x; y = value.y; y = value.z; z = value.w; } }
        public uvec4 zyyw { get => new uvec4(z, y, y, w); set { z = value.x; y = value.y; y = value.z; w = value.w; } }
        public uvec4 zyzx { get => new uvec4(z, y, z, x); set { z = value.x; y = value.y; z = value.z; x = value.w; } }
        public uvec4 zyzy { get => new uvec4(z, y, z, y); set { z = value.x; y = value.y; z = value.z; y = value.w; } }
        public uvec4 zyzz { get => new uvec4(z, y, z, z); set { z = value.x; y = value.y; z = value.z; z = value.w; } }
        public uvec4 zyzw { get => new uvec4(z, y, z, w); set { z = value.x; y = value.y; z = value.z; w = value.w; } }
        public uvec4 zywx { get => new uvec4(z, y, w, x); set { z = value.x; y = value.y; w = value.z; x = value.w; } }
        public uvec4 zywy { get => new uvec4(z, y, w, y); set { z = value.x; y = value.y; w = value.z; y = value.w; } }
        public uvec4 zywz { get => new uvec4(z, y, w, z); set { z = value.x; y = value.y; w = value.z; z = value.w; } }
        public uvec4 zyww { get => new uvec4(z, y, w, w); set { z = value.x; y = value.y; w = value.z; w = value.w; } }
        public uvec4 zzxx { get => new uvec4(z, z, x, x); set { z = value.x; z = value.y; x = value.z; x = value.w; } }
        public uvec4 zzxy { get => new uvec4(z, z, x, y); set { z = value.x; z = value.y; x = value.z; y = value.w; } }
        public uvec4 zzxz { get => new uvec4(z, z, x, z); set { z = value.x; z = value.y; x = value.z; z = value.w; } }
        public uvec4 zzxw { get => new uvec4(z, z, x, w); set { z = value.x; z = value.y; x = value.z; w = value.w; } }
        public uvec4 zzyx { get => new uvec4(z, z, y, x); set { z = value.x; z = value.y; y = value.z; x = value.w; } }
        public uvec4 zzyy { get => new uvec4(z, z, y, y); set { z = value.x; z = value.y; y = value.z; y = value.w; } }
        public uvec4 zzyz { get => new uvec4(z, z, y, z); set { z = value.x; z = value.y; y = value.z; z = value.w; } }
        public uvec4 zzyw { get => new uvec4(z, z, y, w); set { z = value.x; z = value.y; y = value.z; w = value.w; } }
        public uvec4 zzzx { get => new uvec4(z, z, z, x); set { z = value.x; z = value.y; z = value.z; x = value.w; } }
        public uvec4 zzzy { get => new uvec4(z, z, z, y); set { z = value.x; z = value.y; z = value.z; y = value.w; } }
        public uvec4 zzzz { get => new uvec4(z, z, z, z); set { z = value.x; z = value.y; z = value.z; z = value.w; } }
        public uvec4 zzzw { get => new uvec4(z, z, z, w); set { z = value.x; z = value.y; z = value.z; w = value.w; } }
        public uvec4 zzwx { get => new uvec4(z, z, w, x); set { z = value.x; z = value.y; w = value.z; x = value.w; } }
        public uvec4 zzwy { get => new uvec4(z, z, w, y); set { z = value.x; z = value.y; w = value.z; y = value.w; } }
        public uvec4 zzwz { get => new uvec4(z, z, w, z); set { z = value.x; z = value.y; w = value.z; z = value.w; } }
        public uvec4 zzww { get => new uvec4(z, z, w, w); set { z = value.x; z = value.y; w = value.z; w = value.w; } }
        public uvec4 zwxx { get => new uvec4(z, w, x, x); set { z = value.x; w = value.y; x = value.z; x = value.w; } }
        public uvec4 zwxy { get => new uvec4(z, w, x, y); set { z = value.x; w = value.y; x = value.z; y = value.w; } }
        public uvec4 zwxz { get => new uvec4(z, w, x, z); set { z = value.x; w = value.y; x = value.z; z = value.w; } }
        public uvec4 zwxw { get => new uvec4(z, w, x, w); set { z = value.x; w = value.y; x = value.z; w = value.w; } }
        public uvec4 zwyx { get => new uvec4(z, w, y, x); set { z = value.x; w = value.y; y = value.z; x = value.w; } }
        public uvec4 zwyy { get => new uvec4(z, w, y, y); set { z = value.x; w = value.y; y = value.z; y = value.w; } }
        public uvec4 zwyz { get => new uvec4(z, w, y, z); set { z = value.x; w = value.y; y = value.z; z = value.w; } }
        public uvec4 zwyw { get => new uvec4(z, w, y, w); set { z = value.x; w = value.y; y = value.z; w = value.w; } }
        public uvec4 zwzx { get => new uvec4(z, w, z, x); set { z = value.x; w = value.y; z = value.z; x = value.w; } }
        public uvec4 zwzy { get => new uvec4(z, w, z, y); set { z = value.x; w = value.y; z = value.z; y = value.w; } }
        public uvec4 zwzz { get => new uvec4(z, w, z, z); set { z = value.x; w = value.y; z = value.z; z = value.w; } }
        public uvec4 zwzw { get => new uvec4(z, w, z, w); set { z = value.x; w = value.y; z = value.z; w = value.w; } }
        public uvec4 zwwx { get => new uvec4(z, w, w, x); set { z = value.x; w = value.y; w = value.z; x = value.w; } }
        public uvec4 zwwy { get => new uvec4(z, w, w, y); set { z = value.x; w = value.y; w = value.z; y = value.w; } }
        public uvec4 zwwz { get => new uvec4(z, w, w, z); set { z = value.x; w = value.y; w = value.z; z = value.w; } }
        public uvec4 zwww { get => new uvec4(z, w, w, w); set { z = value.x; w = value.y; w = value.z; w = value.w; } }
        public uvec4 wxxx { get => new uvec4(w, x, x, x); set { w = value.x; x = value.y; x = value.z; x = value.w; } }
        public uvec4 wxxy { get => new uvec4(w, x, x, y); set { w = value.x; x = value.y; x = value.z; y = value.w; } }
        public uvec4 wxxz { get => new uvec4(w, x, x, z); set { w = value.x; x = value.y; x = value.z; z = value.w; } }
        public uvec4 wxxw { get => new uvec4(w, x, x, w); set { w = value.x; x = value.y; x = value.z; w = value.w; } }
        public uvec4 wxyx { get => new uvec4(w, x, y, x); set { w = value.x; x = value.y; y = value.z; x = value.w; } }
        public uvec4 wxyy { get => new uvec4(w, x, y, y); set { w = value.x; x = value.y; y = value.z; y = value.w; } }
        public uvec4 wxyz { get => new uvec4(w, x, y, z); set { w = value.x; x = value.y; y = value.z; z = value.w; } }
        public uvec4 wxyw { get => new uvec4(w, x, y, w); set { w = value.x; x = value.y; y = value.z; w = value.w; } }
        public uvec4 wxzx { get => new uvec4(w, x, z, x); set { w = value.x; x = value.y; z = value.z; x = value.w; } }
        public uvec4 wxzy { get => new uvec4(w, x, z, y); set { w = value.x; x = value.y; z = value.z; y = value.w; } }
        public uvec4 wxzz { get => new uvec4(w, x, z, z); set { w = value.x; x = value.y; z = value.z; z = value.w; } }
        public uvec4 wxzw { get => new uvec4(w, x, z, w); set { w = value.x; x = value.y; z = value.z; w = value.w; } }
        public uvec4 wxwx { get => new uvec4(w, x, w, x); set { w = value.x; x = value.y; w = value.z; x = value.w; } }
        public uvec4 wxwy { get => new uvec4(w, x, w, y); set { w = value.x; x = value.y; w = value.z; y = value.w; } }
        public uvec4 wxwz { get => new uvec4(w, x, w, z); set { w = value.x; x = value.y; w = value.z; z = value.w; } }
        public uvec4 wxww { get => new uvec4(w, x, w, w); set { w = value.x; x = value.y; w = value.z; w = value.w; } }
        public uvec4 wyxx { get => new uvec4(w, y, x, x); set { w = value.x; y = value.y; x = value.z; x = value.w; } }
        public uvec4 wyxy { get => new uvec4(w, y, x, y); set { w = value.x; y = value.y; x = value.z; y = value.w; } }
        public uvec4 wyxz { get => new uvec4(w, y, x, z); set { w = value.x; y = value.y; x = value.z; z = value.w; } }
        public uvec4 wyxw { get => new uvec4(w, y, x, w); set { w = value.x; y = value.y; x = value.z; w = value.w; } }
        public uvec4 wyyx { get => new uvec4(w, y, y, x); set { w = value.x; y = value.y; y = value.z; x = value.w; } }
        public uvec4 wyyy { get => new uvec4(w, y, y, y); set { w = value.x; y = value.y; y = value.z; y = value.w; } }
        public uvec4 wyyz { get => new uvec4(w, y, y, z); set { w = value.x; y = value.y; y = value.z; z = value.w; } }
        public uvec4 wyyw { get => new uvec4(w, y, y, w); set { w = value.x; y = value.y; y = value.z; w = value.w; } }
        public uvec4 wyzx { get => new uvec4(w, y, z, x); set { w = value.x; y = value.y; z = value.z; x = value.w; } }
        public uvec4 wyzy { get => new uvec4(w, y, z, y); set { w = value.x; y = value.y; z = value.z; y = value.w; } }
        public uvec4 wyzz { get => new uvec4(w, y, z, z); set { w = value.x; y = value.y; z = value.z; z = value.w; } }
        public uvec4 wyzw { get => new uvec4(w, y, z, w); set { w = value.x; y = value.y; z = value.z; w = value.w; } }
        public uvec4 wywx { get => new uvec4(w, y, w, x); set { w = value.x; y = value.y; w = value.z; x = value.w; } }
        public uvec4 wywy { get => new uvec4(w, y, w, y); set { w = value.x; y = value.y; w = value.z; y = value.w; } }
        public uvec4 wywz { get => new uvec4(w, y, w, z); set { w = value.x; y = value.y; w = value.z; z = value.w; } }
        public uvec4 wyww { get => new uvec4(w, y, w, w); set { w = value.x; y = value.y; w = value.z; w = value.w; } }
        public uvec4 wzxx { get => new uvec4(w, z, x, x); set { w = value.x; z = value.y; x = value.z; x = value.w; } }
        public uvec4 wzxy { get => new uvec4(w, z, x, y); set { w = value.x; z = value.y; x = value.z; y = value.w; } }
        public uvec4 wzxz { get => new uvec4(w, z, x, z); set { w = value.x; z = value.y; x = value.z; z = value.w; } }
        public uvec4 wzxw { get => new uvec4(w, z, x, w); set { w = value.x; z = value.y; x = value.z; w = value.w; } }
        public uvec4 wzyx { get => new uvec4(w, z, y, x); set { w = value.x; z = value.y; y = value.z; x = value.w; } }
        public uvec4 wzyy { get => new uvec4(w, z, y, y); set { w = value.x; z = value.y; y = value.z; y = value.w; } }
        public uvec4 wzyz { get => new uvec4(w, z, y, z); set { w = value.x; z = value.y; y = value.z; z = value.w; } }
        public uvec4 wzyw { get => new uvec4(w, z, y, w); set { w = value.x; z = value.y; y = value.z; w = value.w; } }
        public uvec4 wzzx { get => new uvec4(w, z, z, x); set { w = value.x; z = value.y; z = value.z; x = value.w; } }
        public uvec4 wzzy { get => new uvec4(w, z, z, y); set { w = value.x; z = value.y; z = value.z; y = value.w; } }
        public uvec4 wzzz { get => new uvec4(w, z, z, z); set { w = value.x; z = value.y; z = value.z; z = value.w; } }
        public uvec4 wzzw { get => new uvec4(w, z, z, w); set { w = value.x; z = value.y; z = value.z; w = value.w; } }
        public uvec4 wzwx { get => new uvec4(w, z, w, x); set { w = value.x; z = value.y; w = value.z; x = value.w; } }
        public uvec4 wzwy { get => new uvec4(w, z, w, y); set { w = value.x; z = value.y; w = value.z; y = value.w; } }
        public uvec4 wzwz { get => new uvec4(w, z, w, z); set { w = value.x; z = value.y; w = value.z; z = value.w; } }
        public uvec4 wzww { get => new uvec4(w, z, w, w); set { w = value.x; z = value.y; w = value.z; w = value.w; } }
        public uvec4 wwxx { get => new uvec4(w, w, x, x); set { w = value.x; w = value.y; x = value.z; x = value.w; } }
        public uvec4 wwxy { get => new uvec4(w, w, x, y); set { w = value.x; w = value.y; x = value.z; y = value.w; } }
        public uvec4 wwxz { get => new uvec4(w, w, x, z); set { w = value.x; w = value.y; x = value.z; z = value.w; } }
        public uvec4 wwxw { get => new uvec4(w, w, x, w); set { w = value.x; w = value.y; x = value.z; w = value.w; } }
        public uvec4 wwyx { get => new uvec4(w, w, y, x); set { w = value.x; w = value.y; y = value.z; x = value.w; } }
        public uvec4 wwyy { get => new uvec4(w, w, y, y); set { w = value.x; w = value.y; y = value.z; y = value.w; } }
        public uvec4 wwyz { get => new uvec4(w, w, y, z); set { w = value.x; w = value.y; y = value.z; z = value.w; } }
        public uvec4 wwyw { get => new uvec4(w, w, y, w); set { w = value.x; w = value.y; y = value.z; w = value.w; } }
        public uvec4 wwzx { get => new uvec4(w, w, z, x); set { w = value.x; w = value.y; z = value.z; x = value.w; } }
        public uvec4 wwzy { get => new uvec4(w, w, z, y); set { w = value.x; w = value.y; z = value.z; y = value.w; } }
        public uvec4 wwzz { get => new uvec4(w, w, z, z); set { w = value.x; w = value.y; z = value.z; z = value.w; } }
        public uvec4 wwzw { get => new uvec4(w, w, z, w); set { w = value.x; w = value.y; z = value.z; w = value.w; } }
        public uvec4 wwwx { get => new uvec4(w, w, w, x); set { w = value.x; w = value.y; w = value.z; x = value.w; } }
        public uvec4 wwwy { get => new uvec4(w, w, w, y); set { w = value.x; w = value.y; w = value.z; y = value.w; } }
        public uvec4 wwwz { get => new uvec4(w, w, w, z); set { w = value.x; w = value.y; w = value.z; z = value.w; } }
        public uvec4 wwww { get => new uvec4(w, w, w, w); set { w = value.x; w = value.y; w = value.z; w = value.w; } }

        #endregion
    }
}
