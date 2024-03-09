namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Factories;

internal sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    private readonly RandomParams randomParams = new();

    public IBaseMeasurement CreateBaseMeasurement(Enum context) => new BaseMeasurementChild(SampleParams.rootObject, string.Empty)
    {
        GetBaseMeasureUnit_returns = context,
        GetName_returns = randomParams.GetRandomParamName(),
    };
}
