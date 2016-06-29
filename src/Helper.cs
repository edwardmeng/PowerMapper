using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal static class Helper
    {
        public static TypeBuilder DefineStaticType(this ModuleBuilder builder)
        {
            return builder.DefineType(Guid.NewGuid().ToString("N"),
                TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.AutoClass |
                TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit);
        }

        public static MethodBuilder DefineStaticMethod(this TypeBuilder builder, string methodName)
        {
            return builder.DefineMethod(methodName, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig);
        }

        public static FieldBuilder DefineStaticField<T>(this TypeBuilder builder, string fieldName)
        {
            return builder.DefineField(fieldName, typeof(T), FieldAttributes.Public | FieldAttributes.Static);
        }

        public static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static MethodInfo GetConvertMethod(Type sourceType, Type targetType)
        {
            return targetType.GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null,
                new[] { sourceType }, null) ??
                sourceType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(method =>
                {
                    if (method.IsSpecialName && method.Name == "op_Implicit" && method.ReturnType == targetType)
                    {
                        var parameters = method.GetParameters();
                        return parameters.Length == 1 && parameters[0].ParameterType == sourceType;
                    }
                    return false;
                }).FirstOrDefault() ??
                targetType.GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static, null, new[] { sourceType }, null) ??
                sourceType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(method =>
                {
                    if (method.IsSpecialName && method.Name == "op_Explicit" && method.ReturnType == targetType)
                    {
                        var parameters = method.GetParameters();
                        return parameters.Length == 1 && parameters[0].ParameterType == sourceType;
                    }
                    return false;
                }).FirstOrDefault();
        }

        public static int GetDistance(Type sourceType, Type targetType)
        {
            if (targetType.IsInterface)
            {
                return sourceType.GetInterfaces().Count(interfaceType =>
                {
                    if (!targetType.IsGenericTypeDefinition)
                    {
                        return targetType.IsAssignableFrom(interfaceType);
                    }
                    if (interfaceType.IsGenericType)
                    {
                        return targetType.IsAssignableFrom(interfaceType.GetGenericTypeDefinition());
                    }
                    if (interfaceType.IsGenericTypeDefinition)
                    {
                        return targetType.IsAssignableFrom(interfaceType);
                    }
                    return false;
                });
            }
            var distance = 0;
            while (sourceType != null)
            {
                if (sourceType == targetType)
                {
                    return distance;
                }
                if (sourceType == typeof(object))
                {
                    break;
                }
                distance++;
                sourceType = sourceType.BaseType;
            }
            return -1;
        }

        public static bool IsEnumerable(Type targetType, out Type elementType)
        {
            elementType = null;
            var matchedType = targetType.GetInterfaces().FirstOrDefault(type => type == typeof(IEnumerable<>) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)));
            if (matchedType != null)
            {
                elementType = matchedType.GetGenericArguments()[0];
                return true;
            }
            return false;
        }
    }
}
