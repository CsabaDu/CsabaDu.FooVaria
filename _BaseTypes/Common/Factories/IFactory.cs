namespace CsabaDu.FooVaria.BaseTypes.Common.Factories
{
    public interface IFactory : IRootObject;

    public interface IFactory<T> : IFactory
        where T : class, ICommonBase
    {
        T CreateNew(T other);
    }
}
