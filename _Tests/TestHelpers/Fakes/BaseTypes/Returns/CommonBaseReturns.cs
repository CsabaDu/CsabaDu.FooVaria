namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Returns
{
    public class CommonBaseReturns
    {
        public IFactory GetFactory { get; set; }
    }

    public class MeasurableReturns : CommonBaseReturns
    {
        public Enum GetBaseMeasureUnit { get; set; }
    }

    public class BaseMeasurementReturns : MeasurableReturns
    {
        public string GetName { get; set; }
    }

    public class BaseQuantifiableReturns : MeasurableReturns
    {
        public decimal GetDefaultQuantity { get; set; }
        public bool? FitsIn {  get; set; }
    }

}
