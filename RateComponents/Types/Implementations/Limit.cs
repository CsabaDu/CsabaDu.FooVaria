
namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Limit : RateComponent<ILimit, ulong>, ILimit
{
    #region Constructors
    internal Limit(ILimitFactory factory, IMeasurement measurement, ulong quantity, LimitMode limitMode) : base(factory, measurement)
    {
        Quantity = quantity;

        ValidateLimitMode(limitMode);

        LimitMode = limitMode;
    }
    #endregion

    #region Properties
    public LimitMode LimitMode { get; init; }

    #region Override properties
    public override object Quantity { get; init; }
    #endregion
    #endregion

    #region Public methods
    public bool Equals(ILimit? x, ILimit? y)
    {
        return base.Equals(x, y);
    }

    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return base.GetHashCode(limit);
    }

    public MeasureUnitCode GetLimiterMeasureUnitCode()
    {
        return MeasureUnitCode;
    }

    public ILimit? GetLimit(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode)
    {
        return GetFactory().Create(measureUnit, exchangeRate, quantity, customName, limitMode);
    }

    public ILimit GetLimit(IMeasurement measurement, ulong quantity, LimitMode limitMode)
    {
        return GetFactory().Create(measurement, quantity, limitMode);
    }

    public ILimit GetLimit(IRateComponent baseMeasure, LimitMode limitMode)
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

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        throw new NotImplementedException();
    }
    #endregion
    #endregion
}
