namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.Fakes;

internal sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    private readonly DataFields Fields = new();

    public IBaseMeasurement CreateBaseMeasurement(Enum context)
    {
        Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

        return new BaseMeasurementChild(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
            }
        };
    }
}
