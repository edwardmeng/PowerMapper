namespace PowerMapper.UnitTests.BusinessModel
{
    public class DerivedOrder : Order
    {
        public new string CustomerId { get; set; }

        public decimal Amount { get; set; }
    }
}
