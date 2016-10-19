namespace App.Glsl
{
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
}
