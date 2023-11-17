using CsabaDu.FooVaria.Measurements.Factories;

namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IBaseMeasureFactory : IMeasurableFactory, IFactory<IBaseMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
    RateComponentCode RateComponentCode { get; }
    int DefaultRateComponentQuantity { get; }
}
