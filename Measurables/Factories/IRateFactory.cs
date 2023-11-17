namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IRateFactory : IMeasurableFactory, IBaseRateFactory, IFactory<IRate>
{
    IDenominatorFactory DenominatorFactory { get; init; }
}
