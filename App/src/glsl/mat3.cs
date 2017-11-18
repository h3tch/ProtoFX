using System;

#pragma warning disable IDE1006

namespace protofx.Glsl
{
    public struct mat3
    {
        #region mat3

        internal vec3 C0, C1, C2;
        public vec3 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return C0;
                    case 1: return C1;
                    case 2: return C2;
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
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "[" + C0 + "; " + C1 + "; " + C2 + "]";
        }

        public Array ToArray()
        {
            return new[,] {
            { C0.x, C1.x, C2.x },
            { C0.y, C1.y, C2.y },
            { C0.z, C1.z, C2.z } };
        }

        public mat3(float a) : this(new vec3(a), new vec3(a), new vec3(a)) { }
        public mat3(float[] v) : this(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8]) { }
        public mat3(byte[] data) : this((float[])data.To(typeof(float))) { }
        public mat3(vec3 a, vec3 b, vec3 c) { C0 = a; C1 = b; C2 = c; }
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

        public static vec3 operator *(mat3 a, vec3 b)
        {
            return new vec3(
                a[0][0] * b[0] + a[1][0] * b[1] + a[2][0] * b[2],
                a[0][1] * b[0] + a[1][1] * b[1] + a[2][1] * b[2],
                a[0][2] * b[0] + a[1][2] * b[1] + a[2][2] * b[2]);
        }

        public static vec3 operator *(vec3 a, mat3 b)
        {
            return new vec3(
                a[0] * b[0][0] + a[1] * b[0][1] + a[2] * b[0][2],
                a[0] * b[1][0] + a[1] * b[1][1] + a[2] * b[1][2],
                a[0] * b[2][0] + a[1] * b[2][1] + a[2] * b[2][2]);
        }

        #endregion
    }

    public struct dmat3
    {
        #region mat3
        internal dvec3 C0, C1, C2;
        public dvec3 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return C0;
                    case 1: return C1;
                    case 2: return C2;
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
                }
                throw new IndexOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return "[" + C0 + "; " + C1 + "; " + C2 + "]";
        }

        public Array ToArray()
        {
            return new[,] {
            { C0.x, C1.x, C2.x },
            { C0.y, C1.y, C2.y },
            { C0.z, C1.z, C2.z } };
        }

        public dmat3(double a) : this(new dvec3(a), new dvec3(a), new dvec3(a)) { }
        public dmat3(double[] v) : this(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8]) { }
        public dmat3(byte[] data) : this((double[])data.To(typeof(double))) { }
        public dmat3(dvec3 a, dvec3 b, dvec3 c) { C0 = a; C1 = b; C2 = c; }
        public dmat3(
            double _00, double _10, double _20,
            double _01, double _11, double _21,
            double _02, double _12, double _22) : this(
                new dvec3(_00, _10, _20),
                new dvec3(_01, _11, _21),
                new dvec3(_02, _12, _22))
        { }

        #endregion

        #region Operators

        public static dmat3 operator *(dmat3 a, dmat3 b)
        {
            return new dmat3(
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

        public static dvec3 operator *(dmat3 a, dvec3 b)
        {
            return new dvec3(
                a[0][0] * b[0] + a[1][0] * b[1] + a[2][0] * b[2],
                a[0][1] * b[0] + a[1][1] * b[1] + a[2][1] * b[2],
                a[0][2] * b[0] + a[1][2] * b[1] + a[2][2] * b[2]);
        }

        public static dvec3 operator *(dvec3 a, dmat3 b)
        {
            return new dvec3(
                a[0] * b[0][0] + a[1] * b[0][1] + a[2] * b[0][2],
                a[0] * b[1][0] + a[1] * b[1][1] + a[2] * b[1][2],
                a[0] * b[2][0] + a[1] * b[2][1] + a[2] * b[2][2]);
        }

        #endregion
    }
}
