namespace App.debug
{
    class v2<T>
    {
        public T x, y;

        public v2() { }
        public v2(T x) { this.x = x; this.y = x; }
        public v2(T x, T y) { this.x = x; this.y = y; }

        public v2<T> xy { get { return new v2<T>(x, y); } }
        public v2<T> yx { get { return new v2<T>(y, x); } }
    }
    
    class v3<T> : v2<T>
    {
        public T z;

        public v3() { }
        public v3(T x) { this.x = x; this.y = x; this.z = x; }
        public v3(v2<T> xy, T z) { this.x = xy.x; this.y = xy.y; this.z = z; }
        public v3(T x, v2<T> yz) { this.x = x; this.y = yz.x; this.z = yz.y; }
        public v3(T x, T y, T z) { this.x = x; this.y = y; this.z = z; }

        public v3<T> xyz { get { return new v3<T>(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
        public v3<T> xzy { get { return new v3<T>(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }

        public v3<T> yxz { get { return new v3<T>(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }
        public v3<T> yzx { get { return new v3<T>(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }

        public v3<T> zxy { get { return new v3<T>(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }
        public v3<T> zyx { get { return new v3<T>(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }
    }
    
    class v4<T> : v3<T>
    {
        public T w;

        public v4() { }
        public v4(T x) { this.x = x; this.y = x; this.z = x; this.w = x; }
        public v4(T x, v3<T> yzw) { this.x = x; this.y = yzw.x; this.z = yzw.y; this.w = yzw.z; }
        public v4(v2<T> xy, v2<T> zw) { this.x = xy.x; this.y = xy.y; this.z = zw.x; this.w = zw.y; }
        public v4(v3<T> xyz, T w) { this.x = xyz.x; this.y = xyz.y; this.z = xyz.z; this.w = w; }
        public v4(T x, T y, T z, T w) { this.x = x; this.y = y; this.z = z; this.w = w; }

        public v4<T> xyzw { get { return new v4<T>(x,y,z,w); } set { x = value.x; y = value.y; z = value.z; w = value.w; } }
        public v4<T> xywz { get { return new v4<T>(x,y,w,z); } set { x = value.x; y = value.y; w = value.z; z = value.w; } }
        public v4<T> xzyw { get { return new v4<T>(x,z,y,w); } set { x = value.x; z = value.y; y = value.z; w = value.w; } }
        public v4<T> xzwy { get { return new v4<T>(x,z,w,y); } set { x = value.x; z = value.y; w = value.z; y = value.w; } }
        public v4<T> xwzy { get { return new v4<T>(x,w,z,y); } set { x = value.x; w = value.y; z = value.z; y = value.w; } }
        public v4<T> xwyz { get { return new v4<T>(x,w,y,z); } set { x = value.x; w = value.y; y = value.z; z = value.w; } }

        public v4<T> yxzw { get { return new v4<T>(y,x,z,w); } set { y = value.x; x = value.y; z = value.z; w = value.w; } }
        public v4<T> yxwz { get { return new v4<T>(y,x,w,z); } set { y = value.x; x = value.y; w = value.z; z = value.w; } }
        public v4<T> yzxw { get { return new v4<T>(y,z,x,w); } set { y = value.x; z = value.y; x = value.z; w = value.w; } }
        public v4<T> yzwx { get { return new v4<T>(y,z,w,x); } set { y = value.x; z = value.y; w = value.z; x = value.w; } }
        public v4<T> ywxz { get { return new v4<T>(y,w,x,z); } set { y = value.x; w = value.y; x = value.z; z = value.w; } }
        public v4<T> ywzx { get { return new v4<T>(y,w,z,x); } set { y = value.x; w = value.y; z = value.z; x = value.w; } }

        public v4<T> zxyw { get { return new v4<T>(z,x,y,w); } set { z = value.x; x = value.y; y = value.z; w = value.w; } }
        public v4<T> zxwy { get { return new v4<T>(z,x,w,y); } set { z = value.x; x = value.y; w = value.z; y = value.w; } }
        public v4<T> zyxw { get { return new v4<T>(z,y,x,w); } set { z = value.x; y = value.y; x = value.z; w = value.w; } }
        public v4<T> zywx { get { return new v4<T>(z,y,w,x); } set { z = value.x; y = value.y; w = value.z; x = value.w; } }
        public v4<T> zwxy { get { return new v4<T>(z,w,x,y); } set { z = value.x; w = value.y; x = value.z; y = value.w; } }
        public v4<T> zwyx { get { return new v4<T>(z,w,y,x); } set { z = value.x; w = value.y; y = value.z; x = value.w; } }

        public v4<T> wxyz { get { return new v4<T>(w,x,y,z); } set { w = value.x; x = value.y; y = value.z; z = value.w; } }
        public v4<T> wxzy { get { return new v4<T>(w,x,z,y); } set { w = value.x; x = value.y; z = value.z; y = value.w; } }
        public v4<T> wyxz { get { return new v4<T>(w,y,x,z); } set { w = value.x; y = value.y; x = value.z; z = value.w; } }
        public v4<T> wyzx { get { return new v4<T>(w,y,z,x); } set { w = value.x; y = value.y; z = value.z; x = value.w; } }
        public v4<T> wzxy { get { return new v4<T>(w,z,x,y); } set { w = value.x; z = value.y; x = value.z; y = value.w; } }
        public v4<T> wzyx { get { return new v4<T>(w,z,y,x); } set { w = value.x; z = value.y; y = value.z; x = value.w; } }
    }

    class vec2 : v2<float>
    {
        public vec2() { }
        public vec2(float x) : base(x) { }
        public vec2(float x, float y) : base(x, y) { }
        
        public static vec2 operator +(vec2 r) { return new vec2(r.x, r.y); }
        public static vec2 operator -(vec2 r) { return new vec2(-r.x, -r.y); }
        public static vec2 operator +(vec2 l, vec2 r) { return new vec2(l.x + r.x, l.y + r.y); }
        public static vec2 operator -(vec2 l, vec2 r) { return new vec2(l.x - r.x, l.y - r.y); }
        public static vec2 operator *(vec2 l, vec2 r) { return new vec2(l.x * r.x, l.y * r.y); }
        public static vec2 operator /(vec2 l, vec2 r) { return new vec2(l.x / r.x, l.y / r.y); }
        public static vec2 operator +(vec2 l, float r) { return new vec2(l.x + r, l.y + r); }
        public static vec2 operator -(vec2 l, float r) { return new vec2(l.x - r, l.y - r); }
        public static vec2 operator *(vec2 l, float r) { return new vec2(l.x * r, l.y * r); }
        public static vec2 operator /(vec2 l, float r) { return new vec2(l.x / r, l.y / r); }
        public static vec2 operator +(float l, vec2 r) { return new vec2(l+ r.x, l + r.y); }
        public static vec2 operator -(float l, vec2 r) { return new vec2(l - r.x, l - r.y); }
        public static vec2 operator *(float l, vec2 r) { return new vec2(l * r.x, l * r.y); }
        public static vec2 operator /(float l, vec2 r) { return new vec2(l / r.x, l / r.y); }
    }

    class vec3 : v3<float>
    {
        public vec3() { }
        public vec3(float x) : base(x) { }
        public vec3(float x, vec2 yz) : base(x, yz) { }
        public vec3(vec2 xy, float z) : base(xy, z) { }
        public vec3(float x, float y, float z) : base(x, y, z) { }
        
        public static vec3 operator +(vec3 r) { return new vec3(r.x, r.y, r.z); }
        public static vec3 operator -(vec3 r) { return new vec3(-r.x, -r.y, -r.z); }
        public static vec3 operator +(vec3 l, vec3 r) { return new vec3(l.x + r.x, l.y + r.y, l.z + r.z); }
        public static vec3 operator -(vec3 l, vec3 r) { return new vec3(l.x - r.x, l.y - r.y, l.z - r.z); }
        public static vec3 operator *(vec3 l, vec3 r) { return new vec3(l.x * r.x, l.y * r.y, l.z * r.z); }
        public static vec3 operator /(vec3 l, vec3 r) { return new vec3(l.x / r.x, l.y / r.y, l.z / r.z); }
        public static vec3 operator +(vec3 l, float r) { return new vec3(l.x + r, l.y + r, l.z + r); }
        public static vec3 operator -(vec3 l, float r) { return new vec3(l.x - r, l.y - r, l.z - r); }
        public static vec3 operator *(vec3 l, float r) { return new vec3(l.x * r, l.y * r, l.z * r); }
        public static vec3 operator /(vec3 l, float r) { return new vec3(l.x / r, l.y / r, l.z / r); }
        public static vec3 operator +(float l, vec3 r) { return new vec3(l + r.x, l + r.y, l + r.z); }
        public static vec3 operator -(float l, vec3 r) { return new vec3(l - r.x, l - r.y, l - r.z); }
        public static vec3 operator *(float l, vec3 r) { return new vec3(l * r.x, l * r.y, l * r.z); }
        public static vec3 operator /(float l, vec3 r) { return new vec3(l / r.x, l / r.y, l / r.z); }
    }

    class vec4 : v4<float>
    {
        public vec4() { }
        public vec4(float x) : base(x) { }
        public vec4(vec2 xy, vec2 zw) : base(xy, zw) { }
        public vec4(float x, vec3 yzw) : base(x, yzw) { }
        public vec4(vec3 xyz, float w) : base(xyz, w) { }
        public vec4(float x, float y, float z, float w) : base(x, y, z, w) { }
        
        public static vec4 operator +(vec4 r) { return new vec4(r.x, r.y, r.z, r.w); }
        public static vec4 operator -(vec4 r) { return new vec4(-r.x, -r.y, -r.z, -r.w); }
        public static vec4 operator +(vec4 l, vec4 r) { return new vec4(l.x + r.x, l.y + r.y, l.z + r.z, l.w + r.w); }
        public static vec4 operator -(vec4 l, vec4 r) { return new vec4(l.x - r.x, l.y - r.y, l.z - r.z, l.w - r.w); }
        public static vec4 operator *(vec4 l, vec4 r) { return new vec4(l.x * r.x, l.y * r.y, l.z * r.z, l.w * r.w); }
        public static vec4 operator /(vec4 l, vec4 r) { return new vec4(l.x / r.x, l.y / r.y, l.z / r.z, l.w / r.w); }
        public static vec4 operator +(vec4 l, float r) { return new vec4(l.x + r, l.y + r, l.z + r, l.w + r); }
        public static vec4 operator -(vec4 l, float r) { return new vec4(l.x - r, l.y - r, l.z - r, l.w - r); }
        public static vec4 operator *(vec4 l, float r) { return new vec4(l.x * r, l.y * r, l.z * r, l.w * r); }
        public static vec4 operator /(vec4 l, float r) { return new vec4(l.x / r, l.y / r, l.z / r, l.w / r); }
        public static vec4 operator +(float l, vec4 r) { return new vec4(l + r.x, l + r.y, l + r.z, l + r.w); }
        public static vec4 operator -(float l, vec4 r) { return new vec4(l - r.x, l - r.y, l - r.z, l - r.w); }
        public static vec4 operator *(float l, vec4 r) { return new vec4(l * r.x, l * r.y, l * r.z, l * r.w); }
        public static vec4 operator /(float l, vec4 r) { return new vec4(l / r.x, l / r.y, l / r.z, l / r.w); }
    }

    class dvec2 : v2<double>
    {
        public dvec2() { }
        public dvec2(double x) : base(x) { }
        public dvec2(double x, double y) : base(x, y) { }
        
        public static dvec2 operator +(dvec2 r) { return new dvec2(r.x, r.y); }
        public static dvec2 operator -(dvec2 r) { return new dvec2(-r.x, -r.y); }
        public static dvec2 operator +(dvec2 l, dvec2 r) { return new dvec2(l.x + r.x, l.y + r.y); }
        public static dvec2 operator -(dvec2 l, dvec2 r) { return new dvec2(l.x - r.x, l.y - r.y); }
        public static dvec2 operator *(dvec2 l, dvec2 r) { return new dvec2(l.x * r.x, l.y * r.y); }
        public static dvec2 operator /(dvec2 l, dvec2 r) { return new dvec2(l.x / r.x, l.y / r.y); }
        public static dvec2 operator +(dvec2 l, double r) { return new dvec2(l.x + r, l.y + r); }
        public static dvec2 operator -(dvec2 l, double r) { return new dvec2(l.x - r, l.y - r); }
        public static dvec2 operator *(dvec2 l, double r) { return new dvec2(l.x * r, l.y * r); }
        public static dvec2 operator /(dvec2 l, double r) { return new dvec2(l.x / r, l.y / r); }
        public static dvec2 operator +(double l, dvec2 r) { return new dvec2(l+ r.x, l + r.y); }
        public static dvec2 operator -(double l, dvec2 r) { return new dvec2(l - r.x, l - r.y); }
        public static dvec2 operator *(double l, dvec2 r) { return new dvec2(l * r.x, l * r.y); }
        public static dvec2 operator /(double l, dvec2 r) { return new dvec2(l / r.x, l / r.y); }
    }

    class dvec3 : v3<double>
    {
        public dvec3() { }
        public dvec3(double x) : base(x) { }
        public dvec3(double x, dvec2 yz) : base(x, yz) { }
        public dvec3(dvec2 xy, double z) : base(xy, z) { }
        public dvec3(double x, double y, double z) : base(x, y, z) { }
        
        public static dvec3 operator +(dvec3 r) { return new dvec3(r.x, r.y, r.z); }
        public static dvec3 operator -(dvec3 r) { return new dvec3(-r.x, -r.y, -r.z); }
        public static dvec3 operator +(dvec3 l, dvec3 r) { return new dvec3(l.x + r.x, l.y + r.y, l.z + r.z); }
        public static dvec3 operator -(dvec3 l, dvec3 r) { return new dvec3(l.x - r.x, l.y - r.y, l.z - r.z); }
        public static dvec3 operator *(dvec3 l, dvec3 r) { return new dvec3(l.x * r.x, l.y * r.y, l.z * r.z); }
        public static dvec3 operator /(dvec3 l, dvec3 r) { return new dvec3(l.x / r.x, l.y / r.y, l.z / r.z); }
        public static dvec3 operator +(dvec3 l, double r) { return new dvec3(l.x + r, l.y + r, l.z + r); }
        public static dvec3 operator -(dvec3 l, double r) { return new dvec3(l.x - r, l.y - r, l.z - r); }
        public static dvec3 operator *(dvec3 l, double r) { return new dvec3(l.x * r, l.y * r, l.z * r); }
        public static dvec3 operator /(dvec3 l, double r) { return new dvec3(l.x / r, l.y / r, l.z / r); }
        public static dvec3 operator +(double l, dvec3 r) { return new dvec3(l + r.x, l + r.y, l + r.z); }
        public static dvec3 operator -(double l, dvec3 r) { return new dvec3(l - r.x, l - r.y, l - r.z); }
        public static dvec3 operator *(double l, dvec3 r) { return new dvec3(l * r.x, l * r.y, l * r.z); }
        public static dvec3 operator /(double l, dvec3 r) { return new dvec3(l / r.x, l / r.y, l / r.z); }
    }

    class dvec4 : v4<double>
    {
        public dvec4() { }
        public dvec4(double x) : base(x) { }
        public dvec4(dvec2 xy, dvec2 zw) : base(xy, zw) { }
        public dvec4(double x, dvec3 yzw) : base(x, yzw) { }
        public dvec4(dvec3 xyz, double w) : base(xyz, w) { }
        public dvec4(double x, double y, double z, double w) : base(x, y, z, w) { }
        
        public static dvec4 operator +(dvec4 r) { return new dvec4(r.x, r.y, r.z, r.w); }
        public static dvec4 operator -(dvec4 r) { return new dvec4(-r.x, -r.y, -r.z, -r.w); }
        public static dvec4 operator +(dvec4 l, dvec4 r) { return new dvec4(l.x + r.x, l.y + r.y, l.z + r.z, l.w + r.w); }
        public static dvec4 operator -(dvec4 l, dvec4 r) { return new dvec4(l.x - r.x, l.y - r.y, l.z - r.z, l.w - r.w); }
        public static dvec4 operator *(dvec4 l, dvec4 r) { return new dvec4(l.x * r.x, l.y * r.y, l.z * r.z, l.w * r.w); }
        public static dvec4 operator /(dvec4 l, dvec4 r) { return new dvec4(l.x / r.x, l.y / r.y, l.z / r.z, l.w / r.w); }
        public static dvec4 operator +(dvec4 l, double r) { return new dvec4(l.x + r, l.y + r, l.z + r, l.w + r); }
        public static dvec4 operator -(dvec4 l, double r) { return new dvec4(l.x - r, l.y - r, l.z - r, l.w - r); }
        public static dvec4 operator *(dvec4 l, double r) { return new dvec4(l.x * r, l.y * r, l.z * r, l.w * r); }
        public static dvec4 operator /(dvec4 l, double r) { return new dvec4(l.x / r, l.y / r, l.z / r, l.w / r); }
        public static dvec4 operator +(double l, dvec4 r) { return new dvec4(l + r.x, l + r.y, l + r.z, l + r.w); }
        public static dvec4 operator -(double l, dvec4 r) { return new dvec4(l - r.x, l - r.y, l - r.z, l - r.w); }
        public static dvec4 operator *(double l, dvec4 r) { return new dvec4(l * r.x, l * r.y, l * r.z, l * r.w); }
        public static dvec4 operator /(double l, dvec4 r) { return new dvec4(l / r.x, l / r.y, l / r.z, l / r.w); }
    }

    class ivec2 : v2<int>
    {
        public ivec2() { }
        public ivec2(int x) : base(x) { }
        public ivec2(int x, int y) : base(x, y) { }
        
        public static ivec2 operator +(ivec2 r) { return new ivec2(r.x, r.y); }
        public static ivec2 operator -(ivec2 r) { return new ivec2(-r.x, -r.y); }
        public static ivec2 operator +(ivec2 l, ivec2 r) { return new ivec2(l.x + r.x, l.y + r.y); }
        public static ivec2 operator -(ivec2 l, ivec2 r) { return new ivec2(l.x - r.x, l.y - r.y); }
        public static ivec2 operator *(ivec2 l, ivec2 r) { return new ivec2(l.x * r.x, l.y * r.y); }
        public static ivec2 operator /(ivec2 l, ivec2 r) { return new ivec2(l.x / r.x, l.y / r.y); }
        public static ivec2 operator +(ivec2 l, int r) { return new ivec2(l.x + r, l.y + r); }
        public static ivec2 operator -(ivec2 l, int r) { return new ivec2(l.x - r, l.y - r); }
        public static ivec2 operator *(ivec2 l, int r) { return new ivec2(l.x * r, l.y * r); }
        public static ivec2 operator /(ivec2 l, int r) { return new ivec2(l.x / r, l.y / r); }
        public static ivec2 operator +(int l, ivec2 r) { return new ivec2(l+ r.x, l + r.y); }
        public static ivec2 operator -(int l, ivec2 r) { return new ivec2(l - r.x, l - r.y); }
        public static ivec2 operator *(int l, ivec2 r) { return new ivec2(l * r.x, l * r.y); }
        public static ivec2 operator /(int l, ivec2 r) { return new ivec2(l / r.x, l / r.y); }
    }

    class ivec3 : v3<int>
    {
        public ivec3() { }
        public ivec3(int x) : base(x) { }
        public ivec3(int x, ivec2 yz) : base(x, yz) { }
        public ivec3(ivec2 xy, int z) : base(xy, z) { }
        public ivec3(int x, int y, int z) : base(x, y, z) { }
        
        public static ivec3 operator +(ivec3 r) { return new ivec3(r.x, r.y, r.z); }
        public static ivec3 operator -(ivec3 r) { return new ivec3(-r.x, -r.y, -r.z); }
        public static ivec3 operator +(ivec3 l, ivec3 r) { return new ivec3(l.x + r.x, l.y + r.y, l.z + r.z); }
        public static ivec3 operator -(ivec3 l, ivec3 r) { return new ivec3(l.x - r.x, l.y - r.y, l.z - r.z); }
        public static ivec3 operator *(ivec3 l, ivec3 r) { return new ivec3(l.x * r.x, l.y * r.y, l.z * r.z); }
        public static ivec3 operator /(ivec3 l, ivec3 r) { return new ivec3(l.x / r.x, l.y / r.y, l.z / r.z); }
        public static ivec3 operator +(ivec3 l, int r) { return new ivec3(l.x + r, l.y + r, l.z + r); }
        public static ivec3 operator -(ivec3 l, int r) { return new ivec3(l.x - r, l.y - r, l.z - r); }
        public static ivec3 operator *(ivec3 l, int r) { return new ivec3(l.x * r, l.y * r, l.z * r); }
        public static ivec3 operator /(ivec3 l, int r) { return new ivec3(l.x / r, l.y / r, l.z / r); }
        public static ivec3 operator +(int l, ivec3 r) { return new ivec3(l + r.x, l + r.y, l + r.z); }
        public static ivec3 operator -(int l, ivec3 r) { return new ivec3(l - r.x, l - r.y, l - r.z); }
        public static ivec3 operator *(int l, ivec3 r) { return new ivec3(l * r.x, l * r.y, l * r.z); }
        public static ivec3 operator /(int l, ivec3 r) { return new ivec3(l / r.x, l / r.y, l / r.z); }
    }

    class ivec4 : v4<int>
    {
        public ivec4() { }
        public ivec4(int x) : base(x) { }
        public ivec4(ivec2 xy, ivec2 zw) : base(xy, zw) { }
        public ivec4(int x, ivec3 yzw) : base(x, yzw) { }
        public ivec4(ivec3 xyz, int w) : base(xyz, w) { }
        public ivec4(int x, int y, int z, int w) : base(x, y, z, w) { }
        
        public static ivec4 operator +(ivec4 r) { return new ivec4(r.x, r.y, r.z, r.w); }
        public static ivec4 operator -(ivec4 r) { return new ivec4(-r.x, -r.y, -r.z, -r.w); }
        public static ivec4 operator +(ivec4 l, ivec4 r) { return new ivec4(l.x + r.x, l.y + r.y, l.z + r.z, l.w + r.w); }
        public static ivec4 operator -(ivec4 l, ivec4 r) { return new ivec4(l.x - r.x, l.y - r.y, l.z - r.z, l.w - r.w); }
        public static ivec4 operator *(ivec4 l, ivec4 r) { return new ivec4(l.x * r.x, l.y * r.y, l.z * r.z, l.w * r.w); }
        public static ivec4 operator /(ivec4 l, ivec4 r) { return new ivec4(l.x / r.x, l.y / r.y, l.z / r.z, l.w / r.w); }
        public static ivec4 operator +(ivec4 l, int r) { return new ivec4(l.x + r, l.y + r, l.z + r, l.w + r); }
        public static ivec4 operator -(ivec4 l, int r) { return new ivec4(l.x - r, l.y - r, l.z - r, l.w - r); }
        public static ivec4 operator *(ivec4 l, int r) { return new ivec4(l.x * r, l.y * r, l.z * r, l.w * r); }
        public static ivec4 operator /(ivec4 l, int r) { return new ivec4(l.x / r, l.y / r, l.z / r, l.w / r); }
        public static ivec4 operator +(int l, ivec4 r) { return new ivec4(l + r.x, l + r.y, l + r.z, l + r.w); }
        public static ivec4 operator -(int l, ivec4 r) { return new ivec4(l - r.x, l - r.y, l - r.z, l - r.w); }
        public static ivec4 operator *(int l, ivec4 r) { return new ivec4(l * r.x, l * r.y, l * r.z, l * r.w); }
        public static ivec4 operator /(int l, ivec4 r) { return new ivec4(l / r.x, l / r.y, l / r.z, l / r.w); }
    }

    class uvec2 : v2<uint>
    {
        public uvec2() { }
        public uvec2(uint x) : base(x) { }
        public uvec2(uint x, uint y) : base(x, y) { }

        public static uvec2 operator +(uvec2 l, uvec2 r) { return new uvec2(l.x + r.x, l.y + r.y); }
        public static uvec2 operator -(uvec2 l, uvec2 r) { return new uvec2(l.x - r.x, l.y - r.y); }
        public static uvec2 operator *(uvec2 l, uvec2 r) { return new uvec2(l.x * r.x, l.y * r.y); }
        public static uvec2 operator /(uvec2 l, uvec2 r) { return new uvec2(l.x / r.x, l.y / r.y); }
        public static uvec2 operator +(uvec2 l, uint r) { return new uvec2(l.x + r, l.y + r); }
        public static uvec2 operator -(uvec2 l, uint r) { return new uvec2(l.x - r, l.y - r); }
        public static uvec2 operator *(uvec2 l, uint r) { return new uvec2(l.x * r, l.y * r); }
        public static uvec2 operator /(uvec2 l, uint r) { return new uvec2(l.x / r, l.y / r); }
        public static uvec2 operator +(uint l, uvec2 r) { return new uvec2(l+ r.x, l + r.y); }
        public static uvec2 operator -(uint l, uvec2 r) { return new uvec2(l - r.x, l - r.y); }
        public static uvec2 operator *(uint l, uvec2 r) { return new uvec2(l * r.x, l * r.y); }
        public static uvec2 operator /(uint l, uvec2 r) { return new uvec2(l / r.x, l / r.y); }
    }

    class uvec3 : v3<uint>
    {
        public uvec3() { }
        public uvec3(uint x) : base(x) { }
        public uvec3(uint x, uvec2 yz) : base(x, yz) { }
        public uvec3(uvec2 xy, uint z) : base(xy, z) { }
        public uvec3(uint x, uint y, uint z) : base(x, y, z) { }

        public static uvec3 operator +(uvec3 l, uvec3 r) { return new uvec3(l.x + r.x, l.y + r.y, l.z + r.z); }
        public static uvec3 operator -(uvec3 l, uvec3 r) { return new uvec3(l.x - r.x, l.y - r.y, l.z - r.z); }
        public static uvec3 operator *(uvec3 l, uvec3 r) { return new uvec3(l.x * r.x, l.y * r.y, l.z * r.z); }
        public static uvec3 operator /(uvec3 l, uvec3 r) { return new uvec3(l.x / r.x, l.y / r.y, l.z / r.z); }
        public static uvec3 operator +(uvec3 l, uint r) { return new uvec3(l.x + r, l.y + r, l.z + r); }
        public static uvec3 operator -(uvec3 l, uint r) { return new uvec3(l.x - r, l.y - r, l.z - r); }
        public static uvec3 operator *(uvec3 l, uint r) { return new uvec3(l.x * r, l.y * r, l.z * r); }
        public static uvec3 operator /(uvec3 l, uint r) { return new uvec3(l.x / r, l.y / r, l.z / r); }
        public static uvec3 operator +(uint l, uvec3 r) { return new uvec3(l + r.x, l + r.y, l + r.z); }
        public static uvec3 operator -(uint l, uvec3 r) { return new uvec3(l - r.x, l - r.y, l - r.z); }
        public static uvec3 operator *(uint l, uvec3 r) { return new uvec3(l * r.x, l * r.y, l * r.z); }
        public static uvec3 operator /(uint l, uvec3 r) { return new uvec3(l / r.x, l / r.y, l / r.z); }
    }

    class uvec4 : v4<uint>
    {
        public uvec4() { }
        public uvec4(uint x) : base(x) { }
        public uvec4(uvec2 xy, uvec2 zw) : base(xy, zw) { }
        public uvec4(uint x, uvec3 yzw) : base(x, yzw) { }
        public uvec4(uvec3 xyz, uint  w) : base(xyz, w) { }
        public uvec4(uint x, uint y, uint z, uint w) : base(x, y, z, w) { }

        public static uvec4 operator +(uvec4 l, uvec4 r) { return new uvec4(l.x + r.x, l.y + r.y, l.z + r.z, l.w + r.w); }
        public static uvec4 operator -(uvec4 l, uvec4 r) { return new uvec4(l.x - r.x, l.y - r.y, l.z - r.z, l.w - r.w); }
        public static uvec4 operator *(uvec4 l, uvec4 r) { return new uvec4(l.x * r.x, l.y * r.y, l.z * r.z, l.w * r.w); }
        public static uvec4 operator /(uvec4 l, uvec4 r) { return new uvec4(l.x / r.x, l.y / r.y, l.z / r.z, l.w / r.w); }
        public static uvec4 operator +(uvec4 l, uint r) { return new uvec4(l.x + r, l.y + r, l.z + r, l.w + r); }
        public static uvec4 operator -(uvec4 l, uint r) { return new uvec4(l.x - r, l.y - r, l.z - r, l.w - r); }
        public static uvec4 operator *(uvec4 l, uint r) { return new uvec4(l.x * r, l.y * r, l.z * r, l.w * r); }
        public static uvec4 operator /(uvec4 l, uint r) { return new uvec4(l.x / r, l.y / r, l.z / r, l.w / r); }
        public static uvec4 operator +(uint l, uvec4 r) { return new uvec4(l + r.x, l + r.y, l + r.z, l + r.w); }
        public static uvec4 operator -(uint l, uvec4 r) { return new uvec4(l - r.x, l - r.y, l - r.z, l - r.w); }
        public static uvec4 operator *(uint l, uvec4 r) { return new uvec4(l * r.x, l * r.y, l * r.z, l * r.w); }
        public static uvec4 operator /(uint l, uvec4 r) { return new uvec4(l / r.x, l / r.y, l / r.z, l / r.w); }
    }

    class bvec2 : v2<bool>
    {
        public bvec2() { }
        public bvec2(bool x) : base(x) { }
        public bvec2(bool x, bool y) : base(x, y) { }
    }

    class bvec3 : v3<bool>
    {
        public bvec3() { }
        public bvec3(bool x) : base(x) { }
        public bvec3(bool x, bvec2 yz) : base(x, yz) { }
        public bvec3(bvec2 xy, bool z) : base(xy, z) { }
        public bvec3(bool x, bool y, bool z) : base(x, y, z) { }
    }

    class bvec4 : v4<bool>
    {
        public bvec4() { }
        public bvec4(bool x) : base(x) { }
        public bvec4(bvec2 xy, bvec2 zw) : base(xy, zw) { }
        public bvec4(bool x, bvec3 yzw) : base(x, yzw) { }
        public bvec4(bvec3 xyz, bool w) : base(xyz, w) { }
        public bvec4(bool x, bool y, bool z, bool w) : base(x, y, z, w) { }
    }
}
