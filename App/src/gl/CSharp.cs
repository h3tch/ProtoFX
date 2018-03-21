using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System;
using System.Text.RegularExpressions;

namespace protofx.gl
{
    class Csharp : Object
    {
        #region FIELDS
        [FxField] private string Version = null;
        [FxField] private string[] Assembly = null;
        [FxField] private string[] File = null;
        [FxField] private string[] Folder = null;
        private CompilerResults CompilerResults;
        #endregion

        /// <summary>
        /// Generic constructor used to build the scene objects.
        /// </summary>
        /// <param name="params">A class containing all the parameters
        /// needed to instantiate the class. The GLCsharp class requires a
        /// <code>Compiler.Block</code> object of the respective part in the code
        /// and a <code>Dictionary&lt;string, object&gt;</code> object containing
        /// the scene objects.</param>
        public Csharp(object @params)
            : this(@params.GetFieldValue<Compiler.Block>(),
                   @params.GetFieldValue<Dictionary<string, object>>())
        {
        }

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        private Csharp(Compiler.Block block, Dictionary<string, object> scene)
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
                        err.Error($"Assembly file '{assemblypath}' could not be found.", block);
                    } catch (FileLoadException) {
                        err.Error($"Assembly '{assemblypath}' could not be loaded.", block);
                    } catch {
                        err.Error($"Unknown exception when loading assembly '{assemblypath}'.", block);
                    }
                }
            }

            // replace placeholders with actual path
            var dir = Path.GetDirectoryName(block.Filename) + Path.DirectorySeparatorChar;
            var files = GetCSharpFiles(ProcessPaths(dir, Folder), ProcessPaths(dir, File));

            // COMPILE FILES
            CompilerResults = CompileFilesOrSource(files, Version, block, err);

            // check for errors
            if (err.HasErrors)
                throw err;
        }

        public Csharp(string[] folders) : base("__extensions__", null)
        {
            var err = new CompileException($"csharp '{Name}'");

            Folder = folders;

            // COMPILE FILES
            CompilerResults = CompileFilesOrSource(GetCSharpFiles(Folder, true).ToArray());

            // check for compiler errors
            if (CompilerResults?.Errors.Count != 0)
            {
                foreach (CompilerError message in CompilerResults.Errors)
                {
                    if (message.IsWarning)
                        err.Info(message.ErrorText, message.FileName, message.Line);
                    else
                        err.Error(message.ErrorText, message.FileName, message.Line);
                }
            }
        }

        public IEnumerable<TypeInfo> GetExtensions()
        {
            return CompilerResults?.CompiledAssembly.GetTypesByAttribute("FxAttribute");
        }

        public Type GetType(string name)
        {
            return CompilerResults?.CompiledAssembly.GetType(name, false, true);
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
                rs = CompileFilesOrSource(code, version, tmpNames);
            }
            catch (InvalidOperationException ex)
            {
                throw err.Error(ex.Message, block);
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
                    foreach (CompilerError message in rs.Errors)
                    {
                        if (message.IsWarning)
                            err.Info(message.ErrorText, message.FileName, message.Line);
                        else
                            err.Error(message.ErrorText, message.FileName, message.Line);
                    }
                }
            }
            return rs;
        }

        /// <summary>
        /// Compile a list of files or a list of source code.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="block"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public static CompilerResults CompileFilesOrSource(string[] code,
            string version = null, string[] tmpNames = null)
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
                return provider.CompileAssemblyFromFile(compilerParams, code);
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
                return provider.CompileAssemblyFromFile(compilerParams, filenames);
#else
                return provider.CompileAssemblyFromSource(compilerParams, code);
#endif
            }
            else
            {
                throw new InvalidOperationException("Cannot mix filenames and " +
                    "source code strings when compiling C# code.");
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
            if (csharp == null || !(csharp is Csharp))
            {
                err.Error($"Could not find csharp code '{cmd[0].Text}' of command '{cmd.Text}' ", cmd);
                return null;
            }

            // INSTANTIATE CSHARP CLASS
            return ((Csharp)csharp).GetMethod(cmd, err);
        }

        /// <summary>
        /// Create a new external class instance by processing the specified compiler block.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        internal static object CreateInstance(Compiler.Block block, Dictionary<string, object> scene, object @params, CompileException err)
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
            // check if the command is valid
            if (cmd.ArgCount < 2)
            {
                err.Error("'class' command must specify a class name.", block);
                return null;
            }
            var classname = cmd[1].Text;

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
            return ((Csharp)csharp).CreateInstance(block, classname, @params, err);
        }

        /// <summary>
        /// Create a new external class instance by processing the specified compiler block.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="cmd"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private object CreateInstance(Compiler.Block block, string classname, object @params, CompileException err)
        {
            // create main class from compiled files
            var instance = CompilerResults.CompiledAssembly.CreateInstance(
                classname, false, BindingFlags.Default, null,
                new object[] { @params },
                CultureInfo.CurrentCulture, null);

            if (instance == null)
                throw err.Error($"Main class '{classname}' could not be found.", block);

            InvokeMethod<object>(instance, "Initialize");
            GetField<List<string>>(instance, "Errors")?.ForEach(msg => err.Error(msg, block));
            GetField<List<string>>(instance, "Warnings")?.ForEach(msg => err.Info(msg, block));
            GetField<List<string>>(instance, "Infos")?.ForEach(msg => err.Info(msg, block));

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
        /// Invoke a method of an object instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="methodname"></param>
        /// <returns></returns>
        private T InvokeMethod<T>(object instance, string methodname)
        {
            // try to find and invoke the specified method
            var value = instance.GetType()
                .GetMethod(methodname, BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(instance, null);
            // if nothing was returned, return the default value
            if (value == null)
                return default(T);
            // if the type of the returned value is not of
            // the required type, return the default value
            return value.GetType() == typeof(T) ? (T)value : default(T);
        }

        /// <summary>
        /// Get a property from the specified instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        private T GetField<T>(object instance, string fieldname)
        {
            // try to find and invoke the specified method
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var value = instance.GetType().GetField(fieldname, flags)?.GetValue(instance);
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

        public static string[] GetCSharpFiles(IEnumerable<string> folders, IEnumerable<string> files)
        {
            folders = folders != null ? GetCSharpFiles(folders) : null;

            if (folders != null && files != null)
                return new HashSet<string>(files.Concat(folders)).ToArray();
            else if (folders != null)
                return new HashSet<string>(folders).ToArray();
            else if (files != null)
                return new HashSet<string>(files).ToArray();
            return new string[0];
        }

        private static IEnumerable<string> GetCSharpFiles(IEnumerable<string> folders,
            bool includeSubFolders = false)
        {
            var obt = includeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var folder in folders)
            {
                if (!Directory.Exists(folder) || folder.IndexOf('\n') >= 0)
                    continue;
                foreach (var file in Directory.GetFiles(folder, "*.cs", obt))
                    yield return file;
            }
        }

        /// <summary>
        /// Process paths to replace predefined placeholders like <code>"<sharp>"</code>.
        /// </summary>
        /// <param name="abspath"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        private static IEnumerable<string> ProcessPaths(string abspath, IEnumerable<string> paths)
        {
            if (paths != null)
            {
                var curDir = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;

                foreach (var path in paths)
                {
                    var s = (string)path.Clone();
                    // replace placeholders with actual path
                    var match = Regex.Match(s, @"<[\w\d]+>");
                    if (match.Success && match.Index == 0)
                        s = $"{curDir}../{s.Substring(1, match.Length - 2) + s.Substring(match.Length)}";
                    // convert relative file paths to absolute file paths
                    if (!Path.IsPathRooted(s))
                        s = abspath + s;
                    // use '\\' file paths instead of '/' and set absolute directory path
                    if (Path.DirectorySeparatorChar != '/')
                        s = s.Replace('/', Path.DirectorySeparatorChar);
                    yield return s;
                }
            }
        }
    }
}
