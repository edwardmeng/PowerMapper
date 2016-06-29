using System;

namespace Wheatech.EmitMapper.UnitTests.DataModel
{
    public class OrderItemEntity
    {
        public Guid OrderItemId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
