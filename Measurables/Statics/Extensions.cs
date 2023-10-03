using static CsabaDu.FooVaria.Measurables.Statics.QuantityTypes;

namespace CsabaDu.FooVaria.Common.Statics;

public static class Extensions
{
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

        ValueType getRoundedQuantity()
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
}
