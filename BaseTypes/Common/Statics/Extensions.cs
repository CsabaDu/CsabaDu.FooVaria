namespace CsabaDu.FooVaria.Common.Statics;

public static class Extensions
{
    #region System.Decimal
    public static bool IsValidExchangeRate(this decimal exchangeRate)
    {
        return exchangeRate > 0;
    }

    public static void ValidateExchangeRate(this decimal exchangeRate)
    {
        if (exchangeRate.IsValidExchangeRate()) return;

        throw new ArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate, null);
    }
    #endregion

    #region System.Int32
    public static bool? FitsIn(this int comparison, LimitMode? limitMode)
    {
        return limitMode switch
        {
            LimitMode.BeNotLess => comparison >= 0,
            LimitMode.BeNotGreater => comparison <= 0,
            LimitMode.BeGreater => comparison > 0,
            LimitMode.BeLess => comparison < 0,
            LimitMode.BeEqual => comparison == 0,

            _ => null,
        };
    }
    #endregion
}
