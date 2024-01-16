namespace CsabaDu.FooVaria.Quantifiables.Statics;

public static class Extensions
{
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

    //#region CsabaDu.FooVaria.Common.Enums.MeasureUnitCode
    //public static TypeCode GetQuantityTypeCode(this MeasureUnitCode measureUnitCode)
    //{
    //    return measureUnitCode switch
    //    {
    //        MeasureUnitCode.Currency => TypeCode.Decimal,

    //        MeasureUnitCode.Pieces => TypeCode.Int64,

    //        MeasureUnitCode.AreaUnit or
    //        MeasureUnitCode.DistanceUnit or
    //        MeasureUnitCode.ExtentUnit or
    //        MeasureUnitCode.TimePeriodUnit or
    //        MeasureUnitCode.VolumeUnit or
    //        MeasureUnitCode.WeightUnit => TypeCode.Double,

    //        _ => throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode),
    //    };
    //}

    //public static string GetName(this MeasureUnitCode measureUnitCode)
    //{
    //    return Enum.GetName(Defined(measureUnitCode, nameof(measureUnitCode)))!;
    //}

    //public static IEnumerable<string> GetMeasureUnitDefaultNames(this MeasureUnitCode measureUnitCode)
    //{
    //    Type measureUnitType = measureUnitCode.GetMeasureUnitType();

    //    return Enum.GetNames(measureUnitType);
    //}

    //public static Type GetMeasureUnitType(this MeasureUnitCode measureUnitCode)
    //{
    //    return MeasureUnitTypes.GetMeasureUnitType(measureUnitCode);
    //}

    //public static IEnumerable<Enum> GetAllMeasureUnits(this MeasureUnitCode measureUnitCode)
    //{
    //    Type measureUnitType = measureUnitCode.GetMeasureUnitType();

    //    foreach (Enum item in Enum.GetValues(measureUnitType))
    //    {
    //        yield return item;
    //    }
    //}

    //public static Enum GetDefaultMeasureUnit(this MeasureUnitCode measureUnitCode)
    //{
    //    return measureUnitCode.GetAllMeasureUnits().First();
    //}

    //public static bool IsCustomMeasureUnitCode(this MeasureUnitCode measureUnitCode)
    //{
    //    if (!Enum.IsDefined(measureUnitCode)) return false;

    //    Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    Type measureUnitType = measureUnit.GetType();
    //    string name = Enum.GetName(measureUnitType, measureUnit)!;

    //    return name == DefaultCustomMeasureUnitDefaultName;
    //}
    //#endregion
}
