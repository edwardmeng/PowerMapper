using System;

namespace PowerMapper
{
    /// <summary>
    /// Interface defining the behavior of the converting and mapping container.
    /// </summary>
    public interface IMappingContainer
    {
        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped target object.</returns>
        TTarget Map<TSource, TTarget>(TSource source);

        /// <summary>
        /// Execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        void Map<TSource, TTarget>(TSource source, TTarget target);

        /// <summary>
        /// Returns a mapper instance for specified types.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <returns>A mapper instance for specified types.</returns>
        IInstanceMapper<TSource, TTarget> GetMapper<TSource, TTarget>();

        /// <summary>
        /// Returns a type mapping instance of specified types for the configuration purpose.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <returns>A type mapping instance of specified types for the configuration purpose.</returns>
        ITypeMapper<TSource, TTarget> Configure<TSource, TTarget>();

        /// <summary>
        /// Gets a <see cref="ConventionCollection"/> object that used to manage the conventions.
        /// </summary>
        /// <value>A <see cref="ConventionCollection"/> object that used to manage the conventions.</value>
        ConventionCollection Conventions { get; }

        /// <summary>
        /// Registers a custom converter to the <see cref="IMappingContainer"/> instance.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="expression">Callback to convert from source type to the target type.</param>
        void RegisterConverter<TSource, TTarget>(Func<TSource, TTarget> expression);
    }
}
