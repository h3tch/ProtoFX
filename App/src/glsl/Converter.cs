using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Reflection.BindingFlags;

namespace App.Glsl
{
    public class Converter
    {
        #region Regex

        private static readonly string[] InvalidVariableNames;

        private static class Pattern
        {
            public static readonly string Word = @"\b[\w\d_]+\b";
            public static readonly string InOut = "(in|out)";
            public static readonly string ArrayBraces = MatchingBrace(@"\[",@"\]");
            public static readonly string FunctionBraces = MatchingBrace(@"\(",@"\)");
            public static readonly string BodyBraces = MatchingBrace(@"\{",@"\}");
            public static readonly string Array = $"{Word}\\s*{ArrayBraces}";
            public static readonly string WordOrArray = $"{Word}\\s*({ArrayBraces})?";
            public static readonly string Layout = $"\\blayout\\s*{FunctionBraces}";
            public static readonly string InOutLayout = $"\\blayout\\s*\\(.*\\)\\s*{InOut}\\s*;";
            public static readonly string Const = @"\bconst\s+\w+\s+[\w\d]+\s*=\s*[\w\d.]+;";
            public static string MatchingBrace(string open, string close)
            {
                var oc = $"{open}{close}";
                return $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                    $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}";
            }
        }
        
        private static class RegEx
        {
            public static Regex Operator = new Regex(@"\s*(=|\*=|/=|\+=|\-=|\+\+|\-\-|\(|\))");
            public static Regex Variable = new Regex($"((\\+\\+|\\-\\-)\\s*)?(?<!\\.){Pattern.WordOrArray}(\\s*\\.\\s*{Pattern.WordOrArray})*(\\+\\+|\\-\\-)?", RegexOptions.RightToLeft);
            public static Regex Buffer = new Regex($"({Pattern.Layout}\\s*)?{Pattern.Word}\\s+{Pattern.Word}\\s*{Pattern.BodyBraces}\\s*{Pattern.Word}({Pattern.ArrayBraces})?;", RegexOptions.Singleline | RegexOptions.RightToLeft);
            public static Regex Layout = new Regex(Pattern.Layout, RegexOptions.RightToLeft);
            public static Regex Word = new Regex(Pattern.Word);
            public static Regex VariableDef = new Regex($"{Pattern.Word}\\s+{Pattern.WordOrArray};", RegexOptions.RightToLeft);
            public static Regex Float = new Regex(@"\b[0-9]*\.[0-9]+\b", RegexOptions.RightToLeft);
            public static Regex InVarying = new Regex(@"\bin\s+[\w\d]+\s+[\w\d]+\s*;", RegexOptions.RightToLeft);
            public static Regex ArrayBraces = new Regex($"{Pattern.ArrayBraces}\\s*;", RegexOptions.Singleline);
            public static Regex PreDefOut = new Regex(@"\bout\s+gl_PerVertex\s*\{.*};", RegexOptions.Singleline | RegexOptions.RightToLeft);
            public static Regex InOut = new Regex(Pattern.InOut, RegexOptions.RightToLeft);
            public static Regex InOutLayout = new Regex(Pattern.InOutLayout, RegexOptions.RightToLeft);
            public static Regex Const = new Regex(Pattern.Const, RegexOptions.RightToLeft);
            public static Regex FuncCall(string funcName) => new Regex($"\\b{funcName}\\s*{Pattern.FunctionBraces}", RegexOptions.RightToLeft);
        }

        #endregion

        static Converter()
        {
            // initialize the list of GLSL data types
            InvalidVariableNames = new[] {
                "return", "discard", "continue"
            };
        }

        /// <summary>
        /// Convert GLSL shader to C# code.
        /// </summary>
        /// <param name="text">GLSL shader.</param>
        /// <returns>Return C# code.</returns>
        public static string Shader2Class(ShaderType shaderType, string className, string text, List<int> indices)
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

            // add debug information
            text = DebugTrace(text, indices);

            // invoke all private static methods of this class to convert the body of the shader
            foreach (var method in typeof(Converter).GetMethods(NonPublic | Static))
                if (method.IsDefined(typeof(ExecuteMethod), false))
                    text = (string)method.Invoke(null, new object[] { text });

            // surround by C# class code block
            var code = "using System; "
                + "using App.Glsl.SamplerTypes; "
                + "namespace App.Glsl { "
                + $"class {className} : {shaderClass} {{ "
                + $"public {className}() : this(0) {{ }} "
                + $"public {className}(int l) : base(l) {{ }} "
                + $"{text}}}}}";
            return code;
        }

        /// <summary>
        /// Add debug trace methods.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string DebugTrace(string text, List<int> indices)
        {
            var debugFunctions = new[] { "texture", "texelFetch" };

            // for all functions in the text, find all variable
            // accesses and add the respective debug trace function
            var funcs = Compiler.RegexFunction.Matches(text);

            // process the string from the back to the front
            for (var i_func = funcs.Count - 1; i_func >= 0; i_func--)
            {
                // get the body of the function
                var func = funcs[i_func];
                var start = func.Value.IndexOf('{');
                var body = func.Value.Subrange(start, func.Value.LastIndexOf('}'));
                var offset = func.Index + start;
                var length = body.Length;

                // find all variable accesses in the function
                var variables = FindVariables(body).ToArray().GetEnumerator();
                // find all function calls in the function
                var functions = FindFunctionCalls(body, debugFunctions).ToArray().GetEnumerator();

                if (!variables.MoveNext())
                    variables = null;
                if (!functions.MoveNext())
                    functions = null;

                while (variables?.Current != null || functions?.Current != null)
                {
                    var variable = (Match)variables?.Current;
                    var function = (Match)functions?.Current;
                    if ((variable?.Index ?? int.MinValue) > (function?.Index ?? int.MinValue))
                    {
                        // variable
                        var varname = variable.Value.Trim();
                        var location = LocatoinCtr(text, indices, offset + variable.Index, varname.Length);
                        // add debug trace function
                        body = body
                            .Insert(variable.Index + varname.Length, $", \"{varname}\")")
                            .Insert(variable.Index, $"TraceVariable({location}, ");
                        if (!variables.MoveNext())
                            variables = null;
                    }
                    else if (function != null)
                    {
                        // function
                        var braceIdx = function.Value.IndexOf('(') + 1;
                        var location = LocatoinCtr(text, indices, offset + function.Index, function.Length);
                        // add debug trace function
                        body = body.Insert(function.Index + braceIdx, $"{location}, ");
                        if (!functions.MoveNext())
                            functions = null;
                    }
                }

                // replace old function body with the new debug code
                text = text.Remove(offset, length).Insert(offset, body);
            }

            return text;
        }
        
        #region Methods to Process Shaders

        /// <summary>
        /// Remove version definition.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Version(string text) => Regex.Replace(text, @"#version [0-9]{3}", string.Empty);

        /// <summary>
        /// Remove predefined built in output varyings.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string PredefinedOutputs(string text)
        {
            foreach (Match match in RegEx.PreDefOut.Matches(text))
            {
                var nNewLines = match.Value.Count(x => x == '\n');
                text = text.Remove(match.Index, match.Length)
                    .Insert(match.Index, new string('\n', nNewLines));
            }
            return text;
        }

        /// <summary>
        /// Convert type casts from GLSL to C#.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string TypeCasts(string text)
        {
            text = typecast(text, "bool");
            text = typecast(text, "int");
            text = typecast(text, "uint");
            text = typecast(text, "float");
            return typecast(text, "double");
        }

        /// <summary>
        /// Convert input and output shader layouts to C# friendly code.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string InOutLayouts(string text)
        {
            foreach (Match match in RegEx.InOutLayout.Matches(text))
            {
                // get in/out word
                var word = RegEx.InOut.Match(match.Value);
                // get word position in the text
                var idx = match.Index + word.Index;
                // convert to C# friendly code
                text = text.Insert(idx + word.Length, "__").Insert(idx, "object __");
            }
            return text;
        }

        /// <summary>
        /// Convert GLSL constants to C# friendly code.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Constants(string text)
        {
            foreach (Match match in RegEx.Const.Matches(text))
            {
                // convert constant to C# property
                var index = text.IndexOf('=', match.Index, match.Length);
                text = text
                    .Remove(match.Index + match.Length - 1, 1)
                    .Insert(match.Index + match.Length - 1, "; } }")
                    .Remove(index, 1)
                    .Insert(index, "{ get { return ");
            }

            return Regex.Replace(text, @"\bconst\b", string.Empty);
        }

        /// <summary>
        /// Convert GLSL buffer to C# class.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
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
                var braces = RegEx.ArrayBraces.Match(sub, end);
                var clazz = bufName.Value + (braces.Success ? "[]" : string.Empty);
                
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

        /// <summary>
        /// Convert layout qualifier to class attribute.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Layouts(string text)
        {
            foreach (Match match in RegEx.Layout.Matches(text))
                text = text.Insert(match.Index + match.Length, "]").Insert(match.Index, "[__");
            return text;
        }

        /// <summary>
        /// Convert GLSL array to C# array.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
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
                var braces = RegEx.ArrayBraces.Match(text, match.Index, match.Length);
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

        /// <summary>
        /// Convert uniform keyword to C# attributes.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Uniforms(string text) => Regex.Replace(text, @"\buniform\b", "[__uniform]");

        /// <summary>
        /// Replace discard keyword with return keyword.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Discard(string text) => Regex.Replace(text, @"\bdiscard\b", "return");

        /// <summary>
        /// Convert GLSL floating point number to C# float.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Floats(string text)
        {
            foreach (Match match in RegEx.Float.Matches(text))
                text = text.Insert(match.Index + match.Length, "f");
            return text;
        }

        /// <summary>
        /// Convert GLSL input stream varyings layout to C# attribute.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
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

        /// <summary>
        /// Convert GLSL output stream varyings layout to C# attribute.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Outputs(string text) => Regex.Replace(text, @"\bout\b", "[__out]");

        /// <summary>
        /// Convert GLSL flat qualifier to C# attribute.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Flat(string text) => Regex.Replace(text, @"\bflat\b", "[__flat]");

        /// <summary>
        /// Convert GLSL smooth qualifier to C# attribute.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string Smooth(string text) => Regex.Replace(text, @"\bsmooth\b", "[__smooth]");

        /// <summary>
        /// Convert GLSL main function to C# method.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [ExecuteMethod]
        private static string MainFunc(string text) => Regex.Replace(text, @"\bvoid\s+main\b", "public override void main");

        #endregion

        #region Helpers

        /// <summary>
        /// Create Location structure constructor string.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="indices"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string LocatoinCtr(string text, List<int> indices, int index, int length)
        {
            var id = text.LastIndexOf('\n', index);
            var line = text.LineFromPosition(index);
            var column = id >= 0 ? indices[index] - indices[id] - 1 : 0;
            return $"new Location({line}, {column}, {length})";
        }

        /// <summary>
        /// Create typecast delegate function.
        /// </summary>
        private static Func<string, string, string> typecast = delegate(string text, string type)
        {
            var match = Regex.Matches(text, @"\b" + type + @"\(.*\)");
            for (int i = match.Count - 1; i >= 0; i--)
                text = text.Insert(match[i].Index + type.Length, ")").Insert(match[i].Index, "(");
            return text;
        };

        /// <summary>
        /// Find all variable accesses.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static IEnumerable<Match> FindVariables(string text)
        {
            double tmp;
            text += '\0';

            // for each accessed variable
            foreach (Match variable in RegEx.Variable.Matches(text))
            {
                // do not trace numbers
                if (double.TryParse(variable.Value, out tmp))
                    continue;

                // get preceding and next char
                var name = variable.Value.Trim();
                int preci = text.NextNonWhitespace(variable.Index - 1, -1);
                var prec = preci < 0 ? (char)0 : text[preci];
                int nexti = text.NextNonWhitespace(variable.Index + variable.Length);
                var next = nexti < 0 ? "\0\0" : text.Substring(nexti, 2);

                // If there is a space between two words this is a
                // variable definition which we do not want to trace.
                if (char.IsLetterOrDigit(prec) || char.IsLetterOrDigit(next[0]) ||
                    // is a function name
                    next[0] == '(' ||
                    // is an invalid operator
                    (next[0] == '=' && next[1] != '=') || (next[1] == '=' && next[0] != '=') ||
                    // is an invalid name
                    InvalidVariableNames.Any(x => x == name))
                    // do not return this variable
                    continue;

                yield return variable;
            }
        }

        /// <summary>
        /// Find all function calls.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="funcNames"></param>
        /// <returns></returns>
        private static IEnumerable<Match> FindFunctionCalls(string text, string[] funcNames)
            => SortMatches(funcNames.Select(x => RegEx.FuncCall(x).Matches(text).ToArray()), true);

        /// <summary>
        /// Sort regex matches.
        /// </summary>
        /// <param name="collections"></param>
        /// <returns></returns>
        private static IEnumerable<Match> SortMatches(IEnumerable<IEnumerable<Match>> collections, bool rightToLeft = false)
        {
            // convert match collection to arrays
            var matches = (from x in collections where x.Count() > 0 select x.ToArray()).ToArray();
            var i = new int[matches.Length];
            var count = matches.Select(x => x.Length).ToArray();

            // helper to access the matches
            Func<int, Match> match = delegate (int a) { return matches[a][i[a]]; };

            // while there is any match left
            for (int best = 0; (best = i.Zip(count, (x, y) => x < y).IndexOf(x => x)) >= 0; i[best]++)
            {
                // for all match collections find the next best match
                for (int j = 0; j < matches.Length; j++)
                    if (i[j] < count[j] && (match(best).Index < match(j).Index) == rightToLeft)
                        best = j;
                yield return match(best);
            }
        }

        /// <summary>
        /// Marks a method to be executed by the GLSL to C# converter.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method)]
        private class ExecuteMethod : Attribute { }

        #endregion
    }
}