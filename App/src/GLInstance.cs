using OpenTK;
using System;
using System.Linq;
using System.Reflection;

namespace App
{
    class GLInstance : GLObject
    {
        public object instance = null;
        private MethodInfo update = null;
        private MethodInfo endpass = null;
        private MethodInfo delete = null;

        /// <summary>
        /// Create class instance of a C# class compiled through GLCSharp.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLInstance(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"instance '{name}'");

            // GET CLASS COMMAND
            var cmds = block["class"].ToList();
            if (cmds.Count == 0)
                throw err.Add("Instance must specify a 'class' command (e.g., class csharp_name " +
                    "class_name).", block);
            var cmd = cmds.First();

            // FIND CSHARP CLASS DEFINITION
            var csharp = scene.GetValue<GLCsharp>(cmd[0].Text);
            if (csharp == null)
            {
                err.Add($"Could not find csharp code '{cmd[0].Text}' of command '{cmd.Text}' ", cmd);
                return;
            }

            // INSTANTIATE CSHARP CLASS
            instance = csharp.CreateInstance(block, cmd, err);
            if (err.HasErrors())
                throw err;

            // get Bind method from main class instance
            update = instance.GetType().GetMethod("Update", new[] {
                typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)
            });

            // get Unbind method from main class instance
            endpass = instance.GetType().GetMethod("EndPass", new[] { typeof(int) });

            // get Delete method from main class instance
            delete = instance.GetType().GetMethod("Delete");

            // get all public methods and check whether
            // they can be used as event handlers for glControl
            GLReference reference = scene.GetValue<GLReference>(GraphicControl.nullname);
            GraphicControl glControl = (GraphicControl)reference.reference;
            var methods = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods)
            {
                var info = glControl.GetType().GetEvent(method.Name);
                if (info != null)
                {
                    var csmethod = Delegate.CreateDelegate(info.EventHandlerType, instance, method.Name);
                    info.AddEventHandler(glControl, csmethod);
                }
            }
        }
        
        public void Update(int program, int width, int height, int widthTex, int heightTex)
            => update?.Invoke(instance, new object[] { program, width, height, widthTex, heightTex });

        public void EndPass(int program) => endpass?.Invoke(instance, new object[] { program });

        public override void Delete() => delete?.Invoke(instance, null);
    }
}
