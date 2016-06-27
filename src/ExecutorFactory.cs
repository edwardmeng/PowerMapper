using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Wheatech.ObjectMapper
{
    internal static class ExecutorFactory<TSource, TTarget>
    {
        private static readonly ConcurrentDictionary<ObjectMapper, Func<TSource, TTarget>> _converters =
            new ConcurrentDictionary<ObjectMapper, Func<TSource, TTarget>>();
        private static readonly ConcurrentDictionary<ObjectMapper, Action<TSource, TTarget>> _mappers =
            new ConcurrentDictionary<ObjectMapper, Action<TSource, TTarget>>();

        private static bool TryGetEnumerableConverter(ObjectMapper container, out Func<TSource, TTarget> converter)
        {
            converter = null;
            Type sourceEnumerableType;
            if (Helper.ImplementsGeneric(typeof(TSource), typeof(IEnumerable<>), out sourceEnumerableType) &&
                typeof(TTarget).IsGenericType && typeof(TTarget).GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var sourceElementType = sourceEnumerableType.GetGenericArguments()[0];
                var targetElementType = typeof(TTarget).GetGenericArguments()[0];
                converter = source => (TTarget)Helper.ExecuteMapMethod(sourceElementType, targetElementType, "MapEnumerable",container, source);
                return true;
            }
            return false;
        }

        private static bool TryGetArrayConverter(ObjectMapper container, out Func<TSource, TTarget> converter)
        {
            converter = null;
            if (typeof(TSource).IsArray && typeof(TTarget).IsArray)
            {
                var sourceElementType = typeof(TSource).GetElementType();
                var targetElementType = typeof(TTarget).GetElementType();
                converter = source =>
                {
                    if (ReferenceEquals(source, null)) return default(TTarget);
                    return (TTarget) Helper.ExecuteMapMethod(sourceElementType, targetElementType, "MapArray", container, source);
                };
                return true;
            }
            return false;
        }

        private static bool TryGetListConverter(ObjectMapper container, out Func<TSource, TTarget> converter)
        {
            converter = null;
            if (typeof(TSource).IsGenericType && typeof(TSource).GetGenericTypeDefinition() == typeof(List<>) && 
                typeof(TTarget).IsGenericType &&
                typeof(TTarget).GetGenericTypeDefinition() == typeof(List<>))
            {
                var sourceElementType = typeof(TSource).GetGenericArguments()[0];
                var targetElementType = typeof(TTarget).GetGenericArguments()[0];
                converter = source =>
                {
                    if (ReferenceEquals(source, null)) return default(TTarget);
                    return (TTarget)Helper.ExecuteMapMethod(sourceElementType, targetElementType, "MapList", container, source);
                };
                return true;
            }
            return false;
        }

        private static bool TryGetIListConverter(ObjectMapper container, out Func<TSource, TTarget> converter)
        {
            converter = null;
            Type sourceType;
            if (Helper.ImplementsGeneric(typeof(TSource), typeof(IList<>), out sourceType) && typeof(TTarget).IsGenericType &&
                typeof(TTarget).GetGenericTypeDefinition() == typeof(IList<>))
            {
                var sourceElementType = sourceType.GetGenericArguments()[0];
                var targetElementType = typeof(TTarget).GetGenericArguments()[0];
                converter = source =>
                {
                    if (ReferenceEquals(source, null)) return default(TTarget);
                    return (TTarget)Helper.ExecuteMapMethod(sourceElementType, targetElementType, "MapIList", container, source);
                };
                return true;
            }
            return false;

        }

        private static bool TryGetCollectionConverter(ObjectMapper container, out Func<TSource, TTarget> converter)
        {
            converter = null;
            Type sourceEnumerableType;
            if (Helper.ImplementsGeneric(typeof(TSource), typeof(ICollection<>), out sourceEnumerableType) && 
                typeof(TTarget).IsGenericType &&
                typeof(TTarget).GetGenericTypeDefinition() == typeof(ICollection<>))
            {
                var sourceElementType = sourceEnumerableType.GetGenericArguments()[0];
                var targetElementType = typeof(TTarget).GetGenericArguments()[0];
                converter = source =>
                {
                    if (ReferenceEquals(source, null)) return default(TTarget);
                    return (TTarget)Helper.ExecuteMapMethod(sourceElementType, targetElementType, "MapCollection", container, source);
                };
                return true;
            }
            return false;
        }

        public static Func<TSource, TTarget> GetConverter(ObjectMapper container)
        {
            return _converters.GetOrAdd(container, key =>
            {
                Func<TSource, TTarget> converter;
                if (key.Converters.Get<TSource, TTarget>() == null &&
                    (TryGetArrayConverter(key, out converter) ||
                     TryGetListConverter(key, out converter) ||
                     TryGetIListConverter(key, out converter) ||
                     TryGetCollectionConverter(key, out converter) ||
                     TryGetEnumerableConverter(key, out converter)
                     ))
                {
                    return converter;
                }
                var mapper = InstanceMapper<TSource, TTarget>.GetInstance(key);
                return mapper.Map;
            });
        }

        public static Action<TSource, TTarget> GetMapper(ObjectMapper container)
        {
            return _mappers.GetOrAdd(container, key =>
            {
                Type sourceEnumerableType, targetEnumerableType;
                if (Helper.ImplementsGeneric(typeof(TSource), typeof(IEnumerable<>), out sourceEnumerableType) &&
                    Helper.ImplementsGeneric(typeof(TTarget), typeof(IEnumerable<>), out targetEnumerableType))
                {
                    return
                        (source, target) =>
                            Helper.ExecuteMapMethod(
                                sourceEnumerableType.GetGenericArguments()[0],
                                targetEnumerableType.GetGenericArguments()[0],
                                "MapEnumerable", container, source, target);
                }
                var mapper = InstanceMapper<TSource, TTarget>.GetInstance(key);
                return mapper.Map;
            });
        }
    }
}
