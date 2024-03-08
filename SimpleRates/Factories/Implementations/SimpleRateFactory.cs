namespace CsabaDu.FooVaria.SimpleRates.Factories.Implementations;

public abstract class SimpleRateFactory(IMeasureFactory measureFactory) : ISimpleRateFactory
{
    #region Structs
    protected readonly struct SimpleRateParams(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        public MeasureUnitCode NumeratorCode => numeratorCode;
        public decimal DefaultQuantity => defaultQuantity;
        public MeasureUnitCode DenominatorCode => denominatorCode;
    }
    #endregion

    #region Properties
    public IMeasureFactory MeasureFactory { get; init; } = NullChecked(measureFactory, nameof(measureFactory));
    #endregion

    #region Public methods
    public IBaseRate CreateBaseRate(IQuantifiable numerator, Enum denominator)
    {
        MeasurementElements denominatorElements = new(denominator, nameof(denominator));
        SimpleRateParams simpleRateParams = GetSimpleRateParams(numerator, nameof(numerator), denominatorElements);

        return CreateSimpleRate(simpleRateParams);
    }

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IMeasurable denominator)
    {
        if (denominator is IQuantifiable quantifiable) return CreateBaseRate(numerator, quantifiable);

        const string paramName = nameof(denominator);

        if (denominator is IBaseQuantifiable) throw ArgumentTypeOutOfRangeException(paramName, denominator);

        Enum measureUnit = NullChecked(denominator, paramName).GetBaseMeasureUnit();
        MeasurementElements denominatorElements = new(measureUnit, paramName);
        SimpleRateParams simpleRateParams = GetSimpleRateParams(numerator, nameof(numerator), denominatorElements);

        return CreateSimpleRate(simpleRateParams);
    }

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    {
        SimpleRateParams simpleRateParams = GetSimpleRateParams(numerator, denominator);

        return CreateSimpleRate(simpleRateParams);
    }

    #region Abstract methods
    public abstract ISimpleRate CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
    #endregion
    #endregion

    #region Protected methods
    protected ISimpleRate CreateSimpleRate(SimpleRateParams simpleRateParams)
    {
        return CreateSimpleRate(simpleRateParams.NumeratorCode, simpleRateParams.DefaultQuantity, simpleRateParams.DenominatorCode);
    }

    #region Static methods
    protected static SimpleRateParams GetSimpleRateParams(IQuantifiable numerator, string paramName, MeasurementElements denominatorElements)
    {
        MeasureUnitCode numeratorCode = NullChecked(numerator, paramName).GetMeasureUnitCode();
        decimal defaultQuantity = numerator.GetDefaultQuantity() / denominatorElements.ExchangeRate;
        MeasureUnitCode denominatorCode = denominatorElements.MeasureUnitCode;

        return new(numeratorCode, defaultQuantity, denominatorCode);
    }

    protected static SimpleRateParams GetSimpleRateParams(Enum numeratorContext, decimal quantity, Enum denominator)
    {
        MeasurementElements measurementElements = new(numeratorContext, nameof(numeratorContext));
        MeasureUnitCode numeratorCode = measurementElements.MeasureUnitCode;
        decimal numeratorExchangeRate = measurementElements.ExchangeRate;

        measurementElements = new(denominator, nameof(denominator));
        MeasureUnitCode denominatorCode = measurementElements.MeasureUnitCode;
        decimal denominatorExchangeRate = measurementElements.ExchangeRate;

        if (numeratorExchangeRate != denominatorExchangeRate)
        {
            quantity *= numeratorExchangeRate /= denominatorExchangeRate;
        }

        return new(numeratorCode, quantity, denominatorCode);
    }

    protected static SimpleRateParams GetSimpleRateParams(IQuantifiable numerator, IQuantifiable denominator)
    {
        MeasureUnitCode numeratorCode = NullChecked(numerator, nameof(numerator)).GetMeasureUnitCode();
        MeasureUnitCode denominatorCode = NullChecked(denominator, nameof(denominator)).GetMeasureUnitCode();
        decimal defaultQuantity = numerator.GetDefaultQuantity() / denominator.GetDefaultQuantity();

        return new(numeratorCode, defaultQuantity, denominatorCode);
    }
    #endregion
    #endregion
}
