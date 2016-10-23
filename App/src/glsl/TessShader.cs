using System.Collections.Generic;

namespace App.Glsl
{
    abstract class TessShader : Shader
    {
        #region Input

        protected int gl_PatchVerticesIn;
        protected int gl_PrimitiveID;
        protected int gl_InvocationID;
        protected INOUT[] gl_in;

        #endregion

        #region Output

        public float[] gl_TessLevelOuter = new float[3];
        public float[] gl_TessLevelInner = new float[3];
        public INOUT[] gl_out;

        #endregion

        private VertShader vert;
        private EvalShader eval;

        public override void Init(Shader prev, Shader next)
        {
            vert = (VertShader)prev;
            eval = (EvalShader)next;
        }

        public Dictionary<string, object> DebugPatch(int primitiveID, int invocationID)
        {
            DebugTrace.Clear();
            TraceLog = DebugTrace;
            var result = GetPatch(primitiveID, invocationID);
            TraceLog = null;
            return result;
        }

        internal Dictionary<string, object> GetPatch(int primitiveID, int invocationID)
        {
            // set shader input
            gl_PrimitiveID = primitiveID;
            gl_InvocationID = invocationID;

            // execute shader
            main();

            // get shader output
            var result = new Dictionary<string, object>();
            return result;
        }
    }
}
