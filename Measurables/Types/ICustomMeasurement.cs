namespace CsabaDu.FooVaria.Measurables.Types;

public interface ICustomMeasurement : IBaseMeasurable, ICustomMeasureUnit
{
    ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName);
    bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);
    ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string? customName = null);

    void InitiateCustomExchangeRates(MeasureUnitTypeCode customMeasureUnitTypeCode, IDictionary<string, decimal> customExchangeRateCollection);
}
