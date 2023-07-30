namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IRateFactory : IMeasurableFactory
{
    IDenominatorFactory DenominatorFactory { get; init; }

    IRate Create(IRateFactory rateFactory, IRate rate);
    IRate Create(IRate rate);
}
