namespace CsabaDu.FooVaria.Proportions.Types.Implementations;

internal sealed class ProportionLimit : Proportion, IProportionLimit
{
    #region Constructors
    internal ProportionLimit(IProportionLimitFactory factory, MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode) : base(factory, numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    internal ProportionLimit(IProportionLimitFactory factory, Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    internal ProportionLimit(IProportionLimitFactory factory, IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode) : base(factory, numerator, denominatorMeasurement)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    internal ProportionLimit(IProportionLimitFactory factory, IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode) : base(factory, numerator, denominator)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    internal ProportionLimit(IProportionLimitFactory factory, IBaseRate baseRate, LimitMode limitMode) : base(factory, baseRate)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    internal ProportionLimit(IProportionLimit other) : base(other)
    {
        LimitMode = other.LimitMode;
    }
    #endregion

    #region Properties
    public LimitMode LimitMode { get; init; }
    #endregion

    #region Public methods
    public bool Equals(IProportionLimit? x, IProportionLimit? y)
    {
        if (x == null && y == null) return true;

        if (x == null || y == null) return false;

        if (x.LimitMode != y.LimitMode) return false;

        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] IProportionLimit proportionLimit)
    {
        return HashCode.Combine(proportionLimit.LimitMode, proportionLimit.GetHashCode());
    }

    public decimal GetLimiterDefaultQuantity()
    {
        return GetDefaultQuantity();
    }

    public MeasureUnitCode GetLimiterMeasureUnitCode()
    {
        return GetNumeratorMeasureUnitCode();
    }

    public LimitMode GetLimitMode(ILimiter limiter)
    {
        return NullChecked(limiter, nameof(limiter)).LimitMode;
    }

    public IProportionLimit GetProportionLimit(IBaseRate baseRate, LimitMode limitMode)
    {
        return GetFactory().Create(baseRate, limitMode);
    }

    public IProportionLimit GetProportionLimit(IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode)
    {
        return GetFactory().Create(numerator, denominator, limitMode);
    }

    public IProportionLimit GetProportionLimit(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode)
    {
        return GetFactory().Create(numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode, limitMode);
    }
    public IProportionLimit GetProportionLimit(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit GetProportionLimit(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode, LimitMode limitMode)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnitCode, limitMode);
    }

    public IProportionLimit GetNew(IProportionLimit other)
    {
        return GetFactory().CreateNew(other);
    }

    public IProportionLimit GetProportionLimit(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode)
    {
        return GetFactory().Create(numerator, denominatorMeasurement, limitMode);
    }

    public IProportionLimit GetProportionLimit(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        return GetFactory().Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit, limitMode);
    }

    public bool? Includes(IBaseRate? limitable)
    {
        return limitable?.FitsIn(this);
    }

    #region Override methods
    public override IProportionLimitFactory GetFactory()
    {
        return (IProportionLimitFactory)Factory;
    }

    public override IProportionLimit GetProportion(IBaseRate baseRate)
    {
        return GetFactory().Create(baseRate, default);
    }

    public override IProportionLimit GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode)
    {
        return GetFactory().Create(numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode, default);
    }
    #endregion
    #endregion
}
