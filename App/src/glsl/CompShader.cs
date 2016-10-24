namespace App.Glsl
{
    abstract class CompShader : Shader
    {
        #region Input

        protected uvec3 gl_NumWorkGroups;
        protected uvec3 gl_WorkGroupSize;
        protected uvec3 gl_WorkGroupID;
        protected uvec3 gl_LocalInvocationID;
        protected uvec3 gl_GlobalInvocationID;
        protected uint gl_LocalInvocationIndex;

        #endregion
    }
}
