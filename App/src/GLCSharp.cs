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
        [Field] private string version = null;
        [Field] private string[] file = null;
        private CompilerResults compilerresults;
        #endregion

        /// <summary>
        /// Create C# object compiler class.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLCsharp(Compiler.Block block, Dict<GLObject> scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"csharp '{name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(this, block, err);

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
            var dir = Path.GetDirectoryName(block.File) + Path.DirectorySeparatorChar;
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
                compilerParams.CompilerOptions = "/define:DEBUG";
#else
                compilerParams.IncludeDebugInformation = false;
#endif
                compilerParams.ReferencedAssemblies.AddRange(
                    AppDomain.CurrentDomain.GetAssemblies()
                             .Where(a => !a.IsDynamic)
                             .Select(a => a.Location).ToArray());

                // select compiler version
                CSharpCodeProvider provider = version != null ?
                    new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", version } }) :
                    new CSharpCodeProvider();
                compilerresults = provider.CompileAssemblyFromFile(compilerParams, path.ToArray());
            }
            catch (DirectoryNotFoundException ex)
            {
                throw err.Add(ex.Message, block);
            }

            // check for compiler errors
            if (compilerresults.Errors.Count != 0)
            {
                string msg = "";
                foreach (var message in compilerresults.Errors)
                    msg += "\n" + message;
                throw err.Add(msg, block);
            }
        }

        /// <summary>
        /// Create C# object compiler class.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLCsharp(GLParams @params) : base(@params)
        {
            var err = new CompileException($"csharp '{@params.name}'");

            // PARSE TEXT
            var cmds = new Commands(@params.text, @params.file, @params.cmdLine, @params.cmdPos, err);

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
            path = path.Select(x => Path.IsPathRooted(x) ? x : @params.dir + x);

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
                compilerParams.CompilerOptions = "/define:DEBUG";
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
                throw err.Add(ex.Message, @params.file, @params.nameLine, @params.namePos);
            }

            // check for compiler errors
            if (compilerresults.Errors.Count != 0)
            {
                string msg = "";
                foreach (var message in compilerresults.Errors)
                    msg += "\n" + message;
                throw err.Add(msg, @params.file, @params.nameLine, @params.namePos);
            }
        }
        
        public object CreateInstance(Compiler.Block block, Compiler.Command cmd, CompileException err)
        {
            var classname = cmd[1].Text;

            // create main class from compiled files
            var instance = compilerresults.CompiledAssembly.CreateInstance(
                classname, false, BindingFlags.Default, null,
                new object[] { block.Name, ToDict(block) }, App.culture, null);

            if (instance == null)
                throw err.Add($"Main class '{classname}' could not be found.", cmd);
            
            List<string> errors = InvokeMethod<List<string>>(instance, "GetErrors");
            errors?.ForEach(msg => err.Add(msg, cmd));

            return instance;
        }

        private Dictionary<string, string[]> ToDict(Compiler.Block block)
        {
            // convert triple to dict of string arrays
            var dict = new Dictionary<string, string[]>();
            foreach (var cmd in block)
                dict.Add(cmd.Name, cmd.Select(x => x.Text).ToArray());
            return dict;
        }

        /// <summary>
        /// Create an instance of a C# class.
        /// </summary>
        /// <param name="classname">Name of the class type (class classname { ... }).</param>
        /// <param name="name">Name to identify the class instance.</param>
        /// <param name="cmds">Commands to pass to the class constructor.</param>
        /// <param name="err">[OPTIONAL] Error and exception collector.</param>
        /// <returns></returns>
        public object CreateInstance(string classname, string name, Dictionary<string, string[]> cmds,
            string file, int line, int pos, CompileException err)
        {
            // create main class from compiled files
            var instance = compilerresults.CompiledAssembly.CreateInstance(
                classname, false, BindingFlags.Default, null,
                new object[] { name, cmds }, App.culture, null);

            if (instance == null)
                throw err.Add($"Main class '{classname}' could not be found.", file, line, pos);

            List<string> errors = InvokeMethod<List<string>>(instance, "GetErrors");
            errors?.ForEach(msg => err.Add(msg, file, line, pos));

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
