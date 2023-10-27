namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IExchangeRate<T> : IExchange<T, Enum> where T : class, IBaseMeasurable, IQuantifiable
{
    decimal GetExchangeRate();

    void ValidateExchangeRate(decimal exchangeRate);
}

