namespace CsabaDu.FooVaria.Rates.Factories.Implementations;

public abstract class RateFactory : IRateFactory
{
    #region Constructors
    private protected RateFactory(IDenominatorFactory denominatorFactory)
    {
        DenominatorFactory = NullChecked(denominatorFactory, nameof(denominatorFactory));
    }
    #endregion

    #region Properties
    public IDenominatorFactory DenominatorFactory { get; init; }
    #endregion

    #region Public methods
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

        IRate createRateFrom2or3Params() // TODO fölösleges?
        {
            if (baseMeasures is IBaseMeasure[] rateComponents) return Create(rateComponents);

            throw exception();
        }

        ArgumentOutOfRangeException exception()
        {
            return ArgumentTypeOutOfRangeException(nameof(baseMeasures), baseMeasures!);
        }
        #endregion
    }

    #region Abstract methods
    public abstract IRate Create(params IBaseMeasure[] rateComponents);
    public abstract IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement);
    public abstract IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
    public abstract IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorCode);
    public IBaseRate CreateBaseRate(IBaseRate baseRate)
    {
        if (baseRate is IRate other) return CreateNew(other);

        decimal defaultQuantity = NullChecked(baseRate, nameof(baseRate)).GetDefaultQuantity();
        MeasureUnitCode numeratorCode = baseRate.GetNumeratorCode();
        IBaseMeasure numerator = CreateBaseMeasure(numeratorCode, defaultQuantity);
        MeasureUnitCode denominatorCode = baseRate.GetMeasureUnitCode();

        return CreateBaseRate(numerator, denominatorCode);
    }
    public abstract IRate CreateNew(IRate other);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static T GetValidRateParam<T>(IMeasurable measurable, string paramName)
        where T : class, IMeasurable
    {
        if (measurable is T validRateComponent) return validRateComponent;

        throw ArgumentTypeOutOfRangeException(paramName, measurable);
    }

    #endregion
    #endregion
}
