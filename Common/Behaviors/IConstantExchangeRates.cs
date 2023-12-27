namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IConstantExchangeRates
{
    IDictionary<object, decimal> GetConstantExchangeRateCollection();
    IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode);

    void RestoreConstantExchangeRates();
}
