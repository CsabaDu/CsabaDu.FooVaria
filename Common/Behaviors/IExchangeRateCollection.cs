namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IExchangeRateCollection : IConstantExchangeRates, IExchangeRate
{
    IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode);
    IDictionary<object, decimal> GetExchangeRateCollection();
    decimal GetExchangeRate(Enum measureUnit);
    decimal GetExchangeRate(string name);
    bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit);

    void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit);
}
