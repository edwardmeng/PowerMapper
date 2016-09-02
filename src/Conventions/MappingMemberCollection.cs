using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PowerMapper.Properties;

namespace PowerMapper
{
    /// <summary>
    /// Represents a collection of member objects that inherit from <see cref="MappingMember"/>.
    /// </summary>
    public class MappingMemberCollection : IEnumerable<MappingMember>
    {
        #region Fields

        private readonly List<MappingMember> _members;
        private readonly StringComparer _comparer;

        #endregion

        #region Constructors

        internal MappingMemberCollection()
            : this(StringComparer.Ordinal)
        {
        }

        internal MappingMemberCollection(IEnumerable<MappingMember> collection, StringComparer comparer)
        {
            _comparer = comparer ?? StringComparer.Ordinal;
            _members = collection == null ? new List<MappingMember>() : new List<MappingMember>(collection);
        }

        internal MappingMemberCollection(IEnumerable<MappingMember> collection)
            : this(collection, StringComparer.Ordinal)
        {
        }

        internal MappingMemberCollection(StringComparer comparer)
            : this(null, comparer)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the member with the specified name.
        /// </summary>
        /// <param name="name">The name by which the member is identified.</param>
        /// <returns>The member with the specified name.</returns>
        public MappingMember this[string name]
        {
            get
            {
                var members = GetMembers(name);
                if (members.Length == 0) return null;
                var member = members[0];
                for (int i = 1; i < members.Length; i++)
                {
                    if (members[i].DeclaringType.IsSubclassOf(member.DeclaringType))
                    {
                        member = members[i];
                    }
                }
                return member;
            }
        }

        /// <summary>
        /// Gets all the members with the specified name.
        /// </summary>
        /// <param name="name">The name by which the member is identified.</param>
        /// <returns>The members with the specified name.</returns>
        public MappingMember[] GetMembers(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(Strings.Argument_CannotNullOrEmpty, nameof(name));
            }
            return _members.Where(member => _comparer.Equals(member.MemberName, name)).ToArray();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{MappingMember}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MappingMember> GetEnumerator()
        {
            return _members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
