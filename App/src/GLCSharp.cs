using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System;

namespace App
{
    class GLCsharp : GLObject
    {
        #region FIELDS
        [FxField] private string Version = null;
        [FxField] private string[] Assembly = null;
        [FxField] private string[] File = null;
        [FxField] private string[] Folder = null;
        private CompilerResults CompilerResults;
        #endregion

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLCsharp(Compiler.Block block, Dictionary<string, object> scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"csharp '{Name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(block, err);

            // check for errors
            if (err.HasErrors)
                throw err;
            if ((File == null || File.Length == 0) && (Folder == null || Folder.Length == 0))
                return;

            // LOAD ADDITIONAL ASSEMBLIES
            if (Assembly != null)
            {
                foreach (var assemblypath in Assembly)
                {
                    try {
                        System.Reflection.Assembly.LoadFrom(assemblypath);
                    } catch (FileNotFoundException) {
                        err.Error($"Assembly file '{assemblypath}' cound not be found.", block);
                    } catch (FileLoadException) {
                        err.Error($"Assembly '{assemblypath}' cound not be loaded.", block);
                    } catch {
                        err.Error($"Unknown exception when loading assembly '{assemblypath}'.", block);
                    }
                }
            }

            // replace placeholders with actual path
            var dir = Path.GetDirectoryName(block.Filename) + Path.DirectorySeparatorChar;
            var folders = ProcessPaths(dir, Folder);
            var filepath = ProcessPaths(dir, File).Concat(GetCSharpFiles(folders));
            var unique = new HashSet<string>(filepath).ToArray();

            // COMPILE FILES
            CompilerResults = CompileFilesOrSource(filepath.ToArray(), Version, block, err);

            // check for errors
            if (err.HasErrors)
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
                // set compicompiler parameters
                var compilerParams = new CompilerParameters()
                {
                    GenerateInMemory = true,
                    GenerateExecutable = false,
                    TempFiles = new TempFileCollection("tmp", false),
#if DEBUG
                    IncludeDebugInformation = true,
                    CompilerOptions = "/define:DEBUG",
#else
                    IncludeDebugInformation = false,
#endif
                };
                
                // add assemblies
                compilerParams.ReferencedAssemblies.Add(
                    System.Reflection.Assembly.GetExecutingAssembly().Location);
                compilerParams.ReferencedAssemblies.AddRange(
                    Properties.Resources.CSHARP_REFERENCES.Split('\n')
                    .Select(s => s.Trim()).ToArray());

                // select compiler version
                var provider = version != null
                    ? new CSharpCodeProvider(new Dictionary<string, string> {
                        { "CompilerVersion", version }
                    })
                    : new CSharpCodeProvider();
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
                {
                    throw err.Error("Cannot mix filenames and source code"
                        + "strings when compiling C# code.", block);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                throw err.Error(ex.Message, block);
            }
            catch (FileNotFoundException ex)
            {
                throw err.Error(ex.Message, block);
            }
            finally
            {
                // check for compiler errors
                if (rs?.Errors.Count != 0)
                {
                    foreach (var message in rs.Errors)
                        err.Error(message.ToString(), block);
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
            if (paths != null)
            {
                var curDir = Directory.GetCurrentDirectory() + "/";
                var placeholders = new[] { new[] { "<csharp>", $"{curDir}../csharp" } };

                foreach (var path in paths)
                {
                    var s = (string)path.Clone();
                    // replace placeholders with actual path
                    foreach (var placeholder in placeholders)
                        s = s.Replace(placeholder[0], placeholder[1]);
                    // convert relative file paths to absolut file paths
                    if (!Path.IsPathRooted(s))
                        s = abspath + s;
                    // use '\\' file paths instead of '/' and set absolute directory path
                    if (Path.DirectorySeparatorChar != '/')
                        s = s.Replace('/', Path.DirectorySeparatorChar);
                    yield return s;
                }
            }
        }

        /// <summary>
        /// Create a new external method-call by processing the specified compiler command.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        internal static MethodInfo GetMethod(Compiler.Command cmd, Dictionary<string, object> scene, CompileException err)
        {
            // check command
            if (cmd.ArgCount < 1)
            {
                err.Error("'class' command must specify a csharp object name.", cmd);
                return null;
            }

            // FIND CSHARP CLASS DEFINITION
            var csharp = scene.GetValueOrDefault(cmd[0].Text);
            if (csharp == null || !(csharp is GLCsharp))
            {
                err.Error($"Could not find csharp code '{cmd[0].Text}' of command '{cmd.Text}' ", cmd);
                return null;
            }

            // INSTANTIATE CSHARP CLASS
            return ((GLCsharp)csharp).GetMethod(cmd, err);
        }

        /// <summary>
        /// Create a new external class instance by processing the specified compiler block.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        internal static object CreateInstance(Compiler.Block block, Dictionary<string, object> scene, CompileException err)
        {
            // GET CLASS COMMAND
            var cmds = block["class"].ToList();
            if (cmds.Count == 0)
            {
                err.Error("Instance must specify a 'class' command " +
                    "(e.g., class csharp_name class_name).", block);
                return null;
            }
            var cmd = cmds.First();

            // check command
            if (cmd.ArgCount < 1)
            {
                err.Error("'class' command must specify a csharp object name.", cmd);
                return null;
            }

            // FIND CSHARP CLASS DEFINITION
            var csharp = scene.GetValueOrDefault(cmd[0].Text);
            if (csharp == null)
            {
                err.Error($"Could not find csharp code '{cmd[0].Text}' of command '{cmd.Text}' ", cmd);
                return null;
            }

            // INSTANTIATE CSHARP CLASS
            return ((GLCsharp)csharp).CreateInstance(block, cmd, scene, err);
        }

        /// <summary>
        /// Create a new external class instance by processing the specified compiler block.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="cmd"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private object CreateInstance(Compiler.Block block, Compiler.Command cmd, Dictionary<string, object> scene, CompileException err)
        {
            // check if the command is valid
            if (cmd.ArgCount < 2)
            {
                err.Error("'class' command must specify a class name.", block);
                return null;
            }
            
            // create OpenGL name lookup dictionary
            var glNames = new Dictionary<string, int>(scene.Count);
            scene.Keys.ForEach(scene.Values, (k, v) => glNames.Add(k, ((GLObject)v).glname));

            // create main class from compiled files
            var classname = cmd[1].Text;
            var instance = CompilerResults.CompiledAssembly.CreateInstance(
                classname, false, BindingFlags.Default, null,
                new object[] { block.Name, ToLookup(block), scene, glNames },
                CultureInfo.CurrentCulture, null);

            if (instance == null)
                throw err.Error($"Main class '{classname}' could not be found.", cmd);

            InvokeMethod<object>(instance, "Initialize");
            InvokeMethod<List<string>>(instance, "GetErrors")?.ForEach(msg => err.Error(msg, cmd));

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
                err.Error("'class' command must specify a class name.", cmd);
                return null;
            }
            if (cmd.ArgCount < 3)
            {
                err.Error("'class' command must specify a method name.", cmd);
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
        private ILookup<string, string[]> ToLookup(Compiler.Block block)
        {
            // add commands to dictionary
            return block.ToLookup(cmd => cmd.Name, cmd => cmd.Select(x => x.Text).ToArray());
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
        private static bool IsFilename(string str)
        {
            return str.IndexOf('\n') < 0 && System.IO.File.Exists(str);
        }

        private static IEnumerable<string> GetCSharpFiles(IEnumerable<string> folders)
        {
            foreach (var folder in folders)
            {
                if (!Directory.Exists(folder) || folder.IndexOf('\n') >= 0)
                    continue;
                foreach (var file in Directory.GetFiles(folder))
                {
                    if (file.EndsWith(".cs", StringComparison.CurrentCultureIgnoreCase))
                        yield return file;
                }
            }
        }
    }
}
