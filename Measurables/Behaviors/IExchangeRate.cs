namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IExchangeRate<T> : IExchange<T, Enum> where T : class, IMeasurable
{
    decimal GetExchangeRate();
}
