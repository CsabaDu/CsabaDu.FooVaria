namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IRateFactory : IMeasurableFactory, IBaseRateFactory
{
    IDenominatorFactory DenominatorFactory { get; init; }
}
