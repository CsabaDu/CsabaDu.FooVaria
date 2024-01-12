namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IConstantExchangeRates
{
    IDictionary<object, decimal> GetConstantExchangeRateCollection();
    IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitCode measureUnitCode);

    void RestoreConstantExchangeRates();
}
