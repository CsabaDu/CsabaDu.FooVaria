namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface IRateComponent<out T> : IRateComponent where T : class, IMeasurable, IRateComponent
    {
        T? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
        RateComponentCode GetRateComponentCode();
        IMeasurement GetDefaultMeasurement();
    }

    public interface IDefaultRateComponent<out T> where T : class, IBaseMeasure
    {
        ValueType GetDefaultRateComponentQuantity();
        T GetDefaultRateComponent();
    }
}
