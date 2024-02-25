namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IRateComponent : IBaseMeasure, IDefaultMeasurable
    {
        object GetDefaultRateComponentQuantity();
    }

    public interface IRateComponent<TSelf> : IRateComponent, IDefaultMeasurable<TSelf>
        where TSelf : class, IRateComponent
    {
        IMeasurement Measurement { get; init; }
    }

    public interface IRateComponent<TSelf, TNum> : IRateComponent<TSelf>, IBaseMeasure<TSelf, TNum>
        where TSelf : class, IRateComponent<TSelf>
        where TNum : struct
    {
    }
}
