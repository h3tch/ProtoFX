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
        private List<Triple> cmds = new List<Triple>();

        public IEnumerable<Triple> this[int key] => cmds.Where(x => x.idx == key);
        public IEnumerable<Triple> this[string key] => cmds.Where(x => x.cmd == key);

        public Commands(string body, CompileException err = null)
        {
            Text2Cmds(body, err);
        }

        protected void Text2Cmds(string body, CompileException err = null)
        {
            // split into lines
            var lines = body.Split(new char[] { '\n' });

            // for each line
            int command = 1;
            foreach (var line in lines)
            {
                // parse words, numbers and so on
                MatchCollection matches = Regex.Matches(line, "[\\w./|\\-:<>]+");
                // an command must have at least two arguments
                if (matches.Count >= 2)
                {
                    var args = matches.Cast<Match>().Select(m => m.Value);
                    cmds.Add(new Triple(command++, args.First(), args.Skip(1).ToArray()));
                }
                else if (matches.Count > 0)
                {
                    err?.Add($"Command {command} must specify at least one argument.");
                    command++;
                }
            }
        }

        internal Dictionary<string,string[]> ToDict()
        {
            // convert triple to dict of string arrays
            var dict = new Dictionary<string, string[]>();
            foreach (var cmd in cmds)
                dict.Add(cmd.cmd, cmd.args);
            return dict;
        }

        public void Cmds2Fields<T>(T clazz, CompileException err = null)
        {
            var type = clazz.GetType();
            var removeKeys = new List<Triple>();

            foreach (var triple in cmds)
            {
                // try to find a field with the respective name
                var field = type.GetField(triple.cmd, Instance | Public | NonPublic);
                var prop = type.GetProperty(triple.cmd, Instance | Public | NonPublic);
                MemberInfo member = (MemberInfo)field ?? prop;

                // if no field could be found go to the next command
                if (member == null || member.GetCustomAttributes(typeof(Field), false).Length == 0)
                    continue;

                // remove argument from array
                removeKeys.Add(triple);

                // set value of field
                object val = (object)field ?? prop;
                Type valtype = field?.FieldType ?? prop?.PropertyType;
                SetValue(clazz, val, valtype, triple.cmd, triple.args, err);
            }
            
            // remove all commands that could be used to set a field
            foreach (var triple in removeKeys)
                cmds.Remove(triple);
        }

        static private void SetValue<T>(T clazz, object field, Type fieldType,
            string key, string[] value, CompileException err = null)
        {
            // check for errors
            if (!fieldType.IsArray && value.Length > 1)
                err?.Add($"Command '{key}' has too many arguments (more than one).");
            
            var val = fieldType.IsArray ?
                // if this is an array pass the arguments as string
                value :
                fieldType.IsEnum ?
                    // if this is an enum, convert the string to an enum value
                    Convert.ChangeType(Enum.Parse(fieldType, value[0], true), fieldType) :
                    // else try to convert it to the field type
                    Convert.ChangeType(value[0], fieldType, App.culture);
            
            var SetValue = field.GetType().GetMethod(
                "SetValue", new Type[] { typeof(object), typeof(object) });

            SetValue.Invoke(field, new object[] { clazz, val });
        }

        #region IEnumerable Interface
        public IEnumerator<Triple> GetEnumerator() => cmds.GetEnumerator();
        IEnumerator<Triple> IEnumerable<Triple>.GetEnumerator() => cmds.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => cmds.GetEnumerator();
        #endregion

        #region INNER CLASSES
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
        #endregion
    }
}
