namespace CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

public interface IDeepCopyFactory<T>
    where T : class, ICommonBase
{
    T CreateNew(T other);
}
