namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IMeasurable, ICustomMeasurement, IMeasureUnitCollection, IExchangeRateCollection, ICustomNameCollection, IRateComponent<IMeasurement>, IProportional<IMeasurement, Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit);
    IMeasurement GetMeasurement(IMeasurement other);
    IMeasurement GetMeasurement(IBaseMeasure baseMeasure);
    IMeasurement GetMeasurement(string name);
    IMeasurementFactory GetMeasurementFactory();
    string GetName();
}
