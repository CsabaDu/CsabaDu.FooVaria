
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

    #region System.ValueType
    public static object? ToQuantity(this ValueType quantity, Type conversionType)
    {
        TypeCode conversionTypeCode = Type.GetTypeCode(conversionType);

        return quantity.ToQuantity(conversionTypeCode);
    }

    public static object? ToQuantity(this ValueType quantity, TypeCode conversionTypeCode)
    {
        Type quantityType = quantity.GetType();

        if (!GetQuantityTypes().Contains(quantityType)) return null;

        TypeCode quantityTypeCode = Type.GetTypeCode(quantityType);

        if (quantityTypeCode == conversionTypeCode) return getRoundedQuantity();

        try
        {
            return conversionTypeCode switch
            {
                TypeCode.Int32 or
                TypeCode.Int64 => getIntQuantity(),

                TypeCode.UInt32 or
                TypeCode.UInt64 => getUIntQuantityOrNull(),

                TypeCode.Double or
                TypeCode.Decimal => getRoundedQuantity(),

                _ => null,
            };
        }
        catch (OverflowException)
        {
            return null;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex.InnerException);
        }

        #region Local methods
        object? getIntQuantity()
        {
            return conversionTypeCode switch
            {
                TypeCode.Int32 => Convert.ToInt32(quantity),
                TypeCode.Int64 => Convert.ToInt64(quantity),

                _ => null,
            };
        }

        object? getUIntQuantityOrNull()
        {
            if (Convert.ToDouble(quantity) < 0) return null;

            return conversionTypeCode switch
            {
                TypeCode.UInt32 => Convert.ToUInt32(quantity),
                TypeCode.UInt64 => Convert.ToUInt64(quantity),

                _ => null,
            };
        }

        object getRoundedQuantity()
        {
            return conversionTypeCode switch
            {
                TypeCode.Double => RoundQuantity(Convert.ToDouble(quantity)),
                TypeCode.Decimal => RoundQuantity(Convert.ToDecimal(quantity)),

                _ => quantity,
            };
        }
        #endregion
    }

    public static bool IsValidTypeQuantity(this ValueType quantity)
    {
        Type quantityType = quantity.GetType();

        return GetQuantityTypes().Contains(quantityType);
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

            _ => throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode),
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

    public static bool IsCustomMeasureUnitTypeCode(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (!Enum.IsDefined(measureUnitTypeCode)) return false;

        Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();
        Type measureUnitType = measureUnit.GetType();
        string name = Enum.GetName(measureUnitType, measureUnit)!;

        return name == DefaultCustomMeasureUnitDefaultName;
    }
    #endregion
}
