namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

public interface IExchangeRate
{
    decimal GetExchangeRate();

    void ValidateExchangeRate(decimal exchangeRate, [DisallowNull] string paramName);
}
