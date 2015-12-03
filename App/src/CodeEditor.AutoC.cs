using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App
{
    partial class CodeEditor
    {
        private IEnumerable<string> SelectKeywords()
        {
            var text = App.RemoveComments(Text);
            string block = GetSouroundingBlock(text);
            string prev = GetPreviousWord(text);
            IEnumerable<string> result = null;
            string search;

            // is within a class block
            if (block != null)
            {
                // has a word before it
                if (prev != null)
                {
                    search = $"{block}.{prev}.";
                    result = from x in autoCompleteKeywords
                             where x.StartsWith(search)
                             select x.Substring(search.Length);
                }
                // does not have a word before it (or at least no keyword)
                if (result == null || result.Count() == 0)
                {
                    search = $"{block}.";
                    result = from x in autoCompleteKeywords
                             where x.StartsWith(search)
                             select x.Substring(search.Length);
                }
            }
            // is outside
            else
            {
                // has word before it
                if (prev != null)
                {
                    search = $"{prev},";
                    result = from x in autoCompleteKeywords
                             where x.StartsWith(search)
                             select x.Substring(search.Length);
                }
                // does not have a word before it (or at least no class-block keyword)
                if (result == null || result.Count() == 0)
                    result = from x in autoCompleteKeywords
                             where x.IndexOf('.') < 0 && x.IndexOf(',') < 0
                             select x;
            }

            return result;
        }

        private string GetSouroundingBlock(string text)
        {
            var pos = CurrentPosition;

            for (int open = 0, close = 0; open < pos && open < text.Length; open++)
            {
                if (text[open] != '{')
                    continue;
                int i = BraceMatch(open);
                if (pos < i)
                {
                    var matches = Regex.Matches(text.Substring(close, open - close + 1), @"(\w+\s*){2,3}\{");
                    if (matches.Count == 0)
                        return null;
                    var lastMatch = matches[matches.Count - 1].Value;
                    return Regex.Matches(lastMatch, @"\w+")[0].Value;
                }
                open = close = i;
            }

            return null;
        }

        private string GetPreviousWord(string text)
        {
            var pos = CurrentPosition;
            var matches = Regex.Matches(text.Substring(0, pos), @"\w+\s*");
            return matches.Count > 0 ? matches[matches.Count - 1].Value.Trim() : null;
        }

        private static string[] autoCompleteKeywords = new[] {
            // buffer
            "buffer",
            "buffer.size",
            "buffer.txt",
            "buffer.usage",
            "buffer.xml",
            // csharp
            "csharp",
            "csharp.file",
            // fragoutput
            "fragoutput",
            "fragoutput.color",
            "fragoutput.depth",
            "fragoutput.height",
            "fragoutput.width",
            // image
            "image",
            "image.depth",
            "image.format",
            "image.height",
            "image.length",
            "image.width",
            // instance
            "instance",
            "instance.class",
            "instance.name",
            // pass
            "pass",
            "pass.comp",
            "pass.compute",
            "pass.draw",
            "pass.eval",
            "pass.exec",
            "pass.frag",
            "pass.geom",
            "pass.tess",
            "pass.tex",
            "pass.vert",
            // sampler
            "shader",
            "sampler",
            "sampler.magfilter",
            "sampler.minfilter",
            "sampler.wrap",
            // shader
            "shader,eval",
            "shader,frag",
            "shader,geom",
            "shader,tess",
            "shader,vert",
            "shader.atomic_uint",
            "shader.bvec2",
            "shader.bvec3",
            "shader.bvec4",
            "shader.centroid",
            "shader.discard",
            "shader.dmat2",
            "shader.dmat2x2",
            "shader.dmat2x3",
            "shader.dmat2x4",
            "shader.dmat3",
            "shader.dmat3x2",
            "shader.dmat3x3",
            "shader.dmat3x4",
            "shader.dmat4",
            "shader.dmat4x2",
            "shader.dmat4x3",
            "shader.dmat4x4",
            "shader.dvec2",
            "shader.dvec3",
            "shader.dvec4",
            "shader.flat",
            "shader.gl_ClipDistance",
            "shader.gl_DepthRange",
            "shader.gl_DepthRange.diff",
            "shader.gl_DepthRange.far",
            "shader.gl_DepthRange.near",
            "shader.gl_FragCoord",
            "shader.gl_FragDepth",
            "shader.gl_FrontFacing",
            "shader.gl_GlobalInvocationID",
            "shader.gl_InstanceID",
            "shader.gl_InvocationID",
            "shader.gl_Layer",
            "shader.gl_LocalInvocationID",
            "shader.gl_LocalInvocationIndex",
            "shader.gl_MaxPatchVertices",
            "shader.gl_NumSamples",
            "shader.gl_NumWorkGroups",
            "shader.gl_PatchVerticesIn",
            "shader.gl_PointCoord",
            "shader.gl_PointSize",
            "shader.gl_Position",
            "shader.gl_PrimitiveID",
            "shader.gl_PrimitiveIDIn",
            "shader.gl_SampleID",
            "shader.gl_SampleMask",
            "shader.gl_SampleMaskIn",
            "shader.gl_SamplePosition",
            "shader.gl_TessLevelInner",
            "shader.gl_TessLevelOuter",
            "shader.gl_VertexID",
            "shader.gl_ViewportIndex",
            "shader.gl_WorkGroupID",
            "shader.gl_WorkGroupSize",
            "shader.gl_in",
            "shader.gl_out",
            "shader.imageLoad",
            "shader.imageStore",
            "shader.in",
            "shader.inout",
            "shader.invariant",
            "shader.isampler1D",
            "shader.isampler1DArray",
            "shader.isampler2D",
            "shader.isampler2DArray",
            "shader.isampler2DMS",
            "shader.isampler2DMSArray",
            "shader.isampler2DRect",
            "shader.isampler3D",
            "shader.isamplerBuffer",
            "shader.isamplerCube",
            "shader.isamplerCubeArray",
            "shader.ivec2",
            "shader.ivec3",
            "shader.ivec4",
            "shader.layout",
            "shader.layout.binding",
            "shader.layout.component",
            "shader.layout.depth_any",
            "shader.layout.depth_greater",
            "shader.layout.depth_less",
            "shader.layout.depth_unchanged",
            "shader.layout.early_fragment_tests",
            "shader.layout.index",
            "shader.layout.location",
            "shader.layout.offset",
            "shader.layout.origin_upper_left",
            "shader.layout.pixel_center_integer",
            "shader.layout.std140",
            "shader.layout.vertices",
            "shader.layout.xfb_buffer",
            "shader.layout.xfb_offset",
            "shader.layout.xfb_stride",
            "shader.length",
            "shader.mat2",
            "shader.mat2x2",
            "shader.mat2x3",
            "shader.mat2x4",
            "shader.mat3",
            "shader.mat3x2",
            "shader.mat3x3",
            "shader.mat3x4",
            "shader.mat4",
            "shader.mat4x2",
            "shader.mat4x3",
            "shader.mat4x4",
            "shader.noperspective",
            "shader.normalize",
            "shader.out",
            "shader.patch",
            "shader.precision",
            "shader.precision.highp",
            "shader.precision.lowp",
            "shader.precision.mediump",
            "shader.sample",
            "shader.sampler1D",
            "shader.sampler1DArray",
            "shader.sampler1DArrayShadow",
            "shader.sampler1DShadow ",
            "shader.sampler2D",
            "shader.sampler2DArray",
            "shader.sampler2DArrayShadow",
            "shader.sampler2DMS",
            "shader.sampler2DMSArray",
            "shader.sampler2DRect",
            "shader.sampler2DRectShadow",
            "shader.sampler2DShadow",
            "shader.sampler3D",
            "shader.samplerBuffer",
            "shader.samplerCube",
            "shader.samplerCubeArray",
            "shader.samplerCubeArrayShadow",
            "shader.samplerCubeShadow",
            "shader.smooth",
            "shader.subroutine",
            "shader.texelFetch",
            "shader.texelFetchOffset",
            "shader.texture",
            "shader.textureGather",
            "shader.textureGatherOffset",
            "shader.textureGatherOffsets",
            "shader.textureGrad",
            "shader.textureLod",
            "shader.textureOffset",
            "shader.textureProj",
            "shader.textureProjGrad​",
            "shader.textureProjGradOffset​",
            "shader.textureProjLod​",
            "shader.textureProjLodOffset​",
            "shader.textureProjOffset​",
            "shader.textureQueryLevels",
            "shader.textureSize",
            "shader.uniform",
            "shader.usampler1D",
            "shader.usampler1DArray",
            "shader.usampler2D",
            "shader.usampler2DArray",
            "shader.usampler2DMS",
            "shader.usampler2DMSArray",
            "shader.usampler2DRect",
            "shader.usampler3D",
            "shader.usamplerBuffer",
            "shader.usamplerCube",
            "shader.usamplerCubeArray",
            "shader.uvec2",
            "shader.uvec3",
            "shader.uvec4",
            "shader.vec2",
            "shader.vec3",
            "shader.vec4",
            // tech
            "tech",
            "tech.pass",
            // text
            "text",
            // texture
            "texture",
            "texture.buff",
            "texture.img",
            "texture.samp",
            // vertinput
            "vertinput",
            "vertinput.attr"
        };
    }
}
