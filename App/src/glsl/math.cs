using System;

#pragma warning disable IDE1006

namespace App.Glsl
{
    public class MathFunctions
    {
        #region Arithmetic

        private static float __min(float a, float b)
        {
            return Math.Min(a, b);
        }

        private static double __min(double a, double b)
        {
            return Math.Min(a, b);
        }

        public static float min(float a, float b)
        {
            return __min(a, b);
        }

        public static double min(double a, double b)
        {
            return __min(a, b);
        }

        public static vec2 min(vec2 a, vec2 b)
        {
            return new vec2(__min(a.x, b.x), __min(a.y, b.y));
        }

        public static vec3 min(vec3 a, vec3 b)
        {
            return new vec3(__min(a.x, b.x), __min(a.y, b.y), __min(a.z, b.z));
        }

        public static vec4 min(vec4 a, vec4 b)
        {
            return new vec4(__min(a.x, b.x), __min(a.y, b.y), __min(a.z, b.z), __min(a.w, a.w));
        }

        public static vec2 min(vec2 a, float b)
        {
            return new vec2(__min(a.x, b), __min(a.y, b));
        }

        public static vec3 min(vec3 a, float b)
        {
            return new vec3(__min(a.x, b), __min(a.y, b), __min(a.z, b));
        }

        public static vec4 min(vec4 a, float b)
        {
            return new vec4(__min(a.x, b), __min(a.y, b), __min(a.z, b), __min(a.w, b));
        }

        public static vec2 min(float a, vec2 b)
        {
            return new vec2(__min(a, b.x), __min(a, b.y));
        }

        public static vec3 min(float a, vec3 b)
        {
            return new vec3(__min(a, b.x), __min(a, b.y), __min(a, b.z));
        }

        public static vec4 min(float a, vec4 b)
        {
            return new vec4(__min(a, b.x), __min(a, b.y), __min(a, b.z), __min(a, b.w));
        }

        public static dvec2 min(dvec2 a, double b)
        {
            return new dvec2(__min(a.x, b), __min(a.y, b));
        }

        public static dvec3 min(dvec3 a, double b)
        {
            return new dvec3(__min(a.x, b), __min(a.y, b), __min(a.z, b));
        }

        public static dvec4 min(dvec4 a, double b)
        {
            return new dvec4(__min(a.x, b), __min(a.y, b), __min(a.z, b), __min(a.w, b));
        }

        private static float __max(float a, float b)
        {
            return Math.Max(a, b);
        }

        private static double __max(double a, double b)
        {
            return Math.Max(a, b);
        }

        public static float max(float a, float b)
        {
            return __max(a, b);
        }

        public static double max(double a, double b)
        {
            return __max(a, b);
        }

        public static vec2 max(vec2 a, vec2 b)
        {
            return new vec2(__max(a.x, b.x), __max(a.y, b.y));
        }

        public static vec3 max(vec3 a, vec3 b)
        {
            return new vec3(__max(a.x, b.x), __max(a.y, b.y), __max(a.z, b.z));
        }

        public static vec4 max(vec4 a, vec4 b)
        {
            return new vec4(__max(a.x, b.x), __max(a.y, b.y), __max(a.z, b.z), __max(a.w, a.w));
        }

        public static vec2 max(vec2 a, float b)
        {
            return new vec2(__max(a.x, b), __max(a.y, b));
        }

        public static vec3 max(vec3 a, float b)
        {
            return new vec3(__max(a.x, b), __max(a.y, b), __max(a.z, b));
        }

        public static vec4 max(vec4 a, float b)
        {
            return new vec4(__max(a.x, b), __max(a.y, b), __max(a.z, b), __max(a.w, b));
        }

        public static vec2 max(float a, vec2 b)
        {
            return new vec2(__max(a, b.x), __max(a, b.y));
        }

        public static vec3 max(float a, vec3 b)
        {
            return new vec3(__max(a, b.x), __max(a, b.y), __max(a, b.z));
        }

        public static vec4 max(float a, vec4 b)
        {
            return new vec4(__max(a, b.x), __max(a, b.y), __max(a, b.z), __max(a, b.w));
        }

        public static dvec2 max(dvec2 a, double b)
        {
            return new dvec2(__max(a.x, b), __max(a.y, b));
        }

        public static dvec3 max(dvec3 a, double b)
        {
            return new dvec3(__max(a.x, b), __max(a.y, b), __max(a.z, b));
        }

        public static dvec4 max(dvec4 a, double b)
        {
            return new dvec4(__max(a.x, b), __max(a.y, b), __max(a.z, b), __max(a.w, b));
        }

        private static float __abs(float a)
        {
            return Math.Abs(a);
        }

        private static double __abs(double a)
        {
            return Math.Abs(a);
        }

        public static float abs(float a)
        {
            return __abs(a);
        }

        public static double abs(double a)
        {
            return __abs(a);
        }

        public static vec2 abs(vec2 a)
        {
            return new vec2(__abs(a.x), __abs(a.y));
        }

        public static vec3 abs(vec3 a)
        {
            return new vec3(__abs(a.x), __abs(a.y), __abs(a.z));
        }

        public static vec4 abs(vec4 a)
        {
            return new vec4(__abs(a.x), __abs(a.y), __abs(a.z), __abs(a.w));
        }

        public static dvec2 abs(dvec2 a)
        {
            return new dvec2(__abs(a.x), __abs(a.y));
        }

        public static dvec3 abs(dvec3 a)
        {
            return new dvec3(__abs(a.x), __abs(a.y), __abs(a.z));
        }

        public static dvec4 abs(dvec4 a)
        {
            return new dvec4(__abs(a.x), __abs(a.y), __abs(a.z), __abs(a.w));
        }

        private static float __fract(float a)
        {
            return a - (int)a;
        }

        private static double __fract(double a)
        {
            return a - (long)a;
        }

        public static float fract(float a)
        {
            return __fract(a);
        }

        public static double fract(double a)
        {
            return __fract(a);
        }

        public static vec2 fract(vec2 a)
        {
            return new vec2(__fract(a.x), __fract(a.y));
        }

        public static vec3 fract(vec3 a)
        {
            return new vec3(__fract(a.x), __fract(a.y), __fract(a.z));
        }

        public static vec4 fract(vec4 a)
        {
            return new vec4(__fract(a.x), __fract(a.y), __fract(a.z), __fract(a.w));
        }

        public static dvec2 fract(dvec2 a)
        {
            return new dvec2(__fract(a.x), __fract(a.y));
        }

        public static dvec3 fract(dvec3 a)
        {
            return new dvec3(__fract(a.x), __fract(a.y), __fract(a.z));
        }

        public static dvec4 fract(dvec4 a)
        {
            return new dvec4(__fract(a.x), __fract(a.y), __fract(a.z), __fract(a.w));
        }

        public static float __sqrt(float a)
        {
            return (float)Math.Sqrt(a);
        }

        public static double __sqrt(double a)
        {
            return Math.Sqrt(a);
        }

        public static float sqrt(float a)
        {
            return __sqrt(a);
        }

        public static double sqrt(double a)
        {
            return __sqrt(a);
        }

        public static vec2 sqrt(vec2 a)
        {
            return new vec2(__sqrt(a.x), __sqrt(a.y));
        }

        public static vec3 sqrt(vec3 a)
        {
            return new vec3(__sqrt(a.x), __sqrt(a.y), __sqrt(a.z));
        }

        public static vec4 sqrt(vec4 a)
        {
            return new vec4(__sqrt(a.x), __sqrt(a.y), __sqrt(a.z), __sqrt(a.w));
        }

        public static dvec2 sqrt(dvec2 a)
        {
            return new dvec2(__sqrt(a.x), __sqrt(a.y));
        }

        public static dvec3 sqrt(dvec3 a)
        {
            return new dvec3(__sqrt(a.x), __sqrt(a.y), __sqrt(a.z));
        }

        public static dvec4 sqrt(dvec4 a)
        {
            return new dvec4(__sqrt(a.x), __sqrt(a.y), __sqrt(a.z), __sqrt(a.w));
        }

        public static float __pow(float a, float exp)
        {
            return (float)Math.Pow(a, exp);
        }

        public static double __pow(double a, double exp)
        {
            return Math.Pow(a, exp);
        }

        public static float pow(float a, float exp)
        {
            return __pow(a, exp);
        }

        public static double pow(double a, double exp)
        {
            return __pow(a, exp);
        }

        public static float __exp(float a)
        {
            return (float)Math.Sqrt(a);
        }

        public static double __exp(double a)
        {
            return Math.Sqrt(a);
        }

        public static float exp(float a)
        {
            return __exp(a);
        }

        public static double exp(double a)
        {
            return __exp(a);
        }

        public static float mix(float a, float b, float t)
        {
            return a * (1 - t) + b * t;
        }

        public static vec2 mix(vec2 a, vec2 b, float t)
        {
            return a * (1 - t) + b * t;
        }

        public static vec3 mix(vec3 a, vec3 b, float t)
        {
            return a * (1 - t) + b * t;
        }

        public static vec4 mix(vec4 a, vec4 b, float t)
        {
            return a * (1 - t) + b * t;
        }

        public static vec2 mix(vec2 a, vec2 b, vec2 t)
        {
            return a * (1 - t) + b * t;
        }

        public static vec3 mix(vec3 a, vec3 b, vec3 t)
        {
            return a * (1 - t) + b * t;
        }

        public static vec4 mix(vec4 a, vec4 b, vec4 t)
        {
            return a * (1 - t) + b * t;
        }

        public static double mix(double a, double b, double t)
        {
            return a * (1 - t) + b * t;
        }

        public static dvec2 mix(dvec2 a, dvec2 b, double t)
        {
            return a * (1 - t) + b * t;
        }

        public static dvec3 mix(dvec3 a, dvec3 b, double t)
        {
            return a * (1 - t) + b * t;
        }

        public static dvec4 mix(dvec4 a, dvec4 b, double t)
        {
            return a * (1 - t) + b * t;
        }

        public static dvec2 mix(dvec2 a, dvec2 b, dvec2 t)
        {
            return a * (1 - t) + b * t;
        }

        public static dvec3 mix(dvec3 a, dvec3 b, dvec3 t)
        {
            return a * (1 - t) + b * t;
        }

        public static dvec4 mix(dvec4 a, dvec4 b, dvec4 t)
        {
            return a * (1 - t) + b * t;
        }

        public static float mix(float a, float b, bool t)
        {
            return t ? a : b;
        }

        public static vec2 mix(vec2 a, vec2 b, bvec2 t)
        {
            return new vec2(t.x ? a.x : b.x, t.y ? a.y : b.y);
        }

        public static vec3 mix(vec3 a, vec3 b, bvec3 t)
        {
            return new vec3(t.x ? a.x : b.x, t.y ? a.y : b.y, t.z ? a.z : b.z);
        }

        public static vec4 mix(vec4 a, vec4 b, bvec4 t)
        {
            return new vec4(t.x ? a.x : b.x, t.y ? a.y : b.y, t.z ? a.z : b.z, t.w ? a.w : b.w);
        }

        public static double mix(double a, double b, bool t)
        {
            return t ? a : b;
        }

        public static dvec2 mix(dvec2 a, dvec2 b, bvec2 t)
        {
            return new dvec2(t.x ? a.x : b.x, t.y ? a.y : b.y);
        }

        public static dvec3 mix(dvec3 a, dvec3 b, bvec3 t)
        {
            return new dvec3(t.x ? a.x : b.x, t.y ? a.y : b.y, t.z ? a.z : b.z);
        }

        public static dvec4 mix(dvec4 a, dvec4 b, bvec4 t)
        {
            return new dvec4(t.x ? a.x : b.x, t.y ? a.y : b.y, t.z ? a.z : b.z, t.w ? a.w : b.w);
        }

        public static int mix(int a, int b, bool t)
        {
            return t ? a : b;
        }

        public static ivec2 mix(ivec2 a, ivec2 b, bvec2 t)
        {
            return new ivec2(t.x ? a.x : b.x, t.y ? a.y : b.y);
        }

        public static ivec3 mix(ivec3 a, ivec3 b, bvec3 t)
        {
            return new ivec3(t.x ? a.x : b.x, t.y ? a.y : b.y, t.z ? a.z : b.z);
        }

        public static ivec4 mix(ivec4 a, ivec4 b, bvec4 t)
        {
            return new ivec4(t.x ? a.x : b.x, t.y ? a.y : b.y, t.z ? a.z : b.z, t.w ? a.w : b.w);
        }

        public static uint mix(uint a, uint b, bool t)
        {
            return t ? a : b;
        }

        public static uvec2 mix(uvec2 a, uvec2 b, bvec2 t)
        {
            return new uvec2(t.x ? a.x : b.x, t.y ? a.y : b.y);
        }

        public static uvec3 mix(uvec3 a, uvec3 b, bvec3 t)
        {
            return new uvec3(t.x ? a.x : b.x, t.y ? a.y : b.y, t.z ? a.z : b.z);
        }

        public static uvec4 mix(uvec4 a, uvec4 b, bvec4 t)
        {
            return new uvec4(t.x ? a.x : b.x, t.y ? a.y : b.y, t.z ? a.z : b.z, t.w ? a.w : b.w);
        }

        #endregion

        #region Trigonometry

        public static float __sin(float a)
        {
            return (float)Math.Sin(a);
        }

        public static double __sin(double a)
        {
            return Math.Sin(a);
        }

        public static float sin(float a)
        {
            return __sin(a);
        }

        public static double sin(double a)
        {
            return __sin(a);
        }

        public static vec2 sin(vec2 a)
        {
            return new vec2(__sin(a.x), __sin(a.y));
        }

        public static vec3 sin(vec3 a)
        {
            return new vec3(__sin(a.x), __sin(a.y), __sin(a.z));
        }

        public static vec4 sin(vec4 a)
        {
            return new vec4(__sin(a.x), __sin(a.y), __sin(a.z), __sin(a.w));
        }

        public static float __cos(float a)
        {
            return (float)Math.Cos(a);
        }

        public static double __cos(double a)
        {
            return Math.Cos(a);
        }

        public static float cos(float a)
        {
            return __cos(a);
        }

        public static double cos(double a)
        {
            return __cos(a);
        }

        public static vec2 cos(vec2 a)
        {
            return new vec2(__cos(a.x), __cos(a.y));
        }

        public static vec3 cos(vec3 a)
        {
            return new vec3(__cos(a.x), __cos(a.y), __cos(a.z));
        }

        public static vec4 cos(vec4 a)
        {
            return new vec4(__cos(a.x), __cos(a.y), __cos(a.z), __cos(a.w));
        }

        public static float __tan(float a)
        {
            return (float)Math.Tan(a);
        }

        public static double __tan(double a)
        {
            return Math.Tan(a);
        }

        public static float tan(float a)
        {
            return __tan(a);
        }

        public static double tan(double a)
        {
            return __tan(a);
        }

        public static vec2 tan(vec2 a)
        {
            return new vec2(__tan(a.x), __tan(a.y));
        }

        public static vec3 tan(vec3 a)
        {
            return new vec3(__tan(a.x), __tan(a.y), __tan(a.z));
        }

        public static vec4 tan(vec4 a)
        {
            return new vec4(__tan(a.x), __tan(a.y), __tan(a.z), __tan(a.w));
        }

        public static float dot(vec2 a, vec2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static float dot(vec3 a, vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static float dot(vec4 a, vec4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static vec3 cross(vec3 a, vec3 b)
        {
            return new vec3(a.y * b.z - a.z * b.y, a.x * b.z - a.z * b.x, a.x * b.y - a.y * b.x);
        }

        public static float length(vec2 a)
        {
            return sqrt(a.x * a.x + a.y * a.y);
        }

        public static float length(vec3 a)
        {
            return sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }

        public static float length(vec4 a)
        {
            return sqrt(a.x * a.x + a.y * a.y + a.z * a.z + a.w * a.w);
        }

        public static double length(dvec2 a)
        {
            return sqrt(a.x * a.x + a.y * a.y);
        }

        public static double length(dvec3 a)
        {
            return sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }

        public static double length(dvec4 a)
        {
            return sqrt(a.x * a.x + a.y * a.y + a.z * a.z + a.w * a.w);
        }

        public static vec2 normalize(vec2 a)
        {
            return a / length(a);
        }

        public static vec3 normalize(vec3 a)
        {
            return a / length(a);
        }

        public static vec4 normalize(vec4 a)
        {
            return a / length(a);
        }

        public static dvec2 normalize(dvec2 a)
        {
            return a / length(a);
        }

        public static dvec3 normalize(dvec3 a)
        {
            return a / length(a);
        }

        public static dvec4 normalize(dvec4 a)
        {
            return a / length(a);
        }

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

        public static bool isnan(float a)
        {
            return float.IsNaN(a);
        }

        public static bool isnan(double a)
        {
            return double.IsNaN(a);
        }

        public static bvec2 isnan(vec2 a)
        {
            return new bvec2(float.IsNaN(a.x), float.IsNaN(a.y));
        }

        public static bvec3 isnan(vec3 a)
        {
            return new bvec3(float.IsNaN(a.x), float.IsNaN(a.y), float.IsNaN(a.z));
        }

        public static bvec4 isnan(vec4 a)
        {
            return new bvec4(float.IsNaN(a.x), float.IsNaN(a.y), float.IsNaN(a.z), float.IsNaN(a.w));
        }

        public static bvec2 lessThan(vec2 a, vec2 b)
        {
            return new bvec2(a.x < b.x, a.y < b.y);
        }

        public static bvec3 lessThan(vec3 a, vec3 b)
        {
            return new bvec3(a.x < b.x, a.y < b.y, a.z < b.z);
        }

        public static bvec4 lessThan(vec4 a, vec4 b)
        {
            return new bvec4(a.x < b.x, a.y < b.y, a.z < b.z, a.w < b.w);
        }

        public static bvec2 lessThanEqual(vec2 a, vec2 b)
        {
            return new bvec2(a.x <= b.x, a.y <= b.y);
        }

        public static bvec3 lessThanEqual(vec3 a, vec3 b)
        {
            return new bvec3(a.x <= b.x, a.y <= b.y, a.z <= b.z);
        }

        public static bvec4 lessThanEqual(vec4 a, vec4 b)
        {
            return new bvec4(a.x <= b.x, a.y <= b.y, a.z <= b.z, a.w <= b.w);
        }

        public static bvec2 greaterThan(vec2 a, vec2 b)
        {
            return new bvec2(a.x > b.x, a.y > b.y);
        }

        public static bvec3 greaterThan(vec3 a, vec3 b)
        {
            return new bvec3(a.x > b.x, a.y > b.y, a.z > b.z);
        }

        public static bvec4 greaterThan(vec4 a, vec4 b)
        {
            return new bvec4(a.x > b.x, a.y > b.y, a.z > b.z, a.w > b.w);
        }

        public static bvec2 greaterThanEqual(vec2 a, vec2 b)
        {
            return new bvec2(a.x >= b.x, a.y >= b.y);
        }

        public static bvec3 greaterThanEqual(vec3 a, vec3 b)
        {
            return new bvec3(a.x >= b.x, a.y >= b.y, a.z >= b.z);
        }

        public static bvec4 greaterThanEqual(vec4 a, vec4 b)
        {
            return new bvec4(a.x >= b.x, a.y >= b.y, a.z >= b.z, a.w >= b.w);
        }

        public static bool any(bvec2 a)
        {
            return a.x || a.y;
        }

        public static bool any(bvec3 a)
        {
            return a.x || a.y || a.z;
        }

        public static bool any(bvec4 a)
        {
            return a.x || a.y || a.z || a.w;
        }

        public static bool all(bvec2 a)
        {
            return a.x && a.y;
        }

        public static bool all(bvec3 a)
        {
            return a.x && a.y && a.z;
        }

        public static bool all(bvec4 a)
        {
            return a.x && a.y && a.z && a.w;
        }

        #endregion

        #region Constructors

        public static vec2 vec2(float a)
        {
            return new vec2(a);
        }

        public static vec2 vec2(float x, float y)
        {
            return new vec2(x, y);
        }

        public static vec3 vec3(float a)
        {
            return new vec3(a);
        }

        public static vec3 vec3(float x, float y, float z)
        {
            return new vec3(x, y, z);
        }

        public static vec3 vec3(vec2 xy, float z)
        {
            return new vec3(xy.x, xy.y, z);
        }

        public static vec3 vec3(float x, vec2 yz)
        {
            return new vec3(x, yz.x, yz.y);
        }

        public static vec4 vec4(float a)
        {
            return new vec4(a);
        }

        public static vec4 vec4(float x, float y, float z, float w)
        {
            return new vec4(x, y, z, w);
        }

        public static vec4 vec4(vec2 xy, float z, float w)
        {
            return new vec4(xy.x, xy.y, z, w);
        }

        public static vec4 vec4(float x, vec2 yz, float w)
        {
            return new vec4(x, yz.x, yz.y, w);
        }

        public static vec4 vec4(float x, float y, vec2 zw)
        {
            return new vec4(x, y, zw.x, zw.y);
        }

        public static vec4 vec4(vec2 xy, vec2 zw)
        {
            return new vec4(xy.x, xy.y, zw.x, zw.y);
        }

        public static vec4 vec4(vec3 xyz, float w)
        {
            return new vec4(xyz.x, xyz.y, xyz.z, w);
        }

        public static vec4 vec4(float x, vec3 yzw)
        {
            return new vec4(x, yzw.x, yzw.y, yzw.z);
        }

        public static dvec2 dvec2(double a)
        {
            return new dvec2(a);
        }

        public static dvec2 dvec2(double x, double y)
        {
            return new dvec2(x, y);
        }

        public static dvec3 dvec3(double a)
        {
            return new dvec3(a);
        }

        public static dvec3 dvec3(double x, double y, double z)
        {
            return new dvec3(x, y, z);
        }

        public static dvec3 dvec3(dvec2 xy, double z)
        {
            return new dvec3(xy.x, xy.y, z);
        }

        public static dvec3 dvec3(double x, dvec2 yz)
        {
            return new dvec3(x, yz.x, yz.y);
        }

        public static dvec4 dvec4(double a)
        {
            return new dvec4(a);
        }

        public static dvec4 dvec4(double x, double y, double z, double w)
        {
            return new dvec4(x, y, z, w);
        }

        public static dvec4 dvec4(dvec2 xy, double z, double w)
        {
            return new dvec4(xy.x, xy.y, z, w);
        }

        public static dvec4 dvec4(double x, dvec2 yz, double w)
        {
            return new dvec4(x, yz.x, yz.y, w);
        }

        public static dvec4 dvec4(double x, double y, dvec2 zw)
        {
            return new dvec4(x, y, zw.x, zw.y);
        }

        public static dvec4 dvec4(dvec2 xy, dvec2 zw)
        {
            return new dvec4(xy.x, xy.y, zw.x, zw.y);
        }

        public static dvec4 dvec4(vec3 xyz, double w)
        {
            return new dvec4(xyz.x, xyz.y, xyz.z, w);
        }

        public static dvec4 dvec4(double x, vec3 yzw)
        {
            return new dvec4(x, yzw.x, yzw.y, yzw.z);
        }

        public static ivec2 ivec2(int a)
        {
            return new ivec2(a);
        }

        public static ivec2 ivec2(int x, int y)
        {
            return new ivec2(x, y);
        }

        public static ivec3 ivec3(int a)
        {
            return new ivec3(a);
        }

        public static ivec3 ivec3(int x, int y, int z)
        {
            return new ivec3(x, y, z);
        }

        public static ivec3 ivec3(ivec2 xy, int z)
        {
            return new ivec3(xy.x, xy.y, z);
        }

        public static ivec3 ivec3(int x, ivec2 yz)
        {
            return new ivec3(x, yz.x, yz.y);
        }

        public static ivec4 ivec4(int a)
        {
            return new ivec4(a);
        }

        public static ivec4 ivec4(int x, int y, int z, int w)
        {
            return new ivec4(x, y, z, w);
        }

        public static ivec4 ivec4(ivec2 xy, int z, int w)
        {
            return new ivec4(xy.x, xy.y, z, w);
        }

        public static ivec4 ivec4(int x, ivec2 yz, int w)
        {
            return new ivec4(x, yz.x, yz.y, w);
        }

        public static ivec4 ivec4(int x, int y, ivec2 zw)
        {
            return new ivec4(x, y, zw.x, zw.y);
        }

        public static ivec4 ivec4(ivec2 xy, ivec2 zw)
        {
            return new ivec4(xy.x, xy.y, zw.x, zw.y);
        }

        public static ivec4 ivec4(ivec3 xyz, int w)
        {
            return new ivec4(xyz.x, xyz.y, xyz.z, w);
        }

        public static ivec4 ivec4(int x, ivec3 yzw)
        {
            return new ivec4(x, yzw.x, yzw.y, yzw.z);
        }

        public static uvec2 uvec2(uint a)
        {
            return new uvec2(a);
        }

        public static uvec2 uvec2(uint x, uint y)
        {
            return new uvec2(x, y);
        }

        public static uvec3 uvec3(uint a)
        {
            return new uvec3(a);
        }

        public static uvec3 uvec3(uint x, uint y, uint z)
        {
            return new uvec3(x, y, z);
        }

        public static uvec3 uvec3(uvec2 xy, uint z)
        {
            return new uvec3(xy.x, xy.y, z);
        }

        public static uvec3 uvec3(uint x, uvec2 yz)
        {
            return new uvec3(x, yz.x, yz.y);
        }

        public static uvec4 uvec4(uint a)
        {
            return new uvec4(a);
        }

        public static uvec4 uvec4(uint x, uint y, uint z, uint w)
        {
            return new uvec4(x, y, z, w);
        }

        public static uvec4 uvec4(uvec2 xy, uint z, uint w)
        {
            return new uvec4(xy.x, xy.y, z, w);
        }

        public static uvec4 uvec4(uint x, uvec2 yz, uint w)
        {
            return new uvec4(x, yz.x, yz.y, w);
        }

        public static uvec4 uvec4(uint x, uint y, uvec2 zw)
        {
            return new uvec4(x, y, zw.x, zw.y);
        }

        public static uvec4 uvec4(uvec2 xy, uvec2 zw)
        {
            return new uvec4(xy.x, xy.y, zw.x, zw.y);
        }

        public static uvec4 uvec4(uvec3 xyz, uint w)
        {
            return new uvec4(xyz.x, xyz.y, xyz.z, w);
        }

        public static uvec4 uvec4(uint x, uvec3 yzw)
        {
            return new uvec4(x, yzw.x, yzw.y, yzw.z);
        }

        public static mat2 mat2(float a)
        {
            return new mat2(a);
        }

        public static mat2 mat2(vec2 a, vec2 b)
        {
            return new mat2(a, b);
        }

        public static mat2 mat2(
           float _00, float _10,
           float _01, float _11)
        {
            return new mat2(
                           _00, _10,
                           _01, _11);
        }

        public static mat3 mat3(float a)
        {
            return new mat3(a);
        }

        public static mat3 mat3(vec3 a, vec3 b, vec3 c)
        {
            return new mat3(a, b, c);
        }

        public static mat3 mat3(
           float _00, float _10, float _20,
           float _01, float _11, float _21,
           float _02, float _12, float _22)
        {
            return new mat3(
                           _00, _10, _20,
                           _01, _11, _21,
                           _02, _12, _22);
        }

        public static mat4 mat4(float a)
        {
            return new mat4(a);
        }

        public static mat4 mat4(vec4 a, vec4 b, vec4 c, vec4 d)
        {
            return new mat4(a, b, c, d);
        }

        public static mat4 mat4(
           float _00, float _10, float _20, float _30,
           float _01, float _11, float _21, float _31,
           float _02, float _12, float _22, float _32,
           float _03, float _13, float _23, float _33)
        {
            return new mat4(
                           _00, _10, _20, _30,
                           _01, _11, _21, _31,
                           _02, _12, _22, _32,
                           _03, _13, _23, _33);
        }

        #endregion
    }
}
