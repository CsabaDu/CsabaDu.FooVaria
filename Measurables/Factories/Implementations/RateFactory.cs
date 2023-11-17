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
    public abstract IBaseRate Create(IBaseMeasureTemp numerator, MeasureUnitTypeCode measureUnitTypeCode);
    public abstract IBaseRate Create(IBaseMeasureTemp numerator, Enum denominatorMeasureUnit);
    public abstract IRate Create(IRate other);

    public IBaseRate Create(IBaseMeasureTemp numerator, IBaseMeasurable denominator)
    {
        throw new NotImplementedException();
    }

    //public IBaseRate Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    //{
    //    throw new NotImplementedException();
    //}
    //public abstract IBaseMeasurable CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
    #endregion
    #endregion
}
