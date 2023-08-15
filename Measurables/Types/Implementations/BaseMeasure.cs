using CsabaDu.FooVaria.Common.Statics;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class BaseMeasure : Measurable, IBaseMeasure
{
    private protected BaseMeasure(IBaseMeasure baseMeasure) : base(baseMeasure)
    {
        Measurement = baseMeasure.Measurement;
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName) : base(baseMeasureFactory, customMeasureUnitTypeCode)
    {
        ValidateQuantity(quantity);

        Measurement = baseMeasureFactory.MeasurementFactory.Create(customMeasureUnitTypeCode, exchangeRate, customName);
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit) : base(baseMeasureFactory, measureUnit)
    {
        ValidateQuantity(quantity);

        Measurement = baseMeasureFactory.MeasurementFactory.Create(measureUnit);
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName) : base(baseMeasureFactory, measureUnit)
    {
        ValidateQuantity(quantity);

        Measurement = baseMeasureFactory.MeasurementFactory.Create(measureUnit, exchangeRate, customName);
    }

    private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, IMeasurement measurement) : base(baseMeasureFactory, measurement)
    {
        ValidateQuantity(quantity);

        Measurement = measurement;
    }

    public IMeasurement Measurement { get; init; }
    public abstract TypeCode QuantityTypeCode { get; }
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

    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, string name);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName = null);
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null);
    public abstract IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null);
    public decimal GetDecimalQuantity(IBaseMeasure? baseMeasure = null)
    {
        ValueType quantity = (baseMeasure ?? this).GetQuantity();

        return (decimal)quantity.ToQuantity(TypeCode.Decimal)!;
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

    public TypeCode? GetQuantityTypeCode(ValueType? quantity)
    {
        TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

        return GetQuantityTypeCodes().Contains(quantityTypeCode) ?
            quantityTypeCode
            : null;
    }

    public abstract IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
    public bool IsExchangeableTo(Enum context)
    {
        return Measurement.IsExchangeableTo(context);
    }
    public decimal ProportionalTo(IBaseMeasure baseMeasure)
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(baseMeasure, nameof(baseMeasure)).MeasureUnitTypeCode;

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

    public override sealed void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        Measurement.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);
    }

    public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Measurement.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
    }

    public virtual void ValidateQuantity(ValueType? quantity, TypeCode? quantityTypeCode = null)
    {
        TypeCode typeCode = GetQuantityTypeCode(quantity) ?? throw QuantityArgumentOutOfRangeException(quantity);

        if (quantityTypeCode == null) return;

        if (typeCode == quantityTypeCode) return;

        throw QuantityArgumentOutOfRangeException(quantity);
    }
    public abstract void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
}
