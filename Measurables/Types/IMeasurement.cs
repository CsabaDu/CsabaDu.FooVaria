namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IMeasurable, IExchangeRateCollection, IRateComponent, IValidMeasureUnit, ICustomName, ICustomMeasurement, IProportional<IMeasurement, Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    Enum? GetMeasureUnit(string name);
    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null);
    IMeasurement GetMeasurement(IMeasurement? other = null);
    IMeasurement GetMeasurement(IBaseMeasure baseMeasure);
    IMeasurement? GetMeasurement(string name);
    string GetName(Enum? measureUnit = null);
    bool TryGetMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IMeasurement? measurement);


    void RestoreConstantMeasureUnits();
}
