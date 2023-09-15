namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

internal sealed class BaseMeasurableChild : BaseMeasurable
{
    public BaseMeasurableChild(MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
    {
    }

    public BaseMeasurableChild(Enum measureUnit) : base(measureUnit)
    {
    }

    public BaseMeasurableChild(IBaseMeasurable other) : base(other)
    {
    }

    public override Enum GetMeasureUnit()
    {
        throw new NotImplementedException();
    }
}
