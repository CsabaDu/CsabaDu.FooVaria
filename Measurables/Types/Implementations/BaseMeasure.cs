namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class BaseMeasure : Measurable, IBaseMeasure
{
    #region Constructors
    private protected BaseMeasure(IBaseMeasure baseMeasure) : base(baseMeasure)
    {
        Measurement = baseMeasure.Measurement;
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(baseMeasureFactory, measureUnitTypeCode) // TODO
    {
        ValidateQuantity(quantity);

        Measurement = GetMeasurementFactory(baseMeasureFactory).Create(customName, measureUnitTypeCode, exchangeRate);
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit) : base(baseMeasureFactory, measureUnit)
    {
        ValidateQuantity(quantity);

        Measurement = GetMeasurementFactory(baseMeasureFactory).Create(measureUnit);
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName) : base(baseMeasureFactory, measureUnit) // TODO
    {
        ValidateQuantity(quantity);

        Measurement = GetMeasurementFactory(baseMeasureFactory).Create(measureUnit, exchangeRate, customName);
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, IMeasurement measurement) : base(baseMeasureFactory, measurement)
    {
        ValidateQuantity(quantity);

        Measurement = measurement;
    }
    #endregion

    #region Properties
    public IMeasurement Measurement { get; init; }
    public decimal DefaultQuantity => GetDecimalQuantity() * GetExchangeRate();

    #region Abstract properties
    public abstract object Quantity { get; init; }
    public abstract TypeCode QuantityTypeCode { get; }
    #endregion
    #endregion

    #region Public methods
    public int CompareTo(IBaseMeasure? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitTypeCode(other.MeasureUnitTypeCode);

        return DefaultQuantity.CompareTo(other.DefaultQuantity);
    }

    public virtual bool Equals(IBaseMeasure? other)
    {
        return other is IBaseMeasure baseMeasure
            && baseMeasure.IsExchangeableTo(MeasureUnitTypeCode)
            && baseMeasure.DefaultQuantity == DefaultQuantity;
    }

    public override bool Equals(object? obj)
    {
        return obj is IBaseMeasure other && Equals(other);
    }

    public ValueType? ExchangeTo(decimal exchangeRate)
    {
        if (exchangeRate <= 0) return null;

        ValueType exchanged = DefaultQuantity / exchangeRate;

        return exchanged.ToQuantity(QuantityTypeCode);
    }

    public IBaseMeasure? ExchangeTo(Enum measureUnit)
    {
        if (!IsExchangeableTo(measureUnit)) return null;

        decimal exchangeRate = Measurement.GetExchangeRate(measureUnit);

        ValueType? quantity = ExchangeTo(exchangeRate);

        if (quantity == null) return null;

        return GetBaseMeasure(quantity, measureUnit);
    }

    public IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null)
    {
        return (IBaseMeasure)GetBaseMeasureFactory().Create(other ?? this);
    }

    public IBaseMeasureFactory GetBaseMeasureFactory()
    {
        return MeasurableFactory as IBaseMeasureFactory ?? throw new InvalidOperationException(null);
    }

    public decimal GetDecimalQuantity(IBaseMeasure? other = null)
    {
        ValueType quantity = (other ?? this).GetQuantity();

        return (decimal?)quantity.ToQuantity(TypeCode.Decimal) ?? throw new InvalidOperationException(null);
    }

    public IBaseMeasure GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        base.ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        Enum measureUnit = GetDefaultMeasureUnit(measureUnitTypeCode);
        ValueType quantity = GetDefaultRateComponentQuantity();

        return GetBaseMeasure(quantity, measureUnit);
    }

    public IBaseMeasure GetDefault(RateComponentCode rateComponentCode, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return GetBaseMeasureFactory().CreateDefault(rateComponentCode, measureUnitTypeCode ?? MeasureUnitTypeCode);
    }

    public decimal GetExchangeRate()
    {
        return Measurement.ExchangeRate;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode);
    }

    public virtual LimitMode? GetLimitMode()
    {
        return null;
    }

    public override sealed Enum GetMeasureUnit()
    {
        return Measurement.GetMeasureUnit();
    }

    public virtual ValueType GetQuantity(ValueType? quantity = null)
    {
        if (quantity == null) return (ValueType)Quantity;

        return quantity.ToQuantity(QuantityTypeCode) ?? throw QuantityArgumentOutOfRangeException(quantity);
    }

    public ValueType GetQuantity(RoundingMode roundingMode)
    {
        decimal quantity = roundDecimalQuantity();

        return quantity.ToQuantity(QuantityTypeCode) ?? throw new InvalidOperationException(null);

        #region Local methods
        decimal roundDecimalQuantity()
        {
            quantity = GetDecimalQuantity();

            return roundingMode switch
            {
                RoundingMode.General => decimal.Round(quantity),
                RoundingMode.Ceiling => decimal.Ceiling(quantity),
                RoundingMode.Floor => decimal.Floor(quantity),
                RoundingMode.Half => decimalHalfQuantity(),

                _ => throw InvalidRoundingModeEnumArgumentException(roundingMode),
            };
        }

        decimal decimalHalfQuantity()
        {
            decimal floorQuantity = decimal.Floor(quantity);

            if (floorQuantity == quantity) return floorQuantity;

            decimal halfQuantity = floorQuantity + 0.5m;

            if (quantity <= halfQuantity) return halfQuantity;

            return halfQuantity + 0.5m;
        }
        #endregion
    }

    public ValueType GetQuantity(TypeCode quantityTypeCode)
    {
        return GetQuantity().ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    public TypeCode? GetQuantityTypeCode([DisallowNull] ValueType quantity)
    {
        if (quantity is MeasureUnitTypeCode measureUnitTypeCode) return base.GetQuantityTypeCode(measureUnitTypeCode);

        TypeCode quantityTypeCode = Type.GetTypeCode(NullChecked(quantity, nameof(quantity)).GetType());

        if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return quantityTypeCode;

        return null;
    }

    public IBaseMeasure? GetRateComponent(IRate rate, RateComponentCode rateComponentCode)
    {
        return rate[rateComponentCode];
    }

    public RateComponentCode? GetRateComponentCode()
    {
        return GetBaseMeasureFactory().RateComponentCode;
    }

    public RateComponentCode GetRateComponentCode(IBaseMeasure? baseMeasure = null)
    {
        return (baseMeasure ?? this).GetBaseMeasureFactory().RateComponentCode;
    }

    public bool IsExchangeableTo(Enum? context)
    {
        return Measurement.IsExchangeableTo(context);
    }

    public decimal ProportionalTo(IBaseMeasure baseMeasure)
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(baseMeasure, nameof(baseMeasure)).MeasureUnitTypeCode;

        if (IsExchangeableTo(measureUnitTypeCode)) return DefaultQuantity / baseMeasure.DefaultQuantity;

        throw new ArgumentOutOfRangeException(nameof(baseMeasure), measureUnitTypeCode, null);
    }

    public IBaseMeasure Round(RoundingMode roundingMode)
    {
        ValueType quantity = GetQuantity(roundingMode);
        Enum measureUnit = GetMeasureUnit();

        return GetBaseMeasure(quantity, measureUnit);
    }

    public bool TryExchangeTo(decimal exchangeRate, [NotNullWhen(true)] out ValueType? exchanged)
    {
        exchanged = ExchangeTo(exchangeRate);

        return exchanged != null;
    }

    public bool TryExchangeTo(Enum measureUnit, [NotNullWhen(true)] out IBaseMeasure? exchanged)
    {
        exchanged = ExchangeTo(measureUnit);

        return exchanged != null;
    }

    public abstract bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure);

    public override sealed void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        Measurement.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);
    }

    public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Measurement.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
    }

    public void ValidateQuantity(ValueType? quantity, TypeCode? quantityTypeCode = null)
    {
        _ = NullChecked(quantity, nameof(quantity));

        if (quantityTypeCode == null) return;

        TypeCode? typeCode = GetQuantityTypeCode(quantity!);

        if (typeCode == quantityTypeCode) return;

        throw QuantityArgumentOutOfRangeException(quantity);
    }

    public void ValidateQuantityTypeCode(TypeCode quantityTypeCode)
    {
        if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    #region Abstract methods
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, string name);
    public abstract ValueType GetDefaultRateComponentQuantity();
    #endregion
    #endregion

    #region Protected methods
    protected static bool Equals<T>(T baseMeasure, IBaseMeasure? other) where T : class, IBaseMeasure
    {
        return baseMeasure.Equals(other)
            && baseMeasure.GetRateComponentCode() == other.GetRateComponentCode();
    }

    protected static int GetHashCode<T>(T baseMeasure) where T : class, IBaseMeasure
    {
        return HashCode.Combine(baseMeasure.GetHashCode(), baseMeasure.GetRateComponentCode());
    }
    #endregion

    #region Private methods
    private static IMeasurementFactory GetMeasurementFactory(IBaseMeasureFactory baseMeasureFactory)
    {
        return baseMeasureFactory.MeasurementFactory;
    }
    #endregion
}
