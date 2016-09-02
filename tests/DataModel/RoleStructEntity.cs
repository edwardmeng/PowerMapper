using System;

namespace PowerMapper.UnitTests.DataModel
{
    public struct RoleStructEntity
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public string LoweredRoleName { get; set; }
    }
}
