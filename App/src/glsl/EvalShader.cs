using System.Collections.Generic;

namespace App.Glsl
{
    abstract class EvalShader : Shader
    {
        #region Input
        
        protected vec3 gl_TessCoord;
        protected int gl_PatchVerticesIn;
        protected int gl_PrimitiveID;
        protected float[] gl_TessLevelOuter = new float[3];
        protected float[] gl_TessLevelInner = new float[1];
        protected INOUT[] gl_in;

        #endregion

        #region Output

        public vec4 gl_Position;
        public float gl_PointSize;
        public float[] gl_ClipDistance;

        #endregion

        private TessShader vert;
        private GeomShader eval;

        public override void Init(Shader prev, Shader next)
        {
            vert = (TessShader)prev;
            eval = (GeomShader)next;
        }

        public Dictionary<string, object> DebugPatch(int primitiveID)
        {
            DebugTrace.Clear();
            TraceLog = DebugTrace;
            var result = GetPatch(primitiveID);
            TraceLog = null;
            return result;
        }

        internal Dictionary<string, object> GetPatch(int primitiveID)
        {
            // set shader input
            gl_PrimitiveID = primitiveID;

            // execute shader
            main();

            // get shader output
            var result = new Dictionary<string, object>();
            return result;
        }
    }
}
