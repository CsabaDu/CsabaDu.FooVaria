namespace CsabaDu.FooVaria.Measurements.Behaviors;

public interface ICustomMExchangeRates
{
    void InitializeCustomExchangeRates(MeasureUnitTypeCode measureUnitTypeCode, IDictionary<string, decimal> customExchangeRateCollection);

}
