namespace CsabaDu.FooVaria.BaseMeasures.Types
{
    public interface IDefaultBaseMeasure : IBaseMeasure
    {
    }

    public interface IDefaultBaseMeasure<TSelf, TNum> : IDefaultBaseMeasure, IDefaultMeasurable<TSelf>, IQuantity<TNum>/*, ICommonBase<TSelf>*/
        where TSelf : class, IBaseMeasure, IDefaultBaseMeasure
        where TNum : struct
    {
        TSelf GetBaseMeasure(TNum quantity);
    }
}
