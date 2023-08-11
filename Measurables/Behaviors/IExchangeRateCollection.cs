namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IExchangeRateCollection
{
    IDictionary<Enum, decimal> GetExchangeRateCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);
    IDictionary<Enum, decimal> GetConstantExchangeRateCollection();
    decimal GetExchangeRate(Enum measureUnit);

    void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null);
}
