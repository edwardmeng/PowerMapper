using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Wheatech.EmitMapper
{
    /// <summary>
    /// The convenient entry point of the <see cref="ObjectMapper"/>.
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Gets a <see cref="ConventionCollection"/> object that used to manage the conventions.
        /// </summary>
        /// <value>A <see cref="ConventionCollection"/> object that used to manage the conventions.</value>
        public static ConventionCollection Conventions => ObjectMapper.Default.Conventions;

        /// <summary>
        /// Registers a custom converter to the <see cref="ObjectMapper"/> instance.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="expression">Callback to convert from source type to the target type.</param>
        public static void RegisterConverter<TSource, TTarget>(Func<TSource, TTarget> expression)
        {
            ObjectMapper.Default.RegisterConverter(expression);
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
            return ObjectMapper.Default.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TSource, TTarget>(TSource source, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(source, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TSource, TTarget>(TSource source)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped target object.</returns>
        public static TTarget Map<TTarget>(object source)
        {
            return ObjectMapper.Default.Map<TTarget>(source);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TTarget>(object source, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync<TTarget>(source, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget> MapAsync<TTarget>(object source)
        {
            return ObjectMapper.Default.MapAsync<TTarget>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="source">Source object to map from.</param>
        /// <param name="targetType">The type of target object.</param>
        /// <returns>Mapped target object.</returns>
        public static object Map(object source, Type targetType)
        {
            return ObjectMapper.Default.Map(source, targetType);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="source">Source object to map from.</param>
        /// <param name="targetType">The type of target object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<object> MapAsync(object source, Type targetType, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync(source, targetType, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="source">Source object to map from.</param>
        /// <param name="targetType">The type of target object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target object.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<object> MapAsync(object source, Type targetType)
        {
            return ObjectMapper.Default.MapAsync(source, targetType);
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
            ObjectMapper.Default.Map(source, target);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(TSource source, TTarget target, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync(source, target, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync<TSource, TTarget>(TSource source, TTarget target)
        {
            return ObjectMapper.Default.MapAsync(source, target);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        public static void Map(object source, object target)
        {
            ObjectMapper.Default.Map(source, target);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync(object source, object target, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync(source, target, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task MapAsync(object source, object target)
        {
            return ObjectMapper.Default.MapAsync(source, target);
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
            return ObjectMapper.Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source array to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target array.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget[]> MapAsync<TSource, TTarget>(TSource[] sources, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source array of <typeparamref name="TSource"/> to a new destination array of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TTarget">The element type of the target array.</typeparam>
        /// <param name="sources">The source array to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target array.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<TTarget[]> MapAsync<TSource, TTarget>(TSource[] sources)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources);
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
            return ObjectMapper.Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target <see cref="IEnumerable{TSource}"/>.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IEnumerable<TTarget>> MapAsync<TSource, TTarget>(IEnumerable<TSource> sources, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source <see cref="IEnumerable{TSource}"/> to a new destination <see cref="IEnumerable{TTarget}"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source.</typeparam>
        /// <typeparam name="TTarget">The element type of the target.</typeparam>
        /// <param name="sources">The source <see cref="IEnumerable{TSource}"/> to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target <see cref="IEnumerable{TSource}"/>.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IEnumerable<TTarget>> MapAsync<TSource, TTarget>(IEnumerable<TSource> sources)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources);
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
            return ObjectMapper.Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target collection.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<ICollection<TTarget>> MapAsync<TSource, TTarget>(ICollection<TSource> sources, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source collection of <typeparamref name="TSource"/> to a new destination collection of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source collection.</typeparam>
        /// <typeparam name="TTarget">The element type of the target collection.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target collection.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<ICollection<TTarget>> MapAsync<TSource, TTarget>(ICollection<TSource> sources)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources);
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
            return ObjectMapper.Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target list.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IList<TTarget>> MapAsync<TSource, TTarget>(IList<TSource> sources, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target list.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IList<TTarget>> MapAsync<TSource, TTarget>(IList<TSource> sources)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources);
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
            return ObjectMapper.Default.Map<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target list.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<List<TTarget>> MapAsync<TSource, TTarget>(List<TSource> sources, CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources, cancellationToken);
        }

        /// <summary>
        /// Asynchronously execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped target list.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<List<TTarget>> MapAsync<TSource, TTarget>(List<TSource> sources)
        {
            return ObjectMapper.Default.MapAsync<TSource, TTarget>(sources);
        }

        /// <summary>
        /// Returns a mapper instance for specified types.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <returns>A mapper instance for specified types.</returns>
        public static IInstanceMapper<TSource, TTarget> GetMapper<TSource, TTarget>()
        {
            return ObjectMapper.Default.GetMapper<TSource, TTarget>();
        }

        /// <summary>
        /// Asynchronously returns a mapper instance for specified types.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapper instance for specified types.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IInstanceMapper<TSource, TTarget>> GetMapperAsync<TSource, TTarget>(CancellationToken cancellationToken)
        {
            return ObjectMapper.Default.GetMapperAsync<TSource, TTarget>(cancellationToken);
        }

        /// <summary>
        /// Asynchronously returns a mapper instance for specified types.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapper instance for specified types.</returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported. 
        /// Use 'await' to ensure that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public static Task<IInstanceMapper<TSource, TTarget>> GetMapperAsync<TSource, TTarget>()
        {
            return ObjectMapper.Default.GetMapperAsync<TSource, TTarget>();
        }

        /// <summary>
        /// Returns a type mapping instance of specified types for the configuration purpose.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <returns>A type mapping instance of specified types for the configuration purpose.</returns>
        public static ITypeMapper<TSource, TTarget> Configure<TSource, TTarget>()
        {
            return ObjectMapper.Default.Configure<TSource, TTarget>();
        }
    }
}
