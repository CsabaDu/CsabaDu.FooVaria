namespace CsabaDu.FooVaria.Rates.Factories;

public interface IRateFactory : IBaseRateFactory, IDefaultMeasurableFactory<IRate>, IFactory<IRate>
{
    IDenominatorFactory DenominatorFactory { get; init; }

    IRate Create(params IRateComponent[] rateComponents);
}
