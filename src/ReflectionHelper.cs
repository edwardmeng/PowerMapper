using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal static class ReflectionHelper
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

        public static bool IsNullable(this Type type)
        {
#if NetCore
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
#else
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
#endif
        }

        public static MethodInfo GetConvertMethod(Type sourceType, Type targetType)
        {
            if (sourceType == null || targetType == null) return null;
#if NetCore
            var reflectingSourceType = sourceType.GetTypeInfo();
            var reflectingTargetType = targetType.GetTypeInfo();
#else
            var reflectingSourceType = sourceType;
            var reflectingTargetType = targetType;
#endif
            Func<MethodInfo, string, bool> methodPredicate = (method, name) =>
            {
                if (method.IsSpecialName && method.Name == name && method.ReturnType == targetType)
                {
                    var parameters = method.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == sourceType;
                }
                return false;
            };
            return
                reflectingTargetType.GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(method => methodPredicate(method, "op_Implicit")) ??
                reflectingSourceType.GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(method => methodPredicate(method, "op_Implicit")) ??
                reflectingTargetType.GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(method => methodPredicate(method, "op_Explicit")) ??
                reflectingSourceType.GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(method => methodPredicate(method, "op_Explicit"));
        }

        public static int GetDistance(Type sourceType, Type targetType)
        {
            if (targetType == null) return -1;
#if NetCore
            var reflectingSourceType = sourceType?.GetTypeInfo();
            var reflectingTargetType = targetType.GetTypeInfo();
#else
            var reflectingSourceType = sourceType;
            var reflectingTargetType = targetType;
#endif
            if (reflectingSourceType != null && reflectingTargetType.IsInterface)
            {
                return reflectingSourceType.GetInterfaces().Count(interfaceType =>
                {
#if NetCore
                    var reflectingInterfaceType = interfaceType.GetTypeInfo();
#else
                    var reflectingInterfaceType = interfaceType;
#endif
                    if (!reflectingTargetType.IsGenericTypeDefinition)
                    {
                        return reflectingTargetType.IsAssignableFrom(reflectingInterfaceType);
                    }
                    if (reflectingInterfaceType.IsGenericType)
                    {
                        return reflectingTargetType.IsAssignableFrom(reflectingInterfaceType.GetGenericTypeDefinition());
                    }
                    if (reflectingInterfaceType.IsGenericTypeDefinition)
                    {
                        return reflectingTargetType.IsAssignableFrom(reflectingInterfaceType);
                    }
                    return false;
                });
            }
            var distance = 0;
            while (reflectingSourceType != null)
            {
#if NetCore
                if (reflectingSourceType.GetType() == reflectingTargetType.GetType()) return distance;
                if (reflectingSourceType.GetType() == typeof(object)) break;
                reflectingSourceType = reflectingSourceType.BaseType?.GetTypeInfo();
#else
                if (reflectingSourceType == reflectingTargetType) return distance;
                if (reflectingSourceType == typeof(object)) break;
                reflectingSourceType = reflectingSourceType.BaseType;
#endif
                distance++;
            }
            return -1;
        }

        public static bool IsEnumerable(this Type targetType, out Type elementType)
        {
#if NetCore
            var reflectingTargetType = targetType.GetTypeInfo();
#else
            var reflectingTargetType = targetType;
#endif
            elementType = null;
            IEnumerable<Type> interfaces = reflectingTargetType.GetInterfaces();
            if (reflectingTargetType.IsInterface)
            {
                interfaces = interfaces.Concat(new[] { targetType });
            }
            var matchedType = interfaces.FirstOrDefault(type =>
            {
                if (type == typeof(IEnumerable<>)) return true;
#if NetCore
                var reflectingType = type.GetTypeInfo();
#else
                var  reflectingType = type;
#endif
                return reflectingType.IsGenericType && reflectingType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
            });
            if (matchedType != null)
            {
#if NetCore
                elementType = matchedType.GetTypeInfo().GetGenericArguments()[0];
#else
                elementType = matchedType.GetGenericArguments()[0];
#endif
                return true;
            }
            return false;
        }
    }
}
