namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Limit : BaseMeasure, ILimit
{
    #region Constants
    private const ulong DefaultRateComponentQuantity = default;
    #endregion

    #region Constructors
    internal Limit(ILimit other) : base(other)
    {
        LimitMode = other.LimitMode;
    }

    internal Limit(ILimit other, ValueType quantity) : base(other, quantity)
    {
        LimitMode = other.LimitMode;
    }

    internal Limit(ILimitFactory factory, ValueType quantity, IMeasurement measurement, LimitMode limitMode) : base(factory, quantity, measurement)
    {
        LimitMode = GetValidLimitMode(limitMode);
    }
    #endregion

    #region Properties
    public LimitMode LimitMode { get; init; }
    #endregion

    #region Public methods
    public bool Equals(ILimit? x, ILimit? y)
    {
        if (x == null && y == null) return true;

        if (x == null || y == null) return false;

        if (x.LimitMode != y.LimitMode) return false;

        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return HashCode.Combine(limit as IBaseMeasure, limit.LimitMode);
    }

    public ILimit GetLimit(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity, LimitMode limitMode)
    {
        return GetLimitFactory().Create(measureUnit, exchangeRate, customName, quantity, limitMode);
    }

    public ILimit GetLimit(IMeasurement measurement, ValueType quantity, LimitMode limitMode)
    {
        return GetLimitFactory().Create(measurement, quantity, limitMode);
    }

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        return GetLimitFactory().Create(baseMeasure, limitMode);
    }

    public ILimit GetLimit(ILimit other)
    {
        return (ILimit)GetMeasurable(other);
    }

    public ILimit GetLimit(ValueType quantity)
    {
        return GetLimitFactory().Create(this, quantity);
    }

    public ILimit GetLimit(string name, ValueType quantity, LimitMode limitMode)
    {
        return GetLimitFactory().Create(name, quantity, limitMode);
    }

    public ILimit GetLimit(Enum measureUnit, ValueType quantity, LimitMode limitMode)
    {
        return GetLimitFactory().Create(measureUnit, quantity, limitMode);
    }

    public ILimit GetLimit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode)
    {
        return GetLimitFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity, limitMode);
    }

    public ILimitFactory GetLimitFactory()
    {
        return MeasurableFactory as ILimitFactory ?? throw new InvalidOperationException(null);
    }

    public LimitMode GetLimitMode(ILimit limit)
    {
        return NullChecked(limit, nameof(limit)).LimitMode;
    }

    public bool? Includes(IMeasure measure)
    {
        return NullChecked(measure, nameof(measure)).FitsIn(this);
    }

    public void ValidateLimitMode(LimitMode limitMode)
    {
        if (Enum.IsDefined(limitMode)) return;
        
        throw InvalidLimitModeEnumArgumentException(limitMode);
    }

    #region Overriden methods
    public override bool Equals(IBaseMeasure? other)
    {
        return Equals(this, other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ILimit limit && Equals(limit);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
    {
        return GetLimit(measureUnit, quantity, default);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetLimit(measureUnit, exchangeRate, customName, quantity, default);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement measurement)
    {
        return GetLimit(measurement, quantity, default);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetLimit(measureUnitTypeCode, exchangeRate, customName, quantity, default);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
    {
        return GetLimit(name, quantity, default);
    }

    public override ValueType GetDefaultRateComponentQuantity()
    {
        return DefaultRateComponentQuantity;
    }

    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    public override LimitMode? GetLimitMode()
    {
        return LimitMode;
    }

    public override TypeCode GetQuantityTypeCode()
    {
        return TypeCode.UInt64;
    }
    #endregion
    #endregion

    #region Private methods
    private LimitMode GetValidLimitMode(LimitMode limitMode)
    {
        ValidateLimitMode(limitMode);

        return limitMode;
    }
    #endregion
}
