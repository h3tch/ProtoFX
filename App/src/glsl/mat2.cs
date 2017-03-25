using App.Extensions;
using System;

#pragma warning disable IDE1006

namespace App.Glsl
{
    public struct mat2
    {
        #region mat2

        internal vec2 C0, C1;
        public vec2 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return C0;
                    case 1: return C1;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: C0 = value; break;
                    case 1: C1 = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }
        
        public mat2(float a = 0f) : this(a, a, a, a) { }
        public mat2(float[] v) : this(v[0], v[1], v[2], v[3]) { }
        public mat2(byte[] data) : this((float[])data.To(typeof(float))) { }
        public mat2(vec2 a, vec2 b) { C0 = a; C1 = b; }
        public mat2(
            float _00, float _10,
            float _01, float _11) : this(
                new vec2(_00, _10),
                new vec2(_01, _11))
        { }
        public override string ToString()
        {
            return "[" + C0 + "; " + C1 + "]";
        }

        public Array ToArray()
        {
            return new[,] { { C0.x, C1.x }, { C0.y, C1.y } };
        }

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

    public struct dmat2
    {
        #region mat2
        
        internal dvec2 C0, C1;
        public dvec2 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return C0;
                    case 1: return C1;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0: C0 = value; break;
                    case 1: C1 = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public dmat2(double a = 0f) : this(a, a, a, a) { }
        public dmat2(double[] v) : this(v[0], v[1], v[2], v[3]) { }
        public dmat2(byte[] data) : this((double[])data.To(typeof(double))) { }
        public dmat2(dvec2 a, dvec2 b) { C0 = a; C1 = b; }
        public dmat2(
            double _00, double _10,
            double _01, double _11) : this(
                new dvec2(_00, _10),
                new dvec2(_01, _11))
        { }
        public override string ToString()
        {
            return "[" + C0 + "; " + C1 + "]";
        }

        public Array ToArray()
        {
            return new[,] { { C0.x, C1.x }, { C0.y, C1.y } };
        }

        #endregion

        #region Operators

        public static dmat2 operator *(dmat2 a, dmat2 b)
        {
            return new dmat2(
                a[0][0] * b[0][0] + a[1][0] * b[0][1],
                a[0][0] * b[1][0] + a[1][0] * b[1][1],
                a[0][1] * b[0][0] + a[1][1] * b[0][1],
                a[0][1] * b[1][0] + a[1][1] * b[1][1]);
        }

        public static dvec2 operator *(dmat2 a, dvec2 b)
        {
            return new dvec2(
                a[0][0] * b[0] + a[1][0] * b[1],
                a[0][1] * b[0] + a[1][1] * b[1]);
        }

        public static dvec2 operator *(dvec2 a, dmat2 b)
        {
            return new dvec2(
                a[0] * b[0][0] + a[1] * b[0][1],
                a[0] * b[1][0] + a[1] * b[1][1]);
        }

        #endregion
    }
}
