namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class MeasurableChild : Measurable
{
    #region TestHelpers
    internal Enum GetMeasureUnitValue { private get; set; }
    #endregion

    public MeasurableChild(IMeasurable other) : base(other)
    {
    }

    public MeasurableChild(IMeasurableFactory factory) : base(factory)
    {
    }

    public override Enum GetMeasureUnit()
    {
        return GetMeasureUnitValue;
    }
}
