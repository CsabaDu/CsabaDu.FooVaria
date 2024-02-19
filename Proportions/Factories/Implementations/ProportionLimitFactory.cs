namespace CsabaDu.FooVaria.Proportions.Factories.Implementations;

public sealed class ProportionLimitFactory : IProportionLimitFactory
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

    public ISimpleRate CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        return Create(numeratorCode, defaultQuantity, denominatorCode, default);
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
    {
        return Create(numerator, denominatorMeasurement, default);
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
    {
        return Create(numerator, denominatorMeasureUnit, default);
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorCode)
    {
        return Create(numerator, denominatorCode, default);
    }

    public IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures)
    {
        string paramName = nameof(baseMeasures);
        int count = NullChecked(baseMeasures, paramName).Length;

        if (count != 2) throw CountArgumentOutOfRangeException(count, paramName);

        return Create(baseMeasures[0], baseMeasures[1], default);
    }

    public IBaseRate CreateBaseRate(IBaseRate baseRate)
    {
        if (baseRate is IProportionLimit other) return CreateNew(other);

        return Create(baseRate, default);
    }

    public IProportionLimit CreateNew(IProportionLimit other)
    {
        return new ProportionLimit(other);
    }
    #endregion
}
