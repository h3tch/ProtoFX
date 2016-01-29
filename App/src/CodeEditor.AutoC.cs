using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App
{
    partial class CodeEditor
    {
        /// <summary>
        /// Show auto complete menu for the specified text position.
        /// </summary>
        /// <param name="curPosition">The text position for which 
        /// to show the auto complete menu</param>
        public void AutoCShow(int curPosition)
        {
            // get the list of possible keywords from the current position
            var keywords = SelectKeywords(curPosition);
            var wordPos = WordStartPosition(curPosition, true);
            AutoCShow(curPosition - wordPos, keywords.Merge(" "));
        }

        private IEnumerable<string> SelectKeywords(int curPosition)
        {
            // get necessary information for keyword search
            var text = Compiler.RemoveComments(Text);
            var block = BlockDef(text, curPosition).FirstOrDefault();
            var prev = PrecedingWord(text, curPosition);
            IEnumerable<string> result = null;

            // has a word before it
            if (prev != null)
                result = KeywordsStartingWith(block != null ? $"{block}.{prev}." : $"{prev},");

            // does not have a word before it (or at least no keyword)
            if (result == null || result.Count() == 0)
                result = KeywordsStartingWith(block != null ? $"{block}." : string.Empty);

            // return all keywords that are not hierarchical
            return from x in result
                   where x.IndexOf('.') < 0 && x.IndexOf(',') < 0
                   select x;
        }

        private IEnumerable<string> KeywordsStartingWith(string searchString)
        {
            // search for all keywords starting with <searchString>
            return from x in autoCompleteKeywords
                   where x.StartsWith(searchString)
                   select x.Substring(searchString.Length);
        }

        private IEnumerable<string> BlockDef(string text, int curPosition)
        {
            // find surrounding block
            var block = (from x in Compiler.GetBlockPositions(text)
                         where x[1] < curPosition && curPosition < x[2]
                         select x).FirstOrDefault();

            // surrounding block found
            if (block != null)
            {
                // return all words before the open brace '{'
                var blockDef = text.Substring(block[0], block[1] - block[0]);
                foreach (Match match in Regex.Matches(blockDef, @"\w+"))
                    yield return match.Value;
            }
        }

        private static string PrecedingWord(string text, int textPosition)
        {
            // find all words
            var matches = Regex.Matches(text.Substring(0, textPosition), @"\w+\s*");
            // return the las word
            return matches.Count > 0 ? matches[matches.Count - 1].Value.Trim() : null;
        }

        #region AUTO COMPLETE KEYWORDS
        // Keyword can be defined as follows:
        // <class type>[,<class annotation> | .<class keyword> [.<sub keyword>]]
        private static string[] autoCompleteKeywords = new[] {
            // buffer
            "buffer",
            "buffer.size",
            "buffer.txt",
            "buffer.usage",
            "buffer.usage.DynamicCopy",
            "buffer.usage.DynamicDraw",
            "buffer.usage.DynamicRead",
            "buffer.usage.StaticCopy",
            "buffer.usage.StaticDraw",
            "buffer.usage.StaticRead",
            "buffer.usage.StreamCopy",
            "buffer.usage.StreamDraw",
            "buffer.usage.StreamRead",
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
            "image.format.depth",
            "image.format.depth16",
            "image.format.depth24",
            "image.format.depth24stencil8",
            "image.format.depth32",
            "image.format.depth32f",
            "image.format.depth32fstencil8",
            "image.format.depthstencil",
            "image.format.r8",
            "image.format.r8i",
            "image.format.r8ui",
            "image.format.r16",
            "image.format.r16i",
            "image.format.r16ui",
            "image.format.r16f",
            "image.format.r32i",
            "image.format.r32ui",
            "image.format.r32f",
            "image.format.rg8",
            "image.format.rg8i",
            "image.format.rg8ui",
            "image.format.rg16",
            "image.format.rg16i",
            "image.format.rg16ui",
            "image.format.rg16f",
            "image.format.rg32i",
            "image.format.rg32ui",
            "image.format.rg32f",
            "image.format.rgb8",
            "image.format.rgb8i",
            "image.format.rgb8ui",
            "image.format.rgb16",
            "image.format.rgb16i",
            "image.format.rgb16ui",
            "image.format.rgb16f",
            "image.format.rgb32i",
            "image.format.rgb32ui",
            "image.format.rgb32f",
            "image.format.rgba8",
            "image.format.rgba8i",
            "image.format.rgba8ui",
            "image.format.rgba16",
            "image.format.rgba16i",
            "image.format.rgba16ui",
            "image.format.rgba16f",
            "image.format.rgba32i",
            "image.format.rgba32ui",
            "image.format.rgba32f",
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
            "sampler",
            "sampler.magfilter",
            "sampler.magfilter.linear",
            "sampler.magfilter.nearest",
            "sampler.minfilter",
            "sampler.minfilter.linear",
            "sampler.minfilter.nearest",
            "sampler.minfilter.LinearMipmapLinear",
            "sampler.minfilter.LinearMipmapNearest",
            "sampler.minfilter.NearestMipmapLinear",
            "sampler.minfilter.NearestMipmapNearest",
            "sampler.wrap",
            "sampler.wrap.ClampToBorder",
            "sampler.wrap.ClampToEdge",
            "sampler.wrap.MirroredRepeat",
            "sampler.wrap.Repeat",
            // shader
            "shader",
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
            "shader.sampler1DShadow",
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
        #endregion
    }
}
