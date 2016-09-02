using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PowerMapper
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
    }
}
