namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class BaseMeasure : Measurable, IBaseMeasure
{
    private protected BaseMeasure(IBaseMeasure baseMeasure) : base(baseMeasure)
    {
        Measurement = baseMeasure.Measurement;
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName, ValueType quantity) : base(baseMeasureFactory, measureUnitTypeCode)
    {
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, Enum measureUnit, decimal? exchangeRate, string? customName, ValueType quantity) : base(baseMeasureFactory, measureUnit)
    {
        _ = NullChecked(quantity, nameof(quantity));

        IMeasurementFactory measurementFactory = baseMeasureFactory.MeasurementFactory;

        Measurement = exchangeRate == null ?
            measurementFactory.Create(measureUnit)
            : measurementFactory.Create(measureUnit, exchangeRate.Value);
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, IMeasurement measurement, ValueType quantity) : base(baseMeasureFactory, measurement)
    {
        _ = NullChecked(measurement, nameof(measurement));
        _ = NullChecked(quantity, nameof(quantity));

        Measurement = measurement;
    }

    public IMeasurement Measurement { get; init; }
    public virtual TypeCode QuantityTypeCode => GetQuantityTypeCode(MeasureUnitTypeCode);
    public decimal DefaultQuantity => GetDecimalQuantity() * GetExchangeRate();

    public abstract object Quantity { get; init; }
    public abstract RateComponentCode RateComponentCode { get; init; }

    private IBaseMeasureFactory BaseMeasureFactory => (IBaseMeasureFactory)MeasurableFactory;

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
    public abstract ValueType? ExchangeTo(decimal exchangeRate);
    public abstract IBaseMeasure? ExchangeTo(Enum measureUnit);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, string name);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null, string? customName = null);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null);
    public abstract IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null);
    public decimal GetDecimalQuantity(IBaseMeasure? baseMeasure = null)
    {
        baseMeasure ??= this;

        return baseMeasure.DefaultQuantity / baseMeasure.GetExchangeRate();
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

    public override Enum GetMeasureUnit()
    {
        return Measurement.GetMeasureUnit();
    }

    public ValueType GetQuantity()
    {
        return (ValueType)Quantity;
    }
    public abstract ValueType GetQuantity(RoundingMode roundingMode);
    public abstract ValueType GetQuantity(TypeCode quantityTypeCode);
    public abstract IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
    public bool IsExchangeableTo(Enum context)
    {
        return Measurement.IsExchangeableTo(context);
    }
    public decimal ProportionalTo(IBaseMeasure baseMeasure)
    {
       _ = NullChecked(baseMeasure, nameof(baseMeasure));

        MeasureUnitTypeCode measureUnitTypeCode = baseMeasure.MeasureUnitTypeCode;

        if (IsExchangeableTo(measureUnitTypeCode)) return DefaultQuantity / baseMeasure.DefaultQuantity;

        throw new ArgumentOutOfRangeException(nameof(baseMeasure), measureUnitTypeCode, null);
    }
    public abstract IBaseMeasure Round(RoundingMode roundingMode);
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

    public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Measurement.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
    }

    public abstract void ValidateQuantity(ValueType quantity, TypeCode? quantityTypeCode = null);
    public abstract void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
}
