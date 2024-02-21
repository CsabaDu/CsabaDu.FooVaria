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
        throw new NotImplementedException();
    }

    public IProportionLimit Create(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode)
    {
        return new ProportionLimit(this, numeratorCode, defaultQuantity, denominatorCode, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        Enum numeratorMeasureUnit = NullChecked(numerator, nameof(numerator)).GetMeasureUnit();
        ValueType quantity = (ValueType)numerator.Quantity;

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, MeasureUnitCode denominatorCode, LimitMode limitMode)
    {
        Enum denominatorMeasureUnit = denominatorCode.GetDefaultMeasureUnit();

        return Create(numerator, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode)
    {
        throw new NotImplementedException();
    }

    public IProportionLimit Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        return new ProportionLimit(this, numeratorMeasureUnit, quantity, denominatorMeasureUnit, limitMode);
    }

    public override IBaseRate CreateBaseRate(IBaseRate baseRate)
    {
        LimitMode limitMode = NullChecked(baseRate, nameof(baseRate)).GetLimitMode() ?? default;

        return Create(baseRate, limitMode);
    }

    public IProportionLimit CreateNew(IProportionLimit other)
    {
        return new ProportionLimit(other);
    }

    public override ISimpleRate CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        return Create(numeratorCode, defaultQuantity, denominatorCode, default);
    }
    #endregion
}
