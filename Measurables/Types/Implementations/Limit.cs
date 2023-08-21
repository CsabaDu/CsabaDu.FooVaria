namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Limit : BaseMeasure, ILimit
{
    #region Private constants
    private const ulong DefaultLimitQuantity = default;
    #endregion

    #region Constructors
    public Limit(ILimit limit) : base(limit)
    {
        Quantity = limit.Quantity;
        LimitMode = limit.LimitMode;
    }

    public Limit(IBaseMeasureFactory baseMeasureFactory, ValueType? quantity, Enum measureUnit, LimitMode? limitMode) : base(baseMeasureFactory, quantity ?? DefaultLimitQuantity, measureUnit)
    {
        Quantity = GetLimitQuantity(quantity);
        LimitMode = GetValidLimitMode(LimitMode);
    }

    public Limit(IBaseMeasureFactory baseMeasureFactory, ValueType? quantity, IMeasurement measurement, LimitMode? limitMode) : base(baseMeasureFactory, quantity ?? DefaultLimitQuantity, measurement)
    {
        Quantity = GetLimitQuantity(quantity);
        LimitMode = GetValidLimitMode(LimitMode);
    }

    public Limit(IBaseMeasureFactory baseMeasureFactory, ValueType? quantity, MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName) : base(baseMeasureFactory, quantity ?? DefaultLimitQuantity, customMeasureUnitTypeCode, exchangeRate, customName)
    {
        Quantity = GetLimitQuantity(quantity);
        LimitMode = GetValidLimitMode(LimitMode);
    }

    public Limit(IBaseMeasureFactory baseMeasureFactory, ValueType? quantity, Enum measureUnit, decimal exchangeRate, string? customName) : base(baseMeasureFactory, quantity ?? DefaultLimitQuantity, measureUnit, exchangeRate, customName)
    {
        Quantity = GetLimitQuantity(quantity);
        LimitMode = GetValidLimitMode(LimitMode);
    }
    #endregion

    #region Properties
    public LimitMode LimitMode { get; init; }
    public override object Quantity { get; init; }
    public override TypeCode QuantityTypeCode => TypeCode.UInt64;
    #endregion

    #region Public methods
    public bool Equals(ILimit? x, ILimit? y)
    {
        if (x == null && y == null) return true;

        if (x == null || y == null) return false;

        if (!x.Equals(y)) return false;

        return x.LimitMode == y.LimitMode;
    }

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
        return GetLimit(measureUnit, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName = null)
    {
        return GetLimit(measureUnit, exchangeRate, customName, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        return GetLimit(measurement ?? Measurement, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null)
    {
        return GetLimit(measureUnitTypeCode, exchangeRate, customName, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
    {
        return GetLimit(name, quantity);
    }

    public override IBaseMeasure GetDefault(RateComponentCode rateComponentCode, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        throw new NotImplementedException();
    }

    public override IMeasurable GetDefault()
    {
        throw new NotImplementedException();
    }

    public override ValueType GetDefaultRateComponentQuantity()
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return HashCode.Combine(limit.GetHashCode(), limit.LimitMode);
    }

    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    public ILimit GetLimit(Enum measureUnit, decimal exchangeRate, string? customName = null, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(measureUnit, exchangeRate, customName, quantity, limitMode);
    }

    public ILimit GetLimit(IMeasurement measurement, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(measurement, quantity, limitMode);
    }

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode? limitMode = null)
    {
        throw new NotImplementedException();
    }

    public ILimit GetLimit(ILimit? other = null)
    {
        throw new NotImplementedException();
    }

    public ILimit GetLimit(string name, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(name, quantity, limitMode);
    }

    public ILimit GetLimit(Enum measureUnit, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(measureUnit, quantity, limitMode);
    }

    public ILimit GetLimit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(measureUnitTypeCode, exchangeRate, customName, quantity, limitMode);
    }

    public ILimitFactory GetLimitFactory()
    {
        return MeasurableFactory as ILimitFactory ?? throw new InvalidOperationException(null);
    }

    public override LimitMode? GetLimitMode()
    {
        return GetLimitMode(this);
    }
    public LimitMode GetLimitMode(ILimit? limit = null)
    {
        return (limit ?? this).LimitMode;
    }

    public override ValueType GetQuantity(ValueType? quantity = null)
    {
        quantity = base.GetQuantity(quantity);

        if ((ulong)quantity < 0) throw QuantityArgumentOutOfRangeException(quantity);

        return quantity;
    }

    public bool? Includes(IMeasure measure)
    {
        return measure.FitsIn(this);
    }

    public override bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure)
    {
        throw new NotImplementedException();
    }

    public void ValidateLimitMode(LimitMode limitMode)
    {
        if (!Enum.IsDefined(limitMode)) throw InvalidLimitModeEnumArgumentException(limitMode);
    }
    #endregion

    #region Private methods
    private ValueType GetLimitQuantity(ValueType? quantity)
    {
        return GetQuantity(quantity ?? DefaultLimitQuantity);
    }

    private LimitMode GetValidLimitMode(LimitMode? limitMode)
    {
        limitMode ??= default;

        ValidateLimitMode(limitMode.Value);

        return limitMode.Value;
    }
    #endregion
}
