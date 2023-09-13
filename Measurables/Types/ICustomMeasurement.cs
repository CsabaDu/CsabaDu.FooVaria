namespace CsabaDu.FooVaria.Measurables.Types;

public interface ICustomMeasurement : IBaseMeasurable, ICustomMeasureUnit
{
    ICustomMeasurement GetCustomMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName);
    bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);

    void InitializeCustomExchangeRates(MeasureUnitTypeCode measureUnitTypeCode, IDictionary<string, decimal> customExchangeRateCollection);
}
