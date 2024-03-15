namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.Returns
{
    public class BaseQuantifiableReturns : MeasurableReturns
    {
        public decimal GetDefaultQuantity { get; set; }
        public bool? FitsIn { get; set; }
    }
}
