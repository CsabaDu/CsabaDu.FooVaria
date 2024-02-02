﻿namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Statics;

public static class ExceptionMethods
{
    #region InvalidEnumArgumentException
    public static InvalidEnumArgumentException InvalidLimitModeEnumArgumentException(LimitMode limitMode)
    {
        return InvalidLimitModeEnumArgumentException(limitMode, nameof(limitMode));
    }

    public static InvalidEnumArgumentException InvalidLimitModeEnumArgumentException(LimitMode limitMode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)limitMode, limitMode.GetType());
    }

    public static InvalidEnumArgumentException InvalidRateComponentCodeArgumentException(RateComponentCode rateComponentCode)
    {
        return InvalidRateComponentCodeArgumentException(rateComponentCode, nameof(rateComponentCode));
    }

    public static InvalidEnumArgumentException InvalidRateComponentCodeArgumentException(RateComponentCode rateComponentCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)rateComponentCode, rateComponentCode.GetType());
    }
    #endregion

    #region ArgumentOutOfRangeException
    public static ArgumentOutOfRangeException QuantityArgumentOutOfRangeException(ValueType? quantity)
    {
        return QuantityArgumentOutOfRangeException(nameof(quantity), quantity);
    }

    public static ArgumentOutOfRangeException QuantityArgumentOutOfRangeException(string? paramName, ValueType? quantity)
    {
        return new ArgumentOutOfRangeException(paramName, Type.GetTypeCode(quantity?.GetType()), null);

    }
    #endregion
}
