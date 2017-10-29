using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;

namespace protofx
{
    /// <summary>
    /// This is the base object of which every 
    /// extension needs to be derived from.
    /// </summary>
    class Object
    {
        #region FIELDS

        public static CultureInfo EN = new CultureInfo("en");
        public static CultureInfo culture = EN;
        private Dictionary<int, object> uniformBlocks = new Dictionary<int, object>();
        protected List<string> Errors = new List<string>();
        protected Commands commands;
        protected Objects objects;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cmds"></param>
        /// <param name="objs"></param>
        public Object(Commands cmds, Objects objs)
        {
            commands = cmds;
            objects = objs;
        }

        /// <summary>
        /// Search in the specified shader pipeline for the
        /// specified uniform block name.
        /// </summary>
        /// <param name="pipeline"></param>
        /// <param name="name"></param>
        /// <returns>The uniform block or 'null' if the block could not be found.</returns>
        public UniformBlock GetUniformBlock(int pipeline, string name, string[] variableNames)
        {
            object unif = null;
            if (uniformBlocks.TryGetValue(pipeline, out unif) == false)
            {
                if (UniformBlock.HasUnifromBlock(pipeline, name))
                    unif = new UniformBlock(pipeline, name, variableNames);
                uniformBlocks.Add(pipeline, unif);
            }
            return (UniformBlock)unif;
        }

        /// <summary>
        /// Search for command and convert in into an array.
        /// The result is written to 'v'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmds"></param>
        /// <param name="cmd"></param>
        /// <param name="v"></param>
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
                    Errors.Add("Command '" + cmd + "': Could not convert argument "
                        + (i + 1) + " '" + s[i] + "'.");
            }
        }

        /// <summary>
        /// Search for command and convert in into the specified type.
        /// The result is written to 'v'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmds"></param>
        /// <param name="cmd"></param>
        /// <param name="v"></param>
        protected void Convert<T>(Commands cmds, string cmd, ref T v)
        {
            var s = cmds[cmd].FirstOrDefault();
            if (s == null)
                return;
            if (!TryChangeType(s[0], ref v))
                Errors.Add("Command '" + cmd + "': Could not convert argument 1 '" + s[0] + "'.");
        }

        /// <summary>
        /// Try to change the input to the specified type.
        /// The result is written to 'v'. If the conversion failed,
        /// the method will return 'false'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool TryChangeType<T>(object input, ref T value)
        {
            if (input == null || input as IConvertible == null)
                return false;

            try
            {
                value = (T)System.Convert.ChangeType(input, typeof(T), culture);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
