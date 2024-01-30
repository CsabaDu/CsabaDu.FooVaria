namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal sealed class FlatRate : Rate, IFlatRate
{
    #region Constructors
    internal FlatRate(IFlatRate other) : base(other)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IRate rate) : base(factory, rate)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode) : base(factory, numerator, denominatorMeasureUnitCode)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity) : base(factory, numerator, denominatorMeasureUnit, denominatorQuantity)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, IMeasurement denominatorMeasurement) : base(factory, numerator, denominatorMeasurement)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, numerator, denominator)
    {
    }
    #endregion

    #region Public methods
    public IFlatRate Add(IFlatRate? other)
    {
        return GetSum(other, SummingMode.Add);
    }

    public IFlatRate Divide(decimal divisor)
    {
        IMeasure numerator = Numerator.Divide(divisor);

        return GetFlatRate(numerator);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string name, ValueType quantity)
    {
        return GetFactory().Create(numerator, name, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string name)
    {
        return GetFactory().Create(numerator, name);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, Enum denominatorMeasureUnit, ValueType quantity)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnit, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IMeasurement denominatorMeasurement)
    {
        return GetFactory().Create(numerator, denominatorMeasurement);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnitCode);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator)
    {
        return GetFactory().Create(numerator, denominator);
    }

    public IFlatRate GetFlatRate(IMeasure numerator)
    {
        return GetFlatRate(numerator, Denominator);
    }

    public IFlatRate GetFlatRate(IBaseRate baseRate)
    {
        if (baseRate is IFlatRate flatRate) return GetNew(flatRate);

        if (baseRate is IRate rate) return getFlatRate();

        rate = GetRate(baseRate);

        return getFlatRate();

        #region Local methods
        IFlatRate getFlatRate()
        {
            return GetFactory().Create(rate);
        }
        #endregion
    }

    public IFlatRate GetNew(IFlatRate other)
    {
        return GetFactory().CreateNew(other);
    }

    public IFlatRate Multiply(decimal multiplier)
    {
        IMeasure numerator = Numerator.Multiply(multiplier);

        return GetFlatRate(numerator);
    }

    public IFlatRate Subtract(IFlatRate? other)
    {
        return GetSum(other, SummingMode.Subtract);
    }

    #region Override methods
    public override ILimit? GetLimit()
    {
        return null;
    }

    public override IFlatRateFactory GetFactory()
    {
        return (IFlatRateFactory)Factory;
    }
    #endregion
    #endregion

    #region Private methods
    private IFlatRate GetSum(IFlatRate? other, SummingMode summingMode)
    {
        if (other == null) return GetFlatRate(this);

        if (!other.TryExchangeTo(Denominator, out IRate? exchanged))
        {
            throw InvalidMeasureUnitCodeEnumArgumentException(other!.MeasureUnitCode, nameof(other));
        }

        IMeasure numerator = getNumeratorSum(summingMode);

        return GetFlatRate(numerator);

        #region Local methods
        IMeasure getNumeratorSum(SummingMode summingMode)
        {
            return summingMode switch
            {
                SummingMode.Add => Numerator.Add(exchanged!.Numerator),
                SummingMode.Subtract => Numerator.Subtract(exchanged!.Numerator),

                _ => throw new InvalidOperationException(null),
            };
        }
        #endregion
    }

    public IFlatRate ConvertToLimitable(ILimiter limiter)
    {
        string paramName = nameof(limiter);

        if (NullChecked(limiter, paramName) is IBaseRate baseRate) return (IFlatRate)GetRate(baseRate);

        throw ArgumentTypeOutOfRangeException(paramName, limiter);
    }

    //public bool? FitsIn(ILimiter? limiter)
    //{
    //    if (limiter == null) return null;

    //    LimitMode limitMode = limiter.LimitMode;
    //    IBaseRate baseRate = ConvertToLimitable(limiter);

    //    return FitsIn(baseRate, limitMode);
    //}

    public bool? FitsIn(IBaseRate? baseRate, LimitMode? limitMode)
    {
        bool limitModeHasValue = limitMode.HasValue;

        if (baseRate == null && !limitModeHasValue) return true;

        if (!IsExchangeableTo(baseRate)) return null;

        if (!limitModeHasValue) return CompareTo(baseRate) <= 0;

        _ = Defined(limitMode!.Value, nameof(limitMode));

        MeasureUnitCode measureUnitCode = baseRate!.GetNumeratorMeasureUnitCode();
        Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        ValueType quantity = baseRate.GetDefaultQuantity();
        IBaseMeasure baseMeasure = Numerator.GetBaseMeasure(measureUnit, quantity);

        return Numerator.FitsIn(baseMeasure, limitMode);
    }
    #endregion
}
