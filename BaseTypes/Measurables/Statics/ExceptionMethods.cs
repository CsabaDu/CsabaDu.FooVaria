namespace CsabaDu.FooVaria.BaseTypes.Measurables.Statics;

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

    public static InvalidEnumArgumentException InvalidMeasureUnitCodeEnumArgumentException(MeasureUnitCode measureUnitCode)
    {
        return InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, nameof(measureUnitCode));
    }

    public static InvalidEnumArgumentException InvalidMeasureUnitCodeEnumArgumentException(MeasureUnitCode measureUnitCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)measureUnitCode, measureUnitCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidQuantityTypeCodeEnumArgumentException(TypeCode quantityTypeCode)
    {
        return InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, nameof(quantityTypeCode));
    }

    public static InvalidEnumArgumentException InvalidQuantityTypeCodeEnumArgumentException(TypeCode quantityTypeCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)quantityTypeCode, quantityTypeCode.GetType());
    }
    #endregion

    #region ArgumentOutOfRangeException
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
    #region InvalidEnumArgumentException
    public static T DefinedMeasureUnit<T>(T? measureUnit, string paramName)
        where T : Enum
    {
        if (IsDefinedMeasureUnit(NullChecked(measureUnit, paramName))) return measureUnit!;

        throw new InvalidEnumArgumentException(paramName, (int)(object)measureUnit!, typeof(T));
    }
    #endregion
    #endregion
}
