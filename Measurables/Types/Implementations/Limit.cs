using CsabaDu.FooVaria.Common.Enums;
using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Limit : BaseMeasure, ILimit
{
    #region Constants
    private const ulong DefaultLimitQuantity = default;
    #endregion

    #region Constructors
    internal Limit(ILimit limit) : base(limit)
    {
        Quantity = limit.Quantity;
        LimitMode = limit.LimitMode;
    }

    internal Limit(ILimitFactory limitFactory, ValueType? quantity, Enum measureUnit, LimitMode? limitMode) : base(limitFactory, quantity ?? DefaultLimitQuantity, measureUnit)
    {
        Quantity = GetLimitQuantity(quantity);
        LimitMode = GetValidLimitMode(limitMode);
    }

    internal Limit(ILimitFactory limitFactory, ValueType? quantity, IMeasurement measurement, LimitMode? limitMode) : base(limitFactory, quantity ?? DefaultLimitQuantity, measurement)
    {
        Quantity = GetLimitQuantity(quantity);
        LimitMode = GetValidLimitMode(limitMode);
    }

    internal Limit(ILimitFactory limitFactory, ValueType? quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, LimitMode? limitMode) : base(limitFactory, quantity ?? DefaultLimitQuantity, customName, measureUnitTypeCode, exchangeRate)
    {
        Quantity = GetLimitQuantity(quantity);
        LimitMode = GetValidLimitMode(limitMode);
    }

    internal Limit(ILimitFactory limitFactory, ValueType? quantity, Enum measureUnit, decimal exchangeRate, string customName, LimitMode? limitMode) : base(limitFactory, quantity ?? DefaultLimitQuantity, measureUnit, exchangeRate, customName)
    {
        Quantity = GetLimitQuantity(quantity);
        LimitMode = GetValidLimitMode(limitMode);
    }

    public Limit(ILimitFactory limitFactory, IBaseMeasure baseMeasure, LimitMode? limitMode) : base(limitFactory, baseMeasure) // TODO
    {
        Quantity = GetLimitQuantity(baseMeasure.GetQuantity());
        LimitMode = GetValidLimitMode(limitMode ?? baseMeasure.GetLimitMode());
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

        if (x.LimitMode != y.LimitMode) return false;

        return x.Equals(y);
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

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetLimit(measureUnit, exchangeRate, customName, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        return GetLimit(measurement ?? Measurement, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetLimit(measureUnitTypeCode, exchangeRate, customName, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
    {
        return GetLimit(name, quantity);
    }

    public override ValueType GetDefaultRateComponentQuantity()
    {
        return DefaultLimitQuantity;
    }

    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return HashCode.Combine(limit as IBaseMeasure, limit.LimitMode);
    }

    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    public ILimit GetLimit(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(measureUnit, exchangeRate, customName, quantity, limitMode);
    }

    public ILimit GetLimit(IMeasurement measurement, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(measurement, quantity, limitMode);
    }

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(baseMeasure, limitMode);
    }
    public ILimit GetLimit(ILimit? other = null)
    {
        return GetLimitFactory().Create(other ?? this);
    }

    public ILimit GetLimit(string name, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(name, quantity, limitMode);
    }

    public ILimit GetLimit(Enum measureUnit, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(measureUnit, quantity, limitMode);
    }

    public ILimit GetLimit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity = null, LimitMode? limitMode = null)
    {
        return GetLimitFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity, limitMode);
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

    public bool? Includes(IMeasure measure)
    {
        return NullChecked(measure, nameof(measure)).FitsIn(this);
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
