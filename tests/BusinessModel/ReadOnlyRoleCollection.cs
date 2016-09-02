using System;
using System.Collections;
using System.Collections.Generic;

namespace PowerMapper.UnitTests.BusinessModel
{
    public class ReadOnlyRoleCollection : IEnumerable<Role>
    {
        private readonly IEnumerable<Role> _roles;

        public ReadOnlyRoleCollection(IEnumerable<Role> roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }
            _roles = roles;
        }

        public IEnumerator<Role> GetEnumerator()
        {
            return _roles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ReadOnlyRoleCollection1 : IEnumerable<Role>
    {
        private readonly ICollection<Role> _roles;

        public ReadOnlyRoleCollection1(ICollection<Role> roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }
            _roles = roles;
        }

        public IEnumerator<Role> GetEnumerator()
        {
            return _roles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ReadOnlyRoleCollection2 : IEnumerable<Role>
    {
        private readonly IList<Role> _roles;

        public ReadOnlyRoleCollection2(IList<Role> roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }
            _roles = roles;
        }

        public IEnumerator<Role> GetEnumerator()
        {
            return _roles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ReadOnlyRoleCollection3 : IEnumerable<Role>
    {
        private readonly List<Role> _roles;

        public ReadOnlyRoleCollection3(List<Role> roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }
            _roles = roles;
        }

        public IEnumerator<Role> GetEnumerator()
        {
            return _roles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ReadOnlyRoleCollection4 : IEnumerable<Role>
    {
        private readonly Role[] _roles;

        public ReadOnlyRoleCollection4(Role[] roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }
            _roles = roles;
        }

        public IEnumerator<Role> GetEnumerator()
        {
            return ((IEnumerable<Role>)_roles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
