using CsabaDu.FooVaria.Measurables.Factories.Implementations;

namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Measurables.Factories;

internal sealed class RateFactoryChild : RateFactory
{
    public RateFactoryChild(IDenominatorFactory denominatorFactory) : base(denominatorFactory)
    {
    }

    public override IMeasurable Create(IMeasurable other)
    {
        throw new NotImplementedException();
    }
}

