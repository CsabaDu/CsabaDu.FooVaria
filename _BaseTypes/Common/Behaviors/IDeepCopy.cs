namespace CsabaDu.FooVaria.BaseTypes.Common.Behaviors
{
    public interface IDeepCopy<TSelf>
        where TSelf : class, ICommonBase
    {
        TSelf GetNew(TSelf other);

        TSelf GetNew();
    }
}
