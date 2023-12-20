namespace CsabaDu.FooVaria.RateComponents.Types
{
    public interface IDefaultRateComponent : IBaseMeasure
    {
    }

    public interface IDefaultRateComponent<TSelf, TNum> : IDefaultRateComponent, IDefaultMeasurable<TSelf>, IQuantity<TNum>, ICommonBase<TSelf>
        where TSelf : class, IRateComponent, IDefaultRateComponent
        where TNum : struct
    {
        TNum GetDefaultRateComponentQuantity();
        TSelf GetDefault();
        TSelf GetRateComponent(IRateComponent rateComponent);
    }
}
