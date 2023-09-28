namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class LimitedRate : Rate, ILimitedRate
{
    #region Constructors
    public LimitedRate(ILimitedRate other, ILimit? limit) : base(other)
    {
        Limit = limit ?? other.Limit;
    }

    public LimitedRate(ILimitedRateFactory factory, IMeasure numerator, IDenominator denominator, ILimit? limit) : base(factory, numerator, denominator)
    {
        Limit = GetOrCreateLimit(limit);
    }
    #endregion

    #region Properties
    public ILimit Limit { get; init; }
    #endregion

    #region Public methods
    public bool Equals(ILimitedRate? x, ILimitedRate? y)
    {
        if (x == null && y == null) return true;

        if (x == null || y == null) return false;

        if (!x.Equals(y)) return false;

        ILimit xLimit = x.Limit;

        return xLimit.Equals(xLimit, y.Limit);
    }

    public int GetHashCode([DisallowNull] ILimitedRate limitedRate)
    {
        return HashCode.Combine(limitedRate as IRate, limitedRate.Limit);
    }

    public override ILimit? GetLimit()
    {
        return Limit;
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, string name, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFactory().Create(numerator, name, quantity, limit ?? Limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFactory().Create(numerator, measureUnit, quantity, limit ?? Limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFactory().Create(numerator, measureUnit, exchangeRate, customName, quantity, limit ?? Limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFactory().Create(numerator, customName, measureUnitTypeCode, exchangeRate, quantity, limit ?? Limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFactory().Create(numerator, measurement, quantity, limit ?? Limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null)
    {
        return GetFactory().Create(numerator, denominator ?? Denominator, limit ?? Limit);
    }

    public ILimitedRate GetLimitedRate(IRate rate, ILimit? limit = null)
    {
        return GetFactory().Create(rate, limit ?? Limit);
    }

    public ILimitedRate GetLimitedRate(ILimitedRate? other = null, ILimit? limit = null)
    {
        return GetFactory().Create(other ?? this, limit ?? other?.Limit);
    }

    //public ILimitedRateFactory GetFactory()
    //{
    //    return Factory as ILimitedRateFactory ?? throw new InvalidOperationException(null);
    //}

    public ILimit GetOrCreateLimit(ILimit? limit)
    {
        return limit ?? GetFactory().LimitFactory.Create(Denominator);
    }

    public LimitMode GetLimitMode(ILimitedRate? limitedRate = null)
    {
        return Limit.GetLimitMode((limitedRate ?? this).Limit);
    }

    public override IRate GetRate(IMeasure numerator, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRate(numerator, customName, quantity, limit);
    }

    public override IRate GetRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRate(numerator, measureUnit, quantity, limit);
    }

    public override IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRate(numerator, measureUnit, exchangeRate, customName, quantity, limit);
    }

    public override IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRate(numerator, customName, measureUnitTypeCode, exchangeRate, quantity, limit);
    }

    public override IRate GetRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRate(numerator, measurement, quantity, limit);
    }

    public override IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null)
    {
        return GetLimitedRate(numerator, denominator, limit);
    }

    public bool? Includes(IMeasure measure)
    {
        return Limit.Includes(measure);
    }

    public void ValidateLimitMode(LimitMode limitMode)
    {
        Limit.ValidateLimitMode(limitMode);
    }

    #region Override methods
    public override ILimitedRateFactory GetFactory()
    {
        return (ILimitedRateFactory)Factory;
    }
    #endregion
    #endregion
}
