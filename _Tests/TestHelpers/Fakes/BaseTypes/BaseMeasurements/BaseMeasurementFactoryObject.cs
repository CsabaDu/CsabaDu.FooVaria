namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasurements;

public sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    public IBaseMeasurement CreateBaseMeasurement(Enum context)
    {
        DataFields fields = new();
        Enum measureUnit = BaseMeasure.GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

        return new BaseMeasurementChild(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetFactory = this,
            }
        };
    }
}
