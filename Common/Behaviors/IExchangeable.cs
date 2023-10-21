namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IExchangeable<in T> where T : notnull
{
    bool IsExchangeableTo(T? context);
}