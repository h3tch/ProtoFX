using System.Reflection;
using System.Text.RegularExpressions;

namespace App
{
    class FXGlslToCSharp
    {
        public string Process(string text)
        {
            var methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var method in methods)
                text = (string)method.Invoke(null, new[] { text });

            return text;
        }

        private string Version(string text) => Regex.Replace(text, "#version [0-9]{3}", "");

        private string Floats(string text) => Regex.Replace(text, @"\bfloat\b", "double");

        private string Layouts(string text) => Regex.Replace(text, @"layout\s*\(.*\)", "");

        private string Constants(string text)
        {
            Match  match;

            while (true)
            {
                match = Regex.Match(text, @"const\s+\w+\s+[\w\d]+\s*=\s*[\w\d.]+;");
                if (match == null)
                    break;
                var index = text.IndexOf('=', match.Index);
                text = text.Insert(index + 1, ">");
            }

            return Regex.Replace(text, @"\bconst\b", "");
        }

        private string Uniforms(string text) => Regex.Replace(text, "\buniform\b", "");

        private string UniformBuffers(string text)
        {
            Match match;

            while (true)
            {
                match = Regex.Match(text, @"uniform[\s\w\d]+\{[\s\w\d\[\];]*\}[\s\w\d]*;");
                if (match == null)
                    break;
                var sub = text.Substring(match.Index, match.Length);

                sub = sub.Replace("uniform", "struct");

                text = text.Remove(match.Index, match.Length).Insert(match.Index, sub);
            }
            
            return text;
        }

        private string UniformSamplers(string text) => Regex.Replace(text, @"layout\s*\(.*\)\s*uniform", "");

        private string Inputs(string text) => Regex.Replace(text, "\bin\b", "");

        private string Outputs(string text) => Regex.Replace(text, "\bout\b", "");
    }

    /*class fs_render : VertexShader
    {
        [layout(binding = 0) uniform] struct SpotLight {
            public mat4 viewProj;
            public vec4 camera;
            public vec4 light;
        } SpotLight light;

        [layout(binding = 2) uniform] struct PoissonDisc {
            public vec2[] points;
            public int numPoints;
        } PoissonDisc disc;

        [layout(binding = 0) uniform] sampler2D shadowmap;
    
        [layout(location = 0) IN] vec4 in_lpos;
        [layout(location = 1) IN] vec4 in_col;
        [OUT] vec4 color;

        double PI => 3.14159265358979;
        double lightR => light.light.y;
        double zNear => light.camera.z;
        double zFar => light.camera.w;
        vec2 fovVec => light.light.zw;

        double rand(vec2 xy) {
            return fract(sin(dot(xy, vec2(12.9898,78.233))) * 43758.5453);
        }

        mat2 randRot(double angle) {
            double sa = sin(angle);
            double ca = cos(angle);
            return mat2(ca, -sa, sa, ca);
        }
    
        vec2 depthNormalBias(vec3 lpos) {
            vec3 normal = cross(dFdx(lpos), dFdy(lpos));
            return vec2(1,-1) * normal.xy / normal.z;
        }

        double light2plane(double value, double zReceiver) {
            return value * (zReceiver - zNear) / zReceiver;
        }

        vec2 plane2tex(double value) {
            return vec2(value) / fovVec;
        }

        double light2penumbra(double value, double zBlocker, double zReceiver) {
            return value * (zReceiver - zBlocker) / zBlocker;
        }

        double penumbra2plane(double value, double zReceiver) {
            return value * zNear / zReceiver;
        }

        double bias(vec2 zChange, vec2 offset) {
            return dot(zChange, offset) - 0.001;
        }

        double exp2linDepth(double z) {
            return zFar * zNear / (zFar - z * (zFar - zNear));  
        }

        double BlockerSearch(vec2 filterR, vec3 lpos, vec2 surfBias) {
            int nBlocker = 0;
            double zBlocker = 0.0;
            mat2 R = randRot(2 * PI * rand(lpos.xy));

            for (int i = 0; i < disc.numPoints; i++) {
                vec2 p = R * disc.points[i] * filterR;
                double z = texture(shadowmap, lpos.xy + p).x;
                //z = exp2linDepth(z);
                if (lpos.z + bias(surfBias, p) > z) {
                    zBlocker += z;
                    nBlocker++;
                }
            }
            //return zBlocker / float(nBlocker);
            return exp2linDepth(zBlocker / (double)(nBlocker));
        }

        double PCF(vec2 filterR, vec3 lpos, vec2 surfBias) {
            // PCF SHADOW MAPPING
            double shadow = 0.0f;
            mat2 R = randRot(2 * PI * rand(lpos.xy));

            for (int i = 0; i < disc.numPoints; i++) {
                vec2 p = R * disc.points[i] * filterR;
                double z = texture(shadowmap, lpos.xy + p).x;
                //z = exp2linDepth(z);
                shadow += lpos.z + bias(surfBias, p) > z ? 1.0 : 0.0;
            }

            return shadow / (double)(disc.numPoints);
        }

        double PCSS(vec2 surfBias, vec3 lpos) {
            double receiverZ = exp2linDepth(lpos.z);

            // BLOCKER SEARCH
            double planeR = light2plane(lightR, receiverZ);
            vec2 filterR = plane2tex(planeR);
            double zBlocker = BlockerSearch(filterR, lpos, surfBias);
            if (isnan(zBlocker))
                return 0;//zBlocker = lpos.z;

            // PCF SHADOW MAPPING
            double penumbraR = light2penumbra(lightR, zBlocker, receiverZ);
            planeR = penumbra2plane(penumbraR, receiverZ);
            filterR = plane2tex(planeR);
            return PCF(filterR, lpos, surfBias);
        }
    
        void main () {
            // pass color
            color = in_col;
        
            // if pixel outside light frustum clip space, do nothing
            if (any(greaterThan(abs(in_lpos), in_lpos.wwww)))
                return;
            
            // SHADOW MAPPING
        
            // transform to normalized device coordinates [-1;+1]
            // and then to shadow map texture coordinates [0;1]
            vec3 lpos = (in_lpos.xyz / in_lpos.w) * 0.5 + 0.5;
        
            vec3 normal = cross(dFdx(lpos),dFdy(lpos));
            if (normal.z < 0)
                return;
            
            vec2 surfBias = -normal.xy / normal.z;

            double shadow = PCSS(surfBias, lpos);
            
            if (shadow == 0.0)
                return;
        
            color.xyz *= (1.0 - shadow) * 0.5 + 0.5;
        }
    }*/
}