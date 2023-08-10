namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IMeasurable, IExchangeRateCollection, IRateComponent, ICustomName, ICustomMeasurement, IProportional<IMeasurement, Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    Enum? GetMeasureUnit(string name);
    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null, string? customName = null);
    IMeasurement GetMeasurement(IMeasurement? other = null);
    IMeasurement GetMeasurement(IBaseMeasure baseMeasure);
    IMeasurement? GetMeasurement(string name);
    string GetName(Enum? measureUnit = null);

    bool IsValidMeasureUnit(Enum measureUnit);
    bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit);
    bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit);

    void RestoreConstantMeasureUnits();
}
