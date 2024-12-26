namespace CsabaDu.FooVaria.BaseTypes.Common.Types
{
    public interface ICommonBase : IRootObject
    {
        IFactory GetFactory();
    }

    public interface ICommonBase<TSelf> : ICommonBase
        where TSelf : class, ICommonBase
    {
        TSelf GetNew(TSelf other);
    }
}
