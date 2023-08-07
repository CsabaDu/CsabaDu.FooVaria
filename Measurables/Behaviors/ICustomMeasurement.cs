namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICustomMeasurement : ICustomMeasureUnit
{
    ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);

    void InitiateCustomExchangeRates(MeasureUnitTypeCode measurementUnitTypeCode, params decimal[] exchangeRates);
    void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measurementUnitTypeCode);
}
