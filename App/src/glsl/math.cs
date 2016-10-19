using System;

namespace App.Glsl
{
    class Math
    {
        #region Arithmetic

        public static float abs(float a) => System.Math.Abs(a);
        public static double abs(double a) => System.Math.Abs(a);
        public static vec2 abs(vec2 a) => new vec2(abs(a.x), abs(a.y));
        public static vec3 abs(vec3 a) => new vec3(abs(a.x), abs(a.y), abs(a.z));
        public static vec4 abs(vec4 a) => new vec4(abs(a.x), abs(a.y), abs(a.z), abs(a.w));
        public static float fract(float a) => a - (int)a;
        public static double fract(double a) => a - (long)a;
        public static vec2 fract(vec2 a) => new vec2(fract(a.x), fract(a.y));
        public static vec3 fract(vec3 a) => new vec3(fract(a.x), fract(a.y), fract(a.z));
        public static vec4 fract(vec4 a) => new vec4(fract(a.x), fract(a.y), fract(a.z), fract(a.w));

        #endregion

        #region Trigonometry

        public static float sin(float a) => (float)System.Math.Sin(a);
        public static double sin(double a) => System.Math.Sin(a);
        public static vec2 sin(vec2 a) => new vec2(sin(a.x), sin(a.y));
        public static vec3 sin(vec3 a) => new vec3(sin(a.x), sin(a.y), sin(a.z));
        public static vec4 sin(vec4 a) => new vec4(sin(a.x), sin(a.y), sin(a.z), sin(a.w));
        public static float cos(float a) => (float)System.Math.Cos(a);
        public static double cos(double a) => System.Math.Cos(a);
        public static vec2 cos(vec2 a) => new vec2(cos(a.x), cos(a.y));
        public static vec3 cos(vec3 a) => new vec3(cos(a.x), cos(a.y), cos(a.z));
        public static vec4 cos(vec4 a) => new vec4(cos(a.x), cos(a.y), cos(a.z), cos(a.w));
        public static float tan(float a) => (float)System.Math.Tan(a);
        public static double tan(double a) => System.Math.Tan(a);
        public static vec2 tan(vec2 a) => new vec2(tan(a.x), tan(a.y));
        public static vec3 tan(vec3 a) => new vec3(tan(a.x), tan(a.y), tan(a.z));
        public static vec4 tan(vec4 a) => new vec4(tan(a.x), tan(a.y), tan(a.z), tan(a.w));
        public static float dot(vec2 a, vec2 b) => a.x * b.x + a.y * b.y;
        public static float dot(vec3 a, vec3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
        public static float dot(vec4 a, vec4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static vec3 cross(vec3 a, vec3 b) => new vec3(a.y * b.z - a.z * b.y, a.x * b.z - a.z * b.x, a.x * b.y - a.y * b.x);

        #endregion

        #region Calculus

        public static float dFdx(float a) { throw new NotImplementedException(); }
        public static double dFdx(double a) { throw new NotImplementedException(); }
        public static vec2 dFdx(vec2 a) { throw new NotImplementedException(); }
        public static vec3 dFdx(vec3 a) { throw new NotImplementedException(); }
        public static vec4 dFdx(vec4 a) { throw new NotImplementedException(); }
        public static float dFdy(float a) { throw new NotImplementedException(); }
        public static double dFdy(double a) { throw new NotImplementedException(); }
        public static vec2 dFdy(vec2 a) { throw new NotImplementedException(); }
        public static vec3 dFdy(vec3 a) { throw new NotImplementedException(); }
        public static vec4 dFdy(vec4 a) { throw new NotImplementedException(); }

        #endregion

        #region Inequalities

        public static bool isnan(float a) => float.IsNaN(a);
        public static bool isnan(double a) => double.IsNaN(a);
        public static bvec2 isnan(vec2 a) => new bvec2(isnan(a.x), isnan(a.y));
        public static bvec3 isnan(vec3 a) => new bvec3(isnan(a.x), isnan(a.y), isnan(a.z));
        public static bvec4 isnan(vec4 a) => new bvec4(isnan(a.x), isnan(a.y), isnan(a.z), isnan(a.w));
        public static bvec2 lessThan(vec2 a, vec2 b) => new bvec2(a.x < b.x, a.y < b.y);
        public static bvec3 lessThan(vec3 a, vec3 b) => new bvec3(a.x < b.x, a.y < b.y, a.z < b.z);
        public static bvec4 lessThan(vec4 a, vec4 b) => new bvec4(a.x < b.x, a.y < b.y, a.z < b.z, a.w < b.w);
        public static bvec2 lessThanEqual(vec2 a, vec2 b) => new bvec2(a.x <= b.x, a.y <= b.y);
        public static bvec3 lessThanEqual(vec3 a, vec3 b) => new bvec3(a.x <= b.x, a.y <= b.y, a.z <= b.z);
        public static bvec4 lessThanEqual(vec4 a, vec4 b) => new bvec4(a.x <= b.x, a.y <= b.y, a.z <= b.z, a.w <= b.w);
        public static bvec2 greaterThan(vec2 a, vec2 b) => new bvec2(a.x > b.x, a.y > b.y);
        public static bvec3 greaterThan(vec3 a, vec3 b) => new bvec3(a.x > b.x, a.y > b.y, a.z > b.z);
        public static bvec4 greaterThan(vec4 a, vec4 b) => new bvec4(a.x > b.x, a.y > b.y, a.z > b.z, a.w > b.w);
        public static bvec2 greaterThanEqual(vec2 a, vec2 b) => new bvec2(a.x >= b.x, a.y >= b.y);
        public static bvec3 greaterThanEqual(vec3 a, vec3 b) => new bvec3(a.x >= b.x, a.y >= b.y, a.z >= b.z);
        public static bvec4 greaterThanEqual(vec4 a, vec4 b) => new bvec4(a.x >= b.x, a.y >= b.y, a.z >= b.z, a.w >= b.w);
        public static bool any(bvec2 a) => a.x || a.y;
        public static bool any(bvec3 a) => a.x || a.y || a.z;
        public static bool any(bvec4 a) => a.x || a.y || a.z || a.w;
        public static bool all(bvec2 a) => a.x && a.y;
        public static bool all(bvec3 a) => a.x && a.y && a.z;
        public static bool all(bvec4 a) => a.x && a.y && a.z && a.w;

        #endregion

        #region Constructors

        public static vec2 vec2(float a) => new vec2(a);
        public static vec2 vec2(float x, float y) => new vec2(x, y);
        public static vec3 vec3(float a) => new vec3(a);
        public static vec3 vec3(float x, float y, float z) => new vec3(x, y, z);
        public static vec4 vec4(float a) => new vec4(a);
        public static vec4 vec4(float x, float y, float z, float w) => new vec4(x, y, z, w);
        public static dvec2 dvec2(double a) => new dvec2(a);
        public static dvec2 dvec2(double x, double y) => new dvec2(x, y);
        public static dvec3 dvec3(double a) => new dvec3(a);
        public static dvec3 dvec3(double x, double y, double z) => new dvec3(x, y, z);
        public static dvec4 dvec4(double a) => new dvec4(a);
        public static dvec4 dvec4(double x, double y, double z, double w) => new dvec4(x, y, z, w);
        public static ivec2 ivec2(int a) => new ivec2(a);
        public static ivec2 ivec2(int x, int y) => new ivec2(x, y);
        public static ivec3 ivec3(int a) => new ivec3(a);
        public static ivec3 ivec3(int x, int y, int z) => new ivec3(x, y, z);
        public static ivec4 ivec4(int a) => new ivec4(a);
        public static ivec4 ivec4(int x, int y, int z, int w) => new ivec4(x, y, z, w);
        public static uvec2 uvec2(uint a) => new uvec2(a);
        public static uvec2 uvec2(uint x, uint y) => new uvec2(x, y);
        public static uvec3 uvec3(uint a) => new uvec3(a);
        public static uvec3 uvec3(uint x, uint y, uint z) => new uvec3(x, y, z);
        public static uvec4 uvec4(uint a) => new uvec4(a);
        public static uvec4 uvec4(uint x, uint y, uint z, uint w) => new uvec4(x, y, z, w);
        public static mat2 mat2(float a) => new mat2(a);
        public static mat2 mat2(vec2 a, vec2 b) => new mat2(a, b);
        public static mat2 mat2(
            float _00, float _10,
            float _01, float _11) => new mat2(
                _00, _10,
                _01, _11);
        public static mat3 mat3(float a) => new mat3(a);
        public static mat3 mat3(vec3 a, vec3 b, vec3 c) => new mat3(a, b, c);
        public static mat3 mat3(
            float _00, float _10, float _20,
            float _01, float _11, float _21,
            float _02, float _12, float _22) => new mat3(
                _00, _10, _20,
                _01, _11, _21,
                _02, _12, _22);
        public static mat4 mat4(float a) => new mat4(a);
        public static mat4 mat4(vec4 a, vec4 b, vec4 c, vec4 d) => new mat4(a, b, c, d);
        public static mat4 mat4(
            float _00, float _10, float _20, float _30,
            float _01, float _11, float _21, float _31,
            float _02, float _12, float _22, float _32,
            float _03, float _13, float _23, float _33) => new mat4(
                _00, _10, _20, _30,
                _01, _11, _21, _31,
                _02, _12, _22, _32,
                _03, _13, _23, _33);

        #endregion
    }
}
