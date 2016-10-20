using System.Collections.Generic;
using System.Linq;

namespace App.Glsl
{
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
        public override string ToString() => "[" + C.Select(x => x.ToString()).Cat(", ") + "]";

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
}
