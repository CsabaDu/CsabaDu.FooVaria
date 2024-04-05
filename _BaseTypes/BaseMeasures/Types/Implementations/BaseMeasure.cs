namespace CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types.Implementations;

public abstract class BaseMeasure(IRootObject rootObject, string paramName) : Quantifiable(rootObject, paramName), IBaseMeasure
{
    #region Public methods
    #region Static methods
    public static void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName)
    {
        Type quantityType = NullChecked(quantity, paramName).GetType();

        ValidateQuantityTypeCode(quantityTypeCode, nameof(quantityTypeCode));

        if (quantityTypeCode == Type.GetTypeCode(quantityType)) return;

        throw ArgumentTypeOutOfRangeException(paramName, quantity!);
    }

    public static void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName)
    {
        if (GetValidQuantityTypeCodeOrNull(quantityTypeCode) is not null) return;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, paramName);
    }
    #endregion

    #region Override methods
    #region Sealed methods
    public override sealed int CompareTo(IQuantifiable? other)
    {
        return base.CompareTo(other);
    }

    public override sealed bool Equals(IQuantifiable? other)
    {
        return base.Equals(other);
    }

    public override sealed bool? FitsIn(IQuantifiable? other, LimitMode? limitMode)
    {
        return base.FitsIn(other, limitMode);
    }

    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        return base.FitsIn(limiter);
    }

    public override sealed Enum GetBaseMeasureUnit()
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return baseMeasurement.GetBaseMeasureUnit();
    }

    public override sealed decimal GetDefaultQuantity()
    {
        return GetDefaultQuantity(GetBaseQuantity(), GetExchangeRate());
    }

    public override sealed bool IsExchangeableTo(Enum? context)
    {
        return base.IsExchangeableTo(context)
            && IsValidMeasureUnit(getMeasureUnit());

        #region Local methods
        Enum getMeasureUnit()
        {
            MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));

            return measureUnitElements.MeasureUnit;
        }
        #endregion
    }

    public override sealed IBaseMeasure Round(RoundingMode roundingMode)
    {
        ValueType quantity = (ValueType)GetQuantity(roundingMode);
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return GetBaseMeasure(baseMeasurement, quantity);
    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual LimitMode? GetLimitMode()
    {
        return null;
    }
    #endregion

    #region Abstract methods
    public abstract IBaseMeasurement GetBaseMeasurement();
    public abstract IBaseMeasurementFactory GetBaseMeasurementFactory();
    #endregion

    public bool Equals(IBaseMeasure? x, IBaseMeasure? y)
    {
        if (x is null && y is null) return true;

        return x is not null
            && y is not null
            && x.GetRateComponentCode() == y.GetRateComponentCode()
            && x.GetLimitMode() == y.GetLimitMode()
            && x.Equals(y);
    }

    public IBaseMeasure GetBaseMeasure(ValueType quantity)
    {
        Enum measureUnit = GetBaseMeasureUnit();
        IBaseMeasurementFactory factory = GetBaseMeasurementFactory();
        IBaseMeasurement baseMeasurement = factory.CreateBaseMeasurement(measureUnit)!;

        return GetBaseMeasure(baseMeasurement, quantity);
    }

    public IBaseMeasure GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        return GetBaseMeasureFactory().CreateBaseMeasure(baseMeasurement, quantity);
    }

    public decimal GetExchangeRate()
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return baseMeasurement.GetExchangeRate();
    }

    public int GetHashCode([DisallowNull] IBaseMeasure other)
    {
        return HashCode.Combine(other.GetRateComponentCode(), other.GetLimitMode(), other.GetHashCode());
    }

    public TypeCode? GetQuantityTypeCode(object quantity)
    {
        TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

        return GetValidQuantityTypeCodeOrNull(quantityTypeCode);
    }

    public RateComponentCode GetRateComponentCode()
    {
        return GetBaseMeasureFactory().RateComponentCode;
    }

    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        baseMeasurement.ValidateExchangeRate(exchangeRate, paramName);
    }
    #endregion

    #region Protected methods
    #region Static methods
    //protected static object? GetValidQuantityOrNull(IBaseMeasure baseMeasure, object? quantity)
    //{
    //    quantity = ((ValueType?)quantity)?.ToQuantity(baseMeasure.GetQuantityTypeCode());

    //    return baseMeasure.GetRateComponentCode() switch
    //    {
    //        RateComponentCode.Denominator => getValidDenominatorQuantity(),
    //        RateComponentCode.Numerator or
    //        RateComponentCode.Limit => quantity,

    //        _ => throw new InvalidOperationException(null),
    //    };

    //    #region Local methods
    //    object? getValidDenominatorQuantity()
    //    {
    //        if (quantity is null || (decimal)quantity <= 0) return null;

    //        return quantity;
    //    }
    //    #endregion
    //}

    //protected static TComparable ConvertToLimitable<TComparable>(IBaseMeasure<TComparable> limitable, ILimiter limiter)
    //    where TComparable : class, IBaseMeasure
    //{
    //    string paramName = nameof(limiter);

    //    if (NullChecked(limiter, paramName) is IBaseMeasure baseMeasure) return limitable.GetBaseMeasure(baseMeasure);

    //    throw ArgumentTypeOutOfRangeException(paramName, limiter);
    //}
    #endregion
    #endregion

    #region Private methods
    private IBaseMeasureFactory GetBaseMeasureFactory()
    {
        return (IBaseMeasureFactory)GetFactory();
    }
    #region Static methods
    private static TypeCode? GetValidQuantityTypeCodeOrNull(TypeCode quantityTypeCode)
    {
        if (QuantityTypeCodes.Contains(quantityTypeCode)) return quantityTypeCode;

        return null;
    }
    #endregion
    #endregion
}
