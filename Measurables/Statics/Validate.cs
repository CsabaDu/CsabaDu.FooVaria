using System.ComponentModel;

namespace CsabaDu.FooVaria.Measurables.Statics;

public static class Validate
{
    public static InvalidEnumArgumentException InvalidRateComponentCodeEnumArgumentException(RateComponentCode rateComponentCode)
    {
        return new InvalidEnumArgumentException(nameof(rateComponentCode), (int)rateComponentCode, rateComponentCode.GetType());
    }
}
