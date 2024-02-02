﻿namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal sealed class LimitedRate : Rate, ILimitedRate
{
    #region Constructors
    internal LimitedRate(ILimitedRate other) : base(other)
    {
        Limit = other.Limit;
    }

    internal LimitedRate(ILimitedRateFactory factory, IRate rate, ILimit limit) : base(factory, rate)
    {
        Limit = NullChecked(limit, nameof(limit));
    }

    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode, ILimit limit) : base(factory, numerator, denominatorMeasureUnitCode)
    {
        Limit = NullChecked(limit, nameof(limit));
    }

    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, IMeasurement denominatorMeasurement, ILimit limit) : base(factory, numerator, denominatorMeasurement)
    {
        Limit = NullChecked(limit, nameof(limit));
    }

    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity, ILimit limit) : base(factory, numerator, denominatorMeasureUnit, denominatorQuantity)
    {
        Limit = NullChecked(limit, nameof(limit));
    }

    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, IDenominator denominator, ILimit limit) : base(factory, numerator, denominator)
    {
        Limit = NullChecked(limit, nameof(limit));
    }
    #endregion

    #region Properties
    public ILimit Limit { get ; init; }
    public LimitMode LimitMode
    {
        get => LimitMode;
        init => Limit.GetLimitMode();
    }
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
        return GetFactory().Create(numerator, name, quantity, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, string name, ILimit limit)
    {
        return GetFactory().Create(numerator, name, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum denominatorMeasureUnit, ValueType quantity, ILimit limit)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnit, quantity, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode, ILimit limit)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnitCode, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement denominatorMeasurement, ILimit limit)
    {
        return GetFactory().Create(numerator, denominatorMeasurement, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit limit)
    {
        return GetFactory().Create(numerator, denominator, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, ILimit limit)
    {
        return GetLimitedRate(numerator, Denominator, limit);
    }

    public ILimitedRate GetLimitedRate(IBaseRate baseRate, ILimit limit)
    {
        if (baseRate is IRate rate) return getLimitedRate();

        rate = GetRate(baseRate);

        return getLimitedRate();

        #region Local methods
        ILimitedRate getLimitedRate()
        {
            return GetFactory().Create(rate, limit);
        }
        #endregion
    }

    public ILimitedRate GetNew(ILimitedRate other)
    {
        return GetFactory().CreateNew(other);
    }

    public bool? Includes(IBaseMeasure? limitable)
    {
        return Limit.Includes(limitable);
    }

    #region Override methods
    public override ILimitedRateFactory GetFactory()
    {
        return (ILimitedRateFactory)Factory;
    }

    public MeasureUnitCode GetLimiterMeasureUnitCode()
    {
        return Limit.MeasureUnitCode;
    }

    public decimal GetLimiterDefaultQuantity()
    {
        return Limit.GetDefaultQuantity();
    }

    public LimitMode GetLimitMode(ILimiter limiter)
    {
        return NullChecked(limiter, nameof(limiter)).LimitMode;
    }
    #endregion
    #endregion
}
