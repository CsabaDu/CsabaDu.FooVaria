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

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    {
        IMeasure measure = GetValidNumerator(numerator, nameof(numerator));
        IDenominator baseMeasure = DenominatorFactory.Create(denominator);

        return Create(measure, baseMeasure);
    }

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IMeasurable denominator)
    {
        if (denominator is IQuantifiable quantifiable) return CreateBaseRate(numerator, quantifiable);

        string paramName = nameof(denominator);

        if (denominator is IBaseQuantifiable) throw ArgumentTypeOutOfRangeException(paramName, denominator);

        Enum measureUnit = NullChecked(denominator, paramName).GetBaseMeasureUnit();

        return CreateBaseRate(numerator, measureUnit);
    }
    
    public IDenominator CreateDenominator(IQuantifiable quantifiable)
    {
        return DenominatorFactory.Create(quantifiable);
    }

    #region Abstract methods
    public abstract IRate Create(params IBaseMeasure[] baseMeasures);
    public abstract IBaseRate CreateBaseRate(IQuantifiable numerator, Enum denominator);
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

    protected static IMeasure GetValidNumerator(IQuantifiable numerator, string paramName)
    {
        if (NullChecked(numerator, paramName) is IMeasure measure) return measure;
        
        throw ArgumentTypeOutOfRangeException(paramName, numerator);
    }
    #endregion
    #endregion
}
