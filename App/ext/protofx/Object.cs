﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace protofx
{
    /// <summary>
    /// This is the base object of which every 
    /// extension needs to be derived from.
    /// </summary>
    class Object
    {
        #region FIELDS

        public static CultureInfo EN = new CultureInfo("en");
        public static CultureInfo culture = EN;
        private Dictionary<string, object> uniformBlocks = new Dictionary<string, object>();
        protected List<string> Errors = new List<string>();
        protected ILookup<string, string[]> commands;
        protected Dictionary<string, object> objects;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cmds"></param>
        /// <param name="objs"></param>
        public Object(object @params)
        {
            commands = @params.GetMemberValue<ILookup<string, string[]>>();
            objects = @params.GetMemberValue<Dictionary<string, object>>();
        }

        /// <summary>
        /// Search in the specified shader pipeline for the
        /// specified uniform block name.
        /// </summary>
        /// <param name="pipeline"></param>
        /// <param name="name"></param>
        /// <returns>The uniform block or 'null' if the block could not be found.</returns>
        public UniformBlock GetUniformBlock(int pipeline, string name, string[] variableNames)
        {
            object unif = null;
            var key = name + "(" + pipeline + ")";
            if (uniformBlocks.TryGetValue(key, out unif))
                return (UniformBlock)unif;

            if (UniformBlock.HasUnifromBlock(pipeline, name))
                unif = new UniformBlock(pipeline, name, variableNames);
            uniformBlocks.Add(key, unif);
            return (UniformBlock)unif;
        }

        /// <summary>
        /// Search for command and convert in into an array.
        /// The result is written to 'v'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmds"></param>
        /// <param name="cmd"></param>
        /// <param name="v"></param>
        protected void Convert<T>(ILookup<string, string[]> cmds, string cmd, ref T[] v)
        {
            int i = 0, l;

            int length = v.Length;
            
            var s = cmds[cmd].FirstOrDefault();
            if (s == null)
                return;

            for (l = Math.Min(s.Length, length); i < s.Length; i++)
            {
                if (!TryChangeType(s[i], ref v[i]))
                    Errors.Add("Command '" + cmd + "': Could not convert argument "
                        + (i + 1) + " '" + s[i] + "'.");
            }
        }

        /// <summary>
        /// Search for command and convert in into the specified type.
        /// The result is written to 'v'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmds"></param>
        /// <param name="cmd"></param>
        /// <param name="v"></param>
        protected void Convert<T>(ILookup<string, string[]> cmds, string cmd, ref T v)
        {
            var s = cmds[cmd].FirstOrDefault();
            if (s == null)
                return;
            if (!TryChangeType(s[0], ref v))
                Errors.Add("Command '" + cmd + "': Could not convert argument 1 '" + s[0] + "'.");
        }

        /// <summary>
        /// Try to change the input to the specified type.
        /// The result is written to 'v'. If the conversion failed,
        /// the method will return 'false'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool TryChangeType<T>(object input, ref T value)
        {
            if (input == null || input as IConvertible == null)
                return false;

            try
            {
                value = (T)System.Convert.ChangeType(input, typeof(T), culture);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    static class ObjectExtensions
    {
        /// <summary>
        /// Get the value of a field or property using a string (e.g.,
        /// "obj1.field3.property3", "obj1.TypeName.property3",
        /// "obj1.field3.property[1]", ...) and cast the result
        /// to the specified return type.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="obj"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TReturn GetMemberValue<TReturn>(this object obj, string name)
        {
            return (TReturn)obj.GetMemberValue(name);
        }

        /// <summary>
        /// Get the first member of the specified type.
        /// Fields are preferred over properties.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TReturn GetMemberValue<TReturn>(this object obj)
        {
            TReturn value = obj.GetFieldValue<TReturn>();
            if (value == null)
                value = obj.GetPropertyValue<TReturn>();
            return value;
        }

        /// <summary>
        /// Get the value of a field or property using a string (e.g.,
        /// "obj1.field3.property3", "obj1.TypeName.property3",
        /// "obj1.field3.property[1]", ...).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="text"></param>
        public static object GetMemberValue(this object obj, string name)
        {
            var value = obj.GetFieldValue(name);
            if (value == null)
                value = obj.GetPropertyValue(name);
            return value;
        }

        /// <summary>
        /// Get the first field of the specified type.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TReturn GetFieldValue<TReturn>(this object obj)
        {
            foreach (var field in obj.GetType().GetFields(flags))
                if (field.FieldType == typeof(TReturn))
                    return (TReturn)field.GetValue(obj);
            return default(TReturn);
        }

        /// <summary>
        /// Get the first property of the specified type.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TReturn GetPropertyValue<TReturn>(this object obj)
        {
            foreach (var prop in obj.GetType().GetProperties(flags))
                if (prop.PropertyType == typeof(TReturn))
                    return (TReturn)prop.GetValue(obj);
            return default(TReturn);
        }

        /// <summary>
        /// Get the field value matching the specified name.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetFieldValue(this object obj, string name)
        {
            var field = obj.GetType().GetField(name, flags);
            return field != null ? field.GetValue(obj) : null;
        }

        /// <summary>
        /// Get the property value matching the specified name.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string name)
        {
            var prop = obj.GetType().GetProperty(name, flags);
            return prop != null ? prop.GetValue(obj) : null;
        }

        private static BindingFlags flags =
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance;
    }
}