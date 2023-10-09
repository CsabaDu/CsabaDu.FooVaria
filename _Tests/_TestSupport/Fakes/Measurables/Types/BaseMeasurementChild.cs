namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Measurables.Types;

internal sealed class BaseMeasurementChild : BaseMeasurement
{
    public BaseMeasurementChild(IBaseMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    public override IMeasurable GetDefault()
    {
        throw new NotImplementedException();
    }

    public override IMeasurable GetMeasurable(IMeasurable other)
    {
        throw new NotImplementedException();
    }

    public override Enum GetMeasureUnit()
    {
        throw new NotImplementedException();
    }
}

