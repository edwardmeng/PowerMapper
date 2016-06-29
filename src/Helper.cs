using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal static class Helper
    {
        private static readonly ConcurrentDictionary<Tuple<Type, string>, Delegate> _staticFieldSetters =
            new ConcurrentDictionary<Tuple<Type, string>, Delegate>();

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

        public static void SetStaticField(Type type, string fieldName, object fieldValue)
        {
            _staticFieldSetters.GetOrAdd(Tuple.Create(type, fieldName), key =>
            {
                var field = key.Item1.GetField(key.Item2);
                if (field == null) return null;
                var value = Expression.Parameter(field.FieldType);
                return Expression.Lambda(Expression.Assign(Expression.Field(null, field), value), value).Compile();
            })?.DynamicInvoke(fieldValue);
        }

        /// <summary>
        ///   Checks if a type implements an open generic at any level of the inheritance chain, including all
        ///   base classes
        /// </summary>
        /// <param name = "objectType">The type to check</param>
        /// <param name = "interfaceType">The interface type (must be a generic type definition)</param>
        /// <param name = "matchedType">The matching type that was found for the interface type</param>
        /// <returns>True if the interface is implemented by the type, otherwise false</returns>
        public static bool ImplementsGeneric(Type objectType, Type interfaceType, out Type matchedType)
        {
            matchedType = null;

            if (interfaceType.IsInterface)
            {
                matchedType =
                    objectType.GetInterfaces()
                        .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
                if (matchedType != null)
                    return true;
            }

            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == interfaceType)
            {
                matchedType = objectType;
                return true;
            }

            Type baseType = objectType.BaseType;
            if (baseType == null)
                return false;

            return ImplementsGeneric(baseType, interfaceType, out matchedType);
        }
    }
}
