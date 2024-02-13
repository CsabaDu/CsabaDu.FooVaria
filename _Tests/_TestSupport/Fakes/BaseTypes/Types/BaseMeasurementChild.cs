namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class BaseMeasurementChild(IBaseMeasurementFactory factory, Enum measureUnit) : BaseMeasurement(factory, measureUnit)
{
    public override decimal GetExchangeRate()
    {
        throw new NotImplementedException();
    }

    public override decimal GetExchangeRate(string name)
    {
        throw new NotImplementedException();
    }

    public override Enum GetMeasureUnit()
    {
        throw new NotImplementedException();
    }

    public override string GetName()
    {
        throw new NotImplementedException();
    }
}
