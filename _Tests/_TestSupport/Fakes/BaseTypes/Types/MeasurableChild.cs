namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class MeasurableChild : Measurable
{
    public MeasurableChild(IMeasurable other) : base(other)
    {
    }

    public MeasurableChild(IMeasurableFactory factory, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
    {
    }

    public MeasurableChild(IMeasurableFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    public MeasurableChild(IMeasurableFactory factory, IMeasurable measurable) : base(factory, measurable)
    {
    }

    public override Enum GetMeasureUnit()
    {
        throw new NotImplementedException();
    }
}
