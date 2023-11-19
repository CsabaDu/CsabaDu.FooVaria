namespace CsabaDu.FooVaria.Common.Types
{
    public interface ICommonBase : IRootObject
    {
        IFactory Factory { get; init; }

        IFactory GetFactory();

        void Validate(IRootObject? rootObject, string paramName);
    }

    public interface ICommonBase<T> : ICommonBase where T : class, ICommonBase
    {
        T GetNew(T other);
    }

}
