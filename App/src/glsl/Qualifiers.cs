using System;

namespace App.Glsl
{
    public enum __InputPrimitive
    {
        invalid,
        points,
        lines,
        lines_adjacency​,
        triangles​,
        triangles_adjacency​,
        quads,
        isolines,
    }

    public enum __OutputPrimitive
    {
        invalid,
        points,
        line_strip,
        triangle_strip
    }

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

        public __StdLayout std_layout = __StdLayout.shared;
        public __InputPrimitive input_primitive = __InputPrimitive.invalid;
        public __OutputPrimitive output_primitive = __OutputPrimitive.invalid;
        public __TessSpacing tess_spacing = __TessSpacing.equal_spacing;
        public __FragOrigin pixel_origin = __FragOrigin.origin_default;
        public __FragPixelCenter pixel_location = __FragPixelCenter.pixel_center_default;
        public __FragTest frag_test = __FragTest.default_test;
        public __ImageFormat image_format = __ImageFormat.undefined;
        public __MatrixLayout matrix_layout = __MatrixLayout.column_major;
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
            var fields = GetType().GetFields();
            foreach (var param in @params)
            {
                foreach (var field in fields)
                {
                    if (field.GetType() == param.GetType())
                    {
                        field.SetValue(this, param);
                        break;
                    }
                }
            }
        }
    }
}
