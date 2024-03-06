namespace CsabaDu.FooVaria.SimpleRates.Factories;

public interface IProportionFactory : ISimpleRateFactory
{
    IProportion Create(Enum numeratorContext, decimal quantity, Enum denominator);
    IProportion Create(IBaseRate baseRate);
}
