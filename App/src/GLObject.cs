using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static System.Reflection.BindingFlags;

namespace App
{
    abstract class GLObject
    {
        #region PROPERTIES
        public int glname { get; protected set; }
        public string name { get; protected set; }
        public string anno { get; protected set; }
        #endregion

        public GLObject(string name, string annotation)
        {
            this.glname = 0;
            this.name = name;
            this.anno = annotation;
        }

        public abstract void Delete();

        public override string ToString() => name;

        #region UTIL METHODS
        static protected string[][] Text2Cmds(string text)
        {
            List<string[]> cmds = new List<string[]>();

            // split into lines
            var lines = text.Split(new char[] { '\n' });

            // for each line
            for (int i = 0; i < lines.Length; i++)
            {
                // parse words, numbers and so on
                MatchCollection matches = Regex.Matches(lines[i], "[\\w./|\\-:]+");
                // an command must have at least two arguments
                if (matches.Count >= 2)
                    cmds.Add(matches.Cast<Match>().Select(m => m.Value).ToArray());
            }

            return cmds.ToArray();
        }

        static protected void Cmds2Fields<T>(T clazz, ref string[][] cmds)
        {
            Type type = clazz.GetType();

            for (int i = 0; i < cmds.Length; i++)
            {
                var arg = cmds[i];
                var field = type.GetField(arg[0], Instance | Public | NonPublic);
                if (field == null || field.GetCustomAttributes(typeof(GLField), false).Length == 0)
                    continue;

                // remove argument from array
                cmds[i] = null;

                // if this is an array pass the arguments as string
                if (field.FieldType.IsArray)
                    field.SetValue(clazz, arg.Skip(1).Take(arg.Length-1).ToArray());
                // if this is an enum, convert the string to an enum value
                else if (field.FieldType.IsEnum)
                    field.SetValue(clazz, Convert.ChangeType(
                        Enum.Parse(field.FieldType, arg[1], true), field.FieldType));
                // else try to convert it to the field type
                else
                    field.SetValue(clazz, Convert.ChangeType(arg[1], field.FieldType, App.culture));
            }
        }
        #endregion
    }
}
