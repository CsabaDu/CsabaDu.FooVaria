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
        return x is null == y is null
            && x?.GetLimit() is null == y?.GetLimit() is null
            && x?.Equals(y) != false
            && x?.Numerator.Equals(x.GetLimit(), y?.GetLimit()) != false;
    }

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

        //if (context is null) return false;

        //return Denominator.IsExchangeableTo(context)
        //    || Numerator.IsExchangeableTo(context)
        //    || GetLimit()?.IsExchangeableTo(context) == true;
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

        return exchanged is not null;
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

        return exchanged is not null;
    }

    public void ValidateDenominator(IMeasurable denominator, string paramName)
    {
        MeasureUnitCode denominatorCode = NullChecked(denominator, paramName).GetMeasureUnitCode();

        if (denominator is IQuantifiable or IBaseMeasurement)
        {
            if (IsExchangeableTo(denominatorCode)) return;

            throw InvalidMeasureUnitCodeEnumArgumentException(denominatorCode, paramName);
        }

        throw ArgumentTypeOutOfRangeException(paramName, denominator);
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

    public bool IsExchangeableTo(IBaseMeasure? baseMeasure)
    {
        return baseMeasure?.IsExchangeableTo(GetDenominatorCode()) != true;
    }
    #endregion
    #endregion
}
