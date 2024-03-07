namespace CsabaDu.FooVaria.SimpleRates.Factories.Implementations;

public sealed class ProportionFactory(IMeasureFactory measureFactory) : SimpleRateFactory(measureFactory), IProportionFactory
{
    #region Public methods
    public IProportion Create(IBaseRate baseRate)
    {
        return new Proportion(this, baseRate);
    }

    public IProportion Create(Enum numeratorContext, decimal quantity, Enum denominator)
    {
        if (numeratorContext is MeasureUnitCode numeratorCode
            && denominator is MeasureUnitCode denominatorCode)
        {
            return CreateSimpleRate(numeratorCode, quantity, denominatorCode);
        }

        SimpleRateParams simpleRateParams = GetSimpleRateParams(numeratorContext, quantity, denominator);

        return (IProportion)CreateSimpleRate(simpleRateParams);
    }

    #region Override methods
    public override IProportion CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        return new Proportion(this, numeratorCode, defaultQuantity, denominatorCode);
    }
    #endregion
    #endregion
}
