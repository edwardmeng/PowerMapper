using System.Collections.Generic;

namespace Wheatech.EmitMapper
{
    /// <summary>
    /// Extension methods for convenient of the mapping and converting.
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped target object.</returns>
        public static TTarget MapTo<TTarget>(this object source)
        {
            return Mapper.Map<TTarget>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped target object.</returns>
        public static TTarget MapTo<TSource, TTarget>(this TSource source)
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// Execute a mapping from the source <see cref="IEnumerable{T}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="sources">The source to map from.</param>
        /// <returns>The mapped target instance.</returns>
        public static IEnumerable<TTarget> MapTo<TSource, TTarget>(this IEnumerable<TSource> sources)
        {
            return Mapper.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target collection.</returns>
        public static ICollection<TTarget> MapTo<TSource, TTarget>(this ICollection<TSource> sources)
        {
            return Mapper.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        public static IList<TTarget> MapTo<TSource, TTarget>(this IList<TSource> sources)
        {
            return Mapper.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        public static List<TTarget> MapTo<TSource, TTarget>(this List<TSource> sources)
        {
            return Mapper.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source array to map from.</param>
        /// <returns>The mapped target array.</returns>
        public static TTarget[] MapTo<TSource, TTarget>(this TSource[] sources)
        {
            return Mapper.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        public static void MapTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            Mapper.Map(source, target);
        }
    }
}
