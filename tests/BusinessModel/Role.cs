using System;

namespace Wheatech.EmitMapper.UnitTests.BusinessModel
{
    public class Role
    {
        protected bool Equals(Role other)
        {
            return RoleId.Equals(other.RoleId) && string.Equals(RoleName, other.RoleName) && string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Role) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = RoleId.GetHashCode();
                hashCode = (hashCode*397) ^ (RoleName?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Description?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }
    }
}
