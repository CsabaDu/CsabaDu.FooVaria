using CsabaDu.FooVaria.Common.Enums;
using CsabaDu.FooVaria.Common.Types;
using static CsabaDu.FooVaria.Common.Statics.QuantityTypes;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class BaseMeasure : Measurable, IBaseMeasure
{
    #region Constructors
    private protected BaseMeasure(IBaseMeasure other) : base(other)
    {
        Quantity = GetValidQuantity(other.GetQuantity());
        Measurement = other.Measurement;
    }

    private protected BaseMeasure(IBaseMeasure other, ValueType quantity) : base(other)
    {
        Quantity = GetValidQuantity(quantity);
        Measurement = other.Measurement;
    }

    private protected BaseMeasure(IBaseMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
        Quantity = factory.DefaultRateComponentQuantity;
        Measurement = GetDefaultMeasurement();
    }

    private protected BaseMeasure(IBaseMeasureFactory factory, ValueType quantity, IMeasurement measurement) : base(factory, measurement)
    {
        Quantity = GetValidQuantity(quantity);
        Measurement = measurement;
    }
    #endregion

    #region Properties
    public object Quantity { get; init; }
    public IMeasurement Measurement { get; init; }
    public decimal DefaultQuantity => RoundQuantity(GetDecimalQuantity() * GetExchangeRate());
    public TypeCode QuantityTypeCode => GetQuantityTypeCode();
    #endregion

    //protected const decimal DefaultDenominatorQuantity = decimal.One;
    //protected const ulong DefaultLimitQuantity = default;
    //protected const int DefaultMeasureQuantity = default;

    #region Public methods
    public int CompareTo(IBaseMeasure? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitTypeCode(other.MeasureUnitTypeCode);

        return DefaultQuantity.CompareTo(other.DefaultQuantity);
    }

    public ValueType? ExchangeTo(decimal exchangeRate)
    {
        if (exchangeRate <= 0) return null;

        decimal exchanged = DefaultQuantity / exchangeRate;

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

    public IBaseMeasure GetBaseMeasure(IBaseMeasure other)
    {
        return (IBaseMeasure) GetFactory().Create(other);
    }

    //public IBaseMeasure GetBaseMeasure(IBaseMeasureFactory baseMeasureFactory, IBaseMeasure baseMeasure)
    //{
    //    return GetFactory().Create(baseMeasureFactory, baseMeasure);
    //}

    //public IBaseMeasureFactory GetFactory()
    //{
    //    return Factory as IBaseMeasureFactory ?? throw new InvalidOperationException(null);
    //}

    public decimal GetDecimalQuantity()
    {
        return (decimal)GetQuantity(TypeCode.Decimal);
    }

    public IMeasurement GetDefaultMeasurement()
    {
        return GetFactory().MeasurementFactory.CreateDefault(MeasureUnitTypeCode);
    }

    public ValueType GetDefaultRateComponentQuantity()
    {
        return (ValueType)GetFactory().DefaultRateComponentQuantity;
    }

    public decimal GetExchangeRate()
    {
        return Measurement.ExchangeRate;
    }

    public ValueType GetQuantity()
    {
        return (ValueType)Quantity;
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
            decimal halfQuantity = decimal.Floor(quantity);

            if (quantity == halfQuantity) return quantity;

            halfQuantity += 0.5m;

            if (quantity <= halfQuantity) return halfQuantity;

            return halfQuantity + 0.5m;
        }
        #endregion
    }

    public ValueType GetQuantity(TypeCode quantityTypeCode)
    {
        return GetQuantity().ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    public TypeCode? GetQuantityTypeCode(ValueType quantity)
    {
        TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

        if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return quantityTypeCode;

        return null;
    }

    public IBaseMeasure? GetRateComponent(IRate rate, RateComponentCode rateComponentCode)
    {
        return NullChecked(rate, nameof(rate))[rateComponentCode];
    }

    public RateComponentCode GetRateComponentCode()
    {
        return GetFactory().RateComponentCode;
    }

    //public RateComponentCode GetRateComponentCode(IBaseMeasure baseMeasure)
    //{
    //    return ((IBaseMeasureFactory)NullChecked(baseMeasure, nameof(baseMeasure)).GetFactory()).RateComponentCode;
    //}

    public bool IsExchangeableTo(Enum? context)
    {
        return Measurement.IsExchangeableTo(context);
    }

    public decimal ProportionalTo(IBaseMeasure baseMeasure)
    {
        if (NullChecked(baseMeasure, nameof(baseMeasure)).IsExchangeableTo(MeasureUnitTypeCode)) return DefaultQuantity / baseMeasure.DefaultQuantity;

        throw new ArgumentOutOfRangeException(nameof(baseMeasure), baseMeasure.MeasureUnitTypeCode, null);
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

    public bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure)
    {
        baseMeasure = null;

        if (!Measurement.TrySetCustomMeasurement(measureUnit, exchangeRate, customName)) return false;

        if (!TryGetQuantity(quantity, out ValueType? rateComponentQuantity)) return false;

        try
        {
            baseMeasure = GetBaseMeasure(rateComponentQuantity, measureUnit);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool TryGetQuantity(ValueType? quantity, [NotNullWhen(true)] out ValueType? thisTypeQuantity)
    {
        thisTypeQuantity = quantity == null ?
            GetQuantity()
            : quantity.ToQuantity(QuantityTypeCode);

        return thisTypeQuantity != null;
    }

    public bool TryExchangeTo(Enum measureUnit, [NotNullWhen(true)] out IBaseMeasure? exchanged)
    {
        exchanged = ExchangeTo(measureUnit);

        return exchanged != null;
    }

    public void ValidateQuantity(ValueType? quantity)
    {
        _ = GetValidQuantity(quantity);
    }

    public void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode)
    {
        TypeCode? typeCode = GetQuantityTypeCode(NullChecked(quantity, nameof(quantity)));

        ValidateQuantityTypeCode(quantityTypeCode);

        if (typeCode == quantityTypeCode) return;

        throw QuantityArgumentOutOfRangeException(quantity);
    }

    public void ValidateQuantityTypeCode(TypeCode quantityTypeCode)
    {
        if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IBaseMeasure other && Equals(other);
    }

    public override sealed IBaseMeasure GetDefault()
    {
        Enum measureUnit = GetDefaultMeasureUnit();
        ValueType quantity = GetDefaultRateComponentQuantity();

        return GetBaseMeasure(quantity, measureUnit);
    }

    public override IBaseMeasureFactory GetFactory()
    {
        return (IBaseMeasureFactory)Factory;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode);
    }

    public override sealed Enum GetMeasureUnit()
    {
        return Measurement.GetMeasureUnit();
    }
    #endregion

    #region Virtual methods
    public virtual bool Equals(IBaseMeasure? other)
    {
        return other?.MeasureUnitTypeCode == MeasureUnitTypeCode
            && other.DefaultQuantity == DefaultQuantity;
    }
    #endregion

    #region Abstract methods
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    //public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName);
    //public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement measurement);
    //public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    //public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, string name);
    public abstract LimitMode? GetLimitMode();
    #endregion
    #endregion

    #region Protected methods
    protected object GetValidQuantity(ValueType? quantity)
    {
        quantity = NullChecked(quantity, nameof(quantity)).ToQuantity(QuantityTypeCode);

        if (quantity != null) return GetRateComponentCode() switch
        {
            RateComponentCode.Denominator => getValidDenominatorQuantity(),

            RateComponentCode.Numerator or
            RateComponentCode.Limit => quantity,

            _ => throw new InvalidOperationException(null),
        };

        throw exception();

        #region Local methods
        ValueType getValidDenominatorQuantity()
        {
            if ((decimal)quantity! > 0) return quantity;

            throw exception();
        }

        ArgumentOutOfRangeException exception()
        {
            return QuantityArgumentOutOfRangeException(quantity);
        }
        #endregion
    }

    #region Static methods
    protected static bool Equals<T>(T baseMeasure, IBaseMeasure? other) where T : class, IBaseMeasure
    {
        return baseMeasure.Equals(other)
            && baseMeasure.GetRateComponentCode() == other.GetRateComponentCode();
    }

    protected static int GetHashCode<T>([DisallowNull] T baseMeasure) where T : class, IBaseMeasure
    {
        return HashCode.Combine(baseMeasure as IBaseMeasure, baseMeasure.GetRateComponentCode());
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    #endregion
    #endregion
}
