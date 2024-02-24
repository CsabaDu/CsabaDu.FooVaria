namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Limit(ILimitFactory factory, IMeasurement measurement, ulong quantity, LimitMode limitMode) : RateComponent<ILimit>(factory, measurement), ILimit
{
    #region Properties
    public LimitMode LimitMode { get; init; } = Defined(limitMode, nameof(limitMode));

    #region Override properties
    public override object Quantity { get; init; } = quantity;
    #endregion
    #endregion

    #region Public methods
    public bool Equals(ILimit? x, ILimit? y)
    {
        return base.Equals(x, y);
    }

    public ILimit GetBaseMeasure(ulong quantity)
    {
        return GetRateComponent(quantity);
    }

    public override ILimit GetDefault()
    {
        return GetDefault(this);
    }

    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return base.GetHashCode(limit);
    }

    public MeasureUnitCode GetLimiterMeasureUnitCode()
    {
        return GetMeasureUnitCode();
    }

    public ILimit? GetLimit(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode)
    {
        return GetFactory().Create(measureUnit, exchangeRate, quantity, customName, limitMode);
    }

    public ILimit GetLimit(IMeasurement measurement, ulong quantity, LimitMode limitMode)
    {
        return GetFactory().Create(measurement, quantity, limitMode);
    }

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        return GetFactory().Create(baseMeasure, limitMode);
    }

    public ILimit GetLimit(ILimit other)
    {
        return GetFactory().CreateNew(other);
    }

    public ILimit GetLimit(ulong quantity)
    {
        return GetFactory().Create(this, quantity);
    }

    public ILimit GetLimit(string name, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(name, quantity, limitMode);
    }

    public ILimit GetLimit(Enum measureUnit, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(measureUnit, quantity, limitMode);
    }

    public ILimit? GetLimit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(customName, measureUnitCode, exchangeRate, quantity, limitMode);
    }

    public decimal GetLimiterDefaultQuantity()
    {
        return GetDefaultQuantity();
    }

    public ILimit GetNew(ILimit other)
    {
        return GetFactory().CreateNew(other);
    }

    public ulong GetQuantity()
    {
        return (ulong)Quantity;
    }

    public bool? Includes(IBaseMeasure? limitable)
    {
        return limitable?.FitsIn(this) ?? false;
    }

    #region Override methods
    public override decimal GetDefaultQuantity()
    {
        return GetDefaultQuantity(Quantity);
    }

    public override ILimitFactory GetFactory()
    {
        return (ILimitFactory)Factory;
    }

    public override LimitMode? GetLimitMode()
    {
        return LimitMode;
    }
    #endregion
    #endregion
}
