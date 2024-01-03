namespace CsabaDu.FooVaria.Rates.Types.Implementations;

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

    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode, ILimit limit) : base(factory, numerator, denominatorMeasureUnitTypeCode)
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
    #endregion

    #region Public methods
    public bool Equals(ILimitedRate? x, ILimitedRate? y)
    {
        if (x == null && y == null) return true;

        if (x == null || y == null) return false;

        ILimit xLimit = x.Limit;

        if (!xLimit.Equals(xLimit, y.Limit)) return false;

        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] ILimitedRate other)
    {
        return other.GetHashCode();
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

    public ILimitedRate GetLimitedRate(IMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode, ILimit limit)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnitTypeCode, limit);
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

    public LimitMode GetLimitMode(ILimitedRate limitedRate)
    {
        ILimit limit = NullChecked(limitedRate, nameof(limitedRate)).Limit;

        return limit.LimitMode;
    }

    public ILimitedRate GetNew(ILimitedRate other)
    {
        return GetFactory().CreateNew(other);
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
    public override bool Equals(object? obj)
    {
        return base.Equals(obj)
            && obj is ILimitedRate limitedRate
            && limitedRate.Limit.Equals(Limit);
    }

    public override ILimitedRateFactory GetFactory()
    {
        return (ILimitedRateFactory)Factory;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator, Limit);
    }
    #endregion
    #endregion
}

//{
//    #region Constructors
//    public LimitedRate(ILimitedRate obj) : base(obj)
//    {
//        Limit = obj.Limit;
//    }

//    public LimitedRate(ILimitedRateFactory factory, IMeasure numerator, IDenominator denominator, ILimit limit) : base(factory, numerator, denominator)
//    {
//        Limit = NullChecked(limit, nameof(limit));
//    }

//    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, Enum measureUnit, ILimit limit) : base(factory, numerator, measureUnit)
//    {
//        Limit = NullChecked(limit, nameof(limit));
//    }

//    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, MeasureUnitTypeCode measureUnitTypeCode, ILimit limit) : base(factory, numerator, measureUnitTypeCode)
//    {
//        Limit = NullChecked(limit, nameof(limit));
//    }

//    internal LimitedRate(ILimitedRateFactory factory, IMeasure numerator, IMeasurement measurement, ILimit limit) : base(factory, numerator, measurement)
//    {
//        Limit = NullChecked(limit, nameof(limit));
//    }
//    #endregion

//    #region Properties
//    public ILimit Limit { get; init; }
//    #endregion

//    #region Public methods
//    public bool Equals(ILimitedRate? x, ILimitedRate? y)
//    {
//        if (x == null && y == null) return true;

//        if (x == null || y == null) return false;

//        if (!x.Equals(y)) return false;

//        ILimit xLimit = x.Limit;

//        return xLimit.Equals(xLimit, y.Limit);
//    }

//    public int GetHashCode([DisallowNull] ILimitedRate limitedRate)
//    {
//        return HashCode.Combine(limitedRate as IRate, limitedRate.Limit);
//    }

//    public ILimitedRate GetLimitedRate(IMeasure numerator, string name, decimal quantity, ILimit limit)
//    {
//        return GetFactory().CreateNew(numerator, name, quantity, limit);
//    }

//    public ILimitedRate GetLimitedRate(IMeasure numerator, string name, ILimit limit)
//    {
//        return GetFactory().CreateNew(numerator, name, limit);
//    }

//    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, decimal quantity, ILimit limit)
//    {
//        return GetFactory().CreateNew(numerator, measureUnit, quantity, limit);
//    }

//    public ILimitedRate GetLimitedRate(IMeasure numerator, Enum measureUnit, ILimit limit)
//    {
//        return GetFactory().CreateNew(numerator, measureUnit, limit);
//    }

//    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, decimal quantity, ILimit limit)
//    {
//        return GetFactory().CreateNew(numerator, measurement, quantity, limit);
//    }

//    public ILimitedRate GetLimitedRate(IMeasure numerator, IMeasurement measurement, ILimit limit)
//    {
//        return GetFactory().CreateNew(numerator, measurement, limit);
//    }

//    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit limit)
//    {
//        return GetFactory().CreateNew(numerator, denominator, limit);
//    }

//    public ILimitedRate GetLimitedRate(IMeasure numerator, ILimit limit)
//    {
//        return GetFactory().CreateNew(numerator, Denominator, limit);
//    }

//    public ILimitedRate GetLimitedRate(IRate rate, ILimit limit)
//    {
//        return GetFactory().CreateNew(rate, limit);
//    }

//    public ILimitedRate GetLimitedRate(ILimitedRate obj)
//    {
//        return GetFactory().CreateNew(obj);
//    }

//    public LimitMode GetLimitMode(ILimitedRate limitedRate)
//    {
//        return NullChecked(limitedRate, nameof(limitedRate)).Limit.LimitMode;
//    }

//    public bool? Includes(IMeasure measure)
//    {
//        return Limit.Includes(measure);
//    }

//    public void ValidateLimitMode(LimitMode limitMode)
//    {
//        Limit.ValidateLimitMode(limitMode);
//    }

//    #region Override methods
//    public override bool Equals(IBaseRate? obj)
//    {
//        return obj is ILimitedRate limitedRate
//            && base.Equals(obj)
//            && Limit.Equals(limitedRate.Limit);
//    }

//    public override ILimitedRateFactory GetFactory()
//    {
//        return (ILimitedRateFactory)Factory;
//    }

//    public override ILimit? GetLimit()
//    {
//        return Limit;
//    }

//    //public override ILimitedRate GetMeasurable(IDefaultMeasurable obj)
//    //{
//    //    return (ILimitedRate)GetFactory().CreateNew(obj);
//    //}

//    public override ILimitedRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit)
//    {
//        return GetLimitedRate(numerator, denominator, limit ?? GetFactory().CreateLimit(denominator));
//    }

//    public override void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName)
//    {
//        throw new NotImplementedException();
//    }
//    #endregion
//    #endregion
//}
