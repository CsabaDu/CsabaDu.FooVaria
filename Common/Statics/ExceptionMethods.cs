using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Common.Statics;

public static class ExceptionMethods
{
    #region InvalidEnumArgumentException
    public static InvalidEnumArgumentException InvalidMeasureUnitEnumArgumentException(Enum measureUnit)
    {
        return InvalidMeasureUnitEnumArgumentException(measureUnit, nameof(measureUnit));
    }

    public static InvalidEnumArgumentException InvalidMeasureUnitEnumArgumentException(Enum measureUnit, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)(object)measureUnit, measureUnit.GetType());
    }

    public static InvalidEnumArgumentException InvalidMeasureUnitTypeCodeEnumArgumentException(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, nameof(measureUnitTypeCode));
    }

    public static InvalidEnumArgumentException InvalidMeasureUnitTypeCodeEnumArgumentException(MeasureUnitTypeCode measureUnitTypeCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)measureUnitTypeCode, measureUnitTypeCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidSideCodeEnumArgumentException(SideCode sideCode)
    {
        return InvalidSideCodeEnumArgumentException(sideCode, nameof(sideCode));
    }

    public static InvalidEnumArgumentException InvalidSideCodeEnumArgumentException(SideCode sideCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)sideCode, sideCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidQuantityTypeCodeEnumArgumentException(TypeCode quantityTypeCode)
    {
        return InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, nameof(quantityTypeCode));
    }

    public static InvalidEnumArgumentException InvalidQuantityTypeCodeEnumArgumentException(TypeCode quantityTypeCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)quantityTypeCode, quantityTypeCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidRoundingModeEnumArgumentException(RoundingMode roundingMode)
    {
        return InvalidRoundingModeEnumArgumentException(roundingMode, nameof(roundingMode));
    }

    public static InvalidEnumArgumentException InvalidRoundingModeEnumArgumentException(RoundingMode roundingMode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)roundingMode, roundingMode.GetType());
    }

    public static InvalidEnumArgumentException InvalidLimitModeEnumArgumentException(LimitMode limitMode)
    {
        return InvalidLimitModeEnumArgumentException(limitMode, nameof(limitMode));
    }

    public static InvalidEnumArgumentException InvalidLimitModeEnumArgumentException(LimitMode limitMode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)limitMode, limitMode.GetType());
    }

    public static InvalidEnumArgumentException InvalidComparisonCodeEnumArgumentException(ComparisonCode comparisonCode)
    {
        return InvalidComparisonCodeEnumArgumentException(comparisonCode, nameof(comparisonCode));
    }

    public static InvalidEnumArgumentException InvalidComparisonCodeEnumArgumentException(ComparisonCode comparisonCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)comparisonCode, comparisonCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidSidenCodeEnumArgumentException(SideCode sideCode)
    {
        return InvalidSidenCodeEnumArgumentException(sideCode, nameof(sideCode));
    }

    public static InvalidEnumArgumentException InvalidSidenCodeEnumArgumentException(SideCode sideCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)sideCode, sideCode.GetType());
    }
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

    public static ArgumentOutOfRangeException QuantityArgumentOutOfRangeException(ValueType? quantity)
    {
        return QuantityArgumentOutOfRangeException(nameof(quantity), quantity);
    }

    public static ArgumentOutOfRangeException QuantityArgumentOutOfRangeException(string? paramName, ValueType? quantity)
    {
        return new ArgumentOutOfRangeException(paramName, Type.GetTypeCode(quantity?.GetType()), null);

    }

    public static ArgumentOutOfRangeException MeasureUnitTypeArgumentOutOfRangeException(Type measureUnitType)
    {
        return MeasureUnitTypeArgumentOutOfRangeException(measureUnitType, nameof(measureUnitType));
    }

    public static ArgumentOutOfRangeException MeasureUnitTypeArgumentOutOfRangeException(Type measureUnitType, string paramName)
    {
        return new ArgumentOutOfRangeException(paramName, measureUnitType.FullName, null);
    }
    #endregion

    #region Generic checkers
    #region ArgumentNullException
    public static T NullChecked<T>(T? param, string? paramName)
    {
        return param ?? throw new ArgumentNullException(paramName);
    }
    #endregion

    #region ArgumentOutOfRangeException
    public static T TypeChecked<T>(T param, string paramName, [DisallowNull] Type type)
    {
        Type paramType = NullChecked(param, paramName)!.GetType();

        if (paramType == type) return param;

        throw new ArgumentOutOfRangeException(paramName, paramType, null);
    }
    #endregion

    #region InvalidEnumArgumentException
    public static T Defined<T>(T param, string? paramName, Type enumType) where T : Enum
    {
        if (Enum.IsDefined(enumType, NullChecked(param, paramName))) return param;

        throw new InvalidEnumArgumentException(paramName, (int)(object)param, enumType);
    }

    public static T Defined<T>(T param, string? paramName) where T : struct, Enum
    {
        if (Enum.IsDefined(param)) return param;

        throw new InvalidEnumArgumentException(paramName, (int)(object)param, typeof(T));
    }

    public static T DefinedMeasureUnit<T>(T measureUnit, string paramName) where T : Enum
    {
        if (MeasureUnitTypes.IsDefinedMeasureUnit(NullChecked(measureUnit, paramName))) return measureUnit;

        throw new InvalidEnumArgumentException(paramName, (int)(object)measureUnit, typeof(T));
    }
    #endregion
    #endregion
}
