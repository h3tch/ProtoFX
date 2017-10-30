using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;

namespace protofx
{
    /// <summary>
    /// The Node class provides support for automatically
    /// passing values to connected fields of other Node classes.
    /// </summary>
    class Node : Object
    {
        #region FIELDS

        protected static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        protected Connections connections = new Connections();

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cmds"></param>
        /// <param name="objs"></param>
        public Node(Commands cmds, Objects objs) : base(cmds, objs)
        {
        }

        /// <summary>
        /// This method will be automatically called by the
        /// ProtoFX application.
        /// </summary>
        protected void Initialize()
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
                        var info = GetField(this, connect[0]);
                        if (info.GetCustomAttribute<Connectable>() == null)
                            throw new ArgumentException("'" + connect[0] + "' does not support connections.");
                        connections[connect[0]] = new Connection(this, info);
                    }

                    // get destination object
                    var obj = GetSceneObject(dstScript[0]);
                    if (!(obj is Node))
                        throw new ArgumentException("'" + dstScript[0] + "' does not support connections.");
                    var dst = (Node)obj;

                    // get connection object of the destination object
                    if (!dst.connections.ContainsKey(dstScript[1]))
                    {
                        // If the connection object does not yet exist, create a new one.
                        var info = GetField(dst, dstScript[1]);
                        if (info.GetCustomAttribute<Connectable>() == null)
                            throw new ArgumentException("target '" + connect[1] + "' does not support connections.");
                        dst.connections[dstScript[1]] = new Connection(dst, info);
                    }

                    // connect the two connection objects
                    connections[connect[0]].Connect(dst.connections[dstScript[1]]);
                }
                catch (ArgumentException ex)
                {
                    Errors.Add("command 'connect': " + ex.Message);
                }
            }
        }
        
        /// <summary>
        /// Find the specified scene object.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private object GetSceneObject(string name)
        {
            // get protofx instance object
            object dstGlInstance;
            if (!objects.TryGetValue(name, out dstGlInstance))
                throw new ArgumentException("Could not find an object named '" + name + "'.");

            // get field informations
            var instanceField = dstGlInstance.GetType().GetField("Instance", bindingFlags);
            if (instanceField == null)
                throw new ArgumentException("The target '" + name + "' is not an instance.");

            // get object
            return instanceField.GetValue(dstGlInstance);
        }

        /// <summary>
        /// Find the specified field of the specified class.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static FieldInfo GetField(object obj, string name)
        {
            var info = obj.GetType().GetField(name, bindingFlags);
            if (info == null)
                throw new ArgumentException("This instance does not contain a field named '" + name + "'.");
            if (!info.FieldType.IsPrimitive)
                throw new ArgumentException("Field '" + name + "' must have a primitive type, not '" + info.FieldType.Name + "'.");
            return info;
        }
        
        /// <summary>
        /// Work around the missing 'nameof' function in older C# standard.
        /// </summary>
        /// <param name="member">An expression containing the member of
        /// which the name is requested (e.g. NameOf(() => memberName))</param>
        /// <returns>Returns the name of the member access expression.</returns>
        protected static string NameOf(Expression<Func<object>> member)
        {
            MemberExpression memberExpression;
            if (member.Body is UnaryExpression)
                memberExpression = (MemberExpression)((UnaryExpression)member.Body).Operand;
            else if (member.Body is MemberExpression)
                memberExpression = (MemberExpression)member.Body;
            else
                return null;
            return memberExpression.Member.Name;
        }

        /// <summary>
        /// An utility class to simplify the access to connections.
        /// </summary>
        protected class Connections : Dictionary<string, Connection>
        {
            public Connection this[Expression<Func<object>> member]
            {
                get { return this[NameOf(member)]; }
            }
        }
    }

    /// <summary>
    /// The Connection class connects fields of Node classes.
    /// It is used by the Node class to automatically pass
    /// values to the connection fields of other Node classes.
    /// </summary>
    class Connection
    {
        #region FIELDS

        private FieldObject Field;
        private List<Connection> Targets;
        private event UpdateEvent OnUpdate;
        public delegate void UpdateEvent(object value);

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="clazz"></param>
        /// <param name="field"></param>
        public Connection(object clazz, FieldInfo field)
        {
            Field = new FieldObject(clazz, field);
        }

        /// <summary>
        /// Connect two fields. When calling the Update method,
        /// the value of will be automatically propagated to the
        /// connected field.
        /// </summary>
        /// <param name="other"></param>
        public void Connect(Connection other)
        {
            if (Targets == null)
                Targets = new List<Connection>();
            Targets.Add(other);
        }

        /// <summary>
        /// Propagates the value to all connected fields.
        /// </summary>
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

        /// <summary>
        /// This utility structure uniquely identifies a field.
        /// In contrast to FieldInfo, this allows us to change
        /// the value of the field.
        /// </summary>
        private struct FieldObject
        {
            private object Object;
            private FieldInfo Info;
            public object Value
            {
                get { return Info.GetValue(Object); }
                set
                {
                    var v = Convert.ChangeType(value, Info.FieldType, protofx.Object.culture);
                    Info.SetValue(Object, v);
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

    /// <summary>
    /// The Connectable attribute marks fields that can be connected.
    /// </summary>
    class Connectable : Attribute { }
}
