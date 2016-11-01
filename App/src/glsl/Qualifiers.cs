using System;

namespace App.Glsl
{
    public class points { }
    public class lines { }
    public class lines_adjacency​ { }
    public class triangles​ { }
    public class triangles_adjacency​ { }
    public class quads { }
    public class isolines { }
    public class line_strip { }
    public class triangle_strip { }
    
    public enum __TessSpacing
    {
        equal_spacing​,
        fractional_even_spacing​,
        fractional_odd_spacing​
    }

    public enum __StdLayout
    {
        std140,
        std430,
        shared,
        packed,
    }

    public enum __MatrixLayout
    {
        row_major,
        column_major,
    }

    public enum __FragOrigin
    {
        origin_default,
        origin_upper_left,
    }

    public enum __FragPixelCenter
    {
        pixel_center_default,
        pixel_center_integer,
    }

    public enum __FragTest
    {
        default_test,
        early_fragment_tests
    }

    public enum __ImageFormat
    {
        undefined,
        rgba32f,
        rgba16f,
        rg32f,
        rg16f,
        r11f_g11f_b10f,
        r32f,
        r16f,
        rgba16,
        rgb10_a2,
        rgba8,
        rg16,
        rg8,
        r16,
        r8,
        rgba16_snorm,
        rgba8_snorm,
        rg16_snorm,
        rg8_snorm,
        r16_snorm,
        r8_snorm,
        rgba32i,
        rgba16i,
        rgba8i,
        rg32i,
        rg16i,
        rg8i,
        r32i,
        r16i,
        r8i,
        rgba32ui,
        rgba16ui,
        rgb10_a2ui,
        rgba8ui,
        rg32ui,
        rg16ui,
        rg8ui,
        r32ui,
        r16ui,
        r8ui,
    }

    [AttributeUsage(AttributeTargets.All)]
    public class __in : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __float : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __smooth : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __out : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __layout : Attribute
    {
#pragma warning disable 0649
#pragma warning disable 0169

        public object[] @params;
        public int location = -1;
        public int index = -1;
        public int component = -1;
        public int binding = -1;
        public int offset = -1;
        public int vertices = -1;
        public int invocations = 1;
        public int max_vertices = 1;
        public int xfb_offset = -1;
        public int xfb_stride = -1;
        public int xfb_buffer = -1;

#pragma warning restore 0649
#pragma warning restore 0169

        public __layout() { }
        public __layout(params object[] @params)
        {
            this.@params = @params;
        }
    }
}
