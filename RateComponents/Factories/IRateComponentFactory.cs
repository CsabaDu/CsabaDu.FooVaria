namespace CsabaDu.FooVaria.RateComponents.Factories
{
    public interface IRateComponentFactory : IBaseMeasureFactory, IFactory<IRateComponent>
    {
        IMeasurementFactory MeasurementFactory { get; init; }
        RateComponentCode RateComponentCode { get; }
        int DefaultRateComponentQuantity { get; }
    }

    public interface IRateComponentFactory<T> : IRateComponentFactory
}
