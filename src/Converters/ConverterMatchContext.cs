using System;
using System.Collections;

namespace Wheatech.EmitMapper
{
    internal sealed class ConverterMatchContext
    {
        private Hashtable _properties;

        public ConverterMatchContext(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public Type SourceType { get; }

        public Type TargetType { get; }

        public Hashtable Properties => _properties ?? (_properties = new Hashtable());
    }
}
