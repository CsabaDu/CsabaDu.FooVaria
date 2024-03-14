namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Factories;

public sealed class BaseMeasurementFactoryObject : IBaseMeasurementFactory
{
    public IBaseMeasurement CreateBaseMeasurement(Enum context) => new BaseMeasurementChild(new RootObject(), null)
    {
        Returns = new()
        {
            GetBaseMeasureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit,
        }
    };
}
