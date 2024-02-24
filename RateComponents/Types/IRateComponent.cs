namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IRateComponent : IBaseMeasure, IDefaultMeasurable
    {
        object GetDefaultRateComponentQuantity();
    }

    public interface IRateComponent<TSelf> : IRateComponent, IDefaultMeasurable<TSelf>
        where TSelf : class, IBaseMeasure
    {
        IMeasurement Measurement { get; init; }
    }
}
