namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Factories;

internal sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    public IBaseMeasurement CreateBaseMeasurement(Enum context) => new BaseMeasurementChild(SampleParams.rootObject, null)
    {
        Returns = new()
        {
            GetBaseMeasureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit,
        }
    };
}
