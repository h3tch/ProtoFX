using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using STYLE = ScintillaNET.Style.Cpp;

namespace App
{
    partial class CodeEditor
    {
        /// <summary>
        /// Show auto complete menu for the specified text position.
        /// </summary>
        /// <param name="position">The text position for which 
        /// to show the auto complete menu</param>
        public void AutoCShow(int position)
        {
            string search;
            int skip;

            // create search string
            SelectString(position, out search, out skip);

            // search for all keywords starting with the
            // search string and not containing subkeywords
            var invalid = new[] { '.', ',', ' ' };
            var keywords = from x in Keywords
                           where x.StartsWith(search) && invalid.All(y => x.IndexOf(y, search.Length) < 0)
                           select x.Substring(skip);

            // show auto complete list
            AutoCShow(position - WordStartPosition(position, true), keywords.Cat("|"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            string search;
            int skip;

            // check if code hints are enabled
            if (EnableCodeHints == false)
                return;

            // convert cursor position to text position
            int pos = CharPositionFromPoint(e.X, e.Y);

            // select keywords using the current text position
            // is the style at that position a valid hint style
            var style = GetStyleAt(pos);
            if (new[] { STYLE.Word, STYLE.Word2 }.Any(x => x == style))
            {
                // is there a word at that position
                var word = GetWordFromPosition(pos);
                if (word?.Length > 0)
                {
                    // create search string
                    SelectString(pos, out search, out skip);
                    search += ' ';
                    // select hints
                    var hints = from x in Keywords
                                where x.StartsWith(search)
                                select x.Substring(skip);
                    
                    if (hints.Count() > 0)
                    {
                        CallTipShow(WordStartPosition(pos, true), hints.Cat("\n"));
                        return;
                    }
                }
            }

            CallTipCancel();
        }

        public void SelectString(int position, out string search, out int skip)
        {
            // get word and preceding word at caret position
            var word = GetWordFromPosition(position);
            var prec = GetWordFromPosition(WordStartPosition(WordStartPosition(position, true), false));
            // get block surrounding caret position
            var block = BlockPosition(position);
            // get block header from block position
            var header = BlockHeader(block).ToArray();

            // is the caret inside the header
            var inHeader = block != null ? block[0] <= position && position <= block[1] : false;
            var inBody = block != null ? block[1] < position && position <= block[2] : false;

            // create search string
            search = inHeader
                // in header
                ? header[0] == word
                    // word represents the block type -> search for block type
                    ? word
                    // preceding word is the block type
                    : header[0] == prec
                        // -> search for block annotation
                        ? $"{header[0]},{word}"
                        // this is the name of the block -> search for nothing
                        : "~"
                // not in header but body
                : inBody
                    // preceding word has subkeywords
                    ? Keywords.Any(x => x.StartsWith($"{header[0]}.{prec}."))
                        // -> search for subkeywords
                        ? $"{header[0]}.{prec}.{word}"
                        // -> search for keywords
                        : $"{header[0]}.{word}"
                    // nether in header nor body -> search for block type
                    : word;
            skip = search.Length - word.Length;
        }

        /// <summary>
        /// Get the block surrounding the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Returns the start, open brace and end position of the block as an int array.</returns>
        private int[] BlockPosition(int position)
        {
            // find surrounding block
            var blocks = from x in this.GetBlockPositions()
                         select new[] { x.Index, x.Index + x.Value.IndexOf('{'), x.Index + x.Length };

            // get headers of the block surrounding the text position
            foreach (var x in blocks)
                if (x[0] < position && position < x[2])
                    return x;
            return null;
        }

        /// <summary>
        /// Get the header of the block.
        /// </summary>
        /// <param name="block"></param>
        /// <returns>Returns a list of words making up the header or nothing.</returns>
        private IEnumerable<string> BlockHeader(int[] block)
        {
            if (block != null)
            {
                // get headers of the block surrounding the text position
                var header = Text.Substring(block[0], block[1] - block[0]);

                // return all words before the open brace '{'
                foreach (Match match in Regex.Matches(header, @"\w+"))
                    yield return match.Value;
            }
        }

        #region AUTO COMPLETE KEYWORDS
        // Keyword can be defined as follows:
        // <class type>[,<class annotation> | .<class keyword> [.<sub keyword>]]
        private static string[] Keywords = new[] {
            #region buffer
            "buffer",
            "buffer <name>",
            "buffer.size",
            "buffer.size <num_bytes>",
            "buffer.txt",
            "buffer.txt <text_name>",
            "buffer.usage",
            "buffer.usage <usage_hint>",
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
            "buffer.xml <file_path> <xml_node>",
            #endregion
            #region csharp
            "csharp",
            "csharp <name>",
            "csharp.file",
            "csharp.file <path> [path] [...]",
            #endregion
            #region fragoutput
            "fragoutput",
            "fragoutput <name>",
            "fragoutput.color",
            "fragoutput.depth",
            "fragoutput.height",
            "fragoutput.width",
            #endregion
            #region image
            "image",
            "image <name>",
            "image.depth",
            "image.depth <num_layers>",
            "image.file",
            "image.file <path> [path] [...]",
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
            "image.height <num_pixels>",
            "image.length",
            "image.length <num_layers>",
            "image.type",
            "image.type.texture1D",
            "image.type.texture2D",
            "image.type.texture3D",
            "image.type.texture1DArray",
            "image.type.texture2DArray",
            "image.width",
            "image.width <num_pixels>",
            #endregion
            #region instance
            "instance",
            "instance <name>",
            "instance.class",
            "instance.class <csharp_name> <c# class>",
            "instance.name",
            "instance.name <internal name>",
            #endregion
            #region pass
            "pass",
            "pass <name>",
            "pass.comp",
            "pass.comp <shader_name>",
            "pass.compute",
            "pass.draw",
            "pass.draw <vertinput> <primitive> <base_vertex> <vertex_count> [base_instance] [instance_count]",
            "pass.draw <vertinput> <index buffer> <index_type> <primitive> <base_vertex> <base_index> <index_count> [base_instance] [instance_count]",
            "pass.draw <vertinput> <draw_buffer> <primitive> [buffer_offset]",
            "pass.draw <vertinput> <index_buffer> <index_type> <draw_buffer> <primitive> [buffer_offset]",
            "pass.eval",
            "pass.eval <shader_name>",
            "pass.exec",
            "pass.exec <instance_name>",
            "pass.frag",
            "pass.frag <shader_name>",
            "pass.geom",
            "pass.geom <shader_name>",
            "pass.tess",
            "pass.tess <shader_name>",
            "pass.tex",
            "pass.tex <tex_name> <bind_unit>",
            "pass.vert",
            "pass.vert <shader_name>",
            #endregion
            #region sampler
            "sampler",
            "sampler <name>",
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
            #endregion
            #region shader
            "shader",
            "shader <shader_type> <name>",
            "shader,comp",
            "shader,comp \n"
            + "  in  uvec3 gl_NumWorkGroups\n"
            + "  in  uvec3 gl_WorkGroupSize\n"
            + "  in  uvec3 gl_WorkGroupID\n"
            + "  in  uvec3 gl_LocalInvocationID\n"
            + "  in  uvec3 gl_GlobalInvocationID\n"
            + "  in  uint  gl_LocalInvocationIndex",
            "shader,eval",
            "shader,eval \n"
            + "  in  vec3  gl_TessCoord\n"
            + "  in  int   gl_PatchVerticesIn\n"
            + "  in  int   gl_PrimitiveID\n"
            + "  in  float gl_TessLevelOuter[4]\n"
            + "  in  float gl_TessLevelInner[2]\n"
            + "  in  vec4  gl_in[].gl_Position\n"
            + "  in  float gl_in[].gl_PointSize\n"
            + "  in  float gl_in[].gl_ClipDistance[]\n"
            + "  out vec4  gl_out[].gl_Position\n"
            + "  out float gl_out[].gl_PointSize\n"
            + "  out float gl_out[].gl_ClipDistance[]",
            "shader,frag",
            "shader,frag \n"
            + "  in  vec4  gl_FragCoord\n"
            + "  in  bool  gl_FrontFacing\n"
            + "  in  vec2  gl_PointCoord\n"
            + "  in  int   gl_SampleID\n"
            + "  in  vec2  gl_SamplePosition\n"
            + "  in  int   gl_SampleMaskIn[]\n"
            + "  in  float gl_ClipDistance[]\n"
            + "  in  int   gl_PrimitiveID\n"
            + "  in  int   gl_Layer\n"
            + "  in  int   gl_ViewportIndex\n"
            + "  out float gl_FragDepth\n"
            + "  out int   gl_SampleMask[]",
            "shader,geom",
            "shader,geom \n"
            + "  in  int   gl_PrimitiveIDIn\n"
            + "  in  int   gl_InvocationID\n"
            + "  in  vec4  gl_in[].gl_Position\n"
            + "  in  float gl_in[].gl_PointSize\n"
            + "  in  float gl_in[].gl_ClipDistance[]\n"
            + "  out int   gl_PrimitiveID\n"
            + "  out int   gl_Layer\n"
            + "  out int   gl_ViewportIndex\n"
            + "  out vec4  gl_out[].gl_Position\n"
            + "  out float gl_out[].gl_PointSize\n"
            + "  out float gl_out[].gl_ClipDistance[]",
            "shader,tess",
            "shader,tess \n"
            + "  in  int   gl_PatchVerticesIn\n"
            + "  in  int   gl_PrimitiveID\n"
            + "  in  int   gl_InvocationID\n"
            + "  in  vec4  gl_in[].gl_Position\n"
            + "  in  float gl_in[].gl_PointSize\n"
            + "  in  float gl_in[].gl_ClipDistance[]\n"
            + "  out float gl_TessLevelOuter[4]\n"
            + "  out float gl_TessLevelInner[4]\n"
            + "  out vec4  gl_out[].gl_Position\n"
            + "  out float gl_out[].gl_PointSize\n"
            + "  out float gl_out[].gl_ClipDistance[]",
            "shader,vert",
            "shader,vert \n"
            + "  in  int   gl_VertexID\n"
            + "  in  int   gl_InstanceID\n"
            + "  out vec4  gl_Position\n"
            + "  out float gl_PointSize\n"
            + "  out float gl_ClipDistance[]",
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
            #endregion
            #region tech
            "tech",
            "tech <name>",
            "tech.pass",
            "tech.pass <name>",
            #endregion
            #region text
            "text",
            "text <name>",
            #endregion
            #region texture
            "texture",
            "texture <name>",
            "texture.buff",
            "texture.buff <name>",
            "texture.img",
            "texture.img <name>",
            "texture.samp",
            "texture.samp <name>",
            #endregion
            #region vertinput
            "vertinput",
            "vertinput <name>",
            "vertinput.attr",
            "vertinput.attr <buff_name> <type> <dim> [stride] [offset] [divisor]",
            #endregion
            #region vertoutput
            "vertoutput",
            "vertoutput <name>",
            "vertoutput.buff",
            "vertoutput.buff <name>",
            "vertoutput.pause",
            "vertoutput.pause <true_false>",
            "vertoutput.resume",
            "vertoutput.resume <true_false>"
            #endregion
        };
        #endregion
    }
}
