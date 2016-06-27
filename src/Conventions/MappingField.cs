using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Wheatech.ObjectMapper
{
    internal class MappingField : MappingMember
    {
        private readonly FieldInfo _field;

        public MappingField(FieldInfo field)
        {
            _field = field;
        }

        public override Type DeclaringType => _field.DeclaringType;

        public override string MemberName => _field.Name;

        public override Type MemberType => _field.FieldType;

        public override MemberInfo ClrMember => _field;

        public override bool CanRead(bool includeNonPublic) => includeNonPublic || _field.IsPublic;

        public override bool CanWrite(bool includeNonPublic) => (includeNonPublic || _field.IsPublic) && !(_field.IsLiteral || _field.IsInitOnly);

        internal override void EmitSetter(CompilationContext context)
        {
            var local = context.DeclareLocal(context.CurrentType);
            context.Emit(OpCodes.Stloc, local);
            context.LoadTarget(LoadPurpose.MemberAccess);
            context.Emit(OpCodes.Ldloc, local);
            if (MemberType != context.CurrentType)
            {
                context.EmitCast(MemberType);
            }
            context.Emit(OpCodes.Stfld, _field);
            context.CurrentType = null;
        }

        internal override void EmitGetter(CompilationContext context)
        {
            context.LoadSource(LoadPurpose.MemberAccess);
            context.Emit(OpCodes.Ldfld, _field);
            context.CurrentType = _field.FieldType;
        }
    }
}
