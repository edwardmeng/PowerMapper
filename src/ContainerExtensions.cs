using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Wheatech.EmitMapper
{
    /// <summary>
    /// Extension methods for the <see cref="IMappingContainer"/>.
    /// </summary>
    public static class ContainerExtensions
    {
        #region Helper
        
        private static readonly ConcurrentDictionary<Tuple<Type, Type>, Delegate> _convertMethods =
            new ConcurrentDictionary<Tuple<Type, Type>, Delegate>();
        private static readonly ConcurrentDictionary<Tuple<Type, Type>, Delegate> _mapMethods =
            new ConcurrentDictionary<Tuple<Type, Type>, Delegate>();

        private static object ExecuteConvertMethod(Type sourceType, Type targetType, IMappingContainer container, object sourceValue)
        {
            return _convertMethods.GetOrAdd(Tuple.Create(sourceType, targetType), key =>
            {
                var method =
                    typeof(IMappingContainer).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .Single(x => x.Name == "Map" && x.GetParameters().Length == 1)
                        .MakeGenericMethod(sourceType, targetType);
                var containerParameter = Expression.Parameter(typeof(IMappingContainer));
                var sourceParameter = Expression.Parameter(sourceType);
                return Expression.Lambda(Expression.Call(containerParameter, method, sourceParameter), containerParameter, sourceParameter).Compile();
            }).DynamicInvoke(container, sourceValue);
        }

        private static void ExecuteMapMethod(Type sourceType, Type targetType, IMappingContainer container, object sourceValue, object targetValue)
        {
            _mapMethods.GetOrAdd(Tuple.Create(sourceType, targetType), key =>
            {
                var method =
                    typeof(MappingContainer).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .Single(x => x.Name == "Map" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(sourceType, targetType);
                var containerParameter = Expression.Parameter(typeof(IMappingContainer));
                var sourceParameter = Expression.Parameter(sourceType);
                var targetParameter = Expression.Parameter(targetType);
                var call = Expression.Call(containerParameter, method, sourceParameter, targetParameter);
                return Expression.Lambda(call, containerParameter, sourceParameter, targetParameter).Compile();
            })?.DynamicInvoke(container, sourceValue, targetValue);
        }

        private static void CheckContainer(IMappingContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
        }

        #endregion

        #region Classic

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
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. </returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task ClassicMapAsync(this IMappingContainer container, object source, object target, CancellationToken cancellationToken)
        {
            CheckContainer(container);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => container.ClassicMap(source, target), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <returns>A task that represents the asynchronous operation. </returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task ClassicMapAsync(this IMappingContainer container, object source, object target)
        {
            return container.ClassicMapAsync(source, target, CancellationToken.None);
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
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="targetType">The type of target object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> or <paramref name="targetType"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<object> ClassicMapAsync(this IMappingContainer container, object source, Type targetType, CancellationToken cancellationToken)
        {
            CheckContainer(container);
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => container.ClassicMap(source, targetType), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="targetType">The type of target object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> or <paramref name="targetType"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<object> ClassicMapAsync(this IMappingContainer container, object source, Type targetType)
        {
            return container.ClassicMapAsync(source, targetType, CancellationToken.None);
        }

        #endregion

        #region Convert

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
            return (TTarget) (ClassicMap(container, source, typeof(TTarget)) ?? default(TTarget));
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TTarget>(this IMappingContainer container, object source, CancellationToken cancellationToken)
        {
            CheckContainer(container);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => container.Map<TTarget>(source), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TTarget>(this IMappingContainer container, object source)
        {
            return container.MapAsync<TTarget>(source, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TSource, TTarget>(this IMappingContainer container, TSource source, CancellationToken cancellationToken)
        {
            CheckContainer(container);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => container.Map<TSource, TTarget>(source), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TSource, TTarget>(this IMappingContainer container, TSource source)
        {
            return container.MapAsync<TSource, TTarget>(source, CancellationToken.None);
        }

        #endregion

        #region Convert IEnumerable

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
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{T}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IEnumerable<TTarget>> MapAsync<TSource, TTarget>(this IMappingContainer container, IEnumerable<TSource> sources, CancellationToken cancellationToken)
        {
            return container.MapAsync<IEnumerable<TSource>, IEnumerable<TTarget>>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{T}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IEnumerable<TTarget>> MapAsync<TSource, TTarget>(this IMappingContainer container, IEnumerable<TSource> sources)
        {
            return container.MapAsync<TSource, TTarget>(sources, CancellationToken.None);
        }

        #endregion

        #region Convert Array

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
        /// Asynchronously execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source array to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget[]> MapAsync<TSource, TTarget>(this IMappingContainer container, TSource[] sources, CancellationToken cancellationToken)
        {
            return container.MapAsync<TSource[], TTarget[]>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source array to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget[]> MapAsync<TSource, TTarget>(this IMappingContainer container, TSource[] sources)
        {
            return container.MapAsync<TSource, TTarget>(sources, CancellationToken.None);
        }

        #endregion

        #region Convert Collection

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
        /// Asynchronously execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<ICollection<TTarget>> MapAsync<TSource, TTarget>(this IMappingContainer container, ICollection<TSource> sources, CancellationToken cancellationToken)
        {
            return container.MapAsync<ICollection<TSource>, ICollection<TTarget>>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<ICollection<TTarget>> MapAsync<TSource, TTarget>(this IMappingContainer container, ICollection<TSource> sources)
        {
            return container.MapAsync<TSource, TTarget>(sources, CancellationToken.None);
        }

        #endregion

        #region Convert IList

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
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IList<TTarget>> MapAsync<TSource, TTarget>(this IMappingContainer container, IList<TSource> sources, CancellationToken cancellationToken)
        {
            return container.MapAsync<IList<TSource>, IList<TTarget>>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IList<TTarget>> MapAsync<TSource, TTarget>(this IMappingContainer container, IList<TSource> sources)
        {
            return container.MapAsync<TSource, TTarget>(sources, CancellationToken.None);
        }

        #endregion

        #region Convert List

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

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<List<TTarget>> MapAsync<TSource, TTarget>(this IMappingContainer container, List<TSource> sources, CancellationToken cancellationToken)
        {
            return container.MapAsync<List<TSource>, List<TTarget>>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<List<TTarget>> MapAsync<TSource, TTarget>(this IMappingContainer container, List<TSource> sources)
        {
            return container.MapAsync<TSource, TTarget>(sources, CancellationToken.None);
        }

        #endregion

        #region MapAsync

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(this IMappingContainer container, TSource source, TTarget target,
            CancellationToken cancellationToken)
        {
            CheckContainer(container);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => container.Map(source, target), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="container">The mapping container to execute with.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(this IMappingContainer container, TSource source, TTarget target)
        {
            return container.MapAsync(source, target, CancellationToken.None);
        }

        #endregion
    }
}
