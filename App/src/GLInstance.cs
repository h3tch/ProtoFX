﻿using System;
using System.Linq;
using System.Reflection;

namespace App
{
    class GLInstance : GLObject
    {
        private object instance = null;
        private MethodInfo update = null;
        private MethodInfo endpass = null;

        public GLInstance(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            var err = new GLException($"instance '{name}'");

            // PARSE TEXT TO COMMANDS
            var body = new Commands(text, err);

            // GET CLASS COMMAND
            var cmds = body["class"].ToList();
            if (cmds.Count == 0)
                err.Throw("Instance must specify a 'class' command (e.g., class csharp_name class_name).");
            var cmd = cmds.First();

            // FIND CSHARP CLASS DEFINITION
            var csharp = classes.FindClass<GLCsharp>(cmd.args[0]);
            if (csharp == null)
            {
                err.Add($"Could not find csharp code '{cmd.args[0]}' of command '{cmd.cmd} "
                    + string.Join(" ", cmd.args) + "'.");
                return;
            }

            // INSTANTIATE CSHARP CLASS
            instance = csharp.CreateInstance(cmd.args[1], body.ToDict());
            if (instance == null)
            {
                err.Add($"Main class '{cmd.args[1]}' could not be found.");
                return;
            }

            // get bind method from main class instance
            update = instance.GetType().GetMethod("Update", new [] {
                typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)
            });

            // get unbind method from main class instance
            endpass = instance.GetType().GetMethod("EndPass", new [] { typeof(int) });
                
            // get all public methods and check whether
            // they can be used as event handlers for glControl
            GraphicControl glControl = classes.FindClass<GraphicControl>(GraphicControl.nullname);
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
        {
            if (update != null)
                update.Invoke(instance, new object[] { program, width, height, widthTex, heightTex });
        }

        public void EndPass(int program)
        {
            if (endpass != null)
                endpass.Invoke(instance, new object[] { program });
        }

        public override void Delete() { }
    }
}
