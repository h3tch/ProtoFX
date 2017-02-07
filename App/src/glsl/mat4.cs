using System;

namespace App.Glsl
{
    public struct mat4
    {
        #region mat4
        
        internal vec4 C0, C1, C2, C3;
        public vec4 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return C0;
                    case 1: return C1;
                    case 2: return C2;
                    case 3: return C3;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: C0 = value; break;
                    case 1: C1 = value; break;
                    case 2: C2 = value; break;
                    case 3: C3 = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public static mat4 Identity = new mat4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        public override string ToString() => "[" + C0 + "; " + C1 + "; " + C2 + "; " + C3 + "]";
        public Array ToArray() => new [,] {
            { C0.x, C1.x, C2.x, C3.x },
            { C0.y, C1.y, C2.y, C3.y },
            { C0.z, C1.z, C2.z, C3.z },
            { C0.w, C1.w, C2.w, C3.w } };

        public mat4(float a) : this(new vec4(a), new vec4(a), new vec4(a), new vec4(a)) { }
        public mat4(float[] v) : this(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9], v[10], v[11], v[12], v[13], v[14], v[15]) { }
        public mat4(byte[] data) : this((float[])data.To(typeof(float))) { }
        public mat4(vec4 a, vec4 b, vec4 c, vec4 d) { C0 = a; C1 = b; C2 = c; C3 = d; }
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

    public struct dmat4
    {
        #region mat4
        
        internal dvec4 C0, C1, C2, C3;
        public dvec4 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return C0;
                    case 1: return C1;
                    case 2: return C2;
                    case 3: return C3;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: C0 = value; break;
                    case 1: C1 = value; break;
                    case 2: C2 = value; break;
                    case 3: C3 = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public static mat4 Identity = new mat4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        public override string ToString() => "[" + C0 + "; " + C1 + "; " + C2 + "; " + C3 + "]";
        public Array ToArray() => new [,] {
            { C0.x, C1.x, C2.x, C3.x },
            { C0.y, C1.y, C2.y, C3.y },
            { C0.z, C1.z, C2.z, C3.z },
            { C0.w, C1.w, C2.w, C3.w } };

        public dmat4(double a = 0) : this(new dvec4(a), new dvec4(a), new dvec4(a), new dvec4(a)) { }
        public dmat4(double[] v) : this(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8], v[9], v[10], v[11], v[12], v[13], v[14], v[15]) { }
        public dmat4(byte[] data) : this((double[])data.To(typeof(double))) { }
        public dmat4(dvec4 a, dvec4 b, dvec4 c, dvec4 d) { C0 = a; C1 = b; C2 = c; C3 = d; }
        public dmat4(
            double _00, double _10, double _20, double _30,
            double _01, double _11, double _21, double _31,
            double _02, double _12, double _22, double _32,
            double _03, double _13, double _23, double _33) : this(
                new dvec4(_00, _10, _20, _30),
                new dvec4(_01, _11, _21, _31),
                new dvec4(_02, _12, _22, _32),
                new dvec4(_03, _13, _23, _33))
        { }

        #endregion

        #region Operator

        public static dmat4 operator *(dmat4 a, dmat4 b)
        {
            return new dmat4(
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

        public static dvec4 operator *(dmat4 a, dvec4 b)
        {
            return new dvec4(
                a[0][0] * b[0] + a[1][0] * b[1] + a[2][0] * b[2] + a[3][0] * b[3],
                a[0][1] * b[0] + a[1][1] * b[1] + a[2][1] * b[2] + a[3][1] * b[3],
                a[0][2] * b[0] + a[1][2] * b[1] + a[2][2] * b[2] + a[3][2] * b[3],
                a[0][3] * b[0] + a[1][3] * b[1] + a[2][3] * b[2] + a[3][3] * b[3]);
        }

        public static dvec4 operator *(dvec4 a, dmat4 b)
        {
            return new dvec4(
                a[0] * b[0][0] + a[1] * b[0][1] + a[2] * b[0][2] + a[3] * b[0][3],
                a[0] * b[1][0] + a[1] * b[1][1] + a[2] * b[1][2] + a[3] * b[1][3],
                a[0] * b[2][0] + a[1] * b[2][1] + a[2] * b[2][2] + a[3] * b[2][3],
                a[0] * b[3][0] + a[1] * b[3][1] + a[2] * b[3][2] + a[3] * b[3][3]);
        }

        #endregion
    }
}
