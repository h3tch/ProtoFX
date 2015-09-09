﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenTK;

namespace gled
{
    class GLCsharp : GLObject
    {
        #region FIELDS

        public string version = null;
        public string[] file = null;
        private CompilerResults compilerresults;

        #endregion

        public GLCsharp(string name, string annotation, string text, Dictionary<string, GLObject> classes)
            : base(name, annotation)
        {
            // PARSE TEXT
            var args = Text2Cmds(text);

            // PARSE ARGUMENTS
            Cmds2Fields(this, ref args);

            // check for errors
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
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>
            {
                {"CompilerVersion", version != null ? version : "v4.0"}
            });

            // use '\\' file paths instead of '/'
            for (int i = 0; i < file.Length; i++)
                file[i] = file[i].Replace('/', Path.DirectorySeparatorChar);

            // compile files
            compilerresults = provider.CompileAssemblyFromFile(compilerParams, file);

            // check for compiler errors
            if (compilerresults.Errors.Count != 0)
                throw new Exception("ERROR in csharp " + name + ":\n" + compilerresults.Output);
        }

        public object CreateInstance(string classname, string[] args)
        {
            // create main class from compiled files
            object clazz = compilerresults.CompiledAssembly.CreateInstance(
                classname, false, BindingFlags.Default, null, new object[] { args }, App.culture, null);
            if (clazz == null)
                throw new Exception("INTERNAL_ERROR in csharp " + name + ": Main class '" + classname + "' could not be found.");

            return clazz;
        }

        public override void Delete()
        {
        }
    }
}
