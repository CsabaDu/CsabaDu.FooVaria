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

        other.ValidateMeasureUnitTypeCode(MeasureUnitTypeCode, nameof(other));

        return DefaultQuantity.CompareTo(other.DefaultQuantity);
    }

    public IBaseMeasure? ExchangeTo(Enum measureUnit)
    {
        if (!IsExchangeableTo(measureUnit)) return null;

        decimal exchangeRate = Measurement.GetExchangeRate(measureUnit);

        if (GetRateComponentCode() == RateComponentCode.Limit && DefaultQuantity % exchangeRate > 0) return null;

        decimal quantity = DefaultQuantity / exchangeRate;

        return GetBaseMeasure(quantity, measureUnit);
    }

    public decimal GetDecimalQuantity()
    {
        return (decimal)GetQuantity(TypeCode.Decimal);
    }

    public decimal GetExchangeRate()
    {
        return Measurement.ExchangeRate;
    }

    public object GetQuantity(RoundingMode roundingMode)
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
                RoundingMode.Half => getHalfQuantity(),

                _ => throw InvalidRoundingModeEnumArgumentException(roundingMode),
            };
        }

        decimal getHalfQuantity()
        {
            decimal halfQuantity = decimal.Floor(quantity);
            decimal half = 0.5m;

            if (quantity == halfQuantity) return quantity;

            halfQuantity += half;

            if (quantity <= halfQuantity) return halfQuantity;

            return halfQuantity + half;
        }
        #endregion
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        return ((ValueType)Quantity).ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    public TypeCode? GetQuantityTypeCode(object quantity)
    {
        TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

        return GetValidQuantityTypeCodeOrNull(quantityTypeCode);
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
        ValueType quantity = (ValueType)GetQuantity(roundingMode);
        Enum measureUnit = Measurement.GetMeasureUnit();

        return GetBaseMeasure(quantity, measureUnit);
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

    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        Measurement.ValidateExchangeRate(exchangeRate, paramName);
    }

    public bool TryGetQuantity(ValueType quantity, [NotNullWhen(true)] out ValueType? thisTypeQuantity)
    {
        thisTypeQuantity = (ValueType?)quantity.ToQuantity(QuantityTypeCode);

        return thisTypeQuantity != null;
    }

    public void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode)
    {
        TypeCode? typeCode = GetQuantityTypeCode(NullChecked(quantity, nameof(quantity)));

        ValidateQuantityTypeCode(quantityTypeCode);

        if (typeCode == quantityTypeCode) return;

        throw QuantityArgumentOutOfRangeException(quantity);
    }

    public void ValidateQuantity(ValueType? quantity, string paramName)
    {
        try
        {
            _ = GetValidQuantity(quantity);
        }
        catch (ArgumentNullException)
        {
            throw new ArgumentNullException(paramName);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex.InnerException);
        }
    }

    public void ValidateQuantityTypeCode(TypeCode quantityTypeCode)
    {
        if (GetValidQuantityTypeCodeOrNull(quantityTypeCode) != null) return;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    #region Override methods
    public override IBaseMeasureFactory GetFactory()
    {
        return (IBaseMeasureFactory)Factory;
    }

    public override void Validate(IRootObject? rootObject, string paramName)
    {
        Validate(this, rootObject, validateBaseMeasure, paramName);

        #region Local methods
        void validateBaseMeasure()
        {
            _ = GetValidBaseMeasure(this, rootObject!, paramName);
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

    public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        Measurement.ValidateMeasureUnit(measureUnit, paramName);
    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual bool Equals(IBaseMeasure? other)
    {
        return MeasureUnitTypeCode == other?.MeasureUnitTypeCode
            && DefaultQuantity == other?.DefaultQuantity;
    }
    #endregion

    #region Abstract methods
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit);
    public abstract LimitMode? GetLimitMode();
    #endregion
    #endregion

    #region Protected methods
    protected U GetDefaultRateComponentQuantity<U>() where U : struct
    {
        return (U)GetFactory().DefaultRateComponentQuantity.ToQuantity(typeof(U))!;
    }

    protected object GetValidQuantity(ValueType? quantity)
    {
        _ = NullChecked(quantity, nameof(quantity));

        return GetValidQuantityOrNull(this, quantity) ?? throw QuantityArgumentOutOfRangeException(quantity);
    }

    #region Static methods
    protected static T GetValidBaseMeasure<T>(T commonBase, IRootObject other, string paramName) where T : class, IBaseMeasure
    {
        T baseMeasure = GetValidBaseMeasurable(commonBase, other, paramName);
        object quantity = baseMeasure.Quantity;

        if (GetValidQuantityOrNull(commonBase, baseMeasure.Quantity) != null) return baseMeasure;

        throw QuantityArgumentOutOfRangeException(paramName, (ValueType)quantity);
    }
    
    protected static object? GetValidQuantityOrNull(IBaseMeasure baseMeasure, object? quantity)
    {
        quantity = ((ValueType?)quantity)?.ToQuantity(baseMeasure.QuantityTypeCode);

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

    protected static void ValidateBaseMeasure<T>(T commonBase, IRootObject other, string paramName) where T : class, IBaseMeasure
    {
        T baseMeasure = GetValidBaseMeasurable(commonBase, other, paramName);
        RateComponentCode rateComponentCode = commonBase.GetRateComponentCode();
        RateComponentCode otherRateComponentCode = baseMeasure.GetRateComponentCode();

        _ = GetValidBaseMeasurable(baseMeasure, rateComponentCode, otherRateComponentCode, paramName);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static TypeCode? GetValidQuantityTypeCodeOrNull(TypeCode quantityTypeCode)
    {
        if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return quantityTypeCode;

        return null;
    }
    #endregion
    #endregion
}
