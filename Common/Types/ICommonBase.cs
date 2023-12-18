namespace CsabaDu.FooVaria.Common.Types
{
    public interface ICommonBase : IRootObject
    {
        IFactory Factory { get; init; }

        IFactory GetFactory();

        void Validate(IRootObject? rootObject, string paramName);
    }

    public interface ICommonBase<TSelf> : ICommonBase
        where TSelf : class, ICommonBase
    {
        TSelf GetNew(TSelf other);
    }

}
