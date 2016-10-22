namespace App.Glsl
{
    struct sampler1D { public int i; public static implicit operator int(sampler1D a) => a.i; public static implicit operator sampler1D(int a) => new sampler1D { i = a }; }
    struct isampler1D { public int i; public static implicit operator int(isampler1D a) => a.i; public static implicit operator isampler1D(int a) => new isampler1D { i = a }; }
    struct usampler1D { public int i; public static implicit operator int(usampler1D a) => a.i; public static implicit operator usampler1D(int a) => new usampler1D { i = a }; }
    struct sampler2D { public int i; public static implicit operator int(sampler2D a) => a.i; public static implicit operator sampler2D(int a) => new sampler2D { i = a }; }
    struct isampler2D { public int i; public static implicit operator int(isampler2D a) => a.i; public static implicit operator isampler2D(int a) => new isampler2D { i = a }; }
    struct usampler2D { public int i; public static implicit operator int(usampler2D a) => a.i; public static implicit operator usampler2D(int a) => new usampler2D { i = a }; }
    struct sampler3D { public int i; public static implicit operator int(sampler3D a) => a.i; public static implicit operator sampler3D(int a) => new sampler3D { i = a }; }
    struct isampler3D { public int i; public static implicit operator int(isampler3D a) => a.i; public static implicit operator isampler3D(int a) => new isampler3D { i = a }; }
    struct usampler3D { public int i; public static implicit operator int(usampler3D a) => a.i; public static implicit operator usampler3D(int a) => new usampler3D { i = a }; }
    struct samplerCube { public int i; public static implicit operator int(samplerCube a) => a.i; public static implicit operator samplerCube(int a) => new samplerCube { i = a }; }
    struct isamplerCube { public int i; public static implicit operator int(isamplerCube a) => a.i; public static implicit operator isamplerCube(int a) => new isamplerCube { i = a }; }
    struct usamplerCube { public int i; public static implicit operator int(usamplerCube a) => a.i; public static implicit operator usamplerCube(int a) => new usamplerCube { i = a }; }
    struct sampler1DShadow { public int i; public static implicit operator int(sampler1DShadow a) => a.i; public static implicit operator sampler1DShadow(int a) => new sampler1DShadow { i = a }; }
    struct sampler2DShadow { public int i; public static implicit operator int(sampler2DShadow a) => a.i; public static implicit operator sampler2DShadow(int a) => new sampler2DShadow { i = a }; }
    struct samplerCubeShadow { public int i; public static implicit operator int(samplerCubeShadow a) => a.i; public static implicit operator samplerCubeShadow(int a) => new samplerCubeShadow { i = a }; }
    struct sampler1DArray { public int i; public static implicit operator int(sampler1DArray a) => a.i; public static implicit operator sampler1DArray(int a) => new sampler1DArray { i = a }; }
    struct isampler1DArray { public int i; public static implicit operator int(isampler1DArray a) => a.i; public static implicit operator isampler1DArray(int a) => new isampler1DArray { i = a }; }
    struct usampler1DArray { public int i; public static implicit operator int(usampler1DArray a) => a.i; public static implicit operator usampler1DArray(int a) => new usampler1DArray { i = a }; }
    struct sampler2DArray { public int i; public static implicit operator int(sampler2DArray a) => a.i; public static implicit operator sampler2DArray(int a) => new sampler2DArray { i = a }; }
    struct usampler2DArray { public int i; public static implicit operator int(usampler2DArray a) => a.i; public static implicit operator usampler2DArray(int a) => new usampler2DArray { i = a }; }
    struct isampler2DArray { public int i; public static implicit operator int(isampler2DArray a) => a.i; public static implicit operator isampler2DArray(int a) => new isampler2DArray { i = a }; }
    struct samplerCubeArray { public int i; public static implicit operator int(samplerCubeArray a) => a.i; public static implicit operator samplerCubeArray(int a) => new samplerCubeArray { i = a }; }
    struct usamplerCubeArray { public int i; public static implicit operator int(usamplerCubeArray a) => a.i; public static implicit operator usamplerCubeArray(int a) => new usamplerCubeArray { i = a }; }
    struct isamplerCubeArray { public int i; public static implicit operator int(isamplerCubeArray a) => a.i; public static implicit operator isamplerCubeArray(int a) => new isamplerCubeArray { i = a }; }
    struct sampler1DArrayShadow { public int i; public static implicit operator int(sampler1DArrayShadow a) => a.i; public static implicit operator sampler1DArrayShadow(int a) => new sampler1DArrayShadow { i = a }; }
    struct sampler2DArrayShadow { public int i; public static implicit operator int(sampler2DArrayShadow a) => a.i; public static implicit operator sampler2DArrayShadow(int a) => new sampler2DArrayShadow { i = a }; }
    struct usampler2DArrayShadow { public int i; public static implicit operator int(usampler2DArrayShadow a) => a.i; public static implicit operator usampler2DArrayShadow(int a) => new usampler2DArrayShadow { i = a }; }
    struct isampler2DArrayShadow { public int i; public static implicit operator int(isampler2DArrayShadow a) => a.i; public static implicit operator isampler2DArrayShadow(int a) => new isampler2DArrayShadow { i = a }; }
    struct sampler2DRect { public int i; public static implicit operator int(sampler2DRect a) => a.i; public static implicit operator sampler2DRect(int a) => new sampler2DRect { i = a }; }
    struct isampler2DRect { public int i; public static implicit operator int(isampler2DRect a) => a.i; public static implicit operator isampler2DRect(int a) => new isampler2DRect { i = a }; }
    struct usampler2DRect { public int i; public static implicit operator int(usampler2DRect a) => a.i; public static implicit operator usampler2DRect(int a) => new usampler2DRect { i = a }; }
    struct sampler2DRectShadow { public int i; public static implicit operator int(sampler2DRectShadow a) => a.i; public static implicit operator sampler2DRectShadow(int a) => new sampler2DRectShadow { i = a }; }
    struct samplerCubeArrayShadow { public int i; public static implicit operator int(samplerCubeArrayShadow a) => a.i; public static implicit operator samplerCubeArrayShadow(int a) => new samplerCubeArrayShadow { i = a }; }
    struct isamplerCubeArrayShadow { public int i; public static implicit operator int(isamplerCubeArrayShadow a) => a.i; public static implicit operator isamplerCubeArrayShadow(int a) => new isamplerCubeArrayShadow { i = a }; }
    struct usamplerCubeArrayShadow { public int i; public static implicit operator int(usamplerCubeArrayShadow a) => a.i; public static implicit operator usamplerCubeArrayShadow(int a) => new usamplerCubeArrayShadow { i = a }; }
}
