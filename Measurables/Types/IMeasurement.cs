namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IMeasurable, IExchangeRateCollection, IRateComponent<IMeasurement>, IValidMeasureUnit, ICustomName, ICustomMeasurement, IProportional<IMeasurement, Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    //IMeasurement GetMeasurement(Enum measureUnit, string? customName = null);
    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null, string? customName = null);
    IMeasurement GetMeasurement(IMeasurement? other = null);
    IMeasurement GetMeasurement(IBaseMeasure baseMeasure);
    IMeasurement GetMeasurement(string name);
    string GetName(Enum? measureUnit = null);
    IMeasurementFactory GetMeasurementFactory();
}

    //bool TryGetMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IMeasurement? measurement);