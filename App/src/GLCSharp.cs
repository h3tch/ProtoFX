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
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLCsharp(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"csharp '{name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

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
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete() { }

        /// <summary>
        /// Create a new external class instance by processing the specified compiler block.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="cmd"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public object CreateInstance(Compiler.Block block, Compiler.Command cmd, CompileException err)
        {
            var classname = cmd.Text;

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

        /// <summary>
        /// Create a new external class instance by processing the specified compiler block.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public static object CreateInstance(Compiler.Block block, Dict scene, CompileException err)
        {
            // GET CLASS COMMAND
            var cmds = block["class"].ToList();
            if (cmds.Count == 0)
            {
                err.Add("Instance must specify a 'class' command (e.g., class csharp_name " +
                    "class_name).", block);
                return null;
            }
            var cmd = cmds.First();

            // FIND CSHARP CLASS DEFINITION
            var csharp = scene.GetValueOrDefault<GLCsharp>(cmd[0].Text);
            if (csharp == null)
            {
                err.Add($"Could not find csharp code '{cmd[0].Text}' of command '{cmd.Text}' ", cmd);
                return null;
            }

            // INSTANTIATE CSHARP CLASS
            return csharp.CreateInstance(block, cmd, err);
        }

        /// <summary>
        /// Convert code block commands to dictionary.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private Dictionary<string, string[]> ToDict(Compiler.Block block)
        {
            // convert to dictionary of string arrays
            var dict = new Dictionary<string, string[]>();
            // add commands to dictionary
            block.Do(cmd => dict.Add(cmd.Name, cmd.Select(x => x.Text).ToArray()));
            return dict;
        }
        
        /// <summary>
        /// Invoke a method of an object instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="methodname"></param>
        /// <returns></returns>
        private T InvokeMethod<T>(object instance, string methodname)
        {
            // try to find and invoke the specified method
            var value = instance.GetType().GetMethod(methodname)?.Invoke(instance, new object[] { });
            // if nothing was returned, return the default value
            if (value == null)
                return default(T);
            // if the type of the returned value is not of
            // the required type, return the default value
            return value.GetType() == typeof(T) ? (T)value : default(T);
        }
    }
}
