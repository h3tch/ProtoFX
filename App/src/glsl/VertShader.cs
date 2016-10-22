namespace App.Glsl
{
    abstract class VertShader : Shader
    {
        #region Input

        protected int gl_VertexID;
        protected int gl_InstanceID;
        protected vec4 gl_Position = new vec4(0, 0, 0, 1);

        #endregion

        #region Output

        public float gl_PointSize;
        public float[] gl_ClipDistance;

        #endregion

        private TessShader tess;

        public override void Init(Shader prev, Shader next)
        {
            tess = (TessShader)next;
        }
    }

}
