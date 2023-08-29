namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class LimitedRate : Rate, ILimitedRate
{
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

    public ILimit Limit { get; init; }

    public bool Equals(ILimitedRate? x, ILimitedRate? y)
    {
        if (x == null && y == null) return true;

        if (x == null || y == null) return false;

        if (!x.Limit.Equals(y.Limit)) return false;

        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] ILimitedRate limitedRate)
    {
        return HashCode.Combine(limitedRate as IRate, limitedRate.Limit);
    }

    public override ILimit? GetLimit()
    {
        return Limit;
    }
    public ILimitedRate GetLimitedRate(IMeasure numerator, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCoce, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public ILimitedRate GetLimitedRate(IRate rate, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public ILimitedRate GetLimitedRate(ILimitedRate? other = null)
    {
        throw new NotImplementedException();
    }

    public ILimitedRateFactory GetLimitedRateFactory()
    {
        throw new NotImplementedException();
    }

    public LimitMode GetLimitMode(ILimitedRate? limitedRate = null)
    {
        return Limit.GetLimitMode((limitedRate ?? this).Limit);
    }

    public override IRate GetRate(IMeasure numerator, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public override IRate GetRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public override IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public override IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public override IRate GetRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public override IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null)
    {
        throw new NotImplementedException();
    }

    public bool? Includes(IMeasure measure)
    {
        return Limit.Includes(measure);
    }

    public void ValidateLimitMode(LimitMode limitMode)
    {
        Limit.ValidateLimitMode(limitMode);
    }

    #region Private methods
    private ILimit GetOrCreateLimit(ILimitedRateFactory limitedRateFactory, ILimit? limit)
    {
        return limit ?? limitedRateFactory.LimitFactory.Create(Denominator);
    }
    #endregion
}
