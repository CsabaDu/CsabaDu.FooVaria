using CsabaDu.FooVaria.Measurements.Factories;

namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IBaseMeasureFactory : IMeasurableFactory, IFactory<IRateComponent>
{
    IMeasurementFactory MeasurementFactory { get; init; }
    RateComponentCode RateComponentCode { get; }
    int DefaultRateComponentQuantity { get; }
}
