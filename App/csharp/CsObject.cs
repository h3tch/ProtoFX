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
        #region FIELDS

        public static CultureInfo EN = new CultureInfo("en");
        public static CultureInfo culture = EN;
        protected static BindingFlags bindingFlags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;
        protected List<string> errors = new List<string>();
        private Dictionary<string, Connection> connections = new Dictionary<string, Connection>();
        private Commands commands;
        private Objects objects;
        protected bool connectionsInitialized = false;

        #endregion

        public CsObject(Commands cmds, Objects objs)
        {
            commands = cmds;
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

        protected void InitializeConnections()
        {
            foreach (var connect in commands["connect"])
            {
                try
                {
                    // check the connect command syntax
                    if (connect.Length != 2)
                        throw new ArgumentException("Exactly two arguments are expected (e.g. 'object_name.value_name').");

                    // split target string
                    var dstScript = connect[1].Split('.');
                    if (dstScript.Length != 2)
                        throw new ArgumentException("The second argument expects 'object_name.value_name' as syntax.");

                    // get connection object
                    if (!connections.ContainsKey(connect[0]))
                    {
                        // If the connection object does not yet exist, create a new one.
                        var info = FindField(this, connect[0]);
                        connections[connect[0]] = new Connection(this, info);
                    }

                    // get destination object
                    var d = (CsObject)FindSceneObject(dstScript[0]);

                    // get connection object of the destination object
                    if (!d.connections.ContainsKey(dstScript[1]))
                    {
                        // If the connection object does not yet exist, create a new one.
                        var info = FindField(d, dstScript[1]);
                        d.connections[dstScript[1]] = new Connection(d, info);
                    }

                    // connect the two connection objects
                    connections[connect[0]].Connect(d.connections[dstScript[1]]);
                }
                catch (ArgumentException ex)
                {
                    errors.Add(ex.Message);
                }
                catch (FieldAccessException ex)
                {
                    errors.Add(ex.Message);
                }
                catch { }
            }

            connectionsInitialized = true;
        }

        protected void UpdateConnections()
        {
            foreach (var connection in connections.Values)
                connection.Update();
        }

        private void AddCommandError(string message)
        {
            errors.Add("command 'connect': " + message);
        }

        private object FindSceneObject(string name)
        {
            // get protofx instance object
            if (!objects.Keys.Contains(name))
                throw new ArgumentException("Could not find an object named '" + name + "'.");
            var dstGlInstance = objects[name];

            // get field informations
            var instanceField = dstGlInstance.GetType().GetField("Instance", bindingFlags);
            if (instanceField == null)
                throw new FieldAccessException("The target '" + name + "' is not an instance.");

            // get object
            return instanceField.GetValue(dstGlInstance);
        }

        private FieldInfo FindField(object obj, string name)
        {
            var info = obj.GetType().GetField(name, bindingFlags);
            if (info == null)
                throw new FieldAccessException("This instance does not contain a field named '" + name + "'.");
            if (!info.FieldType.IsPrimitive)
                throw new FieldAccessException("Field '" + name + "' must have a primitive type, not '" + info.FieldType.Name + "'.");
            return info;
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
