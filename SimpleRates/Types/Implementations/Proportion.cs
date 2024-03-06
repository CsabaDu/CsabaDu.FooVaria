namespace CsabaDu.FooVaria.Proportions.Types.Implementations;

internal sealed class Proportion : SimpleRate, IProportion
{
    #region Constructors
    public Proportion(IProportionFactory factory, IBaseRate other) : base(factory, other)
    {
    }

    public Proportion(IProportionFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode) : base(factory, numeratorCode, defaultQuantity, denominatorCode)
    {
    }
    #endregion

    #region Public methods
    public IProportion GetProportion(IQuantifiable numerator, IQuantifiable denominator)
    {
        return (IProportion)GetFactory().CreateBaseRate(numerator, denominator);
    }

    public IProportion GetProportion(IQuantifiable numerator, IBaseMeasurement denominator)
    {
        return (IProportion)GetFactory().CreateBaseRate(numerator, denominator);
    }

    public IProportion GetProportion(IQuantifiable numerator, Enum denominatorContext)
    {
        return (IProportion)GetFactory().CreateBaseRate(numerator, denominatorContext);
    }

    public IProportion GetProportion(Enum numeratorContext, decimal quantity, Enum denominatorContext)
    {
        return GetFactory().Create(numeratorContext, quantity, denominatorContext);
    }

    public IProportion GetProportion(IBaseRate baseRate)
    {
        return GetFactory().Create(baseRate);
    }

    public override IProportionFactory GetFactory()
    {
        return (IProportionFactory)Factory;
    }
    #endregion
}
