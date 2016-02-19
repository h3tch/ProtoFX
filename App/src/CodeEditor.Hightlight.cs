using ScintillaNET;
using System;
using System.Drawing;
using System.Linq;

namespace App
{
    public partial class CodeEditor
    {
        private FXLexer lexer = new FXLexer(BlockKeywords, AnnoKeywords, CmdKeywords, ArgKeywords, GlslKeywords, GlslQualifier, GlslFunction);
        private int[] BlockStyles = new[] { FXLexer.Keyword, FXLexer.Annotation };
        private int[] CmdStyles = new[] { FXLexer.Command, FXLexer.Argument };
        private int[] GlslStyles = new[] { FXLexer.GlslKeyword, FXLexer.GlslQualifier, FXLexer.GlslFunction };

        private void InitializeHighlighting()
        {
            StyleResetDefault();
            Styles[Style.Default].Font = "Consolas";
            Styles[Style.Default].Size = 10;
            StyleClearAll();

            Styles[Style.LineNumber].ForeColor = Color.Gray;

            Styles[FXLexer.Default].ForeColor = Color.Silver;
            Styles[FXLexer.Comment].ForeColor = Color.FromArgb(0x7F9F00);
            Styles[FXLexer.Operator].ForeColor = Color.FromArgb(0x3050CC);
            Styles[FXLexer.Preprocessor].ForeColor = Color.FromArgb(0xE47426);
            Styles[FXLexer.Number].ForeColor = Color.FromArgb(0x108030);
            Styles[FXLexer.String].ForeColor = Color.Maroon;
            Styles[FXLexer.Char].ForeColor = Color.FromArgb(163, 21, 21);

            Styles[FXLexer.Keyword].ForeColor = Color.Blue;
            Styles[FXLexer.Annotation].ForeColor = Color.FromArgb(30, 120, 255);
            Styles[FXLexer.Command].ForeColor = Color.FromArgb(30, 120, 255);
            Styles[FXLexer.Argument].ForeColor = Color.Purple;
            Styles[FXLexer.GlslKeyword].ForeColor = Color.Blue;
            Styles[FXLexer.GlslQualifier].ForeColor = Color.FromArgb(30, 120, 255);
            Styles[FXLexer.GlslFunction].ForeColor = Color.Purple;

            Lexer = Lexer.Container;

            StyleNeeded += new EventHandler<StyleNeededEventArgs>(HandleStyleNeeded);
        }
        
        private void HandleStyleNeeded(object sender, StyleNeededEventArgs ev)
        {
            var start = Lines[LineFromPosition(GetEndStyled())].Position;
            var end = ev.Position;
            var blocks = BlockPositions().ToArray();

            foreach (var block in blocks)
            {
                var blockStart = block[1];
                var blockEnd = block[2];
                
                if (blockEnd < start)
                    continue;
                if (blockStart >= end)
                    break;

                var blockType = GetWordFromPosition(block[0]);
                var keywords = blockType == "shader" ? GlslStyles : CmdStyles;

                if (blockStart <= start)
                {
                    if (blockEnd < end)
                    {
                        lexer.Style(this, start, blockEnd, keywords);
                        start = blockEnd;
                    }
                    else
                    {
                        lexer.Style(this, start, end, keywords);
                        start = end;
                        break;
                    }
                }
                else
                {
                    lexer.Style(this, start, blockStart, BlockStyles);
                    start = blockStart;

                    if (blockEnd < end)
                    {
                        lexer.Style(this, start, blockEnd, keywords);
                        start = blockEnd;
                    }
                    else
                    {
                        lexer.Style(this, start, end, keywords);
                        start = end;
                        break;
                    }
                }
            }

            if (start < end)
                lexer.Style(this, start, end, BlockStyles);
        }

        #region KEYWORDS
        private static string[] BlockKeywords = new[]
        {
            "buffer",
            "csharp",
            "fragoutput",
            "geomoutput",
            "image",
            "instance",
            "pass",
            "sampler",
            "shader",
            "tech",
            "text",
            "texture",
            "vertinput",
            "vertoutput",
        };

        private static string[] AnnoKeywords = new[]
        {
            "comp",
            "eval",
            "frag",
            "geom",
            "tess",
            "vert",
        };

        private static string[] CmdKeywords = new[]
        {
            "comp",
            "eval",
            "frag",
            "geom",
            "tess",
            "vert",
            "attr",
            "binding",
            "buff",
            "class",
            "color",
            "comp",
            "depth",
            "draw",
            "exec",
            "file",
            "format",
            "fragout",
            "geomout",
            "gpuformat",
            "height",
            "img",
            "length",
            "magfilter",
            "minfilter",
            "mipmaps",
            "name",
            "pass",
            "samp",
            "size",
            "stencil",
            "tex",
            "txt",
            "type",
            "usage",
            "vertout",
            "width",
            "wrap",
            "xml",
        };

        private static string[] ArgKeywords = new[]
        {
            "dynamicCopy",
            "dynamicDraw",
            "dynamicRead",
            "staticCopy",
            "staticDraw",
            "staticRead",
            "streamCopy",
            "streamDraw",
            "streamRead",
            "depth16",
            "depth24",
            "depth24stencil8",
            "depth32",
            "depth32f",
            "depth32fstencil8",
            "depthstencil",
            "r8",
            "r8i",
            "r8ui",
            "r16",
            "r16i",
            "r16ui",
            "r16f",
            "r32i",
            "r32ui",
            "r32f",
            "rg8",
            "rg8i",
            "rg8ui",
            "rg16",
            "rg16i",
            "rg16ui",
            "rg16f",
            "rg32i",
            "rg32ui",
            "rg32f",
            "rgb8",
            "rgb8i",
            "rgb8ui",
            "rgb16",
            "rgb16i",
            "rgb16ui",
            "rgb16f",
            "rgb32i",
            "rgb32ui",
            "rgb32f",
            "rgba8",
            "rgba8i",
            "rgba8ui",
            "rgba16",
            "rgba16i",
            "rgba16ui",
            "rgba16f",
            "rgba32i",
            "rgba32ui",
            "rgba32f",
            "texture1D",
            "texture2D",
            "texture3D",
            "texture1DArray",
            "texture2DArray",
            "linear",
            "nearest",
            "linearMipmapLinear",
            "linearMipmapNearest",
            "nearestMipmapLinear",
            "nearestMipmapNearest",
            "clampToBorder",
            "clampToEdge",
            "mirroredRepeat",
            "repeat",
            "float",
            "double",
            "int",
            "uint",
            "short",
            "ushort",
            "char",
            "byte",
            "ubyte",
        };

        private static string[] GlslKeywords = new[]
        {
            "atomic_uint",
            "bvec2",
            "bvec3",
            "bvec4",
            "discard",
            "dmat2",
            "dmat2x2",
            "dmat2x3",
            "dmat2x4",
            "dmat3",
            "dmat3x2",
            "dmat3x3",
            "dmat3x4",
            "dmat4",
            "dmat4x2",
            "dmat4x3",
            "dmat4x4",
            "dvec2",
            "dvec3",
            "dvec4",
            "gl_ClipDistance",
            "gl_DepthRange",
            "gl_DepthRange.diff",
            "gl_DepthRange.far",
            "gl_DepthRange.near",
            "gl_FragCoord",
            "gl_FragDepth",
            "gl_FrontFacing",
            "gl_GlobalInvocationID",
            "gl_InstanceID",
            "gl_InvocationID",
            "gl_Layer",
            "gl_LocalInvocationID",
            "gl_LocalInvocationIndex",
            "gl_MaxPatchVertices",
            "gl_NumSamples",
            "gl_NumWorkGroups",
            "gl_PatchVerticesIn",
            "gl_PointCoord",
            "gl_PointSize",
            "gl_Position",
            "gl_PrimitiveID",
            "gl_PrimitiveIDIn",
            "gl_SampleID",
            "gl_SampleMask",
            "gl_SampleMaskIn",
            "gl_SamplePosition",
            "gl_TessLevelInner",
            "gl_TessLevelOuter",
            "gl_VertexID",
            "gl_ViewportIndex",
            "gl_WorkGroupID",
            "gl_WorkGroupSize",
            "gl_in",
            "gl_out",
            "in",
            "inout",
            "ivec2",
            "ivec3",
            "ivec4",
            "isampler1D",
            "isampler1DArray",
            "isampler2D",
            "isampler2DArray",
            "isampler2DMS",
            "isampler2DMSArray",
            "isampler2DRect",
            "isampler3D",
            "isamplerBuffer",
            "isamplerCube",
            "isamplerCubeArray",
            "layout",
            "mat2",
            "mat2x2",
            "mat2x3",
            "mat2x4",
            "mat3",
            "mat3x2",
            "mat3x3",
            "mat3x4",
            "mat4",
            "mat4x2",
            "mat4x3",
            "mat4x4",
            "out",
            "patch",
            "precision",
            "sampler1D",
            "sampler1DArray",
            "sampler1DArrayShadow",
            "sampler1DShadow",
            "sampler2D",
            "sampler2DArray",
            "sampler2DArrayShadow",
            "sampler2DMS",
            "sampler2DMSArray",
            "sampler2DRect",
            "sampler2DRectShadow",
            "sampler2DShadow",
            "sampler3D",
            "samplerBuffer",
            "samplerCube",
            "samplerCubeArray",
            "samplerCubeArrayShadow",
            "samplerCubeShadow",
            "subroutine",
            "uniform",
            "usampler1D",
            "usampler1DArray",
            "usampler2D",
            "usampler2DArray",
            "usampler2DMS",
            "usampler2DMSArray",
            "usampler2DRect",
            "usampler3D",
            "usamplerBuffer",
            "usamplerCube",
            "usamplerCubeArray",
            "uvec2",
            "uvec3",
            "uvec4",
            "vec2",
            "vec3",
            "vec4",
            "void",
    };

        private static string[] GlslQualifier = new[]
        {
            "centroid",
            "flat",
            "invariant",
            "binding",
            "component",
            "depth_any",
            "depth_greater",
            "depth_less",
            "depth_unchanged",
            "early_fragment_tests",
            "index",
            "location",
            "offset",
            "origin_upper_left",
            "pixel_center_integer",
            "std140",
            "vertices",
            "xfb_buffer",
            "xfb_offset",
            "xfb_stride",
            "noperspective",
            "highp",
            "lowp",
            "mediump",
            "smooth",
        };

        private static string[] GlslFunction = new[]
        {
            "abs",
            "acos",
            "acosh",
            "all",
            "any",
            "asin",
            "asinh",
            "atan",
            "atanh",
            "atomicAdd",
            "atomicAnd",
            "atomicCompSwap",
            "atomicCounter",
            "atomicCounterDecrement",
            "atomicCounterIncrement",
            "atomicExchange",
            "atomicMin",
            "atomicMax",
            "atomicOr",
            "atomicXor",
            "barrier",
            "bitCount",
            "bitfieldExtract",
            "bitfieldInsert",
            "bitfieldReverse",
            "ceil",
            "clamp",
            "cos",
            "cosh",
            "cross",
            "degrees",
            "determinant",
            "dFdx",
            "dFdxCoarse",
            "dFdxFine",
            "dFdy",
            "dFdyCoarse",
            "dFdyFine",
            "distance",
            "dot",
            "EmitStreamVertex",
            "EmitVertex",
            "EndPrimitive",
            "EndStreamPrimitive",
            "equal",
            "exp",
            "exp2",
            "faceforward",
            "findLSB",
            "findMSB",
            "floor",
            "fma",
            "fract",
            "frexp",
            "fwidth",
            "fwidthCoarse",
            "fwidthFine",
            "imageAtomicAdd",
            "imageAtomicAnd",
            "imageAtomicCompSwap",
            "imageAtomicExchange",
            "imageAtomicMax",
            "imageAtomicMin",
            "imageAtomicOr",
            "imageAtomicXor",
            "imageLoad",
            "imageSamples",
            "imageSize",
            "imageStore",
            "imulExtended",
            "intBitsToFloat",
            "interpolateAtCentroid",
            "interpolateAtOffset",
            "interpolateAtSample",
            "inverse",
            "inversesqrt",
            "isinf",
            "isnan",
            "ldexp",
            "length",
            "lessThan",
            "lessThanEqual",
            "log",
            "log2",
            "matrixCompMult",
            "max",
            "memoryBarrier",
            "memoryBarrierAtomicCounter",
            "memoryBarrierBuffer",
            "memoryBarrierImage",
            "memoryBarrierShared",
            "min",
            "mix",
            "mod",
            "modf",
            "noise",
            "noise1",
            "noise2",
            "noise3",
            "noise4",
            "normalize",
            "not",
            "notEqual",
            "outerProduct",
            "packDouble2x32",
            "packHalf2x16",
            "packSnorm2x16",
            "packSnorm4x8",
            "packUnorm",
            "packUnorm2x16",
            "packUnorm4x8",
            "pow",
            "radians",
            "reflect",
            "refract",
            "removedTypes",
            "round",
            "roundEven",
            "sign",
            "sin",
            "sinh",
            "smoothstep",
            "sqrt",
            "step",
            "tan",
            "tanh",
            "texelFetch",
            "texelFetchOffset",
            "texture",
            "textureGather",
            "textureGatherOffset",
            "textureGatherOffsets",
            "textureGrad",
            "textureLod",
            "textureOffset",
            "textureProj",
            "textureProjGrad​",
            "textureProjGradOffset​",
            "textureProjLod​",
            "textureProjLodOffset​",
            "textureProjOffset​",
            "textureQueryLevels",
            "textureQueryLod",
            "textureSamples",
            "textureSize",
            "transpose",
            "trunc",
            "uaddCarry",
            "uintBitsToFloat",
            "umulExtended",
            "unpackDouble2x32",
            "unpackHalf2x16",
            "unpackSnorm2x16",
            "unpackSnorm4x8",
            "unpackUnorm",
            "unpackUnorm2x16",
            "unpackUnorm4x8",
            "usubBorrow",
        };
        #endregion
    }
}
