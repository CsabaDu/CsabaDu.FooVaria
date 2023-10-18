using CsabaDu.FooVaria.Common;
using static CsabaDu.FooVaria.Measurables.Statics.QuantityTypes;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class BaseMeasure : Measurable, IBaseMeasure
{
    #region Constructors
    private protected BaseMeasure(IBaseMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
        Quantity = factory.DefaultRateComponentQuantity;
        Measurement = factory.MeasurementFactory.CreateDefault(measureUnitTypeCode);
    }

    private protected BaseMeasure(IBaseMeasureFactory factory, ValueType quantity, Enum measureUnit) : base(factory, measureUnit)
    {
        Quantity = GetValidQuantity(quantity);
        Measurement = factory.MeasurementFactory.Create(measureUnit);
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

    #region Public methods
    public int CompareTo(IBaseMeasure? other)
    {
        if (other == null) return 1;

        other.ValidateMeasureUnitTypeCode(MeasureUnitTypeCode);

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

    public IBaseMeasure GetBaseMeasure(IMeasurable other)
    {
        return (IBaseMeasure) GetFactory().Create(other);
    }

    public decimal GetDecimalQuantity()
    {
        return (decimal)GetQuantity(TypeCode.Decimal);
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
        Enum measureUnit = Measurement.GetMeasureUnit();

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
    public override IBaseMeasureFactory GetFactory()
    {
        return (IBaseMeasureFactory)Factory;
    }

    public override void Validate(IFooVariaObject? fooVariaObject)
    {
        ValidateCommonBaseAction = () => validateBaseMeasure(this, fooVariaObject!);

        Validate(this, fooVariaObject);

        #region Local methods
        static void validateBaseMeasure<T>(T commonBase, IFooVariaObject other) where T : class, IBaseMeasure
        {
            T baseMeasure = GetValidBaseMeasurable(commonBase, other);

            _ = GetValidQuantity(commonBase, baseMeasure.GetQuantity());
        }
        #endregion
    }

    #region Sealed methods
    public override sealed bool Equals(object? obj)
    {
        return obj is IBaseMeasure other && Equals(other);
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode);
    }

    //public override sealed Enum GetMeasureUnit()
    //{
    //    return Measurement.GetMeasureUnit();
    //}

    public override sealed void ValidateMeasureUnit(Enum measureUnit)
    {
        Measurement.ValidateMeasureUnit(measureUnit);
    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual bool Equals(IBaseMeasure? other)
    {
        return MeasureUnitTypeCode == other?.MeasureUnitTypeCode
            && DefaultQuantity == other?.DefaultQuantity;
    }

    public virtual void ValidateQuantity(ValueType? quantity)
    {
        if (NullChecked(quantity, nameof(quantity)).IsValidTypeQuantity()) return;

        throw QuantityArgumentOutOfRangeException(quantity);
    }
    #endregion

    #region Abstract methods
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    public abstract LimitMode? GetLimitMode();
    #endregion
    #endregion

    #region Protected methods
    protected T GetDefaultRateComponentQuantity<T>() where T : struct
    {
        return (T)GetFactory().DefaultRateComponentQuantity;
    }

    protected object GetValidQuantity(ValueType? quantity)
    {
        _ = NullChecked(quantity, nameof(quantity));

        return GetValidQuantity(this, quantity) ?? throw QuantityArgumentOutOfRangeException(quantity);
    }

    #region Static methods
    protected static void ValidateBaseMeasure<T>(T commonBase, IFooVariaObject other) where T : class, IBaseMeasure
    {
        T baseMeasure = GetValidBaseMeasurable(commonBase, other);
        RateComponentCode rateComponentCode = commonBase.GetRateComponentCode();
        RateComponentCode otherRateComponentCode = baseMeasure.GetRateComponentCode();

        _ = GetValidBaseMeasurable(baseMeasure, rateComponentCode, otherRateComponentCode);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static object? GetValidQuantity(IBaseMeasure baseMeasure, ValueType? quantity)
    {
        quantity = quantity?.ToQuantity(baseMeasure.QuantityTypeCode);

        return baseMeasure.GetRateComponentCode() switch
        {
            RateComponentCode.Denominator => getValidDenominatorQuantity(),
            RateComponentCode.Numerator or
            RateComponentCode.Limit => quantity,

            _ => throw new InvalidOperationException(null),
        };

        #region Local methods
        object? getValidDenominatorQuantity()
        {
            if (quantity == null || (decimal)quantity <= 0) return null;

            return quantity;
        }
        #endregion
    }
    #endregion
    #endregion
}
