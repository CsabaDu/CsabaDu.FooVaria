namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class Rate : Measurable, IRate
{
    #region Constructors
    private protected Rate(IRate other) : base(other)
    {
        Numerator = other.Numerator;
        Denominator = other.Denominator;
    }
    private protected Rate(IRateFactory factory, IMeasure numerator, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = (IDenominator)factory.DenominatorFactory.CreateDefault(measureUnitTypeCode);
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IMeasurement measurement) : base(factory, measurement)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = factory.DenominatorFactory.Create(measurement);
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, denominator)
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

        return GetRateDefaultQuantity().CompareTo(other.GetRateDefaultQuantity());
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

    public override sealed IRate GetDefault()
    {
        IMeasure numerator = Numerator.GetDefaultRateComponent();
        IDenominator denominator = Denominator.GetDefaultRateComponent();
        ILimit? limit = GetLimit()?.GetDefaultRateComponent();

        return GetRate(numerator, denominator, limit);
    }

    public decimal GetRateDefaultQuantity()
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

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return Numerator.GetQuantityTypeCode();
    }

    public IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[Defined(rateComponentCode, nameof(rateComponentCode))];
    }

    public IRate GetRate(IRate other)
    {
        return (IRate)GetFactory().Create(other);
    }

    public bool IsExchangeableTo(IRate? other)
    {
        return other?.Denominator.IsExchangeableTo(MeasureUnitTypeCode) == true
            && other.Numerator.IsExchangeableTo(Numerator.MeasureUnitTypeCode);
    }

    public decimal ProportionalTo(IRate rate)
    {
        return GetRateDefaultQuantity() / NullChecked(rate, nameof(rate)).GetRateDefaultQuantity();
    }

    public bool TryExchangeTo(IDenominator denominator, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = ExchangeTo(denominator);

        return exchanged != null;
    }

    #region Override methods
    public override IRateFactory GetFactory()
    {
        return (IRateFactory)Factory;
    }
    #endregion

    #region Abstract methods
    public abstract IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit);
    #endregion
    #endregion
}
