using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Caliburn.Micro.ReactiveUI
{
    public static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            return type.GetTypeInfo().DeclaredProperties;
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetTypeInfo().DeclaredProperties.FirstOrDefault(p => p.Name == name);
        }

        public static MethodInfo GetMethod(this Type type, string name)
        {
            return type.GetTypeInfo().DeclaredMethods.FirstOrDefault(p => p.Name == name);
        }
    }
}
