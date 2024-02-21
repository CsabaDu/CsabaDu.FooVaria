namespace CsabaDu.FooVaria.AbstractTypes.SimpleRates.Factories.Implementations;

public abstract class SimpleRateFactory : ISimpleRateFactory
{
    #region Public methods
    public IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
    {
        return CreateSimpleRate(numerator, nameof(numerator), getDenominatorComponents);

        #region Local methods
        (MeasureUnitCode, decimal) getDenominatorComponents()
        {
            Enum? measureUnit = denominatorMeasurement?.GetMeasureUnit();

            return GetSimpleRateComponents(measureUnit, nameof(denominatorMeasurement));
        }
        #endregion
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
    {
        return CreateSimpleRate(numerator, nameof(numerator), getDenominatorComponents);

        #region Local methods
        (MeasureUnitCode, decimal) getDenominatorComponents()
        {
            return GetSimpleRateComponents(denominatorMeasureUnit, nameof(denominatorMeasureUnit));
        }
        #endregion
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorCode)
    {
        var (numeratorCode, defaultQuantity) = GetSimpleRateComponents(numerator, nameof(numerator));

        return CreateSimpleRate(numeratorCode, defaultQuantity, denominatorCode);
    }

    public IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures)
    {
        string paramName = nameof(baseMeasures);
        int count = NullChecked(baseMeasures, paramName).Length;

        if (count != 2) throw CountArgumentOutOfRangeException(count, paramName);

        return CreateSimpleRate(baseMeasures[0], paramName, getDenominatorComponents);

        #region Local methods
        (MeasureUnitCode, decimal) getDenominatorComponents()
        {
            return GetSimpleRateComponents(baseMeasures[1], paramName);
        }
        #endregion
    }

    #region Abstract methods
    public abstract IBaseRate CreateBaseRate(IBaseRate baseRate);
    public abstract ISimpleRate CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static (MeasureUnitCode MeasureUnitCode, decimal Quantity) GetSimpleRateComponents(IBaseMeasure baseMeasure, string paramName)
    {
        MeasureUnitCode measureUnitCode = NullChecked(baseMeasure, paramName).GetMeasureUnitCode();
        decimal quantity = baseMeasure.GetDefaultQuantity();

        return (measureUnitCode, quantity);
    }

    protected static (MeasureUnitCode, decimal, MeasureUnitCode) GetSimpleRateParams(IBaseMeasure numerator, string paramName, Func<(MeasureUnitCode, decimal)> getDenominatorComponents)
    {
        var (numeratorCode, defaultQuantity) = GetSimpleRateComponents(numerator, paramName);
        var (denominatorCode, exchangeRate) = getDenominatorComponents();
        defaultQuantity /= exchangeRate;

        return (numeratorCode, defaultQuantity, denominatorCode);
    }
    #endregion
    #endregion

    #region Private methods
    private ISimpleRate CreateSimpleRate(IBaseMeasure numerator, string paramName, Func<(MeasureUnitCode, decimal)> getDenominatorComponents)
    {
        var (numeratorCode, defaultQuantity, denominatorCode) = GetSimpleRateParams(numerator, paramName, getDenominatorComponents);

        return CreateSimpleRate(numeratorCode, defaultQuantity, denominatorCode);
    }

    #region Static methods
    private static (MeasureUnitCode MeasureUnitCode, decimal ExchangeRate) GetSimpleRateComponents(Enum? measureUnit, string paramName)
    {
        decimal exchangeRate = GetExchangeRate(measureUnit, paramName);
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode(measureUnit!);

        return (measureUnitCode, exchangeRate); 
    }
    #endregion
    #endregion
}
