namespace CsabaDu.FooVaria.BaseTypes.Common.Factories
{
    public interface IFactory : IRootObject;

    public interface IFactory<T>/* : ICreateNew*/
        where T : class, ICommonBase
    {
        T CreateNew(T other);
    }
}
