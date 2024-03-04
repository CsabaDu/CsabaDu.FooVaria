using CsabaDu.FooVaria.BaseTypes.Measurables.Enums;
using CsabaDu.FooVaria.RateComponents.Types;
using static CsabaDu.FooVaria.BaseTypes.Measurables.Types.Implementations.Measurable;

namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal abstract class Rate : BaseRate, IRate
{
    #region Constructors
    private protected Rate(IRate other) : base(other)
    {
        Numerator = other.Numerator;
        Denominator = other.Denominator;
    }

    protected Rate(IRateFactory factory, IRate rate) : base(factory)
    {
        Numerator = NullChecked(rate, nameof(rate)).Numerator;
        Denominator = rate.Denominator;
    }

    //private protected Rate(IRateFactory factory, IMeasure numerator, MeasureUnitCode denominatorCode) : base(factory)
    //{
    //    Numerator = NullChecked(numerator, nameof(numerator));
    //    Denominator = CreateDenominator(denominatorCode);
    //}

    //private protected Rate(IRateFactory factory, IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity) : base(factory)
    //{
    //    Numerator = NullChecked(numerator, nameof(numerator));
    //    Denominator = factory.DenominatorFactory.Create(denominatorMeasureUnit, denominatorQuantity);
    //}

    //private protected Rate(IRateFactory factory, IMeasure numerator, IMeasurement denominatorMeasurement) : base(factory)
    //{
    //    Numerator = NullChecked(numerator, nameof(numerator));
    //    Denominator = factory.DenominatorFactory.Create(denominatorMeasurement);
    //}

    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory)
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
        string paramName = nameof(denominator);
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
        ILimit? yLimit = y.GetLimit();

        return xLimit == null
            && yLimit == null
            || xLimit?.Equals(xLimit, yLimit) == true;
    }

    public IRate? ExchangeTo(IMeasurable? context)
    {
        if (context is IMeasurement measurement) return exchangeToMeasurement(measurement);

        if (context is IBaseMeasure baseMeasure) return exchangeToBaseMeasure(baseMeasure);

        return null;

        #region Local methods
        IRate? exchangeToMeasurement(IMeasurement? measurement)
        {
            if (measurement?.IsExchangeableTo(GetMeasureUnitCode()) != true) return null;

            IDenominator denominator = Denominator.GetDenominator(measurement);
            decimal proportionQuantity = denominator.Measurement.ProportionalTo(measurement);

            return exchange(denominator, proportionQuantity);
        }

        IRate? exchangeToBaseMeasure(IBaseMeasure? baseMeasure)
        {
            if (baseMeasure?.IsExchangeableTo(GetMeasureUnitCode()) != true) return null;

            IDenominator denominator = Denominator.GetBaseMeasure(baseMeasure);
            decimal proportionQuantity = denominator.ProportionalTo(baseMeasure);

            return exchange(denominator, proportionQuantity);
        }

        IRate? exchange(IDenominator denominator, decimal proportionQuantity)
        {
            IMeasure numerator = Numerator.Divide(proportionQuantity);
            ILimit? limit = GetLimit();

            return limit == null ?
                GetRate(numerator, denominator)
                : GetRate(numerator, denominator, limit);
        }
        #endregion
    }

    public int GetHashCode([DisallowNull] IRate rate)
    {
        return HashCode.Combine(rate.GetHashCode(), rate.GetLimit()?.GetHashCode());
    }

    public IRate GetRate(params IBaseMeasure[] rateComponents)
    {
        return GetFactory().Create(rateComponents);
    }

    //public IRate GetRate(IMeasure numerator, IDenominator denominator)
    //{
    //    return GetFactory().Create(numerator, denominator);
    //}
    public IBaseMeasure GetBaseMeasure(RateComponentCode rateComponentCode)
    {
        return GetRateComponent(rateComponentCode) ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }

    //public IRate GetRate(IBaseRate baseRate)
    //{
    //    //decimal defaultQuantity = NullChecked(baseRate, nameof(baseRate)).GetDefaultQuantity();
    //    //MeasureUnitCode numeratorCode = GetMeasureUnitCode(RateComponentCode.Numerator);
    //    //MeasureUnitCode denominatorCode = GetMeasureUnitCode(RateComponentCode.Denominator);

    //    return (IRate)GetBaseRate(baseRate);
    //}

    public bool IsExchangeableTo(IMeasurable? context)
    {
        return context switch
        {
            BaseRate baseRate => base.IsExchangeableTo(baseRate),

            BaseMeasure or
            BaseMeasurement => context!.HasMeasureUnitCode(GetMeasureUnitCode()),

           _ => false,
        };
    }

    public decimal ProportionalTo(IRate? other)
    {
        return base.ProportionalTo(other);
    }

    public bool TryExchangeTo(IMeasurable? context, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = ExchangeTo(context);

        return exchanged != null;
    }

    public void ValidateDenominator(IQuantifiable denominator)
    {
        if (GetBaseMeasure(RateComponentCode.Denominator).Equals(denominator)) return;

        throw new ArgumentOutOfRangeException(nameof(denominator), denominator, null);
    }

    #region Override methods
    public override IRateFactory GetFactory()
    {
        return (IRateFactory)Factory;
    }

    #region Sealed methods
    //public override sealed IRate GetBaseRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    //{
    //    Enum numeratorMeasureUnit = numeratorCode.GetDefaultMeasureUnit();

    //    ValidateQuantity(defaultQuantity, nameof(defaultQuantity));

    //    IBaseMeasure denominator = CreateDenominator(denominatorCode);
    //    IBaseMeasure numerator = Numerator.GetBaseMeasure(numeratorMeasureUnit, defaultQuantity);

    //    return GetRate(numerator, denominator);
    //}

    //public override sealed IRate GetBaseRate(IBaseRate baseRate)
    //{
    //    if (NullChecked(baseRate, nameof(baseRate)) is IRate rate) return GetRate(rate);

    //    MeasureUnitCode numeratorCode = baseRate.GetNumeratorCode();
    //    decimal defaultQuantity = baseRate.GetDefaultQuantity();
    //    MeasureUnitCode denominatorCode = baseRate.GetDenominatorCode();

    //    return GetBaseRate(numeratorCode, defaultQuantity, denominatorCode);
    //}

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
    #endregion
    #endregion

    #region Private methods
    private IDenominator CreateDenominator(MeasureUnitCode denominatorCode)
    {
        IDenominatorFactory factory = GetFactory().DenominatorFactory;

        return (IDenominator)(factory.CreateDefault(denominatorCode)
            ?? throw InvalidMeasureUnitCodeEnumArgumentException(denominatorCode, nameof(denominatorCode)));
    }
    #endregion
}
