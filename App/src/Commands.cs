using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static System.Reflection.BindingFlags;

namespace App
{
    class Commands : IEnumerable<Commands.Triple>
    {
        public struct Triple
        {
            public Triple(int idx, string cmd, string[] args)
            {
                this.idx = idx;
                this.cmd = cmd;
                this.args = args;
            }
            public int idx;
            public string cmd;
            public string[] args;
        }

        private List<Triple> cmds = new List<Triple>();

        public IEnumerable<Triple> this[int key] {
            get
            {
                return cmds.Where(x => x.idx == key);
            }
        }

        public IEnumerable<Triple> this[string key]
        {
            get
            {
                return cmds.Where(x => x.cmd == key);
            }
        }

        public Commands(string body, GLException err = null)
        {
            Text2Cmds(body, err);
        }

        protected void Text2Cmds(string body, GLException err = null)
        {
            // split into lines
            var lines = body.Split(new char[] { '\n' });

            // for each line
            int command = 1;
            foreach (var line in lines)
            {
                // parse words, numbers and so on
                MatchCollection matches = Regex.Matches(line, "[\\w./|\\-:]+");
                // an command must have at least two arguments
                if (matches.Count >= 2)
                {
                    var args = matches.Cast<Match>().Select(m => m.Value);
                    var triple = new Triple(command++, args.First(), args.Skip(1).ToArray());
                    cmds.Add(triple);
                }
                else if (matches.Count > 0)
                {
                    err?.Add($"Command {command} musst specify at least one argument.");
                    command++;
                }
            }
        }

        internal Dictionary<string,string[]> ToDict()
        {
            var dict = new Dictionary<string, string[]>();
            foreach (var cmd in cmds)
                dict.Add(cmd.cmd, cmd.args);
            return dict;
        }

        public void Cmds2Fields<T>(T clazz, GLException err = null)
        {
            Type type = clazz.GetType();
            List<Triple> removeKeys = new List<Triple>();

            foreach (var triple in cmds)
            {
                var field = type.GetField(triple.cmd, Instance | Public | NonPublic);
                var prop = type.GetProperty(triple.cmd, Instance | Public | NonPublic);
                MemberInfo member = (MemberInfo)field ?? prop;

                if (member == null || member.GetCustomAttributes(typeof(GLField), false).Length == 0)
                    continue;

                // remove argument from array
                removeKeys.Add(triple);

                object val = (object)field ?? prop;
                Type valtype = field?.FieldType ?? prop?.PropertyType;
                SetValue(clazz, val, valtype, triple.cmd, triple.args, err);
            }

            foreach (var triple in removeKeys)
                cmds.Remove(triple);
        }

        static private void SetValue<T>(T clazz, object field, Type fieldType, string key, string[] value, GLException err = null)
        {
            var SetValue = field.GetType().GetMethod("SetValue", new Type[] { typeof(object), typeof(object) });

            // if this is an array pass the arguments as string
            if (fieldType.IsArray)
                SetValue.Invoke(field, new object[] { clazz, value });

            // if this is an enum, convert the string to an enum value
            else if (fieldType.IsEnum)
            {
                if (value.Length > 1)
                    err?.Add($"Command '{key}' has too many arguments (more than one).");
                else
                    SetValue.Invoke(field, new object[] { clazz,
                        Convert.ChangeType(Enum.Parse(fieldType, value[0], true), fieldType) });
            }
            // else try to convert it to the field type
            else
            {
                if (value.Length > 1)
                    err?.Add($"Command '{key}' has too many arguments (more than one).");
                else
                    SetValue.Invoke(field, new object[] { clazz,
                        Convert.ChangeType(value[0], fieldType, App.culture) });
            }
        }

        public IEnumerator<Triple> GetEnumerator()
        {
            return cmds.GetEnumerator();
        }

        IEnumerator<Triple> IEnumerable<Triple>.GetEnumerator()
        {
            return cmds.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cmds.GetEnumerator();
        }
    }
}
