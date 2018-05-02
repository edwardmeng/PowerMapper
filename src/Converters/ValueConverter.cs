using System;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal abstract class ValueConverter
    {
        internal bool Intrinsic { get; set; }

        internal MappingContainer Container { get; set; }

        public abstract int Match(ConverterMatchContext context);

        public abstract void Compile(ModuleBuilder builder);

        public abstract void Emit(Type sourceType, Type targetType, CompilationContext context);

        public virtual Delegate CreateDelegate(Type sourceType, Type targetType, ModuleBuilder builder)
        {
            var typeBuilder = builder.DefineStaticType();
            var methodBuilder = typeBuilder.DefineStaticMethod("Convert");
            methodBuilder.SetReturnType(targetType);
            methodBuilder.SetParameters(sourceType);
            var il = methodBuilder.GetILGenerator();
            var context = new CompilationContext(il);
            il.Emit(OpCodes.Ldarg_0);
            context.CurrentType = sourceType;
            Emit(sourceType, targetType, context);
            context.Emit(OpCodes.Ret);
#if NETSTANDARD
            return typeBuilder.CreateTypeInfo()
                .GetMethod("Convert", BindingFlags.Static | BindingFlags.Public)
                .CreateDelegate(typeof(Func<,>).MakeGenericType(sourceType, targetType));
#else
            return Delegate.CreateDelegate(typeof(Func<,>).MakeGenericType(sourceType, targetType), typeBuilder.CreateType(), "Convert");
#endif
        }
    }
}
