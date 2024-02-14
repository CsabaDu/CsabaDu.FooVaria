namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class BaseMeasurementChild(IBaseMeasurementFactory factory, Enum measureUnit) : BaseMeasurement(factory, measureUnit)
{
    public override Enum GetMeasureUnit()
    {
        throw new NotImplementedException();
    }

    public override string GetName()
    {
        throw new NotImplementedException();
    }
}
