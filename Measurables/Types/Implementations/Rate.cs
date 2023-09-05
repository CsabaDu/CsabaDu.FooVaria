namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class Rate : Measurable, IRate
{
    #region Constructors
    private protected Rate(IRate rate) : base(rate)
    {
        Numerator = rate.Numerator;
        Denominator = rate.Denominator;
    }

    private protected Rate(IRateFactory rateFactory, IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity) : base(rateFactory, measureUnitTypeCode)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = GetDenominatorFactory(rateFactory).Create(customName, measureUnitTypeCode, exchangeRate, quantity);
    }

    private protected Rate(IRateFactory rateFactory, IMeasure numerator, Enum measureUnit, decimal? quantity) : base(rateFactory, measureUnit)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = GetDenominatorFactory(rateFactory).Create(measureUnit, quantity);
    }

    private protected Rate(IRateFactory rateFactory, IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity) : base(rateFactory, measureUnit)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = GetDenominatorFactory(rateFactory).Create(measureUnit, exchangeRate, customName, quantity);
    }

    private protected Rate(IRateFactory rateFactory, IRate rate) : base(rateFactory, rate)
    {
        Numerator = rate.Numerator;
        Denominator = rate.Denominator;
    }

    private protected Rate(IRateFactory rateFactory, IMeasure numerator, IDenominator denominator) : base(rateFactory, denominator)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = denominator;
    }
    #endregion

    #region Properties
    public IBaseMeasure? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Denominator => Denominator,
        RateComponentCode.Numerator => Numerator,
        RateComponentCode.Limit => GetLimit(),

        _ => null,
    };
    public IDenominator Denominator { get; init; }
    public IMeasure Numerator { get; init; }
    #endregion

    #region Public methods
    public int CompareTo(IRate? other)
    {
        if (other == null) return 1;

        if (!IsExchangeableTo(other)) throw new ArgumentOutOfRangeException(nameof(other));

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    public bool Equals(IRate? other)
    {
        return other?.Denominator.Equals(Denominator) == true
            && other.Numerator.Equals(Numerator);
    }

    public override sealed bool Equals(object? obj)
    {
        return obj is IRate other
            && Equals(other);
    }

    public IRate? ExchangeTo(IDenominator denominator)
    {
        if (denominator?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;
        
        IMeasure numerator = Numerator.Divide(Denominator.ProportionalTo(denominator!));

        return GetRate(numerator, denominator, GetLimit());
    }

    public override sealed IMeasurable GetDefault()
    {
        IMeasure numerator = (IMeasure)Numerator.GetDefault();
        IDenominator denominator = (IDenominator)Denominator.GetDefault();
        ILimit? limit = (ILimit?)GetLimit()?.GetDefault();

        return GetRate(numerator, denominator, limit);
    }

    public decimal GetDefaultQuantity()
    {
        return Numerator.DefaultQuantity / Denominator.DefaultQuantity;
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }

    public abstract ILimit? GetLimit();

    public override sealed Enum GetMeasureUnit()
    {
        return Denominator.GetMeasureUnit();
    }

    public override sealed TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return base.GetQuantityTypeCode(measureUnitTypeCode ?? Numerator.MeasureUnitTypeCode);
    }

    public IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[rateComponentCode];
    }

    public IRate GetRate(IRate? other = null)
    {
        return GetRateFactory().Create(other ?? this);
    }

    public IRateFactory GetRateFactory()
    {
        return MeasurableFactory as IRateFactory ?? throw new InvalidOperationException(null);
    }

    public bool IsExchangeableTo(IRate? other)
    {
        return other?.Denominator.IsExchangeableTo(MeasureUnitTypeCode) == true
            && other.Numerator.IsExchangeableTo(Numerator.MeasureUnitTypeCode);
    }

    public decimal ProportionalTo(IRate rate)
    {
        return GetDefaultQuantity() / NullChecked(rate, nameof(rate)).GetDefaultQuantity();
    }

    public bool TryExchangeTo(IDenominator denominator, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = ExchangeTo(denominator);

        return exchanged != null;
    }

    #region Abstract methods
    public abstract IRate GetRate(IMeasure numerator, string customName, decimal? quantity = null, ILimit? limit = null);
    public abstract IRate GetRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null);
    public abstract IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null);
    public abstract IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null);
    public abstract IRate GetRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null);
    public abstract IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
    #endregion
    #endregion

    #region Private methods
    private static IDenominatorFactory GetDenominatorFactory(IRateFactory rateFactory)
    {
        return rateFactory.DenominatorFactory;
    }
    #endregion
}
