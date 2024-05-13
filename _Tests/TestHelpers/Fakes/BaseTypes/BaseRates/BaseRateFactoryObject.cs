namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseRates;

public sealed class BaseRateFactoryObject : IBaseRateFactory
{
    public IBaseRate CreateBaseRate(IQuantifiable numerator, Enum denominator)
    {
        return BaseRateChild.GetBaseRateChild(NullChecked(numerator, nameof(numerator)), GetMeasureUnitCode(denominator));
    }

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IMeasurable denominator)
    {
        return BaseRateChild.GetBaseRateChild(NullChecked(numerator, nameof(numerator)), NullChecked(denominator, nameof(denominator)).GetMeasureUnitCode());
    }

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    {
        return BaseRateChild.GetBaseRateChild(NullChecked(numerator, nameof(numerator)), NullChecked(denominator, nameof(denominator)).GetMeasureUnitCode());
    }
}
