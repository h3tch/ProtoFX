using System;
using System.Collections.Generic;
using System.Reflection;

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
                    var newValue = Convert.ChangeType(value, Info.FieldType, CsObject.culture);
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
}
