namespace CsabaDu.FooVaria.Rates.Factories;

public interface IRateFactory : IBaseRateFactory, IDeepCopyFactory<IRate>
{
    IDenominatorFactory DenominatorFactory { get; init; }

    IRate Create(params IBaseMeasure[] rateComponents);
    IDenominator CreateDenominator(IQuantifiable quantifiable);
}
