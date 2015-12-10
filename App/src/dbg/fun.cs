using System;

namespace App.debug
{
    class fun
    {
        #region vec
        public vec2 vec2()
        {
            return new debug.vec2();
        }

        public vec2 vec2(float x)
        {
            return new debug.vec2(x);
        }

        public vec2 vec2(float x, float y)
        {
            return new debug.vec2(x, y);
        }

        public vec3 vec3()
        {
            return new debug.vec3();
        }

        public vec3 vec3(float x)
        {
            return new debug.vec3(x);
        }

        public vec3 vec3(float x, vec2 yz)
        {
            return new debug.vec3(x, yz);
        }

        public vec3 vec3(vec2 xy, float z)
        {
            return new debug.vec3(xy, z);
        }

        public vec3 vec3(float x, float y, float z)
        {
            return new debug.vec3(x, y, z);
        }

        public vec4 vec4()
        {
            return new debug.vec4();
        }

        public vec4 vec4(float x)
        {
            return new debug.vec4(x);
        }

        public vec4 vec4(vec2 xy, vec2 zw)
        {
            return new debug.vec4(xy, zw);
        }

        public vec4 vec4(float x, vec3 yzw)
        {
            return new debug.vec4(x, yzw);
        }

        public vec4 vec4(vec3 xyz, float w)
        {
            return new debug.vec4(xyz, w);
        }

        public vec4 vec4(float x, float y, float z, float w)
        {
            return new debug.vec4(x, y, z, w);
        }
        #endregion
        #region dvec
        public dvec2 dvec2()
        {
            return new debug.dvec2();
        }

        public dvec2 dvec2(double x)
        {
            return new debug.dvec2(x);
        }

        public dvec2 dvec2(double x, double y)
        {
            return new debug.dvec2(x, y);
        }

        public dvec3 dvec3(double x)
        {
            return new debug.dvec3(x);
        }

        public dvec3 dvec3(double x, dvec2 yz)
        {
            return new debug.dvec3(x, yz);
        }

        public dvec3 dvec3(dvec2 xy, double z)
        {
            return new debug.dvec3(xy, z);
        }

        public dvec3 dvec3(double x, double y, double z)
        {
            return new debug.dvec3(x, y, z);
        }

        public dvec4 dvec4(double x)
        {
            return new debug.dvec4(x);
        }

        public dvec4 dvec4(dvec2 xy, dvec2 zw)
        {
            return new debug.dvec4(xy, zw);
        }

        public dvec4 dvec4(double x, dvec3 yzw)
        {
            return new debug.dvec4(x, yzw);
        }

        public dvec4 dvec4(dvec3 xyz, double w)
        {
            return new debug.dvec4(xyz, w);
        }

        public dvec4 dvec4(double x, double y, double z, double w)
        {
            return new debug.dvec4(x, y, z, w);
        }
        #endregion
        #region ivec
        public ivec2 ivec2()
        {
            return new debug.ivec2();
        }

        public ivec2 ivec2(int x)
        {
            return new debug.ivec2(x);
        }

        public ivec2 ivec2(int x, int y)
        {
            return new debug.ivec2(x, y);
        }

        public ivec3 ivec3()
        {
            return new debug.ivec3();
        }

        public ivec3 ivec3(int x)
        {
            return new debug.ivec3(x);
        }

        public ivec3 ivec3(int x, ivec2 yz)
        {
            return new debug.ivec3(x, yz);
        }

        public ivec3 ivec3(ivec2 xy, int z)
        {
            return new debug.ivec3(xy, z);
        }

        public ivec3 ivec3(int x, int y, int z)
        {
            return new debug.ivec3(x, y, z);
        }

        public ivec4 ivec4()
        {
            return new debug.ivec4();
        }

        public ivec4 ivec4(int x)
        {
            return new debug.ivec4(x);
        }

        public ivec4 ivec4(ivec2 xy, ivec2 zw)
        {
            return new debug.ivec4(xy, zw);
        }

        public ivec4 ivec4(int x, ivec3 yzw)
        {
            return new debug.ivec4(x, yzw);
        }

        public ivec4 ivec4(ivec3 xyz, int w)
        {
            return new debug.ivec4(xyz, w);
        }

        public ivec4 ivec4(int x, int y, int z, int w)
        {
            return new debug.ivec4(x, y, z, w);
        }
        #endregion
        #region uvec
        public uvec2 uvec2()
        {
            return new debug.uvec2();
        }

        public uvec2 uvec2(uint x)
        {
            return new debug.uvec2(x);
        }

        public uvec2 uvec2(uint x, uint y)
        {
            return new debug.uvec2(x, y);
        }

        public uvec3 uvec3()
        {
            return new debug.uvec3();
        }

        public uvec3 uvec3(uint x)
        {
            return new debug.uvec3(x);
        }

        public uvec3 uvec3(uint x, uvec2 yz)
        {
            return new debug.uvec3(x, yz);
        }

        public uvec3 uvec3(uvec2 xy, uint z)
        {
            return new debug.uvec3(xy, z);
        }

        public uvec3 uvec3(uint x, uint y, uint z)
        {
            return new debug.uvec3(x, y, z);
        }

        public uvec4 uvec4()
        {
            return new debug.uvec4();
        }

        public uvec4 uvec4(uint x)
        {
            return new debug.uvec4(x);
        }

        public uvec4 uvec4(uvec2 xy, uvec2 zw)
        {
            return new debug.uvec4(xy, zw);
        }

        public uvec4 uvec4(uint x, uvec3 yzw)
        {
            return new debug.uvec4(x, yzw);
        }

        public uvec4 uvec4(uvec3 xyz, uint w)
        {
            return new debug.uvec4(xyz, w);
        }

        public uvec4 uvec4(uint x, uint y, uint z, uint w)
        {
            return new debug.uvec4(x, y, z, w);
        }
        #endregion
        #region bvec
        public bvec2 bvec2()
        {
            return new debug.bvec2();
        }

        public bvec2 bvec2(bool x)
        {
            return new debug.bvec2(x);
        }

        public bvec2 bvec2(bool x, bool y)
        {
            return new debug.bvec2(x, y);
        }

        public bvec3 bvec3(bool x)
        {
            return new debug.bvec3(x);
        }

        public bvec3 bvec3(bool x, bvec2 yz)
        {
            return new debug.bvec3(x, yz);
        }

        public bvec3 bvec3(bvec2 xy, bool z)
        {
            return new debug.bvec3(xy, z);
        }

        public bvec3 bvec3(bool x, bool y, bool z)
        {
            return new debug.bvec3(x, y, z);
        }

        public bvec4 bvec4(bool x)
        {
            return new debug.bvec4(x);
        }

        public bvec4 bvec4(bvec2 xy, bvec2 zw)
        {
            return new debug.bvec4(xy, zw);
        }

        public bvec4 bvec4(bool x, bvec3 yzw)
        {
            return new debug.bvec4(x, yzw);
        }

        public bvec4 bvec4(bvec3 xyz, bool w)
        {
            return new debug.bvec4(xyz, w);
        }

        public bvec4 bvec4(bool x, bool y, bool z, bool w)
        {
            return new debug.bvec4(x, y, z, w);
        }
        #endregion
        #region dot
        public float dot(vec2 l, vec2 r)
        {
            return l.x * r.x + l.y * r.y;
        }

        public float dot(vec3 l, vec3 r)
        {
            return l.x * r.x + l.y * r.y + l.z * r.z;
        }

        public float dot(vec4 l, vec4 r)
        {
            return l.x * r.x + l.y * r.y + l.z * r.z + l.w * r.w;
        }

        public double dot(dvec2 l, dvec2 r)
        {
            return l.x * r.x + l.y * r.y;
        }

        public double dot(dvec3 l, dvec3 r)
        {
            return l.x * r.x + l.y * r.y + l.z * r.z;
        }

        public double dot(dvec4 l, dvec4 r)
        {
            return l.x * r.x + l.y * r.y + l.z * r.z + l.w * r.w;
        }
        #endregion
        #region length
        public float length(vec3 v)
        {
            return (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        }

        public double length(dvec3 v)
        {
            return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        }
        #endregion
        #region cross
        public vec3 cross(vec3 l, vec3 r)
        {
            return new vec3(
                l.y * r.z - l.z * r.y,
                l.z * r.x - l.x * r.z,
                l.x * r.y - l.y * r.x);
        }

        public dvec3 cross(dvec3 l, dvec3 r)
        {
            return new dvec3(
                l.y * r.z - l.z * r.y,
                l.z * r.x - l.x * r.z,
                l.x * r.y - l.y * r.x);
        }
        #endregion
        #region normalize
        public vec3 normalize(vec3 v)
        {
            float l = (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            return new vec3(v.x / l, v.y / l, v.z / l);
        }

        public dvec3 normalize(dvec3 v)
        {
            double l = Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            return new dvec3(v.x / l, v.y / l, v.z / l);
        }
        #endregion
        #region distance
        public float distance(vec3 l, vec3 r)
        {
            l.x -= r.x;
            l.y -= r.y;
            l.z -= r.z;
            return (float)Math.Sqrt(l.x * l.x + l.y * l.y + l.z * l.z);
        }

        public double distance(dvec3 l, dvec3 r)
        {
            l.x -= r.x;
            l.y -= r.y;
            l.z -= r.z;
            return Math.Sqrt(l.x * l.x + l.y * l.y + l.z * l.z);
        }
        #endregion
        #region radians
        const double rad2deg = 180 / System.Math.PI;
        const double deg2rad = System.Math.PI / 180;

        public float radians(float v)
        {
            return (float)(v * deg2rad);
        }

        public vec2 radians(vec2 v)
        {
            return new vec2((float)(v.x * deg2rad), (float)(v.y * deg2rad));
        }

        public vec3 radians(vec3 v)
        {
            return new vec3((float)(v.x * deg2rad), (float)(v.y * deg2rad), (float)(v.z * deg2rad));
        }

        public vec4 radians(vec4 v)
        {
            return new vec4((float)(v.x * deg2rad), (float)(v.y * deg2rad), (float)(v.z * deg2rad), (float)(v.w * deg2rad));
        }

        public double radians(double v)
        {
            return (v * deg2rad);
        }

        public dvec2 radians(dvec2 v)
        {
            return new dvec2(v.x * deg2rad, v.y * deg2rad);
        }

        public dvec3 radians(dvec3 v)
        {
            return new dvec3(v.x * deg2rad, v.y * deg2rad, v.z * deg2rad);
        }

        public dvec4 radians(dvec4 v)
        {
            return new dvec4(v.x * deg2rad, v.y * deg2rad, v.z * deg2rad, v.w * deg2rad);
        }
        #endregion
        #region degrees
        public float degrees(float v)
        {
            return (float)(v * rad2deg);
        }

        public vec2 degrees(vec2 v)
        {
            return new vec2((float)(v.x * rad2deg), (float)(v.y * rad2deg));
        }

        public vec3 degrees(vec3 v)
        {
            return new vec3((float)(v.x * rad2deg), (float)(v.y * rad2deg), (float)(v.z * rad2deg));
        }

        public vec4 degrees(vec4 v)
        {
            return new vec4((float)(v.x * rad2deg), (float)(v.y * rad2deg), (float)(v.z * rad2deg), (float)(v.w * rad2deg));
        }

        public double degrees(double v)
        {
            return (v * rad2deg);
        }

        public dvec2 degrees(dvec2 v)
        {
            return new dvec2(v.x * rad2deg, v.y * rad2deg);
        }

        public dvec3 degrees(dvec3 v)
        {
            return new dvec3(v.x * rad2deg, v.y * rad2deg, v.z * rad2deg);
        }

        public dvec4 degrees(dvec4 v)
        {
            return new dvec4(v.x * rad2deg, v.y * rad2deg, v.z * rad2deg, v.w * rad2deg);
        }
        #endregion
        #region sin
        public float sin(float v)
        {
            return (float)Math.Sin(v);
        }

        public vec2 sin(vec2 v)
        {
            return new vec2((float)Math.Sin(v.x), (float)Math.Sin(v.y));
        }

        public vec3 sin(vec3 v)
        {
            return new vec3((float)Math.Sin(v.x), (float)Math.Sin(v.y), (float)Math.Sin(v.z));
        }

        public vec4 sin(vec4 v)
        {
            return new vec4((float)Math.Sin(v.x), (float)Math.Sin(v.y), (float)Math.Sin(v.z), (float)Math.Sin(v.w));
        }
        public double sin(double v)
        {
            return Math.Sin(v);
        }

        public dvec2 sin(dvec2 v)
        {
            return new dvec2(Math.Sin(v.x), Math.Sin(v.y));
        }

        public dvec3 sin(dvec3 v)
        {
            return new dvec3(Math.Sin(v.x), Math.Sin(v.y), Math.Sin(v.z));
        }

        public dvec4 sin(dvec4 v)
        {
            return new dvec4(Math.Sin(v.x), Math.Sin(v.y), Math.Sin(v.z), Math.Sin(v.w));
        }
        #endregion
        #region cos
        public float cos(float v)
        {
            return (float)Math.Cos(v);
        }

        public vec2 cos(vec2 v)
        {
            return new vec2((float)Math.Cos(v.x), (float)Math.Cos(v.y));
        }

        public vec3 cos(vec3 v)
        {
            return new vec3((float)Math.Cos(v.x), (float)Math.Cos(v.y), (float)Math.Cos(v.z));
        }

        public vec4 cos(vec4 v)
        {
            return new vec4((float)Math.Cos(v.x), (float)Math.Cos(v.y), (float)Math.Cos(v.z), (float)Math.Cos(v.w));
        }
        public double cos(double v)
        {
            return Math.Cos(v);
        }

        public dvec2 cos(dvec2 v)
        {
            return new dvec2(Math.Cos(v.x), Math.Cos(v.y));
        }

        public dvec3 cos(dvec3 v)
        {
            return new dvec3(Math.Cos(v.x), Math.Cos(v.y), Math.Cos(v.z));
        }

        public dvec4 cos(dvec4 v)
        {
            return new dvec4(Math.Cos(v.x), Math.Cos(v.y), Math.Cos(v.z), Math.Cos(v.w));
        }
        #endregion
        #region tan
        public float tan(float v)
        {
            return (float)Math.Tan(v);
        }

        public vec2 tan(vec2 v)
        {
            return new vec2((float)Math.Tan(v.x), (float)Math.Tan(v.y));
        }

        public vec3 tan(vec3 v)
        {
            return new vec3((float)Math.Tan(v.x), (float)Math.Tan(v.y), (float)Math.Tan(v.z));
        }

        public vec4 tan(vec4 v)
        {
            return new vec4((float)Math.Tan(v.x), (float)Math.Tan(v.y), (float)Math.Tan(v.z), (float)Math.Tan(v.w));
        }
        public double tan(double v)
        {
            return Math.Tan(v);
        }

        public dvec2 tan(dvec2 v)
        {
            return new dvec2(Math.Tan(v.x), Math.Tan(v.y));
        }

        public dvec3 tan(dvec3 v)
        {
            return new dvec3(Math.Tan(v.x), Math.Tan(v.y), Math.Tan(v.z));
        }

        public dvec4 tan(dvec4 v)
        {
            return new dvec4(Math.Tan(v.x), Math.Tan(v.y), Math.Tan(v.z), Math.Tan(v.w));
        }
        #endregion
        #region asin
        public float asin(float v)
        {
            return (float)Math.Asin(v);
        }

        public vec2 asin(vec2 v)
        {
            return new vec2((float)Math.Asin(v.x), (float)Math.Asin(v.y));
        }

        public vec3 asin(vec3 v)
        {
            return new vec3((float)Math.Asin(v.x), (float)Math.Asin(v.y), (float)Math.Asin(v.z));
        }

        public vec4 asin(vec4 v)
        {
            return new vec4((float)Math.Asin(v.x), (float)Math.Asin(v.y), (float)Math.Asin(v.z), (float)Math.Asin(v.w));
        }
        public double asin(double v)
        {
            return Math.Sin(v);
        }

        public dvec2 asin(dvec2 v)
        {
            return new dvec2(Math.Asin(v.x), Math.Asin(v.y));
        }

        public dvec3 asin(dvec3 v)
        {
            return new dvec3(Math.Asin(v.x), Math.Asin(v.y), Math.Asin(v.z));
        }

        public dvec4 asin(dvec4 v)
        {
            return new dvec4(Math.Asin(v.x), Math.Asin(v.y), Math.Asin(v.z), Math.Asin(v.w));
        }
        #endregion
        #region acos
        public float acos(float v)
        {
            return (float)Math.Acos(v);
        }

        public vec2 acos(vec2 v)
        {
            return new vec2((float)Math.Acos(v.x), (float)Math.Acos(v.y));
        }

        public vec3 acos(vec3 v)
        {
            return new vec3((float)Math.Acos(v.x), (float)Math.Acos(v.y), (float)Math.Acos(v.z));
        }

        public vec4 acos(vec4 v)
        {
            return new vec4((float)Math.Acos(v.x), (float)Math.Acos(v.y), (float)Math.Acos(v.z), (float)Math.Acos(v.w));
        }
        public double acos(double v)
        {
            return Math.Acos(v);
        }

        public dvec2 acos(dvec2 v)
        {
            return new dvec2(Math.Acos(v.x), Math.Acos(v.y));
        }

        public dvec3 acos(dvec3 v)
        {
            return new dvec3(Math.Acos(v.x), Math.Acos(v.y), Math.Acos(v.z));
        }

        public dvec4 acos(dvec4 v)
        {
            return new dvec4(Math.Acos(v.x), Math.Acos(v.y), Math.Acos(v.z), Math.Acos(v.w));
        }
        #endregion
        #region atan
        public float atan(float v)
        {
            return (float)Math.Atan(v);
        }

        public vec2 atan(vec2 v)
        {
            return new vec2((float)Math.Atan(v.x), (float)Math.Atan(v.y));
        }

        public vec3 atan(vec3 v)
        {
            return new vec3((float)Math.Atan(v.x), (float)Math.Atan(v.y), (float)Math.Atan(v.z));
        }

        public vec4 atan(vec4 v)
        {
            return new vec4((float)Math.Atan(v.x), (float)Math.Atan(v.y), (float)Math.Atan(v.z), (float)Math.Atan(v.w));
        }

        public double atan(double v)
        {
            return Math.Atan(v);
        }

        public dvec2 atan(dvec2 v)
        {
            return new dvec2(Math.Atan(v.x), Math.Atan(v.y));
        }

        public dvec3 atan(dvec3 v)
        {
            return new dvec3(Math.Atan(v.x), Math.Atan(v.y), Math.Atan(v.z));
        }

        public dvec4 atan(dvec4 v)
        {
            return new dvec4(Math.Atan(v.x), Math.Atan(v.y), Math.Atan(v.z), Math.Atan(v.w));
        }
        #endregion
        #region pow
        public float pow(float a, float b)
        {
            return (float)Math.Pow(a, b);
        }

        public vec2 pow(vec2 a, vec2 b)
        {
            return new vec2((float)Math.Pow(a.x, b.x), (float)Math.Pow(a.y, b.y));
        }

        public vec3 pow(vec3 a, vec3 b)
        {
            return new vec3((float)Math.Pow(a.x, b.x), (float)Math.Pow(a.y, b.y), (float)Math.Pow(a.z, b.z));
        }

        public vec4 pow(vec4 a, vec4 b)
        {
            return new vec4((float)Math.Pow(a.x, b.x), (float)Math.Pow(a.y, b.y), (float)Math.Pow(a.z, b.z), (float)Math.Pow(a.w, b.w));
        }

        public double pow(double a, double b)
        {
            return Math.Atan(b);
        }

        public dvec2 pow(dvec2 a, dvec2 b)
        {
            return new dvec2(Math.Pow(a.x, b.x), Math.Pow(a.y, b.y));
        }

        public dvec3 pow(dvec3 a, dvec3 b)
        {
            return new dvec3(Math.Pow(a.x, b.x), Math.Pow(a.y, b.y), Math.Pow(a.z, b.z));
        }

        public dvec4 pow(dvec4 a, dvec4 b)
        {
            return new dvec4(Math.Pow(a.x, b.x), Math.Pow(a.y, b.y), Math.Pow(a.z, b.z), Math.Pow(a.w, b.w));
        }
        #endregion
        #region exp
        public float exp(float v)
        {
            return (float)Math.Exp(v);
        }

        public vec2 exp(vec2 v)
        {
            return new vec2((float)Math.Exp(v.x), (float)Math.Exp(v.y));
        }

        public vec3 exp(vec3 v)
        {
            return new vec3((float)Math.Exp(v.x), (float)Math.Exp(v.y), (float)Math.Exp(v.z));
        }

        public vec4 exp(vec4 v)
        {
            return new vec4((float)Math.Exp(v.x), (float)Math.Exp(v.y), (float)Math.Exp(v.z), (float)Math.Exp(v.w));
        }

        public double exp(double v)
        {
            return Math.Exp(v);
        }

        public dvec2 exp(dvec2 v)
        {
            return new dvec2(Math.Exp(v.x), Math.Exp(v.y));
        }

        public dvec3 exp(dvec3 v)
        {
            return new dvec3(Math.Exp(v.x), Math.Exp(v.y), Math.Exp(v.z));
        }

        public dvec4 exp(dvec4 v)
        {
            return new dvec4(Math.Exp(v.x), Math.Exp(v.y), Math.Exp(v.z), Math.Exp(v.w));
        }
        #endregion
        #region log
        public float log(float v)
        {
            return (float)Math.Log(v);
        }

        public vec2 log(vec2 v)
        {
            return new vec2((float)Math.Log(v.x), (float)Math.Log(v.y));
        }

        public vec3 log(vec3 v)
        {
            return new vec3((float)Math.Log(v.x), (float)Math.Log(v.y), (float)Math.Log(v.z));
        }

        public vec4 log(vec4 v)
        {
            return new vec4((float)Math.Log(v.x), (float)Math.Log(v.y), (float)Math.Log(v.z), (float)Math.Log(v.w));
        }

        public double log(double v)
        {
            return Math.Log(v);
        }

        public dvec2 log(dvec2 v)
        {
            return new dvec2(Math.Log(v.x), Math.Log(v.y));
        }

        public dvec3 log(dvec3 v)
        {
            return new dvec3(Math.Log(v.x), Math.Log(v.y), Math.Log(v.z));
        }

        public dvec4 log(dvec4 v)
        {
            return new dvec4(Math.Log(v.x), Math.Log(v.y), Math.Log(v.z), Math.Log(v.w));
        }
        #endregion
        #region sqrt
        public float sqrt(float v)
        {
            return (float)Math.Sqrt(v);
        }

        public vec2 sqrt(vec2 v)
        {
            return new vec2((float)Math.Sqrt(v.x), (float)Math.Sqrt(v.y));
        }

        public vec3 sqrt(vec3 v)
        {
            return new vec3((float)Math.Sqrt(v.x), (float)Math.Sqrt(v.y), (float)Math.Sqrt(v.z));
        }

        public vec4 sqrt(vec4 v)
        {
            return new vec4((float)Math.Sqrt(v.x), (float)Math.Sqrt(v.y), (float)Math.Sqrt(v.z), (float)Math.Sqrt(v.w));
        }

        public double sqrt(double v)
        {
            return Math.Sqrt(v);
        }

        public dvec2 sqrt(dvec2 v)
        {
            return new dvec2(Math.Sqrt(v.x), Math.Sqrt(v.y));
        }

        public dvec3 sqrt(dvec3 v)
        {
            return new dvec3(Math.Sqrt(v.x), Math.Sqrt(v.y), Math.Sqrt(v.z));
        }

        public dvec4 sqrt(dvec4 v)
        {
            return new dvec4(Math.Sqrt(v.x), Math.Sqrt(v.y), Math.Sqrt(v.z), Math.Sqrt(v.w));
        }
        #endregion
        #region inversesqrt
        public float inversesqrt(float v)
        {
            return 1/(float)Math.Sqrt(v);
        }

        public vec2 inversesqrt(vec2 v)
        {
            return new vec2(1 / (float)Math.Sqrt(v.x), 1 / (float)Math.Sqrt(v.y));
        }

        public vec3 inversesqrt(vec3 v)
        {
            return new vec3(1 / (float)Math.Sqrt(v.x), 1 / (float)Math.Sqrt(v.y), 1 / (float)Math.Sqrt(v.z));
        }

        public vec4 inversesqrt(vec4 v)
        {
            return new vec4(1 / (float)Math.Sqrt(v.x), 1 / (float)Math.Sqrt(v.y), 1 / (float)Math.Sqrt(v.z), 1 / (float)Math.Sqrt(v.w));
        }

        public double inversesqrt(double v)
        {
            return 1 / Math.Sqrt(v);
        }

        public dvec2 inversesqrt(dvec2 v)
        {
            return new dvec2(1 / Math.Sqrt(v.x), 1 / Math.Sqrt(v.y));
        }

        public dvec3 inversesqrt(dvec3 v)
        {
            return new dvec3(1 / Math.Sqrt(v.x), 1 / Math.Sqrt(v.y), 1 / Math.Sqrt(v.z));
        }

        public dvec4 inversesqrt(dvec4 v)
        {
            return new dvec4(1 / Math.Sqrt(v.x), 1 / Math.Sqrt(v.y), 1 / Math.Sqrt(v.z), 1 / Math.Sqrt(v.w));
        }
        #endregion
        #region abs
        public float abs(float v)
        {
            return (float)Math.Abs(v);
        }

        public vec2 abs(vec2 v)
        {
            return new vec2((float)Math.Abs(v.x), (float)Math.Abs(v.y));
        }

        public vec3 abs(vec3 v)
        {
            return new vec3((float)Math.Abs(v.x), (float)Math.Abs(v.y), (float)Math.Abs(v.z));
        }

        public vec4 abs(vec4 v)
        {
            return new vec4((float)Math.Abs(v.x), (float)Math.Abs(v.y), (float)Math.Abs(v.z), (float)Math.Abs(v.w));
        }

        public double abs(double v)
        {
            return Math.Abs(v);
        }

        public dvec2 abs(dvec2 v)
        {
            return new dvec2(Math.Abs(v.x), Math.Abs(v.y));
        }

        public dvec3 abs(dvec3 v)
        {
            return new dvec3(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z));
        }

        public dvec4 abs(dvec4 v)
        {
            return new dvec4(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z), Math.Abs(v.w));
        }
        #endregion
        #region sign
        public float sign(float v)
        {
            return (float)Math.Sign(v);
        }

        public vec2 sign(vec2 v)
        {
            return new vec2((float)Math.Sign(v.x), (float)Math.Sign(v.y));
        }

        public vec3 sign(vec3 v)
        {
            return new vec3((float)Math.Sign(v.x), (float)Math.Sign(v.y), (float)Math.Sign(v.z));
        }

        public vec4 sign(vec4 v)
        {
            return new vec4((float)Math.Sign(v.x), (float)Math.Sign(v.y), (float)Math.Sign(v.z), (float)Math.Sign(v.w));
        }

        public double sign(double v)
        {
            return Math.Sign(v);
        }

        public dvec2 sign(dvec2 v)
        {
            return new dvec2(Math.Sign(v.x), Math.Sign(v.y));
        }

        public dvec3 sign(dvec3 v)
        {
            return new dvec3(Math.Sign(v.x), Math.Sign(v.y), Math.Sign(v.z));
        }

        public dvec4 sign(dvec4 v)
        {
            return new dvec4(Math.Sign(v.x), Math.Sign(v.y), Math.Sign(v.z), Math.Sign(v.w));
        }
        #endregion
        #region floor
        public float floor(float v)
        {
            return (float)Math.Floor(v);
        }

        public vec2 floor(vec2 v)
        {
            return new vec2((float)Math.Floor(v.x), (float)Math.Floor(v.y));
        }

        public vec3 floor(vec3 v)
        {
            return new vec3((float)Math.Floor(v.x), (float)Math.Floor(v.y), (float)Math.Floor(v.z));
        }

        public vec4 floor(vec4 v)
        {
            return new vec4((float)Math.Floor(v.x), (float)Math.Floor(v.y), (float)Math.Floor(v.z), (float)Math.Floor(v.w));
        }

        public double floor(double v)
        {
            return Math.Floor(v);
        }

        public dvec2 floor(dvec2 v)
        {
            return new dvec2(Math.Floor(v.x), Math.Floor(v.y));
        }

        public dvec3 floor(dvec3 v)
        {
            return new dvec3(Math.Floor(v.x), Math.Floor(v.y), Math.Floor(v.z));
        }

        public dvec4 floor(dvec4 v)
        {
            return new dvec4(Math.Floor(v.x), Math.Floor(v.y), Math.Floor(v.z), Math.Floor(v.w));
        }
        #endregion
        #region ceil
        public float ceil(float v)
        {
            return (float)Math.Ceiling(v);
        }

        public vec2 ceil(vec2 v)
        {
            return new vec2((float)Math.Ceiling(v.x), (float)Math.Ceiling(v.y));
        }

        public vec3 ceil(vec3 v)
        {
            return new vec3((float)Math.Ceiling(v.x), (float)Math.Ceiling(v.y), (float)Math.Ceiling(v.z));
        }

        public vec4 ceil(vec4 v)
        {
            return new vec4((float)Math.Ceiling(v.x), (float)Math.Ceiling(v.y), (float)Math.Ceiling(v.z), (float)Math.Ceiling(v.w));
        }

        public double ceil(double v)
        {
            return Math.Ceiling(v);
        }

        public dvec2 ceil(dvec2 v)
        {
            return new dvec2(Math.Ceiling(v.x), Math.Ceiling(v.y));
        }

        public dvec3 ceil(dvec3 v)
        {
            return new dvec3(Math.Ceiling(v.x), Math.Ceiling(v.y), Math.Ceiling(v.z));
        }

        public dvec4 ceil(dvec4 v)
        {
            return new dvec4(Math.Ceiling(v.x), Math.Ceiling(v.y), Math.Ceiling(v.z), Math.Ceiling(v.w));
        }
        #endregion
        #region fract
        public float fract(float v)
        {
            return v - (float)Math.Floor(v);
        }

        public vec2 fract(vec2 v)
        {
            return new vec2(v.x - (float)Math.Floor(v.x), v.y - (float)Math.Floor(v.y));
        }

        public vec3 fract(vec3 v)
        {
            return new vec3(v.x - (float)Math.Floor(v.x), v.y - (float)Math.Floor(v.y), v.z - (float)Math.Floor(v.z));
        }

        public vec4 fract(vec4 v)
        {
            return new vec4(v.x - (float)Math.Floor(v.x), v.y - (float)Math.Floor(v.y), v.z - (float)Math.Floor(v.z), v.w - (float)Math.Floor(v.w));
        }

        public double fract(double v)
        {
            return v - Math.Floor(v);
        }

        public dvec2 fract(dvec2 v)
        {
            return new dvec2(v.x - Math.Floor(v.x), v.y - Math.Floor(v.y));
        }

        public dvec3 fract(dvec3 v)
        {
            return new dvec3(v.x - Math.Floor(v.x), v.y - Math.Floor(v.y), v.z - Math.Floor(v.z));
        }

        public dvec4 fract(dvec4 v)
        {
            return new dvec4(v.x - Math.Floor(v.x), v.y - Math.Floor(v.y), v.z - Math.Floor(v.z), v.w - Math.Floor(v.w));
        }
        #endregion
        #region mod
        public float mod(float a, float b)
        {
            return a - b * floor(a / b);
        }

        public vec2 mod(vec2 a, vec2 b)
        {
            return a - b * floor(a / b);
        }

        public vec3 mod(vec3 a, vec3 b)
        {
            return a - b * floor(a / b);
        }

        public vec4 mod(vec4 a, vec4 b)
        {
            return a - b * floor(a / b);
        }

        public vec2 mod(vec2 a, float b)
        {
            debug.vec2 B = vec2(b);
            return a - B * floor(a / B);
        }

        public vec3 mod(vec3 a, float b)
        {
            debug.vec3 B = vec3(b);
            return a - B * floor(a / B);
        }

        public vec4 mod(vec4 a, float b)
        {
            debug.vec4 B = vec4(b);
            return a - B * floor(a / B);
        }

        public double mod(double a, double b)
        {
            return a - b * floor(a / b);
        }

        public dvec2 mod(dvec2 a, dvec2 b)
        {
            return a - b * floor(a / b);
        }

        public dvec3 mod(dvec3 a, dvec3 b)
        {
            return a - b * floor(a / b);
        }

        public dvec4 mod(dvec4 a, dvec4 b)
        {
            return a - b * floor(a / b);
        }

        public dvec2 mod(dvec2 a, double b)
        {
            debug.dvec2 B = dvec2(b);
            return a - B * floor(a / B);
        }

        public dvec3 mod(dvec3 a, double b)
        {
            debug.dvec3 B = dvec3(b);
            return a - B * floor(a / B);
        }

        public dvec4 mod(dvec4 a, double b)
        {
            debug.dvec4 B = dvec4(b);
            return a - B * floor(a / B);
        }

        public int mod(int a, int b)
        {
            return a - b * (a / b);
        }

        public ivec2 mod(ivec2 a, ivec2 b)
        {
            return a - b * (a / b);
        }

        public ivec3 mod(ivec3 a, ivec3 b)
        {
            return a - b * (a / b);
        }

        public ivec4 mod(ivec4 a, ivec4 b)
        {
            return a - b * (a / b);
        }

        public ivec2 mod(ivec2 a, int b)
        {
            debug.ivec2 B = ivec2(b);
            return a - B * (a / B);
        }

        public ivec3 mod(ivec3 a, int b)
        {
            debug.ivec3 B = ivec3(b);
            return a - B * (a / B);
        }

        public ivec4 mod(ivec4 a, int b)
        {
            debug.ivec4 B = ivec4(b);
            return a - B * (a / B);
        }

        public uint mod(uint a, uint b)
        {
            return a - b * (a / b);
        }

        public uvec2 mod(uvec2 a, uvec2 b)
        {
            return a - b * (a / b);
        }

        public uvec3 mod(uvec3 a, uvec3 b)
        {
            return a - b * (a / b);
        }

        public uvec4 mod(uvec4 a, uvec4 b)
        {
            return a - b * (a / b);
        }

        public uvec2 mod(uvec2 a, uint b)
        {
            debug.uvec2 B = uvec2(b);
            return a - B * (a / B);
        }

        public uvec3 mod(uvec3 a, uint b)
        {
            debug.uvec3 B = uvec3(b);
            return a - B * (a / B);
        }

        public uvec4 mod(uvec4 a, uint b)
        {
            debug.uvec4 B = uvec4(b);
            return a - B * (a / B);
        }
        #endregion
        #region min
        public float min(float a, float b)
        {
            return Math.Min(a, b);
        }

        public vec2 min(vec2 a, vec2 b)
        {
            return new debug.vec2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
        }

        public vec3 min(vec3 a, vec3 b)
        {
            return new debug.vec3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        public vec4 min(vec4 a, vec4 b)
        {
            return new debug.vec4(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z), Math.Min(a.w, b.w));
        }

        public vec2 min(vec2 a, float b)
        {
            return new debug.vec2(Math.Min(a.x, b), Math.Min(a.y, b));
        }

        public vec3 min(vec3 a, float b)
        {
            return new debug.vec3(Math.Min(a.x, b), Math.Min(a.y, b), Math.Min(a.z, b));
        }

        public vec4 min(vec4 a, float b)
        {
            return new debug.vec4(Math.Min(a.x, b), Math.Min(a.y, b), Math.Min(a.z, b), Math.Min(a.w, b));
        }

        public double min(double a, double b)
        {
            return Math.Min(a, b);
        }

        public dvec2 min(dvec2 a, dvec2 b)
        {
            return new debug.dvec2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
        }

        public dvec3 min(dvec3 a, dvec3 b)
        {
            return new debug.dvec3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        public dvec4 min(dvec4 a, dvec4 b)
        {
            return new debug.dvec4(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z), Math.Min(a.w, b.w));
        }

        public dvec2 min(dvec2 a, double b)
        {
            return new debug.dvec2(Math.Min(a.x, b), Math.Min(a.y, b));
        }

        public dvec3 min(dvec3 a, double b)
        {
            return new debug.dvec3(Math.Min(a.x, b), Math.Min(a.y, b), Math.Min(a.z, b));
        }

        public dvec4 min(dvec4 a, double b)
        {
            return new debug.dvec4(Math.Min(a.x, b), Math.Min(a.y, b), Math.Min(a.z, b), Math.Min(a.w, b));
        }

        public int min(int a, int b)
        {
            return Math.Min(a, b);
        }

        public ivec2 min(ivec2 a, ivec2 b)
        {
            return new debug.ivec2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
        }

        public ivec3 min(ivec3 a, ivec3 b)
        {
            return new debug.ivec3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        public ivec4 min(ivec4 a, ivec4 b)
        {
            return new debug.ivec4(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z), Math.Min(a.w, b.w));
        }

        public ivec2 min(ivec2 a, int b)
        {
            return new debug.ivec2(Math.Min(a.x, b), Math.Min(a.y, b));
        }

        public ivec3 min(ivec3 a, int b)
        {
            return new debug.ivec3(Math.Min(a.x, b), Math.Min(a.y, b), Math.Min(a.z, b));
        }

        public ivec4 min(ivec4 a, int b)
        {
            return new debug.ivec4(Math.Min(a.x, b), Math.Min(a.y, b), Math.Min(a.z, b), Math.Min(a.w, b));
        }

        public uint min(uint a, uint b)
        {
            return Math.Min(a, b);
        }

        public uvec2 min(uvec2 a, uvec2 b)
        {
            return new debug.uvec2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
        }

        public uvec3 min(uvec3 a, uvec3 b)
        {
            return new debug.uvec3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        public uvec4 min(uvec4 a, uvec4 b)
        {
            return new debug.uvec4(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z), Math.Min(a.w, b.w));
        }

        public uvec2 min(uvec2 a, uint b)
        {
            return new debug.uvec2(Math.Min(a.x, b), Math.Min(a.y, b));
        }

        public uvec3 min(uvec3 a, uint b)
        {
            return new debug.uvec3(Math.Min(a.x, b), Math.Min(a.y, b), Math.Min(a.z, b));
        }

        public uvec4 min(uvec4 a, uint b)
        {
            return new debug.uvec4(Math.Min(a.x, b), Math.Min(a.y, b), Math.Min(a.z, b), Math.Min(a.w, b));
        }
        #endregion
        #region max
        public float max(float a, float b)
        {
            return Math.Max(a, b);
        }

        public vec2 max(vec2 a, vec2 b)
        {
            return new debug.vec2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
        }

        public vec3 max(vec3 a, vec3 b)
        {
            return new debug.vec3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }

        public vec4 max(vec4 a, vec4 b)
        {
            return new debug.vec4(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z), Math.Max(a.w, b.w));
        }

        public vec2 max(vec2 a, float b)
        {
            return new debug.vec2(Math.Max(a.x, b), Math.Max(a.y, b));
        }

        public vec3 max(vec3 a, float b)
        {
            return new debug.vec3(Math.Max(a.x, b), Math.Max(a.y, b), Math.Max(a.z, b));
        }

        public vec4 max(vec4 a, float b)
        {
            return new debug.vec4(Math.Max(a.x, b), Math.Max(a.y, b), Math.Max(a.z, b), Math.Max(a.w, b));
        }

        public double max(double a, double b)
        {
            return Math.Max(a, b);
        }

        public dvec2 max(dvec2 a, dvec2 b)
        {
            return new debug.dvec2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
        }

        public dvec3 max(dvec3 a, dvec3 b)
        {
            return new debug.dvec3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }

        public dvec4 max(dvec4 a, dvec4 b)
        {
            return new debug.dvec4(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z), Math.Max(a.w, b.w));
        }

        public dvec2 max(dvec2 a, double b)
        {
            return new debug.dvec2(Math.Max(a.x, b), Math.Max(a.y, b));
        }

        public dvec3 max(dvec3 a, double b)
        {
            return new debug.dvec3(Math.Max(a.x, b), Math.Max(a.y, b), Math.Max(a.z, b));
        }

        public dvec4 max(dvec4 a, double b)
        {
            return new debug.dvec4(Math.Max(a.x, b), Math.Max(a.y, b), Math.Max(a.z, b), Math.Max(a.w, b));
        }

        public int max(int a, int b)
        {
            return Math.Max(a, b);
        }

        public ivec2 max(ivec2 a, ivec2 b)
        {
            return new debug.ivec2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
        }

        public ivec3 max(ivec3 a, ivec3 b)
        {
            return new debug.ivec3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }

        public ivec4 max(ivec4 a, ivec4 b)
        {
            return new debug.ivec4(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z), Math.Max(a.w, b.w));
        }

        public ivec2 max(ivec2 a, int b)
        {
            return new debug.ivec2(Math.Max(a.x, b), Math.Max(a.y, b));
        }

        public ivec3 max(ivec3 a, int b)
        {
            return new debug.ivec3(Math.Max(a.x, b), Math.Max(a.y, b), Math.Max(a.z, b));
        }

        public ivec4 max(ivec4 a, int b)
        {
            return new debug.ivec4(Math.Max(a.x, b), Math.Max(a.y, b), Math.Max(a.z, b), Math.Max(a.w, b));
        }

        public uint max(uint a, uint b)
        {
            return Math.Max(a, b);
        }

        public uvec2 max(uvec2 a, uvec2 b)
        {
            return new debug.uvec2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
        }

        public uvec3 max(uvec3 a, uvec3 b)
        {
            return new debug.uvec3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }

        public uvec4 max(uvec4 a, uvec4 b)
        {
            return new debug.uvec4(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z), Math.Max(a.w, b.w));
        }

        public uvec2 max(uvec2 a, uint b)
        {
            return new debug.uvec2(Math.Max(a.x, b), Math.Max(a.y, b));
        }

        public uvec3 max(uvec3 a, uint b)
        {
            return new debug.uvec3(Math.Max(a.x, b), Math.Max(a.y, b), Math.Max(a.z, b));
        }

        public uvec4 max(uvec4 a, uint b)
        {
            return new debug.uvec4(Math.Max(a.x, b), Math.Max(a.y, b), Math.Max(a.z, b), Math.Max(a.w, b));
        }
        #endregion
        #region clamp
        public float clamp(float v, float minVal, float maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public vec2 clamp(vec2 v, vec2 minVal, vec2 maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public vec3 clamp(vec3 v, vec3 minVal, vec3 maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public vec4 clamp(vec4 v, vec4 minVal, vec4 maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public vec2 clamp(vec2 v, float minVal, float maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public vec3 clamp(vec3 v, float minVal, float maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public vec4 clamp(vec4 v, float minVal, float maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public double clamp(double v, double minVal, double maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public dvec2 clamp(dvec2 v, dvec2 minVal, dvec2 maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public dvec3 clamp(dvec3 v, dvec3 minVal, dvec3 maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public dvec4 clamp(dvec4 v, dvec4 minVal, dvec4 maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public dvec2 clamp(dvec2 v, double minVal, double maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public dvec3 clamp(dvec3 v, double minVal, double maxVal)
        {
            return max(min(v, minVal), maxVal);
        }

        public dvec4 clamp(dvec4 v, double minVal, double maxVal)
        {
            return max(min(v, minVal), maxVal);
        }
        #endregion
        #region mix
        public float mix(float x, float y, float t)
        {
            return x * (1 - t) + y * t;
        }

        public vec2 mix(vec2 x, vec2 y, vec2 t)
        {
            return x * (vec2(1) - t) + y * t;
        }

        public vec3 mix(vec3 x, vec3 y, vec3 t)
        {
            return x * (vec3(1) - t) + y * t;
        }

        public vec4 mix(vec4 x, vec4 y, vec4 t)
        {
            return x * (vec4(1) - t) + y * t;
        }

        public vec2 mix(vec2 x, vec2 y, float t)
        {
            return x * vec2(1 - t) + y * vec2(t);
        }

        public vec3 mix(vec3 x, vec3 y, float t)
        {
            return x * vec3(1 - t) + y * vec3(t);
        }

        public vec4 mix(vec4 x, vec4 y, float t)
        {
            return x * vec4(1 - t) + y * vec4(t);
        }

        public double mix(double x, double y, double t)
        {
            return x * (1 - t) + y * t;
        }

        public dvec2 mix(dvec2 x, dvec2 y, dvec2 t)
        {
            return x * (dvec2(1) - t) + y * t;
        }

        public dvec3 mix(dvec3 x, dvec3 y, dvec3 t)
        {
            return x * (dvec3(1) - t) + y * t;
        }

        public dvec4 mix(dvec4 x, dvec4 y, dvec4 t)
        {
            return x * (dvec4(1) - t) + y * t;
        }

        public dvec2 mix(dvec2 x, dvec2 y, double t)
        {
            return x * dvec2(1 - t) + y * dvec2(t);
        }

        public dvec3 mix(dvec3 x, dvec3 y, double t)
        {
            return x * dvec3(1 - t) + y * dvec3(t);
        }

        public dvec4 mix(dvec4 x, dvec4 y, double t)
        {
            return x * dvec4(1 - t) + y * dvec4(t);
        }
        #endregion
        #region step
        public float step(float edge, float x)
        {
            return x < edge ? 0f : 1f;
        }

        public vec2 step(vec2 edge, vec2 x)
        {
            return new debug.vec2(x.x < edge.x ? 0f : 1f, x.y < edge.y ? 0f : 1f);
        }

        public vec3 step(vec3 edge, vec3 x)
        {
            return new debug.vec3(x.x < edge.x ? 0f : 1f, x.y < edge.y ? 0f : 1f, x.z < edge.z ? 0f : 1f);
        }

        public vec4 step(vec4 edge, vec4 x)
        {
            return new debug.vec4(x.x < edge.x ? 0f : 1f, x.y < edge.y ? 0f : 1f, x.z < edge.z ? 0f : 1f, x.w < edge.w ? 0f : 1f);
        }

        public vec2 step(float edge, vec2 x)
        {
            return new debug.vec2(x.x < edge ? 0f : 1f, x.y < edge ? 0f : 1f);
        }

        public vec3 step(float edge, vec3 x)
        {
            return new debug.vec3(x.x < edge ? 0f : 1f, x.y < edge ? 0f : 1f, x.z < edge ? 0f : 1f);
        }

        public vec4 step(float edge, vec4 x)
        {
            return new debug.vec4(x.x < edge ? 0f : 1f, x.y < edge ? 0f : 1f, x.z < edge ? 0f : 1f, x.w < edge ? 0f : 1f);
        }

        public double step(double edge, double x)
        {
            return x < edge ? 0 : 1;
        }

        public dvec2 step(dvec2 edge, dvec2 x)
        {
            return new debug.dvec2(x.x < edge.x ? 0 : 1, x.y < edge.y ? 0 : 1);
        }

        public dvec3 step(dvec3 edge, dvec3 x)
        {
            return new debug.dvec3(x.x < edge.x ? 0 : 1, x.y < edge.y ? 0 : 1, x.z < edge.z ? 0 : 1);
        }

        public dvec4 step(dvec4 edge, dvec4 x)
        {
            return new debug.dvec4(x.x < edge.x ? 0 : 1, x.y < edge.y ? 0 : 1, x.z < edge.z ? 0 : 1, x.w < edge.w ? 0 : 1);
        }

        public dvec2 step(double edge, dvec2 x)
        {
            return new debug.dvec2(x.x < edge ? 0 : 1, x.y < edge ? 0 : 1);
        }

        public dvec3 step(double edge, dvec3 x)
        {
            return new debug.dvec3(x.x < edge ? 0 : 1, x.y < edge ? 0 : 1, x.z < edge ? 0 : 1);
        }

        public dvec4 step(double edge, dvec4 x)
        {
            return new debug.dvec4(x.x < edge ? 0 : 1, x.y < edge ? 0 : 1, x.z < edge ? 0 : 1, x.w < edge ? 0 : 1);
        }
        #endregion
        #region smoothstep
        public float smoothstep(float edge0, float edge1, float x)
        {
            if (x < edge0) return 0f;
            if (x > edge1) return 1f;
            return (x - edge0) / (edge1 - edge0);
        }

        public vec2 smoothstep(vec2 edge0, vec2 edge1, vec2 x)
        {
            return vec2(smoothstep(edge0.x, edge1.x, x.x), smoothstep(edge0.y, edge1.y, x.y));
        }

        public vec3 smoothstep(vec3 edge0, vec3 edge1, vec3 x)
        {
            return vec3(smoothstep(edge0.x, edge1.x, x.x), smoothstep(edge0.y, edge1.y, x.y), smoothstep(edge0.z, edge1.z, x.z));
        }

        public vec4 smoothstep(vec4 edge0, vec4 edge1, vec4 x)
        {
            return vec4(smoothstep(edge0.x, edge1.x, x.x), smoothstep(edge0.y, edge1.y, x.y), smoothstep(edge0.z, edge1.z, x.z), smoothstep(edge0.w, edge1.w, x.w));
        }

        public vec2 smoothstep(float edge0, float edge1, vec2 x)
        {
            return vec2(smoothstep(edge0, edge1, x.x), smoothstep(edge0, edge1, x.y));
        }

        public vec3 smoothstep(float edge0, float edge1, vec3 x)
        {
            return vec3(smoothstep(edge0, edge1, x.x), smoothstep(edge0, edge1, x.y), smoothstep(edge0, edge1, x.z));
        }

        public vec4 smoothstep(float edge0, float edge1, vec4 x)
        {
            return vec4(smoothstep(edge0, edge1, x.x), smoothstep(edge0, edge1, x.y), smoothstep(edge0, edge1, x.z), smoothstep(edge0, edge1, x.w));
        }

        public double smoothstep(double edge0, double edge1, double x)
        {
            if (x < edge0) return 0;
            if (x > edge1) return 1;
            return (x - edge0) / (edge1 - edge0);
        }

        public dvec2 smoothstep(dvec2 edge0, dvec2 edge1, dvec2 x)
        {
            return dvec2(smoothstep(edge0.x, edge1.x, x.x), smoothstep(edge0.y, edge1.y, x.y));
        }

        public dvec3 smoothstep(dvec3 edge0, dvec3 edge1, dvec3 x)
        {
            return dvec3(smoothstep(edge0.x, edge1.x, x.x), smoothstep(edge0.y, edge1.y, x.y), smoothstep(edge0.z, edge1.z, x.z));
        }

        public dvec4 smoothstep(dvec4 edge0, dvec4 edge1, dvec4 x)
        {
            return dvec4(smoothstep(edge0.x, edge1.x, x.x), smoothstep(edge0.y, edge1.y, x.y), smoothstep(edge0.z, edge1.z, x.z), smoothstep(edge0.w, edge1.w, x.w));
        }

        public dvec2 smoothstep(double edge0, double edge1, dvec2 x)
        {
            return dvec2(smoothstep(edge0, edge1, x.x), smoothstep(edge0, edge1, x.y));
        }

        public dvec3 smoothstep(double edge0, double edge1, dvec3 x)
        {
            return dvec3(smoothstep(edge0, edge1, x.x), smoothstep(edge0, edge1, x.y), smoothstep(edge0, edge1, x.z));
        }

        public dvec4 smoothstep(double edge0, double edge1, dvec4 x)
        {
            return dvec4(smoothstep(edge0, edge1, x.x), smoothstep(edge0, edge1, x.y), smoothstep(edge0, edge1, x.z), smoothstep(edge0, edge1, x.w));
        }
        #endregion
        #region faceforward
        public float faceforward(float N, float I, float Nref)
        {
            return I * Nref < 0 ? -N : N;
        }

        public vec2 faceforward(vec2 N, vec2 I, vec2 Nref)
        {
            return dot(I, Nref) < 0 ? -N : N;
        }

        public vec3 faceforward(vec3 N, vec3 I, vec3 Nref)
        {
            return dot(I, Nref) < 0 ? -N : N;
        }

        public vec4 faceforward(vec4 N, vec4 I, vec4 Nref)
        {
            return dot(I, Nref) < 0 ? -N : N;
        }

        public double faceforward(double N, double I, double Nref)
        {
            return I * Nref < 0 ? -N : N;
        }

        public dvec2 faceforward(dvec2 N, dvec2 I, dvec2 Nref)
        {
            return dot(I, Nref) < 0 ? -N : N;
        }

        public dvec3 faceforward(dvec3 N, dvec3 I, dvec3 Nref)
        {
            return dot(I, Nref) < 0 ? -N : N;
        }

        public dvec4 faceforward(dvec4 N, dvec4 I, dvec4 Nref)
        {
            return dot(I, Nref) < 0 ? -N : N;
        }
        #endregion
        #region reflect
        public float reflect(float I, float N)
        {
            return I - 2f * (N * I) * N;
        }

        public vec2 reflect(vec2 I, vec2 N)
        {
            return I - vec2(2f * dot(N, I)) * N;
        }

        public vec3 reflect(vec3 I, vec3 N)
        {
            return I - vec3(2f * dot(N, I)) * N;
        }

        public vec4 reflect(vec4 I, vec4 N)
        {
            return I - vec4(2f * dot(N, I)) * N;
        }

        public double reflect(double I, double N)
        {
            return I - 2f * (N * I) * N;
        }

        public dvec2 reflect(dvec2 I, dvec2 N)
        {
            return I - dvec2(2f * dot(N, I)) * N;
        }

        public dvec3 reflect(dvec3 I, dvec3 N)
        {
            return I - dvec3(2f * dot(N, I)) * N;
        }

        public dvec4 reflect(dvec4 I, dvec4 N)
        {
            return I - dvec4(2f * dot(N, I)) * N;
        }
        #endregion
        #region refract
        public float refract(float I, float N, float eta)
        {
            var ni = N * I;
            var k = 1f - eta * eta * (1f - ni * ni);
            return k < 0 ? 0f : eta * I - (eta * ni + sqrt(k)) * N;
        }

        public vec2 refract(vec2 I, vec2 N, float eta)
        {
            var ni = dot(N, I);
            var k = 1f - eta * eta * (1f - ni);
            return k < 0 ? vec2(0f) : eta * I - (eta * ni + sqrt(k)) * N;
        }

        public vec3 refract(vec3 I, vec3 N, float eta)
        {
            var ni = dot(N, I);
            var k = 1f - eta * eta * (1f - ni);
            return k < 0 ? vec3(0f) : eta * I - (eta * ni + sqrt(k)) * N;
        }

        public vec4 refract(vec4 I, vec4 N, float eta)
        {
            var ni = dot(N, I);
            var k = 1f - eta * eta * (1f - ni);
            return k < 0 ? vec4(0f) : eta * I - (eta * ni + sqrt(k)) * N;
        }

        public double refract(double I, double N, float eta)
        {
            var ni = N * I;
            var k = 1f - eta * eta * (1f - ni * ni);
            return k < 0 ? 0 : eta * I - (eta * ni + sqrt(k)) * N;
        }

        public dvec2 refract(dvec2 I, dvec2 N, float eta)
        {
            var ni = dot(N, I);
            var k = 1f - eta * eta * (1f - ni);
            return k < 0 ? dvec2(0) : eta * I - (eta * ni + sqrt(k)) * N;
        }

        public dvec3 refract(dvec3 I, dvec3 N, float eta)
        {
            var ni = dot(N, I);
            var k = 1f - eta * eta * (1f - ni);
            return k < 0 ? dvec3(0) : eta * I - (eta * ni + sqrt(k)) * N;
        }

        public dvec4 refract(dvec4 I, dvec4 N, float eta)
        {
            var ni = dot(N, I);
            var k = 1f - eta * eta * (1f - ni);
            return k < 0 ? dvec4(0) : eta * I - (eta * ni + sqrt(k)) * N;
        }
        #endregion
        #region lessThan
        public bvec2 lessThan(vec2 x, vec2 y)
        {
            return bvec2(x.x < y.x, x.y < y.y);
        }

        public bvec3 lessThan(vec3 x, vec3 y)
        {
            return bvec3(x.x < y.x, x.y < y.y, x.z < y.z);
        }

        public bvec4 lessThan(vec4 x, vec4 y)
        {
            return bvec4(x.x < y.x, x.y < y.y, x.z < y.z, x.w < y.w);
        }

        public bvec2 lessThan(ivec2 x, ivec2 y)
        {
            return bvec2(x.x < y.x, x.y < y.y);
        }

        public bvec3 lessThan(ivec3 x, ivec3 y)
        {
            return bvec3(x.x < y.x, x.y < y.y, x.z < y.z);
        }

        public bvec4 lessThan(ivec4 x, ivec4 y)
        {
            return bvec4(x.x < y.x, x.y < y.y, x.z < y.z, x.w < y.w);
        }

        public bvec2 lessThan(uvec2 x, uvec2 y)
        {
            return bvec2(x.x < y.x, x.y < y.y);
        }

        public bvec3 lessThan(uvec3 x, uvec3 y)
        {
            return bvec3(x.x < y.x, x.y < y.y, x.z < y.z);
        }

        public bvec4 lessThan(uvec4 x, uvec4 y)
        {
            return bvec4(x.x < y.x, x.y < y.y, x.z < y.z, x.w < y.w);
        }

        public bvec2 lessThan(dvec2 x, dvec2 y)
        {
            return bvec2(x.x < y.x, x.y < y.y);
        }

        public bvec3 lessThan(dvec3 x, dvec3 y)
        {
            return bvec3(x.x < y.x, x.y < y.y, x.z < y.z);
        }

        public bvec4 lessThan(dvec4 x, dvec4 y)
        {
            return bvec4(x.x < y.x, x.y < y.y, x.z < y.z, x.w < y.w);
        }
        #endregion
        #region lessThanEqual
        public bvec2 lessThanEqual(vec2 x, vec2 y)
        {
            return bvec2(x.x <= y.x, x.y <= y.y);
        }

        public bvec3 lessThanEqual(vec3 x, vec3 y)
        {
            return bvec3(x.x <= y.x, x.y <= y.y, x.z <= y.z);
        }

        public bvec4 lessThanEqual(vec4 x, vec4 y)
        {
            return bvec4(x.x <= y.x, x.y <= y.y, x.z <= y.z, x.w <= y.w);
        }

        public bvec2 lessThanEqual(ivec2 x, ivec2 y)
        {
            return bvec2(x.x <= y.x, x.y <= y.y);
        }

        public bvec3 lessThanEqual(ivec3 x, ivec3 y)
        {
            return bvec3(x.x <= y.x, x.y <= y.y, x.z <= y.z);
        }

        public bvec4 lessThanEqual(ivec4 x, ivec4 y)
        {
            return bvec4(x.x <= y.x, x.y <= y.y, x.z <= y.z, x.w <= y.w);
        }

        public bvec2 lessThanEqual(uvec2 x, uvec2 y)
        {
            return bvec2(x.x <= y.x, x.y <= y.y);
        }

        public bvec3 lessThanEqual(uvec3 x, uvec3 y)
        {
            return bvec3(x.x <= y.x, x.y <= y.y, x.z <= y.z);
        }

        public bvec4 lessThanEqual(uvec4 x, uvec4 y)
        {
            return bvec4(x.x <= y.x, x.y <= y.y, x.z <= y.z, x.w <= y.w);
        }

        public bvec2 lessThanEqual(dvec2 x, dvec2 y)
        {
            return bvec2(x.x <= y.x, x.y <= y.y);
        }

        public bvec3 lessThanEqual(dvec3 x, dvec3 y)
        {
            return bvec3(x.x <= y.x, x.y <= y.y, x.z <= y.z);
        }

        public bvec4 lessThanEqual(dvec4 x, dvec4 y)
        {
            return bvec4(x.x <= y.x, x.y <= y.y, x.z <= y.z, x.w <= y.w);
        }
        #endregion
        #region greaterThan
        public bvec2 greaterThan(vec2 x, vec2 y)
        {
            return bvec2(x.x > y.x, x.y > y.y);
        }

        public bvec3 greaterThan(vec3 x, vec3 y)
        {
            return bvec3(x.x > y.x, x.y > y.y, x.z > y.z);
        }

        public bvec4 greaterThan(vec4 x, vec4 y)
        {
            return bvec4(x.x > y.x, x.y > y.y, x.z > y.z, x.w > y.w);
        }

        public bvec2 greaterThan(ivec2 x, ivec2 y)
        {
            return bvec2(x.x > y.x, x.y > y.y);
        }

        public bvec3 greaterThan(ivec3 x, ivec3 y)
        {
            return bvec3(x.x > y.x, x.y > y.y, x.z > y.z);
        }

        public bvec4 greaterThan(ivec4 x, ivec4 y)
        {
            return bvec4(x.x > y.x, x.y > y.y, x.z > y.z, x.w > y.w);
        }

        public bvec2 greaterThan(uvec2 x, uvec2 y)
        {
            return bvec2(x.x > y.x, x.y > y.y);
        }

        public bvec3 greaterThan(uvec3 x, uvec3 y)
        {
            return bvec3(x.x > y.x, x.y > y.y, x.z > y.z);
        }

        public bvec4 greaterThan(uvec4 x, uvec4 y)
        {
            return bvec4(x.x > y.x, x.y > y.y, x.z > y.z, x.w > y.w);
        }

        public bvec2 greaterThan(dvec2 x, dvec2 y)
        {
            return bvec2(x.x > y.x, x.y > y.y);
        }

        public bvec3 greaterThan(dvec3 x, dvec3 y)
        {
            return bvec3(x.x > y.x, x.y > y.y, x.z > y.z);
        }

        public bvec4 greaterThan(dvec4 x, dvec4 y)
        {
            return bvec4(x.x > y.x, x.y > y.y, x.z > y.z, x.w > y.w);
        }
        #endregion
        #region greaterThanEqual
        public bvec2 greaterThanEqual(vec2 x, vec2 y)
        {
            return bvec2(x.x >= y.x, x.y >= y.y);
        }

        public bvec3 greaterThanEqual(vec3 x, vec3 y)
        {
            return bvec3(x.x >= y.x, x.y >= y.y, x.z >= y.z);
        }

        public bvec4 greaterThanEqual(vec4 x, vec4 y)
        {
            return bvec4(x.x >= y.x, x.y >= y.y, x.z >= y.z, x.w >= y.w);
        }

        public bvec2 greaterThanEqual(ivec2 x, ivec2 y)
        {
            return bvec2(x.x >= y.x, x.y >= y.y);
        }

        public bvec3 greaterThanEqual(ivec3 x, ivec3 y)
        {
            return bvec3(x.x >= y.x, x.y >= y.y, x.z >= y.z);
        }

        public bvec4 greaterThanEqual(ivec4 x, ivec4 y)
        {
            return bvec4(x.x >= y.x, x.y >= y.y, x.z >= y.z, x.w >= y.w);
        }

        public bvec2 greaterThanEqual(uvec2 x, uvec2 y)
        {
            return bvec2(x.x >= y.x, x.y >= y.y);
        }

        public bvec3 greaterThanEqual(uvec3 x, uvec3 y)
        {
            return bvec3(x.x >= y.x, x.y >= y.y, x.z >= y.z);
        }

        public bvec4 greaterThanEqual(uvec4 x, uvec4 y)
        {
            return bvec4(x.x >= y.x, x.y >= y.y, x.z >= y.z, x.w >= y.w);
        }

        public bvec2 greaterThanEqual(dvec2 x, dvec2 y)
        {
            return bvec2(x.x >= y.x, x.y >= y.y);
        }

        public bvec3 greaterThanEqual(dvec3 x, dvec3 y)
        {
            return bvec3(x.x >= y.x, x.y >= y.y, x.z >= y.z);
        }

        public bvec4 greaterThanEqual(dvec4 x, dvec4 y)
        {
            return bvec4(x.x >= y.x, x.y >= y.y, x.z >= y.z, x.w >= y.w);
        }
        #endregion
        #region equal
        public bvec2 equal(vec2 x, vec2 y)
        {
            return bvec2(x.x == y.x, x.y == y.y);
        }

        public bvec3 equal(vec3 x, vec3 y)
        {
            return bvec3(x.x == y.x, x.y == y.y, x.z == y.z);
        }

        public bvec4 equal(vec4 x, vec4 y)
        {
            return bvec4(x.x == y.x, x.y == y.y, x.z == y.z, x.w == y.w);
        }

        public bvec2 equal(ivec2 x, ivec2 y)
        {
            return bvec2(x.x == y.x, x.y == y.y);
        }

        public bvec3 equal(ivec3 x, ivec3 y)
        {
            return bvec3(x.x == y.x, x.y == y.y, x.z == y.z);
        }

        public bvec4 equal(ivec4 x, ivec4 y)
        {
            return bvec4(x.x == y.x, x.y == y.y, x.z == y.z, x.w == y.w);
        }

        public bvec2 equal(uvec2 x, uvec2 y)
        {
            return bvec2(x.x == y.x, x.y == y.y);
        }

        public bvec3 equal(uvec3 x, uvec3 y)
        {
            return bvec3(x.x == y.x, x.y == y.y, x.z == y.z);
        }

        public bvec4 equal(uvec4 x, uvec4 y)
        {
            return bvec4(x.x == y.x, x.y == y.y, x.z == y.z, x.w == y.w);
        }

        public bvec2 equal(dvec2 x, dvec2 y)
        {
            return bvec2(x.x == y.x, x.y == y.y);
        }

        public bvec3 equal(dvec3 x, dvec3 y)
        {
            return bvec3(x.x == y.x, x.y == y.y, x.z == y.z);
        }

        public bvec4 equal(dvec4 x, dvec4 y)
        {
            return bvec4(x.x == y.x, x.y == y.y, x.z == y.z, x.w == y.w);
        }
        #endregion
        #region notEqual
        public bvec2 notEqual(vec2 x, vec2 y)
        {
            return bvec2(x.x != y.x, x.y != y.y);
        }

        public bvec3 notEqual(vec3 x, vec3 y)
        {
            return bvec3(x.x != y.x, x.y != y.y, x.z != y.z);
        }

        public bvec4 notEqual(vec4 x, vec4 y)
        {
            return bvec4(x.x != y.x, x.y != y.y, x.z != y.z, x.w != y.w);
        }

        public bvec2 notEqual(ivec2 x, ivec2 y)
        {
            return bvec2(x.x != y.x, x.y != y.y);
        }

        public bvec3 notEqual(ivec3 x, ivec3 y)
        {
            return bvec3(x.x != y.x, x.y != y.y, x.z != y.z);
        }

        public bvec4 notEqual(ivec4 x, ivec4 y)
        {
            return bvec4(x.x != y.x, x.y != y.y, x.z != y.z, x.w != y.w);
        }

        public bvec2 notEqual(uvec2 x, uvec2 y)
        {
            return bvec2(x.x != y.x, x.y != y.y);
        }

        public bvec3 notEqual(uvec3 x, uvec3 y)
        {
            return bvec3(x.x != y.x, x.y != y.y, x.z != y.z);
        }

        public bvec4 notEqual(uvec4 x, uvec4 y)
        {
            return bvec4(x.x != y.x, x.y != y.y, x.z != y.z, x.w != y.w);
        }

        public bvec2 notEqual(dvec2 x, dvec2 y)
        {
            return bvec2(x.x != y.x, x.y != y.y);
        }

        public bvec3 notEqual(dvec3 x, dvec3 y)
        {
            return bvec3(x.x != y.x, x.y != y.y, x.z != y.z);
        }

        public bvec4 notEqual(dvec4 x, dvec4 y)
        {
            return bvec4(x.x != y.x, x.y != y.y, x.z != y.z, x.w != y.w);
        }
        #endregion
        #region any
        public bool any(bvec2 x)
        {
            return x.x || x.y;
        }

        public bool any(bvec3 x)
        {
            return x.x || x.y || x.z;
        }

        public bool any(bvec4 x)
        {
            return x.x || x.y || x.z || x.w;
        }
        #endregion
        #region all
        public bool all(bvec2 x)
        {
            return x.x && x.y;
        }

        public bool all(bvec3 x)
        {
            return x.x && x.y && x.z;
        }

        public bool all(bvec4 x)
        {
            return x.x && x.y && x.z && x.w;
        }
        #endregion
        #region not
        public bvec2 not(bvec2 x)
        {
            return bvec2(!x.x, !x.y);
        }

        public bvec3 not(bvec3 x)
        {
            return bvec3(!x.x, !x.y, !x.z);
        }

        public bvec4 not(bvec4 x)
        {
            return bvec4(!x.x, !x.y, !x.z, !x.w);
        }
        #endregion
        #region TEXTURE
        vec4 texture(sampler1D sampler, float P, float bias = 0f)
        {
            return vec4();
        }
 
        vec4 texture(sampler2D sampler, vec2 P, float bias = 0f)
        {
            return vec4();
        }

        vec4 texture(sampler3D sampler, vec3 P, float bias = 0f)
        {
            return vec4();
        }

        vec4 texture(samplerCube sampler, vec3 P, float bias = 0f)
        {
            return vec4();
        }

        vec4 texture(sampler1DArray sampler, vec2 P, float bias = 0f)
        {
            return vec4();
        }

        vec4 texture(sampler2DArray sampler, vec3 P, float bias = 0f)
        {
            return vec4();
        }

        vec4 texture(samplerCubeArray sampler, vec4 P, float bias = 0f)
        {
            return vec4();
        }

        vec4 texture(sampler2DRect sampler, vec2 P)
        {
            return vec4();
        }

        ivec4 texture(isampler1D sampler, float P, float bias = 0f)
        {
            return ivec4();
        }

        ivec4 texture(isampler2D sampler, vec2 P, float bias = 0f)
        {
            return ivec4();
        }

        ivec4 texture(isampler3D sampler, vec3 P, float bias = 0f)
        {
            return ivec4();
        }

        ivec4 texture(isamplerCube sampler, vec3 P, float bias = 0f)
        {
            return ivec4();
        }

        ivec4 texture(isampler1DArray sampler, vec2 P, float bias = 0f)
        {
            return ivec4();
        }

        ivec4 texture(isampler2DArray sampler, vec3 P, float bias = 0f)
        {
            return ivec4();
        }

        ivec4 texture(isamplerCubeArray sampler, vec4 P, float bias = 0f)
        {
            return ivec4();
        }

        ivec4 texture(isampler2DRect sampler, vec2 P)
        {
            return ivec4();
        }

        uvec4 texture(usampler1D sampler, float P, float bias = 0f)
        {
            return uvec4();
        }

        uvec4 texture(usampler2D sampler, vec2 P, float bias = 0f)
        {
            return uvec4();
        }

        uvec4 texture(usampler3D sampler, vec3 P, float bias = 0f)
        {
            return uvec4();
        }

        uvec4 texture(usamplerCube sampler, vec3 P, float bias = 0f)
        {
            return uvec4();
        }

        uvec4 texture(usampler1DArray sampler, vec2 P, float bias = 0f)
        {
            return uvec4();
        }

        uvec4 texture(usampler2DArray sampler, vec3 P, float bias = 0f)
        {
            return uvec4();
        }

        uvec4 texture(usamplerCubeArray sampler, vec4 P, float bias = 0f)
        {
            return uvec4();
        }

        uvec4 texture(usampler2DRect sampler, vec2 P)
        {
            return uvec4();
        }

        float texture(sampler1DShadow sampler, vec3 P, float bias = 0f)
        {
            return 0f;
        }

        float texture(sampler2DShadow sampler, vec3 P, float bias = 0f)
        {
            return 0f;
        }

        float texture(samplerCubeShadow sampler, vec3 P, float bias = 0f)
        {
            return 0f;
        }

        float texture(sampler1DArrayShadow sampler, vec3 P, float bias = 0f)
        {
            return 0f;
        }

        float texture(sampler2DArrayShadow sampler, vec4 P, float bias = 0f)
        {
            return 0f;
        }

        float texture(sampler2DRectShadow sampler, vec3 P)
        {
            return 0f;
        }

        float texture(samplerCubeArrayShadow sampler, vec4 P, float compare)
        {
            return 0f;
        }
        #endregion
        #region TEXELFETCH
        #endregion
        #region TEXTURESIZE
        ivec2 textureSize(int sampler)
        {
            return ivec2();
        }
        #endregion
    }
}
