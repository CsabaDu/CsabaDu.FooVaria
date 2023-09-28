namespace CsabaDu.FooVaria.Common.Types
{
    public interface IFactory
    {
    }

    public interface IFactory<T> : IFactory where T : class, ICommonBase
    {
        T Create(T other);
    }
}
