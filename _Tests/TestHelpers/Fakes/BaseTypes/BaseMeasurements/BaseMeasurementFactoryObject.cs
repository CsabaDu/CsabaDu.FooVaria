namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasurements;

public sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    public IBaseMeasurement CreateBaseMeasurement(Enum context)
    {
        

        Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

        return new BaseMeasurementChild(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetFactoryValue = this,
            }
        };
    }
}
