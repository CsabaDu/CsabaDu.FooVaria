namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal sealed class LimitedRate : Rate, ILimitedRate
{
    #region Constructors
    internal LimitedRate(ILimitedRate other) : base(other)
    {
        Limit = other.Limit;
        Factory = other.Factory;
    }

    internal LimitedRate(ILimitedRateFactory factory, IRate rate, ILimit limit) : base(factory, rate)
    {
        Limit = NullChecked(limit, nameof(limit));
        Factory = factory;
    }

    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, IDenominator denominator, ILimit limit) : base(factory, numerator, denominator)
    {
        Limit = NullChecked(limit, nameof(limit));
        Factory = factory;
    }
    #endregion

    #region Properties
    public ILimit Limit { get; init; }
    public ILimitedRateFactory Factory { get; init; }
    #endregion

    #region Public methods
    public bool Equals(ILimitedRate? x, ILimitedRate? y)
    {
        return base.Equals(x, y);
    }

    public int GetHashCode([DisallowNull] ILimitedRate other)
    {
        return base.GetHashCode(other);
    }

    public override ILimit? GetLimit()
    {
        return Limit;
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, string name, ValueType quantity, ILimit limit)
    {
        return Factory.Create(numerator, name, quantity, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, string name, ILimit limit)
    {
        return Factory.Create(numerator, name, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum denominatorMeasureUnit, ValueType quantity, ILimit limit)
    {
        return Factory.Create(numerator, denominatorMeasureUnit, quantity, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, MeasureUnitCode denominatorCode, ILimit limit)
    {
        return Factory.Create(numerator, denominatorCode, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement denominatorMeasurement, ILimit limit)
    {
        return Factory.Create(numerator, denominatorMeasurement, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit limit)
    {
        return Factory.Create(numerator, denominator, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, ILimit limit)
    {
        return GetLimitedRate(numerator, Denominator, limit);
    }

    public ILimitedRate GetLimitedRate(IRate rate, ILimit limit)
    {
        return Factory.Create(rate, limit);
    }

    public ILimitedRate GetNew(ILimitedRate other)
    {
        return Factory.CreateNew(other);
    }

    public bool? Includes(IBaseMeasure? limitable)
    {
        return Limit.Includes(limitable);
    }

    #region Override methods
    public override ILimitedRateFactory GetFactory()
    {
        return Factory;
    }

    public MeasureUnitCode GetLimiterMeasureUnitCode()
    {
        return Limit.GetMeasureUnitCode();
    }

    public decimal GetLimiterDefaultQuantity()
    {
        return Limit.GetDefaultQuantity();
    }

    public override ILimitedRate GetRate(IRate rate)
    {
        return (ILimitedRate)Factory.CreateNew(rate);
    }

    public override LimitMode? GetLimitMode()
    {
        return Limit.LimitMode;
    }
    #endregion
    #endregion
}
