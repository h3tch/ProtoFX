using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Reflection.BindingFlags;

namespace App.Glsl
{
    public class Converter
    {
        #region Regex

        private static class Pattern
        {
            public static readonly string Word = @"\b[\w\d_]+\b";
            public static readonly string ArrayBraces = Helpers.RegexMatchingBrace(@"\[",@"\]");
            public static readonly string FunctionBraces = Helpers.RegexMatchingBrace(@"\(",@"\)");
            public static readonly string BodyBraces = Helpers.RegexMatchingBrace(@"\{",@"\}");
            public static readonly string Array = $"{Word}\\s*{ArrayBraces}";
            public static readonly string WordOrArray = $"{Word}\\s*({ArrayBraces})?";
            public static readonly string Layout = $"\\blayout\\s*{FunctionBraces}";
        }
        
        private static class RegEx
        {
            public static Regex Operation = new Regex(@"\s*(=|\*=|/=|\+=|\-=|\+\+|\-\-|\(|\))");
            public static Regex Variable = new Regex($"(?<!\\.){Pattern.WordOrArray}(\\s*\\.\\s*{Pattern.WordOrArray})*", RegexOptions.RightToLeft);
            public static Regex Buffer = new Regex($"({Pattern.Layout}\\s*)?{Pattern.Word}\\s+{Pattern.Word}\\s*\\{{.*?\\}}\\s*{Pattern.Word}(\\[.*?\\])?;", RegexOptions.Singleline | RegexOptions.RightToLeft);
            public static Regex Layout = new Regex(Pattern.Layout);
            public static Regex Word = new Regex(Pattern.Word);
            public static Regex VariableDef = new Regex($"{Pattern.Word}\\s+{Pattern.WordOrArray};", RegexOptions.RightToLeft);
            public static Regex Float = new Regex(@"\b[0-9]*\.[0-9]+\b", RegexOptions.RightToLeft);
            public static Regex InVarying = new Regex(@"\bin\s+[\w\d]+\s+[\w\d]+\s*;", RegexOptions.RightToLeft);
            //public static Regex OutBuffer = new Regex($"\\bout\\s+{Pattern.Word}\\s*{Pattern.BodyBraces}\\s*({Word})?;");
            //public static Regex InBuffer = new Regex($"\\bin\\s+{Pattern.Word}\\s*{Pattern.BodyBraces}\\s*({Word})?;");
        }
        
        private static readonly string[] DataTypes;
        private static readonly string[] SamplerTypes;
        //private static Regex outBuffer = new Regex(@"\bout\s+[\w\d_]+\{[\s\w\d\[\];]*\}[\s\w\d]*;");
        //private static Regex inBuffer = new Regex(@"\bin\s+[\w\d_]+\{[\s\w\d\[\];]*\}[\s\w\d]*;");
        //private static Regex variable = new Regex(@"\b[\w\d_]+\s+[\w\d\[\]_]+;");
        private static Regex rexArrayBraces = new Regex($"{Pattern.ArrayBraces}\\s*;", RegexOptions.Singleline);
        //private static Regex number = new Regex(@"\b[0-9]*\.[0-9]+\b");
        private static Regex PreDefOut = new Regex(@"\bout\s+gl_PerVertex\s*\{.*};", RegexOptions.Singleline);
        private static Func<string, string, string> typecast = delegate(string text, string type)
        {
            var match = Regex.Matches(text, @"\b" + type + @"\(.*\)");
            for (int i = match.Count - 1; i >= 0; i--)
                text = text.Insert(match[i].Index + type.Length, ")").Insert(match[i].Index, "(");
            return text;
        };

        #endregion

        static Converter()
        {
            // initialize the list of GLSL data types
            var datatypes = new[] {
                "bool", "int", "uint", "float", "double",
                "bvec2", "ivec2", "uvec2", "vec2", "dvec2",
                "bvec3", "ivec3", "uvec3", "vec3", "dvec3",
                "bvec4", "ivec4", "uvec4", "vec4", "dvec4",
                "mat2", "dmat2", "mat3", "dmat3", "mat4", "dmat4",
                "return", "continue", "new", "."
            };
            DataTypes = datatypes.Concat(datatypes.Select(x => x + "[]")).ToArray();

            // initialize the list of GLSL sampler types
            SamplerTypes = new[]
            {
                "sampler1D", "isampler1D", "usampler1D", "sampler2D", "isampler2D",
                "usampler2D", "sampler3D", "isampler3D", "usampler3D", "samplerBuffer",
                "isamplerBuffer", "usamplerBuffer", "samplerCube", "isamplerCube",
                "usamplerCube", "sampler1DShadow", "sampler2DShadow", "samplerCubeShadow",
                "sampler1DArray", "isampler1DArray", "usampler1DArray", "sampler2DArray",
                "usampler2DArray", "isampler2DArray", "samplerCubeArray", "usamplerCubeArray",
                "isamplerCubeArray", "sampler1DArrayShadow", "sampler2DArrayShadow",
                "usampler2DArrayShadow", "isampler2DArrayShadow", "sampler2DRect",
                "isampler2DRect", "usampler2DRect", "sampler2DRectShadow",
                "samplerCubeArrayShadow", "isamplerCubeArrayShadow", "usamplerCubeArrayShadow",
                "sampler2DMS", "isampler2DMS", "usampler2DMS", "sampler2DMSArray",
                "isampler2DMSArray", "usampler2DMSArray",
            };
        }

        /// <summary>
        /// Convert GLSL shader to C# code.
        /// </summary>
        /// <param name="body">GLSL shader.</param>
        /// <returns>Return C# code.</returns>
        public static string Shader2Class(ShaderType shaderType, string className, string body)
        {
            // get shader class name from shader type
            string shaderClass;
            switch (shaderType)
            {
                case ShaderType.VertexShader: shaderClass = typeof(VertShader).Name; break;
                case ShaderType.TessControlShader: shaderClass = typeof(TessShader).Name; break;
                case ShaderType.TessEvaluationShader: shaderClass = typeof(EvalShader).Name; break;
                case ShaderType.GeometryShader: shaderClass = typeof(GeomShader).Name; break;
                case ShaderType.FragmentShader: shaderClass = typeof(FragShader).Name; break;
                case ShaderType.ComputeShader: shaderClass = typeof(CompShader).Name; break;
                default: return null;
            }

            // invoke all private static methods of this class to convert the body of the shader
            foreach (var method in typeof(Converter).GetMethods(NonPublic | Static))
                body = (string)method.Invoke(null, new[] { body });

            // surround by C# class code block
            var code = "using System; using App.Glsl.SamplerTypes; "
                + "namespace App.Glsl { "
                + $"class {className} : {shaderClass} {{ "
                + $"public {className}() : this(0) {{ }} "
                + $"public {className}(int l) : base(l) {{ }} "
                + $"{body}}}}}";
            return code;
        }

        public static IEnumerable<Match> FindVariables(string text)
        {
            double tmp;

            foreach (Match variable in RegEx.Variable.Matches(text))
            {
                var varname = variable.Value.Trim();
                var vartype = text.Word(text.IndexOfWord(variable.Index, -1));
                var invalidChar = RegEx.Operation.Match(text, variable.Index + variable.Length);
                if (DataTypes.Any(x => x == varname || x == vartype)
                    || double.TryParse(varname, out tmp)
                    || (invalidChar.Success && invalidChar.Index == variable.Index + variable.Length))
                    continue;
                yield return variable;
            }
        }

        #region Methods to Process Shaders

        private static string Version(string text) => Regex.Replace(text, @"#version [0-9]{3}", string.Empty);

        private static string PredefinedOutputs(string text)
        {
            var matches = PreDefOut.Matches(text);
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var nNewLines = match.Value.Count(x => x == '\n');
                text = text.Remove(match.Index, match.Length)
                    .Insert(match.Index, new string('\n', nNewLines));
            }
            return text;
        }

        private static string TypeCasts(string text)
        {
            text = typecast(text, "bool");
            text = typecast(text, "int");
            text = typecast(text, "uint");
            text = typecast(text, "float");
            text = typecast(text, "double");
            return text;
        }

        private static string InOutLayouts(string text)
        {
            foreach (var q in new[] { "in", "out" })
            {
                var matches = Regex.Matches(text, @"\blayout\s*\(.*\)\s+" + q + @"\s*;");
                for (int i = matches.Count - 1; i >= 0; i--)
                {
                    Match match = matches[i];
                    var replacement = Regex.Replace(match.Value, $"\\b{q}\\b", $"object __{q}__");
                    text = text.Remove(match.Index, match.Length).Insert(match.Index, replacement);
                }
            }
            return text;
        }

        private static string Constants(string text)
        {
            const string regex = @"\bconst\s+\w+\s+[\w\d]+\s*=\s*[\w\d.]+;";

            for (Match match = Regex.Match(text, regex); match.Success; match = Regex.Match(text, regex))
            {
                var index = text.IndexOf('=', match.Index);
                text = text
                    .Remove(match.Index + match.Length - 1, 1)
                    .Insert(match.Index + match.Length - 1, "; } }")
                    .Remove(index, 1)
                    .Insert(index, "{ get { return ");
            }

            return Regex.Replace(text, @"\bconst\b", string.Empty);
        }

        private static string Buffers(string text)
        {
            // process buffers
            foreach (Match bufMatch in RegEx.Buffer.Matches(text))
            {
                // get buffer definitions
                var sub = bufMatch.Value;
                var idx = sub.IndexOf('{');
                var end = sub.IndexOf('}', idx);
                var bufLayout = RegEx.Layout.Match(sub);
                var bufDef = RegEx.Word.Matches(sub, bufLayout.Index + bufLayout.Length);
                var bufType = bufDef[0];
                var bufName = bufDef[1];

                // convert GLSL type to C# class and constructor
                var braces = rexArrayBraces.Match(sub, end);
                string clazz;
                if (braces.Success)
                {
                    //var array = braces.Value;
                    //var value = array.Subrange(array.IndexOf('[') + 1, array.IndexOf(']')).Trim();
                    clazz = $"{bufName.Value}[]";
                }
                else
                {
                    clazz = $"{bufName.Value}";
                }
                
                // remove GLSL array definition
                if (braces.Success)
                    sub = sub.Remove(braces.Index, braces.Length - 1);
                // add class name before instance name
                sub = sub.Insert(end + 1, $"[__{bufLayout.Value}] [__{bufType}] {clazz} ");

                // process variable names
                foreach (Match varMatch in RegEx.VariableDef.Matches(sub.Substring(idx, end - idx)))
                    sub = sub.Insert(idx + varMatch.Index, "public ");

                // replace type with class keyword but keep in/out attributes
                sub = sub.Insert(bufType.Index + bufType.Length, "class");
                sub = sub.Remove(bufType.Index, bufType.Length);
                sub = sub.Remove(bufLayout.Index, bufLayout.Length);

                // commit changes to text
                text = text.Remove(bufMatch.Index, bufMatch.Length).Insert(bufMatch.Index, sub);
            }
            return text;
        }

        private static string Layouts(string text)
        {
            for (Match match = RegEx.Layout.Match(text); match.Success; match = RegEx.Layout.Match(text))
                text = text.Insert(match.Index + match.Length, "]").Insert(match.Index, "[__");
            return text;
        }

        private static string Arrays(string text)
        {
            // type name [ . ] ;
            // type[] name = new type[ . ];
            var matches = Regex.Matches(text, $"{Pattern.Word}\\s+{Pattern.Array}\\s*;");
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                // get variable definitions
                var match = matches[i];
                var Def = RegEx.Word.Matches(match.Value);
                var Type = Def[0].Value;
                if (Type == "new")
                    continue;
                var Name = Def[1].Value;
                var braces = rexArrayBraces.Match(text, match.Index, match.Length);
                text = text
                    // replace array braces with return string
                    .Remove(braces.Index, braces.Length)
                    .Insert(braces.Index, $" = new {Type}{braces.Value}")
                    // add array braces to the class type
                    .Insert(match.Index + Type.Length, "[]");
            }

            // type name [ . ] = type [ . ] ( type ( word , word ) , ...);
            // type[] name = new type[] { new type { first = word, second = word }, ... };

            // type name [ . ] = { { word, word }, ... };
            // type[] name = new type[] { new type { first = word, second = word }, ... };

            return text;
        }

        private static string Uniforms(string text) => Regex.Replace(text, @"\buniform\b", "[__uniform]");
        
        private static string Discard(string text) => Regex.Replace(text, @"\bdiscard\b", "return");

        private static string DebugTrace(string text)
        {
            // for all functions in the text, find all variable
            // accesses and add the respective debug trace function
            var funcs = Compiler.RegexFunction.Matches(text);

            // process the string from the back to the front
            for (var i_func = funcs.Count - 1; i_func >= 0; i_func--)
            {
                // get the body of the function
                var func = funcs[i_func];
                var start = func.Value.IndexOf('{');
                var body = func.Value.Substring(start);
                var length = body.Length;

                // find all variable accesses in the function
                foreach (Match variable in FindVariables(body))
                {
                    // add debug trace function
                    var varname = variable.Value.Trim();
                    var column = variable.Index - body.LastIndexOf('\n', variable.Index) + 1;
                    body = body
                        .Insert(variable.Index + varname.Length, $", \"{varname}\")")
                        .Insert(variable.Index, $"TraceVariable({column}, {varname.Length}, ");
                }
                
                // replace old function body with the new debug code
                text = text.Remove(func.Index + start, length).Insert(func.Index + start, body);
            }

            return text;
        }

        private static string Floats(string text)
        {
            foreach (Match match in RegEx.Float.Matches(text))
                text = text.Insert(match.Index + match.Length, "f");
            return text;
        }

        private static string Inputs(string text)
        {
            foreach (Match match in RegEx.InVarying.Matches(text))
            {
                var words = RegEx.Word.Matches(match.Value);
                var type = words[1].Value;
                var name = words[2].Value;
                text = text.Insert(match.Index + 2, "]").Insert(match.Index, "[__");
            }
            return text;
        }

        private static string Outputs(string text) => Regex.Replace(text, @"\bout\b", "[__out]");

        private static string Flat(string text) => Regex.Replace(text, @"\bflat\b", "[__flat]");

        private static string Smooth(string text) => Regex.Replace(text, @"\bsmooth\b", "[__smooth]");

        private static string MainFunc(string text) => Regex.Replace(text, @"\bvoid\s+main\b", "public override void main");

        #endregion

        static class Helpers
        {
            public static string RegexMatchingBrace(string open, string close)
            {
                var oc = $"{open}{close}";
                return  $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                    $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}";
            }
        }
    }
}