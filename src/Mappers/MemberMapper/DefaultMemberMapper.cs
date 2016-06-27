using System;

namespace Wheatech.ObjectMapper
{
    internal class DefaultMemberMapper : MemberMapper
    {
        private readonly MappingMember _sourceMember;

        public DefaultMemberMapper(ObjectMapper container, MemberMapOptions options, MappingMember targetMember, MappingMember sourceMember,
            Converter converter)
            : base(container, options, targetMember, converter)
        {
            if (sourceMember == null)
            {
                throw new ArgumentNullException(nameof(sourceMember));
            }
            _sourceMember = sourceMember;
        }

        public override Type SourceType => _sourceMember.MemberType;

        protected override void EmitSource(CompilationContext context)
        {
            ((IMemberBuilder)_sourceMember).EmitGetter(context);
        }
    }
}
