namespace CsabaDu.FooVaria.BaseTypes.Common.Statics;

public static class Extensions
{
    #region System.Decimal
    public static bool IsValidExchangeRate(this decimal exchangeRate)
    {
        return exchangeRate > 0;
    }
    #endregion

    #region TEnum : struct, Enum
    public static bool IsDefined<TEnum>(this TEnum enumeration)
        where TEnum : struct, Enum
    {
        return Enum.IsDefined(enumeration);
    }
    #endregion
}