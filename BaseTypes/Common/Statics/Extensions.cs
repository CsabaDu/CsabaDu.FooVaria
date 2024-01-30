namespace CsabaDu.FooVaria.BaseTypes.Common.Statics;

public static class Extensions
{
    #region System.Decimal
    public static bool IsValidExchangeRate(this decimal exchangeRate)
    {
        return exchangeRate > 0;
    }

    //public static void ValidateExchangeRate(this decimal exchangeRate)
    //{
    //    if (exchangeRate.IsValidExchangeRate()) return;

    //    throw new ArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate, null);
    //}
    #endregion

    public static bool IsDefined<T>(this T enumeration)
        where T : struct, Enum
    {
        return Enum.IsDefined(enumeration);
    }
}