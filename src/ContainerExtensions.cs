using System;
#if !Net35
using System.Collections.Concurrent;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PowerMapper
{
    /// <summary>
    /// Extension methods for the <see cref="IMappingContainer"/>.
    /// </summary>
    public static class ContainerExtensions
    {
#region Helper

#if Net35
        private static readonly object _syncLock = new object();
        private static readonly Dictionary<Pair<Type, Type>, Delegate> _convertMethods = new Dictionary<Pair<Type, Type>, Delegate>();
        private static readonly Dictionary<Pair<Type, Type>, Delegate> _mapMethods = new Dictionary<Pair<Type, Type>, Delegate>();

        private static object ExecuteConvertMethod(Type sourceType, Type targetType, IMappingContainer container, object sourceValue)
        {
            var key = Pair.Create(sourceType, targetType);
            Delegate method;
            if (!_convertMethods.TryGetValue(key,out method))
            {
                lock (_syncLock)
                {
                    if (!_convertMethods.TryGetValue(key, out method))
                    {
                        method = CreateConvertMethod(sourceType, targetType);
                        _convertMethods.Add(key, method);
                    }
                }
            }
            return method.DynamicInvoke(container, sourceValue);
        }

        private static void ExecuteMapMethod(Type sourceType, Type targetType, IMappingContainer container, object sourceValue, object targetValue)
        {
            var key = Pair.Create(sourceType, targetType);
            Delegate method;
            if (!_mapMethods.TryGetValue(key, out method))
            {
                lock (_syncLock)
                {
                    if (!_mapMethods.TryGetValue(key, out method))
                    {
                        method = CreateMapMethod(sourceType, targetType);
                        _mapMethods.Add(key, method);
                    }
                }
            }
            method.DynamicInvoke(container, sourceValue);
        }
#else
        private static readonly ConcurrentDictionary<Pair<Type, Type>, Delegate> _convertMethods = new ConcurrentDictionary<Pair<Type, Type>, Delegate>();
        private static readonly ConcurrentDictionary<Pair<Type, Type>, Delegate> _mapMethods = new ConcurrentDictionary<Pair<Type, Type>, Delegate>();

        private static object ExecuteConvertMethod(Type sourceType, Type targetType, IMappingContainer container, object sourceValue)
        {
            return _convertMethods.GetOrAdd(Pair.Create(sourceType, targetType), key => CreateConvertMethod(key.First, key.Second)).DynamicInvoke(container, sourceValue);
        }

        private static void ExecuteMapMethod(Type sourceType, Type targetType, IMappingContainer container, object sourceValue, object targetValue)
        {
            _mapMethods.GetOrAdd(Pair.Create(sourceType, targetType), key => CreateMapMethod(key.First,key.Second))?.DynamicInvoke(container, sourceValue, targetValue);
        }
#endif

        private static Delegate CreateConvertMethod(Type sourceType, Type targetType)
        {
#if NetCore
            var containerMethods = typeof(IMappingContainer).GetTypeInfo().GetMethods(BindingFlags.Instance | BindingFlags.Public);
#else
            var containerMethods = typeof(IMappingContainer).GetMethods(BindingFlags.Instance | BindingFlags.Public);
#endif
            var method = containerMethods.Single(x => x.Name == "Map" && x.GetParameters().Length == 1).MakeGenericMethod(sourceType, targetType);
            var containerParameter = Expression.Parameter(typeof(IMappingContainer), "container");
            var sourceParameter = Expression.Parameter(sourceType, "source");
            return Expression.Lambda(Expression.Call(containerParameter, method, sourceParameter), containerParameter, sourceParameter).Compile();
        }

        private static Delegate CreateMapMethod(Type sourceType, Type targetType)
        {
#if NetCore
            var containerMethods = typeof(MappingContainer).GetTypeInfo().GetMethods(BindingFlags.Instance | BindingFlags.Public);
#else
            var containerMethods = typeof(MappingContainer).GetMethods(BindingFlags.Instance | BindingFlags.Public);
#endif
            var method = containerMethods.Single(x => x.Name == "Map" && x.GetParameters().Length == 2).MakeGenericMethod(sourceType, targetType);
            var containerParameter = Expression.Parameter(typeof(IMappingContainer), "container");
            var sourceParameter = Expression.Parameter(sourceType, "source");
            var targetParameter = Expression.Parameter(targetType, "target");
            var call = Expression.Call(containerParameter, method, sourceParameter, targetParameter);
            return Expression.Lambda(call, containerParameter, sourceParameter, targetParameter).Compile();
        }

        private static void CheckContainer(IMappingContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
        }

#endregion

        /// <summary>
        /// Execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        public static void ClassicMap(this IMappingContainer container, object source, object target)
        {
            CheckContainer(container);
            if (source == null || target == null) return;
            ExecuteMapMethod(source.GetType(), target.GetType(), container, source, target);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="targetType">The type of target object.</param>
        /// <returns>Mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> or <paramref name="targetType"/> is <see langword="null"/>.</exception>
        public static object ClassicMap(this IMappingContainer container, object source, Type targetType)
        {
            CheckContainer(container);
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            return source == null ? null : ExecuteConvertMethod(source.GetType(), targetType, container, source);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        public static TTarget Map<TTarget>(this IMappingContainer container, object source)
        {
            return (TTarget)(ClassicMap(container, source, typeof(TTarget)) ?? default(TTarget));
        }

        /// <summary>
        /// Execute a mapping from the source <see cref="IEnumerable{T}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <returns>The mapped target <see cref="IEnumerable{TSource}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TTarget> Map<TSource, TTarget>(this IMappingContainer container, IEnumerable<TSource> sources)
        {
            return container.Map<IEnumerable<TSource>, IEnumerable<TTarget>>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source array to map from.</param>
        /// <returns>The mapped target array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        public static TTarget[] Map<TSource, TTarget>(this IMappingContainer container, TSource[] sources)
        {
            return container.Map<TSource[], TTarget[]>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        public static ICollection<TTarget> Map<TSource, TTarget>(this IMappingContainer container, ICollection<TSource> sources)
        {
            return container.Map<ICollection<TSource>, ICollection<TTarget>>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        public static IList<TTarget> Map<TSource, TTarget>(this IMappingContainer container, IList<TSource> sources)
        {
            return container.Map<IList<TSource>, IList<TTarget>>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        public static List<TTarget> Map<TSource, TTarget>(this IMappingContainer container, List<TSource> sources)
        {
            return container.Map<List<TSource>, List<TTarget>>(sources);
        }
    }
}
