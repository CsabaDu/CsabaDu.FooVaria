namespace CsabaDu.FooVaria.Proportions.Factories.Implementations;

public sealed class ProportionLimitFactory : IProportionLimitFactory
{
    public ProportionLimitFactory(/*IProportionFactory proportionFactory*/)
    {
    }

    public IProportionLimit Create(IBaseRate baseRate, LimitMode limitMode)
    {
        return new ProportionLimit(this, baseRate, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode)
    {
        return new ProportionLimit(this, numerator, denominator, limitMode);
    }

    public IProportionLimit Create(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode)
    {
        return new ProportionLimit(this, numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        Enum numeratorMeasureUnit = NullChecked(numerator, nameof(numerator)).GetMeasureUnit();
        ValueType quantity = (ValueType)numerator.Quantity;

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode)
    {
        Enum denominatorMeasureUnit = denominatorMeasureUnitCode.GetDefaultMeasureUnit();

        return Create(numerator, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit Create(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode)
    {
        return new ProportionLimit(this, numerator, denominatorMeasurement, limitMode);
    }

    public IProportionLimit Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        return new ProportionLimit(this, numeratorMeasureUnit, quantity, denominatorMeasureUnit, limitMode);
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
    {
        return Create(numerator, denominatorMeasurement, default);
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
    {
        return Create(numerator, denominatorMeasureUnit, default);
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
    {
        return Create(numerator, denominatorMeasureUnitCode, default);
    }

    public IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures)
    {
        string paramName = nameof(baseMeasures);
        int count = NullChecked(baseMeasures, paramName).Length;

        if (count != 2) throw CountArgumentOutOfRangeException(count, paramName);

        return Create(baseMeasures[0], baseMeasures[1], default);
    }

    public IProportionLimit CreateNew(IProportionLimit other)
    {
        return new ProportionLimit(other);
    }
}
