namespace CsabaDu.FooVaria.Measurements.Types;

public interface IMeasurement : IBaseMeasurement, ICustomMeasurement/*, IRateComponent<IMeasurement>*/, IProportional<IMeasurement>, IExchangeable<Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit);
    IMeasurement GetMeasurement(IMeasurement other);
    IMeasurement GetMeasurement(IBaseMeasurable baseMeasurable);
    IMeasurement GetMeasurement(string name);
}
