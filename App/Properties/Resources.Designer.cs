﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("App.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  0 default             #C0C0C0
        /// 1 block               #0000FF
        /// 2 annotation          #0099CC
        /// 3 command             #6666FF
        /// 4 argument            #6633CC
        /// 5 GLSLtype            #0000FF
        /// 6 GLSLfunction        #993333
        /// 7 GLSLlayoutqualifier #0099CC
        /// 8 GLSLflowcontrol     #0000FF
        /// 9 GLSLbuiltin         #0000FF
        ///10 opengl              #993333
        ///11 identifier          #000000
        ///12 number              #108030
        ///13 string              #800000
        ///14 char                #A03030
        ///15 linecomment         #7F9F00
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string colors {
            get {
                return ResourceManager.GetString("colors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #define _DBG_BOOL 1
        ///#define _DBG_INT 2
        ///#define _DBG_UINT 3
        ///#define _DBG_FLOAT 4
        ///#define _i2f intBitsToFloat
        ///#define _u2f uintBitsToFloat
        ///const int _dbgStageOffset = &lt;&lt;stage offset&gt;&gt;;
        ///
        ///layout(rgba32f) uniform writeonly imageBuffer _dbgOut;
        ///
        ///int _dbgStore(int idx, vec4 val) {
        ///	imageStore(_dbgOut, _dbgStageOffset + idx, val);
        ///	return ++idx;
        ///}
        ///int _dbgStore(int idx, ivec4 val) {
        ///	return _dbgStore(idx, vec4(_i2f(val.x), _i2f(val.y), _i2f(val.z), _i2f(val.w)));
        ///}
        ///int _dbgStore(int idx, uvec4 val) [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string dbg {
            get {
                return ResourceManager.GetString("dbg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &gt;&gt;.
        /// </summary>
        internal static string DBG_CLOSE {
            get {
                return ResourceManager.GetString("DBG_CLOSE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to all(equal(_dbgVert, ivec2(gl_InstanceID, gl_VertexID)))
        ///all(equal(_dbgTess, ivec2(gl_InvocationID, gl_PrimitiveID)))
        ///_dbgEval == gl_PrimitiveID
        ///all(equal(_dbgGeom, ivec2(gl_PrimitiveIDIn, gl_InvocationID)))
        ///all(equal(_dbgFrag, ivec4(int(gl_FragCoord.x), int(gl_FragCoord.y), gl_Layer, gl_ViewportIndex)))
        ///all(equal(_dbgComp, gl_GlobalInvocationID)).
        /// </summary>
        internal static string DBG_CONDITIONS {
            get {
                return ResourceManager.GetString("DBG_CONDITIONS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;&lt;.
        /// </summary>
        internal static string DBG_OPEN {
            get {
                return ResourceManager.GetString("DBG_OPEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ivec2 _dbgVert
        ///ivec2 _dbgTess
        ///int _dbgEval
        ///ivec2 _dbgGeom
        ///ivec4 _dbgFrag
        ///uvec3 _dbgComp.
        /// </summary>
        internal static string DBG_UNIFORMS {
            get {
                return ResourceManager.GetString("DBG_UNIFORMS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform &lt;&lt;debug uniform&gt;&gt;;
        ///uniform &lt;&lt;debug frame&gt;&gt;;
        ///
        ///void _dbgMain() {
        ///	int _dbgIdx = 1;
        ///	&lt;&lt;debug code&gt;&gt;
        ///	_dbgStore(0, ivec2(_dbgIdx-1, _dbgFrame));
        ///}
        ///
        ///void _runMain() {
        ///	&lt;&lt;runtime code&gt;&gt;
        ///}
        ///
        ///void main() {
        ///	if (&lt;&lt;debug condition&gt;&gt;)
        ///		_dbgMain();
        ///	else
        ///		_runMain();
        ///}.
        /// </summary>
        internal static string dbgBody {
            get {
                return ResourceManager.GetString("dbgBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgClose {
            get {
                object obj = ResourceManager.GetObject("ImgClose", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgComment {
            get {
                object obj = ResourceManager.GetObject("ImgComment", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgDbg {
            get {
                object obj = ResourceManager.GetObject("ImgDbg", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgNew {
            get {
                object obj = ResourceManager.GetObject("ImgNew", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgOpen {
            get {
                object obj = ResourceManager.GetObject("ImgOpen", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgPick {
            get {
                object obj = ResourceManager.GetObject("ImgPick", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgRun {
            get {
                object obj = ResourceManager.GetObject("ImgRun", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgSave {
            get {
                object obj = ResourceManager.GetObject("ImgSave", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgSaveAll {
            get {
                object obj = ResourceManager.GetObject("ImgSaveAll", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgSaveAs {
            get {
                object obj = ResourceManager.GetObject("ImgSaveAs", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ImgUncomment {
            get {
                object obj = ResourceManager.GetObject("ImgUncomment", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ┬
        ///├1║buffer║buffer &lt;name&gt;¶
        ///├1║csharp║assembly &lt;path&gt; [path] [...]¶
        ///├1║fragoutput║fragoutput &lt;name&gt;¶
        ///├1║image║image &lt;name&gt;¶
        ///├1║instance║instance &lt;name&gt;¶
        ///├1║pass║pass &lt;name&gt;¶
        ///├1║sampler║sampler &lt;name&gt;¶
        ///├1║shader║shader &lt;shader_type&gt; &lt;name&gt;¶
        ///├1║tech║tech &lt;name&gt;¶
        ///├1║text║text &lt;name&gt;¶
        ///├1║texture║texture &lt;name&gt;¶
        ///├1║vertinput║vertinput &lt;name&gt;¶
        ///├1║vertoutput║vertoutput &lt;name&gt;¶
        ///├┬buffer||{|}
        ///│├3║size║size &lt;bytes&gt;¶
        ///│├3║xml║xml &lt;path&gt; &lt;node&gt;¶
        ///│├3║txt║txt &lt;text_name&gt;¶
        ///│├3║usage║usage &lt;usage_hint&gt;¶
        ///│├ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string keywords {
            get {
                return ResourceManager.GetString("keywords", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string keywordsGLSL {
            get {
                return ResourceManager.GetString("keywordsGLSL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to settings.xml.
        /// </summary>
        internal static string WINDOW_SETTINGS_FILE {
            get {
                return ResourceManager.GetString("WINDOW_SETTINGS_FILE", resourceCulture);
            }
        }
    }
}
