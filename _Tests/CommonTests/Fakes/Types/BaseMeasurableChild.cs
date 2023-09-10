namespace CsabaDu.FooVaria.Tests.CommonTests.Fakes.Types;

internal sealed class BaseMeasurableChild : BaseMeasurable
{
    public BaseMeasurableChild(MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
    {
    }

    public BaseMeasurableChild(Enum measureUnit) : base(measureUnit)
    {
    }

    public BaseMeasurableChild(IBaseMeasurable baseMeasurable) : base(baseMeasurable)
    {
    }

    public override Enum GetMeasureUnit()
    {
        return GetDefaultMeasureUnit();
    }
}
