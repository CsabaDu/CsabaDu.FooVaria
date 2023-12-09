namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Limit : RateComponent, ILimit
{
    #region Constructors
    internal Limit(ILimitFactory factory, IMeasurement measurement, ValueType quantity, LimitMode limitMode) : base(factory, measurement, quantity)
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

    public ILimit GetDefault()
    {
        return GetFactory().CreateDefault(MeasureUnitTypeCode);
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

    public ILimit GetLimit(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode)
    {
        return GetFactory().Create(measureUnit, exchangeRate, quantity, customName, limitMode);
    }

    public ILimit GetLimit(IMeasurement measurement, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(measurement, quantity, limitMode);
    }

    public ILimit GetLimit(IRateComponent baseMeasure, LimitMode limitMode)
    {
        return GetFactory().Create(baseMeasure, limitMode);
    }

    public ILimit GetLimit(ILimit other)
    {
        return GetFactory().Create(other);
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
    public override bool Equals(IRateComponent? other)
    {
        return other is ILimit
            && Equals(this, other);
    }

    public override ILimitFactory GetFactory()
    {
        return (ILimitFactory)Factory;
    }

    public override LimitMode? GetLimitMode()
    {
        return LimitMode;
    }

    public override TypeCode? GetQuantityTypeCode(object quantity)
    {
        if (quantity is IQuantity<ulong> limit) return Quantifiable.GetQuantityTypeCode(limit);

        return base.GetQuantityTypeCode(quantity);
    }

    public override TypeCode GetQuantityTypeCode()
    {
        return Quantifiable.GetQuantityTypeCode(this);
    }

    public ILimit GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetDefault();
    }

    public override void Validate(IRootObject? rootObject, string paramName)
    {
        Validate(this, rootObject, validateLimit, paramName);

        #region Local methods
        void validateLimit()
        {
            ValidateBaseMeasure(this, rootObject!, paramName);
        }
        #endregion
    }

    public ILimit GetRateComponent(IRateComponent rateComponent)
    {
        LimitMode limitMode = rateComponent.GetLimitMode() ?? default;

        return GetLimit(rateComponent, limitMode);
    }

    public ILimit GetNew(ILimit other)
    {
        return GetFactory().Create(other);
    }
    #endregion
    #endregion
}
