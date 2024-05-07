namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseRates;

internal sealed class BaseRateFactoryObject : IBaseRateFactory
{
    public IBaseRate CreateBaseRate(IQuantifiable numerator, Enum denominator)
    {
        throw new NotImplementedException();
    }

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IMeasurable denominator)
    {
        throw new NotImplementedException();
    }

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    {
        throw new NotImplementedException();
    }
}
