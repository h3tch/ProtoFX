using System.Collections.Generic;

namespace App.Glsl
{
    abstract class GeomShader : Shader
    {
        #region Input
        
        protected int gl_PrimitiveIDIn;
        protected int gl_InvocationID;
        protected INOUT[] gl_in;

        #endregion

        #region Output

        public int gl_PrimitiveID;
        public int gl_Layer;
        public int gl_ViewportIndex;
        public vec4 gl_Position;
        public float gl_PointSize;
        public float[] gl_ClipDistance;

        #endregion

        private EvalShader vert;
        private FragShader frag;

        public override void Init(Shader prev, Shader next)
        {
            vert = (EvalShader)prev;
            frag = (FragShader)next;
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
