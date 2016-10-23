using System.Collections.Generic;
using System.Reflection;

namespace App.Glsl
{
    abstract class VertShader : Shader
    {
        #region Input

        protected int gl_VertexID;
        protected int gl_InstanceID;

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

        public Dictionary<string, object> DebugVertex(int vertexID, int instanceID)
        {
            DebugTrace.Clear();
            TraceLog = DebugTrace;
            var result = GetVertex(vertexID, instanceID);
            TraceLog = null;
            return result;
        }

        internal Dictionary<string, object> GetVertex(int vertexID, int instanceID)
        {
            // set shader input
            gl_VertexID = vertexID;
            gl_InstanceID = instanceID;
            SetVertexAttributes(vertexID);

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

        private void SetVertexAttributes(int vertexID)
        {

        }
    }
}
