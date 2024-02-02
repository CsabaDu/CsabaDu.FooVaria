namespace CsabaDu.FooVaria.BaseTypes.BaseMeasures.Statics;

public static class ExceptionMethods
{
    #region InvalidEnumArgumentException
    public static InvalidEnumArgumentException InvalidRoundingModeEnumArgumentException(RoundingMode roundingMode)
    {
        return InvalidRoundingModeEnumArgumentException(roundingMode, nameof(roundingMode));
    }

    public static InvalidEnumArgumentException InvalidRoundingModeEnumArgumentException(RoundingMode roundingMode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)roundingMode, roundingMode.GetType());
    }
    #endregion
}
