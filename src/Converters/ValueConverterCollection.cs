using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
#if !Net35
using System.Collections.Concurrent;
#endif

namespace PowerMapper
{
    internal sealed class ValueConverterCollection
    {
        private readonly MappingContainer _container;
        private readonly IList<ValueConverter> _converters = new List<ValueConverter>();
        private bool _readonly;

#if Net35
        private readonly Dictionary<Pair<Type, Type>, ValueConverter> _resolvedConverters =
            new Dictionary<Pair<Type, Type>, ValueConverter>();
        internal ValueConverter Get(Type sourceType, Type targetType)
        {
            if (sourceType == null)
            {
                throw new ArgumentNullException(nameof(sourceType));
            }
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            var key = Pair.Create(sourceType, targetType);
            ValueConverter converter;
            if (!_resolvedConverters.TryGetValue(key, out converter))
            {
                lock (_resolvedConverters)
                {
                    if (!_resolvedConverters.TryGetValue(key, out converter))
                    {
                        converter = Find(new ConverterMatchContext(key.First, key.Second));
                        _resolvedConverters.Add(key, converter);
                    }
                }
            }
            return converter;
        }
#else
        private readonly ConcurrentDictionary<Pair<Type, Type>, ValueConverter> _resolvedConverters =
            new ConcurrentDictionary<Pair<Type, Type>, ValueConverter>();

        internal ValueConverter Get(Type sourceType, Type targetType)
        {
            if (sourceType == null)
            {
                throw new ArgumentNullException(nameof(sourceType));
            }
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            return _resolvedConverters.GetOrAdd(Pair.Create(sourceType, targetType), key => Find(new ConverterMatchContext(key.First, key.Second)));
        }
#endif

        public ValueConverterCollection(MappingContainer container)
        {
            _container = container;
        }

        private void CheckReadOnly()
        {
            if (_readonly)
            {
                throw new NotSupportedException(Strings.Collection_ReadOnly);
            }
        }

        internal void SetReadOnly()
        {
            if (!_readonly)
            {
                _readonly = true;
            }
        }

        internal void Add(ValueConverter converter)
        {
            CheckReadOnly();
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }
            converter.Container = _container;
            _converters.Add(converter);
            _resolvedConverters.Clear();
        }

        public void Add<TSource, TTarget>(Func<TSource, TTarget> expression)
        {
            Add(new LambdaValueConverter<TSource, TTarget>(expression));
        }

        internal void Compile(ModuleBuilder builder)
        {
            foreach (var converter in _converters)
            {
                converter.Compile(builder);
            }
        }

        internal void AddIntrinsic<TSource, TTarget>(Func<TSource, TTarget> expression)
        {
            Add(new LambdaValueConverter<TSource, TTarget>(expression) { Intrinsic = true });
        }

        internal ValueConverter Get<TSource, TTarget>()
        {
            return Get(typeof(TSource), typeof(TTarget));
        }

        internal ValueConverter Find(ConverterMatchContext context)
        {
            return (from converter in _converters
                    let score = converter.Match(context)
                    where score >= 0
                    orderby score, converter.Intrinsic ? 1 : 0
                    select converter).FirstOrDefault();
        }
    }
}
