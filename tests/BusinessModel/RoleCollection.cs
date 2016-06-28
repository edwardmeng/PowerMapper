using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheatech.ObjectMapper.UnitTests.BusinessModel
{
    public class RoleCollection : ICollection<Role>
    {
        private readonly List<Role> _roles = new List<Role>();
        public IEnumerator<Role> GetEnumerator()
        {
            return _roles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Role item)
        {
            _roles.Add(item);
        }

        public void Clear()
        {
            _roles.Clear();
        }

        public bool Contains(Role item)
        {
            return _roles.Contains(item);
        }

        public void CopyTo(Role[] array, int arrayIndex)
        {
            _roles.CopyTo(array,arrayIndex);
        }

        public bool Remove(Role item)
        {
            return _roles.Remove(item);
        }

        public int Count
        {
            get
            {
                return _roles.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
    }
}
