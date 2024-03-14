namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Returns
{
    public class BaseQuantifiableReturns : MeasurableReturns
    {
        public decimal GetDefaultQuantity { get; set; }
        public bool? FitsIn {  get; set; }
    }

}
