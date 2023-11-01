using System.ComponentModel;

namespace CsabaDu.FooVaria.Measurables.Statics;

public static class ExceptionMethods
{
    public static InvalidEnumArgumentException InvalidRateComponentCodeArgumentException(RateComponentCode rateComponentCode)
    {
        return InvalidRateComponentCodeArgumentException(rateComponentCode, nameof(rateComponentCode));
    }

    public static InvalidEnumArgumentException InvalidRateComponentCodeArgumentException(RateComponentCode rateComponentCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)rateComponentCode, rateComponentCode.GetType());
    }
}
