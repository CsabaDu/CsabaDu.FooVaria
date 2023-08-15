namespace CsabaDu.FooVaria.Common.Statics;

public static class Extensions
{
    private static HashSet<Type> QuantityTypes => new()
    {
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(double),
        typeof(decimal),
    };

    public static IEnumerable<TypeCode> GetQuantityTypeCodes()
    {
        foreach (Type item in QuantityTypes)
        {
            yield return Type.GetTypeCode(item);
        }
    }

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

        if (!QuantityTypes.Contains(quantityType)) return null;

        TypeCode quantityTypeCode = Type.GetTypeCode(quantityType);

        if (quantityTypeCode == conversionTypeCode) return getRoundedQuantity(8);

        try
        {
            return conversionTypeCode switch
            {
                TypeCode.Int32 or
                TypeCode.Int64 => getIntQuantity(),
                TypeCode.UInt32 or
                TypeCode.UInt64 => getUIntQuantityOrNull(),
                TypeCode.Double or
                TypeCode.Decimal => getRoundedQuantity(8),

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
    #endregion
}
