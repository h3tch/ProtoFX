using OpenTK.Graphics.OpenGL4;
using System.Text;

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
        public string cmdText;
        public int namePos;
        public int cmdPos;
        public string dir;
        public Dict<GLObject> scene;
        public bool debuging;
        public GLParams(
            string name = null, 
            string anno = null,
            string cmdText = null,
            int namePos = -1,
            int cmdPos = -1,
            string dir = null, 
            Dict<GLObject> scene = null,
            bool debuging = false)
        {
            this.name = name;
            this.anno = anno;
            this.cmdText = cmdText;
            this.namePos = namePos;
            this.cmdPos = cmdPos;
            this.dir = dir;
            this.scene = scene;
            this.debuging = debuging;
        }
    }

    abstract class GLObject
    {
        public int glname { get; protected set; }
        [Field] public string name { get; protected set; }
        public string anno { get; protected set; }

        /// <summary>
        /// Instantiate and initialize object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLObject(GLParams @params)
        {
            this.glname = 0;
            this.name = @params.name;
            this.anno = @params.anno;
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
        
        static protected bool HasErrorOrGlError(CompileException err, int pos = -1)
        {
            var errcode = GL.GetError();
            if (errcode != ErrorCode.NoError)
            {
                err?.Add($"OpenGL error '{errcode}' occurred.", pos);
                return true;
            }
            return err != null ? err.HasErrors() : false;
        }
    }
}
