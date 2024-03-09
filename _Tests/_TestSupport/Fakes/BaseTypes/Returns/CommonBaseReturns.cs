namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Returns
{
    internal class CommonBaseReturns
    {
        internal IFactory GetFactory { get; set; }
    }

    internal class MeasurableReturns : CommonBaseReturns
    {
        internal Enum GetBaseMeasureUnit { get; set; }
    }

    internal class BaseMeasurementReturns : MeasurableReturns
    {
        internal string GetName { get; set; }
    }

    internal class BaseQuantifiableReturns : MeasurableReturns
    {
        internal decimal GetDefaultQuantity { get; set; }
        internal bool? FitsIn {  get; set; }
    }

}
