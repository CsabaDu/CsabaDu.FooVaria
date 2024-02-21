namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class MeasurableChild : Measurable
{
    public MeasurableChild(IMeasurable other) : base(other)
    {
    }

    public MeasurableChild(IMeasurableFactory factory) : base(factory)
    {
    }

    public override Enum GetMeasureUnit()
    {
        throw new NotImplementedException();
    }
}
