namespace CsabaDu.FooVaria.BaseTypes.Common.Types
{
    public interface ICommonBase : IRootObject
    {
        IFactory Factory { get; init; }

        IFactory GetFactory();
    }

    public interface ICommonBase<TSelf> : ICommonBase
        where TSelf : class, ICommonBase
    {
        TSelf GetNew(TSelf other);
    }
}
