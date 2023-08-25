namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IMeasurable, ICustomMeasurement, IMeasureUnitCollection, IExchangeRateCollection, ICustomNameCollection, IRateComponent<IMeasurement>, IProportional<IMeasurement, Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit);
    IMeasurement GetMeasurement(IMeasurement? other = null);
    IMeasurement GetMeasurement(IBaseMeasure baseMeasure);
    IMeasurement GetMeasurement(string name);
    string GetName(Enum? measureUnit = null);
    IMeasurementFactory GetMeasurementFactory();
}

    //IMeasurement GetMeasurement(Enum measureUnit, string? customName = null);
    //bool TryGetMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IMeasurement? measurement);