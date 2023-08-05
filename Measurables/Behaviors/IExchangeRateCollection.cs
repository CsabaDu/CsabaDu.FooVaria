namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IExchangeRateCollection
{
    IDictionary<Enum, decimal> GetExchangeRateCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);
    decimal GetExchangeRate(Enum measureUnit);
}
