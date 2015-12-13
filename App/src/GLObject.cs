using OpenTK.Graphics.OpenGL4;

namespace App
{
    struct GLParams
    {
        public string name;
        public string anno;
        public string text;
        public string dir;
        public Dict<GLObject> scene;
        public GLParams(string name)
        {
            this.name = name;
            this.anno = null;
            this.text = null;
            this.dir = null;
            this.scene = null;
        }
        public GLParams(string name, string anno)
        {
            this.name = name;
            this.anno = anno;
            this.text = null;
            this.dir = null;
            this.scene = null;
        }
        public GLParams(string name, string anno, string text, string dir, Dict<GLObject> scene)
        {
            this.name = name;
            this.anno = anno;
            this.text = text;
            this.dir = dir;
            this.scene = scene;
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
