namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IDefaultRateComponent : IBaseMeasure
    {
    }

    public interface IDefaultRateComponent<T, U> : IDefaultRateComponent, IDefaultMeasurable<T>, IQuantity<U>, ICommonBase<T> where T : class, IRateComponent, IDefaultRateComponent where U : struct
    {
        U GetDefaultRateComponentQuantity();
        T GetDefaultRateComponent();
        T GetRateComponent(IRateComponent other);
    }
}
