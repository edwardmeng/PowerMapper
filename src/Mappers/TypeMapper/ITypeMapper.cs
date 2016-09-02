using System;
using System.Collections.Generic;

namespace PowerMapper
{
    /// <summary>
    /// Mapping configuration options
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    public interface ITypeMapper<out TSource, TTarget>
    {
        /// <summary>
        /// Specify options for the member mapping algorithm.
        /// </summary>
        /// <param name="options">The options for the member mapping algorithm.</param>
        /// <returns>Itself</returns>
        ITypeMapper<TSource, TTarget> WithOptions(MemberMapOptions options);

        /// <summary>
        /// Skip member mapping and use a custom function to map to the target type.
        /// </summary>
        /// <param name="expression">Callback to map from source type to target type.</param>
        /// <returns>Itself</returns>
        ITypeMapper<TSource, TTarget> MapWith(Action<TSource, TTarget> expression);

        /// <summary>
        /// Supply a custom instantiation function for the target type.
        /// </summary>
        /// <param name="expression">Callback to create the target type given the source object.</param>
        /// <returns>Itself</returns>
        ITypeMapper<TSource, TTarget> CreateWith(Func<TSource, TTarget> expression);

        /// <summary>
        /// Skip specified convention member mapping and use a custom function to map to the target member.
        /// </summary>
        /// <typeparam name="TMember">The type of the target member.</typeparam>
        /// <param name="targetMember">The name of the target member.</param>
        /// <param name="expression">Callback to map from source type to the target member</param>
        /// <returns>Itself</returns>
        ITypeMapper<TSource, TTarget> MapMember<TMember>(string targetMember, Func<TSource, TMember> expression);

        /// <summary>
        /// Ignore the members during mapping.
        /// </summary>
        /// <param name="members">The member names to ignore during mapping.</param>
        /// <returns>Itself</returns>
        ITypeMapper<TSource, TTarget> Ignore(IEnumerable<string> members);

        /// <summary>
        /// Execute a custom function to the source and/or destination types before member mapping
        /// </summary>
        /// <param name="expression">Callback for the source/destination types</param>
        /// <returns>Itself</returns>
        ITypeMapper<TSource, TTarget> BeforeMap(Action<TSource, TTarget> expression);

        /// <summary>
        /// Execute a custom function to the source and/or destination types after member mapping
        /// </summary>
        /// <param name="expression">Callback for the source/destination types</param>
        /// <returns>Itself</returns>
        ITypeMapper<TSource, TTarget> AfterMap(Action<TSource, TTarget> expression);
    }
}
