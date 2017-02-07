using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Globalization;

namespace App
{
    class GLCsharp : GLObject
    {
        #region FIELDS
        [FxField] private string Version = null;
        [FxField] private string[] Assembly = null;
        [FxField] private string[] File = null;
        private CompilerResults CompilerResults;
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
            if (File == null || File.Length == 0)
                return;

            // LOAD ADDITIONAL ASSEMBLIES
            if (Assembly != null)
            {
                foreach (var assemblypath in Assembly)
                {
                    try {
                        System.Reflection.Assembly.LoadFrom(assemblypath);
                    } catch (FileNotFoundException) {
                        err.Add($"Assembly file '{assemblypath}' cound not be found.", block);
                    } catch (FileLoadException) {
                        err.Add($"Assembly '{assemblypath}' cound not be loaded.", block);
                    } catch {
                        err.Add($"Unknown exception when loading assembly '{assemblypath}'.", block);
                    }
                }
            }

            // replace placeholders with actual path
            var dir = Path.GetDirectoryName(block.Filename) + Path.DirectorySeparatorChar;
            var filepath = ProcessPaths(dir, File);

            // COMPILE FILES
            CompilerResults = CompileFilesOrSource(filepath.ToArray(), Version, block, err);

            // check for errors
            if (err.HasErrors())
                throw err;
        }
        
        /// <summary>
        /// Compile a list of files or a list of source code.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="block"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        internal static CompilerResults CompileFilesOrSource(string[] code, string version,
            Compiler.Block block, CompileException err, string[] tmpNames = null)
        {
            CompilerResults rs = null;
            try
            {
                // set compiler parameters and assemblies
                var compilerParams = new CompilerParameters();
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
                var provider = version != null ?
                    new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", version } }) :
                    new CSharpCodeProvider();
                var check = code.Count(x => IsFilename(x));
                if (check == code.Length)
                {
                    rs = provider.CompileAssemblyFromFile(compilerParams, code);
                }
                else if (check == 0)
                {
#if DEBUG
                    Directory.CreateDirectory("tmp");
                    var filenames = new string[code.Length];
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        filenames[i] = $"tmp{Path.DirectorySeparatorChar}tmp_{tmpNames[i]}.cs";
                        if (System.IO.File.Exists(filenames[i]))
                        {
                            var tmp = System.IO.File.ReadAllText(filenames[i]);
                            if (tmp != code[i])
                                System.IO.File.WriteAllText(filenames[i], code[i]);
                        }
                        else
                            System.IO.File.WriteAllText(filenames[i], code[i]);
                    }
                    rs = provider.CompileAssemblyFromFile(compilerParams, filenames);
#else
                    rs = provider.CompileAssemblyFromSource(compilerParams, code);
#endif
                }
                else
                    throw err.Add("Cannot mix filenames and source code"
                        + "strings when compiling C# code.", block);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw err.Add(ex.Message, block);
            }
            catch (FileNotFoundException ex)
            {
                throw err.Add(ex.Message, block);
            }
            finally
            {
                // check for compiler errors
                if (rs?.Errors.Count != 0)
                {
                    string msg = "";
                    foreach (var message in rs.Errors)
                        msg += $"\n{message}";
                    err.Add(msg, block);
                }
            }
            return rs;
        }

        /// <summary>
        /// Process paths to replace predefined placeholders like <code>"<sharp>"</code>.
        /// </summary>
        /// <param name="abspath"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        private IEnumerable<string> ProcessPaths(string abspath, string[] paths)
        {
            // replace placeholders with actual path
            var path = (IEnumerable<string>)paths;
            var curDir = Directory.GetCurrentDirectory() + "/";
            var placeholders = new[] { new[] { "<csharp>", $"{curDir}../csharp" } };
            foreach (var placeholder in placeholders)
                path = path.Select(x => x.Replace(placeholder[0], placeholder[1]));

            // convert relative file paths to absolut file paths
            path = path.Select(x => Path.IsPathRooted(x) ? x : abspath + x);

            // use '\\' file paths instead of '/' and set absolute directory path
            if (Path.DirectorySeparatorChar != '/')
                path = path.Select(x => x.Replace('/', Path.DirectorySeparatorChar));

            return path;
        }

        /// <summary>
        /// Create a new external method-call by processing the specified compiler command.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        internal static MethodInfo GetMethod(Compiler.Command cmd, Dict scene, CompileException err)
        {
            // check command
            if (cmd.ArgCount < 1)
            {
                err.Add("'class' command must specify a csharp object name.", cmd);
                return null;
            }

            // FIND CSHARP CLASS DEFINITION
            var csharp = scene.GetValueOrDefault<GLCsharp>(cmd[0].Text);
            if (csharp == null)
            {
                err.Add($"Could not find csharp code '{cmd[0].Text}' of command '{cmd.Text}' ", cmd);
                return null;
            }

            // INSTANTIATE CSHARP CLASS
            return csharp.GetMethod(cmd, err);
        }

        /// <summary>
        /// Create a new external class instance by processing the specified compiler block.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        internal static object CreateInstance(Compiler.Block block, Dict scene, CompileException err)
        {
            // GET CLASS COMMAND
            var cmds = block["class"].ToList();
            if (cmds.Count == 0)
            {
                err.Add("Instance must specify a 'class' command " +
                    "(e.g., class csharp_name class_name).", block);
                return null;
            }
            var cmd = cmds.First();

            // check command
            if (cmd.ArgCount < 1)
            {
                err.Add("'class' command must specify a csharp object name.", cmd);
                return null;
            }

            // FIND CSHARP CLASS DEFINITION
            var csharp = scene.GetValueOrDefault<GLCsharp>(cmd[0].Text);
            if (csharp == null)
            {
                err.Add($"Could not find csharp code '{cmd[0].Text}' of command '{cmd.Text}' ", cmd);
                return null;
            }

            // INSTANTIATE CSHARP CLASS
            return csharp.CreateInstance(block, cmd, scene, err);
        }

        /// <summary>
        /// Create a new external class instance by processing the specified compiler block.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="cmd"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private object CreateInstance(Compiler.Block block, Compiler.Command cmd, Dict scene, CompileException err)
        {
            // check if the command is valid
            if (cmd.ArgCount < 2)
            {
                err.Add("'class' command must specify a class name.", block);
                return null;
            }
            
            // create OpenGL name lookup dictionary
            var glNames = new Dictionary<string, int>(scene.Count);
            scene.Keys.ForEach(scene.Values, (k, v) => glNames.Add(k, v.glname));

            // create main class from compiled files
            var classname = cmd[1].Text;
            var instance = CompilerResults.CompiledAssembly.CreateInstance(
                classname, false, BindingFlags.Default, null,
                new object[] { block.Name, ToDict(block), glNames }, CultureInfo.CurrentCulture, null);

            if (instance == null)
                throw err.Add($"Main class '{classname}' could not be found.", cmd);
            
            InvokeMethod<List<string>>(instance, "GetErrors")?.ForEach(msg => err.Add(msg, cmd));

            return instance;
        }

        /// <summary>
        /// Create a new external method-call by processing the specified compiler command.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private MethodInfo GetMethod(Compiler.Command cmd, CompileException err)
        {
            // check if the command is valid
            if (cmd.ArgCount < 2)
            {
                err.Add("'class' command must specify a class name.", cmd);
                return null;
            }
            if (cmd.ArgCount < 3)
            {
                err.Add("'class' command must specify a method name.", cmd);
                return null;
            }

            var classname = cmd[1].Text;
            var methodname = cmd[2].Text;
            var type = CompilerResults.CompiledAssembly.GetType(classname);
            return type?.GetMethod(methodname, BindingFlags.Public | BindingFlags.Static);
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
            block.ForEach(cmd => dict.Add(cmd.Name, cmd.Select(x => x.Text).ToArray()));
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

        /// <summary>
        /// Check if the string is a filename.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsFilename(string str) => str.IndexOf('\n') < 0 && System.IO.File.Exists(str);
    }
}
