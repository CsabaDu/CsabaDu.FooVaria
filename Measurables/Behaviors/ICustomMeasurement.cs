namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasurement
{
    ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate);
    bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);
    bool IsCustomMeasureUnit(Enum measureUnit);

    void InitiateCustomExchangeRates(MeasureUnitTypeCode measurementUnitTypeCode, params decimal[] exchangeRates);
    void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measurementUnitTypeCode);
    void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null);
}
