using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Caliburn.Micro.ReactiveUI
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns all the public properties of the current Type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            return type.GetTypeInfo().DeclaredProperties
                        .Where(p => p.GetMethod.IsPublic)
                        .Where(p => p.SetMethod.IsPublic);
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetProperties().FirstOrDefault(p => p.Name == name);
        }

        public static MethodInfo GetMethod(this Type type, string name)
        {
            return type.GetTypeInfo()
                       .DeclaredMethods
                       .Where(m => m.IsPublic)
                       .FirstOrDefault(p => p.Name == name);
        }
    }
}
