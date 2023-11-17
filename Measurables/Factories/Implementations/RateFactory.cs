namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public abstract class RateFactory : IRateFactory
{
    #region Constructors
    protected RateFactory(IDenominatorFactory denominatorFactory)
    {
        DenominatorFactory = NullChecked(denominatorFactory, nameof(denominatorFactory));
    }
    #endregion

    #region Properties
    public IDenominatorFactory DenominatorFactory { get; init; }
    #endregion

    #region Public methods
    #region Abstract methods
    public abstract IMeasurable Create(IMeasurable other);
    public abstract IBaseRate Create(IQuantifiable numerator, MeasureUnitTypeCode measureUnitTypeCode);
    public abstract IBaseRate Create(IQuantifiable numerator, Enum denominatorMeasureUnit);
    public abstract IRate Create(IRate other);
    //public abstract IBaseMeasurable CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
    #endregion
    #endregion
}
