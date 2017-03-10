using System;
using System.ComponentModel;
using System.Globalization;

#pragma warning disable IDE1006

namespace App.Glsl.SamplerTypes
{
    [TypeConverter(typeof(Converter<sampler1D>))]
    public struct sampler1D
    {
        public int i;
        public sampler1D(int I) { i = I; }
        public static implicit operator int(sampler1D a) => a.i;
        public static implicit operator sampler1D(int a) => new sampler1D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler1D>))]
    public struct isampler1D {
        public int i; public isampler1D(int I) { i = I; }
        public static implicit operator int(isampler1D a) => a.i;
        public static implicit operator isampler1D(int a) => new isampler1D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler1D>))]
    public struct usampler1D {
        public int i;
        public usampler1D(int I) { i = I; }
        public static implicit operator int(usampler1D a) => a.i;
        public static implicit operator usampler1D(int a) => new usampler1D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler2D>))]
    public struct sampler2D {
        public int i;
        public sampler2D(int I) { i = I; }
        public static implicit operator int(sampler2D a) => a.i;
        public static implicit operator sampler2D(int a) => new sampler2D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler2D>))]
    public struct isampler2D
    {
        public int i;
        public isampler2D(int I) { i = I; }
        public static implicit operator int(isampler2D a) => a.i;
        public static implicit operator isampler2D(int a) => new isampler2D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler2D>))]
    public struct usampler2D
    {
        public int i;
        public usampler2D(int I) { i = I; }
        public static implicit operator int(usampler2D a) => a.i;
        public static implicit operator usampler2D(int a) => new usampler2D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler3D>))]
    public struct sampler3D
    {
        public int i;
        public sampler3D(int I) { i = I; }
        public static implicit operator int(sampler3D a) => a.i;
        public static implicit operator sampler3D(int a) => new sampler3D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler3D>))]
    public struct isampler3D
    {
        public int i;
        public isampler3D(int I) { i = I; }
        public static implicit operator int(isampler3D a) => a.i;
        public static implicit operator isampler3D(int a) => new isampler3D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler3D>))]
    public struct usampler3D
    {
        public int i;
        public usampler3D(int I) { i = I; }
        public static implicit operator int(usampler3D a) => a.i;
        public static implicit operator usampler3D(int a) => new usampler3D { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<samplerBuffer>))]
    public struct samplerBuffer
    {
        public int i;
        public samplerBuffer(int I) { i = I; }
        public static implicit operator int(samplerBuffer a) => a.i;
        public static implicit operator samplerBuffer(int a) => new samplerBuffer { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isamplerBuffer>))]
    public struct isamplerBuffer
    {
        public int i;
        public isamplerBuffer(int I) { i = I; }
        public static implicit operator int(isamplerBuffer a) => a.i;
        public static implicit operator isamplerBuffer(int a) => new isamplerBuffer { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usamplerBuffer>))]
    public struct usamplerBuffer
    {
        public int i;
        public usamplerBuffer(int I) { i = I; }
        public static implicit operator int(usamplerBuffer a) => a.i;
        public static implicit operator usamplerBuffer(int a) => new usamplerBuffer { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<samplerCube>))]
    public struct samplerCube
    {
        public int i;
        public samplerCube(int I) { i = I; }
        public static implicit operator int(samplerCube a) => a.i;
        public static implicit operator samplerCube(int a) => new samplerCube { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isamplerCube>))]
    public struct isamplerCube
    {
        public int i;
        public isamplerCube(int I) { i = I; }
        public static implicit operator int(isamplerCube a) => a.i;
        public static implicit operator isamplerCube(int a) => new isamplerCube { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usamplerCube>))]
    public struct usamplerCube
    {
        public int i;
        public usamplerCube(int I) { i = I; }
        public static implicit operator int(usamplerCube a) => a.i;
        public static implicit operator usamplerCube(int a) => new usamplerCube { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler1DShadow>))]
    public struct sampler1DShadow
    {
        public int i;
        public sampler1DShadow(int I) { i = I; }
        public static implicit operator int(sampler1DShadow a) => a.i;
        public static implicit operator sampler1DShadow(int a) => new sampler1DShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler2DShadow>))]
    public struct sampler2DShadow
    {
        public int i;
        public sampler2DShadow(int I) { i = I; }
        public static implicit operator int(sampler2DShadow a) => a.i;
        public static implicit operator sampler2DShadow(int a) => new sampler2DShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<samplerCubeShadow>))]
    public struct samplerCubeShadow
    {
        public int i;
        public samplerCubeShadow(int I) { i = I; }
        public static implicit operator int(samplerCubeShadow a) => a.i;
        public static implicit operator samplerCubeShadow(int a) => new samplerCubeShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler1DArray>))]
    public struct sampler1DArray
    {
        public int i;
        public sampler1DArray(int I) { i = I; }
        public static implicit operator int(sampler1DArray a) => a.i;
        public static implicit operator sampler1DArray(int a) => new sampler1DArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler1DArray>))]
    public struct isampler1DArray
    {
        public int i;
        public isampler1DArray(int I) { i = I; }
        public static implicit operator int(isampler1DArray a) => a.i;
        public static implicit operator isampler1DArray(int a) => new isampler1DArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler1DArray>))]
    public struct usampler1DArray
    {
        public int i;
        public usampler1DArray(int I) { i = I; }
        public static implicit operator int(usampler1DArray a) => a.i;
        public static implicit operator usampler1DArray(int a) => new usampler1DArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler2DArray>))]
    public struct sampler2DArray
    {
        public int i;
        public sampler2DArray(int I) { i = I; }
        public static implicit operator int(sampler2DArray a) => a.i;
        public static implicit operator sampler2DArray(int a) => new sampler2DArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler2DArray>))]
    public struct usampler2DArray
    {
        public int i;
        public usampler2DArray(int I) { i = I; }
        public static implicit operator int(usampler2DArray a) => a.i;
        public static implicit operator usampler2DArray(int a) => new usampler2DArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler2DArray>))]
    public struct isampler2DArray
    {
        public int i;
        public isampler2DArray(int I) { i = I; }
        public static implicit operator int(isampler2DArray a) => a.i;
        public static implicit operator isampler2DArray(int a) => new isampler2DArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<samplerCubeArray>))]
    public struct samplerCubeArray
    {
        public int i;
        public samplerCubeArray(int I) { i = I; }
        public static implicit operator int(samplerCubeArray a) => a.i;
        public static implicit operator samplerCubeArray(int a) => new samplerCubeArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usamplerCubeArray>))]
    public struct usamplerCubeArray
    {
        public int i;
        public usamplerCubeArray(int I) { i = I; }
        public static implicit operator int(usamplerCubeArray a) => a.i;
        public static implicit operator usamplerCubeArray(int a) => new usamplerCubeArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isamplerCubeArray>))]
    public struct isamplerCubeArray
    {
        public int i;
        public isamplerCubeArray(int I) { i = I; }
        public static implicit operator int(isamplerCubeArray a) => a.i;
        public static implicit operator isamplerCubeArray(int a) => new isamplerCubeArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler1DArrayShadow>))]
    public struct sampler1DArrayShadow
    {
        public int i;
        public sampler1DArrayShadow(int I) { i = I; }
        public static implicit operator int(sampler1DArrayShadow a) => a.i;
        public static implicit operator sampler1DArrayShadow(int a) => new sampler1DArrayShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler2DArrayShadow>))]
    public struct sampler2DArrayShadow
    {
        public int i;
        public sampler2DArrayShadow(int I) { i = I; }
        public static implicit operator int(sampler2DArrayShadow a) => a.i;
        public static implicit operator sampler2DArrayShadow(int a) => new sampler2DArrayShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler2DArrayShadow>))]
    public struct usampler2DArrayShadow
    {
        public int i;
        public usampler2DArrayShadow(int I) { i = I; }
        public static implicit operator int(usampler2DArrayShadow a) => a.i;
        public static implicit operator usampler2DArrayShadow(int a) => new usampler2DArrayShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler2DArrayShadow>))]
    public struct isampler2DArrayShadow
    {
        public int i;
        public isampler2DArrayShadow(int I) { i = I; }
        public static implicit operator int(isampler2DArrayShadow a) => a.i;
        public static implicit operator isampler2DArrayShadow(int a) => new isampler2DArrayShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler2DRect>))]
    public struct sampler2DRect
    {
        public int i;
        public sampler2DRect(int I) { i = I; }
        public static implicit operator int(sampler2DRect a) => a.i;
        public static implicit operator sampler2DRect(int a) => new sampler2DRect { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler2DRect>))]
    public struct isampler2DRect
    {
        public int i;
        public isampler2DRect(int I) { i = I; }
        public static implicit operator int(isampler2DRect a) => a.i;
        public static implicit operator isampler2DRect(int a) => new isampler2DRect { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler2DRect>))]
    public struct usampler2DRect
    {
        public int i;
        public usampler2DRect(int I) { i = I; }
        public static implicit operator int(usampler2DRect a) => a.i;
        public static implicit operator usampler2DRect(int a) => new usampler2DRect { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler2DRectShadow>))]
    public struct sampler2DRectShadow
    {
        public int i;
        public sampler2DRectShadow(int I) { i = I; }
        public static implicit operator int(sampler2DRectShadow a) => a.i;
        public static implicit operator sampler2DRectShadow(int a) => new sampler2DRectShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<samplerCubeArrayShadow>))]
    public struct samplerCubeArrayShadow
    {
        public int i;
        public samplerCubeArrayShadow(int I) { i = I; }
        public static implicit operator int(samplerCubeArrayShadow a) => a.i;
        public static implicit operator samplerCubeArrayShadow(int a) => new samplerCubeArrayShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isamplerCubeArrayShadow>))]
    public struct isamplerCubeArrayShadow
    {
        public int i;
        public isamplerCubeArrayShadow(int I) { i = I; }
        public static implicit operator int(isamplerCubeArrayShadow a) => a.i;
        public static implicit operator isamplerCubeArrayShadow(int a) => new isamplerCubeArrayShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usamplerCubeArrayShadow>))]
    public struct usamplerCubeArrayShadow
    {
        public int i;
        public usamplerCubeArrayShadow(int I) { i = I; }
        public static implicit operator int(usamplerCubeArrayShadow a) => a.i;
        public static implicit operator usamplerCubeArrayShadow(int a) => new usamplerCubeArrayShadow { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler2DMS>))]
    public struct sampler2DMS
    {
        public int i; public sampler2DMS(int I) { i = I; }
        public static implicit operator int(sampler2DMS a) => a.i;
        public static implicit operator sampler2DMS(int a) => new sampler2DMS { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler2DMS>))]
    public struct isampler2DMS
    {
        public int i;
        public isampler2DMS(int I) { i = I; }
        public static implicit operator int(isampler2DMS a) => a.i;
        public static implicit operator isampler2DMS(int a) => new isampler2DMS { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler2DMS>))]
    public struct usampler2DMS
    {
        public int i;
        public usampler2DMS(int I) { i = I; }
        public static implicit operator int(usampler2DMS a) => a.i;
        public static implicit operator usampler2DMS(int a) => new usampler2DMS { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<sampler2DMSArray>))]
    public struct sampler2DMSArray
    {
        public int i;
        public sampler2DMSArray(int I) { i = I; }
        public static implicit operator int(sampler2DMSArray a) => a.i;
        public static implicit operator sampler2DMSArray(int a) => new sampler2DMSArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<isampler2DMSArray>))]
    public struct isampler2DMSArray
    {
        public int i;
        public isampler2DMSArray(int I) { i = I; }
        public static implicit operator int(isampler2DMSArray a) => a.i;
        public static implicit operator isampler2DMSArray(int a) => new isampler2DMSArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }
    [TypeConverter(typeof(Converter<usampler2DMSArray>))]
    public struct usampler2DMSArray
    {
        public int i;
        public usampler2DMSArray(int I) { i = I; }
        public static implicit operator int(usampler2DMSArray a) => a.i;
        public static implicit operator usampler2DMSArray(int a) => new usampler2DMSArray { i = a };
        public override string ToString()
        {
            return $"{i}";
        }
    }

    public class Converter<T> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(int) ? true : base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value is int
                ? Activator.CreateInstance(typeof(T), new[] { value })
                : base.ConvertFrom(context, culture, value);
        }
    }
}
