namespace CsabaDu.FooVaria.Measurements.Types;

public interface IMeasurement : IBaseMeasurement, ICustomMeasurement, IMeasurable<IMeasurement>, IProportional<IMeasurement>, IExchangeable<Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit);
    IMeasurement GetMeasurement(IMeasurement other);
    IMeasurement GetMeasurement(IMeasurable baseMeasurable);
    IMeasurement GetMeasurement(string name);
}
