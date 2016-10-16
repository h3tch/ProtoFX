using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Reflection.BindingFlags;
using static System.Reflection.MemberTypes;

namespace App
{
    /// <summary>
    /// The <code>Field</code> attribute class is used to identify
    /// fields that can receive values from the application at compile time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FxField : Attribute { }
    
    abstract class GLObject
    {
        public int glname { get; protected set; }
        [FxField] public string name { get; protected set; }
        public string anno { get; protected set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="anno"></param>
        public GLObject(string name, string anno)
        {
            this.glname = 0;
            this.name = name;
            this.anno = anno;
        }
        
        /// <summary>
        /// Get debug label to the specified OpenGL object.
        /// </summary>
        /// <param name="type">OpenGL object type</param>
        /// <param name="glname">OpenGL object name</param>
        /// <returns>Returns the debug string of the object if specified.</returns>
        public static string GetLabel(ObjectLabelIdentifier type, int glname)
        {
            int length = 64;
            var label = new StringBuilder(length);
            GL.GetObjectLabel(type, glname, length, out length, label);
            return label.ToString();
        }

        /// <summary>
        /// Delete and release resources. Called on recompilation or when the app closes.
        /// </summary>
        public virtual void Delete() { }

        /// <summary>
        /// Readable identifier of the class.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => name;

        /// <summary>
        /// Find all external OpenGL objects of the specified type.
        /// </summary>
        /// <param name="existing">List of objects already existent in the scene.</param>
        /// <param name="types">Type of objects to search for.</param>
        /// <param name="glIs">The respective OpenGL function to check whether the object ID exists.</param>
        /// <param name="glLabel">The respective OpenGL function to get the label to the object.</param>
        /// <param name="range">Range in which to search for external objects (starting with 0).</param>
        /// <returns></returns>
        protected static IEnumerable<GLObject> FindObjects(GLObject[] existing, Type[] types,
            MethodInfo glIs, MethodInfo glLabel, int range)
        {
            // select all object IDs in the scene that are of one of the specified types
            var internID = from x in existing where types.Any(y => y == x.GetType()) select x.glname;
            // find all external object IDs of the same type in OpenGL
            var externID = from x in Enumerable.Range(0, range)
                           where !internID.Contains(x) && (bool)glIs.Invoke(null, new object[] { x })
                           select x;
            // get respective OpenGL labels to ObenGL IDs
            var externName = from x in externID select (string)glLabel.Invoke(null, new object[] { x });
            // create ProtoFX classes referencing these OpenGL objects
            return externID
                .Zip(externName, (i, n) => n.Length == 0 ? null : new object[] { n, "tex", i })
                .Where(x => x != null)
                .Select(x => (GLObject)Activator.CreateInstance(types[0], x));
        }

        #region Error handling
        /// <summary>
        /// Check for compiler and OpenGL errors.
        /// </summary>
        /// <param name="err"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        static protected bool HasErrorOrGlError(CompileException err, Compiler.Block block)
        {
            var errcode = GL.GetError();
            if (errcode != ErrorCode.NoError)
            {
                err.Add($"OpenGL error '{errcode}' occurred.", block);
                return true;
            }
            return err.HasErrors();
        }

        /// <summary>
        /// Check for compiler and OpenGL errors.
        /// </summary>
        /// <param name="err"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        static protected bool HasErrorOrGlError(CompileException err, string file, int line)
        {
            var errcode = GL.GetError();
            if (errcode != ErrorCode.NoError)
            {
                err.Add($"OpenGL error '{errcode}' occurred.", file, line);
                return true;
            }
            return err.HasErrors();
        }
        #endregion

        #region Set field values from code block commands
        /// <summary>
        /// Process object block and try to convert commands to class internal fields.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="err"></param>
        protected void Cmds2Fields(Compiler.Block block, CompileException err = null)
        {
            foreach (var cmd in block)
            {
                // check if the name is too short for a field
                if (cmd.Name.Length < 2)
                    continue;

                // get field member info
                var member = GetFxField(cmd.Name);

                if (member != null)
                    // set value of field
                    SetValue(this, member, member is FieldInfo
                        ? (member as FieldInfo).FieldType
                        : (member as PropertyInfo).PropertyType, cmd, err);
            }
        }

        /// <summary>
        /// Search for members that declare FxField as an attribute.
        /// </summary>
        /// <param name="name">Member name.</param>
        /// <returns>Returns the member, which can be a property
        /// or field, or null if no member was found.</returns>
        protected MemberInfo GetFxField(string name)
        {
            var Name = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
            var member = GetType().GetMember(Name, Field | Property, Instance | Public | NonPublic);
            if (member.Length == 0 || member[0].GetCustomAttributes(typeof(FxField), false).Length == 0)
                return null;
            return member[0];
        }

        /// <summary>
        /// Set a field of the specified class to the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clazz"></param>
        /// <param name="field"></param>
        /// <param name="fieldType"></param>
        /// <param name="cmd"></param>
        /// <param name="err"></param>
        static private void SetValue<T>(T clazz, object field, Type fieldType,
            Compiler.Command cmd, CompileException err = null)
        {
            // check for errors
            if (cmd.ArgCount == 0)
                err?.Add($"Command '{cmd.Text}' has no arguments (must have at least one).", cmd);
            if (!fieldType.IsArray && cmd.ArgCount > 1)
                err?.Add($"Command '{cmd.Text}' has too many arguments (more than one).", cmd);
            if (err != null && err.HasErrors())
                return;

            object val = null;

            if (fieldType.IsArray)
            {
                var elType = fieldType.GetElementType();
                var array = Array.CreateInstance(elType, cmd.Length);
                for (int i = 0; i < cmd.Length; i++)
                    array.SetValue(Convert.ChangeType(cmd[i].Text, elType, CultureInfo.CurrentCulture), i);
                val = array;
            }
            else
            {
                val = fieldType.IsEnum
                    // if this is an enum, convert the string to an enum value
                    ? Convert.ChangeType(Enum.Parse(fieldType, cmd[0].Text, true), fieldType)
                    // else try to convert it to the field type
                    : Convert.ChangeType(cmd[0].Text, fieldType, CultureInfo.CurrentCulture);
            }

            // set value of the field
            field.GetType()
                .GetMethod("SetValue", new[] { typeof(object), typeof(object) })
                .Invoke(field, new object[] { clazz, val });
        }
        #endregion
    }
}
