using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Wheatech.EmitMapper
{
    /// <summary>
    /// Represents a collection of member mapping strategies.
    /// </summary>
    public class MemberMappingCollection : IEnumerable<MemberMapping>
    {
        private readonly List<MemberMapping> _mappings = new List<MemberMapping>();

        /// <summary>
        /// Specify the member mapping strategy with the source and target members.
        /// </summary>
        /// <param name="sourceMember">The source mapping member.</param>
        /// <param name="targetMember">The target mapping member.</param>
        /// <returns>The member mapping strategy.</returns>
        public MemberMapping Set(MappingMember sourceMember, MappingMember targetMember)
        {
            var mapping = _mappings.FirstOrDefault(m => m.TargetMember == targetMember);
            if (mapping == null)
            {
                _mappings.Add(mapping = new MemberMapping(sourceMember, targetMember));
            }
            else
            {
                mapping.SourceMember = sourceMember;
            }
            return mapping;
        }

        /// <summary>
        /// Ignore the specified member during mapping.
        /// </summary>
        /// <param name="targetMember">The target member to ignore during mapping.</param>
        public void Ignore(MappingMember targetMember)
        {
            _mappings.RemoveAll(mapping => mapping.TargetMember == targetMember);
        }

        /// <summary>
        /// Ignore the specified member during mapping.
        /// </summary>
        /// <param name="memberName">The member name to ignore during mapping.</param>
        public void Ignore(string memberName)
        {
            _mappings.RemoveAll(mapping => mapping.TargetMember.MemberName == memberName);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{MemberMapping}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MemberMapping> GetEnumerator()
        {
            return _mappings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
