using System;

namespace App.Glsl
{
    public class MathFunctions
    {
        #region Arithmetic

        private static float __abs(float a) => Math.Abs(a);
        private static double __abs(double a) => Math.Abs(a);
        public static float abs(float a) => Shader.TraceFunction(__abs(a), a);
        public static double abs(double a) => Shader.TraceFunction(__abs(a), a);
        public static vec2 abs(vec2 a) => Shader.TraceFunction(new vec2(__abs(a.x), __abs(a.y)), a);
        public static vec3 abs(vec3 a) => Shader.TraceFunction(new vec3(__abs(a.x), __abs(a.y), __abs(a.z)), a);
        public static vec4 abs(vec4 a) => Shader.TraceFunction(new vec4(__abs(a.x), __abs(a.y), __abs(a.z), __abs(a.w)), a);
        private static float __fract(float a) => a - (int)a;
        private static double __fract(double a) => a - (long)a;
        public static float fract(float a) => Shader.TraceFunction(__fract(a), a);
        public static double fract(double a) => Shader.TraceFunction(__fract(a), a);
        public static vec2 fract(vec2 a) => Shader.TraceFunction(new vec2(__fract(a.x), __fract(a.y)), a);
        public static vec3 fract(vec3 a) => Shader.TraceFunction(new vec3(__fract(a.x), __fract(a.y), __fract(a.z)), a);
        public static vec4 fract(vec4 a) => Shader.TraceFunction(new vec4(__fract(a.x), __fract(a.y), __fract(a.z), __fract(a.w)), a);

        #endregion

        #region Trigonometry

        public static float __sin(float a) => (float)Math.Sin(a);
        public static double __sin(double a) => Math.Sin(a);
        public static float sin(float a) => Shader.TraceFunction(__sin(a), a);
        public static double sin(double a) => Shader.TraceFunction(__sin(a), a);
        public static vec2 sin(vec2 a) => Shader.TraceFunction(new vec2(__sin(a.x), __sin(a.y)), a);
        public static vec3 sin(vec3 a) => Shader.TraceFunction(new vec3(__sin(a.x), __sin(a.y), __sin(a.z)), a);
        public static vec4 sin(vec4 a) => Shader.TraceFunction(new vec4(__sin(a.x), __sin(a.y), __sin(a.z), __sin(a.w)), a);
        public static float __cos(float a) => (float)Math.Cos(a);
        public static double __cos(double a) => Math.Cos(a);
        public static float cos(float a) => Shader.TraceFunction(__cos(a), a);
        public static double cos(double a) => Shader.TraceFunction(__cos(a), a);
        public static vec2 cos(vec2 a) => Shader.TraceFunction(new vec2(__cos(a.x), __cos(a.y)), a);
        public static vec3 cos(vec3 a) => Shader.TraceFunction(new vec3(__cos(a.x), __cos(a.y), __cos(a.z)), a);
        public static vec4 cos(vec4 a) => Shader.TraceFunction(new vec4(__cos(a.x), __cos(a.y), __cos(a.z), __cos(a.w)), a);
        public static float __tan(float a) => (float)Math.Tan(a);
        public static double __tan(double a) => Math.Tan(a);
        public static float tan(float a) => Shader.TraceFunction(__tan(a), a);
        public static double tan(double a) => Shader.TraceFunction(__tan(a), a);
        public static vec2 tan(vec2 a) => Shader.TraceFunction(new vec2(__tan(a.x), __tan(a.y)), a);
        public static vec3 tan(vec3 a) => Shader.TraceFunction(new vec3(__tan(a.x), __tan(a.y), __tan(a.z)), a);
        public static vec4 tan(vec4 a) => Shader.TraceFunction(new vec4(__tan(a.x), __tan(a.y), __tan(a.z), __tan(a.w)), a);
        public static float dot(vec2 a, vec2 b) => Shader.TraceFunction(a.x * b.x + a.y * b.y, a, b);
        public static float dot(vec3 a, vec3 b) => Shader.TraceFunction(a.x * b.x + a.y * b.y + a.z * b.z, a, b);
        public static float dot(vec4 a, vec4 b) => Shader.TraceFunction(a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w, a, b);
        public static vec3 cross(vec3 a, vec3 b) => Shader.TraceFunction(new vec3(a.y * b.z - a.z * b.y, a.x * b.z - a.z * b.x, a.x * b.y - a.y * b.x), a, b);

        #endregion

        #region Calculus

        public static float dFdx(float a) { return a; }
        public static double dFdx(double a) { return a; }
        public static vec2 dFdx(vec2 a) { return a; }
        public static vec3 dFdx(vec3 a) { return a; }
        public static vec4 dFdx(vec4 a) { return a; }
        public static float dFdy(float a) { return a; }
        public static double dFdy(double a) { return a; }
        public static vec2 dFdy(vec2 a) { return a; }
        public static vec3 dFdy(vec3 a) { return a; }
        public static vec4 dFdy(vec4 a) { return a; }

        #endregion

        #region Inequalities

        public static bool isnan(float a) => Shader.TraceFunction(float.IsNaN(a), a);
        public static bool isnan(double a) => Shader.TraceFunction(double.IsNaN(a), a);
        public static bvec2 isnan(vec2 a) => Shader.TraceFunction(new bvec2(float.IsNaN(a.x), float.IsNaN(a.y)), a);
        public static bvec3 isnan(vec3 a) => Shader.TraceFunction(new bvec3(float.IsNaN(a.x), float.IsNaN(a.y), float.IsNaN(a.z)), a);
        public static bvec4 isnan(vec4 a) => Shader.TraceFunction(new bvec4(float.IsNaN(a.x), float.IsNaN(a.y), float.IsNaN(a.z), float.IsNaN(a.w)), a);
        public static bvec2 lessThan(vec2 a, vec2 b) => Shader.TraceFunction(new bvec2(a.x < b.x, a.y < b.y), a, b);
        public static bvec3 lessThan(vec3 a, vec3 b) => Shader.TraceFunction(new bvec3(a.x < b.x, a.y < b.y, a.z < b.z), a, b);
        public static bvec4 lessThan(vec4 a, vec4 b) => Shader.TraceFunction(new bvec4(a.x < b.x, a.y < b.y, a.z < b.z, a.w < b.w), a, b);
        public static bvec2 lessThanEqual(vec2 a, vec2 b) => Shader.TraceFunction(new bvec2(a.x <= b.x, a.y <= b.y), a, b);
        public static bvec3 lessThanEqual(vec3 a, vec3 b) => Shader.TraceFunction(new bvec3(a.x <= b.x, a.y <= b.y, a.z <= b.z), a, b);
        public static bvec4 lessThanEqual(vec4 a, vec4 b) => Shader.TraceFunction(new bvec4(a.x <= b.x, a.y <= b.y, a.z <= b.z, a.w <= b.w), a, b);
        public static bvec2 greaterThan(vec2 a, vec2 b) => Shader.TraceFunction(new bvec2(a.x > b.x, a.y > b.y), a, b);
        public static bvec3 greaterThan(vec3 a, vec3 b) => Shader.TraceFunction(new bvec3(a.x > b.x, a.y > b.y, a.z > b.z), a, b);
        public static bvec4 greaterThan(vec4 a, vec4 b) => Shader.TraceFunction(new bvec4(a.x > b.x, a.y > b.y, a.z > b.z, a.w > b.w), a, b);
        public static bvec2 greaterThanEqual(vec2 a, vec2 b) => Shader.TraceFunction(new bvec2(a.x >= b.x, a.y >= b.y), a, b);
        public static bvec3 greaterThanEqual(vec3 a, vec3 b) => Shader.TraceFunction(new bvec3(a.x >= b.x, a.y >= b.y, a.z >= b.z), a, b);
        public static bvec4 greaterThanEqual(vec4 a, vec4 b) => Shader.TraceFunction(new bvec4(a.x >= b.x, a.y >= b.y, a.z >= b.z, a.w >= b.w), a, b);
        public static bool any(bvec2 a) => Shader.TraceFunction(a.x || a.y, a);
        public static bool any(bvec3 a) => Shader.TraceFunction(a.x || a.y || a.z, a);
        public static bool any(bvec4 a) => Shader.TraceFunction(a.x || a.y || a.z || a.w, a);
        public static bool all(bvec2 a) => Shader.TraceFunction(a.x && a.y, a);
        public static bool all(bvec3 a) => Shader.TraceFunction(a.x && a.y && a.z, a);
        public static bool all(bvec4 a) => Shader.TraceFunction(a.x && a.y && a.z && a.w, a);

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
            float _01, float _11)
            => new mat2(
                _00, _10,
                _01, _11);
        public static mat3 mat3(float a) => new mat3(a);
        public static mat3 mat3(vec3 a, vec3 b, vec3 c) => new mat3(a, b, c);
        public static mat3 mat3(
            float _00, float _10, float _20,
            float _01, float _11, float _21,
            float _02, float _12, float _22)
            => new mat3(
                _00, _10, _20,
                _01, _11, _21,
                _02, _12, _22);
        public static mat4 mat4(float a) => new mat4(a);
        public static mat4 mat4(vec4 a, vec4 b, vec4 c, vec4 d) => new mat4(a, b, c, d);
        public static mat4 mat4(
            float _00, float _10, float _20, float _30,
            float _01, float _11, float _21, float _31,
            float _02, float _12, float _22, float _32,
            float _03, float _13, float _23, float _33)
            => new mat4(
                _00, _10, _20, _30,
                _01, _11, _21, _31,
                _02, _12, _22, _32,
                _03, _13, _23, _33);

        #endregion
    }
}
