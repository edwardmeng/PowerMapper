using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Wheatech.ObjectMapper
{
    internal class MemberMapperCollection : IEnumerable<MemberMapper>
    {
        private readonly ObjectMapper _container;
        private readonly MemberMapOptions _options;
        private readonly List<MemberMapper> _mappers = new List<MemberMapper>();
        private bool _readonly;

        public MemberMapperCollection(ObjectMapper container, MemberMapOptions options)
        {
            _container = container;
            _options = options;
        }

        private void CheckReadOnly()
        {
            if (_readonly)
            {
                throw new NotSupportedException("Collection is read-only");
            }
        }

        public void SetReadOnly()
        {
            if (!_readonly)
            {
                _readonly = true;
            }
        }

        public void Clear()
        {
            CheckReadOnly();
            _mappers.Clear();
        }

        public void Set(MemberMapper mapper)
        {
            CheckReadOnly();
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
            Remove(mapper.TargetMember);
            _mappers.Add(mapper);
        }

        public void Set(MappingMember targetMember, MappingMember sourceMember, Converter converter = null)
        {
            CheckReadOnly();
            if (targetMember == null)
            {
                throw new ArgumentNullException(nameof(targetMember));
            }
            if (sourceMember == null)
            {
                throw new ArgumentNullException(nameof(sourceMember));
            }
            Remove(targetMember);
            _mappers.Add(new DefaultMemberMapper(_container, _options, targetMember, sourceMember, converter));
        }

        public void Set<TSource, TMember>(MappingMember targetMember, Func<TSource, TMember> expression)
        {
            CheckReadOnly();
            if (targetMember == null)
            {
                throw new ArgumentNullException(nameof(targetMember));
            }
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            Remove(targetMember);
            _mappers.Add(new LambdaMemberMapper<TSource, TMember>(_container, _options, targetMember, expression));
        }

        public bool Remove(MappingMember targetMember)
        {
            CheckReadOnly();
            if (targetMember == null)
            {
                throw new ArgumentNullException(nameof(targetMember));
            }
            return _mappers.RemoveAll(m => m.TargetMember == targetMember) > 0;
        }

        public MemberMapper Get(MappingMember targetMember)
        {
            if (targetMember == null)
            {
                throw new ArgumentNullException(nameof(targetMember));
            }
            return _mappers.FirstOrDefault(mapper => mapper.TargetMember == targetMember);
        }

        public MemberMapper this[MappingMember targetMember] => Get(targetMember);

        public IEnumerator<MemberMapper> GetEnumerator()
        {
            return _mappers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
