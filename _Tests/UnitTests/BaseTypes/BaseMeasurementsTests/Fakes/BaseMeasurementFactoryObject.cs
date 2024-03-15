namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.Fakes;

internal sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    public IBaseMeasurement CreateBaseMeasurement(Enum context) => new BaseMeasurementChild(new RootObject(), null)
    {
        Return = new()
        {
            GetBaseMeasureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit,
        }
    };
}
