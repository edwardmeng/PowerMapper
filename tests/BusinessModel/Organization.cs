using System;

namespace PowerMapper.UnitTests
{
    public class Organization
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Organization[] Children { get; set; }
    }
}
