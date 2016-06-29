using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal class MappingProperty : MappingMember
    {
        private readonly PropertyInfo _property;

        public MappingProperty(PropertyInfo property)
        {
            _property = property;
        }

        public override Type DeclaringType => _property.DeclaringType;

        public override string MemberName => _property.Name;

        public override Type MemberType => _property.PropertyType;

        public override MemberInfo ClrMember => _property;

        public override bool CanRead(bool includeNonPublic) => _property.GetGetMethod(includeNonPublic) != null;

        public override bool CanWrite(bool includeNonPublic) => _property.GetSetMethod(includeNonPublic) != null;

        internal override void EmitSetter(CompilationContext context)
        {
            var local = context.DeclareLocal(context.CurrentType);
            context.Emit(OpCodes.Stloc, local);

            var setMethod = _property.GetSetMethod(true);
            context.LoadTarget(LoadPurpose.MemberAccess);
            context.Emit(OpCodes.Ldloc, local);
            if (MemberType != context.CurrentType)
            {
                context.EmitCast(MemberType);
            }
            context.EmitCall(setMethod);
            context.CurrentType = null;
        }

        internal override void EmitGetter(CompilationContext context)
        {
            var getMethod = _property.GetGetMethod(true);
            context.LoadSource(LoadPurpose.MemberAccess);
            context.EmitCall(getMethod);
            context.CurrentType = MemberType;
        }
    }
}
