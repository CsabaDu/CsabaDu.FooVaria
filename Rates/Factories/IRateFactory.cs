namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IRateFactory : IBaseRateFactory, IDefaultMeasurableFactory<IRate>, IFactory<IRate>
{
    IDenominatorFactory DenominatorFactory { get; init; }
}
