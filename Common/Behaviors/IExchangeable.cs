namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IExchangeable<T, in U> where T : class where U : notnull
{
    bool IsExchangeableTo(U? context);
}