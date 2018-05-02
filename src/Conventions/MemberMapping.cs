using System;
using System.Globalization;
using System.Reflection;

namespace PowerMapper
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
                throw new ArgumentNullException(nameof(converter));
            }
#if NETSTANDARD
            if (!typeof(TSource).GetTypeInfo().IsAssignableFrom(SourceMember.MemberType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.Converter_InvalidSourceType, typeof(TSource), SourceMember.MemberType), nameof(converter));
            }
            if (!typeof(TTarget).GetTypeInfo().IsAssignableFrom(TargetMember.MemberType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.Converter_InvalidTargetType, typeof(TTarget), TargetMember.MemberType), nameof(converter));
            }
#else
            if (!typeof(TSource).IsAssignableFrom(SourceMember.MemberType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,Strings.Converter_InvalidSourceType, typeof(TSource), SourceMember.MemberType), nameof(converter));
            }
            if (!typeof(TTarget).IsAssignableFrom(TargetMember.MemberType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.Converter_InvalidTargetType, typeof(TTarget), TargetMember.MemberType), nameof(converter));
            }
#endif
            Converter = new LambdaValueConverter<TSource, TTarget>(converter);
        }
    }
}
