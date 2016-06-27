using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wheatech.ObjectMapper
{
    /// <summary>
    /// Asynchronous extension methods for the <see cref="ObjectMapper"/>.
    /// </summary>
    public static class MapperAsyncExtensions
    {
        private static void CheckMapper<TSource, TTarget>(IInstanceMapper<TSource, TTarget> mapper)
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
    }
}
