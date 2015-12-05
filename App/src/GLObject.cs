using OpenTK.Graphics.OpenGL4;

namespace App
{
    abstract class GLObject
    {
        public int glname { get; protected set; }
        [Field] public string name { get; protected set; }
        public string anno { get; protected set; }

        /// <summary>
        /// Instantiate and initialize object.
        /// </summary>
        /// <param name="name">Name used to identify the object.</param>
        /// <param name="annotation">Annotation used for special initialization.</param>
        public GLObject(string name, string annotation)
        {
            this.glname = 0;
            this.name = name;
            this.anno = annotation;
        }

        public abstract void Delete();

        public override string ToString() => name;
        
        static protected bool HasErrorOrGlError(CompileException err)
        {
            var errcode = GL.GetError();
            if (errcode != ErrorCode.NoError)
            {
                err?.Add($"OpenGL error '{errcode}' occurred during buffer allocation.");
                return true;
            }
            return err != null ? err.HasErrors() : false;
        }
    }
}
