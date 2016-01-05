using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static System.Reflection.BindingFlags;

namespace App
{
    class Commands : IEnumerable<Commands.Cmd>
    {
        private List<Cmd> cmds = new List<Cmd>();
        public IEnumerable<Cmd> this[int key] => cmds.Where(x => x.idx == key);
        public IEnumerable<Cmd> this[string key] => cmds.Where(x => x.cmd == key);

        public Commands(string body, int pos, CompileException err = null)
        {
            // split into lines
            var linePos = CodeEditor.GetNewLines(body);
            var lines = body.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

            // for each line
            for (int i = 0, idx = 1; i < lines.Length; i++)
            {
                // parse words, numbers and so on
                MatchCollection matches = Regex.Matches(lines[i], "[\\w./|\\-:<>]+");
                if (matches.Count == 0)
                    continue;
                // command position in the text
                var cmdPos = pos + linePos[i] + matches[0].Index;
                // an command must have at least two arguments
                if (matches.Count >= 2)
                {
                    var args = matches.Cast<Match>().Select(m => m.Value);
                    cmds.Add(new Cmd
                    {
                        idx = idx++,
                        pos = cmdPos,
                        cmd = args.First(),
                        args = args.Skip(1).ToArray()
                    });
                }
                else
                    err?.Add($"Command[{idx++}] '{matches[0].Value}' " +
                        "must specify at least one argument.", cmdPos);
            }
        }

        internal Dictionary<string, string[]> ToDict()
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
            var removeKeys = new List<Cmd>();

            foreach (var cmd in cmds)
            {
                // try to find a field with the respective name
                var field = type.GetField(cmd.cmd, Instance | Public | NonPublic);
                var prop = type.GetProperty(cmd.cmd, Instance | Public | NonPublic);
                MemberInfo member = (MemberInfo)field ?? prop;

                // if no field could be found go to the next command
                if (member == null || member.GetCustomAttributes(typeof(Field), false).Length == 0)
                    continue;

                // remove argument from array
                removeKeys.Add(cmd);

                // set value of field
                object val = (object)field ?? prop;
                Type valtype = field?.FieldType ?? prop?.PropertyType;
                SetValue(clazz, val, valtype, cmd, err);
            }
            
            // remove all commands that could be used to set a field
            foreach (var triple in removeKeys)
                cmds.Remove(triple);
        }

        static private void SetValue<T>(T clazz, object field, Type fieldType,
            Cmd cmd, CompileException err = null)
        {
            // check for errors
            if (!fieldType.IsArray && cmd.args.Length > 1)
                err?.Add($"Command '{cmd.cmd}' has too many arguments (more than one).", cmd.pos);
            
            var val = fieldType.IsArray ?
                // if this is an array pass the arguments as string
                cmd.args :
                fieldType.IsEnum ?
                    // if this is an enum, convert the string to an enum value
                    Convert.ChangeType(Enum.Parse(fieldType, cmd.args[0], true), fieldType) :
                    // else try to convert it to the field type
                    Convert.ChangeType(cmd.args[0], fieldType, App.culture);
            
            var SetValue = field.GetType().GetMethod(
                "SetValue", new Type[] { typeof(object), typeof(object) });

            SetValue.Invoke(field, new object[] { clazz, val });
        }

        #region IEnumerable Interface
        public IEnumerator<Cmd> GetEnumerator() => cmds.GetEnumerator();

        IEnumerator<Cmd> IEnumerable<Cmd>.GetEnumerator() => cmds.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => cmds.GetEnumerator();
        #endregion

        #region INNER CLASSES
        public struct Cmd
        {
            public Cmd(int idx, int pos, string cmd, string[] args)
            {
                this.idx = idx;
                this.pos = pos;
                this.cmd = cmd;
                this.args = args;
            }
            public int idx;
            public int pos;
            public string cmd;
            public string[] args;
        }
        #endregion
    }
}
