using System.Linq;
using System.Reflection;

namespace System
{
    static class ObjectExtensions
    {
        /// <summary>
        /// Perform a deep copy.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="outType"></param>
        /// <returns></returns>
        public static object DeepCopy(this object obj, Type outType = null)
        {
            if (obj == null)
                return null;

            var inType = obj.GetType();
            if (outType == null)
                outType = inType;

            // If the type of object is the value type, we will always get a new object when  
            // the original object is assigned to another variable. So if the type of the  
            // object is primitive or enum, we just return the object. We will process the  
            // struct type subsequently because the struct type may contain the reference  
            // fields. 
            // If the string variables contain the same chars, they always refer to the same  
            // string in the heap. So if the type of the object is string, we also return the  
            // object. 
            if (inType.IsPrimitive || inType.IsEnum || inType == typeof(string))
                return inType == outType ? obj : null;

            // If the type of the object is the Array, we use the CreateInstance method to get 
            // a new instance of the array. We also process recursively this method in the  
            // elements of the original array because the type of the element may be the reference  
            // type. 
            else if (inType.IsArray)
            {
                var array = obj as Array;
                outType = outType.GetElementType();
                var copy = Array.CreateInstance(outType, array.Length);

                for (int i = 0; i < array.Length; i++)
                    // Get the deep clone of the element in the original
                    // array and assign the   clone to the new array. 
                    copy.SetValue(array.GetValue(i).DeepCopy(outType), i);

                return copy;
            }

            // If the type of the object is class or struct, it may contain the reference fields,  
            // so we use reflection and process recursively this method in the fields of the object  
            // to get the deep clone of the object.  
            // We use Type.IsValueType method here because there is no way to indicate directly 
            // whether  the Type is a struct type. 
            else if (inType.IsClass || inType.IsValueType)
            {
                var copy = Activator.CreateInstance(outType);
                var flags =
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance;

                // Copy all fields. 
                var inFields = inType.GetFields(flags);
                var outFields = outType.GetFields(flags);
                for (int i = 0; i < inFields.Length; i++)
                {
                    var inField = inFields[i];
                    var outField = outFields[i];
                    var value = inField.GetValue(obj).DeepCopy(outField.FieldType);
                    outField.SetValue(copy, value);
                }

                return copy;
            }
            else
                throw new ArgumentException("The object has an unknown type");
        }

        /// <summary>
        /// Find a member from a string (e.g., "obj1.field3.property3", 
        /// "obj1.TypeName.property3", "obj1.field3.property[1]", ...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static (object Owner, object Info, int Index) FindMember<T>(this T me, string text)
        {
            FindMember(me, text, out object owner, out object info, out int index);
            return (owner, info, index);
        }

        /// <summary>
        /// Set the value of a field or property using a string (e.g.,
        /// "obj1.field3.property3", "obj1.TypeName.property3",
        /// "obj1.field3.property[1]", ...).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static void SetValue<T>(this T obj, string text, object value)
        {
            (object owner, object info, int index) = obj.FindMember(text);
            SetValue(owner, info, index, value);
        }

        /// <summary>
        /// see public static (object Owner, object Info, int Index) FindMember<T>(this T me, string text)
        /// This method is used for debugging purposes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="access"></param>
        /// <param name="Owner"></param>
        /// <param name="Info"></param>
        /// <param name="Index"></param>
        private static void FindMember<T>(this T me, string access, out object Owner, out object Info, out int Index)
        {
            var parts = access.Split('.');
            object owner = me;
            object info = null;
            object value = null;
            string name;
            int index;

            Owner = null;
            Info = null;
            Index = -1;

            // navigate to last field
            for (int i = 0; i < parts.Length - 1; i++, owner = value)
            {
                (name, index) = GetNameAndIndex(parts[i]);

                // update info
                info = GetFieldOrProp(owner.GetType(), name);
                if (info == null)
                    return;

                // update value
                switch (info)
                {
                    case FieldInfo field: value = field.GetValue(owner); break;
                    case PropertyInfo prop: value = prop.GetValue(owner); break;
                }
                // is array
                if (index >= 0)
                    value = (value as Array).GetValue(index);
            }

            (name, index) = GetNameAndIndex(parts[parts.Length - 1]);
            info = GetFieldOrProp(owner.GetType(), name);
            Owner = owner;
            Info = info;
            Index = index;

            /// LOCAL FUNCTIONS

            (string, int) GetNameAndIndex(string part)
            {
                int idx = -1;
                var open = part.IndexOf('[');
                var str = part;

                if (open >= 0)
                {
                    var close = part.IndexOf(']');
                    if (close < 0)
                        throw new FormatException($"']' missing in '{part}'");
                    idx = int.Parse(part.Subrange(open + 1, close));
                    str = part.Substring(0, open);
                }

                return (str, idx);
            }

            object GetFieldOrProp(Type type, string str)
            {
                object fieldInfo = type.GetField(str, flags);
                if (fieldInfo != null)
                    return fieldInfo;

                fieldInfo = type.GetProperty(str, flags);
                if (fieldInfo != null)
                    return fieldInfo;

                fieldInfo = type.GetFields(flags).FirstOrDefault(f => f.FieldType.Name == str);
                if (fieldInfo != null)
                    return fieldInfo;

                fieldInfo = type.GetProperties(flags).FirstOrDefault(p => p.PropertyType.Name == str);
                if (fieldInfo != null)
                    return fieldInfo;

                return null;
            }

        }

        /// <summary>
        /// Set the value of a field or property, which can also be an array.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="info"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        private static void SetValue(object owner, object info, int index, object value)
        {
            // get property or field information
            object field = null;
            Type fieldType = null;

            switch (info)
            {
                case FieldInfo f:
                    field = f;
                    fieldType = f.FieldType;
                    break;
                case PropertyInfo p:
                    field = p;
                    fieldType = p.PropertyType;
                    break;
            }

            // if we need to set the value of an array we need the
            // element type, otherwise the field type is used
            var type = index >= 0 ? fieldType.GetElementType() : fieldType;

            // we need to convert the value if the types do not match up
            if (type != value.GetType())
            {
                var ctr = type.GetConstructor(new[] { value.GetType() });
                if (ctr != null)
                    value = ctr.Invoke(new[] { value });
            }
            
            // in case of an array
            if (index >= 0)
            {
                var method = field.GetType().GetMethod("GetValue", flags, null,
                    CallingConventions.Any, new[] { typeof(object) }, null);
                var array = method.Invoke(field, new[] { owner }) as Array;
                array.SetValue(value, index);
            }
            else
            {
                var method = field.GetType().GetMethod("SetValue", flags, null,
                    CallingConventions.Any, new[] { typeof(object), typeof(object) }, null);
                method.Invoke(field, new[] { owner, value });
            }
        }

        private static BindingFlags flags = 
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance;
    }
}
