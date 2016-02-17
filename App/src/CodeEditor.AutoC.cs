using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="curPosition">The text position for which 
        /// to show the auto complete menu</param>
        public void AutoCShow(int curPosition)
        {
            // get the list of possible keywords from the current position
            var keywords = SelectCommands(curPosition);
            var wordPos = WordStartPosition(curPosition, true);
            AutoCShow(curPosition - wordPos, keywords.Cat("|"));
        }

        /// <summary>
        /// Select all commands that can be used at the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="isHint"></param>
        /// <returns></returns>
        private IEnumerable<string> SelectCommands(int position, bool isHint = false)
        {
            // get necessary information for keyword search
            var block = BlockHeader(position).FirstOrDefault();
            var word = GetWordFromPosition(position);

            // if not inside a block swap the
            // block type is given by the word
            if (block == null)
            {
                block = word;
                word = null;
            }

            // search for keywords
            var search = block + (word != null ? $".{word}" : "");
            var selection = from x in Keywords where x.StartsWith(search) select x;

            // define the select function based the isHint parameter
            var start = search.Length;
            var func = isHint
                // search only for hints (contains '<'; must not contain '.' and ',')
                ? (x => x.IndexOf('<', start) != -1 && x.IndexOf(y => y == '.' || y == ',', start) == -1)
                // search only for keywords (must not contain '<', '.' and ',')
                : (Func<string, bool>)(x => x.IndexOf(y => y == '.' || y == ',' || y == '<', start) == -1);

            // select keywords and remove hierarchy prefix
            start = block != null && word != null ? block.Length + 1 : 0;
            return selection.Where(func).Select(x => x.Substring(start));
        }

        /// <summary>
        /// Get header of the block surrounding the specified text position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Returns a list of words making up the header or nothing.</returns>
        private IEnumerable<string> BlockHeader(int position)
        {
            // find surrounding block
            var blocks = from x in this.GetBlockPositions()
                         select new[] { x.Index, x.Index + x.Value.IndexOf('{'), x.Index + x.Length };

            // get headers of the block surrounding the text position
            var headers = from x in blocks
                          where x[1] < position && position < x[2]
                          select Text.Substring(x[0], x[1] - x[0]);
            
            // return all words before the open brace '{'
            foreach (var header in headers)
                foreach (Match match in Regex.Matches(header, @"\w+"))
                    yield return match.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            // get class references
            var editor = (CodeEditor)sender;

            if (editor.EnableCodeInformation == false)
                return;

            // convert cursor position to text position
            int pos = editor.CharPositionFromPoint(e.X, e.Y);
            var style = editor.GetStyleAt(pos);
            if (!new[] { STYLE.Default, STYLE.Comment, STYLE.CommentLine, STYLE.CommentLineDoc }.Any(x => x == style))
            {
                var word = editor.GetWordFromPosition(pos);
                if (word?.Length > 0)
                {
                    var cmds = editor.SelectCommands(pos, true);
                    if (cmds.Count() > 0)
                    {
                        pos = editor.WordStartPosition(pos, true);
                        editor.CallTipShow(pos, cmds.Cat("\n"));
                    }
                    return;
                }
            }

            editor.CallTipCancel();
        }

        #region AUTO COMPLETE KEYWORDS
        // Keyword can be defined as follows:
        // <class type>[,<class annotation> | .<class keyword> [.<sub keyword>]]
        private static string[] Keywords = new[] {
            // buffer
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
            // csharp
            "csharp",
            "csharp <name>",
            "csharp.file",
            "csharp.file <path> [path] [...]",
            // fragoutput
            "fragoutput",
            "fragoutput <name>",
            "fragoutput.color",
            "fragoutput.depth",
            "fragoutput.height",
            "fragoutput.width",
            // image
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
            // instance
            "instance",
            "instance <name>",
            "instance.class",
            "instance.class <csharp_name> <c# class>",
            "instance.name",
            "instance.name <internal name>",
            // pass
            "pass",
            "pass <name>",
            "pass.comp",
            "pass.comp <shader_name>",
            "pass.compute",
            "pass.draw",
            "pass.draw <vertinput> <primitive> <base_vertex> <vertex_count> [base_instance] [instance_count]",
            "pass.draw <vertinput> <index buffer> <index_type> <primitive> <base_vertex> <base_index> <index_count> [base_instance] [instance_count]",
            "pass.draw <vertinput> <draw_buffer> <primitive> [buffer_offset]",
            "pass.draw <vertinput> <index_buffer> <index_type> <draw_ buffer> <primitive> [buffer_offset]",
            "pass.eval",
            "pass.eval <shader_name>",
            "pass.exec",
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
            // sampler
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
            // shader
            "shader",
            "shader <shader type> <name>",
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
            "tech <name>",
            "tech.pass",
            // text
            "text",
            "text <name>",
            // texture
            "texture",
            "texture <name>",
            "texture.buff",
            "texture.buff <name>",
            "texture.img",
            "texture.img <name>",
            "texture.samp",
            "texture.samp <name>",
            // vertinput
            "vertinput",
            "vertinput <name>",
            "vertinput.attr",
            "vertinput.attr <buff_name> <type> <dim> [stride] [offset] [divisor]",
            // vertoutput
            "vertoutput",
            "vertoutput <name>",
            "vertoutput.buff",
            "vertoutput.buff <name>",
            "vertoutput.pause",
            "vertoutput.pause <true_false>",
            "vertoutput.resume",
            "vertoutput.resume <true_false>"
        };
        #endregion
    }
}
