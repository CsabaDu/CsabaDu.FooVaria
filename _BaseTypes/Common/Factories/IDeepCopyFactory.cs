namespace CsabaDu.FooVaria.BaseTypes.Common.Factories
{
    public interface IFactory : IRootObject;

    public interface IDeepCopyFactory<T>/* : IDeepCopyFactory*/
        where T : class, ICommonBase
    {
        T CreateNew(T other);
    }
}
