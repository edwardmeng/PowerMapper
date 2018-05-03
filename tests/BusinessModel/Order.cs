using System;
using System.Collections.Generic;

namespace PowerMapper.UnitTests.BusinessModel
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public string OrderCode { get; set; }

        public Guid CustomerId { get; set; }

        public string Address { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
