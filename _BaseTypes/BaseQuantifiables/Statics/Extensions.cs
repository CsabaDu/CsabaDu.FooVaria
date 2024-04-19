namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Statics;

public static class Extensions
{
    #region Constants
    private const int SinglePrecisionDecimals = 4;
    private const int DoublePrecisionDecimals = 8;
    #endregion

    #region System.Decimal
    public static bool? FitsIn(this decimal quantity, decimal other, LimitMode? limitMode)
    {
        return limitMode switch
        {
            LimitMode.BeNotLess => quantity >= other,
            LimitMode.BeNotGreater => quantity <= other,
            LimitMode.BeGreater => quantity > other,
            LimitMode.BeLess => quantity < other,
            LimitMode.BeEqual => quantity == other,

            _ => null,
        };
    }

    public static decimal Round(this decimal quantity, RoundingMode roundingMode = default)
    {
        return roundingMode switch
        {
            //RoundingMode.General => decimal.Round(quantity),
            RoundingMode.Ceiling => decimal.Ceiling(quantity),
            RoundingMode.Floor => decimal.Floor(quantity),
            RoundingMode.Half => getHalfQuantity(),
            RoundingMode.SinglePrecision => decimal.Round(quantity, SinglePrecisionDecimals),
            RoundingMode.DoublePrecision => decimal.Round(quantity, DoublePrecisionDecimals),

            _ => throw InvalidRoundingModeEnumArgumentException(roundingMode),
        };

        #region Local methods
        decimal getHalfQuantity()
        {
            decimal rounded = decimal.Floor(quantity);

            if (quantity == rounded) return rounded;

            const decimal half = 0.5m;
            rounded += half;

            if (quantity <= rounded) return rounded;

            return rounded + half;
        }
        #endregion
    }
    #endregion

    #region System.Double
    public static double Round(this double quantity, RoundingMode roundingMode = default)
    {
        return roundingMode switch
        {
            //RoundingMode.General => Math.Round(quantity),
            RoundingMode.Ceiling => Math.Ceiling(quantity),
            RoundingMode.Floor => Math.Floor(quantity),
            RoundingMode.Half => getHalfQuantity(),
            RoundingMode.SinglePrecision => Math.Round(quantity, SinglePrecisionDecimals),
            RoundingMode.DoublePrecision => Math.Round(quantity, DoublePrecisionDecimals),

            _ => throw InvalidRoundingModeEnumArgumentException(roundingMode),
        };

        #region Local methods
        double getHalfQuantity()
        {
            double rounded = Math.Floor(quantity);

            if (quantity == rounded) return rounded;

            const double half = 0.5;
            rounded += half;

            if (quantity <= rounded) return rounded;

            return rounded + half;
        }
        #endregion
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

    public static int? CompareToComparison(this int comparison, int other)
    {
        if (other == 0) return comparison;

        if (comparison == 0)
        {
            comparison = other;
        }

        if (comparison > 0 == other > 0) return comparison;

        return null;
    }
    #endregion

    #region System.ValueType
    public static TypeCode? GetQuantityTypeCode(this ValueType quantity)
    {
        return quantity.IsValidTypeQuantity() ?
            Type.GetTypeCode(quantity.GetType())
            : null;
    }

    public static object? ToQuantity(this ValueType quantity, Type conversionType)
    {
        TypeCode conversionTypeCode = Type.GetTypeCode(conversionType);

        return quantity.ToQuantity(conversionTypeCode);
    }

    public static object? ToQuantity(this ValueType quantity, TypeCode conversionTypeCode)
    {
        TypeCode? quantityTypeCode = quantity.GetQuantityTypeCode();

        if (quantityTypeCode is null) return null;

        if (quantityTypeCode == conversionTypeCode) return getRoundedQuantity();

        try
        {
            return conversionTypeCode switch
            {
                //TypeCode.Int32 or /*=> Convert.ToInt32(quantity),*/
                TypeCode.Int64 => Convert.ToInt64(quantity),
                //TypeCode.UInt32 or
                TypeCode.UInt64 => getRoundedUIntQuantityOrNull(),
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
            throw new InvalidOperationException(ex.Message, ex);
        }

        #region Local methods
        object? getRoundedUIntQuantityOrNull()
        {
            double converted = Convert.ToDouble(quantity);

            if (converted < 0 || converted >= ulong.MaxValue) return null;

            return Convert.ToUInt64(quantity);
        }

        object getRoundedQuantity()
        {
            return conversionTypeCode switch
            {
                TypeCode.Double => Convert.ToDouble(quantity).Round(RoundingMode.DoublePrecision),
                TypeCode.Decimal => Convert.ToDecimal(quantity).Round(RoundingMode.DoublePrecision),

                _ => quantity,
            };
        }
        #endregion
    }

    public static object? Round(this ValueType quantity, RoundingMode roundingMode = default)
    {
        return quantity switch
        {
            //int or
            //uint or
            long or
            ulong => quantity,
            double doubleQuyantity => doubleQuyantity.Round(roundingMode),
            decimal decimalQuyantity => decimalQuyantity.Round(roundingMode),

            _ => null,
        };
    }

    public static bool IsValidTypeQuantity(this ValueType quantity)
    {
        return QuantityTypeSet.Contains(quantity.GetType());
    }
    #endregion
}
