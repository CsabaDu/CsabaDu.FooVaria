namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.BaseMeasurements;

public sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    public IBaseMeasurement CreateBaseMeasurement(Enum context)
    {
        

        Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

        return new BaseMeasurementChild(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = new()
            {
                GetBaseMeasureUnitReturnValue = measureUnit,
                GetFactoryReturnValue = this,
            }
        };
    }
}
