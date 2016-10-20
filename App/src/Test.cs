using System.Linq;

namespace App.Glsl
{
    class fs_render : frag {
    
        class SpotLight
        {
            public mat4 viewProj = Glsl.mat4.Identity;
            public vec4 camera = vec4(0, 0, 0, 1);
            public vec4 light = vec4(0, 0, 0, 1);
        }
        SpotLight light = new SpotLight();


        class PoissonDisc
        {
            public vec2[] points = Enumerable.Repeat(new vec2(0), 25).ToArray();
            public int numPoints = 25;
        }
        PoissonDisc disc = new PoissonDisc();
        
        sampler2D shadowmap = 0;
        
        vec4 in_lpos = vec4(0, 0, 0, 1);
        vec4 in_col = vec4(1);
        vec4 color = vec4(1);

        float PI => 3.14159265358979f;
        float lightR => light.light.y;
        float zNear => light.camera.z;
        float zFar => light.camera.w;
        vec2 fovVec => light.light.zw;

        float rand(vec2 xy)
        {
            return fract(sin(dot(xy, vec2(12.9898f, 78.233f))) * 43758.5453f);
        }

        mat2 randRot(float angle)
        {
            float sa = sin(angle);
            float ca = cos(angle);
            return mat2(ca, -sa, sa, ca);
        }

        vec2 depthNormalBias(vec3 lpos)
        {
            vec3 normal = cross(dFdx(lpos), dFdy(lpos));
            return vec2(1, -1) * normal.xy / normal.z;
        }

        float light2plane(float value, float zReceiver)
        {
            return value * (zReceiver - zNear) / zReceiver;
        }

        vec2 plane2tex(float value)
        {
            return vec2(value) / fovVec;
        }

        float light2penumbra(float value, float zBlocker, float zReceiver)
        {
            return value * (zReceiver - zBlocker) / zBlocker;
        }

        float penumbra2plane(float value, float zReceiver)
        {
            return value * zNear / zReceiver;
        }

        float bias(vec2 zChange, vec2 offset)
        {
            return dot(zChange, offset) - 0.001f;
        }

        float exp2linDepth(float z)
        {
            return zFar * zNear / (zFar - z * (zFar - zNear));
        }

        float BlockerSearch(vec2 filterR, vec3 lpos, vec2 surfBias)
        {
            int nBlocker = 0;
            float zBlocker = 0.0f;
            mat2 R = randRot(2 * PI * rand(lpos.xy));

            for (int i = 0; i < disc.numPoints; i++)
            {
                vec2 p = R * disc.points[i] * filterR;
                float z = texture(shadowmap, lpos.xy + p).x;
                //z = exp2linDepth(z);
                if (lpos.z + bias(surfBias, p) > z)
                {
                    zBlocker += z;
                    nBlocker++;
                }
            }
            //return zBlocker / float(nBlocker);
            return exp2linDepth(zBlocker / (float)(nBlocker));
        }

        float PCF(vec2 filterR, vec3 lpos, vec2 surfBias)
        {
            // PCF SHADOW MAPPING
            float shadow = 0.0f;
            mat2 R = randRot(2 * PI * rand(lpos.xy));

            for (int i = 0; i < disc.numPoints; i++)
            {
                vec2 p = R * disc.points[i] * filterR;
                float z = texture(shadowmap, lpos.xy + p).x;
                //z = exp2linDepth(z);
                shadow += lpos.z + bias(surfBias, p) > z ? 1.0f : 0.0f;
            }

            return shadow / (float)(disc.numPoints);
        }

        float PCSS(vec2 surfBias, vec3 lpos)
        {
            float receiverZ = exp2linDepth(lpos.z);

            // BLOCKER SEARCH
            float planeR = light2plane(lightR, receiverZ);
            vec2 filterR = plane2tex(planeR);
            float zBlocker = BlockerSearch(filterR, lpos, surfBias);
            if (isnan(zBlocker))
                return 0;//zBlocker = lpos.z;

            // PCF SHADOW MAPPING
            float penumbraR = light2penumbra(lightR, zBlocker, receiverZ);
            planeR = penumbra2plane(penumbraR, receiverZ);
            filterR = plane2tex(planeR);
            return PCF(filterR, lpos, surfBias);
        }

        public void main()
        {
            // pass color
            color = in_col;

            // if pixel outside light frustum clip space, do nothing
            if (any(greaterThan(abs(in_lpos), in_lpos.wwww)))
                return;

            // SHADOW MAPPING

            // transform to normalized device coordinates [-1;+1]
            // and then to shadow map texture coordinates [0;1]
            vec3 lpos = (in_lpos.xyz / in_lpos.w) * 0.5f + 0.5f;

            vec3 normal = cross(dFdx(lpos), dFdy(lpos));
            if (normal.z < 0)
                return;

            vec2 surfBias = -normal.xy / normal.z;

            float shadow = PCSS(surfBias, lpos);

            if (shadow == 0.0)
                return;

            color.xyz *= (1.0f - shadow) * 0.5f + 0.5f;
        }
    }
}
