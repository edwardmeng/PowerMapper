using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal sealed class ValueConverterCollection
    {
        private readonly ObjectMapper _container;
        private readonly IList<ValueConverter> _converters = new List<ValueConverter>();

        private readonly ConcurrentDictionary<Tuple<Type, Type>, ValueConverter> _resolvedConverters =
            new ConcurrentDictionary<Tuple<Type, Type>, ValueConverter>();
        private bool _readonly;

        public ValueConverterCollection(ObjectMapper container)
        {
            _container = container;
        }

        private void CheckReadOnly()
        {
            if (_readonly)
            {
                throw new NotSupportedException("Collection is read-only");
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
            return _resolvedConverters.GetOrAdd(Tuple.Create(sourceType, targetType), key => Find(new ConverterMatchContext(key.Item1, key.Item2)));
        }
    }
}
