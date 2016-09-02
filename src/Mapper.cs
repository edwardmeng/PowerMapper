using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PowerMapper
{
    /// <summary>
    /// The convenient entry point of the <see cref="MappingContainer"/>.
    /// </summary>
    public static class Mapper
    {
        private static readonly Lazy<IMappingContainer> _instance = new Lazy<IMappingContainer>(CreateContainer);

        /// <summary>
        /// Gets the default object mapper container.
        /// </summary>
        public static IMappingContainer Default => _instance.Value;

        /// <summary>
        /// Create a new instance of <see cref="IMappingContainer"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="IMappingContainer"/>.</returns>
        public static IMappingContainer CreateContainer()
        {
            return new MappingContainer();
        }

        /// <summary>
        /// Gets a <see cref="ConventionCollection"/> object that used to manage the conventions.
        /// </summary>
        /// <value>A <see cref="ConventionCollection"/> object that used to manage the conventions.</value>
        public static ConventionCollection Conventions => Default.Conventions;

        /// <summary>
        /// Registers a custom converter to the <see cref="MappingContainer"/> instance.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="expression">Callback to convert from source type to the target type.</param>
        public static void RegisterConverter<TSource, TTarget>(Func<TSource, TTarget> expression)
        {
            Default.RegisterConverter(expression);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped target object.</returns>
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            return Default.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped target object.</returns>
        public static TTarget Map<TTarget>(object source)
        {
            return Default.Map<TTarget>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        public static void Map<TSource, TTarget>(TSource source, TTarget target)
        {
            Default.Map(source, target);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        public static void Map(object source, object target)
        {
            Default.Map(source, target);
        }

        /// <summary>
        /// Execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source array to map from.</param>
        /// <returns>The mapped target array.</returns>
        public static TTarget[] Map<TSource, TTarget>(TSource[] sources)
        {
            return Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source <see cref="IEnumerable{T}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="sources">The source to map from.</param>
        /// <returns>The mapped target instance.</returns>
        public static IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> sources)
        {
            return Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target collection.</returns>
        public static ICollection<TTarget> Map<TSource, TTarget>(ICollection<TSource> sources)
        {
            return Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        public static IList<TTarget> Map<TSource, TTarget>(IList<TSource> sources)
        {
            return Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        public static List<TTarget> Map<TSource, TTarget>(List<TSource> sources)
        {
            return Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Returns a mapper instance for specified types.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <returns>A mapper instance for specified types.</returns>
        public static IInstanceMapper<TSource, TTarget> GetMapper<TSource, TTarget>()
        {
            return Default.GetMapper<TSource, TTarget>();
        }

        /// <summary>
        /// Returns a type mapping instance of specified types for the configuration purpose.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <returns>A type mapping instance of specified types for the configuration purpose.</returns>
        public static ITypeMapper<TSource, TTarget> Configure<TSource, TTarget>()
        {
            return Default.Configure<TSource, TTarget>();
        }
    }
}
