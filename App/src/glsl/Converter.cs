using System;
using System.Text.RegularExpressions;
using static System.Reflection.BindingFlags;

namespace App.Glsl
{
    public class Converter
    {
        #region Regex

        private static Regex buffer = new Regex(@"\buniform[\s\w\d]+\{[\s\w\d\[\];]*\}[\s\w\d]*;");
        private static Regex variable = new Regex(@"\b[\w\d]+\s+[\w\d\[\]]+;");
        private static Regex vararray = new Regex(@"\[.*\]");
        private static Regex version = new Regex(@"#version [0-9]{3}");
        private static Regex layout = new Regex(@"\blayout\s*\(.*\)");
        private static Regex number = new Regex(@"\b[0-9]*\.[0-9]+\b");
        private static Regex uniform = new Regex(@"\buniform\b");
        private static Regex IN = new Regex(@"\bin\s+[\w\d]+\s+[\w\d]+\s*;");
        private static Regex OUT = new Regex(@"\bout\b");
        private static Regex main = new Regex(@"\bvoid\s+main\b");
        private static Regex word = new Regex(@"\b[\w\d]+\b");
        private static Func<string, string, string> typecast = delegate(string text, string type)
        {
            var match = Regex.Matches(text, @"\b" + type + @"\(.*\)");
            for (int i = match.Count - 1; i >= 0; i--)
                text = text.Insert(match[i].Index + type.Length, ")").Insert(match[i].Index, "(");
            return text;
        };

        #endregion

        /// <summary>
        /// Convert GLSL shader to C# code.
        /// </summary>
        /// <param name="text">GLSL shader.</param>
        /// <returns>Return C# code.</returns>
        public static string Shader2Csharp(string text)
        {
            foreach (var method in typeof(Converter).GetMethods(NonPublic | Static))
                text = (string)method.Invoke(null, new[] { text });
            return text;
        }

        #region Methods to Process Shaders

        private static string Version(string text) => version.Replace(text, string.Empty);

        private static string Floats(string text)
        {
            var match = number.Matches(text);
            for (int i = match.Count - 1; i >= 0; i--)
                text = text.Insert(match[i].Index + match[i].Length, "f");
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

        private static string Layouts(string text)
        {
            for (Match match = layout.Match(text); match.Success; match = layout.Match(text))
                text = text.Insert(match.Index + match.Length, "]").Insert(match.Index, "[__");
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

        private static string UniformBuffers(string text)
        {
            const string uniform = "uniform";
            const string clazz = "class";
            const string Public = "public ";

            for (var match = buffer.Match(text); match.Success;)
            {
                var sub = match.Value;
                var idx = sub.IndexOf('{');
                var name = sub.Word(sub.IndexOfWord(idx, -1));

                // replace 'uniform' with 'struct'
                sub = sub
                    .Insert(sub.Length - 1, $" = new {name}()")
                    .Remove(0, uniform.Length)
                    .Insert(0, clazz);

                // insert struct name before instance name
                var end = sub.IndexOf('}', idx);
                sub = sub.Insert(end + 1, name + ' ');

                // insert 'public' keyword before variable names
                var varMatches = variable.Matches(sub.Substring(0, end), idx);
                for (int i = varMatches.Count - 1; i >= 0; i--)
                {
                    var varMatch = varMatches[i];
                    var words = word.Matches(varMatch.Value);
                    var type = words[0].Value;
                    var field = words[1].Value;

                    var arrayMatch = vararray.Match(sub, varMatch.Index, varMatch.Length);
                    var arrayType = type + (arrayMatch.Success ? "[]" : string.Empty);

                    sub = sub
                        .Remove(varMatch.Index + varMatch.Length - 1, 1)
                        .Insert(varMatch.Index + varMatch.Length - 1,
                            $"{{ get {{ return ({arrayType})GetUniform<{arrayType}>(\"{name}.{field}\"); }} }}");

                    if (arrayMatch.Success)
                    {
                        sub = sub
                            .Remove(arrayMatch.Index, arrayMatch.Length)
                            .Insert(varMatch.Index + type.Length, "[]");
                    }

                    sub = sub.Insert(varMatch.Index, Public);

                    end += Public.Length;
                    varMatch = variable.Match(sub, varMatch.Index + varMatch.Length + Public.Length);
                }

                // commit changes to text
                text = text.Remove(match.Index, match.Length).Insert(match.Index, sub);
                match = buffer.Match(text, match.Index + sub.Length);
            }
            
            return text;
        }

        private static string Uniforms(string text) => uniform.Replace(text, string.Empty);

        private static string Discard(string text) => Regex.Replace(text, @"\bdiscard\b", "return");

        private static string Inputs(string text)
        {
            var matches = IN.Matches(text);
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var words = word.Matches(match.Value);
                var type = words[1].Value;
                var name = words[2].Value;
                text = text.Remove(match.Index + match.Length - 1, 1).Insert(
                    match.Index + match.Length - 1,
                    $" {{ get {{ return GetInputVarying<{type}>(\"{name}\"); }} }}");
                text = text.Insert(match.Index + 2, "]").Insert(match.Index, "[__");
            }
            return text;
        }

        private static string Outputs(string text) => OUT.Replace(text, "[__out]");

        private static string MainFunc(string text) => main.Replace(text, "public override void main");

        #endregion
    }
}