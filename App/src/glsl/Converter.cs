using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace App.Glsl
{
    public class Converter
    {
        public static string Process(string text)
        {
            var methods = typeof(Converter).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var method in methods)
                text = (string)method.Invoke(null, new[] { text });

            return text;
        }

        private static string Version(string text) => Regex.Replace(text, "#version [0-9]{3}", "");

        private static string Floats(string text)
        {
            var regex = @"\b[0-9]*\.[0-9]+\b";

            for (Match match = Regex.Match(text, regex); match.Success; match = Regex.Match(text, regex))
                text = text.Insert(match.Index + match.Length, "f");

            return text;
        }

        private static string TypeCasts(string text)
        {
            Action<string> replace = delegate(string t)
            {
                var regex = new Regex(@"\b" + t + @"\(.*\)");

                for (var match = regex.Match(text); match.Success;)
                {
                    text = text.Insert(match.Index + t.Length, ")");
                    text = text.Insert(match.Index, "(");
                    match = regex.Match(text, match.Index + match.Length + 2);
                }
            };

            replace("bool");
            replace("int");
            replace("uint");
            replace("float");
            replace("double");

            return text;
        }

        private static string Layouts(string text) => Regex.Replace(text, @"\blayout\s*\(.*\)", "");

        private static string Constants(string text)
        {
            var regex = @"\bconst\s+\w+\s+[\w\d]+\s*=\s*[\w\d.]+;";

            for (Match match = Regex.Match(text, regex); match.Success; match = Regex.Match(text, regex))
            {
                var index = text.IndexOf('=', match.Index);
                text = text.Insert(index + 1, ">");
            }

            return Regex.Replace(text, @"\bconst\b", "");
        }

        private static string UniformBuffers(string text)
        {
            var buffer = new Regex(@"\buniform[\s\w\d]+\{[\s\w\d\[\];]*\}[\s\w\d]*;");
            var variable = new Regex(@"\b[\w\d]+\s+[\w\d\[\]]+;");
            var vararray = new Regex(@"\[.*\]");
            const string uniform = "uniform";
            const string clazz = "class";
            const string Public = "public ";

            for (var match = buffer.Match(text); match.Success;)
            {
                var sub = match.Value;

                // replace 'uniform' with 'struct'
                sub = sub.Remove(0, uniform.Length).Insert(0, clazz);

                // insert struct name before instance name
                var idx = sub.IndexOf('{');
                var end = sub.IndexOf('}', idx);
                var name = sub.Word(sub.IndexOfWord(idx, -1)) + ' ';
                sub = sub.Insert(end + 1, name);

                // insert 'public' keyword before variable names
                for (var varMatch = variable.Match(sub, idx); varMatch.Success && varMatch.Index < end;)
                {
                    var arrayMatch = vararray.Match(sub, varMatch.Index, varMatch.Length);
                    if (arrayMatch.Success)
                    {
                        var type = sub.Word(varMatch.Index);
                        var New = $"= new {type}";
                        sub = sub.Insert(arrayMatch.Index, New);
                        sub = sub.Insert(varMatch.Index + type.Length, "[]");
                        end += New.Length + 2;
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

        private static string Uniforms(string text) => Regex.Replace(text, @"\buniform\b", "");

        private static string Inputs(string text) => Regex.Replace(text, @"\bin\b", "");

        private static string Outputs(string text) => Regex.Replace(text, @"\bout\b", "");
    }
}