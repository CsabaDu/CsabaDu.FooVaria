namespace CsabaDu.FooVaria.BaseMeasurements.Behaviors;

public interface IExchangeRateCollection : IConstantExchangeRates, IExchangeRate
{
    IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitCode measureUnitCode);
    IDictionary<object, decimal> GetExchangeRateCollection();
    decimal GetExchangeRate(string name);
    bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit);

    void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit);
}
