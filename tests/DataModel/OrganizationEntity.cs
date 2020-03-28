using System;

namespace PowerMapper.UnitTests
{
    public class OrganizationEntity
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public OrganizationEntity[] Children { get; set; }
    }
}
