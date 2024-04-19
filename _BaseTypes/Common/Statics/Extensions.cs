namespace CsabaDu.FooVaria.BaseTypes.Common.Statics;

public static class Extensions
{
    #region System.Decimal
    public static bool IsValidExchangeRate(this decimal exchangeRate)
    {
        return exchangeRate > 0;
    }
    #endregion

    #region T : struct, Enum
    public static bool IsDefined<T>(this T enumeration)
        where T : struct, Enum
    {
        return Enum.IsDefined(enumeration);
    }
    #endregion
}