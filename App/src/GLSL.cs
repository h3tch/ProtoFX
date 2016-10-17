/*namespace GLSL
{
    class __vec2
    {
        public double[] v;

        public double this[int i] { get { return v[i]; } set { v[i] = value; } }

        public double x { get { return v[0]; } set { v[0] = value; } }
        public double y { get { return v[1]; } set { v[1] = value; } }

        public double r { get { return v[0]; } set { v[0] = value; } }
        public double g { get { return v[1]; } set { v[1] = value; } }

        public vec2 xy { get { return new vec2(x, y); } set { x = value.x; y = value.y; } }
        public vec2 yx { get { return new vec2(y, x); } set { y = value.x; x = value.y; } }

        public vec2 rg { get { return new vec2(x, y); } set { x = value.x; y = value.y; } }
        public vec2 gr { get { return new vec2(y, x); } set { y = value.x; x = value.y; } }
    }

    class __vec3 : __vec2
    {
        public double z { get { return v[2]; } set { v[2] = value; } }
        public double b { get { return v[2]; } set { v[2] = value; } }
        
        public vec2 xz { get { return new vec2(x, z); } set { x = value.x; z = value.y; } }
        public vec2 zx { get { return new vec2(z, x); } set { z = value.x; x = value.y; } }
        public vec2 yz { get { return new vec2(y, z); } set { y = value.x; z = value.y; } }
        public vec2 zy { get { return new vec2(z, y); } set { z = value.x; y = value.y; } }

        public vec3 xyz { get { return new vec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public vec3 yxz => new vec3(y, x, z);
        public vec3 xzy => new vec3(x, z, y);
        public vec3 yzx => new vec3(y, z, x);
        public vec3 zxy => new vec3(z, x, y);
        public vec3 zyx => new vec3(z, y, x);

        public vec2 rb => new vec2(x, z);
        public vec2 br => new vec2(z, x);
        public vec2 gb => new vec2(y, z);
        public vec2 bg => new vec2(z, y);

        public vec3 rgb { get { return new vec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public vec3 rbg => new vec3(r, b, g);
        public vec3 grb => new vec3(g, r, b);
        public vec3 gbr => new vec3(g, b, r);
        public vec3 brg => new vec3(b, r, g);
        public vec3 bgr => new vec3(b, g, r);
    }

    class __vec4 : __vec3
    {
        public double w { get { return v[3]; } set { v[3] = value; } }
        public double a { get { return v[3]; } set { v[3] = value; } }

        public vec2 xw => new vec2(x, w);
        public vec2 wx => new vec2(w, x);
        public vec2 yw => new vec2(y, w);
        public vec2 wy => new vec2(w, y);
        public vec2 zw => new vec2(z, w);
        public vec2 wz => new vec2(w, z);

        public vec3 wyz => new vec3(w, y, z);
        public vec3 wzy => new vec3(w, z, y);
        public vec3 ywz => new vec3(y, w, z);
        public vec3 yzw => new vec3(y, z, w);
        public vec3 zwy => new vec3(z, w, y);
        public vec3 zyw => new vec3(z, y, w);

        public vec3 xwz => new vec3(x, w, z);
        public vec3 xzw => new vec3(x, z, w);
        public vec3 wxz => new vec3(w, x, z);
        public vec3 wzx => new vec3(w, z, x);
        public vec3 zxw => new vec3(z, x, w);
        public vec3 zwx => new vec3(z, w, x);

        public vec3 xyw => new vec3(x, y, w);
        public vec3 xwy => new vec3(x, w, y);
        public vec3 yxw => new vec3(y, x, w);
        public vec3 ywx => new vec3(y, w, x);
        public vec3 wxy => new vec3(w, x, y);
        public vec3 wyx => new vec3(w, y, x);

        public vec4 xyzw => new vec4(x, y, z, w);
        public vec4 yxzw => new vec4(y, x, z, w);
        public vec4 xzyw => new vec4(x, z, y, w);
        public vec4 yzxw => new vec4(y, z, x, w);
        public vec4 zxyw => new vec4(z, x, y, w);
        public vec4 zyxw => new vec4(z, y, x, w);

        public vec4 xywz => new vec4(x, y, w, z);
        public vec4 yxwz => new vec4(y, x, w, z);
        public vec4 xzwy => new vec4(x, z, w, y);
        public vec4 yzwx => new vec4(y, z, w, x);
        public vec4 zxwy => new vec4(z, x, w, y);
        public vec4 zywx => new vec4(z, y, w, x);

        public vec4 xwyz => new vec4(x, w, y, z);
        public vec4 ywxz => new vec4(y, w, x, z);
        public vec4 xwzy => new vec4(x, w, z, y);
        public vec4 ywzx => new vec4(y, w, z, x);
        public vec4 zwxy => new vec4(z, w, x, y);
        public vec4 zwyx => new vec4(z, w, y, x);

        public vec4 wxyz => new vec4(w, x, y, z);
        public vec4 wyxz => new vec4(w, y, x, z);
        public vec4 wxzy => new vec4(w, x, z, y);
        public vec4 wyzx => new vec4(w, y, z, x);
        public vec4 wzxy => new vec4(w, z, x, y);
        public vec4 wzyx => new vec4(w, z, y, x);

        public vec2 ra => new vec2(r, a);
        public vec2 ar => new vec2(a, r);
        public vec2 ga => new vec2(g, a);
        public vec2 ag => new vec2(a, g);
        public vec2 ba => new vec2(b, a);
        public vec2 ab => new vec2(a, b);

        public vec3 agb => new vec3(a, g, b);
        public vec3 abg => new vec3(a, b, g);
        public vec3 gab => new vec3(g, a, b);
        public vec3 gba => new vec3(g, b, a);
        public vec3 bag => new vec3(b, a, g);
        public vec3 bga => new vec3(b, g, a);

        public vec3 rab => new vec3(r, a, b);
        public vec3 rba => new vec3(r, b, a);
        public vec3 arb => new vec3(a, r, b);
        public vec3 abr => new vec3(a, b, r);
        public vec3 bra => new vec3(b, r, a);
        public vec3 bar => new vec3(b, a, r);

        public vec3 rga => new vec3(r, g, a);
        public vec3 rag => new vec3(r, a, g);
        public vec3 gra => new vec3(g, r, a);
        public vec3 gar => new vec3(g, a, r);
        public vec3 arg => new vec3(a, r, g);
        public vec3 agr => new vec3(a, g, r);

        public vec4 rgba => new vec4(r, g, b, a);
        public vec4 rbga => new vec4(r, b, g, a);
        public vec4 grba => new vec4(g, r, b, a);
        public vec4 gbra => new vec4(g, b, r, a);
        public vec4 brga => new vec4(b, r, g, a);
        public vec4 bgra => new vec4(b, g, r, a);

        public vec4 rgab => new vec4(r, g, a, b);
        public vec4 rbag => new vec4(r, b, a, g);
        public vec4 grab => new vec4(g, r, a, b);
        public vec4 gbar => new vec4(g, b, a, r);
        public vec4 brag => new vec4(b, r, a, g);
        public vec4 bgar => new vec4(b, g, a, r);

        public vec4 ragb => new vec4(r, a, g, b);
        public vec4 rabg => new vec4(r, a, b, g);
        public vec4 garb => new vec4(g, a, r, b);
        public vec4 gabr => new vec4(g, a, b, r);
        public vec4 barg => new vec4(b, a, r, g);
        public vec4 bagr => new vec4(b, a, g, r);

        public vec4 argb => new vec4(a, r, g, b);
        public vec4 arbg => new vec4(a, r, b, g);
        public vec4 agrb => new vec4(a, g, r, b);
        public vec4 agbr => new vec4(a, g, b, r);
        public vec4 abrg => new vec4(a, b, r, g);
        public vec4 abgr => new vec4(a, b, g, r);
    }

    class __mat2
    {
        protected vec2[] C;
        public vec2 this[int i] { get { return C[i]; } set { C[i] = value; } }
    }

    class __mat3
    {
        protected vec3[] C;
        public vec3 this[int i] { get { return C[i]; } set { C[i] = value; } }
    }

    class __mat4
    {
        protected vec4[] C;
        public vec4 this[int i] { get { return C[i]; } set { C[i] = value; } }
    }

    class vec2 : __vec2
    {
        public vec2() { v = new double[2]; }
        public vec2(double a) : this(a, a) { }
        public vec2(double X, double Y) : this() { x = (float)X; y = (float)Y; }

        public static vec2 operator +(vec2 a) => new vec2(a.x, a.y);
        public static vec2 operator -(vec2 a) => new vec2(-a.x, -a.y);
        public static vec2 operator +(vec2 a, vec2 b) => new vec2(a.x + b.x, a.y + b.y);
        public static vec2 operator +(vec2 a, double b) => new vec2(a.x + b, a.y + b);
        public static vec2 operator +(double a, vec2 b) => new vec2(a + b.x, a + b.y);
        public static vec2 operator -(vec2 a, vec2 b) => new vec2(a.x - b.x, a.y - b.y);
        public static vec2 operator -(vec2 a, double b) => new vec2(a.x - b, a.y - b);
        public static vec2 operator -(double a, vec2 b) => new vec2(a - b.x, a - b.y);
        public static vec2 operator *(vec2 a, vec2 b) => new vec2(a.x * b.x, a.y * b.y);
        public static vec2 operator /(vec2 a, vec2 b) => new vec2(a.x / b.x, a.y / b.y);
        public static vec2 operator *(vec2 a, double b) => new vec2(a.x * b, a.y * b);
        public static vec2 operator /(vec2 a, double b) => new vec2(a.x / b, a.y / b);
        public static vec2 operator *(double a, vec2 b) => new vec2(a * b.x, a * b.y);
        public static vec2 operator /(double a, vec2 b) => new vec2(a / b.x, a / b.y);
    }

    class vec3 : __vec3
    {
        public vec3() { v = new double[3]; }
        public vec3(double a) : this(a, a, a) { }
        public vec3(double X, double Y, double Z) : this() { x = X; y = Y; z = Z; }

        public static vec3 operator +(vec3 a, vec3 b) => new vec3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static vec3 operator +(vec3 a, double b) => new vec3(a.x + b, a.y + b, a.z + b);
        public static vec3 operator +(double a, vec3 b) => new vec3(a + b.x, a + b.y, a + b.z);
        public static vec3 operator -(vec3 a, vec3 b) => new vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static vec3 operator -(vec3 a, double b) => new vec3(a.x - b, a.y - b, a.z - b);
        public static vec3 operator -(double a, vec3 b) => new vec3(a - b.x, a - b.y, a - b.z);
        public static vec3 operator *(vec3 a, vec3 b) => new vec3(a.x * b.x, a.y * b.y, a.z * b.z);
        public static vec3 operator /(vec3 a, vec3 b) => new vec3(a.x / b.x, a.y / b.y, a.z / b.z);
        public static vec3 operator *(vec3 a, double b) => new vec3(a.x * b, a.y * b, a.z * b);
        public static vec3 operator *(double a, vec3 b) => new vec3(a * b.x, a * b.y, a * b.z);
        public static vec3 operator /(double a, vec3 b) => new vec3(a / b.x, a / b.y, a / b.z);
        public static vec3 operator /(vec3 a, double b) => new vec3(a.x / b, a.y / b, a.z / b);
    }

    class vec4 : __vec4
    {
        public vec4() { v = new double[4]; }
        public vec4(double a) : this(a, a, a, a) { }
        public vec4(double X, double Y, double Z, double W) : this() { x = X; y = Y; z = Z; w = W; }

        public static vec4 operator +(vec4 a, vec4 b) => new vec4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static vec4 operator -(vec4 a, vec4 b) => new vec4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static vec4 operator *(vec4 a, vec4 b) => new vec4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        public static vec4 operator /(vec4 a, vec4 b) => new vec4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    }

    class mat2 : __mat2
    {
        public mat2() { C = new vec2[] { new vec2(), new vec2() }; }
        public mat2(double a) : this(a, a, a, a) { }
        public mat2(vec2 a, vec2 b) { C = new vec2[] { a, b }; }
        public mat2(
            double _00, double _10,
            double _01, double _11) : this(
                new vec2(_00, _10),
                new vec2(_01, _11)) { }

        public static mat2 operator *(mat2 a, mat2 b)
        {
            return new mat2(
                a[0][0] * b[0][0] + a[1][0] * b[0][1],
                a[0][0] * b[1][0] + a[1][0] * b[1][1],
                a[0][1] * b[0][0] + a[1][1] * b[0][1],
                a[0][1] * b[1][0] + a[1][1] * b[1][1]);
        }
    }

    class mat3 : __mat3
    {
        public mat3() { C = new vec3[] { new vec3(), new vec3(), new vec3() }; }
        public mat3(double a) : this(new vec3(a), new vec3(a), new vec3(a)) { }
        public mat3(vec3 a, vec3 b, vec3 c) { C = new vec3[] { a, b, c }; }
        public mat3(
            double _00, double _10, double _20,
            double _01, double _11, double _21,
            double _02, double _12, double _22) : this(
                new vec3(_00, _10, _20),
                new vec3(_01, _11, _21),
                new vec3(_02, _12, _22)) { }

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
    }

    class mat4 : __mat4
    {
        public mat4() { C = new vec4[] { new vec4(), new vec4(), new vec4(), new vec4() }; }
        public mat4(double a) : this(new vec4(a), new vec4(a), new vec4(a), new vec4(a)) { }
        public mat4(vec4 a, vec4 b, vec4 c, vec4 d) { C = new vec4[] { a, b, c, d }; }
        public mat4(
            double _00, double _10, double _20, double _30,
            double _01, double _11, double _21, double _31,
            double _02, double _12, double _22, double _32,
            double _03, double _13, double _23, double _33) : this(
                new vec4(_00, _10, _20, _30),
                new vec4(_01, _11, _21, _31),
                new vec4(_02, _12, _22, _32),
                new vec4(_03, _13, _23, _33)) { }

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
    }

    class Math
    {
        #region 

        public static double abs(double a) => System.Math.Abs(a);
        public static double fract(double a) => a - (long)a;
        public static double sin(double a) => System.Math.Sin(a);
        public static double cos(double a) => System.Math.Cos(a);
        public static double tan(double a) => System.Math.Tan(a);
        public static double dot(vec2 a, vec2 b) => a.x * b.x + a.y * b.y;
        public static double dot(vec3 a, vec3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
        public static double dot(vec4 a, vec4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static vec3 cross(vec3 a, vec3 b) => new GLSL.vec3();
        public static bool isnan(double a) => double.IsNaN(a);

        #endregion

        #region Constructors
        
        public static vec2 vec2(double a) => new GLSL.vec2(a);
        public static vec2 vec2(double x, double y) => new GLSL.vec2(x, y);
        public static vec3 vec3(double a) => new GLSL.vec3(a);
        public static vec3 vec3(double x, double y, double z) => new GLSL.vec3(x, y, z);
        public static vec4 vec4(double a) => new GLSL.vec4(a);
        public static vec4 vec4(double x, double y, double z, double w) => new GLSL.vec4(x, y, z, w);
        public static mat2 mat2(double a) => new GLSL.mat2(a);
        public static mat2 mat2(vec2 a, vec2 b) => new GLSL.mat2(a, b);
        public static mat2 mat2(
            double _00, double _10,
            double _01, double _11) => new GLSL.mat2(
                _00, _10,
                _01, _11);
        public static mat3 mat3(double a) => new GLSL.mat3(a);
        public static mat3 mat3(vec3 a, vec3 b, vec3 c) => new GLSL.mat3(a, b, c);
        public static mat3 mat3(
            double _00, double _10, double _20,
            double _01, double _11, double _21,
            double _02, double _12, double _22) => new GLSL.mat3(
                _00, _10, _20,
                _01, _11, _21,
                _02, _12, _22);
        public static mat4 mat4(float a) => new GLSL.mat4(a);
        public static mat4 mat4(vec4 a, vec4 b, vec4 c, vec4 d) => new GLSL.mat4(a, b, c, d);
        public static mat4 mat4(
            double _00, double _10, double _20, double _30,
            double _01, double _11, double _21, double _31,
            double _02, double _12, double _22, double _32,
            double _03, double _13, double _23, double _33) => new GLSL.mat4(
                _00, _10, _20, _30,
                _01, _11, _21, _31,
                _02, _12, _22, _32,
                _03, _13, _23, _33);

        #endregion
    }

    class Shader : Math
    {
        public static int[] gl_SamplerID = new int[32];
        public static object[][] gl_InAttr = new object[32][];
        public static object[] gl_UniformObject = new object[32];

        public static T ToUniformBuffer<T>(int id)
        {
            return default(T);
        }
    }

    class VertexShader : Shader
    {
        public static int gl_VertexID;
        public static int gl_InstanceID;
        public static vec4 gl_Position;
        public static float gl_PointSize;
        public static float[] gl_ClipDistance = new float[4];
    }

    class CShartToGlsl
    {
        public string Version(string text)
        {
            return text;
        }

        public string Constants(string text)
        {
            return text;
        }

        public string FloatToDouble(string text)
        {
            return text;
        }

        public string UniformSampler(string text)
        {
            return text;
        }

        public string UniformBuffer(string text)
        {
            return text;
        }

        public string VertexInput(string text)
        {
            return text;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class IN : System.Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class OUT : System.Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Struct | System.AttributeTargets.Field)]
    public class layout : System.Attribute
    {
        public int binding;
        public int location;
    }

    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class uniform : System.Attribute
    {
        public int binding;
    }

    class fs_render : VertexShader
    {
        [layout(binding = 0)] struct SpotLight {
            public mat4 viewProj;
            public vec4 camera;
            public vec4 light;
        } [uniform] static SpotLight light;

        [layout(binding = 2)] struct PoissonDisc {
            public vec2[] points;
            public int numPoints;
        } [uniform] static PoissonDisc disc;

        [layout(binding = 0)] int shadowmap;
    
        [layout(location = 0) IN] vec4 in_lpos;
        [layout(location = 1) IN] vec4 in_col;
        [OUT] vec4 color;

        double PI => 3.14159265358979;
        double lightR => light.light.y;
        double zNear => light.camera.z;
        double zFar => light.camera.w;
        vec2 fovVec => light.light.zw;

        double rand(vec2 xy) {
            return fract(sin(dot(xy, vec2(12.9898,78.233))) * 43758.5453);
        }

        mat2 randRot(double angle) {
            double sa = sin(angle);
            double ca = cos(angle);
            return mat2(ca, -sa, sa, ca);
        }
    
        vec2 depthNormalBias(vec3 lpos) {
            vec3 normal = cross(dFdx(lpos), dFdy(lpos));
            return vec2(1,-1) * normal.xy / normal.z;
        }

        double light2plane(double value, double zReceiver) {
            return value * (zReceiver - zNear) / zReceiver;
        }

        vec2 plane2tex(double value) {
            return vec2(value) / fovVec;
        }

        double light2penumbra(double value, double zBlocker, double zReceiver) {
            return value * (zReceiver - zBlocker) / zBlocker;
        }

        double penumbra2plane(double value, double zReceiver) {
            return value * zNear / zReceiver;
        }

        double bias(vec2 zChange, vec2 offset) {
            return dot(zChange, offset) - 0.001;
        }

        double exp2linDepth(double z) {
            return zFar * zNear / (zFar - z * (zFar - zNear));  
        }

        double BlockerSearch(vec2 filterR, vec3 lpos, vec2 surfBias) {
            int nBlocker = 0;
            double zBlocker = 0.0;
            mat2 R = randRot(2 * PI * rand(lpos.xy));

            for (int i = 0; i < disc.numPoints; i++) {
                vec2 p = R * disc.points[i] * filterR;
                double z = texture(shadowmap, lpos.xy + p).x;
                //z = exp2linDepth(z);
                if (lpos.z + bias(surfBias, p) > z) {
                    zBlocker += z;
                    nBlocker++;
                }
            }
            //return zBlocker / float(nBlocker);
            return exp2linDepth(zBlocker / (double)(nBlocker));
        }

        double PCF(vec2 filterR, vec3 lpos, vec2 surfBias) {
            // PCF SHADOW MAPPING
            double shadow = 0.0f;
            mat2 R = randRot(2 * PI * rand(lpos.xy));

            for (int i = 0; i < disc.numPoints; i++) {
                vec2 p = R * disc.points[i] * filterR;
                double z = texture(shadowmap, lpos.xy + p).x;
                //z = exp2linDepth(z);
                shadow += lpos.z + bias(surfBias, p) > z ? 1.0 : 0.0;
            }

            return shadow / (double)(disc.numPoints);
        }

        double PCSS(vec2 surfBias, vec3 lpos) {
            double receiverZ = exp2linDepth(lpos.z);

            // BLOCKER SEARCH
            double planeR = light2plane(lightR, receiverZ);
            vec2 filterR = plane2tex(planeR);
            double zBlocker = BlockerSearch(filterR, lpos, surfBias);
            if (isnan(zBlocker))
                return 0;//zBlocker = lpos.z;

            // PCF SHADOW MAPPING
            double penumbraR = light2penumbra(lightR, zBlocker, receiverZ);
            planeR = penumbra2plane(penumbraR, receiverZ);
            filterR = plane2tex(planeR);
            return PCF(filterR, lpos, surfBias);
        }
    
        void main () {
            // pass color
            color = in_col;
        
            // if pixel outside light frustum clip space, do nothing
            if (any(greaterThan(abs(in_lpos), in_lpos.wwww)))
                return;
            
            // SHADOW MAPPING
        
            // transform to normalized device coordinates [-1;+1]
            // and then to shadow map texture coordinates [0;1]
            vec3 lpos = (in_lpos.xyz / in_lpos.w) * 0.5 + 0.5;
        
            vec3 normal = cross(dFdx(lpos),dFdy(lpos));
            if (normal.z < 0)
                return;
            
            vec2 surfBias = -normal.xy / normal.z;

            double shadow = PCSS(surfBias, lpos);
            
            if (shadow == 0.0)
                return;
        
            color.rgb *= (1.0 - shadow) * 0.5 + 0.5;
        }
    }
}*/
