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
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public class Field : System.Attribute { }

    struct GLParams
    {
        public string name;
        public string anno;
        public string text;
        public int nameLine;
        public int namePos;
        public string file;
        public int cmdLine;
        public int cmdPos;
        public string dir;
        public Dict scene;
        public bool debugging;
        public GLParams(
            string name = null, 
            string anno = null,
            string text = null,
            string file = null,
            int nameLine = -1,
            int namePos = -1,
            int cmdLine = -1,
            int cmdPos = -1,
            string dir = null, 
            Dict scene = null,
            bool debugging = false)
        {
            this.name = name;
            this.anno = anno;
            this.text = text;
            this.nameLine = nameLine;
            this.namePos = namePos;
            this.file = file;
            this.cmdLine = cmdLine;
            this.cmdPos = cmdPos;
            this.dir = dir;
            this.scene = scene;
            this.debugging = debugging;
        }
    }

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
        
        protected static string GetLable(ObjectLabelIdentifier type, int glname)
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

        protected void Cmds2Fields<T>(T clazz, Compiler.Block block, CompileException err = null)
        {
            var type = clazz.GetType();

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
                SetValue(clazz, val, valtype, cmd, err);
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

            var val = fieldType.IsArray ?
                // if this is an array pass the arguments as string
                cmd.Select(x => x.Text).ToArray() :
                fieldType.IsEnum ?
                    // if this is an enum, convert the string to an enum value
                    Convert.ChangeType(Enum.Parse(fieldType, cmd[0].Text, true), fieldType) :
                    // else try to convert it to the field type
                    Convert.ChangeType(cmd[0].Text, fieldType, App.culture);

            var SetValue = field.GetType().GetMethod(
                "SetValue", new Type[] { typeof(object), typeof(object) });

            SetValue.Invoke(field, new object[] { clazz, val });
        }
    }
}
