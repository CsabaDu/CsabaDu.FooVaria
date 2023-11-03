namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Limit : BaseMeasure, ILimit
{
    #region Constructors
    internal Limit(ILimitFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
        LimitMode = default;
    }

    internal Limit(ILimitFactory factory, ValueType quantity, IMeasurement measurement, LimitMode limitMode) : base(factory, quantity, measurement)
    {
        ValidateLimitMode(limitMode);

        LimitMode = limitMode;
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

    public ILimit GetDefaultRateComponent()
    {
        return GetDefault(this);
    }

    public ulong GetDefaultRateComponentQuantity()
    {
        return GetDefaultRateComponentQuantity<ulong>();
    }

    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return HashCode.Combine(limit.DefaultQuantity, limit.MeasureUnitTypeCode, limit.LimitMode);
    }

    public Enum GetMeasureUnit()
    {
        return Measurement.GetMeasureUnit();
    }

    public ILimit GetLimit(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(measureUnit, exchangeRate, customName, quantity, limitMode);
    }

    public ILimit GetLimit(IMeasurement measurement, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(measurement, quantity, limitMode);
    }

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        return GetFactory().Create(baseMeasure, limitMode);
    }

    public ILimit GetLimit(ILimit other)
    {
        return (ILimit)GetMeasurable(other);
    }

    public ILimit GetLimit(ValueType quantity)
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

    public ILimit GetLimit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity, limitMode);
    }

    public LimitMode GetLimitMode(ILimit limit)
    {
        return NullChecked(limit, nameof(limit)).LimitMode;
    }

    public ulong GetQuantity()
    {
        return (ulong)Quantity;
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

    #region Override methods
    public override bool Equals(IBaseMeasure? other)
    {
        return other is ILimit
            && base.Equals(other);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
    {
        return GetLimit(measureUnit, quantity, default);
    }

    public override ILimitFactory GetFactory()
    {
        return (ILimitFactory)Factory;
    }

    public override LimitMode? GetLimitMode()
    {
        return LimitMode;
    }

    public override TypeCode GetQuantityTypeCode()
    {
        return TypeCode.UInt64;
    }

    public override ILimit GetDefault()
    {
        return GetDefaultRateComponent();
    }

    public override ILimit GetMeasurable(IMeasurable other)
    {
        return (ILimit)GetFactory().Create(other);
    }

    public override void Validate(IFooVariaObject? fooVariaObject, string paramName)
    {
        ValidateCommonBaseAction = () => ValidateBaseMeasure(this, fooVariaObject!, paramName);

        Validate(this, fooVariaObject, paramName);
    }
    #endregion
    #endregion
}
