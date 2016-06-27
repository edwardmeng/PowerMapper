using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wheatech.ObjectMapper
{
    /// <summary>
    /// Asynchronous extension methods for the <see cref="ObjectMapper"/>.
    /// </summary>
    public static class ObjectMapperAsyncExtensions
    {
        private static void CheckMapper(ObjectMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TSource, TTarget>(this ObjectMapper mapper, TSource source, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => mapper.Map<TSource, TTarget>(source), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TSource, TTarget>(this ObjectMapper mapper, TSource source)
        {
            return MapAsync<TSource, TTarget>(mapper, source, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(this ObjectMapper mapper, TSource source, TTarget target, CancellationToken cancellationToken)
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
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(this ObjectMapper mapper, TSource source, TTarget target)
        {
            return MapAsync(mapper, source, target, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TTarget>(this ObjectMapper mapper, object source, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(mapper.Map<TTarget>, source, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TTarget>(this ObjectMapper mapper, object source)
        {
            return MapAsync<TTarget>(mapper, source, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="targetType">The type of target object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<object> MapAsync(this ObjectMapper mapper, object source, Type targetType, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => mapper.Map(source, targetType), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="targetType">The type of target object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<object> MapAsync(this ObjectMapper mapper, object source, Type targetType)
        {
            return MapAsync(mapper, source, targetType, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync(this ObjectMapper mapper, object source, object target, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(() => mapper.Map(source, target), cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync(this ObjectMapper mapper, object source, object target)
        {
            return MapAsync(mapper, source, target, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target <see cref="IEnumerable{TSource}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static async Task<IEnumerable<TTarget>> MapAsync<TSource, TTarget>(this ObjectMapper mapper, IEnumerable<TSource> sources, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            if (sources == null) return null;
            var map = await mapper.GetMapperAsync<TSource, TTarget>(cancellationToken);
            var list = new List<TTarget>();
            foreach (var source in sources)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var target = await map.MapAsync(source, cancellationToken);
                list.Add(target);
            }
            return list;
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target <see cref="IEnumerable{TSource}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IEnumerable<TTarget>> MapAsync<TSource, TTarget>(this ObjectMapper mapper, IEnumerable<TSource> sources)
        {
            return MapAsync<TSource, TTarget>(mapper, sources, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to the existing destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="targets">The target <see cref="IEnumerable{TSource}"/> to map to.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static async Task MapAsync<TSource, TTarget>(this ObjectMapper mapper, IEnumerable<TSource> sources, IEnumerable<TTarget> targets, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            if (sources == null || targets == null) return;
            var map = await mapper.GetMapperAsync<TSource, TTarget>(cancellationToken);
            var sourceEnumerator = sources.GetEnumerator();
            var targetEnumerator = targets.GetEnumerator();
            while (sourceEnumerator.MoveNext() && targetEnumerator.MoveNext())
            {
                cancellationToken.ThrowIfCancellationRequested();
                await map.MapAsync(sourceEnumerator.Current, targetEnumerator.Current, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to the existing destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="targets">The target <see cref="IEnumerable{TSource}"/> to map to.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(this ObjectMapper mapper, IEnumerable<TSource> sources, IEnumerable<TTarget> targets)
        {
            return MapAsync(mapper, sources, targets, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static async Task<List<TTarget>> MapAsync<TSource, TTarget>(this ObjectMapper mapper, List<TSource> sources, CancellationToken cancellationToken)
        {
            return (await MapAsync<TSource, TTarget>(mapper, (IEnumerable<TSource>)sources, cancellationToken)).ToList();
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<List<TTarget>> MapAsync<TSource, TTarget>(this ObjectMapper mapper, List<TSource> sources)
        {
            return MapAsync<TSource, TTarget>(mapper, sources, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static async Task<ICollection<TTarget>> MapAsync<TSource, TTarget>(this ObjectMapper mapper, ICollection<TSource> sources, CancellationToken cancellationToken)
        {
            return (await MapAsync<TSource, TTarget>(mapper, (IEnumerable<TSource>)sources, cancellationToken)).ToArray();
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<ICollection<TTarget>> MapAsync<TSource, TTarget>(this ObjectMapper mapper, ICollection<TSource> sources)
        {
            return MapAsync<TSource, TTarget>(mapper, sources, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static async Task<IList<TTarget>> MapAsync<TSource, TTarget>(this ObjectMapper mapper, IList<TSource> sources, CancellationToken cancellationToken)
        {
            return (IList<TTarget>)(await MapAsync<TSource, TTarget>(mapper, (ICollection<TSource>)sources, cancellationToken));
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IList<TTarget>> MapAsync<TSource, TTarget>(this ObjectMapper mapper, IList<TSource> sources)
        {
            return MapAsync<TSource, TTarget>(mapper, sources, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source array to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static async Task<TTarget[]> MapAsync<TSource, TTarget>(this ObjectMapper mapper, TSource[] sources, CancellationToken cancellationToken)
        {
            return (TTarget[])(await MapAsync<TSource, TTarget>(mapper, (ICollection<TSource>)sources, cancellationToken));
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="sources">The source array to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget[]> MapAsync<TSource, TTarget>(this ObjectMapper mapper, TSource[] sources)
        {
            return MapAsync<TSource, TTarget>(mapper, sources, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously returns a mapper instance for specified types.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapper instance for specified types.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IInstanceMapper<TSource, TTarget>> GetMapperAsync<TSource, TTarget>(this ObjectMapper mapper, CancellationToken cancellationToken)
        {
            CheckMapper(mapper);
            cancellationToken.ThrowIfCancellationRequested();
            return Task.Factory.StartNew(mapper.GetMapper<TSource, TTarget>, cancellationToken);
        }

        /// <summary>
        /// Asynchronously returns a mapper instance for specified types.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="mapper">An <see cref="ObjectMapper"/> that contains the mapping configurations.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapper instance for specified types.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IInstanceMapper<TSource, TTarget>> GetMapperAsync<TSource, TTarget>(this ObjectMapper mapper)
        {
            return GetMapperAsync<TSource, TTarget>(mapper, CancellationToken.None);
        }
    }
}
