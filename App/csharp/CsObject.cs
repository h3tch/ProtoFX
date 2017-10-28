using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;

namespace protofx
{
    class CsObject
    {
        #region FIELDS

        public static CultureInfo EN = new CultureInfo("en");
        public static CultureInfo culture = EN;
        protected List<string> errors = new List<string>();
        private Dictionary<int, object> uniformBlocks = new Dictionary<int, object>();
        protected Commands commands;
        protected Objects objects;

        #endregion

        public CsObject(Commands cmds, Objects objs)
        {
            commands = cmds;
            objects = objs;
        }

        public UniformBlock<Names> GetUniformBlock<Names>(int pipeline, string name)
        {
            object unif = null;
            if (uniformBlocks.TryGetValue(pipeline, out unif) == false)
            {
                if (UniformBlock<Names>.HasUnifromBlock(pipeline, name))
                    unif = new UniformBlock<Names>(pipeline, name);
                uniformBlocks.Add(pipeline, unif);
            }
            return (UniformBlock<Names>)unif;
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
