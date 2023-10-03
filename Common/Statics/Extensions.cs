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

    #region CsabaDu.FooVaria.Common.Enums.MeasureUnitTypeCode
    public static TypeCode GetQuantityTypeCode(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.Currency => TypeCode.Decimal,

            MeasureUnitTypeCode.Pieces => TypeCode.Int64,

            MeasureUnitTypeCode.AreaUnit or
            MeasureUnitTypeCode.DistanceUnit or
            MeasureUnitTypeCode.ExtentUnit or
            MeasureUnitTypeCode.TimePeriodUnit or
            MeasureUnitTypeCode.VolumeUnit or
            MeasureUnitTypeCode.WeightUnit => TypeCode.Double,

            _ => throw new InvalidEnumArgumentException(nameof(measureUnitTypeCode), (int)(object)measureUnitTypeCode, typeof(MeasureUnitTypeCode)
            
            
            ),
        };
    }

    public static string GetName(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        return Enum.GetName(Defined(measureUnitTypeCode, nameof(measureUnitTypeCode)))!;
    }

    public static IEnumerable<string> GetMeasureUnitDefaultNames(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        Type measureUnitType = measureUnitTypeCode.GetMeasureUnitType();

        return Enum.GetNames(measureUnitType);
    }

    public static Type GetMeasureUnitType(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        return MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);
    }

    public static IEnumerable<Enum> GetAllMeasureUnits(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        Type measureUnitType = measureUnitTypeCode.GetMeasureUnitType();

        foreach (Enum item in Enum.GetValues(measureUnitType))
        {
            yield return item;
        }

    }

    public static Enum GetDefaultMeasureUnit(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode.GetAllMeasureUnits().First();
    }
    #endregion
}
