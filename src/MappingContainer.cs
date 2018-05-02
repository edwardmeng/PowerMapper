﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;

namespace PowerMapper
{
    /// <summary>
    /// Main entry point for the object mapper component.
    /// </summary>
    internal class MappingContainer : IMappingContainer
    {
        #region Fields

        private static int _counter;
        private readonly ModuleBuilder _moduleBuilder;
        private bool _compiled;

        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance of <see cref="MappingContainer"/>.
        /// </summary>
        public MappingContainer()
        {

            var assemblyName = new AssemblyName("ILEmit_TypeMappers" + Interlocked.Increment(ref _counter));
#if NETSTANDARD
            _moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect).DefineDynamicModule(assemblyName.Name);
#else
            _moduleBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave)
                    .DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll", false);
#endif
            Converters = new ValueConverterCollection(this);
            Converters.Add(new PrimitiveValueConverter { Intrinsic = true });
            Converters.Add(new ObjectToStringConverter { Intrinsic = true });
            Converters.Add(new FromStringConverter { Intrinsic = true });

            #region bool Converters

            // sbyte -> bool
            Converters.AddIntrinsic((sbyte source) => source != 0);
            // byte -> bool
            Converters.AddIntrinsic((byte source) => source != 0);
            // char -> bool
            Converters.AddIntrinsic((char source) => source != 0);
            // int -> bool
            Converters.AddIntrinsic((int source) => source != 0);
            // uint -> bool
            Converters.AddIntrinsic((uint source) => source != 0);
            // long -> bool
            Converters.AddIntrinsic((long source) => source != 0);
            // ulong -> bool
            Converters.AddIntrinsic((ulong source) => source != 0);
            // short -> bool
            Converters.AddIntrinsic((short source) => source != 0);
            // ushort -> bool
            Converters.AddIntrinsic((ushort source) => source != 0);
            // decimal -> bool
            Converters.AddIntrinsic((decimal source) => source != 0);
            // float -> bool
            Converters.AddIntrinsic((float source) => Math.Abs(source) > float.Epsilon);
            // double -> bool
            Converters.AddIntrinsic((double source) => Math.Abs(source) > double.Epsilon);

            // bool -> int
            Converters.AddIntrinsic((bool source) => (source ? 1 : 0));
            // bool -> char
            Converters.AddIntrinsic((bool source) => source ? 'T' : 'F');
            // bool -> byte
            Converters.AddIntrinsic((bool source) => (byte)(source ? 1 : 0));
            // bool -> short
            Converters.AddIntrinsic((bool source) => (short)(source ? 1 : 0));
            // bool -> ushort
            Converters.AddIntrinsic((bool source) => (ushort)(source ? 1 : 0));
            // bool -> uint
            Converters.AddIntrinsic((bool source) => (uint)(source ? 1 : 0));
            // bool -> long
            Converters.AddIntrinsic((bool source) => (long)(source ? 1 : 0));
            // bool -> ulong
            Converters.AddIntrinsic((bool source) => (ulong)(source ? 1 : 0));
            // bool -> decimal
            Converters.AddIntrinsic((bool source) => (decimal)(source ? 1 : 0));
            // bool -> float
            Converters.AddIntrinsic((bool source) => (float)(source ? 1 : 0));
            // bool -> double
            Converters.AddIntrinsic((bool source) => (double)(source ? 1 : 0));
            // bool -> sbyte
            Converters.AddIntrinsic((bool source) => (sbyte)(source ? 1 : 0));

            #endregion

            #region DateTime Converters

            // DateTime -> long
            Converters.AddIntrinsic((DateTime source) => source.Ticks);
            // DateTime -> ulong
            Converters.AddIntrinsic((DateTime source) => (ulong)source.Ticks);

            // sbyte -> DateTime
            Converters.AddIntrinsic((sbyte source) => new DateTime(source));
            // byte -> DateTime
            Converters.AddIntrinsic((byte source) => new DateTime(source));
            // char -> DateTime
            Converters.AddIntrinsic((char source) => new DateTime(source));
            // int -> DateTime
            Converters.AddIntrinsic((int source) => new DateTime(source));
            // uint -> DateTime
            Converters.AddIntrinsic((uint source) => new DateTime(source));
            // long -> DateTime
            Converters.AddIntrinsic((long source) => new DateTime(source));
            // ulong -> DateTime
            Converters.AddIntrinsic((ulong source) => new DateTime((long)source));
            // short -> DateTime
            Converters.AddIntrinsic((short source) => new DateTime(source));
            // ushort -> DateTime
            Converters.AddIntrinsic((ushort source) => new DateTime(source));
            // decimal -> DateTime
            Converters.AddIntrinsic((decimal source) => new DateTime((long)source));
            // float -> DateTime
            Converters.AddIntrinsic((float source) => new DateTime((long)source));
            // double -> DateTime
            Converters.AddIntrinsic((double source) => new DateTime((long)source));

            #endregion

            #region TimeSpan Converters

            // TimeSpan -> long
            Converters.AddIntrinsic((TimeSpan source) => source.Ticks);
            // TimeSpan -> ulong
            Converters.AddIntrinsic((TimeSpan source) => (ulong)source.Ticks);

            // sbyte -> TimeSpan
            Converters.AddIntrinsic((sbyte source) => new TimeSpan(source));
            // byte -> TimeSpan
            Converters.AddIntrinsic((byte source) => new TimeSpan(source));
            // char -> TimeSpan
            Converters.AddIntrinsic((char source) => new TimeSpan(source));
            // int -> TimeSpan
            Converters.AddIntrinsic((int source) => new TimeSpan(source));
            // uint -> TimeSpan
            Converters.AddIntrinsic((uint source) => new TimeSpan(source));
            // long -> TimeSpan
            Converters.AddIntrinsic((long source) => new TimeSpan(source));
            // ulong -> TimeSpan
            Converters.AddIntrinsic((ulong source) => new TimeSpan((long)source));
            // short -> TimeSpan
            Converters.AddIntrinsic((short source) => new TimeSpan(source));
            // ushort -> TimeSpan
            Converters.AddIntrinsic((ushort source) => new TimeSpan(source));
            // decimal -> TimeSpan
            Converters.AddIntrinsic((decimal source) => new TimeSpan((long)source));
            // float -> TimeSpan
            Converters.AddIntrinsic((float source) => new TimeSpan((long)source));
            // double -> TimeSpan
            Converters.AddIntrinsic((double source) => new TimeSpan((long)source));

            #endregion

            #region Byte Array Converters

            // bool -> byte[]
            Converters.AddIntrinsic((bool source) => BitConverter.GetBytes(source));
            // byte[] -> bool
            Converters.AddIntrinsic((byte[] source) => source != null && BitConverter.ToBoolean(source, 0));

            // char -> byte[]
            Converters.AddIntrinsic((char source) => BitConverter.GetBytes(source));
            // byte[] -> char
            Converters.AddIntrinsic((byte[] source) => source == null ? '\0' : BitConverter.ToChar(source, 0));

            // double -> byte[]
            Converters.AddIntrinsic((double source) => BitConverter.GetBytes(source));
            // byte[] -> double
            Converters.AddIntrinsic((byte[] source) => source == null ? (double)0 : BitConverter.ToDouble(source, 0));

            // float -> byte[]
            Converters.AddIntrinsic((float source) => BitConverter.GetBytes(source));
            // byte[] -> float
            Converters.AddIntrinsic((byte[] source) => source == null ? (float)0 : BitConverter.ToSingle(source, 0));

            // short -> byte[]
            Converters.AddIntrinsic((short source) => BitConverter.GetBytes(source));
            // byte[] -> short
            Converters.AddIntrinsic((byte[] source) => source == null ? (short)0 : BitConverter.ToInt16(source, 0));

            // ushort -> byte[]
            Converters.AddIntrinsic((ushort source) => BitConverter.GetBytes(source));
            // byte[] -> ushort
            Converters.AddIntrinsic((byte[] source) => source == null ? (ushort)0 : BitConverter.ToUInt16(source, 0));

            // int -> byte[]
            Converters.AddIntrinsic((int source) => BitConverter.GetBytes(source));
            // byte[] -> int
            Converters.AddIntrinsic((byte[] source) => source == null ? 0 : BitConverter.ToInt32(source, 0));

            // uint -> byte[]
            Converters.AddIntrinsic((uint source) => BitConverter.GetBytes(source));
            // byte[] -> uint
            Converters.AddIntrinsic((byte[] source) => source == null ? (uint)0 : BitConverter.ToUInt32(source, 0));

            // long -> byte[]
            Converters.AddIntrinsic((long source) => BitConverter.GetBytes(source));
            // byte[] -> long
            Converters.AddIntrinsic((byte[] source) => source == null ? (long)0 : BitConverter.ToInt64(source, 0));

            // ulong -> byte[]
            Converters.AddIntrinsic((ulong source) => BitConverter.GetBytes(source));
            // byte[] -> ulong
            Converters.AddIntrinsic((byte[] source) => source == null ? (ulong)0 : BitConverter.ToUInt64(source, 0));

            #endregion

            #region Misc Converters

            // DateTime -> DateTimeOffset
            Converters.AddIntrinsic((DateTime source) => new DateTimeOffset(source));
            // Guid -> byte[]
            Converters.AddIntrinsic((Guid source) => source.ToByteArray());
            // byte[] -> Guid
            Converters.AddIntrinsic((byte[] source) => source == null ? Guid.Empty : new Guid(source));
            // byte[] -> IPAddress
            Converters.AddIntrinsic((byte[] source) => source != null ? new IPAddress(source) : null);
            // IPAddress -> byte[]
            Converters.AddIntrinsic((IPAddress source) => source?.GetAddressBytes());
            // string -> Uri
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? null : new Uri(source));
            // string -> StringBuilder
            Converters.AddIntrinsic((string source) => source == null ? null : new StringBuilder(source));

#if NET35
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? null : new Version(source));
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? Guid.Empty : new Guid(source));
#else
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? null : new FrameworkName(source));
            Converters.AddIntrinsic((System.Numerics.BigInteger source) => source.ToByteArray());
            Converters.AddIntrinsic((byte[] source) => source == null ? System.Numerics.BigInteger.Zero : new System.Numerics.BigInteger(source));
#endif
#if !NETSTANDARD
            // string -> Type
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? null : TypeNameConverter.GetType(source, true, false));
            // string -> TimeZoneInfo
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? null : TimeZoneInfo.FromSerializedString(source));
            // TimeZoneInfo -> string
            Converters.AddIntrinsic((TimeZoneInfo source) => source?.ToSerializedString());
#else
            // string -> Type
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? null : Type.GetType(source, true, false));
            // TypeInfo -> string
            Converters.AddIntrinsic((TypeInfo source)=> source?.AssemblyQualifiedName);
            // string -> TypeInfo
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? null : Type.GetType(source, true, false)?.GetTypeInfo());
            // TypeInfo -> Type
            Converters.AddIntrinsic((TypeInfo source) => source?.AsType());
            // TypeInfo -> Type
            Converters.AddIntrinsic((Type source) => source?.GetTypeInfo());
            // string -> TimeZoneInfo
            Converters.AddIntrinsic((string source) => string.IsNullOrEmpty(source) || source.Trim().Length == 0 ? null : TimeZoneInfo.FindSystemTimeZoneById(source));
            // TimeZoneInfo -> string
            Converters.AddIntrinsic((TimeZoneInfo source) => source?.Id);
#endif
            // Type -> string
            Converters.AddIntrinsic((Type source) => source?.AssemblyQualifiedName);

            #endregion

            Conventions.Add<MatchNameConvention>();
        }

        #endregion

        #region Extension Points

        /// <summary>
        /// Gets a <see cref="ConventionCollection"/> object that used to manage the conventions.
        /// </summary>
        /// <value>A <see cref="ConventionCollection"/> object that used to manage the conventions.</value>
        public ConventionCollection Conventions { get; } = new ConventionCollection();

        /// <summary>
        /// Registers a custom converter to the <see cref="MappingContainer"/> instance.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="expression">Callback to convert from source type to the target type.</param>
        public void RegisterConverter<TSource, TTarget>(Func<TSource, TTarget> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            Converters.Add(expression);
        }

        #endregion

        #region Entry Points

        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped target object.</returns>
        public TTarget Map<TSource, TTarget>(TSource source)
        {
            return InstanceMapper<TSource, TTarget>.GetInstance(this).Map(source);
        }

        /// <summary>
        /// Execute a mapping from the source list of <typeparamref name="TSource"/> to a new destination list of <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TTarget">The element type of the target list.</typeparam>
        /// <param name="sources">The source collection to map from.</param>
        /// <returns>The mapped target list.</returns>
        public List<TTarget> Map<TSource, TTarget>(List<TSource> sources)
        {
            return Map<List<TSource>, List<TTarget>>(sources);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="target">Target object to map into.</param>
        public void Map<TSource, TTarget>(TSource source, TTarget target)
        {
            InstanceMapper<TSource, TTarget>.GetInstance(this).Map(source, target);
        }

        /// <summary>
        /// Returns a mapper instance for specified types.
        /// </summary>
        /// <typeparam name="TSource">The type of source object.</typeparam>
        /// <typeparam name="TTarget">The type of target object.</typeparam>
        /// <returns>A mapper instance for specified types.</returns>
        public IInstanceMapper<TSource, TTarget> GetMapper<TSource, TTarget>()
        {
            return InstanceMapper<TSource, TTarget>.GetInstance(this);
        }

        /// <summary>
        /// Returns a type mapping instance of specified types for the configuration purpose.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <returns>A type mapping instance of specified types for the configuration purpose.</returns>
        public ITypeMapper<TSource, TTarget> Configure<TSource, TTarget>()
        {
            return TypeMapper<TSource, TTarget>.GetInstance(this);
        }

        #endregion

        #region Native Points

        /// <summary>
        /// Gets a <see cref="ValueConverterCollection"/> object that used to register converters.
        /// </summary>
        /// <value>A <see cref="ValueConverterCollection"/> object that used to register converters.</value>
        internal ValueConverterCollection Converters { get; }

        internal Func<TSource, TTarget> GetMapFunc<TSource, TTarget>()
        {
            Compile();
            var converter = Converters.Get<TSource, TTarget>();
            if (converter != null)
            {
                return (Func<TSource, TTarget>)converter.CreateDelegate(typeof(TSource), typeof(TTarget), _moduleBuilder);
            }
            if (EnumerableValueConverter.TryCreate(typeof(TSource), typeof(TTarget), this, out converter))
            {
                converter.Compile(_moduleBuilder);
                return (Func<TSource, TTarget>)converter.CreateDelegate(typeof(TSource), typeof(TTarget), _moduleBuilder);
            }
            var typeMapper = TypeMapper<TSource, TTarget>.GetInstance(this);
            typeMapper.SetReadOnly();
            typeMapper.Compile(_moduleBuilder);
            return typeMapper.CreateConverter(_moduleBuilder);
        }

        internal Action<TSource, TTarget> GetMapAction<TSource, TTarget>()
        {
            Compile();
            ValueMapper mapper;
            if (EnumerableMapper.TryCreate(typeof(TSource), typeof(TTarget), this, out mapper))
            {
                mapper.Compile(_moduleBuilder);
                return (Action<TSource, TTarget>)mapper.CreateDelegate(typeof(TSource), typeof(TTarget), _moduleBuilder);
            }

            var typeMapper = TypeMapper<TSource, TTarget>.GetInstance(this);
            typeMapper.SetReadOnly();
            typeMapper.Compile(_moduleBuilder);
            return typeMapper.CreateMapper(_moduleBuilder);
        }

        internal void Compile()
        {
            if (!_compiled)
            {
                lock (this)
                {
                    if (!_compiled)
                    {
                        Conventions.SetReadOnly();
                        Converters.SetReadOnly();
                        Converters.Compile(_moduleBuilder);
                        _compiled = true;
                    }
                }
            }
        }

        #endregion
    }
}
