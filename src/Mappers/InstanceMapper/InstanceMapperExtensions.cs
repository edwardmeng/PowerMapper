using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Wheatech.EmitMapper
{
    /// <summary>
    /// Extension methods for the <see cref="IInstanceMapper{TSource, TTarget}"/>.
    /// </summary>
    public static class InstanceMapperExtensions
    {
        private static void CheckMapper<TSource, TTarget>(IInstanceMapper<TSource, TTarget> mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
        }

        #region Convert Async

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="source">Source object to map from</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, TSource source, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => mapper.Map(source), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="source">Source object to map from</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, TSource source)
        {
            return MapAsync(mapper, source, CancellationToken.None);
        }

        #endregion

        #region Convert IEnumerable

        /// <summary>
        /// Execute a mapping from the source <see cref="IEnumerable{TSource}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static IEnumerable<TTarget> Map<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IEnumerable<TSource> sources)
        {
            CheckMapper(mapper);
            if (sources == null) return null;
            var sourceArray = sources.ToArray();
            var targetArray = new TTarget[sourceArray.Length];
            for (int i = 0; i < sourceArray.Length; i++)
            {
                targetArray[i] = mapper.Map(sourceArray[i]);
            }
            return targetArray;
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static async Task<IEnumerable<TTarget>> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IEnumerable<TSource> sources, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            if (sources == null) return null;
            var sourceArray = sources.ToArray();
            var targetArray = new TTarget[sourceArray.Length];
            for (int i = 0; i < sourceArray.Length; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                targetArray[i] = await mapper.MapAsync(sourceArray[i], cancellationToken);
            }
            return targetArray;
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <returns>The mapped target <see cref="IEnumerable{TSource}"/>.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IEnumerable<TTarget>> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IEnumerable<TSource> sources)
        {
            return mapper.MapAsync(sources, CancellationToken.None);
        }

        #endregion

        #region Convert ICollection

        /// <summary>
        /// Execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static ICollection<TTarget> Map<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, ICollection<TSource> sources)
        {
            return (ICollection<TTarget>)mapper.Map((IEnumerable<TSource>)sources);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static async Task<ICollection<TTarget>> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, ICollection<TSource> sources, CancellationToken cancellationToken)
        {
            return (ICollection<TTarget>)await mapper.MapAsync((IEnumerable<TSource>)sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static Task<ICollection<TTarget>> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, ICollection<TSource> sources)
        {
            return mapper.MapAsync(sources, CancellationToken.None);
        }

        #endregion

        #region Convert IList

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static IList<TTarget> Map<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IList<TSource> sources)
        {
            return (IList<TTarget>)mapper.Map((IEnumerable<TSource>)sources);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static async Task<IList<TTarget>> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IList<TSource> sources, CancellationToken cancellationToken)
        {
            return (IList<TTarget>)await mapper.MapAsync((IEnumerable<TSource>)sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static Task<IList<TTarget>> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IList<TSource> sources)
        {
            return mapper.MapAsync(sources, CancellationToken.None);
        }

        #endregion

        #region Convert Array

        /// <summary>
        /// Execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source array to map from.</param>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <returns>The mapped target array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static TTarget[] Map<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, TSource[] sources)
        {
            return (TTarget[])mapper.Map((IEnumerable<TSource>)sources);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source array to map from.</param>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static async Task<TTarget[]> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper,
            TSource[] sources, CancellationToken cancellationToken)
        {
            return (TTarget[])await mapper.MapAsync((IEnumerable<TSource>)sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source array to map from.</param>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static Task<TTarget[]> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, TSource[] sources)
        {
            return mapper.MapAsync(sources, CancellationToken.None);
        }

        #endregion

        #region Convert List

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        public static List<TTarget> Map<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, List<TSource> sources)
        {
            if (sources == null) return null;
            return new List<TTarget>(mapper.Map((IEnumerable<TSource>)sources));
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static async Task<List<TTarget>> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, List<TSource> sources, CancellationToken cancellationToken)
        {
            if (sources == null) return null;
            return new List<TTarget>(await mapper.MapAsync((IEnumerable<TSource>)sources, cancellationToken));
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        public static Task<List<TTarget>> MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, List<TSource> sources)
        {
            return mapper.MapAsync(sources, CancellationToken.None);
        }

        #endregion

        #region Map Async

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="source">Source object to map from</param>
        /// <param name="target">Target object to map into</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, TSource source, TTarget target, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => mapper.Map(source, target), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="source">Source object to map from</param>
        /// <param name="target">Target object to map into</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, TSource source, TTarget target)
        {
            return MapAsync(mapper, source, target, CancellationToken.None);
        }

        #endregion

        #region Map IEnumerable

        /// <summary>
        /// Execute a mapping from the source <see cref="IEnumerable{TSource}"/> to the existing destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="targets">The target <see cref="IEnumerable{TSource}"/> to map to.</param>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        public static void Map<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IEnumerable<TSource> sources, IEnumerable<TTarget> targets)
        {
            CheckMapper(mapper);
            if (sources == null || targets == null) return;
            var sourceEnumerator = sources.GetEnumerator();
            var targetEnumerator = targets.GetEnumerator();
            while (sourceEnumerator.MoveNext()&&targetEnumerator.MoveNext())
            {
                mapper.Map(sourceEnumerator.Current, targetEnumerator.Current);
            }
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to the existing destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="targets">The target <see cref="IEnumerable{TSource}"/> to map to.</param>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static async Task MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IEnumerable<TSource> sources,
            IEnumerable<TTarget> targets, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            if (sources == null || targets == null) return;
            var sourceEnumerator = sources.GetEnumerator();
            var targetEnumerator = targets.GetEnumerator();
            while (sourceEnumerator.MoveNext() && targetEnumerator.MoveNext())
            {
                await mapper.MapAsync(sourceEnumerator.Current, targetEnumerator.Current, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to the existing destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="targets">The target <see cref="IEnumerable{TSource}"/> to map to.</param>
        /// <param name="mapper">The instance mapping execution strategy.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(this IInstanceMapper<TSource, TTarget> mapper, IEnumerable<TSource> sources, IEnumerable<TTarget> targets)
        {
            return mapper.MapAsync(sources, targets, CancellationToken.None);
        }

        #endregion
    }
}
