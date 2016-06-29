using System;

namespace Wheatech.EmitMapper.UnitTests.DataModel
{
    public class RoleEntity
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public string LoweredRoleName { get; set; }
    }
}
