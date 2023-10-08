namespace CsabaDu.FooVaria.Measurables.Types;

public interface ICustomMeasurement : ICustomMeasureUnitType, ICustomMeasureUnit
{
    ICustomMeasurement GetCustomMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    ICustomMeasurement? GetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName);
    bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);
    bool TrySetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName);
    void InitializeCustomExchangeRates(MeasureUnitTypeCode measureUnitTypeCode, IDictionary<string, decimal> customExchangeRateCollection);

}
