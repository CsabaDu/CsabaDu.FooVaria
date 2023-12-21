//namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

//public abstract class RateFactory : IRateFactory
//{
//    #region Constructors
//    protected RateFactory(IDenominatorFactory denominatorFactory)
//    {
//        DenominatorFactory = NullChecked(denominatorFactory, nameof(denominatorFactory));
//    }
//    #endregion

//    #region Properties
//    public IDenominatorFactory DenominatorFactory { get; init; }
//    #endregion

//    #region Public methods
//    #region Abstract methods
//    public abstract IMeasurable CreateNew(IMeasurable other);
//    public abstract IBaseRate CreateNew(IBaseMeasure numerator, MeasureUnitTypeCode measureUnitTypeCode);
//    public abstract IBaseRate CreateNew(IBaseMeasure numerator, Enum denominatorMeasureUnit);
//    public abstract IRate CreateNew(IRate other);

//    public IBaseRate CreateNew(IBaseMeasure numerator, IMeasurable denominator)
//    {
//        throw new NotImplementedException();
//    }

//    //public IBaseRate CreateNew(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
//    //{
//    //    throw new NotImplementedException();
//    //}
//    //public abstract IBaseMeasurable CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
//    #endregion
//    #endregion
//}
