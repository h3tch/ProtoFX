using System;

namespace App.Glsl
{
    public class MathFunctions
    {
        #region Arithmetic
        
        private static float __min(float a, float b) => Math.Min(a, b);
        private static double __min(double a, double b) => Math.Min(a, b);
        public static float min(float a, float b) => Shader.TraceFunction(__min(a, b), a, b);
        public static double min(double a, double b) => Shader.TraceFunction(__min(a, b), a, b);
        public static vec2 min(vec2 a, vec2 b) => Shader.TraceFunction(new vec2(__min(a.x, b.x), __min(a.y, b.y)), a, b);
        public static vec3 min(vec3 a, vec3 b) => Shader.TraceFunction(new vec3(__min(a.x, b.x), __min(a.y, b.y), __min(a.z, b.z)), a, b);
        public static vec4 min(vec4 a, vec4 b) => Shader.TraceFunction(new vec4(__min(a.x, b.x), __min(a.y, b.y), __min(a.z, b.z), __min(a.w, a.w)), a, b);
        public static vec2 min(vec2 a, float b) => Shader.TraceFunction(new vec2(__min(a.x, b), __min(a.y, b)), a, b);
        public static vec3 min(vec3 a, float b) => Shader.TraceFunction(new vec3(__min(a.x, b), __min(a.y, b), __min(a.z, b)), a, b);
        public static vec4 min(vec4 a, float b) => Shader.TraceFunction(new vec4(__min(a.x, b), __min(a.y, b), __min(a.z, b), __min(a.w, b)), a, b);
        public static vec2 min(float a, vec2 b) => Shader.TraceFunction(new vec2(__min(a, b.x), __min(a, b.y)), a, b);
        public static vec3 min(float a, vec3 b) => Shader.TraceFunction(new vec3(__min(a, b.x), __min(a, b.y), __min(a, b.z)), a, b);
        public static vec4 min(float a, vec4 b) => Shader.TraceFunction(new vec4(__min(a, b.x), __min(a, b.y), __min(a, b.z), __min(a, b.w)), a, b);
        public static dvec2 min(dvec2 a, double b) => Shader.TraceFunction(new dvec2(__min(a.x, b), __min(a.y, b)), a, b);
        public static dvec3 min(dvec3 a, double b) => Shader.TraceFunction(new dvec3(__min(a.x, b), __min(a.y, b), __min(a.z, b)), a, b);
        public static dvec4 min(dvec4 a, double b) => Shader.TraceFunction(new dvec4(__min(a.x, b), __min(a.y, b), __min(a.z, b), __min(a.w, b)), a, b);
        private static float __max(float a, float b) => Math.Max(a, b);
        private static double __max(double a, double b) => Math.Max(a, b);
        public static float max(float a, float b) => Shader.TraceFunction(__max(a, b), a, b);
        public static double max(double a, double b) => Shader.TraceFunction(__max(a, b), a, b);
        public static vec2 max(vec2 a, vec2 b) => Shader.TraceFunction(new vec2(__max(a.x, b.x), __max(a.y, b.y)), a, b);
        public static vec3 max(vec3 a, vec3 b) => Shader.TraceFunction(new vec3(__max(a.x, b.x), __max(a.y, b.y), __max(a.z, b.z)), a, b);
        public static vec4 max(vec4 a, vec4 b) => Shader.TraceFunction(new vec4(__max(a.x, b.x), __max(a.y, b.y), __max(a.z, b.z), __max(a.w, a.w)), a, b);
        public static vec2 max(vec2 a, float b) => Shader.TraceFunction(new vec2(__max(a.x, b), __max(a.y, b)), a, b);
        public static vec3 max(vec3 a, float b) => Shader.TraceFunction(new vec3(__max(a.x, b), __max(a.y, b), __max(a.z, b)), a, b);
        public static vec4 max(vec4 a, float b) => Shader.TraceFunction(new vec4(__max(a.x, b), __max(a.y, b), __max(a.z, b), __max(a.w, b)), a, b);
        public static vec2 max(float a, vec2 b) => Shader.TraceFunction(new vec2(__max(a, b.x), __max(a, b.y)), a, b);
        public static vec3 max(float a, vec3 b) => Shader.TraceFunction(new vec3(__max(a, b.x), __max(a, b.y), __max(a, b.z)), a, b);
        public static vec4 max(float a, vec4 b) => Shader.TraceFunction(new vec4(__max(a, b.x), __max(a, b.y), __max(a, b.z), __max(a, b.w)), a, b);
        public static dvec2 max(dvec2 a, double b) => Shader.TraceFunction(new dvec2(__max(a.x, b), __max(a.y, b)), a, b);
        public static dvec3 max(dvec3 a, double b) => Shader.TraceFunction(new dvec3(__max(a.x, b), __max(a.y, b), __max(a.z, b)), a, b);
        public static dvec4 max(dvec4 a, double b) => Shader.TraceFunction(new dvec4(__max(a.x, b), __max(a.y, b), __max(a.z, b), __max(a.w, b)), a, b);
        private static float __abs(float a) => Math.Abs(a);
        private static double __abs(double a) => Math.Abs(a);
        public static float abs(float a) => Shader.TraceFunction(__abs(a), a);
        public static double abs(double a) => Shader.TraceFunction(__abs(a), a);
        public static vec2 abs(vec2 a) => Shader.TraceFunction(new vec2(__abs(a.x), __abs(a.y)), a);
        public static vec3 abs(vec3 a) => Shader.TraceFunction(new vec3(__abs(a.x), __abs(a.y), __abs(a.z)), a);
        public static vec4 abs(vec4 a) => Shader.TraceFunction(new vec4(__abs(a.x), __abs(a.y), __abs(a.z), __abs(a.w)), a);
        public static dvec2 abs(dvec2 a) => Shader.TraceFunction(new dvec2(__abs(a.x), __abs(a.y)), a);
        public static dvec3 abs(dvec3 a) => Shader.TraceFunction(new dvec3(__abs(a.x), __abs(a.y), __abs(a.z)), a);
        public static dvec4 abs(dvec4 a) => Shader.TraceFunction(new dvec4(__abs(a.x), __abs(a.y), __abs(a.z), __abs(a.w)), a);
        private static float __fract(float a) => a - (int)a;
        private static double __fract(double a) => a - (long)a;
        public static float fract(float a) => Shader.TraceFunction(__fract(a), a);
        public static double fract(double a) => Shader.TraceFunction(__fract(a), a);
        public static vec2 fract(vec2 a) => Shader.TraceFunction(new vec2(__fract(a.x), __fract(a.y)), a);
        public static vec3 fract(vec3 a) => Shader.TraceFunction(new vec3(__fract(a.x), __fract(a.y), __fract(a.z)), a);
        public static vec4 fract(vec4 a) => Shader.TraceFunction(new vec4(__fract(a.x), __fract(a.y), __fract(a.z), __fract(a.w)), a);
        public static dvec2 fract(dvec2 a) => Shader.TraceFunction(new dvec2(__fract(a.x), __fract(a.y)), a);
        public static dvec3 fract(dvec3 a) => Shader.TraceFunction(new dvec3(__fract(a.x), __fract(a.y), __fract(a.z)), a);
        public static dvec4 fract(dvec4 a) => Shader.TraceFunction(new dvec4(__fract(a.x), __fract(a.y), __fract(a.z), __fract(a.w)), a);
        public static float __sqrt(float a) => (float)Math.Sqrt(a);
        public static double __sqrt(double a) => Math.Sqrt(a);
        public static float sqrt(float a) => Shader.TraceFunction(__sqrt(a), a);
        public static double sqrt(double a) => Shader.TraceFunction(__sqrt(a), a);
        public static vec2 sqrt(vec2 a) => Shader.TraceFunction(new vec2(__sqrt(a.x), __sqrt(a.y)), a);
        public static vec3 sqrt(vec3 a) => Shader.TraceFunction(new vec3(__sqrt(a.x), __sqrt(a.y), __sqrt(a.z)), a);
        public static vec4 sqrt(vec4 a) => Shader.TraceFunction(new vec4(__sqrt(a.x), __sqrt(a.y), __sqrt(a.z), __sqrt(a.w)), a);
        public static dvec2 sqrt(dvec2 a) => Shader.TraceFunction(new dvec2(__sqrt(a.x), __sqrt(a.y)), a);
        public static dvec3 sqrt(dvec3 a) => Shader.TraceFunction(new dvec3(__sqrt(a.x), __sqrt(a.y), __sqrt(a.z)), a);
        public static dvec4 sqrt(dvec4 a) => Shader.TraceFunction(new dvec4(__sqrt(a.x), __sqrt(a.y), __sqrt(a.z), __sqrt(a.w)), a);
        public static float __pow(float a, float exp) => (float)Math.Pow(a, exp);
        public static double __pow(double a, double exp) => Math.Pow(a, exp);
        public static float pow(float a, float exp) => Shader.TraceFunction(__pow(a, exp), a, exp);
        public static double pow(double a, double exp) => Shader.TraceFunction(__pow(a, exp), a, exp);
        //public static vec2 pow(vec2 a, float exp) => Shader.TraceFunction(new vec2(__pow(a.x, exp), __pow(a.y, exp)), a, exp);
        //public static vec3 pow(vec3 a, float exp) => Shader.TraceFunction(new vec3(__pow(a.x, exp), __pow(a.y, exp), __pow(a.z, exp)), a, exp);
        //public static vec4 pow(vec4 a, float exp) => Shader.TraceFunction(new vec4(__pow(a.x, exp), __pow(a.y, exp), __pow(a.z, exp), __pow(a.w, exp)), a, exp);
        //public static dvec2 pow(dvec2 a, double exp) => Shader.TraceFunction(new dvec2(__pow(a.x, exp), __pow(a.y, exp)), a, exp);
        //public static dvec3 pow(dvec3 a, double exp) => Shader.TraceFunction(new dvec3(__pow(a.x, exp), __pow(a.y, exp), __pow(a.z, exp)), a, exp);
        //public static dvec4 pow(dvec4 a, double exp) => Shader.TraceFunction(new dvec4(__pow(a.x, exp), __pow(a.y, exp), __pow(a.z, exp), __pow(a.w, exp)), a, exp);
        public static float __exp(float a) => (float)Math.Sqrt(a);
        public static double __exp(double a) => Math.Sqrt(a);
        public static float exp(float a) => Shader.TraceFunction(__exp(a), a);
        public static double exp(double a) => Shader.TraceFunction(__exp(a), a);
        //public static vec2 exp(vec2 a) => Shader.TraceFunction(new vec2(__exp(a.x), __exp(a.y)), a);
        //public static vec3 exp(vec3 a) => Shader.TraceFunction(new vec3(__exp(a.x), __exp(a.y), __exp(a.z)), a);
        //public static vec4 exp(vec4 a) => Shader.TraceFunction(new vec4(__exp(a.x), __exp(a.y), __exp(a.z), __exp(a.w)), a);
        //public static dvec2 exp(dvec2 a) => Shader.TraceFunction(new dvec2(__exp(a.x), __exp(a.y)), a);
        //public static dvec3 exp(dvec3 a) => Shader.TraceFunction(new dvec3(__exp(a.x), __exp(a.y), __exp(a.z)), a);
        //public static dvec4 exp(dvec4 a) => Shader.TraceFunction(new dvec4(__exp(a.x), __exp(a.y), __exp(a.z), __exp(a.w)), a);

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
        public static float length(vec2 a) => Shader.TraceFunction(sqrt(a.x * a.x + a.y * a.y), a);
        public static float length(vec3 a) => Shader.TraceFunction(sqrt(a.x * a.x + a.y * a.y + a.z * a.z), a);
        public static float length(vec4 a) => Shader.TraceFunction(sqrt(a.x * a.x + a.y * a.y + a.z * a.z + a.w * a.w), a);
        public static double length(dvec2 a) => Shader.TraceFunction(sqrt(a.x * a.x + a.y * a.y), a);
        public static double length(dvec3 a) => Shader.TraceFunction(sqrt(a.x * a.x + a.y * a.y + a.z * a.z), a);
        public static double length(dvec4 a) => Shader.TraceFunction(sqrt(a.x * a.x + a.y * a.y + a.z * a.z + a.w * a.w), a);
        public static vec2 normalize(vec2 a) => Shader.TraceFunction(a / length(a), a);
        public static vec3 normalize(vec3 a) => Shader.TraceFunction(a / length(a), a);
        public static vec4 normalize(vec4 a) => Shader.TraceFunction(a / length(a), a);
        public static dvec2 normalize(dvec2 a) => Shader.TraceFunction(a / length(a), a);
        public static dvec3 normalize(dvec3 a) => Shader.TraceFunction(a / length(a), a);
        public static dvec4 normalize(dvec4 a) => Shader.TraceFunction(a / length(a), a);

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
        public static vec3 vec3(vec2 xy, float z) => new vec3(xy.x, xy.y, z);
        public static vec3 vec3(float x, vec2 yz) => new vec3(x, yz.x, yz.y);
        public static vec4 vec4(float a) => new vec4(a);
        public static vec4 vec4(float x, float y, float z, float w) => new vec4(x, y, z, w);
        public static vec4 vec4(vec2 xy, float z, float w) => new vec4(xy.x, xy.y, z, w);
        public static vec4 vec4(float x, vec2 yz, float w) => new vec4(x, yz.x, yz.y, w);
        public static vec4 vec4(float x, float y, vec2 zw) => new vec4(x, y, zw.x, zw.y);
        public static vec4 vec4(vec2 xy, vec2 zw) => new vec4(xy.x, xy.y, zw.x, zw.y);
        public static vec4 vec4(vec3 xyz, float w) => new vec4(xyz.x, xyz.y, xyz.z, w);
        public static vec4 vec4(float x, vec3 yzw) => new vec4(x, yzw.x, yzw.y, yzw.z);
        public static dvec2 dvec2(double a) => new dvec2(a);
        public static dvec2 dvec2(double x, double y) => new dvec2(x, y);
        public static dvec3 dvec3(double a) => new dvec3(a);
        public static dvec3 dvec3(double x, double y, double z) => new dvec3(x, y, z);
        public static dvec3 dvec3(dvec2 xy, double z) => new dvec3(xy.x, xy.y, z);
        public static dvec3 dvec3(double x, dvec2 yz) => new dvec3(x, yz.x, yz.y);
        public static dvec4 dvec4(double a) => new dvec4(a);
        public static dvec4 dvec4(double x, double y, double z, double w) => new dvec4(x, y, z, w);
        public static dvec4 dvec4(dvec2 xy, double z, double w) => new dvec4(xy.x, xy.y, z, w);
        public static dvec4 dvec4(double x, dvec2 yz, double w) => new dvec4(x, yz.x, yz.y, w);
        public static dvec4 dvec4(double x, double y, dvec2 zw) => new dvec4(x, y, zw.x, zw.y);
        public static dvec4 dvec4(dvec2 xy, dvec2 zw) => new dvec4(xy.x, xy.y, zw.x, zw.y);
        public static dvec4 dvec4(vec3 xyz, double w) => new dvec4(xyz.x, xyz.y, xyz.z, w);
        public static dvec4 dvec4(double x, vec3 yzw) => new dvec4(x, yzw.x, yzw.y, yzw.z);
        public static ivec2 ivec2(int a) => new ivec2(a);
        public static ivec2 ivec2(int x, int y) => new ivec2(x, y);
        public static ivec3 ivec3(int a) => new ivec3(a);
        public static ivec3 ivec3(int x, int y, int z) => new ivec3(x, y, z);
        public static ivec3 ivec3(ivec2 xy, int z) => new ivec3(xy.x, xy.y, z);
        public static ivec3 ivec3(int x, ivec2 yz) => new ivec3(x, yz.x, yz.y);
        public static ivec4 ivec4(int a) => new ivec4(a);
        public static ivec4 ivec4(int x, int y, int z, int w) => new ivec4(x, y, z, w);
        public static ivec4 ivec4(ivec2 xy, int z, int w) => new ivec4(xy.x, xy.y, z, w);
        public static ivec4 ivec4(int x, ivec2 yz, int w) => new ivec4(x, yz.x, yz.y, w);
        public static ivec4 ivec4(int x, int y, ivec2 zw) => new ivec4(x, y, zw.x, zw.y);
        public static ivec4 ivec4(ivec2 xy, ivec2 zw) => new ivec4(xy.x, xy.y, zw.x, zw.y);
        public static ivec4 ivec4(ivec3 xyz, int w) => new ivec4(xyz.x, xyz.y, xyz.z, w);
        public static ivec4 ivec4(int x, ivec3 yzw) => new ivec4(x, yzw.x, yzw.y, yzw.z);
        public static uvec2 uvec2(uint a) => new uvec2(a);
        public static uvec2 uvec2(uint x, uint y) => new uvec2(x, y);
        public static uvec3 uvec3(uint a) => new uvec3(a);
        public static uvec3 uvec3(uint x, uint y, uint z) => new uvec3(x, y, z);
        public static uvec3 uvec3(uvec2 xy, uint z) => new uvec3(xy.x, xy.y, z);
        public static uvec3 uvec3(uint x, uvec2 yz) => new uvec3(x, yz.x, yz.y);
        public static uvec4 uvec4(uint a) => new uvec4(a);
        public static uvec4 uvec4(uint x, uint y, uint z, uint w) => new uvec4(x, y, z, w);
        public static uvec4 uvec4(uvec2 xy, uint z, uint w) => new uvec4(xy.x, xy.y, z, w);
        public static uvec4 uvec4(uint x, uvec2 yz, uint w) => new uvec4(x, yz.x, yz.y, w);
        public static uvec4 uvec4(uint x, uint y, uvec2 zw) => new uvec4(x, y, zw.x, zw.y);
        public static uvec4 uvec4(uvec2 xy, uvec2 zw) => new uvec4(xy.x, xy.y, zw.x, zw.y);
        public static uvec4 uvec4(uvec3 xyz, uint w) => new uvec4(xyz.x, xyz.y, xyz.z, w);
        public static uvec4 uvec4(uint x, uvec3 yzw) => new uvec4(x, yzw.x, yzw.y, yzw.z);
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
