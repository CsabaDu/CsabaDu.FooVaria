namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.Returns
{
    public class BaseQuantifiableReturn : MeasurableReturn
    {
        public decimal GetDefaultQuantity { get; set; }
        public bool? FitsIn { get; set; }
    }
}
