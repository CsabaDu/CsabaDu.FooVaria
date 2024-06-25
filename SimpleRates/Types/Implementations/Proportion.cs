namespace CsabaDu.FooVaria.SimpleRates.Types.Implementations;

internal sealed class Proportion : SimpleRate, IProportion
{
    #region Constructors
    public Proportion(IProportionFactory factory, IBaseRate other) : base(factory, other)
    {
        //Factory = factory;
    }

    public Proportion(IProportionFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode) : base(factory, numeratorCode, defaultQuantity, denominatorCode)
    {
        //Factory = factory;
    }
    #endregion

    #region Properties
    //public IProportionFactory Factory { get; init; }
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

    #region Override methods
    public IProportionFactory GetFactory()
    {
        return (IProportionFactory)Factory;
    }
    #endregion
    #endregion
}
