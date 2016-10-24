namespace App.Glsl
{
    class tvec2<T> : tvecN<T>
    {
        public tvec2() : base(default(T), default(T)) { }
        public tvec2(T a) : base(a, a) { }
        public tvec2(T x, T y) : base(x, y) { }
        public tvec2(byte[] data) : base((T[])data.To(typeof(T))) { }
    }

    class vec2 : tvec2<float>
    {
        #region vec2

        public vec2() : base() { }
        public vec2(float a) : base(a, a) { }
        public vec2(float x, float y) : base(x, y) { }
        public vec2(byte[] data) : base(data) { }

        #endregion

        #region Operators

        public static vec2 operator +(vec2 a) => Shader.TraceVar(new vec2(a.x, a.y));
        public static vec2 operator -(vec2 a) => Shader.TraceVar(new vec2(-a.x, -a.y));
        public static vec2 operator +(vec2 a, vec2 b) => Shader.TraceFunc(new vec2(a.x + b.x, a.y + b.y), a, b);
        public static vec2 operator +(vec2 a, float b) => Shader.TraceFunc(new vec2(a.x + b, a.y + b), a, b);
        public static vec2 operator +(float a, vec2 b) => Shader.TraceFunc(new vec2(a + b.x, a + b.y), a, b);
        public static vec2 operator -(vec2 a, vec2 b) => Shader.TraceFunc(new vec2(a.x - b.x, a.y - b.y), a, b);
        public static vec2 operator -(vec2 a, float b) => Shader.TraceFunc(new vec2(a.x - b, a.y - b), a, b);
        public static vec2 operator -(float a, vec2 b) => Shader.TraceFunc(new vec2(a - b.x, a - b.y), a, b);
        public static vec2 operator *(vec2 a, vec2 b) => Shader.TraceFunc(new vec2(a.x * b.x, a.y * b.y), a, b);
        public static vec2 operator *(vec2 a, float b) => Shader.TraceFunc(new vec2(a.x * b, a.y * b), a, b);
        public static vec2 operator *(float a, vec2 b) => Shader.TraceFunc(new vec2(a * b.x, a * b.y), a, b);
        public static vec2 operator /(vec2 a, vec2 b) => Shader.TraceFunc(new vec2(a.x / b.x, a.y / b.y), a, b);
        public static vec2 operator /(vec2 a, float b) => Shader.TraceFunc(new vec2(a.x / b, a.y / b), a, b);
        public static vec2 operator /(float a, vec2 b) => Shader.TraceFunc(new vec2(a / b.x, a / b.y), a, b);

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
        public dvec2(byte[] data) : base(data) { }

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
        public bvec2(byte[] data) : base(data) { }

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
        public ivec2(byte[] data) : base(data) { }

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
        public uvec2(byte[] data) : base(data) { }

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
}
