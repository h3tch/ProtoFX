using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace gled
{
    class GLCsharp : GLObject
    {
        public string version = null;
        public string main = null;
        public string[] file = null;
        private object csclass = null;
        private MethodInfo bind = null;
        private MethodInfo unbind = null;

        public GLCsharp(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Args(text);

            // PARSE ARGUMENTS
            Args2Prop(this, ref args);

            if (main == null || file == null || file.Length == 0)
                return;
            
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>
            {
                {"CompilerVersion", version != null ? version : "v4.0"}
            });

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                            .Where(a => !a.IsDynamic)
                            .Select(a => a.Location);
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateInMemory = true;
            compilerParams.GenerateExecutable = false;
            compilerParams.IncludeDebugInformation = true;
            compilerParams.ReferencedAssemblies.AddRange(assemblies.ToArray());

            for (int i = 0; i < file.Length; i++)
                file[i] = file[i].Replace('/', Path.DirectorySeparatorChar);
            CompilerResults results = provider.CompileAssemblyFromFile(compilerParams, file);

            if (results.Errors.Count != 0)
                throw new Exception("ERROR in csharp " + name + ":\n" + results.Output);

            GLObject obj;
            if (!classes.TryGetValue(GledControl.nullname, out obj) || obj.GetType() != typeof(GledControl))
                throw new Exception("INTERNAL_ERROR in csharp " + name + ": OpenGL control could not be found.");
            GledControl control = (GledControl)obj;

            csclass = results.CompiledAssembly.CreateInstance(main);
            if (csclass == null)
                throw new Exception("INTERNAL_ERROR in csharp " + name + ": Main class '" + main + "' could not be found.");

            bind = csclass.GetType().GetMethod("Bind", new Type[] { typeof(int) });
            if (bind == null)
                throw new Exception("INTERNAL_ERROR in csharp " + name + ": Main class method 'Bind' could not be found.");

            unbind = csclass.GetType().GetMethod("Unbind", new Type[] { typeof(int) });
            if (unbind == null)
                throw new Exception("INTERNAL_ERROR in csharp " + name + ": Main class method 'Unbind' could not be found.");

            var methods = csclass.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods)
            {
                EventInfo eventInfo = control.control.GetType().GetEvent(method.Name);
                if (eventInfo != null)
                {
                    Delegate csmethod = Delegate.CreateDelegate(eventInfo.EventHandlerType, csclass, method.Name);
                    eventInfo.AddEventHandler(control.control, csmethod);
                }
            }
        }

        public void Bind(int program)
        {
            bind.Invoke(csclass, new object[] { program });
        }

        public void Unbind(int program)
        {
            unbind.Invoke(csclass, new object[] { program });
        }

        public override void Delete()
        {
        }
    }
}
