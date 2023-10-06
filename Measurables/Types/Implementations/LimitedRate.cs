﻿namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class LimitedRate : Rate, ILimitedRate
{
    #region Constructors
    public LimitedRate(ILimitedRate other) : base(other)
    {
        Limit = other.Limit;
    }

    public LimitedRate(ILimitedRateFactory factory, IMeasure numerator, IDenominator denominator, ILimit limit) : base(factory, numerator, denominator)
    {
        Limit = NullChecked(limit, nameof(limit));
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

    public ILimitedRate GetLimitedRate(IMeasure numerator, string name, decimal quantity, ILimit limit)
    {
        return GetFactory().Create(numerator, name, quantity, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, string name, ILimit limit)
    {
        return GetFactory().Create(numerator, name, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal quantity, ILimit limit)
    {
        return GetFactory().Create(numerator, measureUnit, quantity, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, ILimit limit)
    {
        return GetFactory().Create(numerator, measureUnit, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal quantity, ILimit limit)
    {
        return GetFactory().Create(numerator, measurement, quantity, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, ILimit limit)
    {
        return GetFactory().Create(numerator, measurement, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit limit)
    {
        return GetFactory().Create(numerator, denominator, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, ILimit limit)
    {
        return GetFactory().Create(numerator, Denominator, limit);
    }

    public ILimitedRate GetLimitedRate(IRate rate, ILimit limit)
    {
        return GetFactory().Create(rate, limit);
    }

    public ILimitedRate GetLimitedRate(ILimitedRate other)
    {
        return GetFactory().Create(other);
    }

    public LimitMode GetLimitMode(ILimitedRate limitedRate)
    {
        return NullChecked(limitedRate, nameof(limitedRate)).Limit.LimitMode;
    }

    public override ILimitedRate GetMeasurable(IMeasurable other)
    {
        return (ILimitedRate)GetFactory().Create(other);
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
    public override bool Equals(IRate? other)
    {
        return other is ILimitedRate
            && base.Equals(other);
    }

    public override ILimitedRateFactory GetFactory()
    {
        return (ILimitedRateFactory)Factory;
    }

    public override IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit)
    {
        return GetLimitedRate(numerator, denominator, limit ?? GetFactory().CreateLimit(denominator));
    }

    public override void Validate(ICommonBase? other)
    {
        Validate(this, other);
    }
    #endregion
    #endregion
}
