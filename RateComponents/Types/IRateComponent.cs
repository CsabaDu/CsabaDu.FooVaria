namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IRateComponent : IBaseMeasure, IDefaultMeasurable
    {
        IMeasurement Measurement { get; init; }

        object GetDefaultRateComponentQuantity();
    }

    public interface IRateComponent<TSelf> : IRateComponent, IDefaultMeasurable<TSelf>
        where TSelf : class, IBaseMeasure
    {
    }
}
