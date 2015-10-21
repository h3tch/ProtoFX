using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLCsharp : GLObject
    {
        #region FIELDS
        [GLField]
        private string version = null;
        [GLField]
        private string[] file = null;
        private CompilerResults compilerresults;
        #endregion

        public GLCsharp(string dir, string name, string annotation, string text, Dict classes)
            : base(name, annotation)
        {
            var err = new GLException($"csharp '{name}'");

            // PARSE TEXT
            var cmds = new Commands(text, err);

            // PARSE ARGUMENTS
            cmds.Cmds2Fields(this, err);

            // check for errors
            if (err.HasErrors())
                throw err;
            if (file == null || file.Length == 0)
                return;

            // select all app assemplies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                            .Where(a => !a.IsDynamic)
                            .Select(a => a.Location);

            // set compiler parameters and assemblies
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateInMemory = true;
            compilerParams.GenerateExecutable = false;
            compilerParams.IncludeDebugInformation = true;
            compilerParams.ReferencedAssemblies.AddRange(assemblies.ToArray());
            
            // select compiler version
            CSharpCodeProvider provider = new CSharpCodeProvider(
                new Dictionary<string, string> {
                    {"CompilerVersion", version != null ? version : "v4.0"}
                });

            // use '\\' file paths instead of '/' and set absolute directory path
            for (int i = 0; i < file.Length; i++)
                // use correct directory separator
                file[i] = (Path.IsPathRooted(file[i]) ? file[i] : dir + file[i])
                          .Replace('/', Path.DirectorySeparatorChar);
            
            // compile files
            compilerresults = provider.CompileAssemblyFromFile(compilerParams, file);

            // check for compiler errors
            if (compilerresults.Errors.Count != 0)
                err.Throw("\n" + compilerresults.Output);
        }

        public object CreateInstance(string classname, Dictionary<string,string[]> cmds,
            GLException err = null)
        {
            // create main class from compiled files
            object instance = compilerresults.CompiledAssembly.CreateInstance(
                classname, false, BindingFlags.Default, null,
                new object[] { cmds }, App.culture, null);

            if (instance == null)
                err.Throw($"csharp '{name}': Main class '{classname}' could not be found.");
            
            List<string> errors = GetValue<List<string>>(instance, "errors");
            errors?.ForEach(msg => err?.Add(msg));

            return instance;
        }

        private T GetValue<T>(object instance, string valuename)
        {
            var value = instance.GetType().GetField(valuename)?.GetValue(instance);
            if (value == null)
                return default(T);
            return value.GetType() == typeof(T) ? (T)value : default(T);
        }

        public override void Delete() { }
    }
}
