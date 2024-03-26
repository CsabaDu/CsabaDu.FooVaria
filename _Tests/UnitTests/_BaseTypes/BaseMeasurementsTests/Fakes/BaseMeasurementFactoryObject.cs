namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.Fakes;

internal sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    DataFields Fields = new();

    public IBaseMeasurement CreateBaseMeasurement(Enum context) => new BaseMeasurementChild(Fields.RootObject, Fields.paramName)
    {
        Return = new()
        {
            GetBaseMeasureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit,
        }
    };
}
