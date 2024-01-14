namespace CsabaDu.FooVaria.Quantifiables.Types
{
    public interface IDefaultBaseMeasure : IBaseMeasure
    {
    }

    public interface IDefaultBaseMeasure<TSelf, TNum> : IDefaultBaseMeasure, IDefaultMeasurable<TSelf>, IQuantity<TNum>, ICommonBase<TSelf>
        where TSelf : class, IBaseMeasure, IDefaultBaseMeasure
        where TNum : struct
    {
        TSelf GetDefault();
        TSelf GetBaseMeasure(IBaseMeasure baseMeasure);
    }
}
