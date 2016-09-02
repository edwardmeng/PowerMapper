namespace PowerMapper.UnitTests.DataModel
{
    public class DerivedOrderEntity : OrderEntity
    {
        public new string CustomerId { get; set; }

        public decimal Amount { get; set; }
    }
}
