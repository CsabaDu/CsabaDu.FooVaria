namespace CsabaDu.FooVaria.ProportionLimits.Factories.Implementations;

public sealed class ProportionLimitFactory : SimpleRateFactory, IProportionLimitFactory
{
    #region Public methods
    public IProportionLimit Create(IBaseRate baseRate, LimitMode limitMode)
    {
        return new ProportionLimit(this, baseRate, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode)
    {
        var (numeratorCode, defaultQuantity, denominatorCode) = GetSimpleRateParams(numerator, nameof(numerator), getDenominatorComponents);

        return Create(numeratorCode, defaultQuantity, denominatorCode, limitMode);

        #region Local methods
        (MeasureUnitCode, decimal) getDenominatorComponents()
        {
            return GetSimpleRateComponents(denominator, nameof(denominator));
        }
        #endregion
    }

    public IProportionLimit Create(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode)
    {
        return new ProportionLimit(this, numeratorCode, defaultQuantity, denominatorCode, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        Enum numeratorMeasureUnit = NullChecked(numerator, nameof(numerator)).GetBaseMeasureUnit();
        ValueType quantity = numerator.GetBaseQuantity();

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, MeasureUnitCode denominatorCode, LimitMode limitMode)
    {
        Enum denominatorMeasureUnit = denominatorCode.GetDefaultMeasureUnit();

        return Create(numerator, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode)
    {
        Enum denominatorMeasureUnit = NullChecked(denominatorMeasurement, nameof(denominatorMeasurement)).GetBaseMeasureUnit();

        return Create(numerator, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        return new ProportionLimit(this, numeratorMeasureUnit, quantity, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit CreateNew(IProportionLimit other)
    {
        return new ProportionLimit(other);
    }

    #region Override methods
    public override IProportionLimit CreateBaseRate(IBaseRate baseRate)
    {
        if (NullChecked(baseRate, nameof(baseRate)) is IProportionLimit other) return CreateNew(other);

        return Create(baseRate, default);
    }

    public override IProportionLimit CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        return Create(numeratorCode, defaultQuantity, denominatorCode, default);
    }
    #endregion
    #endregion
}
