using System;
#if NetCore
using System.Collections.Generic;
#else
using System.Collections;
#endif

namespace PowerMapper
{
    internal sealed class ConverterMatchContext
    {
        public ConverterMatchContext(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public Type SourceType { get; }

        public Type TargetType { get; }

#if NetCore
        private readonly IDictionary<object,object> _properties = new Dictionary<object, object>();

        public object GetProperty(object key)
        {
            object value;
            return _properties.TryGetValue(key, out value) ? value : null;
        }

        public void SetProperty(object key, object value)
        {
            _properties[key] = value;
        }
#else
        private readonly Hashtable _properties = new Hashtable();
        public object GetProperty(object key)
        {
            return _properties[key];
        }

        public void SetProperty(object key, object value)
        {
            _properties[key] = value;
        }
#endif
    }
}
