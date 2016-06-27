using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Wheatech.ObjectMapper
{
    internal sealed class ConverterCollection
    {
        private readonly ObjectMapper _container;
        private readonly IList<Converter> _converters = new List<Converter>();

        private readonly ConcurrentDictionary<Tuple<Type, Type>, Converter> _resolvedConverters =
            new ConcurrentDictionary<Tuple<Type, Type>, Converter>();
        private bool _readonly;

        public ConverterCollection(ObjectMapper container)
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

        internal void Add(Converter converter)
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
            Add(new LambdaConverter<TSource, TTarget>(expression));
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
            Add(new LambdaConverter<TSource, TTarget>(expression) { Intrinsic = true });
        }

        internal Converter Get<TSource, TTarget>()
        {
            return Get(typeof(TSource), typeof(TTarget));
        }

        internal Converter Find(ConverterMatchContext context)
        {
            return (from converter in _converters
                    let score = converter.Match(context)
                    where score >= 0
                    orderby score, converter.Intrinsic ? 1 : 0
                    select converter).FirstOrDefault();
        }

        internal Converter Get(Type sourceType, Type targetType)
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
