namespace CsabaDu.FooVaria.RateComponents.Factories
{
    public interface IRateComponentFactory : IBaseMeasureFactory, IDefaultMeasurableFactory
    {
        IMeasurementFactory MeasurementFactory { get; }
        object DefaultRateComponentQuantity { get; }
        TypeCode QuantityTypeCode { get; }
    }

    public interface IRateComponentFactory<T> : IRateComponentFactory, IFactory<T>
        where T : class, IBaseMeasure
    {
    }
}
