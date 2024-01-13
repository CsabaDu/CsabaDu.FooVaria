using CsabaDu.FooVaria.Measurables.Enums;

namespace CsabaDu.FooVaria.Measurements.Behaviors;

public interface ICustomMExchangeRates
{
    void InitializeCustomExchangeRates(MeasureUnitCode measureUnitCode, IDictionary<string, decimal> customExchangeRateCollection);

}
