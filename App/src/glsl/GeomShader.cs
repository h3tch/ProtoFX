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
    }
}
