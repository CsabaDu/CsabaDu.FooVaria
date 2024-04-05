namespace CsabaDu.FooVaria.SimpleRates.Types.Implementations;

internal sealed class ProportionLimit : SimpleRate, IProportionLimit
{
    #region Constructors
    internal ProportionLimit(IProportionLimitFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode, LimitMode limitMode) : base(factory, numeratorCode, defaultQuantity, denominatorCode)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
        Factory = factory;
    }

    internal ProportionLimit(IProportionLimitFactory factory, IBaseRate baseRate, LimitMode limitMode) : base(factory, baseRate)
    {
        LimitMode = Defined(limitMode, nameof(limitMode));
        Factory = factory;
    }
    #endregion

    #region Properties
    public LimitMode LimitMode { get; init; }
    public IProportionLimitFactory Factory { get; init; }
    #endregion

    #region Public methods
    public bool Equals(IProportionLimit? x, IProportionLimit? y)
    {
        if (x is null && y is null) return true;

        return x is not null
            && y is not null
            && x.GetLimitMode()!.Value == y.GetLimitMode()!.Value
            && x.Equals(y);
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
        return Factory.Create(baseRate, limitMode);
    }

    public IProportionLimit GetProportionLimit(Enum numeratorMeasureUnit, decimal quantity, Enum denominatorMeasureUnit, LimitMode limitMode)
    {
        return Factory.Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit, limitMode);
    }

    public IProportionLimit GetProportionLimit(IQuantifiable numerator, IQuantifiable denominator, LimitMode limitMode)
    {
        return Factory.Create(numerator, denominator, limitMode);
    }

    public IProportionLimit GetProportionLimit(IQuantifiable numerator, IBaseMeasurement denominatorMeasurement, LimitMode limitMode)
    {
        return Factory.Create(numerator, denominatorMeasurement, limitMode);
    }

    public IProportionLimit GetProportionLimit(IQuantifiable numerator, Enum denominatorContext, LimitMode limitMode)
    {
        return Factory.Create(numerator, denominatorContext, limitMode);
    }

    public bool? Includes(IBaseRate? limitable)
    {
        return limitable is null ?
            false
            : limitable?.FitsIn(this);
    }

    #region Override methods
    public override IProportionLimitFactory GetFactory()
    {
        return Factory;
    }
    #endregion
    #endregion
}
