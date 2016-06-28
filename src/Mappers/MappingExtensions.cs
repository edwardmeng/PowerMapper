using System;
using System.Linq.Expressions;

namespace Wheatech.ObjectMapper
{
    /// <summary>
    /// Extension class that adds a set of convenience overloads to the <see cref="ITypeMapper{TSource,TTarget}"/> class.
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Ignore the specified member during mapping.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <typeparam name="TMember">The type of the member to ignore during mapping of the target.</typeparam>
        /// <param name="typeMapper">The type mapping strategy.</param>
        /// <param name="expression">The expression of the target member to ignore during mapping.</param>
        /// <returns>The type mapping strategy.</returns>
        public static ITypeMapper<TSource, TTarget> Ignore<TSource, TTarget, TMember>(
            this ITypeMapper<TSource, TTarget> typeMapper, Expression<Func<TTarget, TMember>> expression)
        {
            return typeMapper.Ignore(ExtractMember(expression).Member.Name);
        }

        /// <summary>
        /// Ignore the members during mapping.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="typeMapper">The type mapping strategy.</param>
        /// <param name="members">The member names to ignore during mapping.</param>
        /// <returns>The type mapping strategy.</returns>
        public static ITypeMapper<TSource, TTarget> Ignore<TSource, TTarget>(
            this ITypeMapper<TSource, TTarget> typeMapper, params string[] members)
        {
            return typeMapper.Ignore(members);
        }

        /// <summary>
        /// Skip specified convention member mapping and use a custom function to map to the target member.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <typeparam name="TSourceMember">The type of the member to map to of the source.</typeparam>
        /// <typeparam name="TTargetMember">The type of the member to map to of the target.</typeparam>
        /// <param name="typeMapper">The type mapping strategy.</param>
        /// <param name="targetMember">The expression of the target member to map to.</param>
        /// <param name="expression">Callback to map from source type to the target member</param>
        /// <returns>The type mapping strategy.</returns>
        public static ITypeMapper<TSource, TTarget> MapMember<TSource, TTarget, TSourceMember, TTargetMember>(
            this ITypeMapper<TSource, TTarget> typeMapper, Expression<Func<TTarget, TSourceMember>> targetMember,
            Func<TSource, TTargetMember> expression)
        {
            return typeMapper.MapMember(ExtractMember(targetMember).Member.Name, expression);
        }

        private static MemberExpression ExtractMember(LambdaExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var unaryExpression = expression.Body as UnaryExpression;
            var memberExpression = (unaryExpression?.Operand ?? expression.Body) as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The expression must be property or field access expression.");
            }
            return memberExpression;
        }
    }
}
