namespace CsabaDu.FooVaria.Measurements.Behaviors;

public interface IExchangeRateCollection
{
    IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode);
    IDictionary<object, decimal> GetExchangeRateCollection();
    IDictionary<object, decimal> GetConstantExchangeRateCollection();
    IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode);
    decimal GetExchangeRate(Enum measureUnit);
    decimal GetExchangeRate(string name);
    bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit);

    void RestoreConstantExchangeRates();

    void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit);
}
