using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;

namespace protofx
{
    class CsObject
    {
        public static CultureInfo EN = new CultureInfo("en");
        public static CultureInfo culture = EN;
        protected List<string> errors = new List<string>();
        private string[][] connects;
        private Objects objects;
        private bool connectionsInitialized = false;

        public CsObject(Commands cmds, Objects objs)
        {
            connects = cmds["connect"].ToArray();
            objects = objs;
        }

        public UniformBlock<Names> GetUniformBlock<Names>(Dictionary<int, UniformBlock<Names>> uniform, int pipeline, string name)
        {
            UniformBlock<Names> unif = null;
            if (uniform.TryGetValue(pipeline, out unif) == false)
            {
                if (UniformBlock<Names>.HasUnifromBlock(pipeline, name))
                    unif = new UniformBlock<Names>(pipeline, name);
                uniform.Add(pipeline, unif);
            }
            return unif;
        }

        public void InitializeConnections()
        {
            if (connectionsInitialized)
                return;

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | 
                BindingFlags.NonPublic;
            var type = GetType();
            const string err = "command 'connect': ";

            foreach (var connect in connects)
            {
                // check the connect command syntax
                if (connect.Length != 2)
                {
                    errors.Add(err + "Exactly two arguments are expected (e.g. 'object_name.value_name').");
                    continue;
                }

                // split target string
                var dstScript = connect[1].Split('.');
                if (dstScript.Length != 2)
                {
                    errors.Add(err + "The second argument expects 'object_name.value_name' as syntax.");
                    continue;
                }

                // get destination object
                if (!objects.Keys.Contains(dstScript[0]))
                {
                    errors.Add(err + "Could not find an object named '" + dstScript[0] + "'.");
                    continue;
                }
                var dstGlInstance = objects[dstScript[0]];

                // get field informations
                var srcField = type.GetField(connect[0], flags);
                if (srcField == null)
                {
                    errors.Add(err + "This instance does not contain a field named '" + connect[0] + "'.");
                    continue;
                }
                var instanceField = dstGlInstance.GetType().GetField("Instance", flags);
                if (instanceField == null)
                {
                    errors.Add(err + "The target '" + dstScript[0] + "' is not an instance.");
                    continue;
                }
                var dstObj = instanceField.GetValue(dstGlInstance);
                var dstField = dstObj.GetType().GetField(dstScript[1], flags);
                if (dstField == null)
                {
                    errors.Add(err + "The target instance does not contain a field named '" + dstScript[1] + "'.");
                    continue;
                }

                // get field values
                var src = srcField.GetValue(this);
                var dst = dstField.GetValue(dstObj);
                if (!(src is protofx.Double))
                {
                    errors.Add(err + "The source field '" + connect[0] + "' cannot be used for a connection.");
                    continue;
                }
                if (!(dst is protofx.Double))
                {
                    errors.Add(err + "The target field '" + dstScript[1] + "' cannot be used for a connection.");
                    continue;
                }

                // connect the fields
                ((protofx.Double)src).Connect((protofx.Double)dst);
            }

            connectionsInitialized = true;
        }

        public List<string> GetErrors()
        {
            return errors;
        }

        protected void Convert<T>(Commands cmds, string cmd, ref T[] v)
        {
            int i = 0, l;

            int length = v.Length;
            
            var s = cmds[cmd].FirstOrDefault();
            if (s == null)
                return;

            for (l = Math.Min(s.Length, length); i < s.Length; i++)
            {
                if (!TryChangeType(s[i], ref v[i]))
                    errors.Add("Command '" + cmd + "': Could not convert argument "
                        + (i + 1) + " '" + s[i] + "'.");
            }
        }

        protected void Convert<T>(Commands cmds, string cmd, ref T v)
        {
            var s = cmds[cmd].FirstOrDefault();
            if (s == null)
                return;
            if (!TryChangeType(s[0], ref v))
                errors.Add("Command '" + cmd + "': Could not convert argument 1 '" + s[0] + "'.");
        }

        private static bool TryChangeType<T>(object invalue, ref T value)
        {
            if (invalue == null || invalue as IConvertible == null)
                return false;

            try
            {
                value = (T)System.Convert.ChangeType(invalue, typeof(T), culture);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
