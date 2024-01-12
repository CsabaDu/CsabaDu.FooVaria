namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IExchangeRate
{
    decimal GetExchangeRate();

    void ValidateExchangeRate(decimal exchangeRate, string paramName);
}

