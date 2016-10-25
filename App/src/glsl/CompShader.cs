namespace App.Glsl
{
    class CompShader : Shader
    {
        #region Field

        public static readonly CompShader Default = new CompShader();

        #endregion

#pragma warning disable 0649
#pragma warning disable 0169

        #region Input

        protected uvec3 gl_NumWorkGroups;
        protected uvec3 gl_WorkGroupSize;
        protected uvec3 gl_WorkGroupID;
        protected uvec3 gl_LocalInvocationID;
        protected uvec3 gl_GlobalInvocationID;
        protected uint gl_LocalInvocationIndex;

        #endregion

#pragma warning restore 0649
#pragma warning restore 0169

        public void Debug()
        {
            BeginTracing();
            EndTracing();
        }
    }
}
