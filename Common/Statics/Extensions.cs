using static CsabaDu.FooVaria.Common.Statics.QuantityTypes;

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
    public static ValueType? ToQuantity(this ValueType quantity, Type conversionType)
    {
        TypeCode conversionTypeCode = Type.GetTypeCode(conversionType);

        return quantity.ToQuantity(conversionTypeCode);
    }

    public static ValueType? ToQuantity(this ValueType quantity, TypeCode conversionTypeCode)
    {
        Type quantityType = quantity.GetType();

        if (!GetQuantityTypes().Contains(quantityType)) return null;

        TypeCode quantityTypeCode = Type.GetTypeCode(quantityType);
        int roundingDecimals = 8;

        if (quantityTypeCode == conversionTypeCode) return getRoundedQuantity(roundingDecimals);

        try
        {
            return conversionTypeCode switch
            {
                TypeCode.Int32 or
                TypeCode.Int64 => getIntQuantity(),

                TypeCode.UInt32 or
                TypeCode.UInt64 => getUIntQuantityOrNull(),

                TypeCode.Double or
                TypeCode.Decimal => getRoundedQuantity(roundingDecimals),

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
        ValueType? getIntQuantity()
        {
            return conversionTypeCode switch
            {
                TypeCode.Int32 => Convert.ToInt32(quantity),
                TypeCode.Int64 => Convert.ToInt64(quantity),

                _ => null,
            };
        }

        ValueType? getUIntQuantityOrNull()
        {
            if (Convert.ToDouble(quantity) < 0) return null;

            return conversionTypeCode switch
            {
                TypeCode.UInt32 => Convert.ToUInt32(quantity),
                TypeCode.UInt64 => Convert.ToUInt64(quantity),

                _ => null,
            };
        }

        ValueType getRoundedQuantity(int decimals)
        {
            return conversionTypeCode switch
            {
                TypeCode.Double => Math.Round(Convert.ToDouble(quantity), decimals),
                TypeCode.Decimal => decimal.Round(Convert.ToDecimal(quantity), decimals),

                _ => quantity,
            };
        }
        #endregion
    }
    #endregion

    #region CsabaDu.FooVaria.Common.Enums.MeasureUnitTypeCode
    public static TypeCode? GetQuantityTypeCode(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.AreaUnit or
            MeasureUnitTypeCode.DistanceUnit or
            MeasureUnitTypeCode.ExtentUnit or
            MeasureUnitTypeCode.TimePeriodUnit or
            MeasureUnitTypeCode.VolumeUnit or
            MeasureUnitTypeCode.WeightUnit => TypeCode.Double,

            MeasureUnitTypeCode.Currency => TypeCode.Decimal,

            MeasureUnitTypeCode.Pieces => TypeCode.Int64,

            _ => null,
        };
    }

    public static string GetName(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        string? name = Enum.GetName(measureUnitTypeCode);

        return name ?? throw new InvalidEnumArgumentException(nameof(measureUnitTypeCode), (int)measureUnitTypeCode, measureUnitTypeCode.GetType());
    }

    public static IEnumerable<string> GetMeasureUnitDefaultNames(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        Type measureUnitType = measureUnitTypeCode.GetMeasureUnitType();

        return Enum.GetNames(measureUnitType);
    }

    public static Type GetMeasureUnitType(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.AreaUnit => typeof(AreaUnit),
            MeasureUnitTypeCode.Currency => typeof(Currency),
            MeasureUnitTypeCode.DistanceUnit => typeof(DistanceUnit),
            MeasureUnitTypeCode.ExtentUnit => typeof(ExtentUnit),
            MeasureUnitTypeCode.TimePeriodUnit => typeof(TimePeriodUnit),
            MeasureUnitTypeCode.Pieces => typeof(Pieces),
            MeasureUnitTypeCode.VolumeUnit => typeof(VolumeUnit),
            MeasureUnitTypeCode.WeightUnit => typeof(WeightUnit),

            _ => throw new InvalidEnumArgumentException(nameof(measureUnitTypeCode), (int)measureUnitTypeCode, measureUnitTypeCode.GetType()),
        };
    }

    public static Enum GetMeasureUnitDefault(this MeasureUnitTypeCode measureUnitTypeCode)
    {
        Type measureUnitType = measureUnitTypeCode.GetMeasureUnitType();

        return (Enum)Enum.ToObject(measureUnitType, default(int));
    }
    #endregion
}
