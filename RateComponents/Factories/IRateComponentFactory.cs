using CsabaDu.FooVaria.Measurements.Factories;

namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IRateComponentFactory : IMeasurableFactory, IFactory<IRateComponent>
{
    IMeasurementFactory MeasurementFactory { get; init; }
    RateComponentCode RateComponentCode { get; }
    int DefaultRateComponentQuantity { get; }
}
