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
    }
}
