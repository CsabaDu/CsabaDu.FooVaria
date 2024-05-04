namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasurements;

public sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    public IBaseMeasurement CreateBaseMeasurement(Enum context)
    {
        DataFields fields = DataFields.Fields;

        Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

        return new BaseMeasurementChild(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetFactoryValue = this,
            }
        };
    }
}
