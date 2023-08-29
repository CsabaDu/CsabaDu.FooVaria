namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class LimitedRate : Rate, ILimitedRate
{
    public LimitedRate(ILimitedRate limitedRate) : base(limitedRate)
    {
        Limit = limitedRate.Limit;
    }

    public LimitedRate(IFlatRateFactory rateFactory, IRate rate, ILimit? limit) : base(rateFactory, rate)
    {
    }

    public LimitedRate(IRateFactory rateFactory, IMeasure numerator, IDenominator denominator, ILimit? limit) : base(rateFactory, numerator, denominator)
    {
    }

    public LimitedRate(IRateFactory rateFactory, IMeasure numerator, Enum measureUnit, decimal? quantity, ILimit? limit) : base(rateFactory, numerator, measureUnit, quantity)
    {
    }

    public LimitedRate(IRateFactory rateFactory, IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity, ILimit? limit) : base(rateFactory, numerator, customName, measureUnitTypeCode, exchangeRate, quantity)
    {
    }

    public LimitedRate(IRateFactory rateFactory, IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity, ILimit? limit) : base(rateFactory, numerator, measureUnit, exchangeRate, customName, quantity)
    {
    }

    public ILimit Limit { get; init; }

    public bool Equals(ILimitedRate? x, ILimitedRate? y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] ILimitedRate obj)
    {
        throw new NotImplementedException();
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

    public LimitMode GetLimitMode(ILimitedRate? limiter = null)
    {
        throw new NotImplementedException();
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

    public bool? Includes(IMeasure limitable)
    {
        throw new NotImplementedException();
    }

    public void ValidateLimitMode(LimitMode limitMode)
    {
        throw new NotImplementedException();
    }
}
