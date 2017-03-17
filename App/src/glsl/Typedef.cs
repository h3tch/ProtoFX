using System;
using System.ComponentModel;
using System.Globalization;

#pragma warning disable IDE1006
#pragma warning disable IDE0022

namespace App.Glsl.SamplerTypes
{
    [TypeConverter(typeof(Converter<sampler1D>))]
    public struct sampler1D
    {
        private int i;
        public sampler1D(int i) { this.i = i; }
        public static implicit operator int(sampler1D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler1D(int a) => new sampler1D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler1D>))]
    public struct isampler1D {
        private int i;
        public isampler1D(int i) { this.i = i; }
        public static implicit operator int(isampler1D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler1D(int a) => new isampler1D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler1D>))]
    public struct usampler1D {
        private int i;
        public usampler1D(int i) { this.i = i; }
        public static implicit operator int(usampler1D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler1D(int a) => new usampler1D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler2D>))]
    public struct sampler2D {
        private int i;
        public sampler2D(int i) { this.i = i; }
        public static implicit operator int(sampler2D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler2D(int a) => new sampler2D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler2D>))]
    public struct isampler2D
    {
        private int i;
        public isampler2D(int i) { this.i = i; }
        public static implicit operator int(isampler2D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler2D(int a) => new isampler2D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler2D>))]
    public struct usampler2D
    {
        private int i;
        public usampler2D(int i) { this.i = i; }
        public static implicit operator int(usampler2D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler2D(int a) => new usampler2D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler3D>))]
    public struct sampler3D
    {
        private int i;
        public sampler3D(int i) { this.i = i; }
        public static implicit operator int(sampler3D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler3D(int a) => new sampler3D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler3D>))]
    public struct isampler3D
    {
        private int i;
        public isampler3D(int i) { this.i = i; }
        public static implicit operator int(isampler3D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler3D(int a) => new isampler3D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler3D>))]
    public struct usampler3D
    {
        private int i;
        public usampler3D(int i) { this.i = i; }
        public static implicit operator int(usampler3D a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler3D(int a) => new usampler3D { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<samplerBuffer>))]
    public struct samplerBuffer
    {
        private int i;
        public samplerBuffer(int i) { this.i = i; }
        public static implicit operator int(samplerBuffer a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator samplerBuffer(int a) => new samplerBuffer { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isamplerBuffer>))]
    public struct isamplerBuffer
    {
        private int i;
        public static implicit operator int(isamplerBuffer a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isamplerBuffer(int a) => new isamplerBuffer { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usamplerBuffer>))]
    public struct usamplerBuffer
    {
        private int i;
        public usamplerBuffer(int i) { this.i = i; }
        public static implicit operator int(usamplerBuffer a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usamplerBuffer(int a) => new usamplerBuffer { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<samplerCube>))]
    public struct samplerCube
    {
        private int i;
        public samplerCube(int i) { this.i = i; }
        public static implicit operator int(samplerCube a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator samplerCube(int a) => new samplerCube { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isamplerCube>))]
    public struct isamplerCube
    {
        private int i;
        public isamplerCube(int i) { this.i = i; }
        public static implicit operator int(isamplerCube a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isamplerCube(int a) => new isamplerCube { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usamplerCube>))]
    public struct usamplerCube
    {
        private int i;
        public usamplerCube(int i) { this.i = i; }
        public static implicit operator int(usamplerCube a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usamplerCube(int a) => new usamplerCube { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler1DShadow>))]
    public struct sampler1DShadow
    {
        private int i;
        public sampler1DShadow(int i) { this.i = i; }
        public static implicit operator int(sampler1DShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler1DShadow(int a) => new sampler1DShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler2DShadow>))]
    public struct sampler2DShadow
    {
        private int i;
        public sampler2DShadow(int i) { this.i = i; }
        public static implicit operator int(sampler2DShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler2DShadow(int a) => new sampler2DShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<samplerCubeShadow>))]
    public struct samplerCubeShadow
    {
        private int i;
        public samplerCubeShadow(int i) { this.i = i; }
        public static implicit operator int(samplerCubeShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator samplerCubeShadow(int a) => new samplerCubeShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler1DArray>))]
    public struct sampler1DArray
    {
        private int i;
        public sampler1DArray(int i) { this.i = i; }
        public static implicit operator int(sampler1DArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler1DArray(int a) => new sampler1DArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler1DArray>))]
    public struct isampler1DArray
    {
        private int i;
        public isampler1DArray(int i) { this.i = i; }
        public static implicit operator int(isampler1DArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler1DArray(int a) => new isampler1DArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler1DArray>))]
    public struct usampler1DArray
    {
        private int i;
        public usampler1DArray(int i) { this.i = i; }
        public static implicit operator int(usampler1DArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler1DArray(int a) => new usampler1DArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler2DArray>))]
    public struct sampler2DArray
    {
        private int i;
        public sampler2DArray(int i) { this.i = i; }
        public static implicit operator int(sampler2DArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler2DArray(int a) => new sampler2DArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler2DArray>))]
    public struct usampler2DArray
    {
        private int i;
        public usampler2DArray(int i) { this.i = i; }
        public static implicit operator int(usampler2DArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler2DArray(int a) => new usampler2DArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler2DArray>))]
    public struct isampler2DArray
    {
        private int i;
        public isampler2DArray(int i) { this.i = i; }
        public static implicit operator int(isampler2DArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler2DArray(int a) => new isampler2DArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<samplerCubeArray>))]
    public struct samplerCubeArray
    {
        private int i;
        public samplerCubeArray(int i) { this.i = i; }
        public static implicit operator int(samplerCubeArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator samplerCubeArray(int a) => new samplerCubeArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usamplerCubeArray>))]
    public struct usamplerCubeArray
    {
        private int i;
        public usamplerCubeArray(int i) { this.i = i; }
        public static implicit operator int(usamplerCubeArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usamplerCubeArray(int a) => new usamplerCubeArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isamplerCubeArray>))]
    public struct isamplerCubeArray
    {
        private int i;
        public isamplerCubeArray(int i) { this.i = i; }
        public static implicit operator int(isamplerCubeArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isamplerCubeArray(int a) => new isamplerCubeArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler1DArrayShadow>))]
    public struct sampler1DArrayShadow
    {
        private int i;
        public sampler1DArrayShadow(int i) { this.i = i; }
        public static implicit operator int(sampler1DArrayShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler1DArrayShadow(int a) => new sampler1DArrayShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler2DArrayShadow>))]
    public struct sampler2DArrayShadow
    {
        private int i;
        public sampler2DArrayShadow(int i) { this.i = i; }
        public static implicit operator int(sampler2DArrayShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler2DArrayShadow(int a) => new sampler2DArrayShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler2DArrayShadow>))]
    public struct usampler2DArrayShadow
    {
        private int i;
        public usampler2DArrayShadow(int i) { this.i = i; }
        public static implicit operator int(usampler2DArrayShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler2DArrayShadow(int a) => new usampler2DArrayShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler2DArrayShadow>))]
    public struct isampler2DArrayShadow
    {
        private int i;
        public isampler2DArrayShadow(int i) { this.i = i; }
        public static implicit operator int(isampler2DArrayShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler2DArrayShadow(int a) => new isampler2DArrayShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler2DRect>))]
    public struct sampler2DRect
    {
        private int i;
        public sampler2DRect(int i) { this.i = i; }
        public static implicit operator int(sampler2DRect a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler2DRect(int a) => new sampler2DRect { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler2DRect>))]
    public struct isampler2DRect
    {
        private int i;
        public isampler2DRect(int i) { this.i = i; }
        public static implicit operator int(isampler2DRect a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler2DRect(int a) => new isampler2DRect { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler2DRect>))]
    public struct usampler2DRect
    {
        private int i;
        public usampler2DRect(int i) { this.i = i; }
        public static implicit operator int(usampler2DRect a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler2DRect(int a) => new usampler2DRect { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler2DRectShadow>))]
    public struct sampler2DRectShadow
    {
        private int i;
        public sampler2DRectShadow(int i) { this.i = i; }
        public static implicit operator int(sampler2DRectShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler2DRectShadow(int a) => new sampler2DRectShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<samplerCubeArrayShadow>))]
    public struct samplerCubeArrayShadow
    {
        private int i;
        public samplerCubeArrayShadow(int i) { this.i = i; }
        public static implicit operator int(samplerCubeArrayShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator samplerCubeArrayShadow(int a) => new samplerCubeArrayShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isamplerCubeArrayShadow>))]
    public struct isamplerCubeArrayShadow
    {
        private int i;
        public isamplerCubeArrayShadow(int i) { this.i = i; }
        public static implicit operator int(isamplerCubeArrayShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isamplerCubeArrayShadow(int a) => new isamplerCubeArrayShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usamplerCubeArrayShadow>))]
    public struct usamplerCubeArrayShadow
    {
        private int i;
        public usamplerCubeArrayShadow(int i) { this.i = i; }
        public static implicit operator int(usamplerCubeArrayShadow a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usamplerCubeArrayShadow(int a) => new usamplerCubeArrayShadow { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler2DMS>))]
    public struct sampler2DMS
    {
        private int i;
        public sampler2DMS(int i) { this.i = i; }
        public static implicit operator int(sampler2DMS a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler2DMS(int a) => new sampler2DMS { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler2DMS>))]
    public struct isampler2DMS
    {
        private int i;
        public isampler2DMS(int i) { this.i = i; }
        public static implicit operator int(isampler2DMS a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler2DMS(int a) => new isampler2DMS { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler2DMS>))]
    public struct usampler2DMS
    {
        private int i;
        public static implicit operator int(usampler2DMS a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler2DMS(int a) => new usampler2DMS { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<sampler2DMSArray>))]
    public struct sampler2DMSArray
    {
        private int i;
        public sampler2DMSArray(int i) { this.i = i; }
        public static implicit operator int(sampler2DMSArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator sampler2DMSArray(int a) => new sampler2DMSArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<isampler2DMSArray>))]
    public struct isampler2DMSArray
    {
        private int i;
        public isampler2DMSArray(int i) { this.i = i; }
        public static implicit operator int(isampler2DMSArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator isampler2DMSArray(int a) => new isampler2DMSArray { i = a };
        public override string ToString() => $"{i}";
    }
    [TypeConverter(typeof(Converter<usampler2DMSArray>))]
    public struct usampler2DMSArray
    {
        private int i;
        public usampler2DMSArray(int i) { this.i = i; }
        public static implicit operator int(usampler2DMSArray a) => Attr.GetBinding(a) ?? a.i;
        public static implicit operator usampler2DMSArray(int a) => new usampler2DMSArray { i = a };
        public override string ToString() => $"{i}";
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

    static class Attr
    {
        internal static int? GetBinding<T>(T obj)
        {
            return obj.GetType().GetAttribute<__layout>()?.binding;
        }
    }
}
