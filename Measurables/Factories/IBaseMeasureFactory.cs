namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IBaseMeasureFactory : IMeasurableFactory, IRateComponentFactory<IBaseMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
    RateComponentCode RateComponentCode { get; }
    object DefaultRateComponentQuantity { get; }
}
