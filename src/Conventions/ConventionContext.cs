using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wheatech.ObjectMapper
{
    /// <summary>
    /// Describes the source and target of the mapping strategy for conventions.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ConventionContext
    {
        private readonly ObjectMapper _container;

        internal ConventionContext(ObjectMapper container, Type sourceType, Type targetType, MemberMapOptions options)
        {
            _container = container;
            SourceType = sourceType;
            TargetType = targetType;
            Options = options;
            SourceMembers = new MappingMemberCollection(GetMembers(sourceType, true, false));
            TargetMembers = new MappingMemberCollection(GetMembers(targetType, false, true));
        }

        /// <summary>
        /// Gets the source type to map from.
        /// </summary>
        /// <value>The type to map from.</value>
        public Type SourceType { get; }

        /// <summary>
        /// Gets the target type to map to.
        /// </summary>
        /// <value>The target type to map to.</value>
        public Type TargetType { get; }

        /// <summary>
        /// Gets a <see cref="MappingMemberCollection"/> reprensents the members(properties and fields) of the <see cref="SourceType"/>.
        /// </summary>
        /// <value>The members of the <see cref="SourceType"/>.</value>
        public MappingMemberCollection SourceMembers { get; }

        /// <summary>
        /// Gets a <see cref="MappingMemberCollection"/> reprensents the members(properties and fields) of the <see cref="TargetType"/>.
        /// </summary>
        /// <value>The members of the <see cref="TargetType"/>.</value>
        public MappingMemberCollection TargetMembers { get; }

        /// <summary>
        /// Gets a <see cref="MemberMappingCollection"/> represents the mappings between source members and target members.
        /// </summary>
        /// <value>The mappings between source members and target members.</value>
        public MemberMappingCollection Mappings { get; } = new MemberMappingCollection();

        internal ConverterCollection Converters => _container.Converters;

        /// <summary>
        /// Gets the options that control the member matching algorithm.
        /// </summary>
        /// <value>The options that control the member matching algorithm.</value>
        public MemberMapOptions Options { get; }

        private IEnumerable<MappingMember> GetMembers(Type type, bool includeReadOnly, bool includeWriteOnly)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return type.GetFields(bindingFlags).Select(field => (MappingMember)new MappingField(field))
                .Concat(type.GetProperties(bindingFlags).Select(property => new MappingProperty(property)))
                .Where(member => (member.CanRead(true) && includeReadOnly) || (member.CanWrite(true) && includeWriteOnly));
        }
    }
}
