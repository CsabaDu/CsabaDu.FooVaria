namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class LimitedRate : Rate, ILimitedRate
{
    #region Constructors
    public LimitedRate(ILimitedRate limitedRate) : base(limitedRate)
    {
        Limit = limitedRate.Limit;
    }

    public LimitedRate(ILimitedRateFactory limitedRateFactory, IRate rate, ILimit? limit) : base(limitedRateFactory, rate)
    {
        Limit = GetOrCreateLimit(limitedRateFactory, limit);
    }

    public LimitedRate(ILimitedRateFactory limitedRateFactory, IMeasure numerator, IDenominator denominator, ILimit? limit) : base(limitedRateFactory, numerator, denominator)
    {
        Limit = GetOrCreateLimit(limitedRateFactory, limit);
    }

    public LimitedRate(ILimitedRateFactory limitedRateFactory, IMeasure numerator, Enum measureUnit, decimal? quantity, ILimit? limit) : base(limitedRateFactory, numerator, measureUnit, quantity)
    {
        Limit = GetOrCreateLimit(limitedRateFactory, limit);
    }

    public LimitedRate(ILimitedRateFactory limitedRateFactory, IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity, ILimit? limit) : base(limitedRateFactory, numerator, customName, measureUnitTypeCode, exchangeRate, quantity)
    {
        Limit = GetOrCreateLimit(limitedRateFactory, limit);
    }

    public LimitedRate(ILimitedRateFactory limitedRateFactory, IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity, ILimit? limit) : base(limitedRateFactory, numerator, measureUnit, exchangeRate, customName, quantity)
    {
        Limit = GetOrCreateLimit(limitedRateFactory, limit);
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
        return GetLimitedRateFactory().Create(numerator, name, quantity, GetLimit(limit));
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRateFactory().Create(numerator, measureUnit, quantity, GetLimit(limit));
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRateFactory().Create(numerator, measureUnit, exchangeRate, customName, quantity, GetLimit(limit));
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRateFactory().Create(numerator, customName, measureUnitTypeCode, exchangeRate, quantity, GetLimit(limit));
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null)
    {
        return GetLimitedRateFactory().Create(numerator, measurement, quantity, GetLimit(limit));
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null)
    {
        return GetLimitedRateFactory().Create(numerator, denominator ?? Denominator, GetLimit(limit));
    }

    public ILimitedRate GetLimitedRate(IRate rate, ILimit? limit = null)
    {
        return GetLimitedRateFactory().Create(rate, GetLimit(limit));
    }

    public ILimitedRate GetLimitedRate(ILimitedRate? other = null)
    {
        return GetLimitedRateFactory().Create(other ?? this);
    }

    public ILimitedRateFactory GetLimitedRateFactory()
    {
        return MeasurableFactory as ILimitedRateFactory ?? throw new InvalidOperationException(null);
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
    #endregion

    #region Private methods
    private ILimit GetOrCreateLimit(ILimitedRateFactory limitedRateFactory, ILimit? limit)
    {
        return limit ?? limitedRateFactory.LimitFactory.Create(Denominator);
    }

    private ILimit GetLimit(ILimit? limit)
    {
        return limit ?? Limit;
    }
    #endregion
}
