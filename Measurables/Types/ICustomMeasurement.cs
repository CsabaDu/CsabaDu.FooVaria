namespace CsabaDu.FooVaria.Measurables.Types;

public interface ICustomMeasurement : IBaseMeasurable, ICustomMeasureUnit
{
    ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null);
    bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);

    void InitiateCustomExchangeRates(MeasureUnitTypeCode measurementUnitTypeCode, params decimal[] exchangeRates);
}
