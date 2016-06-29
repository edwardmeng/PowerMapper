using System;
using System.Collections.Generic;

namespace Wheatech.EmitMapper.UnitTests.BusinessModel
{
    public class Order
    {
        private IList<OrderItem> _items;
        public Guid OrderId { get; set; }

        public string OrderCode { get; set; }

        public Guid CustomerId { get; set; }

        public string Address { get; set; }

        public IList<OrderItem> Items => _items ?? (_items = new List<OrderItem>());
    }
}
