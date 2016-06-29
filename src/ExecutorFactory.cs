using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Wheatech.EmitMapper
{
    internal static class ExecutorFactory<TSource, TTarget>
    {
        private static readonly ConcurrentDictionary<ObjectMapper, Action<TSource, TTarget>> _mappers =
            new ConcurrentDictionary<ObjectMapper, Action<TSource, TTarget>>();

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
