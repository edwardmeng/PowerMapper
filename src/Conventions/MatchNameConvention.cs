using System;
using System.Linq;

namespace Wheatech.ObjectMapper
{
    /// <summary>
    /// The convention to match mapping member names with options.
    /// </summary>
    public class MatchNameConvention : IConvention
    {
        #region Fields

        private bool _readonly;
        private MemberMapOptions _options;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the options for member matching algorithm.
        /// </summary>
        /// <value>
        /// The options for member matching algorithm.
        /// </value>
        public MemberMapOptions Options
        {
            get { return _options; }
            set
            {
                CheckReadOnly();
                _options = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies the convention to type member mappings.
        /// </summary>
        /// <param name="context">The context to apply conventions.</param>
        public void Apply(ConventionContext context)
        {
            var options = context.Options == MemberMapOptions.Default ? Options : context.Options;
            bool includeNonPublic = options.HasFlag(MemberMapOptions.NonPublic);
            var targetMembers = context.TargetMembers.Where(member => member.CanWrite(includeNonPublic)).ToArray();
            var sourceMembers = context.SourceMembers.Where(member => member.CanRead(includeNonPublic)).ToArray();
            var comparer = options.HasFlag(MemberMapOptions.IgnoreCase)
                ? StringComparer.CurrentCultureIgnoreCase
                : StringComparer.CurrentCulture;
            var hierarchy = options.HasFlag(MemberMapOptions.Hierarchy);
            foreach (var memberName in targetMembers.Select(member => member.MemberName).Distinct(comparer))
            {
                MappingMembers(context,
                    targetMembers.Where(member => comparer.Equals(member.MemberName, memberName)).ToArray(),
                    sourceMembers.Where(member => comparer.Equals(member.MemberName, memberName)).ToArray(),
                    hierarchy);
            }
        }

        private void MappingMembers(ConventionContext context, MappingMember[] targetMembers,
            MappingMember[] sourceMembers, bool hierarchy)
        {
            if (hierarchy)
            {
                var minLength = Math.Min(targetMembers.Length, sourceMembers.Length);
                for (int i = 0; i < minLength; i++)
                {
                    context.Mappings.Set(sourceMembers[sourceMembers.Length - 1 - i],
                        targetMembers[targetMembers.Length - 1 - i]);
                }
            }
            else
            {
                for (int targetIndex = targetMembers.Length - 1, sourceIndex = sourceMembers.Length - 1;
                    targetIndex >= 0 && sourceIndex >= 0; targetIndex--)
                {
                    MappingMember targetMember = targetMembers[targetIndex], sourceMember = sourceMembers[sourceIndex];
                    if (targetMember.MemberType.IsAssignableFrom(sourceMember.MemberType) ||
                        context.Converters.Get(sourceMember.MemberType, targetMember.MemberType) != null)
                    {
                        context.Mappings.Set(sourceMember, targetMember);
                        sourceIndex--;
                    }
                    else if (targetIndex >= sourceIndex)
                    {
                        sourceIndex--;
                    }
                }
            }
        }

        private void CheckReadOnly()
        {
            if (_readonly)
            {
                throw new NotSupportedException("The conversation is read-only");
            }
        }

        void IConvention.SetReadOnly()
        {
            _readonly = true;
        }

        #endregion
    }
}
