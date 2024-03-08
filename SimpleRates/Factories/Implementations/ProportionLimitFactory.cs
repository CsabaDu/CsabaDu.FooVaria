namespace CsabaDu.FooVaria.SimpleRates.Factories.Implementations;

public sealed class ProportionLimitFactory(IMeasureFactory measureFactory) : SimpleRateFactory(measureFactory), IProportionLimitFactory
{
    #region Public methods
    public IProportionLimit Create(IBaseRate baseRate, LimitMode limitMode)
    {
        return new ProportionLimit(this, baseRate, limitMode);
    }

    public IProportionLimit Create(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode)
    {
        return new ProportionLimit(this, numeratorCode, defaultQuantity, denominatorCode, limitMode);
    }

    public IProportionLimit Create(IQuantifiable numerator, IQuantifiable denominator, LimitMode limitMode)
    {
        SimpleRateParams simpleRateParams = GetSimpleRateParams(numerator, denominator);

        return CreateProportionLimit(simpleRateParams, limitMode);
    }

    public IProportionLimit Create(IQuantifiable numerator, Enum denominator, LimitMode limitMode)
    {
        MeasurementElements denominatorElements = GetMeasurementElements(denominator, nameof(denominator));
        SimpleRateParams simpleRateParams = GetSimpleRateParams(numerator, nameof(numerator), denominatorElements);

        return CreateProportionLimit(simpleRateParams, limitMode);
    }

    public IProportionLimit Create(IQuantifiable numerator, IBaseMeasurement denominator, LimitMode limitMode)
    {
        const string paramName = nameof(denominator);
        Enum measureUnit = NullChecked(denominator, paramName).GetBaseMeasureUnit();
        MeasurementElements denominatorElements = GetMeasurementElements(measureUnit, paramName);
        SimpleRateParams simpleRateParams = GetSimpleRateParams(numerator, nameof(numerator), denominatorElements);

        return CreateProportionLimit(simpleRateParams, limitMode);
    }

    public IProportionLimit Create(Enum numeratorContext, decimal quantity, Enum denominator, LimitMode limitMode)
    {
        if (numeratorContext is MeasureUnitCode numeratorCode
            && denominator is MeasureUnitCode denominatorCode)
        {
            return Create(numeratorCode, quantity, denominatorCode, limitMode);
        }

        SimpleRateParams simpleRateParams = GetSimpleRateParams(numeratorContext, quantity, denominator);

        return CreateProportionLimit(simpleRateParams, limitMode);
    }

    #region Override methods
    public override IProportionLimit CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        return Create(numeratorCode, defaultQuantity, denominatorCode, default);
    }
    #endregion
    #endregion

    #region Private methods
    private IProportionLimit CreateProportionLimit(SimpleRateParams simpleRateParams, LimitMode limitMode)
    {
        return Create(simpleRateParams.NumeratorCode, simpleRateParams.DefaultQuantity, simpleRateParams.DenominatorCode, limitMode);
    }
    #endregion
}
