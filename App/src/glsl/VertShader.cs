using OpenTK.Graphics.OpenGL4;
using System.Linq;

namespace App.Glsl
{
    public class VertShader : Shader
    {
        #region Fields

        public static readonly VertShader Default = new VertShader(0);

        #endregion

#pragma warning disable 0649
#pragma warning disable 0169

        #region Input

        protected int gl_VertexID;
        protected int gl_InstanceID;

        #endregion

        #region Output
        
        [__out] public vec4 gl_Position;
        [__out] public float gl_PointSize;
        [__out] public float[] gl_ClipDistance = new float[4];

        #endregion

#pragma warning restore 0649
#pragma warning restore 0169

        #region Constructors

        public VertShader() : this(0) { }

        public VertShader(int startLine) : base(startLine) { }

        #endregion

        internal void Debug()
        {
            if (this != Default)
                BeginTracing();
            Execute(Settings.vs_VertexID, Settings.vs_InstanceID);
            EndTracing();
        }

        internal void Execute(int vertexID, int instanceID)
        {
            // set shader input
            gl_VertexID = vertexID;
            gl_InstanceID = instanceID;
            // execute shader
            main();
        }

        public override T GetInputVarying<T>(string varyingName)
        {
            if (drawcall == null)
                return default(T);

            // get current shader program
            var program = GL.GetInteger(GetPName.CurrentProgram);
            if (program <= 0)
                return default(T);

            // get vertex input attribute location
            var location = GL.GetAttribLocation(program, varyingName);
            if (location < 0)
                return default(T);

            // get vertex input data
            var data = drawcall?.vertin.GetVertexData(gl_VertexID, gl_InstanceID);
            var array = data?.Skip(location).First();
            if (array == null)
                return default(T);

            // return default type
            if (BaseTypes.Any(x => x == typeof(T)))
                return (T)array.To(typeof(T)).GetValue(0);
            
            // create new object from byte array
            return (T)typeof(T).GetConstructor(new[] { typeof(byte[]) })?.Invoke(new[] { array });
        }
    }
}
