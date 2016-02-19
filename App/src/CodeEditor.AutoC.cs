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
            "shader.abs",
            "shader.acos",
            "shader.acosh",
            "shader.all",
            "shader.any",
            "shader.asin",
            "shader.asinh",
            "shader.atan",
            "shader.atanh",
            "shader.atomic_uint",
            "shader.atomicAdd",
            "shader.atomicAdd \n"
            + "  int  atomicAdd(inout int  mem, int  data)"
            + "  uint atomicAdd(inout uint mem, uint data)",
            "shader.atomicAnd",
            "shader.atomicAnd \n"
            + "  int  atomicAdd(inout int  mem, int  data)"
            + "  uint atomicAdd(inout uint mem, uint data)",
            "shader.atomicCompSwap",
            "shader.atomicCompSwap \n"
            + "  int  atomicCompSwap(inout int  mem, uint compare, uint data)\n"
            + "  uint atomicCompSwap(inout uint mem, uint compare, uint data)",
            "shader.atomicCounter",
            "shader.atomicCounter \n"
            + "  uint atomicCounter(atomic_uint c)",
            "shader.atomicCounterDecrement",
            "shader.atomicCounterDecrement \n"
            + "  uint atomicCounterDecrement(atomic_uint c)",
            "shader.atomicCounterIncrement",
            "shader.atomicCounterIncrement \n"
            + "  uint atomicCounterIncrement(atomic_uint c)",
            "shader.atomicExchange",
            "shader.atomicExchange \n"
            + "  int  atomicExchange(inout int  mem, int  data)\n"
            + "  uint atomicExchange(inout uint mem, uint data)",
            "shader.atomicMin",
            "shader.atomicMin \n"
            + "  int  atomicMin(inout int  mem, int  data)\n"
            + "  uint atomicMin(inout uint mem, uint data)",
            "shader.atomicMax",
            "shader.atomicMax \n"
            + "  int  atomicMax(inout int  mem, int  data)\n"
            + "  uint atomicMax(inout uint mem, uint data)",
            "shader.atomicOr",
            "shader.atomicOr \n"
            + "  int  atomicOr(inout int  mem, int  data)\n"
            + "  uint atomicOr(inout uint mem, uint data)",
            "shader.atomicXor",
            "shader.atomicXor \n"
            + "  int  atomicXor(inout int  mem, int  data)\n"
            + "  uint atomicXor(inout uint mem, uint data)",
            "shader.barrier",
            "shader.barrier \n"
            + "  void barrier(void)",
            "shader.bitCount",
            "shader.bitCount \n"
            + "  genIType bitCount(genIType value)\n"
            + "  genIType bitCount(genUType value)",
            "shader.bitfieldExtract",
            "shader.bitfieldExtract \n"
            + "  genIType bitfieldExtract(genIType value, int offset, int bits)\n"
            + "  genUType bitfieldExtract(genUType value, int offset, int bits)",
            "shader.bitfieldInsert",
            "shader.bitfieldInsert \n"
            + "  genIType bitfieldInsert(genIType base, genIType insert, int offset, int bits)\n"
            + "  genUType bitfieldInsert(genUType base, genUType insert, int offset, int bits)",
            "shader.bitfieldReverse",
            "shader.bitfieldReverse \n"
            + "  genIType bitfieldReverse(genIType value)\n"
            + "  genUType bitfieldReverse(genUType value)",
            "shader.bvec2",
            "shader.bvec3",
            "shader.bvec4",
            "shader.ceil",
            "shader.centroid",
            "shader.clamp",
            "shader.cos",
            "shader.cosh",
            "shader.cross",
            "shader.degrees",
            "shader.degrees \n"
            + "  genType degrees(genType radians)",
            "shader.determinant \n"
            + "  float determinant(mat2 m)\n"
            + "  float determinant(mat3 m)\n"
            + "  float determinant(mat4 m)\n"
            + "  double determinant(dmat2 m)\n"
            + "  double determinant(dmat3 m)\n"
            + "  double determinant(dmat4 m)",
            "shader.discard",
            "shader.dFdx",
            "shader.dFdx \n"
            + "  genType dFdx(genType p)",
            "shader.dFdxCoarse",
            "shader.dFdxCoarse \n"
            + "  genType dFdxCoarse(genType p)",
            "shader.dFdxFine",
            "shader.dFdxFine \n"
            + "  genType dFdxFine(genType p)",
            "shader.dFdy \n"
            + "  genType dFdy(genType p)",
            "shader.dFdyCoarse \n"
            + "  genType dFdyCoarse(genType p)",
            "shader.dFdyFine \n"
            + "  genType dFdyFine(genType p)",
            "shader.distance",
            "shader.distance \n"
            + "  float distance(genType p0, genType p1)\n"
            + "  double distance(genDType p0, genDType p1)",
            "shader.dot",
            "shader.dot \n"
            + "  float dot(genType x, genType y)\n"
            + "  double dot(genDType x, genDType y)",
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
            "shader.EmitStreamVertex",
            "shader.EmitVertex",
            "shader.EndPrimitive",
            "shader.EndStreamPrimitive",
            "shader.equal",
            "shader.exp",
            "shader.exp2",
            "shader.faceforward",
            "shader.findLSB",
            "shader.findMSB",
            "shader.flat",
            "shader.floor",
            "shader.fma",
            "shader.fract",
            "shader.frexp",
            "shader.fwidth",
            "shader.fwidthCoarse",
            "shader.fwidthFine",
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
            "shader.imageAtomicAdd",
            "shader.imageAtomicAdd -- atomically add a value to an existing value in memory\n"
            + "                  and return the original value\n"
            + "  uint imageAtomicAdd(gimage1D        image, int   P, uint data)\n"
            + "  uint imageAtomicAdd(gimage2D        image, ivec2 P, uint data)\n"
            + "  uint imageAtomicAdd(gimage3D        image, ivec3 P, uint data)\n"
            + "  uint imageAtomicAdd(gimage2DRect    image, ivec2 P, uint data)\n"
            + "  uint imageAtomicAdd(gimageCube      image, ivec3 P, uint data)\n"
            + "  uint imageAtomicAdd(gbufferImage    image, int   P, uint data)\n"
            + "  uint imageAtomicAdd(gimage1DArray   image, ivec2 P, uint data)\n"
            + "  uint imageAtomicAdd(gimage2DArray   image, ivec3 P, uint data)\n"
            + "  uint imageAtomicAdd(gimageCubeArray image, ivec3 P, uint data)\n"
            + "  uint imageAtomicAdd(gimage2DMS      image, ivec2 P, int sample, uint data)\n"
            + "  uint imageAtomicAdd(gimage2DMSArray image, ivec3 P, int sample, uint data)\n"
            + "  int  imageAtomicAdd(gimage1D        image, int   P, int data)\n"
            + "  int  imageAtomicAdd(gimage2D        image, ivec2 P, int data)\n"
            + "  int  imageAtomicAdd(gimage3D        image, ivec3 P, int data)\n"
            + "  int  imageAtomicAdd(gimage2DRect    image, ivec2 P, int data)\n"
            + "  int  imageAtomicAdd(gimageCube      image, ivec3 P, int data)\n"
            + "  int  imageAtomicAdd(gbufferImage    image, int   P, int data)\n"
            + "  int  imageAtomicAdd(gimage1DArray   image, ivec2 P, int data)\n"
            + "  int  imageAtomicAdd(gimage2DArray   image, ivec3 P, int data)\n"
            + "  int  imageAtomicAdd(gimageCubeArray image, ivec3 P, int data)\n"
            + "  int  imageAtomicAdd(gimage2DMS      image, ivec2 P, int sample, int data)\n"
            + "  int  imageAtomicAdd(gimage2DMSArray image, ivec3 P, int sample, int data)",
            "shader.imageAtomicAnd",
            "shader.imageAtomicAnd -- atomically compute the logical AND of a value with an existing\n"
            + "                  value in memory, store that value and return the original value\n"
            + "  uint imageAtomicAnd(gimage1D        image, int   P, uint data)\n"
            + "  uint imageAtomicAnd(gimage2D        image, ivec2 P, uint data)\n"
            + "  uint imageAtomicAnd(gimage3D        image, ivec3 P, uint data)\n"
            + "  uint imageAtomicAnd(gimage2DRect    image, ivec2 P, uint data)\n"
            + "  uint imageAtomicAnd(gimageCube      image, ivec3 P, uint data)\n"
            + "  uint imageAtomicAnd(gbufferImage    image, int   P, uint data)\n"
            + "  uint imageAtomicAnd(gimage1DArray   image, ivec2 P, uint data)\n"
            + "  uint imageAtomicAnd(gimage2DArray   image, ivec3 P, uint data)\n"
            + "  uint imageAtomicAnd(gimageCubeArray image, ivec3 P, uint data)\n"
            + "  uint imageAtomicAnd(gimage2DMS      image, ivec2 P, int sample, uint data)\n"
            + "  uint imageAtomicAnd(gimage2DMSArray image, ivec3 P, int sample, uint data)\n"
            + "  int  imageAtomicAnd(gimage1D        image, int   P, int data)\n"
            + "  int  imageAtomicAnd(gimage2D        image, ivec2 P, int data)\n"
            + "  int  imageAtomicAnd(gimage3D        image, ivec3 P, int data)\n"
            + "  int  imageAtomicAnd(gimage2DRect    image, ivec2 P, int data)\n"
            + "  int  imageAtomicAnd(gimageCube      image, ivec3 P, int data)\n"
            + "  int  imageAtomicAnd(gbufferImage    image, int   P, int data)\n"
            + "  int  imageAtomicAnd(gimage1DArray   image, ivec2 P, int data)\n"
            + "  int  imageAtomicAnd(gimage2DArray   image, ivec3 P, int data)\n"
            + "  int  imageAtomicAnd(gimageCubeArray image, ivec3 P, int data)\n"
            + "  int  imageAtomicAnd(gimage2DMS      image, ivec2 P, int sample, int data)\n"
            + "  int  imageAtomicAnd(gimage2DMSArray image, ivec3 P, int sample, int data)",
            "shader.imageAtomicCompSwap",
            "shader.imageAtomicCompSwap -- atomically compares supplied data with that in memory and conditionally stores it to memory\n"
            + "  uint imageAtomicCompSwap(gimage1D        image, int   P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimage2D        image, ivec2 P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimage3D        image, ivec3 P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimage2DRect    image, ivec2 P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimageCube      image, ivec3 P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gbufferImage    image, int   P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimage1DArray   image, ivec2 P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimage2DArray   image, ivec3 P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimageCubeArray image, ivec3 P, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimage2DMS      image, ivec2 P, int sample, uint compare, uint data)\n"
            + "  uint imageAtomicCompSwap(gimage2DMSArray image, ivec3 P, int sample, uint compare, uint data)\n"
            + "  int  imageAtomicCompSwap(gimage1D        image, int   P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimage2D        image, ivec2 P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimage3D        image, ivec3 P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimage2DRect    image, ivec2 P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimageCube      image, ivec3 P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gbufferImage    image, int   P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimage1DArray   image, ivec2 P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimage2DArray   image, ivec3 P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimageCubeArray image, ivec3 P, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimage2DMS      image, ivec2 P, int sample, int compare, int data)\n"
            + "  int  imageAtomicCompSwap(gimage2DMSArray image, ivec3 P, int sample, int compare, int data)",
            "shader.imageAtomicExchange",
            "shader.imageAtomicExchange -- atomically store supplied data into memory and return the original value from memory\n"
            + "  uint imageAtomicExchange(gimage1D image, int P, uint data)\n"
            + "  uint imageAtomicExchange(gimage2D image, ivec2 P, uint data)\n"
            + "  uint imageAtomicExchange(gimage3D image, ivec3 P, uint data)\n"
            + "  uint imageAtomicExchange(gimage2DRect image, ivec2 P, uint data)\n"
            + "  uint imageAtomicExchange(gimageCube image, ivec3 P, uint data)\n"
            + "  uint imageAtomicExchange(gbufferImage image, int P, uint data)\n"
            + "  uint imageAtomicExchange(gimage1DArray image, ivec2 P, uint data)\n"
            + "  uint imageAtomicExchange(gimage2DArray image, ivec3 P, uint data)\n"
            + "  uint imageAtomicExchange(gimageCubeArray image, ivec3 P, uint data)\n"
            + "  uint imageAtomicExchange(gimage2DMS image, ivec2 P, int sample, uint data)\n"
            + "  uint imageAtomicExchange(gimage2DMSArray image, ivec3 P, int sample, uint data)\n"
            + "  int  imageAtomicExchange(gimage1D image, int P, int data)\n"
            + "  int  imageAtomicExchange(gimage2D image, ivec2 P, int data)\n"
            + "  int  imageAtomicExchange(gimage3D image, ivec3 P, int data)\n"
            + "  int  imageAtomicExchange(gimage2DRect image, ivec2 P, int data)\n"
            + "  int  imageAtomicExchange(gimageCube image, ivec3 P, int data)\n"
            + "  int  imageAtomicExchange(gbufferImage image, int P, int data)\n"
            + "  int  imageAtomicExchange(gimage1DArray image, ivec2 P, int data)\n"
            + "  int  imageAtomicExchange(gimage2DArray image, ivec3 P, int data)\n"
            + "  int  imageAtomicExchange(gimageCubeArray image, ivec3 P, int data)\n"
            + "  int  imageAtomicExchange(gimage2DMS image, ivec2 P, int sample, int data)\n"
            + "  int  imageAtomicExchange(gimage2DMSArray image, ivec3 P, int sample, int data)\n"
            + "  int  imageAtomicExchange(gimage1D image, int P, float data)\n"
            + "  int  imageAtomicExchange(gimage2D image, ivec2 P, float data)\n"
            + "  int  imageAtomicExchange(gimage3D image, ivec3 P, float data)\n"
            + "  int  imageAtomicExchange(gimage2DRect image, ivec2 P, float data)\n"
            + "  int  imageAtomicExchange(gimageCube image, ivec3 P, float data)\n"
            + "  int  imageAtomicExchange(gbufferImage image, int P, float data)\n"
            + "  int  imageAtomicExchange(gimage1DArray image, ivec2 P, float data)\n"
            + "  int  imageAtomicExchange(gimage2DArray image, ivec3 P, float data)\n"
            + "  int  imageAtomicExchange(gimageCubeArray image, ivec3 P, float data)\n"
            + "  int  imageAtomicExchange(gimage2DMS image, ivec2 P, int sample, float data)\n"
            + "  int  imageAtomicExchange(gimage2DMSArray image, ivec3 P, int sample, float data)",
            "shader.imageAtomicMax",
            "shader.imageAtomicMax -- atomically compute the minimum of a value with an existing\n"
            + "                  value in memory, store that value and return the original value\n"
            + "  uint imageAtomicMax(gimage1D        image, int   P, uint data)\n"
            + "  uint imageAtomicMax(gimage2D        image, ivec2 P, uint data)\n"
            + "  uint imageAtomicMax(gimage3D        image, ivec3 P, uint data)\n"
            + "  uint imageAtomicMax(gimage2DRect    image, ivec2 P, uint data)\n"
            + "  uint imageAtomicMax(gimageCube      image, ivec3 P, uint data)\n"
            + "  uint imageAtomicMax(gbufferImage    image, int   P, uint data)\n"
            + "  uint imageAtomicMax(gimage1DArray   image, ivec2 P, uint data)\n"
            + "  uint imageAtomicMax(gimage2DArray   image, ivec3 P, uint data)\n"
            + "  uint imageAtomicMax(gimageCubeArray image, ivec3 P, uint data)\n"
            + "  uint imageAtomicMax(gimage2DMS      image, ivec2 P, int sample, uint data)\n"
            + "  uint imageAtomicMax(gimage2DMSArray image, ivec3 P, int sample, uint data)\n"
            + "  int  imageAtomicMax(gimage1D        image, int   P, int data)\n"
            + "  int  imageAtomicMax(gimage2D        image, ivec2 P, int data)\n"
            + "  int  imageAtomicMax(gimage3D        image, ivec3 P, int data)\n"
            + "  int  imageAtomicMax(gimage2DRect    image, ivec2 P, int data)\n"
            + "  int  imageAtomicMax(gimageCube      image, ivec3 P, int data)\n"
            + "  int  imageAtomicMax(gbufferImage    image, int   P, int data)\n"
            + "  int  imageAtomicMax(gimage1DArray   image, ivec2 P, int data)\n"
            + "  int  imageAtomicMax(gimage2DArray   image, ivec3 P, int data)\n"
            + "  int  imageAtomicMax(gimageCubeArray image, ivec3 P, int data)\n"
            + "  int  imageAtomicMax(gimage2DMS      image, ivec2 P, int sample, int data)\n"
            + "  int  imageAtomicMax(gimage2DMSArray image, ivec3 P, int sample, int data)",
            "shader.imageAtomicMin",
            "shader.imageAtomicMin -- atomically compute the minimum of a value with an existing\n"
            + "                  value in memory, store that value and return the original value\n"
            + "  uint imageAtomicMin(gimage1D        image, int   P, uint data)\n"
            + "  uint imageAtomicMin(gimage2D        image, ivec2 P, uint data)\n"
            + "  uint imageAtomicMin(gimage3D        image, ivec3 P, uint data)\n"
            + "  uint imageAtomicMin(gimage2DRect    image, ivec2 P, uint data)\n"
            + "  uint imageAtomicMin(gimageCube      image, ivec3 P, uint data)\n"
            + "  uint imageAtomicMin(gbufferImage    image, int   P, uint data)\n"
            + "  uint imageAtomicMin(gimage1DArray   image, ivec2 P, uint data)\n"
            + "  uint imageAtomicMin(gimage2DArray   image, ivec3 P, uint data)\n"
            + "  uint imageAtomicMin(gimageCubeArray image, ivec3 P, uint data)\n"
            + "  uint imageAtomicMin(gimage2DMS      image, ivec2 P, int sample, uint data)\n"
            + "  uint imageAtomicMin(gimage2DMSArray image, ivec3 P, int sample, uint data)\n"
            + "  int  imageAtomicMin(gimage1D        image, int   P, int data)\n"
            + "  int  imageAtomicMin(gimage2D        image, ivec2 P, int data)\n"
            + "  int  imageAtomicMin(gimage3D        image, ivec3 P, int data)\n"
            + "  int  imageAtomicMin(gimage2DRect    image, ivec2 P, int data)\n"
            + "  int  imageAtomicMin(gimageCube      image, ivec3 P, int data)\n"
            + "  int  imageAtomicMin(gbufferImage    image, int   P, int data)\n"
            + "  int  imageAtomicMin(gimage1DArray   image, ivec2 P, int data)\n"
            + "  int  imageAtomicMin(gimage2DArray   image, ivec3 P, int data)\n"
            + "  int  imageAtomicMin(gimageCubeArray image, ivec3 P, int data)\n"
            + "  int  imageAtomicMin(gimage2DMS      image, ivec2 P, int sample, int data)\n"
            + "  int  imageAtomicMin(gimage2DMSArray image, ivec3 P, int sample, int data)",
            "shader.imageAtomicOr",
            "shader.imageAtomicOr -- atomically compute the logical OR of a value with an existing\n"
            + "                 value in memory, store that value and return the original value\n"
            + "  uint imageAtomicOr(gimage1D        image, int   P, uint data)\n"
            + "  uint imageAtomicOr(gimage2D        image, ivec2 P, uint data)\n"
            + "  uint imageAtomicOr(gimage3D        image, ivec3 P, uint data)\n"
            + "  uint imageAtomicOr(gimage2DRect    image, ivec2 P, uint data)\n"
            + "  uint imageAtomicOr(gimageCube      image, ivec3 P, uint data)\n"
            + "  uint imageAtomicOr(gbufferImage    image, int   P, uint data)\n"
            + "  uint imageAtomicOr(gimage1DArray   image, ivec2 P, uint data)\n"
            + "  uint imageAtomicOr(gimage2DArray   image, ivec3 P, uint data)\n"
            + "  uint imageAtomicOr(gimageCubeArray image, ivec3 P, uint data)\n"
            + "  uint imageAtomicOr(gimage2DMS      image, ivec2 P, int sample, uint data)\n"
            + "  uint imageAtomicOr(gimage2DMSArray image, ivec3 P, int sample, uint data)\n"
            + "  int  imageAtomicOr(gimage1D        image, int   P, int data)\n"
            + "  int  imageAtomicOr(gimage2D        image, ivec2 P, int data)\n"
            + "  int  imageAtomicOr(gimage3D        image, ivec3 P, int data)\n"
            + "  int  imageAtomicOr(gimage2DRect    image, ivec2 P, int data)\n"
            + "  int  imageAtomicOr(gimageCube      image, ivec3 P, int data)\n"
            + "  int  imageAtomicOr(gbufferImage    image, int   P, int data)\n"
            + "  int  imageAtomicOr(gimage1DArray   image, ivec2 P, int data)\n"
            + "  int  imageAtomicOr(gimage2DArray   image, ivec3 P, int data)\n"
            + "  int  imageAtomicOr(gimageCubeArray image, ivec3 P, int data)\n"
            + "  int  imageAtomicOr(gimage2DMS      image, ivec2 P, int sample, int data)\n"
            + "  int  imageAtomicOr(gimage2DMSArray image, ivec3 P, int sample, int data)",
            "shader.imageAtomicXor",
            "shader.imageAtomicXor -- atomically compute the logical exclusive OR of a value with an existing\n"
            + "                  value in memory, store that value and return the original value\n"
            + "  uint imageAtomicXor(gimage1D        image, int   P, uint data)\n"
            + "  uint imageAtomicXor(gimage2D        image, ivec2 P, uint data)\n"
            + "  uint imageAtomicXor(gimage3D        image, ivec3 P, uint data)\n"
            + "  uint imageAtomicXor(gimage2DRect    image, ivec2 P, uint data)\n"
            + "  uint imageAtomicXor(gimageCube      image, ivec3 P, uint data)\n"
            + "  uint imageAtomicXor(gbufferImage    image, int   P, uint data)\n"
            + "  uint imageAtomicXor(gimage1DArray   image, ivec2 P, uint data)\n"
            + "  uint imageAtomicXor(gimage2DArray   image, ivec3 P, uint data)\n"
            + "  uint imageAtomicXor(gimageCubeArray image, ivec3 P, uint data)\n"
            + "  uint imageAtomicXor(gimage2DMS      image, ivec2 P, int sample, uint data)\n"
            + "  uint imageAtomicXor(gimage2DMSArray image, ivec3 P, int sample, uint data)\n"
            + "  int  imageAtomicXor(gimage1D        image, int   P, int data)\n"
            + "  int  imageAtomicXor(gimage2D        image, ivec2 P, int data)\n"
            + "  int  imageAtomicXor(gimage3D        image, ivec3 P, int data)\n"
            + "  int  imageAtomicXor(gimage2DRect    image, ivec2 P, int data)\n"
            + "  int  imageAtomicXor(gimageCube      image, ivec3 P, int data)\n"
            + "  int  imageAtomicXor(gbufferImage    image, int   P, int data)\n"
            + "  int  imageAtomicXor(gimage1DArray   image, ivec2 P, int data)\n"
            + "  int  imageAtomicXor(gimage2DArray   image, ivec3 P, int data)\n"
            + "  int  imageAtomicXor(gimageCubeArray image, ivec3 P, int data)\n"
            + "  int  imageAtomicXor(gimage2DMS      image, ivec2 P, int sample, int data)\n"
            + "  int  imageAtomicXor(gimage2DMSArray image, ivec3 P, int sample, int data)",
            "shader.imageLoad",
            "shader.imageLoad -- load a single texel from an image\n"
            + "  gvec4 imageLoad(gimage1D        image, int   P)\n"
            + "  gvec4 imageLoad(gimage2D        image, ivec2 P)\n"
            + "  gvec4 imageLoad(gimage3D        image, ivec3 P)\n"
            + "  gvec4 imageLoad(gimage2DRect    image, ivec2 P)\n"
            + "  gvec4 imageLoad(gimageCube      image, ivec3 P)\n"
            + "  gvec4 imageLoad(gbufferImage    image, int   P)\n"
            + "  gvec4 imageLoad(gimage1DArray   image, ivec2 P)\n"
            + "  gvec4 imageLoad(gimage2DArray   image, ivec3 P)\n"
            + "  gvec4 imageLoad(gimageCubeArray image, ivec3 P)\n"
            + "  gvec4 imageLoad(gimage2DMS      image, ivec2 P, int sample)\n"
            + "  gvec4 imageLoad(gimage2DMSArray image, ivec3 P, int sample)",
            "shader.imageSamples",
            "shader.imageSize",
            "shader.imageStore",
            "shader.imageStore -- write a single texel into an image\n"
            + "  void imageStore(gimage1D        image, int   P, gvec4 data)\n"
            + "  void imageStore(gimage2D        image, ivec2 P, gvec4 data)\n"
            + "  void imageStore(gimage3D        image, ivec3 P, gvec4 data)\n"
            + "  void imageStore(gimage2DRect    image, ivec2 P, gvec4 data)\n"
            + "  void imageStore(gimageCube      image, ivec3 P, gvec4 data)\n"
            + "  void imageStore(gbufferImage    image, int   P, gvec4 data)\n"
            + "  void imageStore(gimage1DArray   image, ivec2 P, gvec4 data)\n"
            + "  void imageStore(gimage2DArray   image, ivec3 P, gvec4 data)\n"
            + "  void imageStore(gimageCubeArray image, ivec3 P, gvec4 data)\n"
            + "  void imageStore(gimage2DMS      image, ivec2 P, int sample, gvec4 data)\n"
            + "  void imageStore(gimage2DMSArray image, ivec3 P, int sample, gvec4 data)",
            "shader.imulExtended",
            "shader.in",
            "shader.inout",
            "shader.intBitsToFloat",
            "shader.interpolateAtCentroid",
            "shader.interpolateAtOffset",
            "shader.interpolateAtSample",
            "shader.invariant",
            "shader.inverse",
            "shader.inversesqrt",
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
            "shader.isinf",
            "shader.isnan",
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
            "shader.ldexp",
            "shader.length",
            "shader.lessThan",
            "shader.lessThanEqual",
            "shader.log",
            "shader.log2",
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
            "shader.matrixCompMult",
            "shader.max",
            "shader.memoryBarrier",
            "shader.memoryBarrierAtomicCounter",
            "shader.memoryBarrierBuffer",
            "shader.memoryBarrierImage",
            "shader.memoryBarrierShared",
            "shader.min",
            "shader.mix",
            "shader.mod",
            "shader.modf",
            "shader.noise",
            "shader.noise1",
            "shader.noise2",
            "shader.noise3",
            "shader.noise4",
            "shader.noperspective",
            "shader.normalize",
            "shader.not",
            "shader.notEqual",
            "shader.out",
            "shader.outerProduct",
            "shader.packDouble2x32",
            "shader.packHalf2x16",
            "shader.packSnorm2x16",
            "shader.packSnorm4x8",
            "shader.packUnorm",
            "shader.packUnorm2x16",
            "shader.packUnorm4x8",
            "shader.patch",
            "shader.pow",
            "shader.precision",
            "shader.precision.highp",
            "shader.precision.lowp",
            "shader.precision.mediump",
            "shader.radians",
            "shader.reflect",
            "shader.refract",
            "shader.removedTypes",
            "shader.round",
            "shader.roundEven",
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
            "shader.sign",
            "shader.sin",
            "shader.sinh",
            "shader.smooth",
            "shader.smoothstep",
            "shader.sqrt",
            "shader.step",
            "shader.subroutine",
            "shader.tan",
            "shader.tanh",
            "shader.texelFetch",
            "shader.texelFetch -- perform a lookup of a single texel within a texture\n"
            + "  gvec4 texelFetch(gsampler1D        sampler, int   P, int lod)\n"
            + "  gvec4 texelFetch(gsampler2D        sampler, ivec2 P, int lod)\n"
            + "  gvec4 texelFetch(gsampler3D        sampler, ivec3 P, int lod)\n"
            + "  gvec4 texelFetch(gsampler2DRect    sampler, ivec2 P)\n"
            + "  gvec4 texelFetch(gsampler1DArray   sampler, ivec2 P, int lod)\n"
            + "  gvec4 texelFetch(gsampler2DArray   sampler, ivec3 P, int lod)\n"
            + "  gvec4 texelFetch(gsamplerBuffer    sampler, int   P)\n"
            + "  gvec4 texelFetch(gsampler2DMS      sampler, ivec2 P, sample sample)\n"
            + "  gvec4 texelFetch(gsampler2DMSArray sampler, ivec3 P, sample sample)",
            "shader.texelFetchOffset",
            "shader.texelFetchOffset -- perform a lookup of a single texel within a texture with an offset\n"
            + "  gvec4 texelFetchOffset(gsampler1D      sampler, int   P, int lod, int offset)\n"
            + "  gvec4 texelFetchOffset(gsampler2D      sampler, ivec2 P, int lod, int offset)\n"
            + "  gvec4 texelFetchOffset(gsampler3D      sampler, ivec3 P, int lod, int offset)\n"
            + "  gvec4 texelFetchOffset(gsampler2DRect  sampler, ivec2 P, int offset)\n"
            + "  gvec4 texelFetchOffset(gsampler1DArray sampler, ivec2 P, int lod, int offset)\n"
            + "  gvec4 texelFetchOffset(gsampler2DArray sampler, ivec3 P, int lod, int offset)",
            "shader.texture",
            "shader.texture -- retrieves texels from a texture\n"
            + "  gvec4 texture(gsampler1D              sampler, float P, [float bias])\n"
            + "  gvec4 texture(gsampler2D              sampler, vec2  P, [float bias])\n"
            + "  gvec4 texture(gsampler3D              sampler, vec3  P, [float bias])\n"
            + "  gvec4 texture(gsamplerCube            sampler, vec3  P, [float bias])\n"
            + "  float texture(sampler1DShadow         sampler, vec3  P, [float bias])\n"
            + "  float texture(sampler2DShadow         sampler, vec3  P, [float bias])\n"
            + "  float texture(samplerCubeShadow       sampler, vec3  P, [float bias])\n"
            + "  gvec4 texture(gsampler1DArray         sampler, vec2  P, [float bias])\n"
            + "  gvec4 texture(gsampler2DArray         sampler, vec3  P, [float bias])\n"
            + "  gvec4 texture(gsamplerCubeArray       sampler, vec4  P, [float bias])\n"
            + "  float texture(sampler1DArrayShadow    sampler, vec3  P, [float bias])\n"
            + "  float texture(gsampler2DArrayShadow   sampler, vec4  P, [float bias])\n"
            + "  gvec4 texture(gsampler2DRect          sampler, vec2  P)\n"
            + "  float texture(sampler2DRectShadow     sampler, vec3  P)\n"
            + "  float texture(gsamplerCubeArrayShadow sampler, vec4  P, float compare)",
            "shader.textureGather",
            "shader.textureGather -- gathers four texels from a texture\n"
            + "  gvec4 textureGather(gsampler2D              sampler, vec2 P, [int comp])\n"
            + "  gvec4 textureGather(gsampler2DArray         sampler, vec3 P, [int comp])\n"
            + "  gvec4 textureGather(gsamplerCube            sampler, vec3 P, [int comp])\n"
            + "  gvec4 textureGather(gsamplerCubeArray       sampler, vec4 P, [int comp])\n"
            + "  gvec4 textureGather(gsampler2DRect          sampler, vec3 P, [int comp])\n"
            + "  vec4  textureGather(gsampler2DShadow        sampler, vec2 P, float refZ)\n"
            + "  vec4  textureGather(gsampler2DArrayShadow   sampler, vec3 P, float refZ)\n"
            + "  vec4  textureGather(gsamplerCubeShadow      sampler, vec3 P, float refZ)\n"
            + "  vec4  textureGather(gsamplerCubeArrayShadow sampler, vec4 P, float refZ)\n"
            + "  vec4  textureGather(gsampler2DRectShadow    sampler, vec3 P, float refZ)",
            "shader.textureGatherOffset",
            "shader.textureGatherOffset -- gathers four texels from a texture with offset\n"
            + "  gvec4 textureGatherOffset(gsampler2D            sampler, vec2 P, ivec2 offset, [int comp])\n"
            + "  gvec4 textureGatherOffset(gsampler2DArray       sampler, vec3 P, ivec2 offset, [int comp])\n"
            + "  gvec4 textureGatherOffset(gsampler2DRect        sampler, vec3 P, ivec2 offset, [int comp])\n"
            + "  vec4  textureGatherOffset(gsampler2DShadow      sampler, vec2 P, float refZ, ivec2 offset)\n"
            + "  vec4  textureGatherOffset(gsampler2DArrayShadow sampler, vec3 P, float refZ, ivec2 offset)\n"
            + "  vec4  textureGatherOffset(gsampler2DRectShadow  sampler, vec3 P, float refZ, ivec2 offset)",
            "shader.textureGatherOffsets",
            "shader.textureGatherOffsets -- gathers four texels from a texture with an array of offsets\n"
            + "  gvec4 textureGatherOffsets(gsampler2D            sampler, vec2 P, ivec2 offsets[4], [int comp])\n"
            + "  gvec4 textureGatherOffsets(gsampler2DArray       sampler, vec3 P, ivec2 offsets[4], [int comp])\n"
            + "  gvec4 textureGatherOffsets(gsampler2DRect        sampler, vec3 P, ivec2 offsets[4], [int comp])\n"
            + "  vec4  textureGatherOffsets(gsampler2DShadow      sampler, vec2 P, float refZ, ivec2 offsets[4])\n"
            + "  vec4  textureGatherOffsets(gsampler2DArrayShadow sampler, vec3 P, float refZ, ivec2 offsets[4])\n"
            + "  vec4  textureGatherOffsets(gsampler2DRectShadow  sampler, vec3 P, float refZ, ivec2 offsets[4])",
            "shader.textureGrad",
            "shader.textureGrad -- perform a texture lookup with explicit gradients\n"
            + "  gvec4 textureGrad(gsampler1D           sampler, float P, float dPdx, float dPdy)\n"
            + "  gvec4 textureGrad(gsampler2D           sampler, vec2  P, vec2  dPdx, vec2  dPdy)\n"
            + "  gvec4 textureGrad(gsampler3D           sampler, vec3  P, vec3  dPdx, vec3  dPdy)\n"
            + "  gvec4 textureGrad(gsamplerCube         sampler, vec3  P, vec3  dPdx, vec3  dPdy)\n"
            + "  gvec4 textureGrad(gsampler2DRect       sampler, vec2  P, vec2  dPdx, vec2  dPdy)\n"
            + "  float textureGrad(gsampler2DRectShadow sampler, vec2  P, vec2  dPdx, vec2  dPdy)\n"
            + "  float textureGrad(sampler1DShadow      sampler, vec3  P, float dPdx, float dPdy)\n"
            + "  float textureGrad(sampler2DShadow      sampler, vec3  P, vec2  dPdx, vec2  dPdy)\n"
            + "  gvec4 textureGrad(gsampler1DArray      sampler, vec2  P, float dPdx, float dPdy)\n"
            + "  gvec4 textureGrad(gsampler2DArray      sampler, vec3  P, vec2  dPdx, vec2  dPdy)\n"
            + "  float textureGrad(sampler1DArrayShadow sampler, vec3  P, float dPdx, float dPdy)\n"
            + "  gvec4 textureGrad(gsamplerCubeArray    sampler, vec4  P, vec3  dPdx, vec3  dPdy)",
            "shader.textureLod",
            "shader.textureLod -- perform a texture lookup with explicit level-of-detail\n"
            + "  gvec4 textureLod(gsampler1D           sampler, float P, float lod)\n"
            + "  gvec4 textureLod(gsampler2D           sampler, vec2  P, float lod)\n"
            + "  gvec4 textureLod(gsampler3D           sampler, vec3  P, float lod)\n"
            + "  gvec4 textureLod(gsamplerCube         sampler, vec3  P, float lod)\n"
            + "  float textureLod(sampler1DShadow      sampler, vec3  P, float lod)\n"
            + "  float textureLod(sampler2DShadow      sampler, vec4  P, float lod)\n"
            + "  gvec4 textureLod(gsampler1DArray      sampler, vec2  P, float lod)\n"
            + "  gvec4 textureLod(gsampler2DArray      sampler, vec3  P, float lod)\n"
            + "  float textureLod(sampler1DArrayShadow sampler, vec3  P, float lod)\n"
            + "  gvec4 textureLod(gsamplerCubeArray    sampler, vec4  P, float lod)",
            "shader.textureOffset",
            "shader.textureOffset -- perform a texture lookup with offset\n"
            + "  gvec4 textureOffset(gsampler1D           sampler, float P, int   offset, [float bias])\n"
            + "  gvec4 textureOffset(gsampler2D           sampler, vec2  P, ivec2 offset, [float bias])\n"
            + "  gvec4 textureOffset(gsampler3D           sampler, vec3  P, ivec3 offset, [float bias])\n"
            + "  gvec4 textureOffset(gsampler2DRect       sampler, vec2  P, ivec2 offset)\n"
            + "  float textureOffset(sampler2DRectShadow  sampler, vec3  P, ivec2 offset)\n"
            + "  float textureOffset(sampler1DShadow      sampler, vec3  P, int   offset, [float bias])\n"
            + "  float textureOffset(sampler2DShadow      sampler, vec4  P, ivec2 offset, [float bias])\n"
            + "  gvec4 textureOffset(gsampler1DArray      sampler, vec2  P, int   offset, [float bias])\n"
            + "  gvec4 textureOffset(gsampler2DArray      sampler, vec3  P, ivec2 offset, [float bias])\n"
            + "  float textureOffset(sampler1DArrayShadow sampler, vec3  P, int   offset)\n"
            + "  float textureOffset(sampler2DArrayShadow sampler, vec4  P, vec2  offset)",
            "shader.textureProj",
            "shader.textureProj -- perform a texture lookup with projection\n"
            + "  vec4  textureProj(gsampler1D           sampler, vec2 P, [float bias])\n"
            + "  gvec4 textureProj(gsampler1D           sampler, vec4 P, [float bias])\n"
            + "  gvec4 textureProj(gsampler2D           sampler, vec3 P, [float bias])\n"
            + "  gvec4 textureProj(gsampler2D           sampler, vec4 P, [float bias])\n"
            + "  gvec4 textureProj(gsampler3D           sampler, vec4 P, [float bias])\n"
            + "  float textureProj(sampler1DShadow      sampler, vec4 P, [float bias])\n"
            + "  float textureProj(sampler2DShadow      sampler, vec4 P, [float bias])\n"
            + "  gvec4 textureProj(gsampler2DRect       sampler, vec3 P)\n"
            + "  gvec4 textureProj(gsampler2DRect       sampler, vec4 P)\n"
            + "  float textureProj(gsampler2DRectShadow sampler, vec4 P)",
            "shader.textureProjGrad​",
            "shader.textureProjGrad​ -- perform a texture lookup with projection and explicit gradients\n"
            + "  gvec4 textureProjGrad(gsampler1D           sampler, vec2 P, float pDx, float pDy)\n"
            + "  gvec4 textureProjGrad(gsampler1D           sampler, vec4 P, float pDx, float pDy)\n"
            + "  gvec4 textureProjGrad(gsampler2D           sampler, vec3 P, vec2  pDx, vec2  pDy)\n"
            + "  gvec4 textureProjGrad(gsampler2D           sampler, vec4 P, vec2  pDx, vec2  pDy)\n"
            + "  gvec4 textureProjGrad(gsampler3D           sampler, vec4 P, vec3  pDx, vec3  pDy)\n"
            + "  float textureProjGrad(sampler1DShadow      sampler, vec4 P, float pDx, float pDy)\n"
            + "  float textureProjGrad(sampler2DShadow      sampler, vec4 P, vec2  pDx, vec2  pDy)\n"
            + "  gvec4 textureProjGrad(gsampler2DRect       sampler, vec3 P, vec2  pDx, vec2  pDy)\n"
            + "  gvec4 textureProjGrad(gsampler2DRect       sampler, vec4 P, vec2  pDx, vec2  pDy)\n"
            + "  float textureProjGrad(gsampler2DRectShadow sampler, vec4 P, vec2  pDx, vec2  pDy)",
            "shader.textureProjGradOffset​",
            "shader.textureProjGradOffset​ -- perform a texture lookup with projection, explicit gradients and offset\n"
            + "  gvec4 textureProjGradOffset(gsampler1D           sampler, vec2 P, float dPdx, float dPdy, int   offset)\n"
            + "  gvec4 textureProjGradOffset(gsampler1D           sampler, vec4 P, float dPdx, float dPdy, int   offset)\n"
            + "  gvec4 textureProjGradOffset(gsampler2D           sampler, vec3 P, vec2  dPdx, vec2  dPdy, ivec2 offset)\n"
            + "  gvec4 textureProjGradOffset(gsampler2D           sampler, vec4 P, vec2  dPdx, vec2  dPdy, ivec2 offset)\n"
            + "  gvec4 textureProjGradOffset(gsampler3D           sampler, vec4 P, vec3  dPdx, vec3  dPdy, ivec3 offset)\n"
            + "  float textureProjGradOffset(sampler1DShadow      sampler, vec4 P, float dPdx, float dPdy, int   offset)\n"
            + "  float textureProjGradOffset(sampler2DShadow      sampler, vec4 P, vec2  dPdx, vec2  dPdy, ivec2 offset)\n"
            + "  gvec4 textureProjGradOffset(gsampler2DRect       sampler, vec3 P, vec2  dPdx, vec2  dPdy, ivec2 offset)\n"
            + "  gvec4 textureProjGradOffset(gsampler2DRect       sampler, vec4 P, vec2  dPdx, vec2  dPdy, ivec2 offset)\n"
            + "  float textureProjGradOffset(gsampler2DRectShadow sampler, vec4 P, vec2  dPdx, vec2  dPdy, ivec2 offset)",
            "shader.textureProjLod​",
            "shader.textureProjLod​ -- perform a texture lookup with projection and explicit level-of-detail\n"
            + "  vec4  textureProjLod(gsampler1D      sampler, vec2 P, float lod)\n"
            + "  gvec4 textureProjLod(gsampler1D      sampler, vec4 P, float lod)\n"
            + "  gvec4 textureProjLod(gsampler2D      sampler, vec3 P, float lod)\n"
            + "  gvec4 textureProjLod(gsampler2D      sampler, vec4 P, float lod)\n"
            + "  gvec4 textureProjLod(gsampler3D      sampler, vec4 P, float lod)\n"
            + "  float textureProjLod(sampler1DShadow sampler, vec4 P, float lod)\n"
            + "  float textureProjLod(sampler2DShadow sampler, vec4 P, float lod)",
            "shader.textureProjLodOffset​",
            "shader.textureProjLodOffset​ -- perform a texture lookup with projection and explicit level-of-detail and offset\n"
            + "  gvec4 textureProjLodOffset(gsampler1D      sampler, vec2 P, float lod, int   offset)\n"
            + "  gvec4 textureProjLodOffset(gsampler1D      sampler, vec4 P, float lod, int   offset)\n"
            + "  gvec4 textureProjLodOffset(gsampler2D      sampler, vec3 P, float lod, ivec2 offset)\n"
            + "  gvec4 textureProjLodOffset(gsampler2D      sampler, vec4 P, float lod, ivec2 offset)\n"
            + "  gvec4 textureProjLodOffset(gsampler3D      sampler, vec4 P, float lod, ivec3 offset)\n"
            + "  float textureProjLodOffset(sampler1DShadow sampler, vec4 P, float lod, int   offset)\n"
            + "  float textureProjLodOffset(sampler2DShadow sampler, vec4 P, float lod, ivec2 offset)",
            "shader.textureProjOffset​",
            "shader.textureProjOffset​ -- perform a texture lookup with projection and offset\n"
            + "  gvec4 textureProjOffset(gsampler1D           sampler, vec2 P, int   offset, [float bias])\n"
            + "  gvec4 textureProjOffset(gsampler1D           sampler, vec4 P, int   offset, [float bias])\n"
            + "  gvec4 textureProjOffset(gsampler2D           sampler, vec3 P, ivec2 offset, [float bias])\n"
            + "  gvec4 textureProjOffset(gsampler2D           sampler, vec4 P, ivec2 offset, [float bias])\n"
            + "  gvec4 textureProjOffset(gsampler3D           sampler, vec4 P, ivec3 offset, [float bias])\n"
            + "  float textureProjOffset(sampler1DShadow      sampler, vec4 P, int   offset, [float bias])\n"
            + "  float textureProjOffset(sampler2DShadow      sampler, vec4 P, ivec2 offset, [float bias])\n"
            + "  gvec4 textureProjOffset(gsampler2DRect       sampler, vec3 P, ivec2 offset)\n"
            + "  gvec4 textureProjOffset(gsampler2DRect       sampler, vec4 P, ivec2 offset)\n"
            + "  float textureProjOffset(gsampler2DRectShadow sampler, vec4 P, ivec2 offset)",
            "shader.textureQueryLevels",
            "shader.textureQueryLevels -- compute the number of accessible mipmap levels of a texture\n"
            + "  int textureQueryLevels(gsampler1D              sampler)\n"
            + "  int textureQueryLevels(gsampler2D              sampler)\n"
            + "  int textureQueryLevels(gsampler3D              sampler)\n"
            + "  int textureQueryLevels(gsamplerCube            sampler)\n"
            + "  int textureQueryLevels(gsampler1DArray         sampler)\n"
            + "  int textureQueryLevels(gsampler2DDArray        sampler)\n"
            + "  int textureQueryLevels(gsamplerCubeArray       sampler)\n"
            + "  int textureQueryLevels(gsampler1DShadow        sampler)\n"
            + "  int textureQueryLevels(gsampler2DShadow        sampler)\n"
            + "  int textureQueryLevels(gsamplerCubeShadow      sampler)\n"
            + "  int textureQueryLevels(gsampler1DArrayShadow   sampler)\n"
            + "  int textureQueryLevels(gsampler2DArrayShadow   sampler)\n"
            + "  int textureQueryLevels(gsamplerCubeArrayShadow sampler)",
            "shader.textureQueryLod",
            "shader.textureQueryLod -- compute the level-of-detail that would be used to sample from a texture\n"
            + "  vec2 textureQueryLod(gsampler1D              sampler, float P)\n"
            + "  vec2 textureQueryLod(gsampler2D              sampler, vec2  P)\n"
            + "  vec2 textureQueryLod(gsampler3D              sampler, vec3  P)\n"
            + "  vec2 textureQueryLod(gsamplerCube            sampler, vec3  P)\n"
            + "  vec2 textureQueryLod(gsampler1DArray         sampler, float P)\n"
            + "  vec2 textureQueryLod(gsampler2DDArray        sampler, vec2  P)\n"
            + "  vec2 textureQueryLod(gsamplerCubeArray       sampler, vec3  P)\n"
            + "  vec2 textureQueryLod(gsampler1DShadow        sampler, float P)\n"
            + "  vec2 textureQueryLod(gsampler2DShadow        sampler, vec2  P)\n"
            + "  vec2 textureQueryLod(gsamplerCubeShadow      sampler, vec3  P)\n"
            + "  vec2 textureQueryLod(gsampler1DArrayShadow   sampler, float P)\n"
            + "  vec2 textureQueryLod(gsampler2DArrayShadow   sampler, vec2  P)\n"
            + "  vec2 textureQueryLod(gsamplerCubeArrayShadow sampler, vec3  P)",
            "shader.textureSamples",
            "shader.textureSamples -- return the number of samples of a texture\n"
            + "  int textureSamples(gsampler2DMS      sampler)\n"
            + "  int textureSamples(gsampler2DMSArray sampler)",
            "shader.textureSize",
            "shader.textureSize -- retrieve the dimensions of a level of a texture\n"
            + "  int   textureSize(gsampler1D             sampler, int lod)\n"
            + "  ivec2 textureSize(gsampler2D             sampler, int lod)\n"
            + "  ivec3 textureSize(gsampler3D             sampler, int lod)\n"
            + "  ivec2 textureSize(gsamplerCube           sampler, int lod)\n"
            + "  int   textureSize(sampler1DShadow        sampler, int lod)\n"
            + "  ivec2 textureSize(sampler2DShadow        sampler, int lod)\n"
            + "  ivec2 textureSize(samplerCubeShadow      sampler, int lod)\n"
            + "  ivec3 textureSize(samplerCubeArray       sampler, int lod)\n"
            + "  ivec3 textureSize(samplerCubeArrayShadow sampler, int lod)\n"
            + "  ivec2 textureSize(gsamplerRect           sampler)\n"
            + "  ivec2 textureSize(gsamplerRectShadow     sampler)\n"
            + "  ivec2 textureSize(gsampler1DArray        sampler, int lod)\n"
            + "  ivec3 textureSize(gsampler2DArray        sampler, int lod)\n"
            + "  ivec2 textureSize(sampler1DArrayShadow   sampler, int lod)\n"
            + "  ivec3 textureSize(sampler2DArrayShadow   sampler, int lod)\n"
            + "  int   textureSize(gsamplerBuffer         sampler)\n"
            + "  ivec2 textureSize(gsampler2DMS           sampler)\n"
            + "  ivec3 textureSize(gsampler2DMSArray      sampler)",
            "shader.transpose",
            "shader.trunc",
            "shader.uaddCarry",
            "shader.uintBitsToFloat",
            "shader.umulExtended",
            "shader.uniform",
            "shader.unpackDouble2x32",
            "shader.unpackHalf2x16",
            "shader.unpackSnorm2x16",
            "shader.unpackSnorm4x8",
            "shader.unpackUnorm",
            "shader.unpackUnorm2x16",
            "shader.unpackUnorm4x8",
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
            "shader.usubBorrow",
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
