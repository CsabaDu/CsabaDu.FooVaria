namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal abstract class Rate : BaseRate, IRate
{
    #region Constructors
    private protected Rate(IRate other) : base(other, nameof(other))
    {
        Numerator = other.Numerator;
        Denominator = other.Denominator;
    }

    protected Rate(IRateFactory factory, IRate rate) : base(factory, nameof(factory))
    {
        Numerator = NullChecked(rate, nameof(rate)).Numerator;
        Denominator = rate.Denominator;
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, nameof(factory))
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = NullChecked(denominator, nameof(denominator));
    }
    #endregion

    #region Properties
    public IMeasure Numerator { get; init; }
    public IDenominator Denominator { get; init; }
    public IBaseMeasure? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => Numerator,
        RateComponentCode.Denominator => Denominator,
        RateComponentCode.Limit => GetLimit(),

        _ => null,
    };
    #endregion

    #region Public methods
    public int CompareTo(IRate? other)
    {
        return base.CompareTo(other);
    }

    public IMeasure Denominate(IQuantifiable denominator)
    {
        const string paramName = nameof(denominator);
        MeasureUnitCode measureUnitCode = NullChecked(denominator, paramName).GetMeasureUnitCode();

        if (!HasMeasureUnitCode(measureUnitCode))
        {
            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
        }

        decimal divisor = Denominator.ProportionalTo(denominator);

        return Numerator.Divide(divisor);
    }

    public bool Equals(IRate? other)
    {
        return base.Equals(other);
    }

    public bool Equals(IRate? x, IRate? y)
    {
        if (x == null && y == null) return true;

        if (x == null || y == null) return false;

        if (!x.Equals(y)) return false;

        ILimit? xLimit = x.GetLimit();

        return xLimit?.Equals(xLimit, y.GetLimit()) == true;
    }

    //public IRate? ExchangeTo(IMeasurable? measurable)
    //{
    //    if (measurable is IMeasurement measureUnit) return exchangeToMeasurement(measureUnit);

    //    if (measurable is IBaseMeasure baseMeasure) return exchangeToBaseMeasure(baseMeasure);

    //    return null;

    //    #region Local methods
    //    IRate? exchangeToMeasurement(IMeasurement? measureUnit)
    //    {
    //        if (IsExchangeableTo(measureUnit)) return null;

    //        IDenominator denominator = Denominator.GetDenominator(measureUnit);
    //        decimal proportionQuantity = denominator.Measurement.ProportionalTo(measureUnit);

    //        return exchange(denominator, proportionQuantity);
    //    }

    //    IRate? exchangeToBaseMeasure(IBaseMeasure? baseMeasure)
    //    {
    //        if (baseMeasure?.IsExchangeableTo(GetMeasureUnitCode()) != true) return null;

    //        IDenominator denominator = Denominator.GetBaseMeasure(baseMeasure);
    //        decimal proportionQuantity = denominator.ProportionalTo(baseMeasure);

    //        return exchange(denominator, proportionQuantity);
    //    }

    //    IRate? exchange(IDenominator denominator, decimal proportionQuantity)
    //    {
    //        IMeasure numerator = Numerator.Divide(proportionQuantity);
    //        ILimit? limit = GetLimit();

    //        return limit == null ?
    //            GetRate(numerator, denominator)
    //            : GetRate(numerator, denominator, limit);
    //    }
    //    #endregion
    //}

    public int GetHashCode([DisallowNull] IRate rate)
    {
        return HashCode.Combine(rate.GetHashCode(), rate.GetLimit()?.GetHashCode());
    }

    public IRate GetRate(params IBaseMeasure[] rateComponents)
    {
        IRateFactory factory = (IRateFactory)GetFactory();

        return factory.Create(rateComponents);
    }

    public IBaseMeasure GetBaseMeasure(RateComponentCode rateComponentCode)
    {
        return GetRateComponent(rateComponentCode) ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }

    public bool IsExchangeableTo(Enum? context)
    {
        return Denominator.IsExchangeableTo(context);
    }

    public bool IsExchangeableTo(IMeasurable? measurable)
    {
        return measurable switch
        {
            BaseRate baseRate => base.IsExchangeableTo(baseRate),

            BaseMeasure or
            BaseMeasurement => measurable!.HasMeasureUnitCode(GetMeasureUnitCode()),

           _ => false,
        };
    }

    public decimal ProportionalTo(IRate? other)
    {
        return base.ProportionalTo(other);
    }

    public bool TryExchangeTo(Enum? measureUnit, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(measureUnit)) return false;

        IDenominator denominator = Denominator.GetDenominator(measureUnit!);
        decimal proportionQuantity = denominator.GetExchangeRate() / GetExchangeRate(measureUnit, nameof(measureUnit));

        exchanged = DivideRate(denominator, proportionQuantity);

        return exchanged != null;
    }

    public bool TryExchangeTo(IMeasurement? measurement, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(measurement)) return false;

        IDenominator denominator = Denominator.GetDenominator(measurement!);
        decimal proportionQuantity = denominator.Measurement.ProportionalTo(measurement);

        exchanged = DivideRate(denominator, proportionQuantity);

        return exchanged != null;
    }

    public bool TryExchangeTo(IBaseMeasure? baseMeasure, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(baseMeasure)) return false;

        IDenominator denominator = Denominator.GetBaseMeasure(baseMeasure!);
        decimal proportionQuantity = denominator.ProportionalTo(baseMeasure);

        exchanged = DivideRate(denominator, proportionQuantity);

        return exchanged != null;
    }

    public void ValidateDenominator(IQuantifiable denominator)
    {
        if (GetBaseMeasure(RateComponentCode.Denominator).Equals(denominator)) return;

        throw new ArgumentOutOfRangeException(nameof(denominator), denominator, null);
    }

    #region Override methods
    #region Sealed methods
    public override sealed decimal GetDefaultQuantity()
    {
        return Numerator.GetDefaultQuantity() / Denominator.GetDefaultQuantity();
    }

    public override sealed Enum GetBaseMeasureUnit()
    {
        return Numerator.GetBaseMeasureUnit();
    }

    public override sealed MeasureUnitCode GetDenominatorCode()
    {
        return Denominator.GetMeasureUnitCode();
    }

    public override sealed MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
    {
        return this[rateComponentCode]?.GetMeasureUnitCode() ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }

    public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return GetRateComponentCodes().Where(x => this[x] is not null).Select(GetMeasureUnitCode);
    }

    public override sealed MeasureUnitCode GetNumeratorCode()
    {
        return Numerator.GetMeasureUnitCode();
    }

    public override sealed IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[rateComponentCode];
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract ILimit? GetLimit();
    public abstract IRate GetRate(IRate rate);

    public bool IsExchangeableTo(IMeasurement? measurement)
    {
        return measurement?.IsExchangeableTo(GetMeasureUnitCode()) != true;
    }

    public bool IsExchangeableTo(IBaseMeasure? baseMeasure)
    {
        return baseMeasure?.IsExchangeableTo(GetMeasureUnitCode()) != true;
    }
    #endregion
    #endregion

    #region Private methods
    private IRate? DivideRate(IDenominator denominator, decimal proportionQuantity)
    {
        IMeasure numerator = Numerator.Divide(proportionQuantity);
        ILimit? limit = GetLimit();

        return limit == null ?
            GetRate(numerator, denominator)
            : GetRate(numerator, denominator, limit);
    }
    #endregion
}
