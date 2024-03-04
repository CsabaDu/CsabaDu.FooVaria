namespace CsabaDu.FooVaria.AbstractTypes.SimpleRates.Factories.Implementations;

public abstract class SimpleRateFactory(IMeasureFactory measureFactory) : ISimpleRateFactory
{
    #region Properties
    public IMeasureFactory MeasureFactory { get; init; } = NullChecked(measureFactory, nameof(measureFactory));
    #endregion

    #region Public methods
    public IBaseRate CreateBaseRate(IQuantifiable numerator, Enum denominator)
    {
        MeasureUnitCode numeratorCode = NullChecked(numerator, nameof(numerator)).GetMeasureUnitCode();
        MeasurementElements denominatorElements = GetMeasurementElements(denominator);
        MeasureUnitCode denominatorCode = denominatorElements.GetMeasureUnitCode();
        decimal defaultQuantity = numerator.GetDecimalQuantity() * denominatorElements.ExchangeRate;

        return CreateSimpleRate(numeratorCode, defaultQuantity, denominatorCode);
    }

    public IBaseRate CreateBaseRate(IQuantifiable numerator, IMeasurable denominator)
    {
        if (denominator is IQuantifiable quantifiable && numerator != null) return CreateBaseRate(numerator, quantifiable);

        if (NullChecked(denominator, nameof(denominator)) is IBaseQuantifiable)
            throw ArgumentTypeOutOfRangeException(nameof(denominator), denominator);

        Enum denominatorMeasureUnit = denominator.GetBaseMeasureUnit();

        return CreateBaseRate(numerator!, denominatorMeasureUnit);
    }

    public IBaseRate CreateBaseRate(params IQuantifiable[] quantifiables)
    {
        string paramName = nameof(quantifiables);
        int count = NullChecked(quantifiables, paramName).Length;

        if (count != 2) throw CountArgumentOutOfRangeException(count, paramName);

        IQuantifiable numerator = quantifiables[0];
        IQuantifiable denominator = quantifiables[1];
        decimal defaultQuantity = numerator.ProportionalTo(denominator);

        return CreateSimpleRate(numerator.GetMeasureUnitCode(), defaultQuantity, denominator.GetMeasureUnitCode());
    }

    #region Abstract methods
    public abstract ISimpleRate CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static (MeasureUnitCode MeasureUnitCode, decimal Quantity) GetSimpleRateComponents(IQuantifiable quantifiable, string paramName)
    {
        MeasureUnitCode measureUnitCode = NullChecked(quantifiable, paramName).GetMeasureUnitCode();
        decimal quantity = quantifiable.GetDefaultQuantity();

        return (measureUnitCode, quantity);
    }

    protected static (Enum MeasureUnit, decimal Quantity) GetSimpleRateComponents_1(IQuantifiable? quantifiable, string paramName)
    {
        Enum measureUnit = NullChecked(quantifiable, paramName).GetBaseMeasureUnit();
        decimal quantity = quantifiable!.GetDecimalQuantity();

        return (measureUnit, quantity);
    }

    protected static (MeasureUnitCode, decimal, MeasureUnitCode) GetSimpleRateParams(IQuantifiable numerator, string paramName, Func<(MeasureUnitCode, decimal)> getDenominatorComponents)
    {
        var (numeratorCode, defaultQuantity) = GetSimpleRateComponents(numerator, paramName);
        var (denominatorCode, exchangeRate) = getDenominatorComponents();
        defaultQuantity /= exchangeRate;

        return (numeratorCode, defaultQuantity, denominatorCode);
    }

    protected static (Enum, decimal, Enum) GetSimpleRateParams_1(IQuantifiable numerator, string paramName, Func<(Enum, decimal)> getDenominatorMeasureUnit)
    {
        var (numeratorMeasureUnit, quantity) = GetSimpleRateComponents_1(numerator, paramName);
        var (denominatorMeasureUnit, exchangeRate) = getDenominatorMeasureUnit();
        quantity /= exchangeRate;

        return (numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }
    #endregion
    #endregion

    #region Private methods
    //private ISimpleRate CreateSimpleRate(IQuantifiable numerator, string paramName, Func<(Enum, decimal)> getDenominatorComponents)
    //{
    //    var (numeratorMeasureUnit, quantity, denominatorMeasureUnit) = GetSimpleRateParams_1(numerator, paramName, getDenominatorComponents);

    //    return CreateSimpleRate(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    //}

    #region Static methods
    //private static (MeasureUnitCode MeasureUnitCode, decimal ExchangeRate) GetSimpleRateComponents(Enum? measureUnit, string paramName)
    //{
    //    decimal exchangeRate = GetExchangeRate(measureUnit, paramName);
    //    MeasureUnitCode measureUnitCode = GetMeasureUnitCode(measureUnit!);

    //    return (measureUnitCode, exchangeRate); 
    //}
    #endregion
    #endregion
}
