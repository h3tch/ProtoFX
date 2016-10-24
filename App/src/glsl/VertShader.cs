using OpenTK.Graphics.OpenGL4;
using System.Linq;

namespace App.Glsl
{
    abstract class VertShader : Shader
    {
        #region Input

        protected int gl_VertexID;
        protected int gl_InstanceID;

        #endregion

        #region Output
        
        [__out] public vec4 gl_Position;
        [__out] public float gl_PointSize;
        [__out] public float[] gl_ClipDistance = new float[4];

        #endregion

        internal void Debug()
        {
            DebugTrace.Clear();
            TraceLog = DebugTrace;
            Execute(Settings.vs_VertexID, Settings.vs_InstanceID);
            TraceLog = null;
        }

        internal void Execute(int vertexID, int instanceID)
        {
            // set shader input
            gl_VertexID = vertexID;
            gl_InstanceID = instanceID;
            // execute shader
            main();
        }

        internal override T GetInputVarying<T>(string varyingName)
        {
            var program = GL.GetInteger(GetPName.CurrentProgram);
            var location = GL.GetAttribLocation(program, varyingName);
            var data = drawcall.vertin.GetVertexData(gl_VertexID, gl_InstanceID);
            var array = data.Skip(location).First();
            if (new[] { typeof(int), typeof(uint), typeof(float), typeof(double) }.Any(x => x == typeof(T)))
                return (T)(array.To(typeof(T)).GetValue(0));
            var ctor = typeof(T).GetConstructor(new[] { typeof(byte[]) });
            return (T)ctor.Invoke(new[] { array });
        }
    }
}
