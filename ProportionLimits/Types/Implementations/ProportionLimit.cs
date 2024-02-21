namespace CsabaDu.FooVaria.ProportionLimits.Types.Implementations;

internal sealed class ProportionLimit : SimpleRate, IProportionLimit
{
    #region Constructors
    internal ProportionLimit(IProportionLimitFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode) : base(factory, numeratorCode, defaultQuantity, denominatorCode)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    internal ProportionLimit(IProportionLimitFactory factory, Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit, LimitMode limitMode) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    internal ProportionLimit(IProportionLimitFactory factory, IBaseRate baseRate, LimitMode limitMode) : base(factory, baseRate)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
    }

    internal ProportionLimit(IProportionLimit other) : base(other)
    {
        LimitMode = other.GetLimitMode()!.Value;
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

        if (x.GetLimitMode()!.Value != y.GetLimitMode()!.Value) return false;

        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] IProportionLimit proportionLimit)
    {
        return HashCode.Combine(proportionLimit.GetLimitMode()!.Value, proportionLimit.GetHashCode());
    }

    public decimal GetLimiterDefaultQuantity()
    {
        return GetDefaultQuantity();
    }

    public MeasureUnitCode GetLimiterMeasureUnitCode()
    {
        return GetNumeratorCode();
    }

    public IProportionLimit GetProportionLimit(IBaseRate baseRate, LimitMode limitMode)
    {
        return GetFactory().Create(baseRate, limitMode);
    }

    public IProportionLimit GetProportionLimit(IBaseMeasure numerator, IBaseMeasure denominator, LimitMode limitMode)
    {
        return GetFactory().Create(numerator, denominator, limitMode);
    }

    public IProportionLimit GetProportionLimit(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode)
    {
        return GetFactory().Create(numeratorCode, defaultQuantity, denominatorCode, limitMode);
    }
    public IProportionLimit GetProportionLimit(IBaseMeasure numerator, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit GetProportionLimit(IBaseMeasure numerator, MeasureUnitCode denominatorCode, LimitMode limitMode)
    {
        return GetFactory().Create(numerator, denominatorCode, limitMode);
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
    #endregion
    #endregion
}
