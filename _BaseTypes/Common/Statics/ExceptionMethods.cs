namespace CsabaDu.FooVaria.BaseTypes.Common.Statics;

public static class ExceptionMethods
{
    #region InvalidEnumArgumentException
    //public static InvalidEnumArgumentException InvalidMeasureUnitEnumArgumentException(Enum measureUnit)
    //{
    //    return InvalidMeasureUnitEnumArgumentException(measureUnit, nameof(measureUnit));
    //}

    //public static InvalidEnumArgumentException InvalidMeasureUnitEnumArgumentException(Enum measureUnit, string paramName)
    //{
    //    return new InvalidEnumArgumentException(paramName, (int)(object)measureUnit, measureUnit.GetType());
    //}

    //public static InvalidEnumArgumentException InvalidMeasureUnitCodeEnumArgumentException(MeasureUnitCode measureUnitCode)
    //{
    //    return InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, nameof(measureUnitCode));
    //}

    //public static InvalidEnumArgumentException InvalidMeasureUnitCodeEnumArgumentException(MeasureUnitCode measureUnitCode, string paramName)
    //{
    //    return new InvalidEnumArgumentException(paramName, (int)measureUnitCode, measureUnitCode.GetType());
    //}

    public static InvalidEnumArgumentException InvalidSideCodeEnumArgumentException(SideCode sideCode)
    {
        return InvalidSideCodeEnumArgumentException(sideCode, nameof(sideCode));
    }

    public static InvalidEnumArgumentException InvalidSideCodeEnumArgumentException(SideCode sideCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)sideCode, sideCode.GetType());
    }

    //public static InvalidEnumArgumentException InvalidQuantityTypeCodeEnumArgumentException(TypeCode quantityTypeCode)
    //{
    //    return InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, nameof(quantityTypeCode));
    //}

    //public static InvalidEnumArgumentException InvalidQuantityTypeCodeEnumArgumentException(TypeCode quantityTypeCode, string paramName)
    //{
    //    return new InvalidEnumArgumentException(paramName, (int)quantityTypeCode, quantityTypeCode.GetType());
    //}

    //public static InvalidEnumArgumentException InvalidRoundingModeEnumArgumentException(RoundingMode roundingMode)
    //{
    //    return InvalidRoundingModeEnumArgumentException(roundingMode, nameof(roundingMode));
    //}

    //public static InvalidEnumArgumentException InvalidRoundingModeEnumArgumentException(RoundingMode roundingMode, string paramName)
    //{
    //    return new InvalidEnumArgumentException(paramName, (int)roundingMode, roundingMode.GetType());
    //}

    //public static InvalidEnumArgumentException InvalidLimitModeEnumArgumentException(LimitMode limitMode)
    //{
    //    return InvalidLimitModeEnumArgumentException(limitMode, nameof(limitMode));
    //}

    //public static InvalidEnumArgumentException InvalidLimitModeEnumArgumentException(LimitMode limitMode, string paramName)
    //{
    //    return new InvalidEnumArgumentException(paramName, (int)limitMode, limitMode.GetType());
    //}

    public static InvalidEnumArgumentException InvalidComparisonCodeEnumArgumentException(ComparisonCode comparisonCode)
    {
        return InvalidComparisonCodeEnumArgumentException(comparisonCode, nameof(comparisonCode));
    }

    public static InvalidEnumArgumentException InvalidComparisonCodeEnumArgumentException(ComparisonCode comparisonCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)comparisonCode, comparisonCode.GetType());
    }

    //public static InvalidEnumArgumentException InvalidSidenCodeEnumArgumentException(SideCode sideCode)
    //{
    //    return InvalidSidenCodeEnumArgumentException(sideCode, nameof(sideCode));
    //}

    //public static InvalidEnumArgumentException InvalidSidenCodeEnumArgumentException(SideCode sideCode, string paramName)
    //{
    //    return new InvalidEnumArgumentException(paramName, (int)sideCode, sideCode.GetType());
    //}

    //public static InvalidEnumArgumentException InvalidRateComponentCodeArgumentException(RateComponentCode rateComponentCode)
    //{
    //    return InvalidRateComponentCodeArgumentException(rateComponentCode, nameof(rateComponentCode));
    //}

    //public static InvalidEnumArgumentException InvalidRateComponentCodeArgumentException(RateComponentCode rateComponentCode, string paramName)
    //{
    //    return new InvalidEnumArgumentException(paramName, (int)rateComponentCode, rateComponentCode.GetType());
    //}
    #endregion

    #region ArgumentOutOfRangeException
    public static ArgumentOutOfRangeException ArgumentTypeOutOfRangeException(string paramName, object arg)
    {
        return new ArgumentOutOfRangeException(paramName, arg.GetType().Name, null);
    }

    public static ArgumentOutOfRangeException DecimalArgumentOutOfRangeException(decimal exchangeRate)
    {
        return DecimalArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate);
    }

    public static ArgumentOutOfRangeException DecimalArgumentOutOfRangeException(string? paramName, decimal exchangeRate)
    {
        return new ArgumentOutOfRangeException(paramName, exchangeRate, null);
    }

    public static ArgumentOutOfRangeException NameArgumentOutOfRangeException(string name)
    {
        return new ArgumentOutOfRangeException(nameof(name), name, null);
    }

    public static ArgumentOutOfRangeException NameArgumentOutOfRangeException(string? paramName, string name)
    {
        return new ArgumentOutOfRangeException(paramName, name ?? string.Empty, null);
    }

    //public static ArgumentOutOfRangeException QuantityArgumentOutOfRangeException(ValueType? quantity)
    //{
    //    return QuantityArgumentOutOfRangeException(nameof(quantity), quantity);
    //}

    //public static ArgumentOutOfRangeException QuantityArgumentOutOfRangeException(string? paramName, ValueType? quantity)
    //{
    //    return new ArgumentOutOfRangeException(paramName, Type.GetTypeCode(quantity?.GetType()), null);

    //}

    //public static ArgumentOutOfRangeException MeasureUnitTypeArgumentOutOfRangeException(Type measureUnitType)
    //{
    //    return MeasureUnitTypeArgumentOutOfRangeException(measureUnitType, nameof(measureUnitType));
    //}

    //public static ArgumentOutOfRangeException MeasureUnitTypeArgumentOutOfRangeException(Type measureUnitType, string paramName)
    //{
    //    return new ArgumentOutOfRangeException(paramName, measureUnitType.FullName, null);
    //}

    public static ArgumentOutOfRangeException CountArgumentOutOfRangeException(int count)
    {
        return CountArgumentOutOfRangeException(count, nameof(count));
    }

    public static ArgumentOutOfRangeException CountArgumentOutOfRangeException(int count, string paramName)
    {
        return new ArgumentOutOfRangeException(paramName, count, null);
    }

    #endregion

    #region Generic checkers
    #region ArgumentNullException
    public static T NullChecked<T>(T? param, string? paramName)
    {
        if (param is null
            || param is IEnumerable enumerable
            && (enumerable.GetEnumerator() is null
            || enumerable.Cast<object>().All(x => x is null)))
        {
            throw new ArgumentNullException(paramName);
        }

        return param;
    }
    #endregion

    #region ArgumentOutOfRangeException
    public static T TypeChecked<T>(T? param, [DisallowNull] string paramName, [DisallowNull] Type type)
    {
        Type paramType = typeof(T);

        if (paramType == type) return NullChecked(param, paramName);

        throw ArgumentTypeOutOfRangeException(paramName, paramType);
    }

    public static T TypeChecked<T>(object? param, [DisallowNull] string paramName)
    {
        if (NullChecked(param, paramName) is T typeChecked) return typeChecked;

        throw ArgumentTypeOutOfRangeException(paramName, param!.GetType());
    }
    #endregion

    #region InvalidEnumArgumentException
    public static T Defined<T>(T param, string? paramName, Type enumType)
        where T : Enum
    {
        if (Enum.IsDefined(enumType, NullChecked(param, paramName))) return param;

        throw new InvalidEnumArgumentException(paramName, (int)(object)param, enumType);
    }

    public static T Defined<T>(T param, string? paramName)
        where T : struct, Enum
    {
        if (Enum.IsDefined(param)) return param;

        throw new InvalidEnumArgumentException(paramName, (int)(object)param, typeof(T));
    }
    #endregion
    #endregion
}
