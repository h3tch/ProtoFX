using System.Collections.Generic;
using System.Linq;

namespace App.Glsl
{
    class mat2
    {
        #region mat2

        internal vec2[] C;
        public vec2 this[int i] { get { return C[i]; } set { C[i] = value; } }

        public mat2() { C = new vec2[] { new vec2(), new vec2() }; }
        public mat2(float a) : this(a, a, a, a) { }
        public mat2(float[] v) : this(v[0], v[1], v[2], v[3]) { }
        public mat2(byte[] data) : this((float[])data.To(typeof(float))) { }
        public mat2(vec2 a, vec2 b) { C = new vec2[] { a, b }; }
        public mat2(
            float _00, float _10,
            float _01, float _11) : this(
                new vec2(_00, _10),
                new vec2(_01, _11))
        { }
        public override string ToString() => "[" + C.Select(x => x.ToString()).Cat(", ") + "]";

        #endregion

        #region Operators

        public static mat2 operator *(mat2 a, mat2 b)
        {
            return Shader.TraceFunc(new mat2(
                a[0][0] * b[0][0] + a[1][0] * b[0][1],
                a[0][0] * b[1][0] + a[1][0] * b[1][1],
                a[0][1] * b[0][0] + a[1][1] * b[0][1],
                a[0][1] * b[1][0] + a[1][1] * b[1][1]), a, b);
        }

        public static vec2 operator *(mat2 a, vec2 b)
        {
            return Shader.TraceFunc(new vec2(
                a[0][0] * b[0] + a[1][0] * b[1],
                a[0][1] * b[0] + a[1][1] * b[1]), a, b);
        }

        public static vec2 operator *(vec2 a, mat2 b)
        {
            return Shader.TraceFunc(new vec2(
                a[0] * b[0][0] + a[1] * b[0][1],
                a[0] * b[1][0] + a[1] * b[1][1]), a, b);
        }

        #endregion
    }
}
