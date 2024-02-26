namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class MeasurableChild : Measurable
{
    #region TestHelpers
    internal Enum GetMeasureUnitReturns { private get; set; }
    #endregion

    internal MeasurableChild(IMeasurable other) : base(other)
    {
    }

    internal MeasurableChild(IMeasurableFactory factory) : base(factory)
    {
    }

    public override Enum GetMeasureUnit()
    {
        return GetMeasureUnitReturns;
    }
}
