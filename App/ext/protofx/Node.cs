using System;
using System.Collections.Generic;
using System.Reflection;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;

namespace protofx
{
    class Connectable : System.Attribute { }


    class Connection
    {
        private FieldObject Field;
        private List<Connection> Targets;
        private event UpdateEvent OnUpdate;
        public delegate void UpdateEvent(object value);

        public Connection(object clazz, FieldInfo field)
        {
            Field = new FieldObject(clazz, field);
        }

        public void Connect(Connection other)
        {
            if (Targets == null)
                Targets = new List<Connection>();
            Targets.Add(other);
        }

        public void Update()
        {
            if (Targets == null)
                return;

            for (int i = 0; i < Targets.Count; i++)
            {
                Targets[i].Field.Value = Field.Value;
                if (OnUpdate != null)
                    OnUpdate(Field.Value);

                Targets[i].Update();
            }
        }

        #region INNER STRUCT

        private struct FieldObject
        {
            private object Object;
            private FieldInfo Info;
            public object Value
            {
                get { return Info.GetValue(Object); }
                set
                {
                    var newValue = Convert.ChangeType(value, Info.FieldType, protofx.Object.culture);
                    Info.SetValue(Object, newValue);
                }
            }

            public FieldObject(object obj, FieldInfo info)
            {
                Object = obj;
                Info = info;
            }
        }

        #endregion
    }


    class Node : Object
    {
        protected static BindingFlags bindingFlags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;
        protected bool connectionsInitialized = false;
        private Dictionary<string, Connection> connections = new Dictionary<string, Connection>();

        public Node(Commands cmds, Objects objs) : base(cmds, objs)
        {
        }

        public void Initialize()
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
                        if (info.GetCustomAttribute<Connectable>() == null)
                            throw new ArgumentException("'" + connect[0] + "' does not support connections.");
                        connections[connect[0]] = new Connection(this, info);
                    }

                    // get destination object
                    var obj = FindSceneObject(dstScript[0]);
                    if (!(obj is Node))
                        throw new ArgumentException("'" + dstScript[0] + "' does not support connections.");
                    var dst = (Node)obj;

                    // get connection object of the destination object
                    if (!dst.connections.ContainsKey(dstScript[1]))
                    {
                        // If the connection object does not yet exist, create a new one.
                        var info = FindField(dst, dstScript[1]);
                        if (info.GetCustomAttribute<Connectable>() == null)
                            throw new ArgumentException("target '" + connect[1] + "' does not support connections.");
                        dst.connections[dstScript[1]] = new Connection(dst, info);
                    }

                    // connect the two connection objects
                    connections[connect[0]].Connect(dst.connections[dstScript[1]]);
                }
                catch (ArgumentException ex)
                {
                    AddCommandError(ex.Message);
                }
                catch (FieldAccessException ex)
                {
                    AddCommandError(ex.Message);
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
            if (!objects.ContainsKey(name))
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

        private object FindValue(object obj, string name)
        {
            var info = FindField(obj, name);
            return info.GetValue(obj);
        }
    }
}
