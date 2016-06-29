using System;

namespace Wheatech.EmitMapper
{
    /// <summary>
    /// Represents the mapping strategy between the source member and the target member.
    /// </summary>
    public class MemberMapping
    {
        internal MemberMapping(MappingMember sourceMember, MappingMember targetMember)
        {
            SourceMember = sourceMember;
            TargetMember = targetMember;
        }

        /// <summary>
        /// Gets the source member of the mapping strategy.
        /// </summary>
        public MappingMember SourceMember { get; internal set; }

        /// <summary>
        /// Gets the target member of the mapping strategy.
        /// </summary>
        public MappingMember TargetMember { get; internal set; }

        internal ValueConverter Converter { get; private set; }

        /// <summary>
        /// Specify a callback function to convert the value of source member to the target member.
        /// </summary>
        /// <typeparam name="TSource">The type of the source member.</typeparam>
        /// <typeparam name="TTarget">The type of the target member.</typeparam>
        /// <param name="converter">The callback function to convert value.</param>
        public void ConvertWith<TSource, TTarget>(Func<TSource, TTarget> converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }
            if (!typeof(TSource).IsAssignableFrom(SourceMember.MemberType))
            {
                throw new ArgumentException(
                    "The source type of the converter does not match the type of the source member.", "converter");
            }
            if (!typeof(TTarget).IsAssignableFrom(TargetMember.MemberType))
            {
                throw new ArgumentException(
                    "The target type of the converter does not match the type of the target member.", "converter");
            }
            Converter = new LambdaValueConverter<TSource, TTarget>(converter);
        }
    }
}
