

namespace CsabaDu.FooVaria.Rates.Factories.Implementations;

public abstract class RateFactory : IRateFactory
{
    public IDenominatorFactory DenominatorFactory { get; init; }

    private protected RateFactory(IDenominatorFactory denominatorFactory)
    {
        DenominatorFactory = NullChecked(denominatorFactory, nameof(denominatorFactory));
    }

    public IBaseMeasure CreateBaseMeasure(Enum measureUnit, ValueType quantity)
    {
        return DenominatorFactory.Create(measureUnit, quantity);
    }

    public IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures)
    {
        int count = baseMeasures?.Length ?? 0;

        return count switch
        {
            1 => createRateFrom1Param(),
            2 or 3 => createRateFrom2or3Params(),

            _ => throw CountArgumentOutOfRangeException(count, nameof(baseMeasures)),
        };

        #region Local methods
        IRate createRateFrom1Param()
        {
            if (baseMeasures![0] is IRate rate) return CreateNew(rate);

            throw exception();
        }

        IRate createRateFrom2or3Params()
        {
            if (baseMeasures is IRateComponent[] rateComponents) return Create(rateComponents);

            throw exception();
        }

        ArgumentOutOfRangeException exception()
        {
            return ArgumentTypeOutOfRangeException(nameof(baseMeasures), baseMeasures!);
        }
        #endregion
    }

    public abstract IRate Create(params IRateComponent[] rateComponents);
    public abstract IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    public abstract IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    public abstract IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    public abstract IRate CreateNew(IRate other);

    protected static T GetValidRateParam<T>(IMeasurable measurable, string paramName) where T : class, IMeasurable
    {
        if (measurable is T validRateComponent) return validRateComponent;

        throw ArgumentTypeOutOfRangeException(paramName, measurable);
    }
}

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
