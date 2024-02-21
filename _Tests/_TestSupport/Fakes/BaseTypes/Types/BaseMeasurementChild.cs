namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class BaseMeasurementChild(IBaseMeasurementFactory factory) : BaseMeasurement(factory)
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
