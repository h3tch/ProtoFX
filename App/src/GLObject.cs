using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Reflection.BindingFlags;

namespace App
{
    /// <summary>
    /// The <code>Field</code> attribute class is used to identify
    /// fields that can receive values from the application at compile time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class Field : Attribute { }
    
    abstract class GLObject
    {
        public int glname { get; protected set; }
        [Field] public string name { get; protected set; }
        public string anno { get; protected set; }

        public GLObject(string name, string anno)
        {
            this.glname = 0;
            this.name = name;
            this.anno = anno;
        }
        
        public static string GetLabel(ObjectLabelIdentifier type, int glname)
        {
            int length = 64;
            var label = new StringBuilder(length);
            GL.GetObjectLabel(type, glname, length, out length, label);
            return label.ToString();
        }

        public abstract void Delete();

        public override string ToString() => name;

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

        protected void Cmds2Fields(Compiler.Block block, CompileException err = null)
        {
            var type = GetType();

            foreach (var cmd in block)
            {
                // try to find a field with the respective name
                var field = type.GetField(cmd.Name, Instance | Public | NonPublic);
                var prop = type.GetProperty(cmd.Name, Instance | Public | NonPublic);
                MemberInfo member = (MemberInfo)field ?? prop;

                // if no field could be found go to the next command
                if (member == null || member.GetCustomAttributes(typeof(Field), false).Length == 0)
                    continue;

                // set value of field
                object val = (object)field ?? prop;
                Type valtype = field?.FieldType ?? prop?.PropertyType;
                SetValue(this, val, valtype, cmd, err);
            }
        }

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

            var val = fieldType.IsArray 
                // if this is an array pass the arguments as string
                ? cmd.Select(x => x.Text).ToArray()
                // if this is an enumerable type
                : fieldType.IsEnum
                    // if this is an enum, convert the string to an enum value
                    ? Convert.ChangeType(Enum.Parse(fieldType, cmd[0].Text, true), fieldType)
                    // else try to convert it to the field type
                    : Convert.ChangeType(cmd[0].Text, fieldType, App.culture);

            // set value of the field
            field.GetType()
                .GetMethod("SetValue", new[] { typeof(object), typeof(object) })
                .Invoke(field, new object[] { clazz, val });
        }
    }
}
