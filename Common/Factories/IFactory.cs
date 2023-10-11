using CsabaDu.FooVaria.Common.Types;

namespace CsabaDu.FooVaria.Common.Factories
{
    public interface IFactory : IFooVariaObject
    {
    }

    public interface IFactory<T> : IFactory where T : class, ICommonBase
    {
        T Create(T other);
    }
}
