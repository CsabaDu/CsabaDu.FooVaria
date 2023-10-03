namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Measurables.Types;

internal sealed class MeasurableChild : Measurable
{
    public MeasurableChild(IMeasurable other) : base(other)
    {
    }

    public MeasurableChild(IMeasurableFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
    }

    public MeasurableChild(IMeasurableFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    public MeasurableChild(IMeasurableFactory factory, IMeasurable measurable) : base(factory, measurable)
    {
    }

    public override IMeasurable GetDefault()
    {
        throw new NotImplementedException();
    }

    public override Enum GetMeasureUnit()
    {
        throw new NotImplementedException();
    }
}
