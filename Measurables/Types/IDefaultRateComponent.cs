namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IDefaultRateComponent : IBaseMeasure
    {
    }

    public interface IDefaultRateComponent<T, U> : IDefaultRateComponent, IMeasurable<T>, IQuantifiable<U>, ICommonBase<T> where T : class, IRateComponent, IDefaultRateComponent where U : struct
    {
        U GetDefaultRateComponentQuantity();
        T GetDefaultRateComponent();
        T GetRateComponent(IRateComponent other);
    }
}
