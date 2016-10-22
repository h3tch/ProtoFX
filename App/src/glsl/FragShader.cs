namespace App.Glsl
{
    abstract class FragShader : Shader
    {
        #region Input

        protected vec4 gl_FragCoord;
        protected bool gl_FrontFacing;
        protected vec2 gl_PointCoord;
        protected int gl_SampleID;
        protected vec2 gl_SamplePosition;
        protected int[] gl_SampleMaskIn;
        protected float gl_ClipDistance;
        protected int gl_PrimitiveID;
        protected int gl_Layer;
        protected int gl_ViewportIndex;

        #endregion

        #region Output

        public float gl_FragDepth;
        public int[] gl_SampleMask;

        #endregion
        
        private GeomShader geom;

        public override void Init(Shader prev, Shader next)
        {
            geom = (GeomShader)prev;
        }
    }
}
