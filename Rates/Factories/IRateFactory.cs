namespace CsabaDu.FooVaria.Rates.Factories;

public interface IRateFactory : IBaseRateFactory, IFactory<IRate>
{
    IDenominatorFactory DenominatorFactory { get; init; }

    IRate Create(params IBaseMeasure[] rateComponents);
    IDenominator CreateDenominator(IQuantifiable quantifiable);
}
