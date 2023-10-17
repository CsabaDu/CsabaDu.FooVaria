using CsabaDu.FooVaria.Common.Behaviors;
using System.Linq;

namespace CsabaDu.FooVaria.Common.Statics;

public static class ExceptionMethods
{
    #region ArgumentNullException
    public static T NullChecked<T>(T? param, string? paramName)
    {
        return param ?? throw new ArgumentNullException(paramName);
    }
    #endregion

    #region InvalidEnumArgumentException
    public static T Defined<T>(T param, string? paramName, Type enumType) where T : Enum
    {
        if (Enum.IsDefined(enumType, param)) return param;

        throw new InvalidEnumArgumentException(paramName, (int)(object)param, enumType);
    }

    public static T Defined<T>(T param, string? paramName) where T : struct, Enum
    {
        if (Enum.IsDefined(param)) return param;

        throw new InvalidEnumArgumentException(paramName, (int)(object)param, typeof(T));
    }

    public static T DefinedMeasureUnit<T>(T measureUnit) where T : Enum
    {
        Type measureUnitType = measureUnit.GetType();

        if (MeasureUnitTypes.GetMeasureUnitTypes().Contains(measureUnit.GetType())
            && Enum.IsDefined(measureUnitType, measureUnit)) return measureUnit;

        throw new InvalidEnumArgumentException(nameof(measureUnit), (int)(object)measureUnit, typeof(T));
    }

    public static InvalidEnumArgumentException InvalidMeasureUnitEnumArgumentException(Enum measureUnit)
    {
        return new InvalidEnumArgumentException(nameof(measureUnit), (int)(object)measureUnit, measureUnit.GetType());
    }

    public static InvalidEnumArgumentException InvalidMeasureUnitTypeCodeEnumArgumentException(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return new InvalidEnumArgumentException(nameof(measureUnitTypeCode), (int)measureUnitTypeCode, measureUnitTypeCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidQuantityTypeCodeEnumArgumentException(TypeCode quantityTypeCode)
    {
        return new InvalidEnumArgumentException(nameof(quantityTypeCode), (int)quantityTypeCode, quantityTypeCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidRoundingModeEnumArgumentException(RoundingMode roundingMode)
    {
        return new InvalidEnumArgumentException(nameof(roundingMode), (int)roundingMode, roundingMode.GetType());
    }

    public static InvalidEnumArgumentException InvalidLimitModeEnumArgumentException(LimitMode limitMode)
    {
        return new InvalidEnumArgumentException(nameof(limitMode), (int)limitMode, limitMode.GetType());
    }
    #endregion

    #region ArgumentOutOfRangeException
    public static ArgumentOutOfRangeException ArgumentTypeOutOfRangeException(string name, object arg)
    {
        return new ArgumentOutOfRangeException(name, arg.GetType().Name, null);
    }
    public static ArgumentOutOfRangeException ExchangeRateArgumentOutOfRangeException(decimal? exchangeRate)
    {
        return new ArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate, null);
    }

    public static ArgumentOutOfRangeException NameArgumentOutOfRangeException(string? name)
    {
        return new ArgumentOutOfRangeException(nameof(name), name, null);
    }

    public static ArgumentOutOfRangeException CustomNameArgumentOutOfRangeException(string? customName)
    {
        return new ArgumentOutOfRangeException(nameof(customName), customName, null);
    }

    public static ArgumentOutOfRangeException QuantityArgumentOutOfRangeException(ValueType? quantity)
    {
        return new ArgumentOutOfRangeException(nameof(quantity), Type.GetTypeCode(quantity?.GetType()), null);
    }

    public static ArgumentOutOfRangeException MeasureUnitTypeArgumentOutOfRangeException(Type measureUnitType)
    {
        return new ArgumentOutOfRangeException(nameof(measureUnitType), measureUnitType.FullName, null);
    }
    #endregion
}
