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
        [GLField] private string version = null;
        [GLField] private string[] file = null;
        private CompilerResults compilerresults;
        #endregion

        public GLCsharp(string dir, string name, string annotation, string text, Dict<GLObject> classes)
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

            // replace placeholders with actual path
            var path = (IEnumerable<string>)file;
            var curDir = Directory.GetCurrentDirectory() + "/";
            var placeholders = new[] { new[] { "<csharp>", curDir + "../csharp" } };
            foreach (var placeholder in placeholders)
                path = path.Select(x => x.Replace(placeholder[0], placeholder[1]));

            // convert relative file paths to absolut file paths
            path = path.Select(x => Path.IsPathRooted(x) ? x : dir + x);

            // use '\\' file paths instead of '/' and set absolute directory path
            if (Path.DirectorySeparatorChar != '/')
                path = path.Select(x => x.Replace('/', Path.DirectorySeparatorChar));
            
            // compile files
            try
            {
                // set compiler parameters and assemblies
                CompilerParameters compilerParams = new CompilerParameters();
                compilerParams.GenerateInMemory = true;
                compilerParams.GenerateExecutable = false;
                #if DEBUG
                compilerParams.IncludeDebugInformation = true;
                #else
                compilerParams.IncludeDebugInformation = false;
                #endif
                compilerParams.ReferencedAssemblies.AddRange(
                    AppDomain.CurrentDomain.GetAssemblies()
                             .Where(a => !a.IsDynamic)
                             .Select(a => a.Location).ToArray());

                // select compiler version
                CSharpCodeProvider provider = version != null ? 
                    new CSharpCodeProvider(new Dictionary<string, string> { {"CompilerVersion", version} }) :
                    new CSharpCodeProvider();
                compilerresults = provider.CompileAssemblyFromFile(compilerParams, path.ToArray());
            }
            catch (DirectoryNotFoundException ex)
            {
                err.Throw(ex.Message);
            }

            // check for compiler errors
            if (compilerresults.Errors.Count != 0)
            {
                string msg = "";
                foreach (var message in compilerresults.Errors)
                    msg += "\n" + message;
                err.Throw(msg);
            }
        }

        public object CreateInstance(string classname, string name, Dictionary<string,string[]> cmds,
            GLException err = null)
        {
            // create main class from compiled files
            object instance = compilerresults.CompiledAssembly.CreateInstance(
                classname, false, BindingFlags.Default, null,
                new object[] { name, cmds }, App.culture, null);

            if (instance == null)
                err?.Throw($"Main class '{classname}' could not be found.");
            
            List<string> errors = InvokeMethod<List<string>>(instance, "GetErrors");
            errors?.ForEach(msg => err?.Add(msg));

            return instance;
        }

        private T InvokeMethod<T>(object instance, string valuename)
        {
            var value = instance.GetType().GetMethod(valuename)?.Invoke(instance, new object[] { });
            if (value == null)
                return default(T);
            return value.GetType() == typeof(T) ? (T)value : default(T);
        }

        public override void Delete() { }
    }
}
