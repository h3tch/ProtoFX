using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace gled
{
    abstract class GLObject
    {
        public int glname { get; protected set; }
        public string name { get; protected set; }
        public string anno { get; protected set; }
        private static CultureInfo culture = new CultureInfo("en");

        public GLObject(string name, string annotation)
        {
			this.glname = 0;
            this.name = name;
            this.anno = annotation;
        }

        public abstract void Delete();

        static protected string[][] Text2Args(string text)
        {
            List<string[]> args = new List<string[]>();

            // split into lines
            var lines = text.Split(new char[] { '\n' });

            // for each line
            for (int i = 0; i < lines.Length; i++)
            {
                // parse words, numbers and so on
                MatchCollection matches = Regex.Matches(lines[i], "[\\w./|]+");
                // an command must have at least two arguments
                if (matches.Count >= 2)
                    args.Add(matches.Cast<Match>().Select(m => m.Value).ToArray());
            }

            return args.ToArray();
        }

        static protected void Args2Prop<T>(T clazz, ref string[][] args)
        {
            Type type = clazz.GetType();

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                var field = type.GetField(arg[0]);
                if (field != null)
                {
                    // remove argument from array
                    args[i] = null;
                    // if this is an array pass the arguments as string
                    if (field.FieldType.IsArray)
                        field.SetValue(clazz, arg.Skip(1).Take(arg.Length-1).ToArray());
                    // if this is an enum, convert the string to an enum value
                    else if (field.FieldType.IsEnum)
                        field.SetValue(clazz, Convert.ChangeType(Enum.Parse(field.FieldType, arg[1], true), field.FieldType));
                    // else try to convert it to the field type
                    else
                        field.SetValue(clazz, Convert.ChangeType(arg[1], field.FieldType, culture));
                }
            }
        }

        static protected void throwExceptionOnOpenGlError(string type, string name, string glevent)
        {
            ErrorCode er = GL.GetError();
            if (er != ErrorCode.NoError)
                throw new Exception("ERROR in " + type + " " + name + ": OpenGL error " + er + " occurred during '" + glevent + "'.");
        }
    }
}
