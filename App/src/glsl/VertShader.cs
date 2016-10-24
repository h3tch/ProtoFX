using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Glsl
{
    abstract class VertShader : Shader
    {
        #region Input

        protected int gl_VertexID;
        protected int gl_InstanceID;
        private GLVertinput vertexInput;

        #endregion

        #region Output
        
        public vec4 gl_Position;
        public float gl_PointSize;
        public float[] gl_ClipDistance = new float[4];

        #endregion

        private TessShader tess;

        public override void Init(Shader prev, Shader next)
        {
            tess = (TessShader)next;
        }
        
        public Dictionary<string, object> DebugVertex(GLVertinput vertin, int vertexID, int instanceID)
        {
            DebugTrace.Clear();
            TraceLog = DebugTrace;
            var result = GetVertex(vertin, vertexID, instanceID);
            TraceLog = null;
            return result;
        }

        internal Dictionary<string, object> GetVertex(GLVertinput vertin, int vertexID, int instanceID)
        {
            // set shader input
            gl_VertexID = vertexID;
            gl_InstanceID = instanceID;
            vertexInput = vertin;

            // execute shader
            main();

            // get shader output
            var result = new Dictionary<string, object>();
            result.Add("gl_PerVertex", new INOUT {
                gl_Position = gl_Position,
                gl_PointSize = gl_PointSize,
                gl_ClipDistance = gl_ClipDistance
            });
            var props = GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.GetCustomAttribute(typeof(__out__)) != null)
                    result.Add(prop.Name, prop.GetValue(this));
            }
            return result;
        }

        public override T GetInputVarying<T>(int location)
        {
            var data = vertexInput.GetVertexData(gl_VertexID, gl_InstanceID);
            var array = data.Skip(location).First();
            if (new[] { typeof(int), typeof(uint), typeof(float), typeof(double) }.Any(x => x == typeof(T)))
                return (T)(array.To(typeof(T)).GetValue(0));
            var ctor = typeof(T).GetConstructor(new[] { typeof(byte[]) });
            return (T)ctor.Invoke(new[] { array });
        }
    }
}
