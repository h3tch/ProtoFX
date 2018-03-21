using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace protofx.gl
{
    class Instance : Object
    {
        public object Owner = null;
        public bool VisualizeAsBuffer = false;
        public bool VisualizeAsImage = false;
        private MethodInfo update = null;
        private MethodInfo endpass = null;
        private MethodInfo delete = null;
        private MethodInfo visualize = null;

        /// <summary>
        /// Generic constructor used to build the scene objects.
        /// </summary>
        /// <param name="params">A class containing all the parameters
        /// needed to instantiate the class. The GLInstance class requires a
        /// <code>Compiler.Block</code> object of the respective part in the code.</param>
        public Instance(object @params)
            : this(@params.GetFieldValue<Compiler.Block>(), @params)
        {
        }

        /// <summary>
        /// Create class instance of a C# class compiled through GLCSharp.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        private Instance(Compiler.Block block, object @params)
            : base(block.Name, block.Anno)
        {
            var scene = @params.GetFieldValue<Dictionary<string, object>>();
            var err = new CompileException($"instance '{Name}'");

            // INSTANTIATE CSHARP CLASS FROM CODE BLOCK
            Owner = Csharp.CreateInstance(block, scene, @params, err);
            if (err.HasErrors)
                throw err;

            // get Bind method from main class instance
            update = Owner.GetType().GetMethod("Update", new[] {
                typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)
            });

            // get Unbind method from main class instance
            endpass = Owner.GetType().GetMethod("EndPass", new[] { typeof(int) });

            // get Delete method from main class instance
            delete = Owner.GetType().GetMethod("Delete");

            // get Visualize method from main class instance
            visualize = Owner.GetType().GetMethod("Visualize");
            if (visualize != null)
            {
                if (visualize.ReturnType.IsAssignableFrom(typeof(byte[])))
                    VisualizeAsBuffer = true;
                if (visualize.ReturnType.IsAssignableFrom(typeof(Bitmap)))
                    VisualizeAsImage = true;
            }

            // get all public methods and check whether
            // they can be used as event handlers for glControl
            var reference = (Reference)scene.GetValueOrDefault(GraphicControl.nullname);
            var glControl = (GraphicControl)reference.Ref;
            var methods = Owner.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods)
            {
                var info = glControl.GetType().GetEvent(method.Name);
                if (info != null)
                {
                    var csmethod = Delegate.CreateDelegate(info.EventHandlerType, Owner, method.Name);
                    info.AddEventHandler(glControl, csmethod);
                }
            }
        }

        /// <summary>
        /// Call update method of the external class instance.
        /// </summary>
        /// <param name="program">Program/pass calling the method.</param>
        /// <param name="width">Width of the OpenGL framebuffer.</param>
        /// <param name="height">Height of the OpenGL framebuffer.</param>
        /// <param name="widthTex">Width of the render target.</param>
        /// <param name="heightTex">Height of the render target.</param>
        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            update?.Invoke(Owner, new object[] { program, width, height, widthTex, heightTex });
        }

        /// <summary>
        /// Call end-pass method of the external class instance.
        /// </summary>
        /// <param name="program">Program/pass calling the method.</param>
        public void EndPass(int program)
        {
            endpass?.Invoke(Owner, new object[] { program });
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            delete?.Invoke(Owner, null);
        }

        public object Visualize()
        {
            return visualize?.Invoke(Owner, null);
        }
    }
}
