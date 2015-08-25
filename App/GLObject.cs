using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace gled
{
    abstract class GLObject
    {
        public int glname { get; protected set; } = 0;
        public string name { get; }
        public string anno { get; }

        public GLObject(string name, string annotation)
        {
            this.name = name;
            this.anno = annotation;
        }

        public abstract void Delete();

        static protected string[][] Text2Args(string text)
        {
            List<string[]> args = new List<string[]>();

            var lines = text.Split(new char[] { '\n' });

            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], "[\\w./|]+");
                if (matches.Count >= 2)
                    args.Add(matches.Cast<Match>().Select(m => m.Value).ToArray());
            }

            return args.ToArray();
        }

        static protected void Args2Prop<T>(T clazz, string[][] args)
        {
            Type type = clazz.GetType();

            foreach (var arg in args)
            {
                var field = type.GetField(arg[0]);
                if (field != null)
                {
                    if (field.FieldType.IsArray)
                    {
                        field.SetValue(clazz, arg.Skip(1).Take(arg.Length-1).ToArray());
                    }
                    else
                    {
                        if (field.FieldType.IsEnum)
                            field.SetValue(clazz, Convert.ChangeType(Enum.Parse(field.FieldType, arg[1], true), field.FieldType));
                        else
                            field.SetValue(clazz, Convert.ChangeType(arg[1], field.FieldType));
                    }
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
