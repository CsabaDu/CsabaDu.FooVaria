namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Returns
{
    public class BaseQuantifiableReturns : MeasurableReturns
    {
        public decimal GetDefaultQuantity {internal get; set; }
        public bool? FitsIn { internal get; set; }
    }
}
