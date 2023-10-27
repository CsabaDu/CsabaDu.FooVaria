﻿using static CsabaDu.FooVaria.Measurables.Statics.QuantityTypes;

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

    public IBaseMeasure? ExchangeTo(Enum measureUnit)
    {
        if (!IsExchangeableTo(measureUnit)) return null;

        decimal exchangeRate = Measurement.GetExchangeRate(measureUnit);
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

        return (ValueType?)quantity.ToQuantity(QuantityTypeCode) ?? throw new InvalidOperationException(null);

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

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        return ((ValueType)Quantity).ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    public TypeCode? GetQuantityTypeCode(object quantity)
    {
        TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

        return GetValidQuantityTypeCode(quantityTypeCode);
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

    public void ValidateExchangeRate(decimal exchangeRate)
    {
        Measurement.ValidateExchangeRate(exchangeRate);
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

    public void ValidateQuantity(ValueType? quantity)
    {
        _ = GetValidQuantity(quantity);
    }

    public void ValidateQuantityTypeCode(TypeCode quantityTypeCode)
    {
        if (GetValidQuantityTypeCode(quantityTypeCode) != null) return;

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

            _ = GetValidQuantity(commonBase, baseMeasure.Quantity);
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

        return GetValidQuantity(this, quantity) ?? throw QuantityArgumentOutOfRangeException(quantity);
    }

    #region Static methods
    protected static object? GetValidQuantity(IBaseMeasure baseMeasure, object? quantity)
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
    private static TypeCode? GetValidQuantityTypeCode(TypeCode quantityTypeCode)
    {
        if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return quantityTypeCode;

        return null;
    }
    #endregion
    #endregion
}
