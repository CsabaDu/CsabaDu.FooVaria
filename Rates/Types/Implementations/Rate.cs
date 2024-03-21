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

    public IMeasure Denominate(IMeasurable denominator)
    {
        ValidateDenominator(denominator, nameof(denominator));

        decimal divisor = denominator switch
        {
            BaseMeasurement baseMeasurement => Denominator.Measurement.ProportionalTo(baseMeasurement) * Denominator.GetDefaultQuantity(),
            Quantifiable quantifiable => Denominator.ProportionalTo(quantifiable),

            _ => throw new InvalidOperationException(null),
        };

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
    //    if (measurable is IMeasurement context) return exchangeToMeasurement(context);

    //    if (measurable is IBaseMeasure baseMeasure) return exchangeToBaseMeasure(baseMeasure);

    //    return null;

    //    #region Local methods
    //    IRate? exchangeToMeasurement(IMeasurement? context)
    //    {
    //        if (IsExchangeableTo(context)) return null;

    //        IDenominator denominator = Denominator.GetDenominator(context);
    //        decimal proportionQuantity = denominator.Measurement.ProportionalTo(context);

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
        if (context == null) return false;

        return Denominator.IsExchangeableTo(context)
            || Numerator.IsExchangeableTo(context)
            || GetLimit()?.IsExchangeableTo(context) == true;
    }

    public decimal ProportionalTo(IRate? other)
    {
        return base.ProportionalTo(other);
    }

    public bool TryExchangeTo(Enum? context, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(context)) return false;

        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));

        foreach (var item in GetRateComponentCodes())
        {
            IBaseMeasure? rateComponent = GetRateComponent(item);

            if (rateComponent?.GetMeasureUnitCode() == measureUnitElements.MeasureUnitCode
                && rateComponent!.TryExchangeTo(measureUnitElements.MeasureUnit, out IQuantifiable? exchangedRateComponent))
            {
                exchanged = item switch
                {
                    RateComponentCode.Denominator => GetRate(Numerator, (IDenominator)exchangedRateComponent!, GetLimit()!),
                    RateComponentCode.Numerator => GetRate((IMeasure)exchangedRateComponent!, Denominator, GetLimit()!),
                    RateComponentCode.Limit => GetRate(Numerator, Denominator, (ILimit)exchangedRateComponent!),

                    _ => null,
                };
            }
        }

        return exchanged != null;
    }

    public bool TryExchangeTo(IBaseMeasure? baseMeasure, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(baseMeasure)) return false;

        IDenominator denominator = Denominator.GetBaseMeasure(baseMeasure!);
        decimal proportionQuantity = denominator.ProportionalTo(baseMeasure);

        IMeasure numerator = Numerator.Divide(proportionQuantity);
        ILimit? limit = GetLimit();

        exchanged = GetRate(numerator, denominator, limit!);

        return exchanged != null;
    }

    public void ValidateDenominator(IMeasurable denominator, string paramName)
    {
        MeasureUnitCode measureUnitCode = NullChecked(denominator, paramName).GetMeasureUnitCode();

        if (!HasMeasureUnitCode(measureUnitCode))
        {
            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
        }

        if (denominator is IQuantifiable or IBaseMeasurement) return;

        ArgumentTypeOutOfRangeException(paramName, denominator);
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
}
