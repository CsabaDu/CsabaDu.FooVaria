using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.SimpleRates.Factories;

public interface IProportionFactory : ISimpleRateFactory, IConcreteFactory
{
    IProportion Create(Enum numeratorContext, decimal quantity, Enum denominator);
    IProportion Create(IBaseRate baseRate);
}
